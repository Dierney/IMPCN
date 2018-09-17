using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace IMPCN
{
    
    class IMPCN : Mod
    {

        private static Random random = null;
        private static string[] titles = null;

        public IMPCN()
        {
        }

		public override void Load()
        {
            if (random == null) random = new Random();
            //LoadAlternateChinese(LanguageManager.Instance);
            
            // If exists Thorium Mod
            if (ModLoader.GetMod("ThoriumMod") != null)
            {
                LoadAlternateChinese(LanguageManager.Instance, "Terraria.Localization.ContentForThoriumMod.");
                // After selecting Chinese, the latest version of Thorium Mod will have an InvalidOperationException when querying the recipe for some items(e.g. Unholy shards).
            }

            else
            {
                LoadAlternateChinese(LanguageManager.Instance, "Terraria.Localization.Content.");
            }
        }

        // Unfortunately this only works on mod reload. It won't work just by changing languages in game. 
        // Prefix is the prefix of the language files, such as "Terraria.Localization.Content."
        private void LoadAlternateChinese(LanguageManager languageManager, string prefix)
		{
			// If Chinese is being loaded.
			if (languageManager.ActiveCulture == GameCulture.Chinese)
			{
				var languageReplacementFilesForCulture = new List<string>();
				foreach (var item in File)
				{
					if (Path.GetFileNameWithoutExtension(item.Key).StartsWith(prefix + languageManager.ActiveCulture.CultureInfo.Name) && item.Key.EndsWith(".json"))
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
						ErrorLogger.Log("Failed to load language file: " + translationFile);
						break;
					}
				}
                // Replace game Chinese titles.
                if (!Main.dedServ)
                {
                    if (titles == null)
                    {
                        titles = System.Text.Encoding.UTF8.GetString(GetFileBytes("GameTitles.txt")).Split('\n');
                    }

                    Main.instance.Window.Title = titles[random.Next(titles.Length)];
                }
            }
		}
    }
}
