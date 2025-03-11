using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Media;
using System.Runtime.InteropServices;

namespace DungeonExplorer
{
    internal class Game
    {
        // Private properties
        private Player player;
        private Room currentRoom;
        private int[] currentIndex;
        private Room[,] roomMatrix;

        public Game()
        {
            // Loops until the player has a name
            while (player == null || player.Name == null)
            {
                // Asks user for their name and sets player's health
                Console.Write("Enter your name:\n> ");
                string playerName = Console.ReadLine();
                int playerHealth = 100;
                int playerAttack = 10;

                // Creates a new player with the name and health
                player = new Player(playerName, playerHealth, playerAttack);
            }

            // Creates all the rooms in the dungeon with an appropriate description and item
            Room entrance = new Room("a stone arch surrounding an entrance into the dungeon facing south.");
            Room room1 = new Room("an empty room with passages to the east and west.");
            Room room2left = new Room("a room containing a treasure chest and a passage to the south.", item: "Sabre");
            Room room2right = new Room("a room containing a shadowed figure and a passage to the south.");
            Room room3left = new Room("a long passage heading south with unusual symbols carved into the walls.");
            Room room3right = new Room("a bridge heading south over an underground ravine.");
            Room room4left = new Room("a cave containing bats that won't let you past and a door to the east.");
            Room room4right = new Room("a cave with a table of glass bottles filled with a mysterious liquid.", item: "Health Potion");
            Room room5 = new Room("a chamber with doors to the east and west.\nThere's a closed metal gate to the south.\nIn front of the gate is a stone on a pedestal.", item: "Stone");
            Room finalRoom = new Room("a vast underground arena with a massive slime creature in the centre.", isAccessible: false);
            Room exit = new Room("Exit.");

            // Organises the rooms into a 2D array
            roomMatrix = new Room[6, 3]
            {
                { null,         entrance,   null        },
                { room2left,    room1,      room2right  },
                { room3left,    null,       room3right  },
                { room4left,    room5,      room4right  },
                { null,         finalRoom,  null        },
                { null,         exit,       null        }
            };

            // Sets the current room to the entrance
            currentRoom = entrance;
            currentIndex = new int[] { 0, 1 };

            // Welcomes the player
            Console.WriteLine($"\nWelcome to the Dungeon, {player.Name}.\nPress any key to start.");
            Console.ReadKey();
        }

        // Checks the possible directions the player can move
        public List<string> CheckDirections()
        {
            // Creates a list which will contain possible directions the player can move from the current room
            List<string> possibleDirections = new List<string>();

            // Gets the index of the room to the north, south, west and east of the current room
            int northIndex = currentIndex[0] - 1;
            int southIndex = currentIndex[0] + 1;
            int westIndex = currentIndex[1] - 1;
            int eastIndex = currentIndex[1] + 1;

            // Checks if the room to the north, south, west and east of the current room exists (and is accessible) and adds the direction to the list if it does
            if (northIndex >= 0)
            {
                Room northRoom = roomMatrix[northIndex, currentIndex[1]];
                if (northRoom != null && northRoom.IsAccessible)
                {
                    possibleDirections.Add("North");
                }
            }

            if (southIndex < roomMatrix.GetLength(0))
            {
                Room southRoom = roomMatrix[southIndex, currentIndex[1]];

                if (southRoom != null && southRoom.IsAccessible)
                {
                    possibleDirections.Add("South");
                }
            }

            if (westIndex >= 0)
            {
                Room westRoom = roomMatrix[currentIndex[0], westIndex];

                if (westRoom != null && westRoom.IsAccessible)
                {
                    possibleDirections.Add("West");
                }
            }

            if (eastIndex < roomMatrix.GetLength(1))
            {
                Room eastRoom = roomMatrix[currentIndex[0], eastIndex];

                if (eastRoom != null && eastRoom.IsAccessible)
                {
                    possibleDirections.Add("East");
                }
            }

            // Returns the list of possible directions
            return possibleDirections;
        }

        // Moves the player to the room in the direction specified
        public void Move(char direction)
        {
            // Updates currentIndex based on the direction the player has chosen
            if (direction.Equals('n'))
            {
                currentIndex[0] -= 1;
            }

            else if (direction.Equals('s'))
            {
                currentIndex[0] += 1;
            }

            else if (direction.Equals('w'))
            {
                currentIndex[1] -= 1;
            }

            else if (direction.Equals('e'))
            {
                currentIndex[1] += 1;
            }

            // Updates the current room
            currentRoom = roomMatrix[currentIndex[0], currentIndex[1]];
        }

        // Uses the item specified by the player
        public void Use(string item)
        {
            string itemName;

            // Checks the item the player has chosen and performs the appropriate action
            if (item == "sa")
            {
                itemName = "Sabre";
                Console.WriteLine("\nYou grab your sabre and feel prepared for battle");
                player.EquipItem(itemName, "attack", 10);
            }

            else if (item == "he")
            {
                itemName = "Health Potion";
                Console.WriteLine("\nYou drink the liquid in the bottle and see your wounds heal instantaneously");
                player.Health += 20;
                player.UseItem(itemName);
            }

            else if (item == "st")
            {
                itemName = "Stone";
                Console.WriteLine("\nYou hold the stone. It makes you feel happy.");
                player.EquipItem(itemName, "happiness", 1000);
            }
        }

        public void Start()
        {
            // Starts the main game loop
            bool playing = true;
            while (playing)
            {
                string action;
                string chosenDirection;
                char chosenDirectionChar;
                bool notMoved = true;
                bool exploredRoom = false;

                // Clears the console and displays the player's options
                Console.Clear();
                Console.WriteLine("Enter:\n[E] to explore the room\n[S] to view your stats\n[I] to view your inventory\n[P] to pick up any items\n[U] to use/equip an item\n[M] to move to another room");

                // Loops while the player has not moved between rooms
                while (notMoved)
                {

                    // Allows player to input their action
                    Console.Write("> ");
                    action = Console.ReadLine().ToLower();
                    Console.WriteLine();

                    // Checks the player's input and performs the appropriate action
                    // If the player explores the room, the room's description is displayed and exploredRoom is set to true
                    if (action == "e")
                    {
                        Console.WriteLine($"You see {currentRoom.GetDescription()}");
                        exploredRoom = true;
                    }

                    // If the player views their stats, their name and health are displayed
                    else if (action == "s")
                    {
                        Console.WriteLine(player.GetStats());
                    }

                    // If the player views their inventory, their inventory contents and equipped item are displayed
                    else if (action == "i")
                    {
                        Console.WriteLine(player.GetInventory());
                    }

                    // If the player picks up an item, the item is added to their inventory and removed from the room
                    else if (action == "p")
                    {
                        // Checks if the player has explored the room
                        if (exploredRoom == false)
                        {
                            Console.WriteLine("You must explore the room to check for items first.");
                        }

                        // Checks if the room has an item
                        else if (currentRoom.Item == null)
                        {
                            Console.WriteLine("There are no items in this room.");
                        }

                        else
                        {
                            // Prints an appropriate message depending on the item in the room
                            if (currentRoom.Item == "Sabre")
                            {
                                Console.WriteLine("You open the treasure chest and find a Sabre.");
                            }

                            else if (currentRoom.Item == "Health Potion")
                            {
                                Console.WriteLine("You pick up one of the bottles.");
                            }

                            // If the player picks up the stone, the metal gate opens, the room's description is updated and the room to the south becomes accessible
                            else if (currentRoom.Item == "Stone")
                            {
                                Console.WriteLine("You pick up the stone.");
                                Console.WriteLine("You watch the metal gate open, allowing you to traverse through it.");
                                roomMatrix[currentIndex[0] + 1, currentIndex[1]].IsAccessible = true;
                                currentRoom.Description = "a chamber with doors to the east and west.\nThere's an open metal gate to the south.\nIn front of the gate is a pedestal.";
                            }

                            // Adds the item to the player's inventory and removes it from the room
                            player.PickUpItem(currentRoom.Item);
                            currentRoom.Item = null;
                        }
                    }

                    // If the player uses an item, they are asked to choose an item from their inventory
                    else if (action == "u")
                    {
                        // Splits the value returned from InventoryContents into an list of items
                        string[] playerInventory = player.InventoryContents().Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
                        List<string> playerInventoryList = new List<string>(playerInventory);
                        List<string> playerInventoryLetters = new List<string>();

                        // Checks if the player has items in their inventory
                        if (playerInventoryList.Count != 0)
                        {
                            // Displays the items in the player's inventory and denotes the first two letters of the item
                            Console.WriteLine("You can use/equip:");
                            foreach (string item in playerInventoryList)
                            {
                                string itemLetters = $"{item[0]}{item[1]}";
                                Console.WriteLine($"[{itemLetters}]{item.Substring(2)}");
                                playerInventoryLetters.Add(itemLetters.ToLower());
                            }

                            // Asks the player which item they would like to use
                            Console.Write("\nEnter the first two letters of the item you would like to use/equip.\n> ");
                            string chosenItem = Console.ReadLine().ToLower().Trim(' ');

                            // Checks if the player has chosen a valid item to use
                            if (playerInventoryLetters.Contains(chosenItem))
                            {
                                Use(chosenItem);
                            }
                        }

                        // If the player has no items in their inventory, an appropriate message is displayed
                        else
                        {
                            Console.WriteLine("You have no items in your inventory.");
                        }
                    }

                    else if (action == "m")
                    {
                        // Gets the possible directions the player can move
                        List<string> possibleDirections = CheckDirections();
                        List<char> possibleDirectionsLetter = new List<char>();

                        bool directionChosen = false;

                        // Loops while the player has not chosen a direction to move
                        while (directionChosen == false)
                        {

                            // Displays the possible directions the player can move and denotes the first letter of the direction
                            Console.WriteLine($"You can move:");
                            foreach (string direction in possibleDirections)
                            {
                                Console.WriteLine($"[{direction[0]}]{direction.Substring(1)}");
                                possibleDirectionsLetter.Add(direction.ToLower()[0]);
                            }

                            // Asks the player which direction they would like to move
                            Console.Write("\nEnter the first letter of the direction you would like to move.\n> ");
                            chosenDirection = Console.ReadLine().ToLower().Trim(' ');

                            if (chosenDirection.Length == 0)
                            {
                                chosenDirectionChar = ' ';
                            }

                            else
                            {
                                chosenDirectionChar = chosenDirection[0];
                            }

                            // Checks if the player has chosen a valid direction to move
                            if (possibleDirectionsLetter.Contains(chosenDirectionChar))
                            {
                                Move(chosenDirectionChar);
                                directionChosen = true;
                                notMoved = false;
                            }

                            else
                            {
                                Console.WriteLine("Cannot move in this direction.\n");
                            }
                        }
                    }

                    if (currentRoom == roomMatrix[5, 1])
                    {
                        Console.WriteLine("\nCongratulations!\nYou made it to the exit of the dungeon.\nThanks for playing.");
                        playing = false;
                    }

                    else if (player.Health <= 0)
                    {
                        Console.WriteLine("\nUnlucky!\nYou ran out of health.");
                        playing = false;
                    }
                }
            }
        }
    }
}