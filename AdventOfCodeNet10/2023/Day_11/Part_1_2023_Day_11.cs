namespace AdventOfCodeNet10._2023.Day_11
{
  internal class Part_1_2023_Day_11 : Days
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
    */
    /// </summary>
    /// <returns>
    /// 9329143
    /// </returns>
    public override string Execute()
    {
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
          space.Add(row);
        }
      }

      // Expand "vertical"
      for (int x = width - 1; x >= 0; x--)
      {
        if (Lines.All(l => l[x] == '.')) // expand "vertical" if no galaxy
        {
          space.ForEach(w => w.Insert(x, '.'));
        }
      }

      height = space.Count();
      width = space[0].Count();

      var galaxies = new List<Point>();
      for (int y = 0; y < height; y++)
      {
        for (int x = 0; x < width; x++)
        {
          if (space[y][x] == '#') galaxies.Add(new Point(x, y));
        }
      }

      int sum = CalcDistance(galaxies);

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

    private int index = 1;

    private int CalcDistance(List<Point> galaxies)
    {
      int sum = 0;
      //Debugger.Log(1, "", "\n");
      for (int i = 1; i < galaxies.Count; i++)
      {
        int distance_x = Math.Max(galaxies[i].X, galaxies[0].X) - Math.Min(galaxies[i].X, galaxies[0].X);
        int distance_y = Math.Max(galaxies[i].Y, galaxies[0].Y) - Math.Min(galaxies[i].Y, galaxies[0].Y);
        sum += distance_x + distance_y;
        //Debugger.Log(1, "", String.Format("{0}: Dis_x_y:'{1}:{2}', Point {3:00#}x{4:00#}, Point {5:00#}x{6:00#}\n", index++, distance_x, distance_y,
        //  galaxies[0].Y, galaxies[0].X,
        //  galaxies[i].Y, galaxies[i].X));
      }

      if (galaxies.Count > 2)
      {
        galaxies.RemoveAt(0);
        sum += CalcDistance(galaxies);
      }
      return sum;
    }
  }
}
