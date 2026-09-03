using FuzzyAdventure.Core;

var mainHall = new Room("Main Hall", "You're in a grand main hall with high ceilings. Sunlight streams through tall windows.");
var library = new Room("Library", "Shelves of books line every wall. The smell of old paper fills the air.");
var garden = new Room("Garden", "A peaceful garden with colorful flowers and a small fountain.");
var kitchen = new Room("Kitchen", "A warm kitchen with the aroma of fresh bread.");

mainHall.AddExit("north", library);
mainHall.AddExit("east", garden);
mainHall.AddExit("south", kitchen);

library.AddExit("south", mainHall);
garden.AddExit("west", mainHall);
kitchen.AddExit("north", mainHall);

mainHall.AddItem(new Item("key", "A brass key with intricate designs.", true));
library.AddItem(new Item("book", "An ancient tome about adventure and mystery.", true));
garden.AddItem(new Item("rose", "A beautiful red rose.", true));
kitchen.AddItem(new Item("bread", "Fresh, warm bread.", true));

var player = new Player("Adventurer", mainHall);
var game = new GameEngine(player);

game.Start();
