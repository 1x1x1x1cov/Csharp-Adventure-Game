using NUnit.Framework;
using SwinAdventure;

namespace SwinAdventure.Tests
{
    [TestFixture]
    public class BagTests
    {
        private Bag _toolBag;
        private Bag _foodBag;
        private Item _item1;
        private Item _item2;

        [SetUp]
        public void Setup()
        {
            _toolBag = new Bag(new[] { "toolbag" }, "Tool Bag", "Bag for tools");
            _foodBag = new Bag(new[] { "foodbag" }, "Food Bag", "Bag for food");
            _item1 = new Item(new[] { "hammer" }, "A Hammer", "Useful for building");
            _item2 = new Item(new[] { "apple" }, "An Apple", "A juicy red apple");
            _toolBag.Inventory.Put(_item1);
            _toolBag.Inventory.Put(_item2);
        }

        [Test]
        public void BagLocatesItems()
        {
            Assert.That(_toolBag.Locate("hammer"), Is.EqualTo(_item1));
            Assert.That(_toolBag.Locate("apple"), Is.EqualTo(_item2));
            Assert.That(_toolBag.Inventory.HasItem("apple"), Is.True);
        }

        [Test]
        public void BagLocatesItself()
        {
            Assert.That(_toolBag.Locate("toolbag"), Is.EqualTo(_toolBag));
        }

        [Test]
        public void BagLocatesNothing()
        {
            Assert.That(_toolBag.Locate("banana"), Is.Null);
        }

        [Test]
        public void BagFullDescription()
        {
            string desc = _toolBag.FullDescription;
            Assert.That(desc, Does.Contain("In the Tool Bag you can see:"));
            Assert.That(desc, Does.Contain("A Hammer"));
            Assert.That(desc, Does.Contain("An Apple"));
        }

        [Test]
        public void BagInBag()
        {
            _toolBag.Inventory.Put(_foodBag);
            Assert.That(_toolBag.Locate("foodbag"), Is.EqualTo(_foodBag));
            Assert.That(_toolBag.Locate("hammer"), Is.EqualTo(_item1));
            Assert.That(_toolBag.Locate("apple"), Is.EqualTo(_item2));
            Assert.That(_toolBag.Locate("itemInB2"), Is.Null); // placeholder check
        }

        [Test]
        public void BagInBagWithPrivilegedItem()
        {
            Bag b1 = new Bag(new[] { "b1" }, "B1", "Outer bag");
            Bag b2 = new Bag(new[] { "b2" }, "B2", "Inner bag");
            Item secretItem = new Item(new[] { "secret" }, "Secret Doc", "Top secret data");
            secretItem.PrivilegeEscalation("6789");
            b2.Inventory.Put(secretItem);
            b1.Inventory.Put(b2);
            Assert.That(b1.Locate("secret"), Is.Null);
        }
    }
}
