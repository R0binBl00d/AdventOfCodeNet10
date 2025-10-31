using AdventOfCodeNet10.Extensions;
using System.Diagnostics;

namespace AdventOfCodeNet10._2024.Day_14
{
  internal class Part_2_2024_Day_14 : Days
  {
    /// <summary>
    /*
    https://adventofcode.com/2024/day/14
--- Day 14: Restroom Redoubt ---
One of The Historians needs to use the bathroom; fortunately, you know there's a bathroom near an unvisited location on their list, and so you're all quickly teleported directly to the lobby of Easter Bunny Headquarters.

Unfortunately, EBHQ seems to have "improved" bathroom security again after your last visit. The area outside the bathroom is swarming with robots!

To get The Historian safely to the bathroom, you'll need a way to predict where the robots will be in the future. Fortunately, they all seem to be moving on the tile floor in predictable straight lines.

You make a list (your puzzle input) of all of the robots' current positions (p) and velocities (v), one robot per line. For example:

p=0,4 v=3,-3
p=6,3 v=-1,-3
p=10,3 v=-1,2
p=2,0 v=2,-1
p=0,0 v=1,3
p=3,0 v=-2,-2
p=7,6 v=-1,-3
p=3,0 v=-1,-2
p=9,3 v=2,3
p=7,3 v=-1,2
p=2,4 v=2,-3
p=9,5 v=-3,-3
Each robot's position is given as p=x,y where x represents the number of tiles the robot is from the left wall and y represents the number of tiles from the top wall (when viewed from above). So, a position of p=0,0 means the robot is all the way in the top-left corner.

Each robot's velocity is given as v=x,y where x and y are given in tiles per second. Positive x means the robot is moving to the right, and positive y means the robot is moving down. So, a velocity of v=1,-2 means that each second, the robot moves 1 tile to the right and 2 tiles up.

The robots outside the actual bathroom are in a space which is 101 tiles wide and 103 tiles tall (when viewed from above). However, in this example, the robots are in a space which is only 11 tiles wide and 7 tiles tall.

The robots are good at navigating over/under each other (due to a combination of springs, extendable legs, and quadcopters), so they can share the same tile and don't interact with each other. Visually, the number of robots on each tile in this example looks like this:

1.12.......
...........
...........
......11.11
1.1........
.........1.
.......1...
These robots have a unique feature for maximum bathroom security: they can teleport. When a robot would run into an edge of the space they're in, they instead teleport to the other side, effectively wrapping around the edges. Here is what robot p=2,4 v=2,-3 does for the first few seconds:

Initial state:
...........
...........
...........
...........
..1........
...........
...........

After 1 second:
...........
....1......
...........
...........
...........
...........
...........

After 2 seconds:
...........
...........
...........
...........
...........
......1....
...........

After 3 seconds:
...........
...........
........1..
...........
...........
...........
...........

After 4 seconds:
...........
...........
...........
...........
...........
...........
..........1

After 5 seconds:
...........
...........
...........
.1.........
...........
...........
...........
The Historian can't wait much longer, so you don't have to simulate the robots for very long. Where will the robots be after 100 seconds?

In the above example, the number of robots on each tile after 100 seconds has elapsed looks like this:

......2..1.
...........
1..........
.11........
.....1.....
...12......
.1....1....
To determine the safest area, count the number of robots in each quadrant after 100 seconds. Robots that are exactly in the middle (horizontally or vertically) don't count as being in any quadrant, so the only relevant robots are:

..... 2..1.
..... .....
1.... .....
           
..... .....
...12 .....
.1... 1....
In this example, the quadrants contain 1, 3, 4, and 1 robot. Multiplying these together gives a total safety factor of 12.

Predict the motion of the robots in your list within a space which is 101 tiles wide and 103 tiles tall. What will the safety factor be after exactly 100 seconds have elapsed?

Your puzzle answer was 217132650.

--- Part Two ---
During the bathroom break, someone notices that these robots seem awfully similar to ones built and used at the North Pole. If they're the same type of robots, they should have a hard-coded Easter egg: very rarely, most of the robots should arrange themselves into a picture of a Christmas tree.

What is the fewest number of seconds that must elapse for the robots to display the Easter egg?

Your puzzle answer was 6516.
    */
    /// </summary>
    /// <returns>
    /// 6515 too low
	/// 6516
    /// </returns>
    public override string Execute()
    {
      string result = "";
      long totalCount = 0;

      //Real(x,y) = (101,103)
      //100 sec -> multiply quadrants

      LongPoint testGrid = new LongPoint(11, 7);
      LongPoint grid = new LongPoint(101, 103);

      //Test(x,y) = (11,7)
      if (Lines.Count < 15) return "Skip Test"; // grid = testGrid;

      var robots = new List<(LongPoint pos, LongPoint vel)>();

      foreach (var line in Lines)
      {
        var chunks = line.Split(new[] { ' ', ',', '=' }, StringSplitOptions.RemoveEmptyEntries);
        robots.Add((
          new LongPoint(Int64.Parse(chunks[1]), Int64.Parse(chunks[2])),
          new LongPoint(Int64.Parse(chunks[4]), Int64.Parse(chunks[5]))
        ));
      }

      var dict = new Dictionary<long, (double, List<LongPoint>)>();
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

    private (double, List<LongPoint> ) CalculateMinDistanceToAverageY(ref List<(LongPoint pos, LongPoint vel)> robots)
    {
      double averageX = (double)robots.Sum(r => r.pos.x) / (double)robots.Count;
      double varianceX = robots.Sum(r => Math.Pow(r.pos.x - averageX, 2)) / (robots.Count - 1);

      double averageY = (double)robots.Sum(r => r.pos.y) / (double)robots.Count;
      double varianceY = robots.Sum(r => Math.Pow(r.pos.y - averageY, 2)) / (robots.Count - 1);

      double stdVariation = (Math.Sqrt(varianceX) + Math.Sqrt(varianceY)) / 2;


      var tmpList = new List<LongPoint>();
      foreach (var rob in robots.Select(r => r.pos).ToList())
      {
        tmpList.Add(new LongPoint(rob.x, rob.y));
      }

      (double, List<LongPoint>) ret = (stdVariation, tmpList);
      return ret;
    }
    

    private void DebugDrawMap(ref List<LongPoint> robots, ref LongPoint grid)
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