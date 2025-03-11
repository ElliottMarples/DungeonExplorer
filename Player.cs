using System;
using System.Collections.Generic;

namespace DungeonExplorer
{
    public class Player
    {
        // Private properties
        private string _name;
        private int _health;
        private int _attack;
        private int _happiness;
        private List<string> _inventory = new List<string>();
        private string _equippedItem;
        private string _equippedItemEffect;
        private int _equippedItemEffectValue;

        // Public properties with getters and setters
        public string Name {
            get { return _name; }
            set
            {
                // If the value is null or empty an appropriate message is printed
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
                // If the value is less than 1 an appropriate message is printed
                if (value < 1)
                {
                    Console.WriteLine("\nHealth cannot be zero or negative.");
                }

                else { _health = value; }
            }
        }
        public int Attack
        {
            get { return _attack; }
            set
            {
                // If the value is less than 1 an appropriate message is printed
                if (value < 1)
                {
                    Console.WriteLine("\nAttack cannot be zero or negative.");
                }

                else
                {
                    _attack = value;
                }
            }
        }
        public int Happiness
        {
            get { return _happiness; }
            set
            {
                // If the value is less than 1 an appropriate message is printed
                if (value < 1)
                {
                    Console.WriteLine("\nHappiness cannot be zero or negative.");
                }

                else
                {
                    _happiness = value;
                }
            }
        }

        // Constructor
        public Player(string name, int health, int attack) 
        {
            this.Name = name;
            this.Health = health;
            this.Attack = attack;
        }

        // Adds an item to the inventory
        public void PickUpItem(string item)
        {
            _inventory.Add(item);
        }

        // Removes an item from the inventory when used
        public void UseItem(string item)
        {
            if (_inventory.Contains(item))
            {
                _inventory.Remove(item);
            }

            else
            {
                Console.WriteLine("You don't have that item in your inventory.");
            }
        }

        // Equips an item, applies its effects and adds the previously equipped item back to the inventory
        public void EquipItem(string item, string effect, int effectValue)
        {
            // Checks if the player has an item equipped
            if (_equippedItem != null)
            {
                // The equipped item is added back to the inventory
                _inventory.Add(_equippedItem);

                // If the equipped item has an attack effect, the effect is removed
                if (_equippedItemEffect == "attack")
                {
                    Attack -= _equippedItemEffectValue;
                }

                // If the equipped item has a health effect, the effect is removed
                else if (_equippedItemEffect == "health")
                {
                    Health -= _equippedItemEffectValue;
                }

                // If the equipped item has a happiness effect, the effect is removed
                else if (_equippedItemEffect == "happiness")
                {
                    Happiness -= _equippedItemEffectValue;
                }
            }

            // The new item is equipped and removed from the inventory
            _equippedItem = item;
            UseItem(item);

            // If the new item has an attack effect, the effect is applied
            if (effect == "attack")
            {
                Attack += effectValue;
            }

            // If the new item has a health effect, the effect is applied
            else if (effect == "health")
            {
                Health += effectValue;
            }

            // If the new item has a happiness effect, the effect is applied
            else if (effect == "happiness")
            {
                Happiness += effectValue;
            }
        }

        // Returns the contents of the inventory
        public string InventoryContents()
        {
            return string.Join(", ", _inventory);
        }

        // Returns the player's inventory
        public string GetInventory()
        {
            // A string containing the player's inventory is created
            string inventory_string = $"{Name}'s Inventory: ";

            // If the player has an equipped item, an appropriate message is returned
            if (_equippedItem != null)
            {
                inventory_string += $"\nEquipped Item: {_equippedItem}";
            }

            // If the player has items in their inventory, an appropriate message is returned
            if (_inventory.Count != 0)
            {
                inventory_string += $"\nContents: {InventoryContents()}";
            }

            // If the player has no items in their inventory, an appropriate message is returned
            if (_equippedItem == null && _inventory.Count == 0)
            {
                return "There is nothing in your inventory.";
            }

            return inventory_string;
        }

        // Returns the player's stats
        public string GetStats()
        {
            // A string containing the player's stats is created
            string stats_string = $"{Name}'s Stats:\nHealth = {Health}\nAttack = {Attack}";

            // If the player's happiness is not 0, an appropriate message is returned
            if (Happiness != 0)
            {
                stats_string += $"\nHappiness = {Happiness}";
            }

            return stats_string;
        }
    }
}