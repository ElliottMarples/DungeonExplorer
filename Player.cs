using System;
using System.Collections.Generic;

namespace DungeonExplorer
{
    public class Player
    {
        private string _name;
        private int _health;

        public string Name {
            get { return _name; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    Console.WriteLine("\nName cannot be empty.");
                }
                else { _name = value; }
            }
        }
        public int Health {
            get { return _health; }
            set
            {
                if (value < 1)
                {
                    Console.WriteLine("\nHealth cannot be zero or negative.");
                }
                else { _health = value; }
            }
        }
        private List<string> inventory = new List<string>();

        public Player(string name, int health) 
        {
            this.Name = name;
            this.Health = health;
        }
        public void PickUpItem(string item)
        {

        }
        public string InventoryContents()
        {
            return string.Join(", ", inventory);
        }
    }
}