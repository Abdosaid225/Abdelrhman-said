namespace FuzzyAdventure.Core;

/// <summary>
/// Represents an item that can be picked up, used, or examined in the game.
/// </summary>
public class Item
{
    public string Name { get; set; }
    public string Description { get; set; }
    public bool CanPickUp { get; set; }

    public Item(string name, string description, bool canPickUp = true)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Description = description ?? throw new ArgumentNullException(nameof(description));
        CanPickUp = canPickUp;
    }

    public override string ToString() => Name;
}
