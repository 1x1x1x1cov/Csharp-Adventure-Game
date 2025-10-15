using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SwinAdventure
{
    public class Inventory
    {
        private List<Item> _items;

        public Inventory()
        {
            _items = new List<Item>();
        }

        public bool HasItem(string id)
        {
            return _items.Any(item => item.AreYou(id));
        }

        public void Put(Item itm)
        {
            if (!HasItem(itm.FirstId))
            {
                _items.Add(itm);
            }
        }

        public Item Take(string id)
        {
            var itm = Fetch(id);
            if (itm != null)
            {
                _items.Remove(itm);
            }
            return itm;
        }

        public Item Fetch(string id)
        {
            return _items.FirstOrDefault(item => item.AreYou(id));
        }

        public string ItemList => string.Join(",", _items.Select(i => i.ShortDescription));


        public void RemoveItems(List<Item> itmList, string studentId)
        {
            bool hasMatch = itmList.Any(i => i.FirstId == studentId);
            if (hasMatch)
            {
                if (_items.Count > 0) _items.RemoveAt(0);
                if (_items.Count > 0) _items.RemoveAt(_items.Count - 1);
            }
            else
            {
                foreach (var item in itmList)
                {
                    _items.Remove(item);
                }
            }
        }

        public List<Item> FetchAll()
        {
        return _items;
        }
    }
}