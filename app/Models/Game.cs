using escape_house.Interfaces;

namespace escape_house.Models
{
  class Game : IGame
  {
    public IPlayer CurrentPlayer { get; set; }
    public IRoom CurrentRoom { get; set; }

    ///<summary>Initalizes data and establishes relationships</summary>
    public Game()
    {
      // NOTE ALL THESE VARIABLES ARE REMOVED AT THE END OF THIS METHOD
      // We retain access to the objects due to the linked list


      // NOTE Create all rooms
      Room frontRoom = new Room("Front Room", "There are still logs burning in the fireplace. Is the owner of the house still home? The front door is locked and you can't find anything to break a window with.");
      Room bathroom = new Room("Bathroom", "The smell slaps you across the face as you enter. There isn't anything worth touching in this room.");
      Room hallway = new Room("Hallway", "The floor boards creak as you walk down the hallway. Are the eyes in the pictures following you?");
      Room kitchen = new Room("Kitchen", "The table is set for two. There is a mysterious liquid bubbling in a pot on the stove.");
      Room diningRoom = new Room("Dining Room", "There is a hutch in the corner filled with jars. You don't want to investigate the contents. The sliding glass door to the north is unlocked.");
      Room livingRoom = new Room("Living Room", "That will only keep the dog busy for a short amount of time. You need to get out of this house quickly! You see a couch torn to shreds and the remains of a small animal in the corner.");
      EndRoom backPorch = new EndRoom("Back Porch", "You step out onto the porch and hear the boards creak under you. The staircase is on the other side of the porch. You run towards the stairs.", false, "The porch colapses under you and you fall 2 stories to the ground. You hear growling coming from next to your head. DEATH!");
      EndRoom garage = new EndRoom("Garage", "There is a truck parked in the garage. You jump in and find the keys in the ignition. ", true, "You crash through the garage door to freedom");

      // NOTE Create all Items
      Item steak = new Item("Steak", "still warm sitting on a plate with mashed potatoes and peas.");
      Item keys = new Item("Keys", "there is at least 15 keys on this ring. Could these be helpful?");
      Item bourbon = new Item("Bourbon", "top shelf brand too!");
      Item woodenSpoon = new Item("Spoon", "still dirty lying in the kitchen sink.");

      // NOTE Make Room Relationships
      frontRoom.Exits.Add("north", hallway);
      hallway.Exits.Add("north", kitchen);
      hallway.Exits.Add("south", frontRoom);
      hallway.Exits.Add("west", bathroom);
      hallway.AddLockedRoom(steak, "east", livingRoom);
      bathroom.Exits.Add("east", hallway);
      kitchen.Exits.Add("south", hallway);
      kitchen.Exits.Add("east", diningRoom);
      diningRoom.Exits.Add("north", backPorch);
      diningRoom.Exits.Add("west", kitchen);
      livingRoom.Exits.Add("west", hallway);
      livingRoom.AddLockedRoom(keys, "south", garage);


      // NOTE put Items in Rooms
      frontRoom.Items.Add(bourbon);
      kitchen.Items.Add(woodenSpoon);
      kitchen.Items.Add(steak);
      diningRoom.Items.Add(keys);



      CurrentRoom = frontRoom;
    }
  }
}