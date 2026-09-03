namespace FuzzyAdventure.Core;

/// <summary>
/// Represents a room/location in the game world.
/// </summary>
public class Room
{
    public string Name { get; set; }
    public string Description { get; set; }
    private readonly List<Item> _items = [];
    private readonly Dictionary<string, Room> _exits = [];

    public IReadOnlyList<Item> Items => _items.AsReadOnly();
    public IReadOnlyDictionary<string, Room> Exits => _exits.AsReadOnly();

    public Room(string name, string description)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Description = description ?? throw new ArgumentNullException(nameof(description));
    }

    public void AddItem(Item item)
    {
        if (item != null)
            _items.Add(item);
    }

    public Item? RemoveItem(Item item)
    {
        if (_items.Remove(item))
            return item;
        return null;
    }

    public Item? FindItem(string name)
    {
        return _items.FirstOrDefault(i => i.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
    }

    public void AddExit(string direction, Room room)
    {
        if (string.IsNullOrWhiteSpace(direction))
            throw new ArgumentException("Direction cannot be empty.", nameof(direction));
        if (room == null)
            throw new ArgumentNullException(nameof(room));

        _exits[direction.ToLowerInvariant()] = room;
    }

    public Room? GetExit(string direction)
    {
        if (string.IsNullOrWhiteSpace(direction))
            return null;

        _exits.TryGetValue(direction.ToLowerInvariant(), out var room);
        return room;
    }
}
