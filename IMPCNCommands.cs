using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace IMPCN
{
    internal class QueryItemNameCommand : CommandTemplate
    {
        public QueryItemNameCommand()
        {
            name = "impcn";
            argstr = "[ItemName]";
            desc = "查询物品名 (ID,改良,旧版,原版,英文,类名)";
        }

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            if (args.Length != 1)
            {
                Main.NewText("非法参数", Colors.RarityRed);
                return;
            }

            if (IMPCNExtension.enabled)
            {
                System.Collections.Generic.HashSet<ItemWithName> item = IMPCNExtension.GetExactItem(args[0].Replace('_', ' '));
                if (item == null)
                {
                    Main.NewText("未找到该物品", Colors.RarityRed);
                    return;
                }
                Main.NewText("找到" + item.Count + "个结果");
                foreach (ItemWithName ii in item)
                {
                    WriteItem(ii);
                }
                return;
            }
        }

        public static void WriteItem(ItemWithName item)
        {
            byte[] color = new byte[3];
            IMPCN.random.NextBytes(color);
            Main.NewText("ID: " + item.ID, color[0], color[1], color[2]);
            Main.NewText("改良：" + item.ImprovedName, color[0], color[1], color[2]);
            Main.NewText("旧版(v1.3.5.1)：" + item.OldImproved1351Name, color[0], color[1], color[2]);
            Main.NewText("旧版(v1.3.4.4)：" + item.OldImproved1344Name, color[0], color[1], color[2]);
            Main.NewText("原版：" + item.OriginalName, color[0], color[1], color[2]);
            Main.NewText("英文：" + item.EnglishName, color[0], color[1], color[2]);
            Main.NewText("类名：" + item.ClassName, color[0], color[1], color[2]);
        }
    }

    public abstract class CommandTemplate : ModCommand
    {
        public string name, argstr, desc;

        public override CommandType Type => CommandType.Chat;

        public override string Command => name;

        public override string Usage => string.Format("/{0} {1}", name, argstr);

        public override string Description => string.Format("{0}", desc);
    }
}
