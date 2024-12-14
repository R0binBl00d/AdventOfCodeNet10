using AdventOfCodeNet9.Extensions;
using System.Diagnostics;
using AdventOfCodeNet9._2024.Day_07;

namespace AdventOfCodeNet9._2024.Day_14
{
  internal class Part_2_2024_Day_14 : Days
  {
    /// <summary>
    /*
    https://adventofcode.com/2024/day/14

        --- Part Two ---
    During the bathroom break, someone notices that these robots seem awfully
    similar to ones built and used at the North Pole. If they're the same type of
    robots, they should have a hard-coded Easter egg: very rarely, most of the
    robots should arrange themselves into a picture of a Christmas tree.
    
    What is the fewest number of seconds that must elapse for the robots to display
    the Easter egg?

    */
    /// </summary>
    /// <returns>
    /// 6515 too low
    /// </returns>
    public override string Execute()
    {
      string result = "";
      long totalCount = 0;

      //Real(x,y) = (101,103)
      //100 sec -> multiply quadrants

      point testGrid = new point(11, 7);
      point grid = new point(101, 103);

      //Test(x,y) = (11,7)
      if (Lines.Count < 15) return "Skip Test"; // grid = testGrid;

      var robots = new List<(point pos, point vel)>();

      foreach (var line in Lines)
      {
        var chunks = line.Split(new[] { ' ', ',', '=' }, StringSplitOptions.RemoveEmptyEntries);
        robots.Add((
          new point(Int64.Parse(chunks[1]), Int64.Parse(chunks[2])),
          new point(Int64.Parse(chunks[4]), Int64.Parse(chunks[5]))
        ));
      }

      var dict = new Dictionary<long, (double, List<point>)>();
      for(long i=1; i<30000; i++)
      {
        //Advance Robots
        foreach (var robot in robots)
        {
          robot.pos.x += robot.vel.x;
          robot.pos.y += robot.vel.y;

          if (robot.pos.x < 0) robot.pos.x += grid.x;
          if (robot.pos.x >= grid.x) robot.pos.x -= grid.x;
          if (robot.pos.y < 0) robot.pos.y += grid.y;
          if (robot.pos.y >= grid.y) robot.pos.y -= grid.y;
        }

        dict.Add(i, CalculateMinDistanceToAverageY(ref robots));
      }

      var bestItems = dict.OrderBy(d => d.Value.Item1).ToList();

      //foreach (var keyValuePair in bestItems)
      //{
      //  var tmp = keyValuePair.Value.Item2.ToList();
      //  DebugDrawMap(ref tmp, ref grid);
      //}

      totalCount = bestItems.First().Key;

      result = totalCount.ToString();
      return result;
    }

    private (double, List<point> ) CalculateMinDistanceToAverageY(ref List<(point pos, point vel)> robots)
    {
      double averageX = (double)robots.Sum(r => r.pos.x) / (double)robots.Count;
      double varianceX = robots.Sum(r => Math.Pow(r.pos.x - averageX, 2)) / (robots.Count - 1);

      double averageY = (double)robots.Sum(r => r.pos.y) / (double)robots.Count;
      double varianceY = robots.Sum(r => Math.Pow(r.pos.y - averageY, 2)) / (robots.Count - 1);

      double stdVariation = (Math.Sqrt(varianceX) + Math.Sqrt(varianceY)) / 2;


      var tmpList = new List<point>();
      foreach (var rob in robots.Select(r => r.pos).ToList())
      {
        tmpList.Add(new point(rob.x, rob.y));
      }

      (double, List<point>) ret = (stdVariation, tmpList);
      return ret;
    }
    

    private void DebugDrawMap(ref List<point> robots, ref point grid)
    {
      Debug.WriteLine("");
      Debug.WriteLine("");
      for (int y = 0; y < grid.y; y++)
      {
        for (int x = 0; x < grid.x; x++)
        {
          int count = robots.Where(r => r.x == x && r.y == y).Count();
          Debug.Write((count > 0) ? $"{count}" : ".");
        }

        Debug.WriteLine("");
      }

      Debug.WriteLine("");
      Debug.WriteLine("");
    }
  }
}