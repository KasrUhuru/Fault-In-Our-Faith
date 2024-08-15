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
        string @class { get; set; } = null;
        public string Description { get; set; }
        public Room CurrentRoom { get; set; }
        public Room SpawnRoom { get; set; }
        public Weapon EquippedWeapon { get; set; } = new Unarmed();
        public List<Item> Inventory { get; set; } = new List<Item>();
        public List<Spell> SpellList { get; set; } = new List<Spell>();
        public Character Target { get; set; }
        Random r = new Random();

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

        public int MaxHealth { get; set; }


        // Set base defense
        private int _baseDefense = 10;

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

        

        /// <summary>
        /// Initializes a new instance of the <see cref="Character"/> class.
        /// </summary>
        public Character()
        {
            UpdateHealth();
            UpdateDefense();
            this.@class = null;
            this.Description = $"You see a {Species}.";
            this.IsPlayer = false;
            this.EquippedWeapon = new Unarmed(); // Create character without any weapons equipped
            this.MaxHealth = Health;
        }

        // The character can now equip weapons to increase damage output
        // Very rough and basic. FIX IMPLEMENTATION by integrating this into an inventory dictionary


        /// <summary>
        /// Updates the health based on the base health and CON attribute.
        /// </summary>
        private void UpdateHealth()
        {
            MaxHealth = _baseHealth + CON;
            Health = _baseHealth + CON;

        }

        /// <summary>
        /// Updates the defense based on the base defense and DEX attribute.
        /// </summary>
        private void UpdateDefense()
        {
            Defense = _baseDefense + DEX;
        }

        public void Attack() // Shorthand method to attack a target
        {
            if  (Target == null)
            {
                Console.WriteLine("You aren't targeting anything!");
                return;
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine($"{Name} swings at {Target.Name} with their {EquippedWeapon.Name}...");
            Console.WriteLine();
            int _bonus = r.Next(1, 20);
            int AttackRoll = this.STR + _bonus;

            if (AttackRoll >= Target.Defense)
            {
                Console.WriteLine($"Attack Roll: {STR} (STR) + {_bonus} (Dice) = {AttackRoll} VS {Target.Defense}");
                Console.WriteLine("Hit!");
                int _swing = EquippedWeapon.RollDamage();
                int _damage = POW + _swing - Target.DamageResist;

                Console.WriteLine($"{POW} (Character POWER) + {_swing} (Damage Roll) - {Target.DamageResist} (Damage Resistance)");
                Console.WriteLine();

                if (_damage < 0) { _damage = 1; }
                Console.WriteLine($"{Name} deals {_damage} {EquippedWeapon.DamageType} damage to {Target.Name}!");

                Target.Health -= _damage;
            }
            else
            {
                Console.WriteLine($"Attack Roll: {STR} (STR) + {_bonus} (Dice) = {AttackRoll} VS {Target.Defense}");
                Console.WriteLine("Miss!");
            }

            if (Target.Health <= 0)
            {
                Target.Health = 0;
                Target.IsAlive = false;
                Target.Die();
                if (Target.IsPlayer) { Target.Respawn(); }
            }
        }
        public void Attack(string targetName) // Use equipped weapon to attack an enemy Character
        {
            // Convert from string user input to Character object reference
            Character target = CurrentRoom.People.FirstOrDefault(p => p.Name.Equals(targetName, StringComparison.OrdinalIgnoreCase));

            // Stop dead character from fighting back
            if (!IsAlive) { return; }

            // Stop non-adjacent characters from attacking each other
            if (target == null)
            {
                Console.WriteLine($"You don't see a {targetName} here!");
                return;
            }

            this.Target = target;

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine($"{Name} swings at {Target.Name} with their {EquippedWeapon.Name}...");
            Console.WriteLine();
            int _bonus = r.Next(1, 20);
            int AttackRoll = this.STR + _bonus;

            if (AttackRoll >= Target.Defense)
            {
                Console.WriteLine($"Attack Roll: {STR} (STR) + {_bonus} (Dice) = {AttackRoll} VS {Target.Defense}");
                Console.WriteLine("Hit!");
                int _swing = EquippedWeapon.RollDamage();
                int _damage = POW + _swing - Target.DamageResist;

                Console.WriteLine($"{POW} (Character POWER) + {_swing} (Damage Roll) - {Target.DamageResist} (Damage Resistance)");
                Console.WriteLine();

                if (_damage < 0) { _damage = 1; }
                Console.WriteLine($"{Name} deals {_damage} {EquippedWeapon.DamageType} damage to {Target.Name}!");

                Target.Health -= _damage;
            }
            else
            {
                Console.WriteLine($"Attack Roll: {STR} (STR) + {_bonus} (Dice) = {AttackRoll} VS {Target.Defense}");
                Console.WriteLine("Miss!");
            }

            if (Target.Health <= 0)
            {
                Target.Health = 0;
                Target.IsAlive = false;
                Target.Die();
                if (Target.IsPlayer) { Target.Respawn(); }
            }
        }
        public async Task AttackPlayerAsync(Character attacker, Character player) // Automated NPC logic for attacking the player 
        {
            // Wait for 5 seconds before the first attack
            await Task.Delay(5000);

            while (player.IsAlive && attacker.CurrentRoom == player.CurrentRoom)
            {
                // Attack the player
                attacker.Attack(player.Name);

                // Wait for 3 seconds before attacking again
                await Task.Delay(3000);
            }
        }
        public void CastSpell(string spellName) // Cast a spell with implicit target
        {
            Spell spell = SpellList.FirstOrDefault(p => p.Name.Equals(spellName, StringComparison.OrdinalIgnoreCase));

            if (spell != null)
            { Console.WriteLine($"You don't know {spellName}! Try your SPELLS list to see what you can cast."); return; }    

            if (@class != "brujadha")
            { Console.WriteLine($"You cast {spellName} - a little toot squeaks out of your hands! You're not a spellcaster!"); }

            else
            { Console.WriteLine($"You cast {spellName} - a little toot squeaks out of your hands! You're a spellcaster but this feature is not yet implemented!"); }
        }
        public void CastSpell(string spellName, string target) // Cast a spell while designating an explicit target
        {
            Character Target = CurrentRoom.People.FirstOrDefault(p => p.Name.Equals(target, StringComparison.OrdinalIgnoreCase));

            Spell spell = SpellList.FirstOrDefault(p => p.Name.Equals(spellName, StringComparison.OrdinalIgnoreCase));

            if (spell != null) { Console.WriteLine(); }
            
            if (target == "me") { this.Target = this; }

            if (Target == null) { Console.WriteLine($"You don't see a {target} here!"); return;}

            if (@class != "brujadha") { Console.WriteLine($"You cast {spellName} - a little toot squeaks out of your hands at {target}! You're not a spellcaster!"); }

            else { Console.WriteLine($"You cast {spellName} - a little toot squeaks out of your hands at {target}! You're a spellcaster but this feature is not yet implemented!"); }
        }
        public void Die() // Convert a Character into a Corpse object and drop equipped weapon
        {
            Console.WriteLine($"{Name} has died!");
            if (!(EquippedWeapon is Unarmed))
            {
                CurrentRoom.Objects.Add(EquippedWeapon);
                Console.WriteLine($"Their {EquippedWeapon.Name} drops to the ground!");
                EquippedWeapon = new Unarmed();
                if (IsPlayer) { CurrentRoom.RemovePerson(this) ; }
                if (!IsPlayer)
                {
                    Name = $"{Name}'s corpse";
                }
            }

            if (IsPlayer)
            {
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("*******************************");
                Console.WriteLine();
                Console.WriteLine("YOU HAVE DIED.");
                Console.WriteLine();
                Console.WriteLine("*******************************");
                Console.WriteLine();
                Console.WriteLine();
                Console.ReadLine();
            }
        }
        public void DisplayInventory() // Check contents of inventory
        {
            Console.WriteLine("You check your inventory...");

            if (Inventory.Count == 0)
            {
                Console.WriteLine("Your inventory is empty.");
                return;
            }

            Console.Write("You have ");

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
        public void DiscardItem(string itemName) // Removes from inventory and it doesn't exist anymore
        {
            Item item = Inventory.FirstOrDefault(p => p.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));

            if (item == null)
            {
                Console.WriteLine("You don't have that in your inventory.");
                return;
            }

            Inventory.Remove(item);
            Console.WriteLine($"You toss the {itemName} from your inventory.");
        }
        public void DisplayStatus() // Check Health, Mana, and Stamina
        {
            Console.WriteLine();
            Console.WriteLine($"You're currently at {Program.player._health}/{Program.player.MaxHealth} HP.");

            double healthPercentage = (double)Program.player._health / Program.player.MaxHealth;

            if (healthPercentage == 1.0)
            {
                Console.WriteLine("You feel perfectly healthy.");
            }
            else if (healthPercentage >= 0.75)
            {
                Console.WriteLine("You're injured, but you feel okay.");
            }
            else if (healthPercentage >= 0.50)
            {
                Console.WriteLine("You're moderately injured.");
            }
            else if (healthPercentage >= 0.25)
            {
                Console.WriteLine("You're severely injured, you may not survive another encounter.");
            }
            else if (healthPercentage > 0)
            {
                Console.WriteLine("You're at death's door. Seek help.");
            }
            Console.WriteLine($"Mana: {INT + 10}/{INT + 10}");
            Console.WriteLine();
            Console.WriteLine($"CONSTITUTION: {CON}");
            Console.WriteLine($"POWER: {POW}");
            Console.WriteLine($"WILL: {WIL}");
            Console.WriteLine($"STRENGTH: {STR}");
            Console.WriteLine($"INTELLIGENCE: {INT}");
            Console.WriteLine($"DEXTERITY: {DEX}");
            Console.WriteLine($"PERCEPTION: {PER}");
            Console.WriteLine();
            Console.WriteLine("Combat Skills");
            Console.WriteLine("********************************");
            Console.WriteLine($"Damage Resistance: {DamageResist} (CON/3)");
            Console.WriteLine($"Spell Damage Bonus: {WIL} (WIL)");
            Console.WriteLine($"Current Weapon: {EquippedWeapon.Name}");
            Console.WriteLine($"Weapon Damage Bonus: {EquippedWeapon.MinDamage}-{EquippedWeapon.MaxDamage}");
            Console.WriteLine($"Physical Damage Type(s): {EquippedWeapon.DamageType}");
            Console.WriteLine($"Physical Damage Bonus: {POW} (POWER)");
            Console.WriteLine("********************************");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
        }
        public void DisplayTarget() // Check what you're targeting
        {
            if (Target == null)
            {
                Console.WriteLine("You aren't currently targeting anything or anyone.\nYou can use target ME to target yourself.\nYou can also use target <target>.");
                return;
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine($"Currently targeting: {Target.Name}.");
        }
        public void DropItem(string itemName) // Removes from inventory and adds to the room
        {
            Item item = Inventory.FirstOrDefault(p => p.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));

            if (item == null)
            {
                Console.WriteLine($"You don't have that in your possession.");
                return;
            }

            // Populate the room's Objects list with the dropped item
            Inventory.Remove(item);
            CurrentRoom.Objects.Add(item);
            Console.WriteLine($"You drop the {itemName} on the floor.");
        }
        public void EquipWeapon(string weaponName) // Remove from inventory and set equipped weapon
        {
            Item item = Inventory.FirstOrDefault(p => p.Name.Equals(weaponName, StringComparison.OrdinalIgnoreCase));

            if (item == null || !(item is Weapon))
            {
                Console.WriteLine($"You don't have a {weaponName} in your inventory.");
                return;
            }

            Weapon weapon = item as Weapon;

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
        public void GetItem(string itemName) // Room item goes straight to inventory, but future method will send it to hands 
        {
            Item item = CurrentRoom.Objects.FirstOrDefault(p => p.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));

            if (item == null)
            {
                Console.WriteLine($"You don't see any {itemName} here.");
                return;
            }

            if (item is RoomFixture roomfixture)
            {
                Console.WriteLine($"You can't pick up the {roomfixture.Name}.");
                return;
            }

            // Remove from the room and create in your inventory
            CurrentRoom.RemoveItem(item);
            Console.WriteLine($"You pick up the {itemName}.");

            // Add the item to your inventory
            Inventory.Add(item);
        }
        public void ExamineRoom(string target) // Display the description of an Item or Character
        {
            Item item = CurrentRoom.Objects.FirstOrDefault(p => p.Name.Equals(target, StringComparison.OrdinalIgnoreCase));

            Character character = CurrentRoom.People.FirstOrDefault(p => p.Name.Equals(target, StringComparison.OrdinalIgnoreCase));

            if (item == null && character == null)
            {
                Console.WriteLine($"You don't see any {target} here.\nYou can use EXAMINE MY \"{target.ToUpper()}\" ");
                return;
            }

            else if (item == null && character != null)
            {
                Console.WriteLine(character.Description);
                return;
            }

            else if (item != null && character == null)
            {
                Console.WriteLine(item.Description);
                return;
            }

        }
        public void ExamineMine(string itemName) // Display the description of an Item in your Inventory
        {
            Item item = Inventory.FirstOrDefault(p => p.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));

            if (item == null)
            {
                Console.WriteLine($"You don't have that in your possession.");
                return;
            }

            Console.WriteLine(item.Description);
        }
        public void Look() // Display the obvious people and objects in the room
        {
            Console.WriteLine("You look around the room...");
            Console.WriteLine();
            Console.WriteLine(CurrentRoom.Description);
            Console.WriteLine();
            Console.Write("People: You see "); // Start listing people and enemies

            if (CurrentRoom.People.Count == 1) // You are the only one in this room
            { Console.WriteLine("no one here."); }

            if (CurrentRoom.People.Count == 2) // There is someone else here
            {
                for (int i = 0; i < CurrentRoom.People.Count; i++)
                    if (!CurrentRoom.People[i].IsPlayer)
                    { Console.WriteLine($"{CurrentRoom.People[i].Name} here."); }
            }

            if (CurrentRoom.People.Count > 2) // You and at least 2 others are here
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

            Console.Write("Items: You see "); // Start listing objects on the ground
            if (CurrentRoom.Objects.Count == 0)
            {
                Console.WriteLine("nothing of interest here.");
            }

            else if (CurrentRoom.Objects.Count == 1)
            {
                Console.WriteLine($"a {CurrentRoom.Objects[0].Name} here.");
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

            Console.Write("You can exit this room from the "); // Start listing exits
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
                Look();
            }

            // Check for non-player characters or enemies in the room
            foreach (var character in CurrentRoom.People)
            {
                if (!character.IsPlayer)
                {
                    // Start the asynchronous attack task
                    Task.Run(() => character.AttackPlayerAsync(character, this));
                }
            }

            if (!IsPlayer && (this.CurrentRoom == Program.player.CurrentRoom)) // Prevent movement from outside NPCs to generate feedback text
            {
                Console.WriteLine($"{this.Name} moves to the {direction}.");
            }

        }
        public void Respawn() // The player returns to their designated SpawnRoom

        {
            Console.WriteLine("You can see the After-Sea. It is the color of the summer sky. It's... enormous, and yet it churns gently.");
            Console.WriteLine();
            Console.WriteLine("This is where you're supposed to be. Your soul begs for entry into its welcoming warmth.");
            Console.WriteLine();
            Console.ReadLine();
            Console.WriteLine("Anahata denies you. A freezing current grips your helpless form and sends you tumbling upwards, upwards, upwards, and then...");
            Console.WriteLine();
            Console.ReadLine();
            Console.WriteLine("You break through the surface of the grey, muddled water with a gasp.");
            Console.WriteLine();
            Console.WriteLine("You died again. But you're here again. Wherever here is.");
            Console.WriteLine();
            Console.WriteLine("Where the killing blow had landed on you, it still throbs painfully...\n\nIt dawns on you that death is not an escape.");
            Console.WriteLine();
            Console.WriteLine("What do you do?");
            Health += MaxHealth / 2;
            CurrentRoom = SpawnRoom;
            CurrentRoom.AddPerson(Program.player);
            IsAlive = true;
        } 
        public void SetTarget(string target) // Character focuses on something
        {
            Character _target = CurrentRoom.People.FirstOrDefault(p => p.Name.Equals(target, StringComparison.OrdinalIgnoreCase));

            if (_target == null)
            {
                Console.WriteLine($"You don't see any {target} here.");
                return;
            }

            Console.WriteLine($"You are now targeting any {_target.Name} that you see.");
        }
        public void UseRoom(string itemName) // Character activates an item in the room
        {
            Item item = CurrentRoom.Objects.FirstOrDefault(p => p.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));

            if (item == null)
            {
                Console.WriteLine($"You don't see any {itemName} here.");
                return;
            }

            if (item is HealingFixture healingFixture)
            {
                Console.WriteLine(healingFixture.UseText);
                healingFixture.FullHeal(this);
                Console.WriteLine(healingFixture.EffectText);
            }
        }
        public void UseMine(string itemName) // Character activates an item from their inventory 
        {
            Item item = Inventory.FirstOrDefault(p => p.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));

            if (item == null)
            {
                Console.WriteLine($"You don't have that in your possession.");
                return;
            }

            if (item is Consumable consumable)
            {
            // FIXME: item.ProduceEffect(this);
            Console.WriteLine($"You consume the {item.Name}... you can't tell if it did anything.");
            Inventory.Remove(item);
            }
            else
            {
                Console.WriteLine("This can't be consumed.");
                return;
            }

        }

    }

    // Example of a derived class
    public class Fannaan : Character
    {
        public Fannaan() : base()
        {
            // Additional initialization for Fannaan class
            string @class = "fannaan";
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
            NPC.Species = "human";
        }
    }

    public class Brujadha : Character
    {
        public Brujadha() : base()
        {
            // Additional initialization for Brujadha class
            string @class = "brujadha";
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
            NPC.Species = "human";
        }
        // Add classes here



        public class Spell
        {
            // Magical effect generated from a spellcaster's will and incantation
            // Some cost MP, but basic weak spells don't - ensure they are balanced with weapon attacks
            public string Name { get; set; }
            public string Chant { get; set; }
            public string Description { get; set; }
            public double CastTime { get; set; }
            public int CastCost { get; set; }
            public bool IsMelee { get; set; } = true;
            public string DamageType { get; set; }
            public int Damage { get; set; }

            // Name: Ice Lash
            // Chant: "ura"
            // Description: A sliver of frozen liquid made from ambient moisture that breaks after a swing, regardless of hit or miss
            // Cast Time: Casting and attacking are the same action
            // Cast Cost: 0
            // Range: Melee
            // Damage Types: Cold, Cut 
            // Damage: Equal to WILL + 1-4

            // Name: Concuss
            // Chant: "asa"
            // Description: Generate a brief explosion from your fingertips at a target
            // Cast Time: Casting and attack are the same action
            // Cast Cost: 0
            // Range: Melee
            // Damage Types: Fire, Blunt
            // Damage: Equal to WILL + 1-4

            public Spell()
            {
                // Construct a spell to add to the Character.SpellList
            }
        }
    }
    // Namespace DMZ
}