using System.Collections.Generic;
using System.Linq;
using escape_house.Interfaces;
using escape_house.Models;

namespace escape_house.Services
{
  class GameService : IGameService
  {
    public List<string> Messages { get; set; }
    private IGame _game { get; set; }

    public GameService(string playerName)
    {
      Messages = new List<string>();
      _game = new Game();
      _game.CurrentPlayer = new Player(playerName);
      Look();
    }

    public bool Go(string direction)
    {
      //if the current room has that direction on the exits dictionary
      if (_game.CurrentRoom.Exits.ContainsKey(direction))
      {
        // set current room to the exit room
        _game.CurrentRoom = _game.CurrentRoom.Exits[direction];
        // populate messages with room description
        Messages.Add($"You Travel {direction}, and discover: ");
        Look();
        EndRoom end = _game.CurrentRoom as EndRoom;
        if (end != null)
        {
          Messages.Add(end.Narrative);
          return false;
        }
        return true;
      }
      //no exit in that direction
      Messages.Add("No Room in that direction");
      Look();
      return true;
    }

    public void Help()
    {
      Messages.Add("Type (look) to see around the room, type (quit) to quit, type (take) to take an item, type (go + direction) to go a certain direction, type (use) to use an item, type (inventory) to see your inventory, type (reset) to reset game.");
    }

    public void Inventory()
    {
      Messages.Add("Current Inventory: ");
      foreach (var item in _game.CurrentPlayer.Inventory)
      {
        Messages.Add($"{item.Name} - {item.Description}");
      }
    }

    public void Look()
    {
      Messages.Add(_game.CurrentRoom.Name);
      Messages.Add(_game.CurrentRoom.Description);
      if (_game.CurrentRoom.Items.Count > 0)
      {
        Messages.Add("In this room there is a ");
        foreach (var item in _game.CurrentRoom.Items)
        {
          Messages.Add(item.Name + ", " + item.Description);
        }
      }
      string exits = string.Join(", ", _game.CurrentRoom.Exits.Keys);
      if (exits.Length > 0)
      {
        Messages.Add("There are exits to the " + exits + ".");
      }
      string lockedExits = "";
      foreach (var lockedRoom in _game.CurrentRoom.LockedExits.Values)
      {
        lockedExits += lockedRoom.Key;
      }
      if (lockedExits == "east")
      {
        Messages.Add("There is a rabid dog to the " + lockedExits + ".");
      }
      if (lockedExits == "south")
      {
        Messages.Add("The garage door to the " + lockedExits + " is locked.");
      }
    }

    public void Reset()
    {
      string name = _game.CurrentPlayer.Name;
      _game = new Game();
      _game.CurrentPlayer = new Player(name);
    }

    public void Take(string itemName)
    {
      IItem found = _game.CurrentRoom.Items.Find(i => i.Name.ToLower() == itemName);
      if (found != null)
      {
        _game.CurrentPlayer.Inventory.Add(found);
        _game.CurrentRoom.Items.Remove(found);
        Messages.Add($"You have taken the {itemName}");
        return;
      }
      Messages.Add("Cannot find item by that name");
    }

    public void Use(string itemName)
    {
      var found = _game.CurrentPlayer.Inventory.Find(i => i.Name.ToLower() == itemName);
      if (found != null)
      {
        Messages.Add(_game.CurrentRoom.Use(found));
        return;
      }
      // check if item is in room
      Messages.Add("You don't have that Item");
    }



  }
}