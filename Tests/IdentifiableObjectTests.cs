using NUnit.Framework;
using SwinAdventure;

namespace SwinAdventure.Tests
{
    [TestFixture]
    public class IdentifiableObjectTests
    {
        private IdentifiableObject _object;

        [SetUp]
        public void Setup()
        {
            _object = new IdentifiableObject(new[] { "apple", "fruit" });
        }

        [Test]
        public void TestAreYou_True()
        {
            Assert.That(_object.AreYou("apple"), Is.True);
            Assert.That(_object.AreYou("fruit"), Is.True);
        }

        [Test]
        public void TestAreYou_False()
        {
            Assert.That(_object.AreYou("banana"), Is.False);
        }

        [Test]
        public void TestFirstId()
        {
            Assert.That(_object.FirstId, Is.EqualTo("apple"));
        }

        [Test]
        public void TestPrivilegeEscalation_ValidPin()
        {
            _object.PrivilegeEscalation("0710");
            Assert.That(_object.FirstId, Is.EqualTo("NewTutorialID"));
        }

        [Test]
        public void TestPrivilegeEscalation_InvalidPin()
        {
            _object.PrivilegeEscalation("0000");
            Assert.That(_object.FirstId, Is.EqualTo("apple"));
        }
    }
}
