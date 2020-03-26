using System;
using System.Threading;
using escape_house.Interfaces;
using escape_house.Services;

namespace escape_house.Controllers
{
  class GameController : IGameController
  {
    private IGameService _gs { get; set; }
    private bool _running { get; set; } = true;
    public void Run()
    {

      Console.WriteLine("Hello there! Enter your name to start playing the game.");
      // NOTE Gets string from readline and passes is as the player name
      _gs = new GameService(Console.ReadLine());
      System.Console.WriteLine(@"
                              .-----.
                            .'       `.
                           :      ^v^  :
                           :           :
                           '           '
            |~        www   `.       .'
           /.\       /#^^\_   `-/\--'
          /#  \     /#%    \   /# \
         /#%   \   /#%______\ /#%__\
        /#%     \   |= I I ||  |- |
        ~~|~~~|~~   |_=_-__|'  |[]|
          |[] |_______\__|/_ _ |= |`.
   ^V^    |-  /= __   __    /-\|= | :;
          |= /- /\/  /\/   /=- \.-' :;
          | /_.=========._/_.-._\  .:'
          |= |-.'.- .'.- |  /|\ |.:'
          \  |=|:|= |:| =| |~|~||'|
           |~|-|:| -|:|  |-|~|~||=|      ^V^
           |=|=|:|- |:|- | |~|~|| |
           | |-_~__=_~__=|_^^^^^|/___
           |-(=-=-=-=-=-(|=====/=_-=/\
           | |=_-= _=- _=| -_=/=_-_/__\ 
           | |- _ =_-  _-|=_- |]#| I II
           |=|_/ \_-_= - |- = |]#| I II
       jgs | /  _/ \. -_=| =__|]!!!I_II!!
          _|/-'/  ` \_/ \|/' _ ^^^^`.==_^.
        _/  _/`-./`-; `-.\_ / \_'\`. `. ===`.
       / .-'  __/_   `.   _/.' .-' `-. ; ====;\
      /.   `./    \ `. \ / -  /  .-'.' ====='  >
     /  \  /  .-' `--.  / .' /  `-.' ======.' /
      ");
      string greeting = "You wake up in the front room of a delapatated house. The hair on the back of your neck stands up as you hear growling coming from somewhere close. How did you end up here? Nevermind, that doesn't matter right now... What matters is that you need to get out of here and fast!";
      foreach (char letter in greeting)
      {
        Console.Write(letter);
        Thread.Sleep(50);
      }
      Thread.Sleep(100);
      Console.WriteLine();
      Print();
      while (_running)
      {
        GetUserInput();
        Print();
      }
    }
    public void GetUserInput()
    {
      // go north
      // take brass key
      // command option
      // look
      // command
      Console.WriteLine("What are you going to do?");
      string input = Console.ReadLine().ToLower() + " "; //go north ;take toilet paper ;look 
      string command = input.Substring(0, input.IndexOf(" ")); //go; take; look
      string option = input.Substring(input.IndexOf(" ") + 1).Trim();//north; toilet paper;''

      Console.Clear();
      switch (command)
      {
        case "quit":
          _running = false;
          break;
        case "reset":
          _gs.Reset();
          break;
        case "look":
          _gs.Look();
          break;
        case "inventory":
          _gs.Inventory();
          break;
        case "go":
          _running = _gs.Go(option);
          break;
        case "take":
          _gs.Take(option);
          break;
        case "use":
          _gs.Use(option);
          break;
        default:
          _gs.Messages.Add("Not a recognized command");
          _gs.Look();
          break;
      }
    }

    public void Print()
    {
      foreach (string message in _gs.Messages)
      {
        Console.WriteLine(message);
      }
      _gs.Messages.Clear();
    }

  }
}