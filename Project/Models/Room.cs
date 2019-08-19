using System;
using System.Collections.Generic;
using System.Threading;
using CastleGrimtol.Project.Interfaces;

namespace CastleGrimtol.Project.Models
{
  public class Room : IRoom
  {
    public string Name { get; set; }
    public string Description { get; set; }
    public List<Item> Items { get; set; }
    public Dictionary<string, IRoom> Exits { get; set; }
    public IRoom Go(string direction) //Directions, 
    {
      if (Exits.ContainsKey(direction))
      {
        if (this.Name == "Water Room" && direction == "west")
        {
          string dl = "You take a deep breath and enter the hole only to get eaten by a Goliath Tigerfish";
          string deathLine = dl.ToUpper();
          Console.WriteLine(deathLine);
          return Exits[direction];
        }
        Console.WriteLine("Traveling....");
        Thread.Sleep(1500);
        Console.Clear();
        return Exits[direction];
      }
      Console.WriteLine("No access this direction");
      return this;
    }

    public void ShowItems()
    {
      Items.ForEach(Item =>
      {
        if (Item.Description == "")
        {
          Item.Description = "None";
          Console.WriteLine("no items");
        }
        Console.WriteLine($"{Item.Name} -- {Item.Description}");
      });
    }

    public Room(string name, string description)
    {
      Name = name;
      Description = description;
      Items = new List<Item>();
      Exits = new Dictionary<string, IRoom>();
    }
  }
}