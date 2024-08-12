using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Antikythera
{
    // User input will inform the events in Program.cs
    public class InputController
    {
        string? PlayerInput;
        string[] cardinalDirections = {"north", "n",
                                        "northeast", "ne",
                                        "east", "e",
                                        "southeast", "se",
                                        "south", "s",
                                        "southwest", "sw",
                                        "west", "w",
                                        "northwest", "nw",
                                        "up", "down", "in", "out"};

        public void GetCommand()
        {
            Console.WriteLine();
            PlayerInput = Console.ReadLine().ToLower();
            ValidateCommand(PlayerInput);
        }

        public void ValidateCommand(string playerInput)
        {
            if (string.IsNullOrEmpty(playerInput))
            { GetCommand(); }
            else { ProcessCommand(playerInput); }
        }

        public void ProcessCommand(string playerInput)
        {
            string[] words = playerInput.Split(' ');

            switch (words[0].ToLower())
            {
                case "look":
                    Program.player.Look();
                    break;
                case "go":
                    if (words.Length < 2)
                    {
                        Console.WriteLine("Try again with a cardinal direction, such as GO NORTH or GO N.");
                        break;
                    }    
                    if (cardinalDirections.Contains(words[1]))
                    {
                        Program.player.Move(words[1]);
                    }
                    else
                    {
                        Console.WriteLine("Try again with a cardinal direction, such as GO NORTH or GO N.");
                    }
                    break;
                case "attack":
                    if (words.Length < 2)
                    {
                        Console.WriteLine("Attack what? Ensure you type the full name as you see it.");
                        break;
                    }
                        Program.player.Attack(words[1]);
                    break;
                case "get":
                    if (words.Length < 2)
                    {
                        Console.WriteLine("Get what?");
                        break;
                    }
                    Program.player.GetItem(words[1]);
                    break;
                case "drop":
                    if (words.Length < 2)
                    {
                        Console.WriteLine("Drop what?");
                        break;
                    }
                    Program.player.DropItem(words[1]);
                    break;
                case "discard":
                    if (words.Length < 2)
                    {
                        Console.WriteLine("Discard what? Check your inventory.");
                        break;
                    }
                    Program.player.DiscardItem(words[1]);
                    break;
                case "inventory":
                    Program.player.DisplayInventory();
                    break;
                case "equip":
                    if (words.Length < 2)
                    {
                        Console.WriteLine("Equip what? Check your inventory.");
                        break;
                    }
                    Program.player.EquipWeapon(words[1]);
                    break;

            }

            GetCommand();
        }
    }
}