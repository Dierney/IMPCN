using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace IMPCN
{
	public class IMPCNItem : GlobalItem
	{
		public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
		{
			if (IMPCN.instance.GetPlayer<IMPCNPlayer>().ShowInTooltip)
			{
				var i = IMPCNExtension.GetItem(item.netID);
				if (i == null)
					return;
				Color c = Util.RandomColor();
				tooltips.AddLineWithColor(new TooltipLine(mod, mod.Name, "ID: " + i.ID), c);
				tooltips.AddLineWithColor(new TooltipLine(mod, mod.Name, "改良：" + i.ImprovedName), c);
				tooltips.AddLineWithColor(new TooltipLine(mod, mod.Name, "旧版(v1.3.5.1)：" + i.OldImproved1351Name), c);
				tooltips.AddLineWithColor(new TooltipLine(mod, mod.Name, "旧版(v1.3.4.4)：" + i.OldImproved1344Name), c);
				tooltips.AddLineWithColor(new TooltipLine(mod, mod.Name, "原版：" + i.OriginalName), c);
				tooltips.AddLineWithColor(new TooltipLine(mod, mod.Name, "英文：" + i.EnglishName), c);
				tooltips.AddLineWithColor(new TooltipLine(mod, mod.Name, "类名：" + i.ClassName), c);
			}
		}
	}
}
