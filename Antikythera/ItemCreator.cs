using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Antikythera
{
    public abstract class Item
    {
        // Base template to create Treasure, Weapon, Armor, Consumable, and Magic Item subclasses
        int Cost { get; set; }
        double Weight { get; set; }
        string Material { get; set; }
    }

    public class Weapon : Item
    {
        // Inherit attributes from Item
        // Add damage range and type
        string DamageType { get; set; }
        int MinDamage { get; set; }
        int MaxDamage { get; set; }
        private static Random random = new Random();

        public int RollDamage()
        {
            return random.Next(MinDamage, MaxDamage + 1);
        }

        public class Spear : Weapon
        {
            public Spear()
            {
                string Name = "spear";
                string DamageType = "pierce";
                int MinDamage = 2;
                int MaxDamage = 4;
            }
        }

        public class Hammer : Weapon
        {
            public Hammer()
            {
                string Name = "hammer";
                string DamageType = "blunt";
                int MinDamage = 1;
                int MaxDamage = 3;
            }
        }

        public class Sickle : Weapon
        {
            public Sickle()
            {
                string Name = "sickle";
                string DamageType = "cut";
                int MinDamage = 1;
                int MaxDamage = 4;
            }
        }

        public class Unarmed : Weapon
        {
            public Unarmed()
            {
                string Name = "fists";
                string DamageType = "blunt";
                int MinDamage = 1;
                int MaxDamage = 2;
            }
        }
    }

    public class  Armor: Item
    {
        // Inherit attributes from Item
        // Add Damage Resistance, Equip Slot
        int DamageResist { get; set; }
        string EquipSlot { get; set; }
    }

    public class Consumable : Item
    {
        // Inherit attributes from Item
        // Add Effect
        // Access with UseItem method
    }
}
