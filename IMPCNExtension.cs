using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.Core;

namespace IMPCN
{
    internal class IMPCNExtension
    {
        public static Dictionary<string, string> improvedDict;
        public static Dictionary<string, string> old_improved_1351Dict;
        public static Dictionary<string, string> old_improved_1344Dict;
        public static Dictionary<string, string> originalDict;
        public static Dictionary<string, string> englishDict;
        public static Dictionary<int, string> idDict;
        public static Dictionary<string, int> getIdDict;

        public static bool enabled;

        public static bool HasLoaded { get; private set; }

        public static void Load()
        {
            HasLoaded = true;
            improvedDict = new Dictionary<string, string>();
            old_improved_1351Dict = new Dictionary<string, string>();
            old_improved_1344Dict = new Dictionary<string, string>();
            originalDict = new Dictionary<string, string>();
            englishDict = new Dictionary<string, string>();
            idDict = new Dictionary<int, string>();
            getIdDict = new Dictionary<string, int>();

            Internal_LoadItemID();
            Internal_LoadImproved();
            Internal_LoadOldImproved1351Name();
            Internal_LoadOldImproved1344Name();
            Internal_LoadOriginal();
            Internal_LoadEnglish();

            enabled = true;
        }

        public static void Unload()
        {
            HasLoaded = false;
            improvedDict = null;
            old_improved_1351Dict = null;
            old_improved_1344Dict = null;
            originalDict = null;
            englishDict = null;
            idDict = null;
            getIdDict = null;
        }

        public static HashSet<ItemWithName> GetExactItem(string name)
        {
            HashSet<ItemWithName> resultSet = new HashSet<ItemWithName>();
            if (int.TryParse(name, out int res))
            {
                ItemWithName i = GetItem(res);
                if (i != null)
                {
                    resultSet.Add(i);
                    return resultSet;
                }

                return null;
            }

            if (!name.Contains(' '))
            {
                ItemWithName i = GetItem(name);
                if (i != null)
                {
                    resultSet.Add(i);
                    return resultSet;
                }
            }

            if (Regex.IsMatch(name, "^[0-9a-zA-Z_ ]+$"))
            {
                foreach (KeyValuePair<string, string> v in englishDict)
                {
                    if (v.Value.ToLower() == name.ToLower())
                    {
                        string className = v.Key;
                        resultSet.Add(GetItem(className));
                    }
                }

                return resultSet;
            }

            foreach (KeyValuePair<string, string> v in originalDict)
            {
                if (v.Value == name)
                {
                    string className = v.Key;
                    resultSet.Add(GetItem(className));
                }
            }

            foreach (KeyValuePair<string, string> v in improvedDict)
            {
                if (v.Value == name)
                {
                    string className = v.Key;
                    resultSet.Add(GetItem(className));
                }
            }

            return resultSet;
        }

        public static ItemWithName GetItem(string className)
        {
            if (!getIdDict.ContainsKey(className))
            {
                return null;
            }

            int id = getIdDict[className];
            string improved = improvedDict.ContainsKey(className) ? improvedDict[className] : null;
            string old_improved_1351 = old_improved_1351Dict.ContainsKey(className) ? old_improved_1351Dict[className] : null;
            string old_improved_1344 = old_improved_1344Dict.ContainsKey(className) ? old_improved_1344Dict[className] : null;
            string original = originalDict.ContainsKey(className) ? originalDict[className] : null;
            string english = englishDict.ContainsKey(className) ? englishDict[className] : null;
            return new ItemWithName(id, className, improved, old_improved_1351, old_improved_1344, original, english);
        }

        public static ItemWithName GetItem(int id)
        {
            if (!idDict.ContainsKey(id))
            {
                return null;
            }

            string className = idDict[id];
            return GetItem(className);
        }

        public static HashSet<ItemWithName> GetItems(string text)
        {
            HashSet<ItemWithName> resultSet = new HashSet<ItemWithName>();
            if (int.TryParse(text, out int res))
            {
                ItemWithName i = GetItem(res);
                if (i != null)
                {
                    resultSet.Add(i);
                    return resultSet;
                }

                return null;
            }

            if (!text.Contains(' '))
            {
                ItemWithName i = GetItem(text);
                if (i != null)
                {
                    resultSet.Add(i);
                    return resultSet;
                }
            }

            if (Regex.IsMatch(text, "^[0-9a-zA-Z_ ]+$"))
            {
                Internal_SearchAndAddEnglish(text, englishDict, resultSet);
                return resultSet;
            }

            Internal_SearchAndAdd(text, improvedDict, resultSet);
            Internal_SearchAndAdd(text, originalDict, resultSet);
            return resultSet;
        }

        private static void Internal_SearchAndAddEnglish(string text, Dictionary<string, string> dict,
            HashSet<ItemWithName> resultSet)
        {
            foreach (KeyValuePair<string, string> v in dict)
            {
                if (v.Value.ToLower().Contains(text.ToLower()))
                {
                    string className = v.Key;
                    resultSet.Add(GetItem(className));
                }
            }
        }

        private static void Internal_SearchAndAdd(string text, Dictionary<string, string> dict,
            HashSet<ItemWithName> resultSet)
        {
            foreach (KeyValuePair<string, string> v in dict)
            {
                if (v.Value.Contains(text))
                {
                    string className = v.Key;
                    resultSet.Add(GetItem(className));
                }
            }
        }

        private static void Internal_LoadImproved()
        {
            Internal_LoadJson("improved.json", improvedDict);
        }

        private static void Internal_LoadOldImproved1351Name()
        {
            Internal_LoadJson("old_improved_1351.json", old_improved_1351Dict);
        }

        private static void Internal_LoadOldImproved1344Name()
        {
            byte[] raw = null;
            TmodFile file1 = typeof(Mod).GetProperty("File", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(IMPCN.instance) as TmodFile;
            foreach (TmodFile.FileEntry item in file1)
            {
                if (item.Name.EndsWith("old_improved_1344.json"))
                {
                    raw = file1.GetBytes(item);
                    break;
                }
            }
            JArray jarr = JArray.Parse(Encoding.UTF8.GetString(raw));
            foreach (JObject jobj in jarr)
            {
                try
                {
                    int id = jobj["Id"].Value<int>();
                    string className = idDict[id];
                    string content = jobj["Content"].Value<string>();
                    old_improved_1344Dict.Add(className, content);
                }
                catch
                {

                }
            }
        }

        private static void Internal_LoadOriginal()
        {
            Internal_LoadJson("original.json", originalDict);
        }

        private static void Internal_LoadEnglish()
        {
            Internal_LoadJson("english.json", englishDict);
        }

        private static void Internal_LoadJson(string file, Dictionary<string, string> dict)
        {
            byte[] raw = null;
            TmodFile file1 = typeof(Mod).GetProperty("File", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(IMPCN.instance) as TmodFile;
            foreach (TmodFile.FileEntry item in file1)
            {
                if (item.Name.EndsWith(file))
                {
                    raw = file1.GetBytes(item);
                    break;
                }
            }
            JObject jobj = JObject.Parse(Encoding.UTF8.GetString(raw));

            foreach (JProperty item in jobj["ItemName"])
            {
                try
                {
                    Main.NewText("Adding: " + item.Name);
                    dict.Add(item.Name, item.Value.Value<string>());
                }
                catch (Exception)
                {
                    continue;
                }
            }
        }

        private static void Internal_LoadItemID()
        {
            byte[] raw = null;
            TmodFile file1 = typeof(Mod).GetProperty("File", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(IMPCN.instance) as TmodFile;
            foreach (TmodFile.FileEntry item in file1)
            {
                if (item.Name.EndsWith("itemID.txt"))
                {
                    raw = file1.GetBytes(item);
                    break;
                }
            }
            string[] data = Encoding.UTF8.GetString(raw).Split('\n');
            foreach (string line in data)
            {
                string[] d = line.Split(',');
                if (d.Length != 2)
                {
                    HasLoaded = false;
                    throw new Exception("Fuck me.");
                }

                string className = d[0];
                int itemID = int.Parse(d[1]);
                try
                {
                    idDict.Add(itemID, className);
                    getIdDict.Add(className, itemID);
                }
                catch (Exception)
                {
                }
            }
        }
    }
}
