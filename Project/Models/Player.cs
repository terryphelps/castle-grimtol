using System;
using System.Collections.Generic;
using CastleGrimtol.Project.Interfaces;

namespace CastleGrimtol.Project.Models
{
  public class Player : IPlayer
  {
    public void ShowInv()
    {
      if (Inventory.Count != 0)
      {
        Inventory.ForEach(Item =>
        {
          Console.WriteLine(Item.Name);
        });
      }
      else
        Console.WriteLine("No Items");
    }

    public string PlayerName { get; set; }
    public List<Item> Inventory { get; set; }
    public Player(string playername)
    {
      PlayerName = playername;
      Inventory = new List<Item>();
    }
  }
}