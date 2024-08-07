using System;


namespace Antikythera;

// Use this workspace to create a 3-room area
public class Room
{
	string RoomName = "";

	// The RoomID will be 
	string RoomID = "";

	string Description = "";

	string? UnseenDescription = "";

	string? HiddenDescription = "";

	List<string> Exits = new List<string>();

	public Room()
	{
		// Constructor here

	}
}
