using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

// 注:以下代码是ai生成的
namespace AllPlay.mcp
{
    [AttributeUsage(AttributeTargets.Class)]
    public class McpRoutePrefixAttribute : Attribute
    {
        public string Prefix { get; }

        public McpRoutePrefixAttribute(string prefix)
        {
            Prefix = prefix.Trim('/').ToLower();
        }
    }

    [AttributeUsage(AttributeTargets.Method)]
    public class McpRouteAttribute : Attribute
    {
        public string Method { get; }
        public string Path { get; }

        public McpRouteAttribute(string method, string path)
        {
            Method = method;
            Path = path;
        }
    }

    public static class McpHttpServer
    {
        private static HttpListener _listener;
        private static bool _running;
        private static readonly Dictionary<(string method, string path), RouteHandler> _routes =
            new Dictionary<(string, string), RouteHandler>();

        private delegate Task RouteHandler(
            HttpListenerRequest request,
            HttpListenerResponse response
        );

        static McpHttpServer()
        {
            RegisterRoutes();
        }

        private static void RegisterRoutes()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in assemblies)
            {
                foreach (var type in assembly.GetTypes())
                {
                    var prefixAttr = type.GetCustomAttribute<McpRoutePrefixAttribute>();
                    var prefix = prefixAttr != null ? prefixAttr.Prefix : "";

                    foreach (
                        var method in type.GetMethods(BindingFlags.Public | BindingFlags.Static)
                    )
                    {
                        var routeAttr = method.GetCustomAttribute<McpRouteAttribute>();
                        if (routeAttr != null)
                        {
                            var fullPath = string.IsNullOrEmpty(prefix)
                                ? routeAttr.Path.Trim('/').ToLower()
                                : $"{prefix}/{routeAttr.Path.Trim('/').ToLower()}";

                            var key = (routeAttr.Method.ToUpper(), fullPath);
                            _routes[key] = (req, res) =>
                                (Task)method.Invoke(null, new object[] { req, res });
                        }
                    }
                }
            }
        }

        public static async void Start()
        {
            if (_running)
                return;
            _running = true;

            _listener = new HttpListener();
            _listener.Prefixes.Add("http://localhost:8080/");
            _listener.Start();

            UnityEngine.Debug.Log("[MCP] 服务已启动：http://localhost:8080");

            while (_running)
            {
                try
                {
                    var ctx = await _listener.GetContextAsync();
                    _ = HandleRequest(ctx);
                }
                catch
                {
                    break;
                }
            }
        }

        public static void Stop()
        {
            _running = false;
            _listener?.Stop();
        }

        private static async Task HandleRequest(HttpListenerContext ctx)
        {
            try
            {
                var req = ctx.Request;
                var res = ctx.Response;

                if (req.Url == null)
                {
                    await SendResponse(res, new { error = "Invalid request" });
                    return;
                }

                var path = req.Url.AbsolutePath.Trim('/').ToLower();
                var method = req.HttpMethod.ToUpper();

                UnityEngine.Debug.Log($"[MCP] {method} {path}");

                var key = (method, path);
                if (_routes.TryGetValue(key, out var handler))
                {
                    await handler(req, res);
                }
                else
                {
                    await SendResponse(res, new { error = "404 not found" });
                }
            }
            catch (Exception ex)
            {
                UnityEngine.Debug.LogError("[MCP错误] " + ex.Message);
            }
        }

        public static async Task SendResponse(HttpListenerResponse res, object data)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(
                JsonConvert.SerializeObject(data, Formatting.None)
            );

            res.ContentType = "application/json";
            res.ContentLength64 = bytes.Length;
            await res.OutputStream.WriteAsync(bytes, 0, bytes.Length);
            res.Close();
        }
    }
}
