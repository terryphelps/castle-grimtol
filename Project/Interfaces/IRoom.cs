using System.Collections.Generic;
using CastleGrimtol.Project.Models;

namespace CastleGrimtol.Project.Interfaces
{
  public interface IRoom
  {
    void ShowItems();
    string Name { get; set; }
    string Description { get; set; }
    List<Item> Items { get; set; }
    Dictionary<string, IRoom> Exits { get; set; }
    IRoom Go(string direction);
  }
}
