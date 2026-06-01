using System;
using System.Net;
using System.Threading.Tasks;
using AllPlay.Mcp;

namespace AllPlay.Controllers
{
    public struct Doc
    {
        public string Url;
        public string desc;
    }

    [McpRoutePrefix("doc")]
    public class DocController
    {
        [McpRoute("GET", "all")]
        public static async Task GetDoc(HttpListenerRequest req, HttpListenerResponse res) { }
    }
}
