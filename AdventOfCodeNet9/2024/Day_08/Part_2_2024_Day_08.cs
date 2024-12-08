using System.Diagnostics;
using AdventOfCodeNet9.Extensions;

namespace AdventOfCodeNet9._2024.Day_08
{
  internal class Part_2_2024_Day_08 : Days
  {
    /// <summary>
    /*
    https://adventofcode.com/2024/day/8
    --- Part Two ---
    Watching over your shoulder as you work, one of The Historians asks if you took
    the effects of resonant harmonics into your calculations.
    
    Whoops!
    
    After updating your model, it turns out that an antinode occurs at any grid
    position exactly in line with at least two antennas of the same frequency,
    regardless of distance. This means that some of the new antinodes will occur at
    the position of each antenna (unless that antenna is the only one of its
    frequency).
    
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
    In fact, the three T-frequency antennas are all exactly in line with two
    antennas, so they are all also antinodes! This brings the total number of
    antinodes in the above example to 9.
    
    The original example now has 34 antinodes, including the antinodes that appear
    on every antenna:
    
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
    Calculate the impact of the signal using this updated model. How many unique
    locations within the bounds of the map contain an antinode?

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

    private char[,] map;

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
          map[pos1.x, pos1.y] = 'X';
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
          map[pos2.x, pos2.y] = 'X';
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
