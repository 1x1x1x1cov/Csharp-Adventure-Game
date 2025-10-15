using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace SwinAdventure
{
    class Program
    {
        static void Main(string[] args)
        {
            Player player = new Player("James", "an explorer");

            Item item1 = new Item(new[] { "silver", "hat" }, "A Silver Hat", "A very shiny silver hat");
            Item item2 = new Item(new[] { "light", "torch" }, "A Torch", "A Torch to light the path");

            player.Inventory.Put(item1);
            player.Inventory.Put(item2);

            Console.WriteLine(player.AreYou("me"));
            Console.WriteLine(player.AreYou("inventory"));

            if (player.Locate("torch") != null)
            {
                Console.WriteLine("The object torch exists");
                Console.WriteLine(player.Inventory.HasItem("torch"));
            }
            else
            {
                Console.WriteLine("The object torch does not exist");
            }

            using (StreamWriter writer = new StreamWriter("TestPlayer.txt"))
            {
                player.SaveTo(writer);
            }

            using (StreamReader reader = new StreamReader("TestPlayer.txt"))
            {
                player.LoadFrom(reader);
            }

            // Polymorphism
            List<IHaveInventory> myContainers = new List<IHaveInventory>();
            myContainers.Add(player);

            Bag testToolBag = new Bag(new[] { "bag", "tool" }, "Tools Bag", "A bag that contains tools");
            Item testItem = new Item(new[] { "stew", "beef" }, "A Beef Stew", "A hearty beef stew");
            testToolBag.Inventory.Put(testItem);

            player.AttachBag(testToolBag);
            myContainers.Add(testToolBag);

            Console.WriteLine("\n=== Inventory Output (Polymorphism) ===\n");

            List<string> containerSummaries = new List<string>();
            for (int i = 0; i < myContainers.Count; i++)
            {
                IHaveInventory container = myContainers[i];
                Inventory inv = null;

                if (container is Player p)
                {
                    inv = p.Inventory;
                }
                else if (container is Bag b)
                {
                    inv = b.Inventory;
                }

                int itemCount = inv?.FetchAll().Count ?? 0;
                containerSummaries.Add($"{container.Name}: {itemCount} item(s)");
            }
            Console.WriteLine(string.Join(" | ", containerSummaries));

            for (int i = 0; i < myContainers.Count; i++)
            {
                IHaveInventory container = myContainers[i];

                Console.WriteLine($"\n[Container: {container.Name}]");
                Console.WriteLine($"Located Self: {container.Locate(container.Name)}\n");

                if (container is GameObject obj)
                {
                    Console.WriteLine("Description:");
                    Console.WriteLine(obj.FullDescription);
                }
                else
                {
                    Console.WriteLine("(Not a GameObject, no description available)");
                }

                Inventory inv = null;
                if (container is Player p)
                {
                    inv = p.Inventory;
                }
                else if (container is Bag b)
                {
                    inv = b.Inventory;
                }

                if (inv != null)
                {
                    Console.WriteLine($"\nItems being carried: {inv.FetchAll().Count} item(s)");

                    List<Item> items = inv.FetchAll();
                    for (int j = 0; j < items.Count; j++)
                    {
                        Item item = items[j];
                        Console.WriteLine("- " + item.Name);
                    }
                }
                else
                {
                    Console.WriteLine("\nItems being carried: 0 item(s)");
                }

                Console.WriteLine("\n------------------------------\n");
            }

            //Location Setup
            Location beach = new Location(new[] { "beach" }, "Beach", "A sunny tropical beach.");
            Location jungle = new Location(new[] { "jungle" }, "Jungle", "A dense and humid jungle.");
            Location mountain = new Location(new[] { "mountain" }, "Mountain", "A cold and snowy mountain.");
            Location desert = new Location(new[] { "desert" }, "Desert", "A hot and dry desert.");
            Location shore = new Location(new[] { "shore" }, "Shore", "A rocky shoreline.");

            Path toJungle = new Path(new[] { "south" }, "Path to Jungle", "A trail leading south to the jungle.", jungle);
            Path toMountain = new Path(new[] { "north" }, "Path to Mountain", "A winding path north to the mountain.", mountain);
            Path toDesert = new Path(new[] { "east" }, "Path to Desert", "A hot trail east to the desert.", desert);
            Path toShore = new Path(new[] { "west" }, "Path to Shore", "A sandy way west to the shore.", shore);

            beach.AddPath(toJungle);
            beach.AddPath(toMountain);
            beach.AddPath(toDesert);
            beach.AddPath(toShore);

            Path backToBeachFromJungle = new Path(new[] { "north" }, "Path to Beach", "A trail back north to the beach.", beach);
            Path backToBeachFromMountain = new Path(new[] { "south" }, "Path to Beach", "A path going back south to the beach.", beach);
            Path backToBeachFromDesert = new Path(new[] { "west" }, "Path to Beach", "A trail heading west to the beach.", beach);
            Path backToBeachFromShore = new Path(new[] { "east" }, "Path to Beach", "A rocky return east to the beach.", beach);

            jungle.AddPath(backToBeachFromJungle);
            mountain.AddPath(backToBeachFromMountain);
            desert.AddPath(backToBeachFromDesert);
            shore.AddPath(backToBeachFromShore);

            player.SetLocation(beach);

            //Command Test 
            MoveCommand moveCmd = new MoveCommand();
            LookCommand lookCmd = new LookCommand();

            Console.WriteLine("\n--- Command Testing ---");
            Console.WriteLine("Try: move north | move south | look | look at beach | look at south");
            Console.WriteLine("Type 'exit' to quit.");

            while (true)
            {
                Console.Write("\nEnter a command: ");
                string input = Console.ReadLine();

                if (input.ToLower() == "exit")
                    break;

                string[] split = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                if (split.Length > 0 && (split[0] == "move" || split[0] == "go" || split[0] == "head" || split[0] == "leave"))
                {
                    string output = moveCmd.Execute(player, split);
                    Console.WriteLine(output);
                }
                else if (split.Length > 0 && split[0] == "look")
                {
                    string output = lookCmd.Execute(player, split);
                    Console.WriteLine(output);
                }
                else
                {
                    Console.WriteLine("Unrecognized command.");
                }
            }
        }
    }
}
