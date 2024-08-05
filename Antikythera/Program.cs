// Antikythera

// This program should do the following:
// 1. Initialize the Game and prevent crashes from invalid input
// 2. With minimal prompting, create a Character with a Class and a Starting Weapon (or Starting Spell).
// 3. With minimal prompting, create an Enemy to fight
// 4. Make Character and Enemy fight (or Character and Character) until one of them generates a "### has died!" message.
// 5. Climb the roots to end the demo

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