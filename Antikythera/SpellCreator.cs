using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Antikythera
{
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
        // Damage: Equal to WILL + 1-2

        // Name: Concuss
        // Chant: "asa"
        // Description: Generate a brief explosion from your fingertips at a target
        // Cast Time: Casting and attack are the same action
        // Cast Cost: 0
        // Range: Melee
        // Damage Types: Fire, Blunt
        // Damage: Equal to WILL + 1-2

        public void AddSpell(Spell spell)
        {
            // Add logic
        }
    }
    // Namespace DMZ
}
