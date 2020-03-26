using System;
using escape_house.Controllers;
using escape_house.Interfaces;

namespace escape_house
{
  class Program
  {
    static void Main(string[] args)
    {
      Console.Clear();
      IGameController gc = new GameController();
      gc.Run();
    }
  }
}
