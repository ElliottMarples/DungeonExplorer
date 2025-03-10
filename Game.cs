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
            // Loops until the player has a name
            while (player == null || player.Name == null)
            {
                // Asks user for their name and sets player's health
                Console.Write("Enter your name:\n> ");
                string playerName = Console.ReadLine();
                int playerHealth = 100;

                // Creates a new player with the name and health
                player = new Player(playerName, playerHealth);
            }

            // Creates all the rooms in the dungeon with an appropriate description
            Room entrance = new Room("a stone arch surrounding an entrance into the dungeon facing south.");
            Room room1 = new Room("an empty room with passages to the east and west.");
            Room room2left = new Room("a room containing a treasure chest and a passage to the south.");
            Room room2right = new Room("a room containing a shadowed figure and a passage to the south.");
            Room room3left = new Room("a long passage heading south with unusual symbols carved into the walls.");
            Room room3right = new Room("a bridge heading south over an underground ravine.");
            Room room4left = new Room("a cave containing bats that won't let you past and a door to the east.");
            Room room4right = new Room("a cave with a table of glass bottles filled with a mysterious liquid.");
            Room room5 = new Room("a chamber with doors to the east and west.\n\nThere's a closed metal gate to the south\nIn front of the gate is a stone on a pedestal.");
            Room finalRoom = new Room("a vast underground arena with a massive slime creature in the centre.");
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
            // Starts the main game loop
            bool playing = true;
            while (playing)
            {
                string action;
                char chosen_direction;
                bool notMoved = true;

                // Loops while the player has not moved between rooms
                while (notMoved)
                {
                    // Asks the player what they would like to do
                    Console.Write("Enter [I] to inspect the room, [S] to view your stats, [P] to pick up any items or [M] to move to another room.\n> ");
                    action = Console.ReadLine().ToLower();

                    // Checks the player's input and performs the appropriate action
                    if (action == "i") { Console.WriteLine($"You see {currentRoom.GetDescription()}"); }
                    else if (action == "s") { Console.WriteLine(player.GetStats()); }
                    else if (action == "m")
                    {
                        // Gets the possible directions the player can move
                        List<string> possible_directions = CheckDirections();
                        List<char> possible_directions_letter = new List<char>();

                        bool directionChosen = false;

                        // Loops while the player has not chosen a direction to move
                        while (directionChosen == false)
                        {

                            // Displays the possible directions the player can move denoting the first letter of the direction
                            Console.WriteLine($"\nYou can move:\n");
                            foreach (string direction in possible_directions)
                            {
                                Console.WriteLine($"[{direction[0]}]{direction.Substring(1)}");
                                possible_directions_letter.Add(direction.ToLower()[0]);
                            }

                            // Asks the player which direction they would like to move
                            Console.Write("Enter the first letter of the direction you would like to move.\n> ");
                            chosen_direction = Console.ReadLine().ToLower()[0];

                            // Checks if the player has chosen a valid direction to move
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