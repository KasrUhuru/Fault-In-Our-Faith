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
    public static Character nemesis1 = new Character(); // Placeholder to be used later in the demo
    public static Character nemesis2 = new Character(); // These 2 nemeses will make the choices that the player doesn't
    public static InputController inputController = new InputController();
    public static bool IsPlaying = true;

    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to the world of Antikythera!");
        Console.WriteLine();
        Console.WriteLine("This is a BARE BONES DEMO of what the true game will be. For now, I present to you the basic concepts.");
        Console.WriteLine();
        Console.WriteLine("Do you want to read the intro? You can type YES or NO.");
        Console.WriteLine();
        Console.WriteLine("YES, I want to learn a little bit about this world.");
        Console.WriteLine();
        Console.WriteLine("NO, I've already seen it and I want to create my character.");

        string command = Console.ReadLine().ToLower();
        while (command != "no" && command != "yes" && command != "fast")
        {
            Console.WriteLine($"{command} is not a valid command. Either type YES or NO to continue.");
            command = Console.ReadLine().ToLower();
        }

        Console.Clear();

        if (command == "no")
        {
            StoryStart();
        }
        if (command == "yes")
        {
            Intro();
        }
        if (command == "fast")
        {
            FastStart();
        }



    }

    static void FastStart()
    {
        // This method initializes the player without the exposition

        // The nemesis characters become what the player doesn't
        Console.Write("Name: ");
        player.Name = Console.ReadLine();
        nemesis1.Name = $"Ur'{player.Name}";
        nemesis2.Name = $"Da-{player.Name}";

        Console.Write("Species: ");
        player.Species = Console.ReadLine().ToLower();
        if (player.Species == "human") { player.Species = "Human"; nemesis1.Species = "Dwarf"; nemesis2.Species = "Peshmurga"; }
        if (player.Species == "peshmurga") { player.Species = "Peshmurga"; nemesis1.Species = "Human"; nemesis2.Species = "Dwarf"; }
        if (player.Species == "dwarf") { player.Species = "Dwarf"; nemesis1.Species = "Peshmurga"; nemesis2.Species = "Dwarf"; }

        Console.Write("Boon: ");
        string boon = Console.ReadLine().ToLower();
        if (boon == "ogre") { player.CON += 3; nemesis1.WIL += 3; nemesis2.POW += 3; }
        if (boon == "antlion") { player.POW += 3; nemesis1.CON += 3; nemesis2.WIL += 3; }
        else { player.WIL += 3; nemesis1.POW += 3; nemesis2.CON += 3; }

        InitializeGame();
    }

    static void Intro()
    {

        Console.WriteLine("Until indicated, you can just hit ENTER to progress.");
        Console.ReadLine();
        Console.Clear();
        Console.WriteLine();
        Console.WriteLine("First, you must understand where you are, and why you should care...");
        Console.ReadLine();
        Console.WriteLine("We are all connected. All living things have sway on others.");
        Console.WriteLine();
        Console.WriteLine("We were formed in the discordant harmony of the universe.");
        Console.ReadLine();
        Console.Clear();
        Console.WriteLine();
        Console.WriteLine("We are the ghosts of stars... observing the aftermath of the supernova.");
        Console.ReadLine();
        Console.WriteLine("We are born from the decay of beautiful things. And life consumes life for the same reason.");
        Console.ReadLine();
        Console.Clear();
        Console.WriteLine("Centuries ago, a consort of kings, empresses, and magicians turned their backs on this balance.");
        Console.WriteLine();
        Thread.Sleep(2000);
        Console.WriteLine("They refused to decay.");
        Console.ReadLine();
        Console.WriteLine("They had vision. Unity. Influence.");
        Console.WriteLine();
        Console.WriteLine("They amassed the wealth and resources of generations to build a grand pyramid.");
        Console.ReadLine();
        Console.Clear();
        Console.WriteLine("They intended to ascend to godhood together.");
        Console.ReadLine();
        Console.Clear();
        Console.WriteLine("It isn't known what happened at the apex of their ambitions.");
        Console.WriteLine();
        Console.WriteLine("Historians and theologists contend different hypotheses as to what occurred at the conclusion of their ritual...");
        Console.WriteLine();
        Console.WriteLine("They have only oral accounts from the traumatized few who survived. And even these don't quite align.");
        Console.ReadLine();
        Console.Clear();
        Console.WriteLine();
        Console.WriteLine("The earth itself roiled like an ocean in tempest.");
        Console.WriteLine();
        Console.WriteLine("The heavens roared as if a great beast had awakened. The air turned to fire for miles around.");
        Console.WriteLine();
        Console.WriteLine("The region, once verdant and rich with life, turned to ash and glass all at once.");
        Console.ReadLine();
        Console.WriteLine("Some say they had succeeded, and the display of raw, unbridled destruction was the death of the gods whose places they took.");
        Console.WriteLine();
        Console.WriteLine("Others say they destroyed themselves in their hubris. Or the gods themselves came to do it. A hopeful thought, for some.");
        Console.ReadLine();
        Console.Clear();
        Console.WriteLine("What is clear is how the monarchs and their magicians were never again seen since.");
        Console.WriteLine();
        Console.WriteLine("The pyramid had erupted into a rubbled husk of its former glory.");
        Console.WriteLine();
        Console.WriteLine("But their influence has been felt for centuries. The consequences carried far into the hearts of many.");
        Console.ReadLine();
        Console.Clear();
        Console.WriteLine("People have since come to rebuild the pyramid.");
        Console.WriteLine();
        Console.WriteLine("Lesser, weaker, pettier men and women have restored it, and their studies have been rewarded with oblivion each time.");
        Console.WriteLine();
        Console.WriteLine("The same sickness of spirit that draws them near... it makes them turn on one another in the end.");
        Console.WriteLine();
        Console.WriteLine("Always. Every time.");
        Console.ReadLine();
        Console.Clear();
        Console.WriteLine("And then there is you.");
        Console.ReadLine();
        Console.Clear();
        Console.WriteLine("You, too, defied the natural order of life.");
        Console.WriteLine();
        Console.WriteLine("You died trying to cling to immortality, and it has stained your very soul.");
        Console.ReadLine();
        Console.Clear();
        Console.WriteLine("Anahata, the Goddess of Water, Fertility, and Harmony, has denied you passage into the After-Sea.");
        Console.ReadLine();
        Console.Clear();
        Console.WriteLine("She will carve you from the clay, just as her First Ones were, to enact your penance and restore balance as her Champion.");
        Console.WriteLine();
        Console.WriteLine("Your soul will not know rest until she is satisfied.");
        Console.WriteLine();
        Console.WriteLine("The scars of your past actions will become clear to you in time.");
        Console.ReadLine();
        Console.Clear();

        // Initialize character creation
        StoryStart();
    }
    static void StoryStart()
    {
        Console.WriteLine("It's dark... peaceful. A distant rumble can be heard in the back of your mind.");
        Console.WriteLine();
        Console.WriteLine("A soft, powerful voice echoes into your consciousness...");
        Console.WriteLine();
        Console.WriteLine("'Arise from the clay, my child. I will it.'");
        Console.ReadLine();
        Console.Clear();
        Console.WriteLine("'I have done the work of carving your form. Now... you must form yourself.'");
        Console.WriteLine();
        Console.WriteLine("'Focus now. What is your name?' ");
        Console.WriteLine();
        Console.Write("You are... ");
        player.Name = Console.ReadLine();

        bool IsSatisfied = false;
        string? command;

        while (!IsSatisfied)
        {
            if (string.IsNullOrEmpty(player.Name))
            {
                Console.Clear();
                Console.WriteLine("'Shh. Slow down. Your name is the most important thing you possess.'");
                Console.WriteLine();
                Console.WriteLine("'Now... what is your name?'");
                player.Name = Console.ReadLine();
                Console.WriteLine();
                Console.WriteLine();
                continue;
            }

            Console.WriteLine($"'Your name is {player.Name}, is that right?' YES or NO");
            command = Console.ReadLine().ToLower();
            if (command == "yes") { IsSatisfied = true; }
            if (command == "no")
            {
                Console.WriteLine("'What is your name?'");
                player.Name = Console.ReadLine();
                
            }

        }

        Console.WriteLine();

        // Take whatever the player name is and use it to name their nemeses 
        nemesis1.Name = $"Ur'{player.Name}";
        nemesis2.Name = $"Da-{player.Name}";

        Console.Clear();
        Console.WriteLine();
        Console.WriteLine($"'You are a raindrop in a storm, {player.Name}. An eternity precedes you, and an eternity shall succeed you.'");
        Console.WriteLine();
        Console.WriteLine("'Three of my children still walk freely under the sun. You must decide which lineage is yours.'");
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine($"'Are you HUMAN, {player.Name}? The most mischievous of my children, forever bent unto the unknown?'");
        Console.WriteLine();
        Console.WriteLine("'Are you a DWARF? Descended from the First of the First Ones who made the world?'");
        Console.WriteLine();
        Console.WriteLine("'Or are you PESHMURGA? Molded from ash, ushered into a world aflame?'");
        Console.WriteLine();

        command = Console.ReadLine().ToLower();
        while (command != "human" && command != "dwarf" && command != "peshmurga")
        {
            Console.WriteLine("'The After-Sea does not hold this soul. Your form obeys your intent. Focus.'");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Human, dwarf, or peshmurga?");
            command = Console.ReadLine().ToLower();
            player.Species = command;
        }


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
        Console.WriteLine("You realize you are facedown. Buried... but only barely.");
        Console.WriteLine();
        Console.WriteLine("No sooner do you envision your arms to push yourself to your knees than you feel how they mold themselves.");
        Console.WriteLine();
        Console.WriteLine("You... are made of clay? You can see your body is not flesh.");
        Console.ReadLine();
        Console.Clear();
        Console.WriteLine("You open your mouth to gasp, but there is no mouth yet.");
        Console.WriteLine();
        Console.WriteLine("Your face splits and contorts into pained existence. There is no voice yet.");
        Console.ReadLine();
        Console.Clear();
        Console.WriteLine("Anahata's soft voice, less stern now, easily cuts through the psychic turmoil of your creation.");
        Console.WriteLine();
        Console.WriteLine("'Shhh... you are becoming. Focus on my voice, my child.'");
        Console.WriteLine();
        Console.WriteLine("'You are almost complete... you need only to accept the spark of life.'");
        Console.WriteLine();

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
                nemesis1.CON += 3;
                nemesis2.WIL += 3;
                Console.WriteLine("Your physique is molded further... your muscles ripple. The ferocity of a predator rumbles within you.");
                break;
            case "peacock":
                player.WIL += 3;
                nemesis1.POW += 3;
                nemesis2.CON += 3;
                Console.WriteLine("Your mind feels sharper... your skull pulses. The song of creation rings in your consciousness.");
                break;
            case "ogre":
                player.CON += 3;
                nemesis1.WIL += 3;
                nemesis2.POW += 3;
                Console.WriteLine("Your skin thickens... your heartbeat is louder. You are indomitable.");
                break;
        }
        Console.ReadLine();
        Console.Clear();
        Console.WriteLine();
        Console.WriteLine("You must make a final choice. Do you defend yourself with brawn or arcana?");
        Console.WriteLine();
        Console.WriteLine("One who manipulates reality with their force of will and incantation is known as a BRUJADHA.");
        Console.WriteLine("If there were such a thing as fate, the brujadha would cut through it like water.");
        Console.WriteLine("Magic is the foundation of their combat style.");
        Console.WriteLine();
        Console.WriteLine("The FANNAAN is peerless in a duel. Their grasp of martial tactics and visceral grit makes them deadly.");
        Console.WriteLine("When the worst weapons are wrought upon the world, the fannaan survives to recount how it happened.");
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine("Are you versed in the ways of the BRUJADHA or the FANNAAN? Choosing one does not preclude you from the tools of the other. But you must specialize in one.");
        Console.WriteLine();

        command = Console.ReadLine().ToLower();

        while (command != "brujadha" && command != "fannaan")
        {
            Console.WriteLine("'You must pick one or the other.'");
            command = Console.ReadLine().ToLower();
        }

        // Depending on which combat class the player takes, the nemeses will adopt their own
        if (command == "brujadha")
        {
            Console.Clear();
            Console.WriteLine("To be a brujadha... The fabric of the universe feels pliable under your fingertips.");
            Console.WriteLine();
            Console.WriteLine("You need only to take it and pull.");
            Console.WriteLine();
            Brujadha player = new Brujadha(Program.player);
            Brujadha nemesis1 = new Brujadha(Program.nemesis1);
            Fannaan nemesis2 = new Fannaan(Program.nemesis2);
        }

        if (command == "fannaan")
        {
            Console.WriteLine("");
            Console.WriteLine();
            Fannaan player = new Fannaan(Program.player);
            Fannaan nemesis1 = new Fannaan(Program.nemesis1);
            Brujadha nemesis2 = new Brujadha(Program.nemesis2);
        }

        Console.WriteLine("As this happens, your face splits open at last. You scream.");
        Console.ReadLine();
        Console.Clear();
        Console.WriteLine("");
        Console.WriteLine("");
        Console.WriteLine("");
        Console.WriteLine("At last... you can scream.");
        Console.ReadLine();
        Console.Clear();

        InitializeGame();
    }

    static void InitializeGame()
    {
        // Instantiate the starting room: The Clay Pits
        // The player can LOOK and EXAMINE things

        Room clayPits = new Room("R001",
                                "Clay Pits",
                                "The sounds of rushing water echoes through this room from an unseen source.\nThe sun struggles to reach the bottom of this pit, whose lip is many meters below the surface.\nIf there were ever ladders or steps built into this place, there's no sign of them.\nBroken wood and cloth scraps are barely visible in the slick muck. This place looks abandoned. "
                                );


        Room coldTunnel = new Room("R002",
                                "Cold Tunnel",
                                "The sound of rushing water is louder here, and the air is frigid.\nThe only sources of light are far behind and far ahead. You can barely see in here. ",
                                "Dozens of faces and limbs silently roil in the walls, as if trying to break through. " +
                                "You can't help but wonder if this had been you only moments before."
                                );


        Room shallowPit = new Room("R003",
                                "Shallow Pit",
                                "Roaring water threatens to deafen you here.\nYou can see vines and roots snaking down into this pit, just barely out of reach."
                                );


        clayPits.AddExit("north", coldTunnel);
        coldTunnel.AddExit("south", clayPits);

        coldTunnel.AddExit("north", shallowPit);
        shallowPit.AddExit("south", coldTunnel);

        player.CurrentRoom = clayPits;
        clayPits.AddPerson(player); // Add player to the People list in clayPits
        Weapon hammer1 = new Hammer();
        clayPits.AddItem(hammer1);

        HealingFixture clayPool = new HealingFixture("clay pool", "This murky water is freezing, but there is something undeniably inviting about it.\nYou sense that you can USE this.");
        clayPool.UseText = "You submerge your hands into the water... and you see the cuts and cracks in your form repairing. You quickly splash this water on the rest of your body.";
        clayPool.EffectText = "All of your pain has completely vanished!";
        clayPits.AddItem(clayPool);

        player.SpawnRoom = clayPits;
        player.IsPlayer = true; // Ensure the Respawn method will work ONLY for the player

        Weapon pickaxe1 = new Pickaxe();
        nemesis1.EquippedWeapon = pickaxe1;

        nemesis1.CurrentRoom = coldTunnel;
        coldTunnel.AddPerson(nemesis1); // Add to the People list in coldTunnel
        nemesis1.Description = "It's... you? But the proportions are all grotesquely deformed. The mouth doesn't open, and 'your' face is uneven. ";
        nemesis1.IsPlayer = false;

        nemesis2.CurrentRoom = shallowPit;
        shallowPit.AddPerson(nemesis2); // Add to the People list in shallowPit
        nemesis2.Description = "It's... you? But the proportions are all grotesquely deformed. The mouth doesn't open, and 'your' face is uneven. ";
        nemesis2.IsPlayer = false;

        // Prompt the player to move around
        Console.WriteLine("It takes you a few moments to finish purging the mud from your belly.");
        Console.WriteLine();
        Console.WriteLine("Coughing, retching, shivering, you shakily get to your feet.");
        Console.WriteLine("");
        Console.WriteLine("You wipe the last of the muck from your hands and face. You can now open your eyes and LOOK around.");

        StartGame();
        // The guided narrative portion is now over. The player can act freely and interact with the world


    }

    static void StartGame()
    {

        while (IsPlaying)
        {
            inputController.GetCommand();
        }

        Console.WriteLine("Thank you for playing my demo!");
        Console.ReadLine();
    }

    public static void EndGame()
    {
        IsPlaying = false;
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