using Terraria;
using Terraria.GameInput;
using Terraria.ModLoader;

namespace IMPCN
{
    internal class IMPCNPlayer : ModPlayer
    {

        public override void OnEnterWorld(Player player)
        {
            Main.NewText("欢迎使用改良中文，请尽情享用吧！", 67, 110, 238, false);
        }

        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if (IMPCN.ToggleTranslationTextsHotKey.JustPressed)
            {
                Main.NewText("此功能未实现，敬请期待......", 255, 48, 48, false);
            }
        }
    }
}
