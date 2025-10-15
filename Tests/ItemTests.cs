using NUnit.Framework;
using SwinAdventure;

namespace SwinAdventure.Tests
{
    [TestFixture]
    public class ItemTests
    {
        private Item _item;

        [SetUp]
        public void Setup()
        {
            _item = new Item(new[] { "silver", "hat" }, "A Silver Hat", "A very shiny silver hat");
        }

        [Test]
        public void TestItemIsIdentifiable_True()
        {
            Assert.That(_item.AreYou("silver"), Is.True);
            Assert.That(_item.AreYou("hat"), Is.True);
        }

        [Test]
        public void TestItemIsIdentifiable_False()
        {
            Assert.That(_item.AreYou("sword"), Is.False);
        }

        [Test]
        public void TestShortDescription()
        {
            Assert.That(_item.ShortDescription, Is.EqualTo("A Silver Hat (silver)"));
        }

        [Test]
        public void TestFullDescription()
        {
            Assert.That(_item.FullDescription, Is.EqualTo("A very shiny silver hat"));
        }

        [Test]
        public void TestPrivilegeEscalation_ValidPin()
        {
            _item.PrivilegeEscalation("6789");
            Assert.That(_item.FirstId, Is.EqualTo("NewTutorialID"));
        }

        [Test]
        public void TestPrivilegeEscalation_InvalidPin()
        {
            _item.PrivilegeEscalation("0000");
            Assert.That(_item.FirstId, Is.EqualTo("silver"));
        }
    }
}