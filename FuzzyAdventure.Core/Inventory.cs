namespace FuzzyAdventure.Core;

/// <summary>
/// Manages the player's inventory of items.
/// </summary>
public class Inventory
{
    private readonly List<Item> _items = [];
    public IReadOnlyList<Item> Items => _items.AsReadOnly();
    public int MaxCapacity { get; set; } = 10;

    public bool AddItem(Item item)
    {
        if (item == null) throw new ArgumentNullException(nameof(item));
        if (!item.CanPickUp) return false;
        if (_items.Count >= MaxCapacity) return false;

        _items.Add(item);
        return true;
    }

    public bool RemoveItem(Item item)
    {
        return _items.Remove(item);
    }

    public Item? FindItem(string name)
    {
        return _items.FirstOrDefault(i => i.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
    }

    public void Clear()
    {
        _items.Clear();
    }
}
