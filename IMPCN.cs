using System;
using System.IO;
using System.Reflection;
using System.Text;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.Core;

namespace IMPCN
{
    internal class IMPCN : Mod
    {

        public static Random random = null;
        private static string[] titles = null;

        public ModHotKey ShowTextKey { get; private set; }

        // Allows the use of Mod Helpers to receive in-game issue reports from players.
        // https://forums.terraria.org/index.php?threads/mod-helpers.63670/#modders
        public static string GithubUserName => "Dierney";
        public static string GithubProjectName => "IMPCN";

        public static IMPCN instance;

        public IMPCN()
        {
        }

        public override void Load()
        {
            instance = this;

            IMPCNExtension.Load();

            Main.versionNumber = "v1.3.5.2\nIMPCN v" + instance.Version.ToString();
            Main.versionNumber2 = "v1.3.5.2\nIMPCN v" + instance.Version.ToString();

            ShowTextKey = RegisterHotKey("查询物品名称(鼠标悬停)", "Z");

            // The new version of tModLoader has been updated C#.
            if (ModLoader.version < new Version(0, 11))
            {
                throw new Exception("\nThis mod uses functionality only present in the latest tModLoader. Please update tModLoader to use this mod\n\n");
            }

            if (random == null)
            {
                random = new Random();
            }
            //LoadAlternateChinese(LanguageManager.Instance);

            if (LanguageManager.Instance.ActiveCulture == GameCulture.Chinese)
            {
                Mod thoriumMod = ModLoader.GetMod("ThoriumMod");
                Version fixed_vers = new Version(1, 5, 1, 2);
                // ThoriumMod fixed the bug in version 1.5.1.2.
                // If exists ThoriumMod, and its version lower than 1.5.1.2.
                if (thoriumMod != null && thoriumMod.Version < fixed_vers)
                {
                    LoadAlternateChinese(LanguageManager.Instance, "Terraria.Localization.ContentForThoriumMod.");
                    // see RemarkOfThoriumMod.txt for details.
                }

                else
                {
                    LoadAlternateChinese(LanguageManager.Instance, "Terraria.Localization.Content.");
                }
            }
        }

        public override void Unload()
        {
            ShowTextKey = null;
        }

        // Unfortunately this only works on mod reload. It won't work just by changing languages in game. 
        // Prefix is the prefix of the language files, such as "Terraria.Localization.Content."
        private void LoadAlternateChinese(LanguageManager languageManager, string prefix)
        {
            // If Chinese is being loaded.
            if (languageManager.ActiveCulture == GameCulture.Chinese)
            {
                foreach (TmodFile.FileEntry item in
                    typeof(Mod)
                    .GetProperty("File", BindingFlags.NonPublic | BindingFlags.Instance)
                    .GetValue(this) as TmodFile)
                {
                    if (Path.GetFileNameWithoutExtension(item.Name).StartsWith(prefix + languageManager.ActiveCulture.CultureInfo.Name) && item.Name.EndsWith(".json"))
                    {
                        try
                        {
                            languageManager.LoadLanguageFromFileText(Encoding.UTF8.GetString(GetFileBytes(item.Name)));
                        }
                        catch
                        {
                            Logger.InfoFormat("Failed to load language file: " + item);
                        }
                    }
                }

                if (LanguageManager.Instance.ActiveCulture == GameCulture.Chinese)
                {
                    // Replace Chinese game titles.
                    if (!Main.dedServ)
                    {
                        if (titles == null)
                        {
                            titles = Encoding.UTF8.GetString(GetFileBytes("GameTitles.txt")).Split('\n');
                        }

                        Main.instance.Window.Title = titles[random.Next(titles.Length)];
                    }
                }
            }
        }
    }
}
