using System.Collections.Generic;
using escape_house.Interfaces;

namespace escape_house.Models
{
  class Room : IRoom
  {
    public string Name { get; set; }
    public string Description { get; set; }
    public List<IItem> Items { get; set; }
    public Dictionary<string, IRoom> Exits { get; set; }
    public Dictionary<IItem, KeyValuePair<string, IRoom>> LockedExits { get; set; }

    public void AddLockedRoom(IItem key, string direction, IRoom room)
    {
      var lockedRoom = new KeyValuePair<string, IRoom>(direction, room);
      LockedExits.Add(key, lockedRoom);
    }

    public string Use(IItem item)
    {
      if (LockedExits.ContainsKey(item))
      {
        Exits.Add(LockedExits[item].Key, LockedExits[item].Value);
        LockedExits.Remove(item);
        if (item.Name.ToLower() == "steak")
        {
          return "You throw the steak out the window and the rabid dog loses interest in you and runs after the steak. You can now go into the living room.";
        }
        if (item.Name.ToLower() == "keys")
        {
          return "You unlock the garage door and quickly enter the garage.";
        }
      }
      return "No use for that here";
    }

    public Room(string name, string description)
    {
      Name = name;
      Description = description;
      Items = new List<IItem>();
      Exits = new Dictionary<string, IRoom>();
      LockedExits = new Dictionary<IItem, KeyValuePair<string, IRoom>>();
    }
  }
}