using System;
using System.IO;

namespace SwinAdventure
{
    public class Player : GameObject, IHaveInventory
    {
        private Inventory _inventory;
        private Bag _attachedBag;
        private Location _location;

        public Player(string name, string desc) : base(new[] { "me", "inventory" }, name, desc)
        {
            _inventory = new Inventory();
        }

        public Inventory Inventory => _inventory;
        public Bag AttachedBag => _attachedBag;

        public Location Location => _location;

        public void AttachBag(Bag bag)
        {
            _attachedBag = bag;
        }

        public void SetLocation(Location location)
        {
            _location = location;
        }

        public GameObject Locate(string id)
        {
            if (AreYou(id))
                return this;

            GameObject found = _inventory.Fetch(id);
            if (found != null)
                return found;

            if (_attachedBag != null)
            {
                GameObject bagResult = _attachedBag.Locate(id);
                if (bagResult != null)
                    return bagResult;
            }

            if (_location != null)
            {
                GameObject locResult = _location.Locate(id);
                if (locResult != null)
                    return locResult;
            }

            return null;
        }

        public override string FullDescription
        {
            get
            {
                string desc = $"You are {Name} {base.FullDescription}\nYou are carrying:\n{_inventory.ItemList}";

                if (_attachedBag != null)
                {
                    desc += $"\n\nContents of {_attachedBag.Name}:\n{_attachedBag.Inventory.ItemList}";
                }

                return desc;
            }
        }

        public override void SaveTo(StreamWriter writer)
        {
            base.SaveTo(writer);
            writer.WriteLine(_inventory.ItemList);
        }

        public override void LoadFrom(StreamReader reader)
        {
            base.LoadFrom(reader);
            string itemList = reader.ReadLine();
            Console.WriteLine("Player information");
            Console.WriteLine(Name);
            Console.WriteLine(ShortDescription);
            Console.WriteLine(itemList);
        }
    }
}
