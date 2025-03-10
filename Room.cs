using System.Collections.Generic;

namespace DungeonExplorer
{
    public class Room
    {
        private string _description;
        private string[] _directions;
        private string _origin_direction;

        public Room(string description, string[] directions)
        {
            this.Description = description;
            this.Directions = directions;
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public string[] Directions
        {
            get { return _directions; }
            set { _directions = value; }
        }

        public string GetDescription()
        {
            return Description;
        }
    }
}