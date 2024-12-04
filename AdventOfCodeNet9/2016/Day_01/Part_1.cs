namespace AdventOfCodeNet9._2016.Day_01
{
  internal class Part_1 : Days
  {
    /// <summary>
    /*
    https://adventofcode.com/2016/day/1
--- Day 1: No Time for a Taxicab ---
Santa's sleigh uses a very high-precision clock to guide its movements, and the clock's oscillator is regulated by stars. Unfortunately, the stars have been stolen... by the Easter Bunny. To save Christmas, Santa needs you to retrieve all fifty stars by December 25th.

Collect stars by solving puzzles. Two puzzles will be made available on each day in the Advent calendar; the second puzzle is unlocked when you complete the first. Each puzzle grants one star. Good luck!

You're airdropped near Easter Bunny Headquarters in a city somewhere. "Near", unfortunately, is as close as you can get - the instructions on the Easter Bunny Recruiting Document the Elves intercepted start here, and nobody had time to work them out further.

The Document indicates that you should start at the given coordinates (where you just landed) and face North. Then, follow the provided sequence: either turn left (L) or right (R) 90 degrees, then walk forward the given number of blocks, ending at a new intersection.

There's no time to follow such ridiculous instructions on foot, though, so you take a moment and work out the destination. Given that you can only walk on the street grid of the city, how far is the shortest path to the destination?

For example:

Following R2, L3 leaves you 2 blocks East and 3 blocks North, or 5 blocks away.
R2, R2, R2 leaves you 2 blocks due South of your starting position, which is 2 blocks away.
R5, L5, R5, R3 leaves you 12 blocks away.
How many blocks away is Easter Bunny HQ?
    */
    /// </summary>
    /// <returns>
    /// Your puzzle answer was 243.
    /// </returns>
    public override string Execute()
    {
      string result = "";

      var chunks = Lines[0].Split(',');

      Point location = new Point(0, 0);
      int heading = 0; // 0 North -y, 1 East +x, 2 South +y, 3 West -x

      foreach (var chunk in chunks)
      {
        var tmp = chunk.Trim();
        ChangeHeading(ref heading, tmp[0]);
        WalkSteps(ref location, heading, tmp.Substring(1).Trim());
      }

      int distance = Math.Abs(location.X) + Math.Abs(location.Y);
      result = distance.ToString();
      return result;
    }

    private void ChangeHeading(ref int h, char c)
    {
      switch (c)
      {
        case 'R':
          h = h + 1;
          break;
        case 'L':
          h = h + 3;
          break;
      }
      h %= 4;
    }

    private void WalkSteps(ref Point p, int heading, string distance)
    {
      int steps = Int32.Parse(distance);
      // 0 North -y, 1 East +x, 2 South +y, 3 West -x
      switch (heading)
      {
        case 0:
          p.Y = p.Y - steps;
          break;
        case 1:
          p.X = p.X + steps;
          break;
        case 2:
          p.Y = p.Y + steps;
          break;
        case 3:
          p.X = p.X - steps;
          break;
      }
    }
  }
}
