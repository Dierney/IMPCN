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
        }
    }
}
