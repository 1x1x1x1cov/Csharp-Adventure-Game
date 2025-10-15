using System;

namespace SwinAdventure
{
    public class LookCommand : Command
    {
        public LookCommand() : base(new string[] { "look" }) { }

        public override string Execute(Player p, string[] text)
        {
            if (text.Length < 1 || text[0].ToLower() != "look")
                return "Error: The command must start with 'look'.";

            if (text.Length == 1)
            {
                if (p.Location != null)
                {
                    return p.Location.FullDescription;
                }
                else
                {
                    return "You are nowhere.";
                }
            }

            if (text.Length < 2 || text[1].ToLower() != "at")
                return "Error: You must specify what to look at using 'at'.";

            if (text.Length != 3 && text.Length != 5)
                return "Error: Command must be either 'look at [item]' or 'look at [item] in [container]'.";

            if (text.Length == 3)
            {
                GameObject obj = p.Locate(text[2]);
                return obj != null ? obj.FullDescription : $"I cannot find the {text[2]}.";
            }

            if (text[3].ToLower() != "in")
                return $"What do you want to look in?";

            IHaveInventory container = FetchContainer(p, text[4]);
            if (container == null)
                return $"I cannot find the {text[4]}.";

            GameObject item = container.Locate(text[2]);
            return item != null ? item.FullDescription : $"I cannot find the {text[2]} in the {text[4]}.";
        }

        private IHaveInventory FetchContainer(Player p, string containerId)
        {
            GameObject obj = p.Locate(containerId);

            if (p.Location != null && p.Location.AreYou(containerId))
            {
                return p.Location;
            }

            return obj is IHaveInventory inv ? inv : null;
        }
    }
}
