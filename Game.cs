using System;
using System.Collections.Generic;
using System.Media;
using System.Runtime.InteropServices;

namespace DungeonExplorer
{
    internal class Game
    {
        private Player player;
        private Room currentRoom;
        private int[] currentIndex;
        private Room[,] roomMatrix;

        public Game()
        {
            while (player == null || player.Name == null)
            {
                // Asks user for their name and sets player's health
                Console.Write("Enter your name:\n> ");
                string playerName = Console.ReadLine();
                int playerHealth = 100;

                player = new Player(playerName, playerHealth);
            }

            // Creates all the rooms in the dungeon
            Room startRoom = new Room("a stone arch surrounding an entrance into the dungeon.");
            Room room1 = new Room("room1");
            Room room2left = new Room("room2left");
            Room room2right = new Room("room2right");
            Room room3left = new Room("room3left");
            Room room3right = new Room("room3right");
            Room room4left = new Room("room4left");
            Room room4right = new Room("room4right");
            Room room5 = new Room("room5");
            Room finalRoom = new Room("finalroom");

            roomMatrix = new Room[5, 3]
            {
                { null,         startRoom,  null        },
                { room2left,    room1,      room2right  },
                { room3left,    null,       room3right  },
                { room4left,    room5,      room4right  },
                { null,         finalRoom,  null        }
            };

            currentRoom = startRoom;
            currentIndex = new int[] {0, 1};


            // Welcomes the player
            Console.WriteLine($"\nWelcome to the Dungeon, {player.Name}.\n");
        }

        public List<string> CheckDirections()
        {
            // Creates a list which will contain possible directions the player can move from the current room
            List<string> possible_directions = new List<string>();

            // Gets the index of the room to the north, south, west and east of the current room
            int north_index = currentIndex[0] - 1;
            int south_index = currentIndex[0] + 1;
            int west_index = currentIndex[1] - 1;
            int east_index = currentIndex[1] + 1;

            Console.WriteLine($"{roomMatrix.GetLength(0)}, {roomMatrix.GetLength(1)}");
            Console.WriteLine($"{north_index}, {currentIndex[1]}");
            Console.WriteLine($"{south_index}, {currentIndex[1]}");
            Console.WriteLine($"{currentIndex[0]}, {west_index}");
            Console.WriteLine($"{currentIndex[0]}, {east_index}");

            // Checks if the room to the north, south, west and east of the current room exists and adds the direction to the list if it does
            if (north_index >= 0)
            {
                if (roomMatrix[north_index, currentIndex[1]] != null)
                {
                    possible_directions.Add("North");
                }
            }
            if (south_index < roomMatrix.GetLength(0))
            {
                if (roomMatrix[south_index, currentIndex[1]] != null)
                {
                    possible_directions.Add("South");
                }
            }
            if (west_index >= 0)
            {
                if (roomMatrix[currentIndex[0], west_index] != null)
                {
                    possible_directions.Add("West");
                }
            }
            if (east_index < roomMatrix.GetLength(1))
            {
                if (roomMatrix[currentIndex[0], east_index] != null)
                {
                    possible_directions.Add("East");
                }
            }

            // Returns the list of possible directions
            return possible_directions;
        }

        public void Move(char direction)
        {
            // Moves the player to the room in the direction specified
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

        public void Start()
        {
            // Change the playing logic into true and populate the while loop
            bool playing = true;
            while (playing)
            {
                string action;
                char chosen_direction;
                bool notMoved = true;
                while (notMoved)
                {
                    Console.Write("Enter [I] to inspect the room, [S] to view your stats, [P] to pick up any items or [M] to move to another room.\n> ");
                    action = Console.ReadLine().ToLower();
                    if (action == "i") { Console.WriteLine($"You see {currentRoom.GetDescription()}"); }
                    else if (action == "s") { Console.WriteLine(player.GetStats()); }
                    else if (action == "m")
                    {
                        List<string> possible_directions = CheckDirections();
                        List<char> possible_directions_letter = new List<char>();
                        bool directionChosen = false;
                        while (directionChosen == false)
                        {
                            Console.WriteLine($"\nYou can move:\n");
                            foreach (string direction in possible_directions)
                            {
                                Console.WriteLine($"[{direction[0]}]{direction.Substring(1)}");
                                possible_directions_letter.Add(direction.ToLower()[0]);
                            }
                            Console.Write("Enter the first letter of the direction you would like to move.\n> ");
                            chosen_direction = Console.ReadLine().ToLower()[0];
                            if (possible_directions_letter.Contains(chosen_direction))
                            {
                                Move(chosen_direction);
                                directionChosen = true;
                                notMoved = false;
                            }
                            else
                            {
                                Console.WriteLine("Cannot move in this direction.");
                            }
                        }
                    }
                }
            }
        }
    }
}