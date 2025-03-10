using System.Collections.Generic;

namespace DungeonExplorer
{
    public class Room
    {
        private string _description;
        private List<string> _directions;
        private string _origin_direction;

        public Room(string description)
        {
            this.Description = description;
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public List<string> Directions
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