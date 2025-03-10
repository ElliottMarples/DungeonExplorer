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
            // Asks user for their name
            Console.Write("Enter your name:\n> ");
            string name = Console.ReadLine();

            // Initialize the game with one room and one player
            currentRoom = new Room("a stone arch surrounding an entrance into the dungeon.");
            player = new Player(name, 100);

            // Welcomes the player
            Console.WriteLine($"Welcome to the Dungeon, {player.Name}.\n");
        }
        public void Start()
        {
            // Change the playing logic into true and populate the while loop
            bool playing = true;
            while (playing)
            {
                // Code your playing logic here
                Console.WriteLine($"{player.Name}'s Stats:\nHealth = {player.Health}\n");
                Console.WriteLine($"You enter the room and see {currentRoom.GetDescription()}");
            }
        }
    }
}