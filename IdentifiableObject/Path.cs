using System;

namespace SwinAdventure
{
    public class Path : Item
    {
        private Location _destination;

        public Path(string[] ids, string name, string desc, Location destination)
            : base(ids, name, desc)
        {
            _destination = destination;
        }

        public Location Destination
        {
            get { return _destination; }
        }
    }
}
