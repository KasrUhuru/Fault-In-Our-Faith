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
                                        "up", "down", "in", "out"}

        public void GetCommand()
        {
            Console.WriteLine("What do you do?");
            PlayerInput = Console.ReadLine().ToLower();
            ValidateCommand(PlayerInput);
        }

        public void ValidateCommand(string playerInput)
        {
            if (string.IsNullOrEmpty(playerInput))
            {
                GetCommand();
            }
            else { ProcessCommand(playerInput); }
        }

        public void ProcessCommand(string playerInput)
        {
            string[] words = playerInput.Split(' ');

            switch (words)
            {
                case words[0] == "look":
                    Program.player.Look();
                    break;
                case words[0] == "go":
                    if (cardinalDirections.Contains(words[1])
                    {
                        Program.player.Move(words[1]);
                    }
                    else
                    {
                        Console.WriteLine("Try again with a cardinal direction, such as GO NORTH or GO N.");
                    }
                    break;
                case words[0] == "attack":
                    Program.player.Attack(words[1])

            }

        GetCommand();
        }
    }
}
