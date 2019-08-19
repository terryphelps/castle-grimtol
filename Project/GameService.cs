using System;
using System.Threading;
using System.Collections.Generic;
using CastleGrimtol.Project.Interfaces;
using CastleGrimtol.Project.Models;

namespace CastleGrimtol.Project
{
  public class GameService : IGameService
  {

    private bool ResetGame { get; set; } = false;
    public bool Surviving { get; set; } = true;
    public IRoom CurrentRoom { get; set; }
    public Player CurrentPlayer { get; set; }

    public void GetUserInput()
    {

    }

    public void Go(string direction) //north, east, south, west
    {
      CurrentRoom = CurrentRoom.Go(direction);
    }
    public GameService()
    {

    }


    public void Help()
    {
      string helpMessage = "Valid commands: go north, go east, go south, go west, take item, use item, look, quit, reset, help, ?";
      Console.WriteLine();
      Console.WriteLine(helpMessage);
      Console.WriteLine();

    }

    public void Inventory()
    {

    }

    public void Look()
    {
      string lookMessage = $@"{CurrentRoom.Description} ";
      Console.WriteLine();
      Console.WriteLine(lookMessage);
      Console.Write("Items in room: "); CurrentRoom.ShowItems();
      Console.WriteLine();
    }

    public void Quit()
    {
      Surviving = false;
      ResetGame = true;
    }

    public void Death()
    {
      string deathMessage = $@"**********************************************************************
                   Good try { CurrentPlayer.PlayerName} YOU LOSE
**********************************************************************";
      Thread.Sleep(5000);
      Console.Clear();
      Console.Write(deathMessage);
    }
    public void Win()
    {
      string winMessage = $@"**********************************************************************
                   Great job { CurrentPlayer.PlayerName} YOU WON!!!!!!!
**********************************************************************";
      Thread.Sleep(3000);
      Console.Clear();
      Console.Write(winMessage);
    }

    public void Reset()
    {
      Setup();
      Surviving = false;
    }

    public void Setup()
    {
      //NOTE Message strings
      string wellOpeningMessage = @"This cavern has a very muddy floor with a faint glow of light in the ceiling from which you entered.
It is too high for you to be able to climb back out of.";
      string rockPileMessage = @"This is a dark and open chamber with many large rocks scattered about the floor. To the west you notice
a pile of rocks that blocks the bottom half of an opening";
      string lowCeilingMessage = @"This is a small opening with a sandy floor and a ceiling so low 
you cannot stand up. You crawl all around and find an opening on the southern side.";
      string debrisMessage = @"This is a fairly large open room and is over waist deep in water. 
You can hear water leaking into the room, but notice it isn't getting any deeper. After looking around you 
could feel under the water an opening on the west wall, on the east wall you can feel a large amount of
debris and can feel the water moving around it. ";
      string deathHoleMessage = @"You take a deep breath and enter the hole only to get eaten by a Goliath Tigerfish";
      string winningLagoonMessage = @"You use the heavy rock to clear away the debris and all of the sudden
you hear things starting to crack and then you are sucked into the water vortex and spit out into a beautiful
lagoon. The heavy rock you were using was actually a gold nugget. You are rich!!";
      //NOTE Item Creation
      Item rock = new Item("Rock", "This rock is very heavy for its size");
      Item mud = new Item("Mud", "This is very gooey");
      Item sand = new Item("Sand", "This sand is nothing special");
      Item stone = new Item("Stone", "This is just another rock");
      Item water = new Item("Water", "There are thousands of gallons, but I wouldn't drink it!");

      //NOTE  Rooms Creation
      Room wellOpening = new Room("Muddy Room", wellOpeningMessage);
      Room rockPile = new Room("Rocky Room", rockPileMessage);
      Room lowCeiling = new Room("Sandy Room", lowCeilingMessage);
      Room debris = new Room("Water Room", debrisMessage);
      Room deathHole = new Room("Death Hole", deathHoleMessage);
      Room winningLagoon = new Room("", winningLagoonMessage);

      //NOTE  Setup Room Exits and room item(s)

      wellOpening.Exits.Add("south", rockPile);
      wellOpening.Items.Add(mud);
      rockPile.Exits.Add("north", wellOpening);
      rockPile.Exits.Add("west", lowCeiling);
      rockPile.Items.Add(rock);
      rockPile.Items.Add(stone);
      lowCeiling.Exits.Add("east", rockPile);
      lowCeiling.Items.Add(sand);
      lowCeiling.Exits.Add("south", debris);
      debris.Items.Add(water);
      debris.Exits.Add("north", lowCeiling);
      debris.Exits.Add("west", deathHole);
      deathHole.Exits.Add("", deathHole);

      //NOTE  Set beginning room
      CurrentRoom = wellOpening;
      CurrentPlayer = new Player("");
    }
    public void StartGame()
    {
      while (!ResetGame)
      {
        Surviving = true;
        Console.Clear();
        Console.Write("To begin enter your name: ");
        CurrentPlayer.PlayerName = Console.ReadLine();
        Console.WriteLine();
        string opening = $@"{CurrentPlayer.PlayerName}, you are walking in the woods looking for a famed huckleberry patch and unknowingly step onto a obscured 
rotting piece of plywood and your feet fall through. You fall 60' down an abandon well into a large open cave. Your
fall was abruptly stopped by landing in two feed of mud. You are physically ok, but you can only see a faint light 
up the hole you fell into. You quickly realize climbing out is impossible. After you get your bearings you notice it 
is too dark to see anything around you. Be careful, and let the survival begin!";
        Console.WriteLine(opening);
        Console.WriteLine();
        while (Surviving)
        {
          string compareRoom = CurrentRoom.Name;
          if (compareRoom == "Death Hole")
          {
            Death();
            Surviving = false;
            ResetGame = true;
            break;
          }
          Console.Write($@"{CurrentPlayer.PlayerName}, you are in the {CurrentRoom.Name} and you possess "); CurrentPlayer.ShowInv();
          Console.Write($"What would you like to do now? ");
          string[] choice = Console.ReadLine().ToLower().Split(' ');
          string command = choice[0]; //command = 'go'
          string option = "";
          if (choice.Length > 1)
          {
            option = choice[1];
          }
          //          'go'
          switch (command)
          {
            case "go":
              Go(option); // option {some direction}
              break;
            case "quit":
              Quit();
              break;
            case "help":
              Help();
              break;
            case "look":
              Look();
              break;
            case "?":
              Help();
              break;
            case "reset":
              Reset();
              break;
            case "take":
              TakeItem(option);
              break;
            case "use":
              UseItem(option);
              break;
            default:
              Console.WriteLine("Invalid Command, try again");
              break;
          }
        }
      }
    }

    public void TakeItem(string itemName)
    {

      // else
      // {
      //   Console.WriteLine("Nothing to take.");
      // }
      switch (itemName)
      {
        case "mud":
          Console.WriteLine($"{itemName} cannot be taken");
          break;
        case "stone":
          Console.WriteLine($"{itemName} cannot be taken");
          break;
        case "sand":
          Console.WriteLine($"{itemName} cannot be taken");
          break;
        case "water":
          Console.WriteLine($"{itemName} cannot be taken");
          break;
        case "rock":
          Item item = CurrentRoom.Items.Find(Item => Item.Name.ToLower() == itemName);
          if (item != null)
          {
            if (CurrentRoom.Name == "Rocky Room")
            {
              Item playerItem = CurrentPlayer.Inventory.Find(Item => Item.Name.ToLower() == itemName);
              if (playerItem != null)
              {
                Console.WriteLine($"You already possess {itemName}");
                break;
              }
              CurrentRoom.Items.Remove(item);
              CurrentPlayer.Inventory.Add(item);
            }
            break;
          }
          else
          {
            Console.WriteLine($"{itemName} cannot be taken");
          }
          break;
        default:
          Console.WriteLine("Invalid Command, try again");
          break;
      }
    }

    public void UseItem(string itemName)
    {
      if (CurrentPlayer.Inventory.Count > 0)
      {
        switch (itemName)
        {
          case "rock":
            if (CurrentRoom.Name == "Water Room")
            {
              string rockMessage = @"You use the heavy rock to clear away the debris and all of the sudden
you hear things starting to crack and then you are sucked into the water vortex and spit out into a beautiful
lagoon. The heavy rock you were using was actually a gold nugget. You are rich!!";
              Console.WriteLine(rockMessage);
              Win();
              Surviving = false;
              ResetGame = true;
              break;
            }
            Console.WriteLine("Cannot use that item in this room");
            break;
          default:
            Console.WriteLine("You do not possess that item");
            break;
        }
      }
      else
      {
        Console.WriteLine("You do not possess any items to use");
      }
    }
  }
}
