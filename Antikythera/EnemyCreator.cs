using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Antikythera
{
    public abstract class Beast
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

    public abstract class Humanoid
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
