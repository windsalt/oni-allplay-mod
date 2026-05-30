using System.Net;
using System.Threading.Tasks;
using AllPlay.Mcp;

namespace AllPlay.Controllers
{
    [McpRoutePrefix("duplicant")]
    public class DuplicantController
    {
        [McpRoute("GET", "info")]
        public static async Task GetAllInfo(HttpListenerRequest req, HttpListenerResponse res)
        {
            var duplicantService = new Service.DuplicantService();
            var info = duplicantService.GetAllInfo();

            await McpHttpServer.SendResponse(res, info);
        }

        // TODO 通过ID给复制人改名
        [McpRoute("POST", "rename")]
        public static async Task ReName(HttpListenerRequest req, HttpListenerResponse res)
        {
            var query = req.QueryString;
            var flag = false;

            var id = query["id"];
            var name = query["name"];
            var duplicantService = new Service.DuplicantService();

            if (int.TryParse(id, out int duplicantId))
            {
                Debug.Log("字符转数字成功");
                Debug.Log(duplicantId);
                flag = duplicantService.ReName(duplicantId, name);
            }

            await McpHttpServer.SendResponse(res, flag);
        }
    }
}
