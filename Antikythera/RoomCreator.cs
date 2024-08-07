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

	Dictionary<string, Room> Exits = new Dictionary<string, Room>();

	List<Item> Objects = new List<Item>();

	// Lists what people and enemies are present in the room
	List<Character> People = new List<Character>();


	public Room(string roomID, string roomName, string description)
	{
        // Constructor here
        RoomID = roomID;
        RoomName = roomName;
        Description = description;
        Exits = new Dictionary<string, Room>();
        Objects = new List<Item>();
        People = new List<Character>();
    }

	public void AddExit(string direction, Room room)
	{
		// Add a direction you can leave the room into an existing room
		Exits[direction] = room;
	}

	// Starting room
	Room clayPits = new Room(roomID: "R001",
							roomName: "Clay Pits",
							description: "The sounds of rushing water echoes through this room from an unseen source. " +
										 "The sun struggles to reach the bottom of this pit, whose lip is many meters below the surface. " +
										 "If there were ever ladders or steps here, there's no sign they were built here. " +
										 "Broken wood and cloth scraps are barely visible in the slick muck. This place looks abandoned. "
							); 
}
