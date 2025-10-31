using AdventOfCodeNet10.Extensions;
using Coordinate = (long x, long y);

namespace AdventOfCodeNet10._2023.Day_11
{
  internal class Part_2_2023_Day_11 : Days
  {
    /// <summary>
    /*
    https://adventofcode.com/2023/day/11
    --- Day 11: Cosmic Expansion ---
    You continue following signs for "Hot Springs" and eventually come across an
    observatory. The Elf within turns out to be a researcher studying cosmic
    expansion using the giant telescope here.
    
    He doesn't know anything about the missing machine parts; he's only visiting
    for this research project. However, he confirms that the hot springs are the
    next-closest area likely to have people; he'll even take you straight there
    once he's done with today's observation analysis.
    
    Maybe you can help him with the analysis to speed things up?
    
    The researcher has collected a bunch of data and compiled the data into a
    single giant image (your puzzle input). The image includes empty space (.) and
    galaxies (#). For example:
    
    ...#......
    .......#..
    #.........
    ..........
    ......#...
    .#........
    .........#
    ..........
    .......#..
    #...#.....
    The researcher is trying to figure out the sum of the lengths of the shortest
    path between every pair of galaxies. However, there's a catch: the universe
    expanded in the time it took the light from those galaxies to reach the
    observatory.
    
    Due to something involving gravitational effects, only some space expands. In
    fact, the result is that any rows or columns that contain no galaxies should
    all actually be twice as big.
    
    In the above example, three columns and two rows contain no galaxies:
    
    v  v  v
    ...#......
    .......#..
    #.........
    >..........<
    ......#...
    .#........
    .........#
    >..........<
    .......#..
    #...#.....
    ^  ^  ^
    These rows and columns need to be twice as big; the result of cosmic expansion
    therefore looks like this:
    
    ....#........
    .........#...
    #............
    .............
    .............
    ........#....
    .#...........
    ............#
    .............
    .............
    .........#...
    #....#.......
    Equipped with this expanded universe, the shortest path between every pair of
    galaxies can be found. It can help to assign every galaxy a unique number:
    
    ....1........
    .........2...
    3............
    .............
    .............
    ........4....
    .5...........
    ............6
    .............
    .............
    .........7...
    8....9.......
    In these 9 galaxies, there are 36 pairs. Only count each pair once; order
    within the pair doesn't matter. For each pair, find any shortest path between
    the two galaxies using only steps that move up, down, left, or right exactly
    one . or # at a time. (The shortest path between two galaxies is allowed to
    pass through another galaxy.)
    
    For example, here is one of the shortest paths between galaxies 5 and 9:
    
    ....1........
    .........2...
    3............
    .............
    .............
    ........4....
    .5...........
    .##.........6
    ..##.........
    ...##........
    ....##...7...
    8....9.......
    This path has length 9 because it takes a minimum of nine steps to get from
    galaxy 5 to galaxy 9 (the eight locations marked # plus the step onto galaxy 9
    itself). Here are some other example shortest path lengths:
    
    Between galaxy 1 and galaxy 7: 15
    Between galaxy 3 and galaxy 6: 17
    Between galaxy 8 and galaxy 9: 5
    In this example, after expanding the universe, the sum of the shortest path
    between all 36 pairs of galaxies is 374.
    
    Expand the universe, then find the length of the shortest path between every
    pair of galaxies. What is the sum of these lengths?
    
    Your puzzle answer was 9329143.
    
    The first half of this puzzle is complete! It provides one gold star: *
    
    --- Part Two ---
    The galaxies are much older (and thus much farther apart) than the researcher
    initially estimated.
    
    Now, instead of the expansion you did before, make each empty row or column one
    million times larger. That is, each empty row should be replaced with 1000000
    empty rows, and each empty column should be replaced with 1000000 empty columns.
    
    (In the example above, if each empty row or column were merely 10 times larger,
    the sum of the shortest paths between every pair of galaxies would be 1030. If
    each empty row or column were merely 100 times larger, the sum of the shortest
    paths between every pair of galaxies would be 8410. However, your universe will
    need to expand far beyond these values.)
    
    Starting with the same initial image, expand the universe according to these
    new rules, then find the length of the shortest path between every pair of
    galaxies. What is the sum of these lengths?
    */
    /// </summary>
    /// <returns>
    /// 82000210   // too low, wrong input text :-/ (example all worked !!)
    /// 2005303969 // too low, because I was using int instead of long !! F**K
    /// 710674907809
    /// </returns>
    public override string Execute()
    {
      // https://github.com/Rodbourn/adventofcode/blob/main/Day011.cs  
      //var res = Day011.Run(Lines.ToArray());
      //string part1 = res.part1;
      //return res.part2;

      string result = "";

      int width = Lines[0].Count();
      int height = Lines.Count();

      List<List<char>> space = new List<List<char>>();

      // map Space
      for (int y = 0; y < height; y++)
      {
        var row = new List<char>();
        for (int x = 0; x < width; x++)
        {
          row.Add(Lines[y][x]);
        }
        space.Add(row);
        if (!Lines[y].Contains('#')) // expand "horizontal" if no galaxy
        {
          horizontalMultipliers.Add(y);
        }
      }

      // Expand "vertical"
      for (int x = width - 1; x >= 0; x--)
      {
        if (Lines.All(l => l[x] == '.')) // expand "vertical" if no galaxy
        {
          vertical__Multipliers.Add(x);
        }
      }

      height = space.Count();
      width = space[0].Count();

      var galaxies = new List<LongPoint>();
      for (int y = 0; y < height; y++)
      {
        for (int x = 0; x < width; x++)
        {
          if (space[y][x] == '#') galaxies.Add(new LongPoint(x, y));
        }
      }

      long sum = CalcDistance(galaxies);

      #region Plot
      /*
      Debugger.Log(1, "", "\n");
      for (int y = 0; y < height; y++)
      {
        for (int x = 0; x < width; x++)
        {
          Debugger.Log(1, "", String.Format("{0} ", space[y][x].ToString()));
        }
        Debugger.Log(1, "", "\n");
      }
      */
      #endregion Plot

      result = sum.ToString();
      return result;
    }

    private int multiplier = 1000000;
    private List<int> horizontalMultipliers = new List<int>();
    private List<int> vertical__Multipliers = new List<int>();

    private long CalcDistance(List<LongPoint> galaxies)
    {
      long sum = 0;
      //Debugger.Log(1, "", "\n");
      for (int i = 1; i < galaxies.Count; i++)
      {
        long distance_x = Math.Max(galaxies[i].x, galaxies[0].x) - Math.Min(galaxies[i].x, galaxies[0].x);
        long gap_x = (from v in vertical__Multipliers where v > Math.Min(galaxies[i].x, galaxies[0].x) && v < Math.Max(galaxies[i].x, galaxies[0].x) select v).Count();
        distance_x = distance_x - gap_x + (gap_x * multiplier);

        long distance_y = Math.Max(galaxies[i].y, galaxies[0].y) - Math.Min(galaxies[i].y, galaxies[0].y);
        long gap_y = (from h in horizontalMultipliers where h > Math.Min(galaxies[i].y, galaxies[0].y) && h < Math.Max(galaxies[i].y, galaxies[0].y) select h).Count();
        distance_y = distance_y - gap_y + (gap_y * multiplier);

        //try
        //{
        //  checked
        //  {
            sum += distance_x + distance_y;
        //  }
        //}
        //catch (Exception e)
        //{
        //  Debugger.Break();
        //}
        //Debugger.Log(1, "", String.Format("{0}: Dis_x_y:'{1}:{2}', Point {3:00#}x{4:00#}, Point {5:00#}x{6:00#}\n", index++, distance_x, distance_y,
        //  galaxies[0].y, galaxies[0].x,
        //  galaxies[i].y, galaxies[i].x));
      }

      if (galaxies.Count > 2)
      {
        galaxies.RemoveAt(0);
        //checked
        //{
          sum += CalcDistance(galaxies);
        //}
      }
      return sum;
    }
  }


  internal class Day011
  {
    public static (string part1, string part2) Run(string[] input)
    {
      return (GetExpandedDistances(input, 2), GetExpandedDistances(input, 1000000));
    }
    public static string GetExpandedDistances(string[] lines, long expansionFactor)
    {
      //var lines = input.Split(Environment.NewLine, StringSplitOptions.TrimEntries);
      var galaxies = new List<Coordinate>();
      var galaxiesXIndex = new Dictionary<long, List<int>>();
      var galaxiesYIndex = new Dictionary<long, List<int>>();
      for (var j = 0; j < lines.Length; j++)
      {
        galaxiesYIndex[j] = [];
        for (var i = 0; i < lines[j].Length; i++)
        {
          if (j == 0)
            galaxiesXIndex[i] = [];

          switch (lines[j][i])
          {
            case '#':
              galaxies.Add((i, j));
              galaxiesXIndex[i].Add(galaxies.Count - 1);
              galaxiesYIndex[j].Add(galaxies.Count - 1);
              break;
          }
        }
      }

      ExpandGalaxy(galaxiesXIndex, expansionFactor - 1, true);
      ExpandGalaxy(galaxiesYIndex, expansionFactor - 1, false);

      var totalDistance = 0L;
      var galaxyArray = galaxies.ToArray();
      for (int i = 0; i < galaxyArray.Length; i++)
      {
        var (x1, y1) = galaxyArray[i];
        for (int j = i + 1; j < galaxyArray.Length; j++)
        {
          var (x2, y2) = galaxyArray[j];
          var dx = x1 - x2;
          var dy = y1 - y2;
          totalDistance += (dx >= 0 ? dx : -dx) + (dy >= 0 ? dy : -dy);
        }
      }

      return totalDistance.ToString();

      void ExpandGalaxy(Dictionary<long, List<int>> galaxyDimensionIndexes, long expansionScale, bool inX)
      {
        var expansionAmount = 0L;
        foreach (var (_, galaxyIndexes) in galaxyDimensionIndexes)
        {
          if (galaxyIndexes.Count == 0)
          {
            expansionAmount += expansionScale;
            continue;
          }

          foreach (var galaxyIndex in galaxyIndexes)
          {
            var c = (galaxies[galaxyIndex].x, galaxies[galaxyIndex].y);
            if (inX)
              c.x += expansionAmount;
            else
              c.y += expansionAmount;
            galaxies[galaxyIndex] = c;
          }
        }
      }
    }
  }
}

