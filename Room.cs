using System.Collections.Generic;

namespace DungeonExplorer
{
    public class Room
    {
        // Private properties
        private string _description;
        private string _item;
        private bool _isAccessible;

        // Public properties with getters and setters
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }
        public string Item
        {
            get { return _item; }
            set { _item = value; }
        }
        public bool IsAccessible
        {
            get { return _isAccessible; }
            set { _isAccessible = value; }
        }

        // Constructor
        public Room(string description, string item = null, bool isAccessible = true)
        {
            this.Description = description;
            this.Item = item;
            this.IsAccessible = isAccessible;
        }

        // Returns the room's description
        public string GetDescription()
        {
            return Description;
        }
    }
}