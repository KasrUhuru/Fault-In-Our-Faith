using System;
using System.Threading;

namespace Antikythera;
// Antikythera

// This program should do the following:
// 1. Initialize the Game and prevent crashes from invalid input
// 2. With minimal prompting, create a Character with a Class and a Starting Weapon (or Starting Spell).
// 3. With minimal prompting, create an Enemy to fight
// 4. Make Character and Enemy fight (or Character and Character) until one of them generates a "### has died!" message.
// 5. Climb the roots to end the demo

class Program
{
    public static Character player = new Character();
    public static Character nemesis = new Character(); // Placeholder to be used later in the demo

    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to the world of Antikythera!");
        Console.WriteLine();
        Console.WriteLine("This is a BARE BONES DEMO of what the true game will be. For now, I present to you the basic concepts.");
        Console.WriteLine();
        Console.WriteLine("Do you want to read the intro? You can type YES or NO.");
        Console.WriteLine("YES, I want to learn a little bit about this world.");
        Console.WriteLine("NO, I've already seen it and want to get to the game.");

        string command = Console.ReadLine().ToLower();
        while (command != "no" && command != "yes" && command != "fast")
        {
            Console.WriteLine($"{command} is not a valid command. Either type YES or NO to continue.");
            command = Console.ReadLine().ToLower();
        }

        if (command == "no") { StoryStart(); }
        if (command == "yes") { Intro(); }
        if (command == "fast") { FastStart(); }


    }

    static void FastStart()
    {
        Console.Write("Name: ");
        player.Name = Console.ReadLine();
        Console.Write("Species: ");
        player.Species = Console.ReadLine();
        Console.Write("Boon: ");
        string boon = Console.ReadLine().ToLower();
        if (boon == "ogre") { player.CON += 3; }
        else if (boon == "antlion") { player.POW += 3; }
        else { player.WIL += 3; }
    }

    static void StoryStart()
    {
        Console.WriteLine("A soft, powerful voice echoes into your consciousness...");
        Console.WriteLine("'Arise from the clay, my Champion. I will it.'");
        Thread.Sleep(1000);
        Console.WriteLine();
        Console.WriteLine("'I have done the work of carving your form. Now... you form yourself.'");
        Console.Write("'Focus now. What is your name?' ");
        player.Name = Console.ReadLine();

        while (player.Name == null)
        {
            Console.WriteLine("'Shh. Slow down. Your name is the most important thing you possess.'");
        }

        Console.Clear();
        Console.WriteLine($"'You are a raindrop in a storm, {player.Name}. An eternity precedes you, and an eternity shall succeed you.'");
        Console.WriteLine();
        Console.WriteLine("'Three of my children still walk freely under the sun.'");
        Console.WriteLine($"'Are you HUMAN, {player.Name}? The most mischievous of my children, forever bent unto the unknown?'");
        
        Console.WriteLine();
        Console.WriteLine("'Are you a DWARF, my Champion? Descended from the First of the First Ones who made the world?'");
        
        Console.WriteLine();
        Console.WriteLine("'Or are you PESHMURGA? Molded from ash, ushered into a world aflame?'");
        
        Console.WriteLine();

        string command = Console.ReadLine().ToLower();
        while (command != "human" && command != "dwarf" && command != "peshmurga")
        {
            Console.WriteLine("'The After-Sea does not hold this soul. Your form obeys your intent. Focus.'");
            command = Console.ReadLine().ToLower();
        }

        string transform;
        switch (command)
        {
            case "human":
                player.Species = "Human";
                Console.WriteLine("Your body molds itself into a lean anatomy, colored no differently from the loamy ground.");
                break;
            case "dwarf":
                player.Species = "Dwarf";
                Console.WriteLine("Your body molds itself into a shorter, robust anatomy, colored no differently from the loamy ground.");
                break;
            case "peshmurga":
                player.Species = "Peshmurga";
                Console.WriteLine("Your body molds itself into a tall, muscled anatomy, with bony plates covering your face, head, back, arms, and hips.");
                break;
        }

        Console.ReadLine();
        Console.Clear();
        Console.WriteLine("You realize you are facedown. Buried... but only barely. " +
            "No sooner do you envision your arms to push yourself to your knees than you feel how they mold themselves.");
        Console.WriteLine();
        Console.WriteLine("You... are made of clay. You can see your body is not flesh.");
        Console.WriteLine();
        Console.WriteLine("You open your mouth to scream, but there is no mouth yet. Your face splits and contorts into pained existence. There is no voice yet.");

        Console.WriteLine("'Shhh... you are becoming. Focus on my voice, my child.'");
        Console.WriteLine("'You are almost complete... now you must accept a boon.'");

        Console.WriteLine("Boon of the ANTLION (Choosing this option will grant you +3 POWER, which makes your physical strikes more dangerous.)");
        Console.WriteLine();
        Console.WriteLine("Boon of the PEACOCK (Choosing this option will grant you +3 WILLPOWER, which makes your spells and your influence on others stronger.)");
        Console.WriteLine();
        Console.WriteLine("Boon of the OGRE (Choosing this option will grant you +3 CONSTITUTION, which makes you more resistant to harm.)");
        Console.WriteLine();


        command = Console.ReadLine().ToLower();
        while (command != "antlion" && command != "peacock" && command != "ogre")
        {
            Console.WriteLine("'You are so close. Focus, my child.'");
            command = Console.ReadLine().ToLower();
        }

        switch (command)
        {
            case "antlion":
                player.POW += 3;
                Console.WriteLine("Your physique is molded further... your muscles ripple. The ferocity of a predator rumbles within you.");
                break;
            case "peacock":
                player.WIL += 3;
                Console.WriteLine("Your mind feels sharper... your skull pulses. The song of creation rings in your consciousness.");
                break;
            case "ogre":
                player.CON += 3;
                Console.WriteLine("Your skin thickens... your heartbeat is louder. A guttural roar sings through your soul.");
                break;
        }

        Console.WriteLine("As this happens, your face splits open at last. You scream." +
            "At last... you scream.");

    }

    static void Intro()
    {
        
        Console.WriteLine("Until indicated, you can just hit ENTER to progress.");
        Console.ReadLine();
        Console.WriteLine();
        Console.WriteLine("First, you must understand where you are, and why you should care...");
        Console.ReadLine();
        Console.WriteLine("We are all connected. All living things have sway on others.");
        Console.WriteLine("The impulse of life is present in all things that grow. It pulses in hearts and roots alike.");
        Console.WriteLine("The beauty of entropy is present in the living and nonliving.");
        Console.WriteLine("We were formed in the discordant harmony of the universe. We are the ghosts of stars observing what comes after the supernova.");
        Console.ReadLine();
        Console.WriteLine("We are born from the decay of beautiful things. And life consumes life for the same reason.");
        Console.ReadLine();
        Console.Clear();
        Console.WriteLine("Centuries ago, a consort of kings, empresses, and magicians turned their backs on this balance. They refused to decay.");
        Console.ReadLine();
        Console.WriteLine("They amassed the wealth and resources of generations to build a grand pyramid, and conspired to ascend to godhood together.");
        Console.ReadLine();
        Console.Clear();
        Console.WriteLine("It isn't known what happened at the apex of their ambition.");
        Console.WriteLine("Historians and theologists contend different hypotheses as to what occurred at the conclusion of their ritual.");
        Console.WriteLine("The earth itself roiled like a stormy ocean. The heavens roared as if a great beast had awakened. The air turned to fire for miles around.");
        Console.ReadLine();
        Console.WriteLine("Some say they had succeeded, and the display of raw, unbridled destruction was the death of the gods whose places they took.");
        Console.WriteLine("Others say they destroyed themselves in their hubris. A hopeful thought.");
        Console.Clear();
        Console.WriteLine("The pyramid erupted into a rubbled husk of its former glory. The monarchs and their magicians were never again seen since...");
        Console.WriteLine("But their influence has been felt for centuries.");
        Console.ReadLine();
        Console.Clear();
        Console.WriteLine("Greedy souls have since conspired to rebuild the pyramid.");
        Console.WriteLine("Lesser, weaker, pettier men and women have restored it, and have been rewarded with oblivion and destruction each time.");
        Console.WriteLine("The same sickness of spirit that draws them near... it makes them turn on one another at the final hour.");
        Console.ReadLine();
        Console.Clear();
        Console.WriteLine("And then there is you.");
        Console.WriteLine("You, too, defied the natural order of life.");
        Console.WriteLine("You died trying to cling to immortality, and it has stained your very spirit.");
        Console.ReadLine();
        Console.Clear();
        Console.WriteLine("Anahata, the Goddess of Water, Fertility, and Harmony, has denied your soul passage into the After-Sea.");
        Console.ReadLine();
        Console.Clear();
        Console.WriteLine("She will carve you from the clay, just as her First Ones were, to enact penance and restore balance as her Champion.");
        Console.WriteLine("Your soul will not know rest until she is satisfied.");
        Console.WriteLine();
        Console.WriteLine("The scars of your actions will become clear to you in time.");
        Console.ReadLine();
        Console.Clear();

        // Initialize character creation
        StoryStart();
    }
}



/////////////////////////////////////// Initialize the Game

// Greet the player and introduce them to the world of Antikythera
// "This is a bare bones working demo to showcase the mechanics of the game"

// Direct the player to start character creation with some informed prompts
// The brujadha uses cunning magic, and the peshmerga uses martial might

// The player will choose their name, species, and class before being initialized in the world

// The starting room will be a clay pit where they are made. Sunlight struggles to touch the bottom of this room.
// The player will be prompted to LOOK around and SEARCH as well
// Basic commands can be learned in this starting room by READING OBELISK:
// Attack, Cast a Spell, Move (Direction), Get Item, Eat Item, and Equip Item

// Different sigils can be seen on the walls around this pit:
// Spear, Hammer, and Sickle
// The player can TOUCH a sigil to call the CreateItem method for the specific item

// To the north, a dark tunnel leads to another dimly lit clay pit 
// A collection of sigils can be seen on the walls:
// A praying mantis, a deformed humanoid (kobold), a brujadha, and a peshmerga
// The player can TOUCH a sigil to call the CreateEnemy or CreateCharacter methods

// 

// To the southeast, roots and vines form a lattice that could be climbed to the surface
// The player can CLIMB ROOTS to attempt an Athletic skill check (DC 10)

// Now out of the clay pits the demo ends