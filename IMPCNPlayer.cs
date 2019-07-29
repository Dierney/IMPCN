using System;
using Terraria;
using Terraria.GameInput;
using Terraria.Localization;
using Terraria.ModLoader;

namespace IMPCN
{
    public class IMPCNPlayer : ModPlayer
    {
	    public override void ProcessTriggers(TriggersSet triggersSet)
	    {
		    if (IMPCN.instance.ShowTextKey.JustPressed)
		    {
			    var item = Main.HoverItem;
			    int id;
			    if (item == null)
			    {
				    var tile = Main.tile[Main.mouseX, Main.mouseY];
				    if (tile == null || tile.type == 0)
					    return;
				    id = tile.blockType();
			    }
			    else
			    {
				    id = item.type;
			    }
			    var info = IMPCNExtension.GetItem(id);
			    if (info == null)
			    {
				    var modItem = ItemLoader.GetItem(id);
				    item = modItem.item;
					info = new ItemWithName(
						id: id, 
						clazz: item.GetType().Name, 
						english: modItem.DisplayName.GetDefault(), 
						original: modItem.DisplayName.GetTranslation(GameCulture.Chinese)
					);
			    }
			    QueryItemNameCommand.WriteItem(info);
		    }
	    }

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
