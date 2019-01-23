using System;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

// TODO: Pressed a hotKey to toggle translation texts(Vanilla, IMPCN & ThoriumMod).

namespace IMPCN
{
    internal class IMPCN : Mod
    {

        public static ModHotKey ToggleTranslationTextsHotKey;

        private static Random random = null;
        private static string[] titles = null;

        // Allows the use of Mod Helpers to receive in-game issue reports from players.
        // https://forums.terraria.org/index.php?threads/mod-helpers.63670/#modders
        public static string GithubUserName => "Dierney";
        public static string GithubProjectName => "IMPCN";

        public IMPCN()
        {
        }

        public override void Load()
        {
            ToggleTranslationTextsHotKey = RegisterHotKey("Toggle Translation Texts", "L");

            if (random == null)
            {
                random = new Random();
            }
            //LoadAlternateChinese(LanguageManager.Instance);

            LoadAlternateChinese(LanguageManager.Instance, "Terraria.Localization.Content.");
        }

        public override void Unload()
        {
            ToggleTranslationTextsHotKey = null;
        }

        // Unfortunately this only works on mod reload. It won't work just by changing languages in game. 
        // Prefix is the prefix of the language files, such as "Terraria.Localization.Content."
        private void LoadAlternateChinese(LanguageManager languageManager, string prefix)
        {
            // If Chinese is being loaded.
            if (languageManager.ActiveCulture == GameCulture.Chinese)
            {
                List<string> languageReplacementFilesForCulture = new List<string>();
                foreach (string item in File)
                {
                    if (Path.GetFileNameWithoutExtension(item.Key).StartsWith(prefix + languageManager.ActiveCulture.CultureInfo.Name) && item.Key.EndsWith(".json"))
                    {
                        languageReplacementFilesForCulture.Add(item.Key);
                    }
                }
                foreach (string translationFile in languageReplacementFilesForCulture)
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

                // Replace Chinese game titles.
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
