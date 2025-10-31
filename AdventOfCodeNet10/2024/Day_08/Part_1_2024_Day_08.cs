using System.Diagnostics;
using System.Drawing.Text;
using System.Security.Cryptography.X509Certificates;
using AdventOfCodeNet10.Extensions;
using Microsoft.Win32.SafeHandles;

namespace AdventOfCodeNet10._2024.Day_08
{
  internal class Part_1_2024_Day_08 : Days
  {
    /// <summary>
    /*
    https://adventofcode.com/2024/day/8

    You find yourselves on the roof of a top-secret Easter Bunny installation.
    
    While The Historians do their thing, you take a look at the familiar huge
    antenna. Much to your surprise, it seems to have been reconfigured to emit a
    signal that makes people 0.1% more likely to buy Easter Bunny brand Imitation
    Mediocre Chocolate as a Christmas gift! Unthinkable!
    
    Scanning across the city, you find that there are actually many such antennas.
    Each antenna is tuned to a specific frequency indicated by a single lowercase
    letter, uppercase letter, or digit. You create a map (your puzzle input) of
    these antennas. For example:
    
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
    The signal only applies its nefarious effect at specific antinodes based on the
    resonant frequencies of the antennas. In particular, an antinode occurs at any
    point that is perfectly in line with two antennas of the same frequency - but
    only when one of the antennas is twice as far away as the other. This means
    that for any pair of antennas with the same frequency, there are two antinodes,
    one on either side of them.
    
    So, for these two antennas with frequency a, they create the two antinodes
    marked with #:
    
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
    Adding a third antenna with the same frequency creates several more antinodes.
    It would ideally add four antinodes, but two are off the right side of the map,
    so instead it adds only two:
    
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
    Antennas with different frequencies don't create antinodes; A and a count as
    different frequencies. However, antinodes can occur at locations that contain
    antennas. In this diagram, the lone antenna with frequency capital A creates no
    antinodes but has a lowercase-a-frequency antinode at its location:
    
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
    The first example has antennas with two different frequencies, so the antinodes
    they create look like this, plus an antinode overlapping the topmost
    A-frequency antenna:
    
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
    Because the topmost A-frequency antenna overlaps with a 0-frequency antinode,
    there are 14 total unique locations that contain an antinode within the bounds
    of the map.
    
    Calculate the impact of the signal. How many unique locations within the bounds
    of the map contain an antinode?

    */
    /// </summary>
    /// <returns>
    /// 273
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
        SetAntinodes(tower.Value[0], tower.Value[1..], ref antinodeLocations);
      }

      for (int y = 0; y < Lines.Count; y++)
      {
        //Debug.WriteLine("");
        for (int x = 0; x < Lines[0].Length; x++)
        {
          //Debug.Write(map[x, y]);

          totalCount += antinodeLocations[x, y];
        }
      }
      //Debug.WriteLine("");
      //Debug.WriteLine("");

      result = totalCount.ToString();
      return result;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    private char[,] map;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

    private void SetAntinodes((int x, int y) reference, List<(int x, int y)> against, ref int[,] antinodeLocations)
    {
      var positions = new List<(int x, int y)>(2);
      foreach (var loc in against)
      {
        (int x, int y) distance = (reference.x - loc.x, reference.y - loc.y);
        positions.Add((reference.x + distance.x, reference.y + distance.y));
        positions.Add((loc.x - distance.x, loc.y - distance.y));

        foreach (var position in positions)
        {
          if
          (
            position.x.InRange(0, antinodeLocations.GetLength(0), IncludeBounds.Lower) &&
            position.y.InRange(0, antinodeLocations.GetLength(1), IncludeBounds.Lower)
          )
          {
            map[position.x, position.y] = 'X';
            antinodeLocations[position.x, position.y] = 1;
          }
          //else
          //{
          //  Debugger.Break();
          //}
        }
        positions.Clear();
      }

      if (against.Count > 1)
      {
        SetAntinodes(against[0], against[1..], ref antinodeLocations);
      }
    }
  }

}
