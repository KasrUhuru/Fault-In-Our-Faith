using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace Antikythera
{
    /// <summary>
    /// Represents a character in the game with various attributes and properties.
    /// </summary>
    public abstract class Character
    {
        public string Name { get; set; }
        public string Species { get; set; }

        public bool Alive { get; set; } = true;

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
            private set { _health = value; }
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
            private set { _defense = value; }
        }

        public Weapon EquippedWeapon { get; private set; }


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

        public void EquipWeapon(Weapon weapon)
        {
            // Remove from personal inventory and create in Weapon Slot
            EquippedWeapon = weapon;
        }

        public void Attack(Character attacker, Character target)
        {
            Console.WriteLine($"{attacker.Name} swings at {target.Name} with their {EquippedWeapon.Name}...");
            int _swing = attacker.EquippedWeapon.RollDamage();
            int _damage = attacker.POW + _swing - target.DamageResist;

            if (_damage < 0) { _damage = 1;}

            target.Health -= _damage;

            if (target.Health <= 0)
            {
                target.Health = 0;
                target.Alive = false;
            }
            
        }

        public void Attack(Character attacker, Enemy target)
        {

        }
    }

    // Example of a derived class
    public class Peshmurga : Character
    {
        public Peshmurga() : base()
        {
            // Additional initialization for Peshmurga class
            string @class = "Peshmurga";
        }

        public static void CreatePeshmurgaNPC(string name)
        {
            Peshmurga NPC = new Peshmurga();
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

        public static void CreateBrujadhaNPC(string name)
        {
            Brujadha NPC = new Brujadha();
            NPC.Name = name;
            NPC.Species = "Human";
        }
    }

}
