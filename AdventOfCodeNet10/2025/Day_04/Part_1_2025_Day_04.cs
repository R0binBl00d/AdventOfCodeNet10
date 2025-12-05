using System.Diagnostics;
using AdventOfCodeNet10.Extensions;

namespace AdventOfCodeNet10._2025.Day_04
{
  internal class Part_1_2025_Day_04 : Days
  {
    /// <summary>
    /*
    https://adventofcode.com/2025/day/4
    --- Day 4: Printing Department ---
    You ride the escalator down to the printing department. They're clearly getting
    ready for Christmas; they have lots of large rolls of paper everywhere, and
    there's even a massive printer in the corner (to handle the really big print
    jobs).
    
    Decorating here will be easy: they can make their own decorations. What you
    really need is a way to get further into the North Pole base while the
    elevators are offline.
    
    "Actually, maybe we can help with that," one of the Elves replies when you ask
    for help. "We're pretty sure there's a cafeteria on the other side of the back
    wall. If we could break through the wall, you'd be able to keep moving. It's
    too bad all of our forklifts are so busy moving those big rolls of paper
    around."
    
    If you can optimize the work the forklifts are doing, maybe they would have
    time to spare to break through the wall.
    
    The rolls of paper (@) are arranged on a large grid; the Elves even have a
    helpful diagram (your puzzle input) indicating where everything is located.
    
    For example:
    
    ..@@.@@@@.
    @@@.@.@.@@
    @@@@@.@.@@
    @.@@@@..@.
    @@.@@@@.@@
    .@@@@@@@.@
    .@.@.@.@@@
    @.@@@.@@@@
    .@@@@@@@@.
    @.@.@@@.@.
    The forklifts can only access a roll of paper if there are fewer than four
    rolls of paper in the eight adjacent positions. If you can figure out which
    rolls of paper the forklifts can access, they'll spend less time looking and
    more time breaking down the wall to the cafeteria.
    
    In this example, there are 13 rolls of paper that can be accessed by a forklift
    (marked with x):
    
    ..xx.xx@x.
    x@@.@.@.@@
    @@@@@.x.@@
    @.@@@@..@.
    x@.@@@@.@x
    .@@@@@@@.@
    .@.@.@.@@@
    x.@@@.@@@@
    .@@@@@@@@.
    x.x.@@@.x.
    Consider your complete diagram of the paper roll locations. How many rolls of
    paper can be accessed by a forklift?
    */
    /// </summary>
    /// <returns>
    /// 1467
    /// </returns>
    public override string Execute()
    {
      string result = "";
      long totalCount = 0;

      MapOrGrid map = new MapOrGrid(Lines[0].Length, Lines.Count);
      map.SetTiles(Lines);

      //List<LongPoint> paperRollsHeCanAccess = new List<LongPoint>();
      
      foreach ((LongPoint, char) tile in map.TilesList)
      {
        if (tile.Item2 == '.') continue;
        if (tile.Item2 != '@') Debugger.Break();

        //map.GetNeighborsIncludingDiagonals(tile.Item1)
        //  .Aggregate(0, (i, point) => map.GetTile(point) == '@' ? i + 1 : i);
        var count = map.GetNeighborsIncludingDiagonals(tile.Item1)
          .Count(point => map.GetTile(point) == '@');

        if (count <= 3) totalCount++;

        //List<LongPoint> temp = map.GetNeighborsIncludingDiagonals(tile.Item1)
        //  .Where(point => map.GetTile(point) == '.')
        //  .ToList();

        //foreach (var longPoint in temp)
        //{
        //  if(!paperRollsHeCanAccess.Contains(longPoint))
        //    paperRollsHeCanAccess.Add(longPoint);
        //}

      }
      result = totalCount.ToString();
      return result;
    }
  }
}
