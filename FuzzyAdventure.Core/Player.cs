namespace FuzzyAdventure.Core;

/// <summary>
/// Represents the player in the game world.
/// </summary>
public class Player
{
    public string Name { get; set; }
    public Room CurrentRoom { get; set; }
    public Inventory Inventory { get; set; }

    public Player(string name, Room startingRoom)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        CurrentRoom = startingRoom ?? throw new ArgumentNullException(nameof(startingRoom));
        Inventory = new Inventory();
    }

    public bool Move(string direction)
    {
        var nextRoom = CurrentRoom.GetExit(direction);
        if (nextRoom == null)
            return false;

        CurrentRoom = nextRoom;
        return true;
    }

    public void PickUpItem(Item item)
    {
        var roomItem = CurrentRoom.RemoveItem(item);
        if (roomItem != null)
        {
            Inventory.AddItem(roomItem);
        }
    }

    public void DropItem(Item item)
    {
        if (Inventory.RemoveItem(item))
        {
            CurrentRoom.AddItem(item);
        }
    }
}
