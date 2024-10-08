﻿using System;
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
            string target; // Catch names with multiple words

            switch (words[0].ToLower())
            {
                case "attack":
                    if (words.Length < 2)
                    {
                        Console.WriteLine("Attack what? Ensure you type the full name as you see it.");
                        break;
                    }
                    target = string.Join(' ', words.Skip(1));
                    Program.player.Attack(target);
                    break;
                case "cast":
                    if (words.Length < 2)
                    {
                        Console.WriteLine("Cast what? Ensure you type the full name as you see it.");
                        break;
                    }
                    if (words.Length == 2)
                    {
                        Program.player.CastSpell(words[1]); // Implicit 
                        break;
                    }
                    if (words.Length >= 3)
                    {
                        target = string.Join(' ', words.Skip(2));
                        Program.player.CastSpell(words[1],target);
                    }
                    break;
                case "commands":
                    ListCommands();
                    break;
                case "discard":
                    if (words.Length < 2)
                    {
                        Console.WriteLine("Discard what? Check your inventory.");
                        break;
                    }
                    Program.player.DiscardItem(words[1]);
                    break;
                case "drop":
                    if (words.Length < 2)
                    {
                        Console.WriteLine("Drop what?");
                        break;
                    }
                    Program.player.DropItem(words[1]);
                    break;
                case "equip":
                    if (words.Length < 2)
                    {
                        Console.WriteLine("Equip what? Check your inventory.");
                        break;
                    }
                    Program.player.EquipWeapon(words[1]);
                    break;
                case "examine":
                    if (words.Length < 2)
                    {
                        Console.WriteLine("Examine what? Check your inventory.");
                        break;
                    }
                    if (words[1] == "my")
                    {
                        target = string.Join(' ', words.Skip(2));
                        Program.player.ExamineMine(target);
                    }
                    else
                    {
                        target = string.Join(' ', words.Skip(1));
                        Program.player.ExamineRoom(target);
                    }
                    break;
                case "get":
                    if (words.Length < 2)
                    {
                        Console.WriteLine("Get what?");
                        break;
                    }
                    target = string.Join(' ', words.Skip(1));
                    Program.player.GetItem(target);
                    break;
                case "go":
                    if (words.Length < 2)
                    {
                        Console.WriteLine("Try again with a cardinal direction, such as GO NORTH.");
                        break;
                    }    
                    if (cardinalDirections.Contains(words[1]))
                    {
                        Program.player.Move(words[1]);
                    }
                    else
                    {
                        Console.WriteLine("Try again with a cardinal direction, such as GO NORTH.");
                    }
                    break;
                case "inventory":
                    Program.player.DisplayInventory();
                    break;
                case "look":
                    Program.player.Look();
                    break;
                case "quit":
                    Program.EndGame();
                    break;
                case "status":
                    Program.player.DisplayStatus();
                    break;
                case "use":
                    if (words.Length < 2)
                    {
                        Console.WriteLine("Use what? You can say USE <item> for an object in the room, or USE MY <item> for an object in your inventory.");
                        break;
                    }
                    if (words[1] == "my")
                    {
                        target = string.Join(' ', words.Skip(2)); Program.player.UseMine(target);
                    }
                    else { target = string.Join(' ', words.Skip(1)); Program.player.UseRoom(target); }
                    break;
                case "target":
                    if (words.Length == 1) { Program.player.DisplayTarget(); }
                    if (words[1] == "me" ) { Program.player.SetTarget(Program.player.Name); }

                    break;
                default:
                    Console.WriteLine("I didn't understand that. Type COMMANDS for a list of possible commands you can try.");
                    break;
            }

            GetCommand();
        }

        public void ListCommands()
        {
            Console.WriteLine("Here are all the commands you can use in this demo:");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("ATTACK: Use your equipped weapon to swing at a target enemy.");
            Console.WriteLine("Syntax: attack <target>");
            Console.WriteLine();
            Console.WriteLine("CAST: Activate a spell from your SPELLS list. ");
            Console.WriteLine("Syntax: cast <spell>, only if TARGET is assigned");
            Console.WriteLine("Syntax: cast <spell> <target>, assigns a TARGET before casting");
            Console.WriteLine();
            Console.WriteLine("DISCARD: Toss away an item from your inventory. Warning: this destroys the object and it doesn't come back!");
            Console.WriteLine("Syntax: discard <item>");
            Console.WriteLine();
            Console.WriteLine("DROP: Transfer an object from your inventory into the current room.");
            Console.WriteLine("Syntax: drop <item>");
            Console.WriteLine();
            Console.WriteLine("EQUIP: Transfer an item from your inventory into your equipped slot. Replaces an equipped weapon, if there is one, and returns it to your inventory.");
            Console.WriteLine("Syntax: equip <item>");
            Console.WriteLine();
            Console.WriteLine("EXAMINE: Get a quick description of someone or something. Works with items and characters.");
            Console.WriteLine("Syntax: examine my <target> for inventory, OR examine <target> in");
            Console.WriteLine();
            Console.WriteLine("GET: Pick up an item you see.");
            Console.WriteLine("Syntax: get <item>");
            Console.WriteLine();
            Console.WriteLine("GO: Move in a direction you can see.");
            Console.WriteLine("Syntax: go <direction>");
            Console.WriteLine();
            Console.WriteLine("INVENTORY: Access your backpack.");
            Console.WriteLine("Syntax: inventory");
            Console.WriteLine();
            Console.WriteLine("LOOK: Get a description of the room and all visible things in it.");
            Console.WriteLine("Syntax: look");
            Console.WriteLine();
            Console.WriteLine("STATUS: List all information about your character.");
            Console.WriteLine("Syntax: status");
            Console.WriteLine();
            Console.WriteLine("TARGET: Check or assign a target");
            Console.WriteLine("Syntax: target, displays your current default target");
            Console.WriteLine("Syntax: target <target>, can be someone or something");
            Console.WriteLine("Syntax: target me, for better and for worse this works");
            Console.WriteLine();
            Console.WriteLine("USE: Activate an item in the room or your inventory.");
            Console.WriteLine("Syntax: use my <item> for inventory, OR use <item> for room");
        }
    }
}