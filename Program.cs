using HarmonyLib;
using KMod;

namespace AllPlay
{
    public class AllPlayModInfo : UserMod2
    {
        public override void OnLoad(Harmony harmony)
        {
            base.OnLoad(harmony);

            Mcp.McpHttpServer.Start();
        }
    }
}
