using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace Antikythera
{
    /// <summary>
    /// Represents a character in the game with various attributes and properties.
    /// </summary>
    public class Character
    {
        public string Name { get; set; }
        public string Species { get; set; }

        public bool IsAlive { get; set; } = true;

        public bool IsPlayer { get; set; } = false;

        public Room CurrentRoom { get; set; }

        public Room SpawnRoom { get; set; }

        /// <summary>
        /// Attributes
        /// </summary>
        // CON determines total health pool (HP), fortitude saves, and damage resistance
        private int _CON = 6;
        public int CON
        {
            get { return _CON; }
            set { _CON = value; UpdateHealth(); }
        }

        // STR determines physical ability to perform athletic feats, and Stamina Pool (SP)
        public int STR { get; set; } = 6;

        // DEX determines defense, attack+damage rating with ranged weapons, and reflex saves
        private int _DEX = 6;
        public int DEX
        {
            get { return _DEX; }
            set { _DEX = value; UpdateDefense(); }
        }

        // INT determines Mana Pool (MP), ability to use magic items
        public int INT { get; set; } = 6;

        // PER determines Base Perception
        public int PER { get; set; } = 6;

        // WIL determines ability to exert influence on people and matter, improves spell damage, and Mind saves
        public int WIL { get; set; } = 6;

        // POW determines raw damage output from melee, and improves damage for martial abilities
        public int POW { get; set; } = 6;

        // Set base health
        private int _baseHealth = 15;

        // Private backing field for the Health property
        private int _health;

        /// <summary>
        /// Gets the current health of the character.
        /// </summary>
        public int Health
        {
            get { return _health; }
            set { _health = value; }
        }

        // Set base defense
        private int _baseDefense = 8;

        // Private backing field for the Defense property
        private int _defense;

        public int DamageResist
        {
            get { return CON / 3; }
        }

        /// <summary>
        /// Gets the current defense of the character.
        /// </summary>
        public int Defense
        {
            get { return _defense; }
            set { _defense = value; }
        }

        public Weapon EquippedWeapon { get; set; } = new Unarmed();

        public List<Item> Inventory { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Character"/> class.
        /// </summary>
        public Character()
        {
            EquippedWeapon = new Unarmed(); // Create character without any weapons equipped
            UpdateHealth();
            UpdateDefense();
        }

        // The character can now equip weapons to increase damage output
        // Very rough and basic. FIX IMPLEMENTATION by integrating this into an inventory dictionary


        /// <summary>
        /// Updates the health based on the base health and CON attribute.
        /// </summary>
        private void UpdateHealth()
        {
            Health = _baseHealth + CON;
        }

        /// <summary>
        /// Updates the defense based on the base defense and DEX attribute.
        /// </summary>
        private void UpdateDefense()
        {
            Defense = _baseDefense + DEX;
        }

        public void Look() // Display the obvious people and objects in the room
        {
            Console.WriteLine("You look around the room...");
            Console.WriteLine(CurrentRoom.Description);
            Console.Write("You see "); // Start listing people and enemies

            if (CurrentRoom.People.Count == 1) // You are the only one in this room
            { Console.WriteLine("no one here."); }

            if (CurrentRoom.People.Count == 2) // There is someone else here
            {
                for (int i = 0; i < CurrentRoom.People.Count; i++)
                    if (!CurrentRoom.People[i].IsPlayer) 
                        { Console.Write($"{CurrentRoom.People[i].Name} here."); }
            }

            else // You and at least 2 others are here
            {
                bool firstPerson = true;

                for (int i = 0; i < CurrentRoom.People.Count; i++)
                {
                    if (!CurrentRoom.People[i].IsPlayer)
                    {
                        if (!firstPerson)
                        {
                            Console.Write(", ");
                        }
                        Console.Write(CurrentRoom.People[i].Name);
                        firstPerson = false;
                    }
                }
                Console.WriteLine("are here.");

            }

            Console.Write("You see "); // Start listing objects on the ground
            if (CurrentRoom.Objects.Count == 0)
            {
                Console.WriteLine("nothing of interest here.");
            }

            else if (CurrentRoom.Objects.Count == 1)
            {
                Console.WriteLine($"a {CurrentRoom.Objects[0]} here.");
            }

            else
            {
                for (int i = 0; i < CurrentRoom.Objects.Count; i++)
                {
                    Item item = CurrentRoom.Objects[i];

                    if (i == CurrentRoom.Objects.Count - 1 && CurrentRoom.Objects.Count > 1)
                    {
                        // For the last item in a list of more than one item, use "and" before the item name
                        Console.Write("and ");
                    }

                    Console.Write($"a {item.Name}");

                    if (i < CurrentRoom.Objects.Count - 1)
                    { Console.Write(", "); }
                    else 
                    { Console.WriteLine("."); }
                }
            }

            Console.Write("You can leave this room from the "); // Start listing exits
            var exitKeys = new List<string>(CurrentRoom.Exits.Keys);  // Get the list of exit directions

            for (int i = 0; i < exitKeys.Count; i++)
            {
                string exit = exitKeys[i];

                if (i == exitKeys.Count - 1 && exitKeys.Count > 1)
                {
                    // For the last exit in a list of more than one, use "and" before the exit name
                    Console.Write("and ");
                }

                Console.Write(exit);

                if (i < exitKeys.Count - 1)
                { Console.Write(", "); }
                else
                { Console.WriteLine("."); }
            }
        }

        public void Examine() // Display 

        public void Move(string direction) // Move from one room to the other
        {
            if (!CurrentRoom.Exits.Keys.Contains(direction))
            { Console.WriteLine("You can't go that way."); return; }

            CurrentRoom.RemovePerson(this);
            CurrentRoom = CurrentRoom.Exits[direction];
            CurrentRoom.AddPerson(this);

            if (IsPlayer) // Feedback for the player
            {
                Console.WriteLine($"You head {direction} to the {CurrentRoom.RoomName}.");
            }

            if (!IsPlayer && (this.CurrentRoom == Program.player.CurrentRoom) ) // Prevent movement from outside NPCs to generate feedback text
            {
                Console.WriteLine($"{this.Name} moves to the {direction}.");
            }

        }

        public void GetItem(Item item, Room room) // Room item goes straight to inventory, but future method will send it to hands 
        {
            // Can't grab something if it's not there
            if (!room.Objects.Contains(item))
            {
                Console.WriteLine($"You don't see any {item.Name} here.");
                return;
            }

            // Remove from the room and create in your inventory
            room.RemoveItem(item);
            Console.WriteLine($"You pick up the {item.Name}.");

            // Add the item to your inventory
            Inventory.Add(item);
        }

        public void DropItem(Item item, Room room) // Removes from inventory and adds to the room
        {
            if (!Inventory.Contains(item))
            {
                Console.WriteLine($"You don't have that in your possession.");
                return;
            }

            // Populate the room's Objects list with the dropped item
            Inventory.Remove(item);
            room.Objects.Add(item);
            Console.WriteLine($"You drop the {item.Name} on the floor.");
        } 

        public void DiscardItem(Item item) // Removes from inventory and it doesn't exist anymore
        {
            Inventory.Remove(item);
            Console.WriteLine($"You toss the {item.Name} from your inventory.");
        }

        public void DisplayInventory() // Check contents of inventory
        {
            Console.WriteLine("You check your inventory...");

            if (Inventory.Count == 0)
            {
                Console.WriteLine("Your inventory is empty.");
                return;
            }

            for (int i = 0; i < Inventory.Count; i++)
            {
                Item item = Inventory[i];

                if (i == Inventory.Count - 1 && Inventory.Count > 1)
                {
                    // For the last item in a list of more than one item, use "and" before the item name
                    Console.Write("and ");
                }

                Console.Write($"a {item.Name}");

                if (i < Inventory.Count - 1)
                { Console.Write(", "); }
                else
                { Console.WriteLine("."); }
            }
        }

        public void EquipWeapon(Weapon weapon) // Remove from inventory and set equipped weapon
        {
            if (!Inventory.Contains(weapon))
            {
                Console.WriteLine($"You don't have a {weapon.Name} in your inventory.");
                return;
            }

            if (EquippedWeapon.Name == "fists")
            {
                EquippedWeapon = weapon;
                Console.WriteLine($"You equip the {weapon.Name}.");
                Inventory.Remove(weapon);
            }

            else
            {
                Inventory.Add(EquippedWeapon);
                Console.WriteLine($"You unequip the {EquippedWeapon.Name}.");
                EquippedWeapon = weapon;
                Console.WriteLine($"You equip the {weapon.Name}.");
                Inventory.Remove(weapon);
            }

        } 

        public void Attack(Character target) // Use equipped weapon to attack an enemy Character
        {
            // Stop dead character from fighting back
            if (!IsAlive) { return; }
            // Stop non-adjacent characters from attacking each other
            if (CurrentRoom != target.CurrentRoom)
            {
                Console.WriteLine($"You don't see {target.Name} here!");
                return;
            }

            Console.WriteLine($"{Name} swings at {target.Name} with their {EquippedWeapon.Name}...");
            int _swing = EquippedWeapon.RollDamage();
            int _damage = POW + _swing - target.DamageResist;


            if (_damage < 0) { _damage = 1; }

            target.Health -= _damage;

            if (target.Health <= 0)
            {
                target.Health = 0;
                target.IsAlive = false;
                target.Die(); // display "### has died!"
                if (target.IsPlayer) { target.Respawn(); }
            }
            
        } 

        public void Attack(Enemy target) // Use equipped weapon to attack a non-Character enemy
        {
            // Stop dead character from fighting back
            if (!IsAlive) { return; }
            // Stop non-adjacent characters from attacking each other
            if (CurrentRoom != target.CurrentRoom)
            {
                Console.WriteLine($"You don't see {target.Name} here!");
                return;
            }

            Console.WriteLine($"{Name} swings at {target.Name} with their {EquippedWeapon.Name}...");
            int _swing = EquippedWeapon.RollDamage();
            int _damage = POW + _swing - target.DamageResist;

            if (_damage < 0) { _damage = 1; }

            target.Health -= _damage;

            if (target.Health <= 0)
            {
                target.Health = 0;
                target.IsAlive = false;
                target.Die(); // display "### has died!"
                if (target.IsPlayer) { target.Respawn(); }
            }
        } 

        public void Die() // Convert a Character into a Corpse object and drop equipped weapon
        {
            Console.WriteLine($"{Name} has died!");
            if (!(EquippedWeapon is Unarmed))
            {
                Console.WriteLine($"Their {EquippedWeapon.Name} drops to the ground!");
                CurrentRoom.Objects.Add(EquippedWeapon);
                EquippedWeapon = new Unarmed();  // Ensure Unarmed is a valid class
            }
            // Drop your equipped item on the ground and become a corpse
            // Inventory is the only thing that stays the same
        } 

        public void Respawn()
        {

            Console.WriteLine("You break through the surface of the grey, muddled water with a gasp.");
            Console.WriteLine("You... died. But you're here again. Where the killing blow had landed still throbs painfully...");
            Health += _baseHealth;
            CurrentRoom = SpawnRoom;
            IsAlive = true;
        }

    }

    // Example of a derived class
    public class Fannaan : Character
    {
        public Fannaan() : base()
        {
            // Additional initialization for Fannaan class
            string @class = "Fannaan";
        }

        public Fannaan(Character target)
        {
            Name = target.Name;
            Species = target.Species;
            CON = target.CON;
            WIL = target.WIL;
            POW = target.POW;
        }

        public static void CreateFannaanNPC(string name)
        {
            Fannaan NPC = new Fannaan();
            NPC.Name = name;
            NPC.Species = "Human";
        }
    }

    public class Brujadha : Character
    {
        public Brujadha() : base()
        {
            // Additional initialization for Brujadha class
            string @class = "Brujadha";
        }

        public Brujadha(Character target)
        {
            Name = target.Name;
            Species = target.Species;
            CON = target.CON;
            WIL = target.WIL;
            POW = target.POW;
        }


        public static void CreateBrujadhaNPC(string name)
        {
            Brujadha NPC = new Brujadha();
            NPC.Name = name;
            NPC.Species = "Human";
        }
    }

}
