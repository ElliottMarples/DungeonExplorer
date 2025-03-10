using System.Collections.Generic;

namespace DungeonExplorer
{
    public class Room
    {
        private string _description;

        public Room(string description)
        {
            this.Description = description;
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public string GetDescription()
        {
            return Description;
        }
    }
}