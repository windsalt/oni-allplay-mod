using HarmonyLib;
using KMod;

namespace allplay
{
    public class AllPlayModInfo : UserMod2
    {
        public override void OnLoad(Harmony harmony)
        {
            base.OnLoad(harmony);

            mcp.McpHttpServer.Start();
        }
    }
}
