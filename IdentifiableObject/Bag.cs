using System;
using SwinAdventure;

namespace SwinAdventure
{
    public class Bag : Item, IHaveInventory
    {
        private Inventory _inventory;

        public Bag(string[] ids, string name, string desc) : base(ids, name, desc)
        {
            _inventory = new Inventory();
        }

        public Inventory Inventory => _inventory;

        public override GameObject Locate(string id)
        {
            if (AreYou(id)) return this;
            if (_inventory.HasItem(id)) return _inventory.Fetch(id);
            return null;
        }

        public override string FullDescription
        {
            get
            {
                return $"In the {Name} you can see:\n{_inventory.ItemList}";
            }
        }
    }
}