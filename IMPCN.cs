using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using System;
using System.Collections.Generic;
using System.IO;

namespace IMPCN
{
    
    class IMPCN : Mod
    {

        private Random random;

        public IMPCN()
		{
		}

		public override void Load()
		{
            random = new Random();
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
                // Replace game Chinese titles.
                if (!Main.dedServ)
                {
                    string t = "未加载标题文件。";
                    foreach (var file in File)
                    {
                        if (Path.GetFileName(file.Key) == "GameTitles.txt")
                            t = System.Text.Encoding.UTF8.GetString(GetFileBytes(file.Key));
                    }
                    var texts = t.Split('\n');

                    Main.instance.Window.Title = texts[random.Next(texts.Length)];
                }
            }
		}
    }
}
