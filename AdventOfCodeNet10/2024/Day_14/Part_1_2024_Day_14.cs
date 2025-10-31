using System.Diagnostics;
using AdventOfCodeNet10.Extensions;

namespace AdventOfCodeNet10._2024.Day_14
{
  internal class Part_1_2024_Day_14 : Days
  {
    /// <summary>
    /*
    https://adventofcode.com/2024/day/14
    --- Day 14: Restroom Redoubt ---
    One of The Historians needs to use the bathroom; fortunately, you know there's
    a bathroom near an unvisited location on their list, and so you're all quickly
    teleported directly to the lobby of Easter Bunny Headquarters.
    
    Unfortunately, EBHQ seems to have "improved" bathroom security again after your
    last visit. The area outside the bathroom is swarming with robots!
    
    To get The Historian safely to the bathroom, you'll need a way to predict where
    the robots will be in the future. Fortunately, they all seem to be moving on
    the tile floor in predictable straight lines.
    
    You make a list (your puzzle input) of all of the robots' current positions (p)
    and velocities (v), one robot per line. For example:
    
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
    Each robot's position is given as p=x,y where x represents the number of tiles
    the robot is from the left wall and y represents the number of tiles from the
    top wall (when viewed from above). So, a position of p=0,0 means the robot is
    all the way in the top-left corner.
    
    Each robot's velocity is given as v=x,y where x and y are given in tiles per
    second. Positive x means the robot is moving to the right, and positive y means
    the robot is moving down. So, a velocity of v=1,-2 means that each second, the
    robot moves 1 tile to the right and 2 tiles up.
    
    The robots outside the actual bathroom are in a space which is 101 tiles wide
    and 103 tiles tall (when viewed from above). However, in this example, the
    robots are in a space which is only 11 tiles wide and 7 tiles tall.
    
    The robots are good at navigating over/under each other (due to a combination
    of springs, extendable legs, and quadcopters), so they can share the same tile
    and don't interact with each other. Visually, the number of robots on each tile
    in this example looks like this:
    
    1.12.......
    ...........
    ...........
    ......11.11
    1.1........
    .........1.
    .......1...
    These robots have a unique feature for maximum bathroom security: they can
    teleport. When a robot would run into an edge of the space they're in, they
    instead teleport to the other side, effectively wrapping around the edges. Here
    is what robot p=2,4 v=2,-3 does for the first few seconds:
    
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
    The Historian can't wait much longer, so you don't have to simulate the robots
    for very long. Where will the robots be after 100 seconds?
    
    In the above example, the number of robots on each tile after 100 seconds has
    elapsed looks like this:
    
    ......2..1.
    ...........
    1..........
    .11........
    .....1.....
    ...12......
    .1....1....
    To determine the safest area, count the number of robots in each quadrant after
    100 seconds. Robots that are exactly in the middle (horizontally or vertically)
    don't count as being in any quadrant, so the only relevant robots are:
    
    ..... 2..1.
    ..... .....
    1.... .....
    
    ..... .....
    ...12 .....
    .1... 1....
    In this example, the quadrants contain 1, 3, 4, and 1 robot. Multiplying these
    together gives a total safety factor of 12.
    
    Predict the motion of the robots in your list within a space which is 101 tiles
    wide and 103 tiles tall. What will the safety factor be after exactly 100
    seconds have elapsed?
    */
    /// </summary>
    /// <returns>
    /// (12) Test
    /// 
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
      if (Lines.Count < 15) grid = testGrid;

      var robots = new List<(LongPoint pos, LongPoint vel)>();

      foreach (var line in Lines)
      {
        var chunks = line.Split(new[] { ' ', ',', '=' }, StringSplitOptions.RemoveEmptyEntries);
        robots.Add((
          new LongPoint(Int64.Parse(chunks[1]), Int64.Parse(chunks[2])),
          new LongPoint(Int64.Parse(chunks[4]), Int64.Parse(chunks[5]))
          ));
      }

      for (int i = 0; i < 100; i++)
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
      }

      //split into quadrants
      long q1_ends_x = grid.x / 2; // excl  upper
      long q2_starts_x = grid.x / 2 + 1; // excl  cardedge
      long q1_ends_y = grid.y / 2;  // excl  upper
      long q2_starts_y = grid.y / 2 + 1; // excl  cardedge

      long[] totalCountQ = new long[4] { 0, 0, 0, 0 };

      foreach (var robot in robots)
      {
        if (robot.pos.y.InRange(0, q1_ends_y, IncludeBounds.Lower)) // tophalf
        {
          if (robot.pos.x.InRange(0, q1_ends_x, IncludeBounds.Lower))
          {
            //Add to Q1 (tl)
            totalCountQ[0]++;
          }
          else if (robot.pos.x.InRange(q2_starts_x , grid.x, IncludeBounds.Lower))
          {
            //Add to Q2 (tr)
            totalCountQ[1]++;
          }
        }
        else if (robot.pos.y.InRange(q1_ends_y + 1, grid.y, IncludeBounds.Lower)) // bottom half
        {
          if (robot.pos.x.InRange(0, q1_ends_x, IncludeBounds.Lower))
          {
            //Add to Q3 (bl)
            totalCountQ[2]++;
          }
          else if (robot.pos.x.InRange(q2_starts_x, grid.x, IncludeBounds.Lower))
          {
            //Add to Q4 (br)
            totalCountQ[3]++;
          }
        }
      }

      //DebugDrawMap(ref robots, ref grid);

      totalCount = totalCountQ[0] * totalCountQ[1] * totalCountQ[2] * totalCountQ[3];
      result = totalCount.ToString();
      return result;
    }

    private void DebugDrawMap(ref List<(LongPoint pos, LongPoint vel)> robots, ref LongPoint grid)
    {
      Debug.WriteLine("");
      Debug.WriteLine("");
      for (int y = 0; y < grid.y; y++)
      {
        for (int x = 0; x < grid.x; x++)
        {
          int count = robots.Where(r => r.pos.x == x && r.pos.y == y).Count();
          Debug.Write((count > 0) ? $"{count}" : ".");
        }
        Debug.WriteLine("");
      }
      Debug.WriteLine("");
      Debug.WriteLine("");
    }
  }
}
