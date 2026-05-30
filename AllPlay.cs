using HarmonyLib;
using KMod;

namespace AllPlay
{
    public class ModInfo : UserMod2
    {
        public override void OnLoad(Harmony harmony)
        {
            base.OnLoad(harmony);

            mcp.McpHttpServer.Start();
        }
    }
}
