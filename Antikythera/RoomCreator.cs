using System;


namespace Antikythera;

// Use this workspace to create a 3-room area
public class Room
{
	public string RoomName = "";

	// The RoomID will be the foundation for transitioning to a new room via Exits
	public string RoomID = "";

	public string Description = "";
	public string? UnseenDescription = "";
	public string? HiddenDescription = "";

	public Dictionary<string, Room> Exits = new Dictionary<string, Room>();

    // Lists all items present in the room
    public List<Item> Objects = new List<Item>();

    // Lists what people and enemies are present in the room
    public List<Character> People = new List<Character>();


	public Room(string roomID, string roomName, string description)
	{
        // Constructor with bare minimum parameters
        RoomID = roomID;
        RoomName = roomName;
        Description = description;
        Exits = new Dictionary<string, Room>();
        Objects = new List<Item>();
        People = new List<Character>();
    }

    public Room(string roomID, string roomName, string description, string unseenDescription)
    {
        // Base constructor with unseen parameter
        RoomID = roomID;
        RoomName = roomName;
        Description = description;
		UnseenDescription = unseenDescription;
        Exits = new Dictionary<string, Room>();
        Objects = new List<Item>();
        People = new List<Character>();
    }

    public void AddItem(Item item) // The object is either created or dropped into the room
    { Objects.Add(item); }

    public void RemoveItem(Item item) // The object is no longer in the room
    { Objects.Remove(item); }

    public void AddPerson(Character character) // Someone has arrived or spawned
    { People.Add(character); }

    public void RemovePerson(Character character) // Someone dies or leaves the room
    { People.Remove(character); }

	public void AddExit(string direction, Room room) // Add a direction you can leave
    { Exits[direction] = room; }

	 
}
