using System;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace IMPCN
{
    internal class IMPCNPlayer : ModPlayer
    {

        public override void OnEnterWorld(Player player)
        {
            if (LanguageManager.Instance.ActiveCulture == GameCulture.Chinese)
            {

                Main.NewText("欢迎使用改良中文，请尽情享用吧！", 67, 110, 238, false);

                Mod thoriumMod = ModLoader.GetMod("ThoriumMod");
                Version fixed_vers = new Version(1, 5, 1, 2);
                if (thoriumMod != null && thoriumMod.Version < fixed_vers)
                {
                    Main.NewText("检测到你已启用ThoriumMod，且版本低于1.5.1.2，为修复异常崩溃Bug已修改某些文本(详见RemarkOfThoriumMod.txt)", 255, 20, 147, false);
                }
            }

            else
            {
                Main.NewText("You didn't select Chinese, so the Improvement of Chinese won't take effect.", 67, 110, 238, false);
            }
        }
    }
}
