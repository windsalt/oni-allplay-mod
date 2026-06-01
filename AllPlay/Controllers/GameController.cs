using System.Net;
using System.Threading.Tasks;
using AllPlay.Mcp;

namespace AllPlay.Controllers
{
    [McpRoutePrefix("game")]
    public class GameController
    {
        [McpRoute("GET", "speed")]
        public static async Task GetSpeed(HttpListenerRequest req, HttpListenerResponse res)
        {
            var game = new Utils.GameUtil();
            await McpHttpServer.SendResponse(res, game.GetSpeed());
        }

        [McpRoute("GET", "save")]
        public static async Task GetSave(HttpListenerRequest req, HttpListenerResponse res)
        {
            var game = new Utils.GameUtil();

            await McpHttpServer.SendResponse(res, game.GetSave());
        }

        public static async Task SetSaveTime(HttpListenerRequest req, HttpListenerResponse res) { }
    }
}
