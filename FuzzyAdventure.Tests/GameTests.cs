namespace FuzzyAdventure.Tests;

using FuzzyAdventure.Core;
using Xunit;

public class ItemTests
{
    [Fact]
    public void Item_ShouldBeCreatedWithNameAndDescription()
    {
        var item = new Item("sword", "A sharp sword", true);

        Assert.Equal("sword", item.Name);
        Assert.Equal("A sharp sword", item.Description);
        Assert.True(item.CanPickUp);
    }

    [Fact]
    public void Item_CannotBePickUpIfNotAllowed()
    {
        var item = new Item("wall", "A solid wall", false);

        Assert.False(item.CanPickUp);
    }

    [Fact]
    public void Item_ToString_ReturnsName()
    {
        var item = new Item("key", "A small key");

        Assert.Equal("key", item.ToString());
    }

    [Fact]
    public void Item_ThrowsExceptionIfNameIsNull()
    {
        Assert.Throws<ArgumentNullException>(() => new Item(null!, "desc"));
    }
}

public class InventoryTests
{
    [Fact]
    public void Inventory_CanAddItem()
    {
        var inventory = new Inventory();
        var item = new Item("sword", "A sword");

        var added = inventory.AddItem(item);

        Assert.True(added);
        Assert.Single(inventory.Items);
    }

    [Fact]
    public void Inventory_CannotAddNonPickupItem()
    {
        var inventory = new Inventory();
        var item = new Item("wall", "A wall", false);

        var added = inventory.AddItem(item);

        Assert.False(added);
        Assert.Empty(inventory.Items);
    }

    [Fact]
    public void Inventory_CannotExceedCapacity()
    {
        var inventory = new Inventory { MaxCapacity = 2 };
        var item1 = new Item("item1", "Item 1");
        var item2 = new Item("item2", "Item 2");
        var item3 = new Item("item3", "Item 3");

        inventory.AddItem(item1);
        inventory.AddItem(item2);
        var added = inventory.AddItem(item3);

        Assert.False(added);
        Assert.Equal(2, inventory.Items.Count);
    }

    [Fact]
    public void Inventory_CanRemoveItem()
    {
        var inventory = new Inventory();
        var item = new Item("sword", "A sword");
        inventory.AddItem(item);

        var removed = inventory.RemoveItem(item);

        Assert.True(removed);
        Assert.Empty(inventory.Items);
    }

    [Fact]
    public void Inventory_CanFindItemByName()
    {
        var inventory = new Inventory();
        var item = new Item("sword", "A sword");
        inventory.AddItem(item);

        var found = inventory.FindItem("sword");

        Assert.NotNull(found);
        Assert.Equal(item, found);
    }

    [Fact]
    public void Inventory_FindItem_IsCaseInsensitive()
    {
        var inventory = new Inventory();
        var item = new Item("Sword", "A sword");
        inventory.AddItem(item);

        var found = inventory.FindItem("sword");

        Assert.NotNull(found);
    }
}

public class RoomTests
{
    [Fact]
    public void Room_CanBeCreated()
    {
        var room = new Room("Throne Room", "A grand room with a throne");

        Assert.Equal("Throne Room", room.Name);
        Assert.Equal("A grand room with a throne", room.Description);
    }

    [Fact]
    public void Room_CanAddItem()
    {
        var room = new Room("Hall", "A hall");
        var item = new Item("key", "A key");

        room.AddItem(item);

        Assert.Single(room.Items);
    }

    [Fact]
    public void Room_CanRemoveItem()
    {
        var room = new Room("Hall", "A hall");
        var item = new Item("key", "A key");
        room.AddItem(item);

        var removed = room.RemoveItem(item);

        Assert.NotNull(removed);
        Assert.Empty(room.Items);
    }

    [Fact]
    public void Room_CanFindItemByName()
    {
        var room = new Room("Hall", "A hall");
        var item = new Item("key", "A key");
        room.AddItem(item);

        var found = room.FindItem("key");

        Assert.NotNull(found);
    }

    [Fact]
    public void Room_CanAddExit()
    {
        var room1 = new Room("Hall", "A hall");
        var room2 = new Room("Library", "A library");

        room1.AddExit("north", room2);

        Assert.NotEmpty(room1.Exits);
        Assert.Equal(room2, room1.GetExit("north"));
    }

    [Fact]
    public void Room_Exit_IsCaseInsensitive()
    {
        var room1 = new Room("Hall", "A hall");
        var room2 = new Room("Library", "A library");

        room1.AddExit("North", room2);

        Assert.Equal(room2, room1.GetExit("north"));
    }
}

public class PlayerTests
{
    [Fact]
    public void Player_CanBeCreated()
    {
        var room = new Room("Hall", "A hall");
        var player = new Player("Hero", room);

        Assert.Equal("Hero", player.Name);
        Assert.Equal(room, player.CurrentRoom);
        Assert.NotNull(player.Inventory);
    }

    [Fact]
    public void Player_CanMove()
    {
        var room1 = new Room("Hall", "A hall");
        var room2 = new Room("Library", "A library");
        room1.AddExit("north", room2);

        var player = new Player("Hero", room1);
        var moved = player.Move("north");

        Assert.True(moved);
        Assert.Equal(room2, player.CurrentRoom);
    }

    [Fact]
    public void Player_CannotMoveInvalidDirection()
    {
        var room = new Room("Hall", "A hall");
        var player = new Player("Hero", room);

        var moved = player.Move("north");

        Assert.False(moved);
        Assert.Equal(room, player.CurrentRoom);
    }

    [Fact]
    public void Player_CanPickUpItem()
    {
        var room = new Room("Hall", "A hall");
        var item = new Item("key", "A key");
        room.AddItem(item);

        var player = new Player("Hero", room);
        player.PickUpItem(item);

        Assert.Single(player.Inventory.Items);
        Assert.Empty(room.Items);
    }

    [Fact]
    public void Player_CanDropItem()
    {
        var room = new Room("Hall", "A hall");
        var item = new Item("key", "A key");
        var player = new Player("Hero", room);
        player.Inventory.AddItem(item);

        player.DropItem(item);

        Assert.Single(room.Items);
        Assert.Empty(player.Inventory.Items);
    }
}

public class ParserTests
{
    [Fact]
    public void Parser_ParsesValidCommand()
    {
        var parser = new Parser();

        var command = parser.Parse("look");

        Assert.Equal("look", command.Verb);
        Assert.Null(command.Object);
    }

    [Fact]
    public void Parser_ParsesCommandWithObject()
    {
        var parser = new Parser();

        var command = parser.Parse("take sword");

        Assert.Equal("take", command.Verb);
        Assert.Equal("sword", command.Object);
    }

    [Fact]
    public void Parser_FuzzyMatchesVerb()
    {
        var parser = new Parser();

        var command = parser.Parse("loak");

        Assert.Equal("look", command.Verb);
    }

    [Fact]
    public void Parser_ThrowsOnEmptyInput()
    {
        var parser = new Parser();

        Assert.Throws<ArgumentException>(() => parser.Parse(""));
    }
}
