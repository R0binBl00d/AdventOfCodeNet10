namespace AdventOfCodeNet9._2015.Day_18
{
  internal class Part_1 : Days
  {
    /// <summary>
    /*
    https://adventofcode.com/2015/day/18
--- Day 18: Like a GIF For Your Yard ---
After the million lights incident, the fire code has gotten stricter: now, at most ten thousand lights are allowed. You arrange them in a 100x100 grid.

Never one to let you down, Santa again mails you instructions on the ideal lighting configuration. With so few lights, he says, you'll have to resort to animation.

Start by setting your lights to the included initial configuration (your puzzle input). A # means "on", and a . means "off".

Then, animate your grid in steps, where each step decides the next configuration based on the current one. Each light's next state (either on or off) depends on its current state and the current states of the eight lights adjacent to it (including diagonals). Lights on the edge of the grid might have fewer than eight neighbors; the missing ones always count as "off".

For example, in a simplified 6x6 grid, the light marked A has the neighbors numbered 1 through 8, and the light marked B, which is on an edge, only has the neighbors marked 1 through 5:

1B5...
234...
......
..123.
..8A4.
..765.
The state a light should have next is based on its current state (on or off) plus the number of neighbors that are on:

A light which is on stays on when 2 or 3 neighbors are on, and turns off otherwise.
A light which is off turns on if exactly 3 neighbors are on, and stays off otherwise.
All of the lights update simultaneously; they all consider the same current state before moving to the next.

Here's a few steps from an example configuration of another 6x6 grid:

Initial state:
.#.#.#
...##.
#....#
..#...
#.#..#
####..

After 1 step:
..##..
..##.#
...##.
......
#.....
#.##..

After 2 steps:
..###.
......
..###.
......
.#....
.#....

After 3 steps:
...#..
......
...#..
..##..
......
......

After 4 steps:
......
......
..##..
..##..
......
......
After 4 steps, this example has four lights on.

In your grid of 100x100 lights, given your initial configuration, how many lights are on after 100 steps?
    */
    /// </summary>
    /// <returns>
    /// Your puzzle answer was 821.
    /// </returns>
    public override string Execute()
    {
      string result = "";

      int[,] lights = new int[100, 100];

      int x = 0;
      int y = 0;

      // initial condition
      foreach (string line in Lines)
      {
        foreach (char c in line)
        {
          lights[x, y] = c == '#' ? 1 : 0;

          x++;
        }
        y++;
        x = 0;
      }

      for (int i = 0; i < 100; i++)
      {
        lights = CalculateNextPicture(lights);
      }

      // count
      int totalOn = 0;
      for (int yy = 0; yy < lights.GetLength(0); yy++)
      {
        for (int xx = 0; xx < lights.GetLength(1); xx++)
        {
          if (lights[xx, yy] == 1)
          {
            totalOn++;
          }
        }
      }

      result = totalOn.ToString();
      return result;
    }

    private int[,] CalculateNextPicture(int[,] lights)
    {
      int[,] nextState = new int[100, 100];

      for (int y = 0; y < lights.GetLength(0); y++)
      {
        for (int x = 0; x < lights.GetLength(1); x++)
        {
          nextState[x, y] = CalculateFromNeighbors(ref lights, x, y);
        }
      }

      return nextState;
    }

    private int CalculateFromNeighbors(ref int[,] lights, int x, int y)
    {
      if (lights[x, y] == 1)
      {
        // A light which is on stays on when 2 or 3 neighbors are on, and turns off otherwise.
        int cn = CountNeighboursOn(ref lights, x, y);
        return (cn == 2 || cn == 3) ? 1 : 0;
      }
      else
      {
        // A light which is off turns on if exactly 3 neighbors are on, and stays off otherwise.
        return (CountNeighboursOn(ref lights, x, y) == 3) ? 1 : 0;
      }
    }

    private int CountNeighboursOn(ref int[,] lights, int x, int y)
    {
      // don't care about myself !!
      int top__left = ((x - 1) < 0 || (y - 1) < 0) ? 0 : lights[x - 1, y - 1];
      int top_midle = ((y - 1) < 0) ? 0 : lights[x, y - 1];
      int top_right = ((x + 1) > 99 || (y - 1) < 0) ? 0 : lights[x + 1, y - 1]; 

      int mid__left = ((x - 1) < 0) ? 0 : lights[x - 1, y]; 
      int mid_right = ((x + 1) > 99) ? 0 : lights[x + 1, y];

      int bot__left = ((x - 1) < 0 || (y + 1) > 99) ? 0 : lights[x - 1, y + 1];
      int bot_midle = ((y + 1) > 99) ? 0 : lights[x, y + 1];
      int bot_right = ((x + 1) > 99|| (y + 1) > 99) ? 0 : lights[x + 1, y + 1];

      return (top__left + top_midle + top_right +
             mid__left + mid_right +
             bot__left + bot_midle + bot_right);
    }
  }
}
