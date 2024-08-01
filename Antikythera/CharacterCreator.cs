using System;
using System.Collections.Generic;

namespace Antikythera
{
    /// <summary>
    /// Represents a character in the game with various attributes and properties.
    /// </summary>
    public abstract class Character
    {
        public string Name { get; set; }
        public string Species { get; set; }

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

        // WIL determines ability to exert influence on people and matter, improves spell damage, mind saves
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

        /// <summary>
        /// Gets the current defense of the character.
        /// </summary>
        public int Defense
        {
            get { return _defense; }
            private set { _defense = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Character"/> class.
        /// </summary>
        public Character(string name)
        {
            string Name = name;
            UpdateHealth();
            UpdateDefense();
        }

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
    }

    // Example of a derived class
    public class Warrior : Character
    {
        public Warrior(string name) : base(name)
        {
            string Name = name;
            // Additional initialization for Warrior class
        }
    }

    public class Mage : Character
    {
        public Mage(string name) : base(name)
        {
            // Additional initialization for Mage class
        }
    }
}
