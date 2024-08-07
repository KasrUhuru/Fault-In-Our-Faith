using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Antikythera
{
    public abstract class Enemy
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

        public Weapon EquippedWeapon { get; set; }

        public void Attack(Enemy attacker, Character target)
        {
            int _swing = attacker.EquippedWeapon.RollDamage();
            int _damage = attacker.POW + _swing - target.DamageResist;

            if (_damage < 0) { _damage = 1; }

            target.Health -= _damage;

            if (target.Health <= 0)
            {
                target.Health = 0;
                target.Alive = false;
            }
        }


        public abstract class Beast : Enemy
        {
            // Generally neutral, but hostile to attackers
            // NOT capable of wielding weapons and armor; do not initialize an inventory for them
            // Anatomy is unique within each species
            public List<string> Tags = new List<string> { "Beast" };

            // Jarad-khatir: wolf-sized praying mantis, hostile to all living creatures. Always hungry, always hunting.
            // Khammac: dog-chicken hybrid. Friendly to most people, often found in civilization.
            // Peacock griffin: Ranges from horse-sized to elephant-sized. Territorial and dangerous.


            public abstract class Insect : Beast
            {
                // Initialize general anatomy for multi-legged enemies such as praying mantis or giant ants
                // Inherit Beast tag

                public Insect()
                {
                    Tags.Add("Insect");
                }
            }

            public class Mantis : Insect
            {
                // Initialize anatomy for 6 legs, 2 claws, 2 wings, 1 head, thorax, abdomen
                public Mantis()
                {
                    Tags.Add("Mantis");
                }
            }

            public abstract class Griffin : Beast
            {
                // Initialize anatomy for creatures: 4-legged && 2-winged && 1 tail
                public Griffin()
                {
                    Tags.Add("Griffin");
                }
            }

            public class LesserPeacockGriffin : Griffin
            {
                // Horse-sized creature that can be tamed by a skilled player
            }

            public class PeacockGriffin : LesserPeacockGriffin
            {
                // Moose-sized creature, too large and aggressive to be tamed
                // Increase all base stats. Give magic abilities
            }

            public class GreaterPeacockGriffin : PeacockGriffin
            {
                // Elephant-sized creature. Too intelligent and strong-willed to be tamed.
                // Increase all base stats. Give telepathy?
            }

        }

        public abstract class Humanoid : Enemy
        {
            // Any creature hostile to civilization
            // Capable of wielding weapons and armor like Characters
            // Anatomy is 95% comparable to Characters
            public List<string> Tags = new List<string> { "Humanoid" };

            // Kobold, deformed and cruel. Attacks legs and arms first.
            // Groqqa, lithe and sneaky. Ambushes in the darkness, aims for the head.
            // Ogre, massive and brutish. Randomly aims at different limbs with massive swings.

        }

        public abstract class Kobold : Humanoid
        {

            public Kobold()
            {
                // Inherit general Humanoid characteristics such as Limbs and Inventory
                // Initialize species as Kobold, inherit Humanoid tag
                Tags.Add("Kobold");
            }

            public class KoboldRunt : Kobold
            {
                // Assign statistics for a low-threat foe that a prepared player can defeat with careful tactics
            }

            public class KoboldWarrior : Kobold
            {
                // Assign statistics for a dangerous foe with a better weapon and melee abilities
            }

            public class KoboldShaman : Kobold
            {
                // Assign statistics for a spellcaster with a magic staff
                // Access to this foe is difficult for the safety of the player
                public KoboldShaman()
                {
                    Tags.Add("Spellcaster");
                }
            }

        }
    }
}
