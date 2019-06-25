
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace IMPCN
{
	public static class Util
	{
		public static List<TooltipLine> AddLineWithColor(this List<TooltipLine> lines, TooltipLine msg, Color color)
		{
			msg.overrideColor = color;
			lines.Add(msg);
			return lines;
		}

		public static Color RandomColor()
		{
			byte[] b = new byte[3];
			IMPCN.random.NextBytes(b);
			return new Color(b[0], b[1], b[2]);
		}
	}
}
