using System;
using System.Media;
using System.Runtime.InteropServices;

namespace DungeonExplorer
{
    internal class Game
    {
        private Player player;
        private Room currentRoom;

        public Game()
        {
            while (player == null || player.Name == null)
            {
                // Asks user for their name and sets player's health
                Console.Write("Enter your name:\n> ");
                string playerName = Console.ReadLine();
                int playerHealth = 100;

                // Sets the starter room's description and directions
                string[] currentRoomDirections = {"s"};
                string currentRoomDescription = "a stone arch surrounding an entrance into the dungeon.";

                // Initialize the game with one room and one player
                currentRoom = new Room(currentRoomDescription, currentRoomDirections);
                player = new Player(playerName, playerHealth);
            }

            // Welcomes the player
            Console.WriteLine($"\nWelcome to the Dungeon, {player.Name}.\n");
        }
        public void Start()
        {
            // Change the playing logic into true and populate the while loop
            bool playing = true;
            while (playing)
            {
                // Code your playing logic here
                Console.WriteLine($"{player.Name}'s Stats:\nHealth = {player.Health}\n");
                Console.WriteLine("\nYou can inspect a room to find out more about it or move to another room.");
                Console.Write("Enter [I] to Inspect or [M] to move to another room.\n> ");
                string action = Console.ReadLine().ToLower();
                Console.WriteLine(action);
                Console.WriteLine($"\nYou can move:\n{currentRoom.Directions}");
                Console.Write("Enter the first letter of the direction you would like to move.\n> ");
                string direction = Console.ReadLine().ToLower();
                Console.WriteLine(direction);
                playing = false;
            }
        }
    }
}