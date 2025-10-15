using NUnit.Framework;
using SwinAdventure;

namespace SwinAdventureTests
{
    public class LookCommandTests
    {
        private Item _testItem;
        private Player _testPlayer;
        private Bag _testMoneyBag;
        private LookCommand _testLookCommand;

        [SetUp]
        public void Setup()
        {
            _testLookCommand = new LookCommand();
            _testPlayer = new Player("HarryPotter", "a student");

            _testItem = new Item(new string[] { "gem", "ruby" }, "A Ruby", "A bright Pink ruby");
            _testMoneyBag = new Bag(new string[] { "bag", "money" }, "Money Bag", "A bag that contains Valuables");

            _testPlayer.Inventory.Put(_testItem); // Default: gem is in player
        }

        [Test]
        public void LookAtPlayer()
        {
            string result = _testLookCommand.Execute(_testPlayer, new string[] { "look", "at", "inventory" });
            Assert.That(result, Does.Contain("You are").And.Contain("You are carrying:"));
        }

        [Test]
        public void LookAtItem()
        {
            string result = _testLookCommand.Execute(_testPlayer, new string[] { "look", "at", "gem" });
            Assert.That(result, Is.EqualTo("A bright Pink ruby"));
        }

        [Test]
        public void LookAtNothing()
        {
            _testPlayer.Inventory.Take("gem"); // remove gem
            string result = _testLookCommand.Execute(_testPlayer, new string[] { "look", "at", "gem" });
            Assert.That(result, Is.EqualTo("I cannot find the gem."));
        }

        [Test]
        public void LookAtItemInPlayer()
        {
            string result = _testLookCommand.Execute(_testPlayer, new string[] { "look", "at", "gem", "in", "inventory" });
            Assert.That(result, Is.EqualTo("A bright Pink ruby"));
        }

        [Test]
        public void LookAtItemInBag()
        {
            _testMoneyBag.Inventory.Put(_testItem);
            _testPlayer.Inventory.Put(_testMoneyBag);
            string result = _testLookCommand.Execute(_testPlayer, new string[] { "look", "at", "gem", "in", "bag" });
            Assert.That(result, Is.EqualTo("A bright Pink ruby"));
        }

        [Test]
        public void LookAtItemInNoBag()
        {
            string result = _testLookCommand.Execute(_testPlayer, new string[] { "look", "at", "gem", "in", "bag" });
            Assert.That(result, Is.EqualTo("I cannot find the bag."));
        }

        [Test]
        public void LookAtNothingInBag()
        {
            _testPlayer.Inventory.Put(_testMoneyBag); // Bag exists but gem isn't inside it
            string result = _testLookCommand.Execute(_testPlayer, new string[] { "look", "at", "gem", "in", "bag" });
            Assert.That(result, Is.EqualTo("I cannot find the gem in the bag."));
        }

        [Test]
        public void InvalidLook()
        {
            string result1 = _testLookCommand.Execute(_testPlayer, new string[] { "look", "around" });
            string result2 = _testLookCommand.Execute(_testPlayer, new string[] { "hello", "your", "student", "ID" });
            string result3 = _testLookCommand.Execute(_testPlayer, new string[] { "look", "at", "your", "name" });

            Assert.That(result1, Is.EqualTo("Error: You must specify what to look at using 'at'."));
            Assert.That(result2, Is.EqualTo("Error: The command must start with 'look'."));
            Assert.That(result3, Is.EqualTo("Error: Command must be either 'look at [item]' or 'look at [item] in [container]'."));
        }

        [Test]
        public void LookAtItemWrongPreposition()
        {
        //setup
            _testMoneyBag.Inventory.Put(_testItem);
            _testPlayer.Inventory.Put(_testMoneyBag);

        //wrong preposition used: "on" instead of "in"
            string result = _testLookCommand.Execute(_testPlayer, new[] { "look", "at", "gem", "on", "bag" });

            Assert.That(result, Is.EqualTo("What do you want to look in?"));
            
            Console.WriteLine("\n===  Week 10 verification task test completed successfully ===\n");
           

        }
    }
}
