using System;

namespace SwinAdventure
{
    public class MoveCommand : Command
    {
        public MoveCommand() : base(new string[] { "move", "go", "head", "leave" })
        {
        }

        public override string Execute(Player p, string[] text)
        {
            if (text.Length != 2)
            {
                return "Move to where?";
            }

            if (p.Location == null)
            {
                return "You are nowhere. There is nowhere to move.";
            }

            GameObject obj = p.Location.Locate(text[1]);
            if (obj is Path path)
            {
                p.SetLocation(path.Destination);
                return $"You move {text[1]} to {path.Destination.Name}.";
            }

            return "There is no path in that direction.";
        }
    }
}
