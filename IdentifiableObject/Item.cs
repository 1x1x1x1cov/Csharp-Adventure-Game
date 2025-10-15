using System;

namespace SwinAdventure
{
    public class Item : GameObject
    {
        public Item(string[] idents, string name, string desc) : base(idents, name, desc) {}

        public void PrivilegeEscalation(string pin)
        {
            if (pin == "6789")
            {
                ReplaceFirstIdentifier("NewTutorialID");
            }
        }

        public static bool AddItem(List<Item> items, Item newItem)
        {
            if (items.Exists(item => item.FirstId == newItem.FirstId))
            {
                return false;
            }
            items.Add(newItem);
            return true;
        }

        public virtual GameObject Locate(string id)
        {
        return null;
        }

    }
}