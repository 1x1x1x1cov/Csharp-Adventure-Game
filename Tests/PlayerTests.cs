using NUnit.Framework;
using SwinAdventure;

namespace SwinAdventure.Tests
{
    [TestFixture]
    public class PlayerTests
    {
        private Player _player;
        private Item _item1;
        private Item _item2;

        [SetUp]
        public void Setup()
        {
            _player = new Player("James", "an explorer");
            _item1 = new Item(new[] { "silver", "hat" }, "A Silver Hat", "A very shiny silver hat");
            _item2 = new Item(new[] { "light", "torch" }, "A Torch", "A Torch to light the path");
            _player.Inventory.Put(_item1);
            _player.Inventory.Put(_item2);
        }

        [Test]
        public void TestPlayerIsIdentifiable()
        {
            Assert.That(_player.AreYou("me"), Is.True);
            Assert.That(_player.AreYou("inventory"), Is.True);
        }

        [Test]
        public void TestPlayerLocatesItself()
        {
            Assert.That(_player.Locate("me"), Is.EqualTo(_player));
            Assert.That(_player.Locate("inventory"), Is.EqualTo(_player));
        }

        [Test]
        public void TestPlayerLocatesItems()
        {
            Assert.That(_player.Locate("hat"), Is.EqualTo(_item1));
            Assert.That(_player.Locate("torch"), Is.EqualTo(_item2));
            Assert.That(_player.Inventory.HasItem("hat"), Is.True);
            Assert.That(_player.Inventory.HasItem("torch"), Is.True);
        }

        [Test]
        public void TestPlayerLocatesNothing()
        {
            Assert.That(_player.Locate("banana"), Is.Null);
        }

        [Test]
        public void TestPlayerFullDescription()
        {
            string desc = _player.FullDescription;
            Assert.That(desc, Does.Contain("You are James"));
            Assert.That(desc, Does.Contain("an explorer"));
            Assert.That(desc, Does.Contain("You are carrying:"));
            Assert.That(desc, Does.Contain("A Silver Hat"));
            Assert.That(desc, Does.Contain("A Torch"));
        }
    }
}