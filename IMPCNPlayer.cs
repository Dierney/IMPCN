using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameInput;

namespace IMPCN
{

    class IMPCNPlayer : ModPlayer
    {

        public override void OnEnterWorld(Player player)
        {
            Main.NewText(Language.GetTextValue("欢迎使用改良中文，请尽情享用吧！"), 67, 110, 238, false);

            if (ModLoader.GetMod("ThoriumMod") != null)
            {
                Main.NewText(Language.GetTextValue("检测到你已启用ThoriumMod，为修复异常崩溃Bug已修改某些文本(详见RemarkOfThoriumMod.txt)"), 255, 20, 147, false);
            }
        }

        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if (IMPCN.ToggleTranslationTextsHotKey.JustPressed)
            { 
                Main.NewText(Language.GetTextValue("此功能未实现，敬请期待......"), 255, 48, 48, false);
            }
        }
    }
}
