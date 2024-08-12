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
        public string Name { get; set; }
        public int Cost { get; set; }
        public double Weight { get; set; }
        public string Material { get; set; }
        public Room CurrentRoom { get; set; }
    }

    public class Weapon : Item
    {
        // Inherit attributes from Item
        // Add damage range and type
        public string DamageType { get; set; }
        public int MinDamage { get; set; }
        public int MaxDamage { get; set; }
        public static Random random = new Random();

        public int RollDamage()
        {
            return random.Next(MinDamage, MaxDamage + 1);
        }
    }
        public class Pickaxe : Weapon
        {
            public Pickaxe()
            {
                this.Name = "pickaxe";
                this.DamageType = "pierce";
                this.MinDamage = 2;
                this.MaxDamage = 4;
            }
        }

        public class Hammer : Weapon
        {
            public Hammer()
            {
                this.Name = "hammer";
                this.DamageType = "blunt";
                this.MinDamage = 1;
                this.MaxDamage = 3;
            }
        }

        public class Shovel : Weapon
        {
            public Shovel()
            {
                this.Name = "shovel";
                this.DamageType = "cut";
                this.MinDamage = 1;
                this.MaxDamage = 4;
            }
        }

        public class Unarmed : Weapon
        {
            public Unarmed()
            {
                this.Name = "fists";
                this.DamageType = "blunt";
                this.MinDamage = 1;
                this.MaxDamage = 2;
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