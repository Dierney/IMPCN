using Terraria.ModLoader;
using Terraria.Localization;
using Terraria;

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
    }
}
