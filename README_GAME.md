# Fuzzy Adventure - C# OOP Console Game

A text-based adventure game built with **C# and .NET 8** to demonstrate core Object-Oriented Programming (OOP) principles and best practices.

## Features

✨ **Interactive Text Adventure** - Explore rooms, collect items, and interact with the game world
🎯 **Fuzzy Command Matching** - Commands are intelligently matched using fuzzy string comparison (forgiving typos)
🏗️ **Clean OOP Architecture** - Demonstrates encapsulation, inheritance-ready design, composition, and single responsibility
✅ **Comprehensive Unit Tests** - 26 xUnit tests covering core functionality
📚 **Learning Resource** - Well-documented code with clear OOP patterns

## Project Structure

```
FuzzyAdventure/
├── FuzzyAdventure.sln
├── FuzzyAdventure.Core/
│   ├── Item.cs              # Represents an item in the game world
│   ├── Room.cs              # Represents a location/room
│   ├── Inventory.cs         # Manages player's items
│   ├── Player.cs            # Represents the player character
│   ├── Command.cs           # Represents a parsed player command
│   ├── Parser.cs            # Parses player input with fuzzy matching
│   ├── GameEngine.cs        # Main game orchestrator and loop
│   ├── Program.cs           # Entry point with sample game setup
│   └── FuzzyAdventure.Core.csproj
│
├── FuzzyAdventure.Tests/
│   ├── GameTests.cs         # Unit tests for all core classes
│   └── FuzzyAdventure.Tests.csproj
│
└── README.md
```

## Core OOP Concepts Demonstrated

### 1. **Encapsulation**
- Private fields with public properties: `Item`, `Room`, `Inventory`
- Controlled access to internal state through methods
- Example: `Room.Items` is read-only; items are managed via `AddItem()` and `RemoveItem()`

### 2. **Composition**
- `Player` *has-a* `Inventory` (not inherits)
- `Room` *contains* `Item` objects
- `GameEngine` *uses* `Player`, `Parser`, and `Room` objects
- Demonstrates "favor composition over inheritance"

### 3. **Single Responsibility Principle (SRP)**
- **Item**: Represents an item
- **Room**: Manages a location and connections
- **Inventory**: Manages item collection
- **Player**: Represents the player character
- **Parser**: Handles input parsing
- **GameEngine**: Orchestrates game logic and display

### 4. **Dependency Injection**
- Classes receive dependencies in constructors
- Example: `Player(string name, Room startingRoom)`
- Example: `GameEngine(Player player)`

### 5. **Null Safety & Validation**
- `ArgumentNullException` thrown for invalid inputs
- Null-coalescing and null-conditional operators

### 6. **Read-Only Collections**
- `IReadOnlyList<Item>` and `IReadOnlyDictionary<string, Room>` prevent external modification
- Internal `List<T>` for management

## Building the Project

### Prerequisites
- .NET 8 SDK or later

### Build
```bash
dotnet build
```

### Run Tests
```bash
dotnet test
```

### Run the Game
```bash
dotnet run --project FuzzyAdventure.Core
```

## Game Commands

| Command | Usage | Example |
|---------|-------|---------|
| `look` | Examine the current room | `look` |
| `go` | Move in a direction | `go north` |
| `take` | Pick up an item | `take key` |
| `drop` | Drop an item from inventory | `drop key` |
| `inventory` | View your inventory | `inventory` |
| `examine` | Examine an item closely | `examine key` |
| `help` | Show command help | `help` |
| `quit` | Exit the game | `quit` |

### Fuzzy Matching in Action
- **Typo forgiveness**: `loak` → `look`, `tke` → `take`
- **Smart matching**: Uses FuzzySharp library for Levenshtein distance
- **Threshold**: Commands must be ≥70% similar to be recognized

## Unit Tests

Run all tests:
```bash
dotnet test
```

Test coverage includes:
- **ItemTests**: Creation, pickup permissions, string conversion
- **InventoryTests**: Adding/removing items, capacity limits, finding items
- **RoomTests**: Room setup, item management, exit connections
- **PlayerTests**: Movement, item pickup/drop
- **ParserTests**: Command parsing, fuzzy matching

### Example Test
```csharp
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
```

## Learning Paths

### 👶 Beginner
1. Understand `Item` and `Room` classes
2. Study `Inventory` capacity and management
3. Trace `Player.Move()` and `Player.PickUpItem()`

### 🎓 Intermediate
1. Review `Parser` and fuzzy matching logic
2. Understand `GameEngine` command dispatching
3. Add new commands in `ExecuteCommand()`

### 🚀 Advanced
1. Implement save/load system
2. Add NPC characters (extend from base class)
3. Create event system for game actions
4. Add puzzle/quest system

## Dependencies

- **FuzzySharp 2.0.2** - Fuzzy string matching for forgiving command input
- **xUnit** - Unit testing framework

## Future Enhancements

- [ ] Save/load game state (serialization)
- [ ] NPC characters with dialogue
- [ ] Quest/puzzle system
- [ ] Combat mechanics
- [ ] Multi-threaded async operations
- [ ] Configuration files for world setup
- [ ] Web-based UI (Blazor)

## Design Patterns Used

1. **Command Pattern**: `Command` class encapsulates requests
2. **Interpreter Pattern**: `Parser` interprets user input
3. **Template Method**: `GameLoop()` defines the skeleton
4. **Dependency Injection**: Constructor-based DI

## OOP Best Practices Employed

✅ Classes have single, clear responsibilities
✅ Immutable/read-only collections exposed to clients
✅ Null validation on constructor parameters
✅ Meaningful exception messages
✅ XML documentation comments
✅ Consistent naming conventions (PascalCase for public)
✅ Private fields with public properties
✅ No deep inheritance hierarchies

## Example: Extending with NPCs

```csharp
public class Character
{
    public string Name { get; set; }
    public Room CurrentRoom { get; set; }
    
    public Character(string name, Room room)
    {
        Name = name;
        CurrentRoom = room;
    }
}

public class Player : Character
{
    public Inventory Inventory { get; set; }
    // Player-specific logic
}

public class NPC : Character
{
    public string Dialogue { get; set; }
    // NPC-specific logic
}
```

## Contributing

Feel free to fork this project and enhance it! Great areas:
- Adding more game features
- Improving test coverage
- Refactoring for better patterns
- Adding documentation

## License

This project is open for educational use.

---

**Happy coding!** 🎮 Use this project to practice C# and OOP principles.
