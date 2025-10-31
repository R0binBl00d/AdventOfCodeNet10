using System.Diagnostics;
using AdventOfCodeNet10.Extensions;

namespace AdventOfCodeNet10._2024.Day_08
{
  internal class Part_2_2024_Day_08 : Days
  {
    /// <summary>
    /*
    https://adventofcode.com/2024/day/8
--- Day 8: Resonant Collinearity ---
You find yourselves on the roof of a top-secret Easter Bunny installation.

While The Historians do their thing, you take a look at the familiar huge antenna. Much to your surprise, it seems to have been reconfigured to emit a signal that makes people 0.1% more likely to buy Easter Bunny brand Imitation Mediocre Chocolate as a Christmas gift! Unthinkable!

Scanning across the city, you find that there are actually many such antennas. Each antenna is tuned to a specific frequency indicated by a single lowercase letter, uppercase letter, or digit. You create a map (your puzzle input) of these antennas. For example:

............
........0...
.....0......
.......0....
....0.......
......A.....
............
............
........A...
.........A..
............
............
The signal only applies its nefarious effect at specific antinodes based on the resonant frequencies of the antennas. In particular, an antinode occurs at any point that is perfectly in line with two antennas of the same frequency - but only when one of the antennas is twice as far away as the other. This means that for any pair of antennas with the same frequency, there are two antinodes, one on either side of them.

So, for these two antennas with frequency a, they create the two antinodes marked with #:

..........
...#......
..........
....a.....
..........
.....a....
..........
......#...
..........
..........
Adding a third antenna with the same frequency creates several more antinodes. It would ideally add four antinodes, but two are off the right side of the map, so instead it adds only two:

..........
...#......
#.........
....a.....
........a.
.....a....
..#.......
......#...
..........
..........
Antennas with different frequencies don't create antinodes; A and a count as different frequencies. However, antinodes can occur at locations that contain antennas. In this diagram, the lone antenna with frequency capital A creates no antinodes but has a lowercase-a-frequency antinode at its location:

..........
...#......
#.........
....a.....
........a.
.....a....
..#.......
......A...
..........
..........
The first example has antennas with two different frequencies, so the antinodes they create look like this, plus an antinode overlapping the topmost A-frequency antenna:

......#....#
...#....0...
....#0....#.
..#....0....
....0....#..
.#....A.....
...#........
#......#....
........A...
.........A..
..........#.
..........#.
Because the topmost A-frequency antenna overlaps with a 0-frequency antinode, there are 14 total unique locations that contain an antinode within the bounds of the map.

Calculate the impact of the signal. How many unique locations within the bounds of the map contain an antinode?

Your puzzle answer was 273.

--- Part Two ---
Watching over your shoulder as you work, one of The Historians asks if you took the effects of resonant harmonics into your calculations.

Whoops!

After updating your model, it turns out that an antinode occurs at any grid position exactly in line with at least two antennas of the same frequency, regardless of distance. This means that some of the new antinodes will occur at the position of each antenna (unless that antenna is the only one of its frequency).

So, these three T-frequency antennas now create many antinodes:

T....#....
...T......
.T....#...
.........#
..#.......
..........
...#......
..........
....#.....
..........
In fact, the three T-frequency antennas are all exactly in line with two antennas, so they are all also antinodes! This brings the total number of antinodes in the above example to 9.

The original example now has 34 antinodes, including the antinodes that appear on every antenna:

##....#....#
.#.#....0...
..#.#0....#.
..##...0....
....0....#..
.#...#A....#
...#..#.....
#....#.#....
..#.....A...
....#....A..
.#........#.
...#......##
Calculate the impact of the signal using this updated model. How many unique locations within the bounds of the map contain an antinode?

Your puzzle answer was 1017.
    */
    /// </summary>
    /// <returns>
    /// 273 -> part 1
    /// 1017
    /// </returns>
    public override string Execute()
    {
      string result = "";
      int totalCount = 0;

      char[,] field = new char[Lines[0].Length, Lines.Count];
      map = new char[Lines[0].Length, Lines.Count];
      int[,] antinodeLocations = new int[Lines[0].Length, Lines.Count];

      var towers = new Dictionary<char, List<(int x, int y)>>();

      for (int y = 0; y < Lines.Count; y++)
      {
        for (int x = 0; x < Lines[0].Length; x++)
        {
          char c = Lines[y][x];
          field[x, y] = c;
          map[x, y] = c;
          antinodeLocations[x, y] = 0;

          if (c != '.') // Add a tower
          {
            if (towers.ContainsKey(c)) towers[c].Add((x, y));
            else towers.Add(c, new List<(int x, int y)>() { (x, y) });
          }
        }
      }

      // Calculate all Antinodes for the towers
      //Parallel.ForEach(towers, tower => SetAntinodes(tower.Value[0], tower.Value[1..], ref antinodeLocations));

      foreach (var tower in towers)
      {
        if (tower.Value.Count > 1)
        {
          antinodeLocations[tower.Value[0].x, tower.Value[0].y] = 1;
          SetAntinodes(tower.Value[0], tower.Value[1..], ref antinodeLocations);
        }
        else
        {
          Debugger.Break();
        }
      }

      for (int y = 0; y < Lines.Count; y++)
      {
        Debug.WriteLine("");
        for (int x = 0; x < Lines[0].Length; x++)
        {
          Debug.Write(map[x, y]);

          totalCount += antinodeLocations[x, y];
        }
      }
      Debug.WriteLine("");
      Debug.WriteLine("");

      result = totalCount.ToString();
      return result;
    }

    private char[,]? map;

    private void SetAntinodes((int x, int y) reference, List<(int x, int y)> against, ref int[,] antinodeLocations)
    {
      foreach (var loc in against)
      {
        (int x, int y) distance = (reference.x - loc.x, reference.y - loc.y);

        (int x, int y) pos1 = ((reference.x + distance.x, reference.y + distance.y));
        while
        (
          pos1.x.InRange(0, antinodeLocations.GetLength(0), IncludeBounds.Lower) &&
          pos1.y.InRange(0, antinodeLocations.GetLength(1), IncludeBounds.Lower)
        )
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
          map[pos1.x, pos1.y] = 'X';
#pragma warning restore CS8602 // Dereference of a possibly null reference.
          antinodeLocations[pos1.x, pos1.y] = 1;

          pos1 = ((pos1.x + distance.x, pos1.y + distance.y));
        }

        (int x, int y) pos2 = ((loc.x - distance.x, loc.y - distance.y));
        while
        (
          pos2.x.InRange(0, antinodeLocations.GetLength(0), IncludeBounds.Lower) &&
          pos2.y.InRange(0, antinodeLocations.GetLength(1), IncludeBounds.Lower)
        )
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
          map[pos2.x, pos2.y] = 'X';
#pragma warning restore CS8602 // Dereference of a possibly null reference.
          antinodeLocations[pos2.x, pos2.y] = 1;

          pos2 = ((pos2.x - distance.x, pos2.y - distance.y));
        }
      }

      if (against.Count > 1)
      {
        antinodeLocations[against[0].x, against[0].y] = 1;
        SetAntinodes(against[0], against[1..], ref antinodeLocations);
      }
      else
      {
        // last tower !!
        antinodeLocations[against[0].x, against[0].y] = 1;
      }
    }
  }
}
