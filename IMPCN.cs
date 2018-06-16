using Terraria.ModLoader;
using Terraria.Localization;
using System;
using System.Collections.Generic;
using System.IO;

namespace IMPCN
{
	class IMPCN : Mod
	{
		public IMPCN()
		{
		}

		public override void Load()
		{
			LoadAlternateChinese(LanguageManager.Instance);
		}

		// Unfortunately this only works on mod reload. It won't work just by changing languages in game. 
		private void LoadAlternateChinese(LanguageManager languageManager)
		{
			// If Chinese is being loaded.
			if (languageManager.ActiveCulture == GameCulture.Chinese)
			{
				var languageReplacementFilesForCulture = new List<string>();
				foreach (var item in File)
				{
					if (Path.GetFileNameWithoutExtension(item.Key).StartsWith("Terraria.Localization.Content." + languageManager.ActiveCulture.CultureInfo.Name) && item.Key.EndsWith(".json"))
						languageReplacementFilesForCulture.Add(item.Key);
				}
				foreach (var translationFile in languageReplacementFilesForCulture)
				{
					try
					{
						string translationFileContents = System.Text.Encoding.UTF8.GetString(GetFileBytes(translationFile));
						languageManager.LoadLanguageFromFileText(translationFileContents);
					}
					catch (Exception)
					{
						ErrorLogger.Log("无法加载语言文件: " + translationFile);
						break;
					}
				}
			}
		}
	}
}
