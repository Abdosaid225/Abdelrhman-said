namespace FuzzyAdventure.Core;

/// <summary>
/// Main game engine that orchestrates game state, player actions, and room navigation.
/// </summary>
public class GameEngine
{
    private readonly Player _player;
    private readonly Parser _parser;
    private bool _isRunning;

    public GameEngine(Player player)
    {
        _player = player ?? throw new ArgumentNullException(nameof(player));
        _parser = new Parser();
        _isRunning = false;
    }

    public void Start()
    {
        _isRunning = true;
        Console.WriteLine("=== Fuzzy Adventure ===\n");
        ShowCurrentRoomDescription();
        GameLoop();
    }

    private void GameLoop()
    {
        while (_isRunning)
        {
            Console.Write("\n> ");
            var input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
                continue;

            try
            {
                var command = _parser.Parse(input);
                ExecuteCommand(command);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }

    private void ExecuteCommand(Command command)
    {
        switch (command.Verb.ToLowerInvariant())
        {
            case "look":
                ShowCurrentRoomDescription();
                break;
            case "go":
                HandleMove(command.Object);
                break;
            case "take":
                HandleTakeItem(command.Object);
                break;
            case "drop":
                HandleDropItem(command.Object);
                break;
            case "inventory":
                ShowInventory();
                break;
            case "examine":
                HandleExamine(command.Object);
                break;
            case "help":
                ShowHelp();
                break;
            case "quit":
                _isRunning = false;
                Console.WriteLine("Thanks for playing!");
                break;
            default:
                Console.WriteLine("I don't understand that command.");
                break;
        }
    }

    private void ShowCurrentRoomDescription()
    {
        var room = _player.CurrentRoom;
        Console.WriteLine($"\n[{room.Name}]");
        Console.WriteLine(room.Description);

        if (room.Items.Count > 0)
        {
            Console.WriteLine("Items here: " + string.Join(", ", room.Items.Select(i => i.Name)));
        }

        if (room.Exits.Count > 0)
        {
            Console.WriteLine("Exits: " + string.Join(", ", room.Exits.Keys));
        }
    }

    private void HandleMove(string? direction)
    {
        if (string.IsNullOrWhiteSpace(direction))
        {
            Console.WriteLine("Go where? (north, south, east, west)");
            return;
        }

        if (_player.Move(direction))
        {
            Console.WriteLine($"You go {direction}.");
            ShowCurrentRoomDescription();
        }
        else
        {
            Console.WriteLine("You can't go that way.");
        }
    }

    private void HandleTakeItem(string? itemName)
    {
        if (string.IsNullOrWhiteSpace(itemName))
        {
            Console.WriteLine("Take what?");
            return;
        }

        var item = _player.CurrentRoom.FindItem(itemName);
        if (item != null)
        {
            _player.PickUpItem(item);
            Console.WriteLine($"You took the {item.Name}.");
        }
        else
        {
            Console.WriteLine($"There's no {itemName} here.");
        }
    }

    private void HandleDropItem(string? itemName)
    {
        if (string.IsNullOrWhiteSpace(itemName))
        {
            Console.WriteLine("Drop what?");
            return;
        }

        var item = _player.Inventory.FindItem(itemName);
        if (item != null)
        {
            _player.DropItem(item);
            Console.WriteLine($"You dropped the {item.Name}.");
        }
        else
        {
            Console.WriteLine($"You don't have {itemName}.");
        }
    }

    private void ShowInventory()
    {
        if (_player.Inventory.Items.Count == 0)
        {
            Console.WriteLine("Your inventory is empty.");
        }
        else
        {
            Console.WriteLine("You are carrying:");
            foreach (var item in _player.Inventory.Items)
            {
                Console.WriteLine($"  - {item.Name}");
            }
        }
    }

    private void HandleExamine(string? itemName)
    {
        if (string.IsNullOrWhiteSpace(itemName))
        {
            Console.WriteLine("Examine what?");
            return;
        }

        var item = _player.CurrentRoom.FindItem(itemName) ?? _player.Inventory.FindItem(itemName);
        if (item != null)
        {
            Console.WriteLine($"{item.Name}: {item.Description}");
        }
        else
        {
            Console.WriteLine($"You don't see {itemName}.");
        }
    }

    private void ShowHelp()
    {
        Console.WriteLine("Commands:");
        Console.WriteLine("  look         - Examine the current room");
        Console.WriteLine("  go <dir>     - Move in a direction (north, south, east, west)");
        Console.WriteLine("  take <item>  - Pick up an item");
        Console.WriteLine("  drop <item>  - Drop an item from inventory");
        Console.WriteLine("  inventory    - View your inventory");
        Console.WriteLine("  examine      - Examine an item more closely");
        Console.WriteLine("  help         - Show this help message");
        Console.WriteLine("  quit         - Exit the game");
    }
}
