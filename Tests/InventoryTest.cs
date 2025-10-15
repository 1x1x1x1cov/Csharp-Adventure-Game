using NUnit.Framework;
using SwinAdventure;
using System.Collections.Generic;

namespace SwinAdventure.Tests
{
    [TestFixture]
    public class InventoryTests
    {
        private Inventory _inventory;
        private Item _item1;
        private Item _item2;

        [SetUp]
        public void Setup()
        {
            _inventory = new Inventory();
            _item1 = new Item(new[] { "sword" }, "Bronze Sword", "A sharp bronze sword");
            _item2 = new Item(new[] { "shield" }, "Silver Shield", "A sturdy silver shield");
        }

        [Test]
        public void TestPutAndHasItem()
        {
            _inventory.Put(_item1);
            Assert.That(_inventory.HasItem("sword"), Is.True);
        }

        [Test]
        public void TestPreventDuplicateItems()
        {
            _inventory.Put(_item1);
            _inventory.Put(_item1);
            Assert.That(_inventory.ItemList.Split('\n').Length, Is.EqualTo(1));
        }

        [Test]
        public void TestFetchItem()
        {
            _inventory.Put(_item2);
            var fetched = _inventory.Fetch("shield");
            Assert.That(fetched, Is.EqualTo(_item2));
        }

        [Test]
        public void TestTakeItem()
        {
            _inventory.Put(_item1);
            var taken = _inventory.Take("sword");
            Assert.That(taken, Is.EqualTo(_item1));
            Assert.That(_inventory.HasItem("sword"), Is.False);
        }

        [Test]
        public void TestItemList()
        {
            _inventory.Put(_item1);
            _inventory.Put(_item2);
            string list = _inventory.ItemList;
            Assert.That(list, Does.Contain("Bronze Sword"));
            Assert.That(list, Does.Contain("Silver Shield"));
        }
    }
}