namespace IMPCN
{
    internal class ItemWithName
    {
        public readonly int ID;

        public readonly string ClassName;

        public readonly string ImprovedName;

        public readonly string OldImproved1351Name;

        public readonly string OldImproved1344Name;

        public readonly string OriginalName;

        public readonly string EnglishName;

        public ItemWithName(int id, string clazz, string improved, string old_improved_1351, string old_improved_1344, string original, string english)
        {
            ID = id;
            ClassName = clazz;
            ImprovedName = improved;
            OldImproved1351Name = old_improved_1351;
            OldImproved1344Name = old_improved_1344;
            OriginalName = original;
            EnglishName = english;
        }

        public override int GetHashCode()
        {
            return ID;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (obj.GetType() != typeof(ItemWithName))
            {
                return false;
            }

            ItemWithName i = (ItemWithName)obj;
            return i.ID == ID;
        }
    }
}
