namespace AdventOfCodeNet10._2023.Day_10
{
  internal class Part_2_2023_Day_10 : Days
  {
    /// <summary>
    /*
    https://adventofcode.com/2023/day/10
    --- Day 10: Pipe Maze ---
    You use the hang glider to ride the hot air from Desert Island all the way up
    to the floating metal island. This island is surprisingly cold and there
    definitely aren't any thermals to glide on, so you leave your hang glider
    behind.
    
    You wander around for a while, but you don't find any people or animals.
    However, you do occasionally find signposts labeled "Hot Springs" pointing in a
    seemingly consistent direction; maybe you can find someone at the hot springs
    and ask them where the desert-machine parts are made.
    
    The landscape here is alien; even the flowers and trees are made of metal. As
    you stop to admire some metal grass, you notice something metallic scurry away
    in your peripheral vision and jump into a big pipe! It didn't look like any
    animal you've ever seen; if you want a better look, you'll need to get ahead of
    it.
    
    Scanning the area, you discover that the entire field you're standing on is
    densely packed with pipes; it was hard to tell at first because they're the
    same metallic silver color as the "ground". You make a quick sketch of all of
    the surface pipes you can see (your puzzle input).
    
    The pipes are arranged in a two-dimensional grid of tiles:
    
    | is a vertical pipe connecting north and south.
    - is a horizontal pipe connecting east and west.
    L is a 90-degree bend connecting north and east.
    J is a 90-degree bend connecting north and west.
    7 is a 90-degree bend connecting south and west.
    F is a 90-degree bend connecting south and east.
    . is ground; there is no pipe in this tile.
    S is the starting position of the animal; there is a pipe on this tile, but
    your sketch doesn't show what shape the pipe has.
    Based on the acoustics of the animal's scurrying, you're confident the pipe
    that contains the animal is one large, continuous loop.
    
    For example, here is a square loop of pipe:
    
    .....
    .F-7.
    .|.|.
    .L-J.
    .....
    If the animal had entered this loop in the northwest corner, the sketch would
    instead look like this:
    
    .....
    .S-7.
    .|.|.
    .L-J.
    .....
    In the above diagram, the S tile is still a 90-degree F bend: you can tell
    because of how the adjacent pipes connect to it.
    
    Unfortunately, there are also many pipes that aren't connected to the loop!
    This sketch shows the same loop as above:
    
    -L|F7
    7S-7|
    L|7||
    -L-J|
    L|-JF
    In the above diagram, you can still figure out which pipes form the main loop:
    they're the ones connected to S, pipes those pipes connect to, pipes those
    pipes connect to, and so on. Every pipe in the main loop connects to its two
    neighbors (including S, which will have exactly two pipes connecting to it, and
    which is assumed to connect back to those two pipes).
    
    Here is a sketch that contains a slightly more complex main loop:
    
    ..F7.
    .FJ|.
    SJ.L7
    |F--J
    LJ...
    Here's the same example sketch with the extra, non-main-loop pipe tiles also
    shown:
    
    7-F7-
    .FJ|7
    SJLL7
    |F--J
    LJ.LJ
    If you want to get out ahead of the animal, you should find the tile in the
    loop that is farthest from the starting position. Because the animal is in the
    pipe, it doesn't make sense to measure this by direct distance. Instead, you
    need to find the tile that would take the longest number of steps along the
    loop to reach from the starting point - regardless of which way around the loop
    the animal went.
    
    In the first example with the square loop:
    
    .....
    .S-7.
    .|.|.
    .L-J.
    .....
    You can count the distance each tile in the loop is from the starting point
    like this:
    
    .....
    .012.
    .1.3.
    .234.
    .....
    In this example, the farthest point from the start is 4 steps away.
    
    Here's the more complex loop again:
    
    ..F7.
    .FJ|.
    SJ.L7
    |F--J
    LJ...
    Here are the distances for each tile on that loop:
    
    ..45.
    .236.
    01.78
    14567
    23...
    Find the single giant loop starting at S. How many steps along the loop does it
    take to get from the starting position to the point farthest from the starting
    position?
    
    Your puzzle answer was 7005.
    
    The first half of this puzzle is complete! It provides one gold star: *
    
    --- Part Two ---
    You quickly reach the farthest point of the loop, but the animal never emerges.
    Maybe its nest is within the area enclosed by the loop?
    
    To determine whether it's even worth taking the time to search for such a nest,
    you should calculate how many tiles are contained within the loop. For example:
    
    ...........
    .S-------7.
    .|F-----7|.
    .||.....||.
    .||.....||.
    .|L-7.F-J|.
    .|..|.|..|.
    .L--J.L--J.
    ...........
    The above loop encloses merely four tiles - the two pairs of . in the southwest
    and southeast (marked I below). The middle . tiles (marked O below) are not in
    the loop. Here is the same loop again with those regions marked:
    
    ...........
    .S-------7.
    .|F-----7|.
    .||OOOOO||.
    .||OOOOO||.
    .|L-7OF-J|.
    .|II|O|II|.
    .L--JOL--J.
    .....O.....
    In fact, there doesn't even need to be a full tile path to the outside for
    tiles to count as outside the loop - squeezing between pipes is also allowed!
    Here, I is still within the loop and O is still outside the loop:
    
    ..........
    .S------7.
    .|F----7|.
    .||OOOO||.
    .||OOOO||.
    .|L-7F-J|.
    .|II||II|.
    .L--JL--J.
    ..........
    In both of the above examples, 4 tiles are enclosed by the loop.
    
    Here's a larger example:
    
    .F----7F7F7F7F-7....
    .|F--7||||||||FJ....
    .||.FJ||||||||L7....
    FJL7L7LJLJ||LJ.L-7..
    L--J.L7...LJS7F-7L7.
    ....F-J..F7FJ|L7L7L7
    ....L7.F7||L7|.L7L7|
    .....|FJLJ|FJ|F7|.LJ
    ....FJL-7.||.||||...
    ....L---J.LJ.LJLJ...
    The above sketch has many random bits of ground, some of which are in the loop
    (I) and some of which are outside it (O):
    
    OF----7F7F7F7F-7OOOO
    O|F--7||||||||FJOOOO
    O||OFJ||||||||L7OOOO
    FJL7L7LJLJ||LJIL-7OO
    L--JOL7IIILJS7F-7L7O
    OOOOF-JIIF7FJ|L7L7L7
    OOOOL7IF7||L7|IL7L7|
    OOOOO|FJLJ|FJ|F7|OLJ
    OOOOFJL-7O||O||||OOO
    OOOOL---JOLJOLJLJOOO
    In this larger example, 8 tiles are enclosed by the loop.
    
    Any tile that isn't part of the main loop can count as being enclosed by the
    loop. Here's another example with many bits of junk pipe lying around that
    aren't connected to the main loop at all:
    
    FF7FSF7F7F7F7F7F---7
    L|LJ||||||||||||F--J
    FL-7LJLJ||||||LJL-77
    F--JF--7||LJLJ7F7FJ-
    L---JF-JLJ.||-FJLJJ7
    |F|F-JF---7F7-L7L|7|
    |FFJF7L7F-JF7|JL---7
    7-L-JL7||F7|L7F-7F7|
    L.L7LFJ|||||FJL7||LJ
    L7JLJL-JLJLJL--JLJ.L
    Here are just the tiles that are enclosed by the loop marked with I:
    
    FF7FSF7F7F7F7F7F---7
    L|LJ||||||||||||F--J
    FL-7LJLJ||||||LJL-77
    F--JF--7||LJLJIF7FJ-
    L---JF-JLJIIIIFJLJJ7
    |F|F-JF---7IIIL7L|7|
    |FFJF7L7F-JF7IIL---7
    7-L-JL7||F7|L7F-7F7|
    L.L7LFJ|||||FJL7||LJ
    L7JLJL-JLJLJL--JLJ.L
    In this last example, 10 tiles are enclosed by the loop.
    
    Figure out whether you have time to search for the nest by calculating the area
    within the loop. How many tiles are enclosed by the loop?
    */
    /// </summary>
    /// <returns>
    /// 417
    /// </returns>
    public override string Execute()
    {
      string result = "";
      int width = Lines[0].Length;
      int height = Lines.Count;
      input = new char[width, height]; // X, Y
      cPath = new char[width, height]; // X, Y
      iPath = new int[width, height]; // X, Y

      Point start = new Point(int.MinValue, int.MinValue);
      Point[] pathLocation = new Point[2];

      for (int y = 0; y < width; y++)
      {
        for (int x = 0; x < height; x++)
        {
          char c = Lines[y][x];
          input[x, y] = c;
          if (c == 'S')
          {
            start.X = x;
            start.Y = y;
            cPath[x, y] = '0';
            iPath[x, y] = 0;
          }
          else
          {
            cPath[x, y] = '.';
            iPath[x, y] = int.MinValue;
          }
        }
      }

      Bitmap enclosedPath = new Bitmap(width, height);
      Graphics g = Graphics.FromImage(enclosedPath);
      g.FillRectangles(Brushes.White, new Rectangle[] { new Rectangle(0, 0, width, height) });

      List<Point> polyPoints = new List<Point>();
      polyPoints.Add(start);

      // search first segments (paths)
      Headings[] heading = GetPathsFirstStep(start, ref pathLocation, width, height);

      int step = 2;
      while (!pathLocation[0].Equals(start))
      {
        polyPoints.Add(pathLocation[0]);
        GoStep(ref pathLocation[0], step, ref heading[0], width, height);
        step++;
      }

      g.FillPolygon(Brushes.Black, polyPoints.ToArray());
      g.DrawPolygon(Pens.Black, polyPoints.ToArray());
      g.Flush();
      //enclosedPath.Save(@"c:\Temp\enclosedPath.bmp");

      int encolsedTiles = 0;
      for (int y = 0; y < height; y++)
      {
        for (int x = 0; x < width; x++)
        {
          if(enclosedPath.GetPixel(x, y).ToArgb() == Color.Black.ToArgb())
          {
            // in enclosure
            if (cPath[x,y] == '.')
            {
              encolsedTiles++;
              enclosedPath.SetPixel(x, y, Color.Red);
            }
          }
        }
      }
      //enclosedPath.Save(@"c:\Temp\enclosedTiles.bmp");

      #region print map
      //Debugger.Log(1, "", "\n");
      //for (int y = 0; y < height; y++)
      //{
      //  Debugger.Log(1, "", "\n");
      //  for (int x = 0; x < width; x++)
      //  {
      //    Debugger.Log(1, "", String.Format("{0} ", cPath[x, y]));
      //  }
      //}
      //Debugger.Log(1, "", "\n\n");
      #endregion print map

      result = encolsedTiles.ToString();
      return result;
    }

    private char[,] input;
    private char[,] cPath;
    private int[,] iPath;

    private void GoStep(ref Point pathLocation, int step, ref Headings heading, int width, int height)
    {
      char c;

      switch (heading)
      {
        case Headings.north: // optionsConnectToTileFromNorth
          c = input[pathLocation.X, pathLocation.Y - 1];
          pathLocation.Y--;
          switch (c)
          {
            case '|': heading = Headings.north; break;
            case '7': heading = Headings.west; break;
            case 'F': heading = Headings.east; break;
          }
          break;
        case Headings.east:  // optionsConnectToTileFromEast
          c = input[pathLocation.X + 1, pathLocation.Y];
          pathLocation.X++;
          switch (c)
          {
            case '-': heading = Headings.east; break;
            case 'J': heading = Headings.north; break;
            case '7': heading = Headings.south; break;
          }
          break;
        case Headings.south: // optionsConnectToTileFromSouth
          c = input[pathLocation.X, pathLocation.Y + 1];
          pathLocation.Y++;
          switch (c)
          {
            case '|': heading = Headings.south; break;
            case 'L': heading = Headings.east; break;
            case 'J': heading = Headings.west; break;
          }
          break;
        case Headings.west: // optionsConnectToTileFromWest
          c = input[pathLocation.X - 1, pathLocation.Y];
          pathLocation.X--;
          switch (c)
          {
            case '-': heading = Headings.west; break;
            case 'L': heading = Headings.north; break;
            case 'F': heading = Headings.south; break;
          }
          break;
      }

      iPath[pathLocation.X, pathLocation.Y] = step;

      int seqStep = step % 36;
      char charInt = seqStep < 10 ? seqStep.ToString()[0] : (char)(seqStep - 10 + (int)'a');
      cPath[pathLocation.X, pathLocation.Y] = charInt;
    }

    private List<char> optionsConnectToTileFromNorth = new List<char>()
            {
              '|', // is a vertical pipe  connecting north and south.
              '7', // is a 90-degree bend connecting south and west.
              'F'  // is a 90-degree bend connecting south and east.
            };

    private List<char> optionsConnectToTileFromEast = new List<char>()
            {
              '-', // is a horizontal pipe connecting east and west.
              'J', // is a 90-degree bend connecting north and west
              '7'  // is a 90-degree bend connecting south and west.
            };

    private List<char> optionsConnectToTileFromSouth = new List<char>()
            {
              '|', // is a vertical pipe  connecting north and south.
              'L', // is a 90-degree bend connecting north and east
              'J'  // is a 90-degree bend connecting north and west.
            };

    private List<char> optionsConnectToTileFromWest = new List<char>()
            {
              '-', // is a horizontal pipe connecting east and west.
              'L', // is a 90-degree bend connecting north and east.
              'F'  // is a 90-degree bend connecting south and east.
            };

    private Headings[] GetPathsFirstStep(Point start, ref Point[] pathLocation, int width, int height)
    {
      // create surrounding
      int startLine = start.Y - 1;
      int endLine = start.Y + 1;
      int startColumn = start.X - 1;
      int endColumn = start.X + 1;

      Headings[] heading = new Headings[2];
      int index = 0;

      for (int y = startLine; y <= endLine; y++)
      {
        if (y < 0 || y >= height) continue;

        for (int x = startColumn; x <= endColumn; x++)
        {
          if (x < 0 || x >= width) continue;

          char c = input[x, y];

          // check north, east, south, west
          if (y == start.Y - 1 && x == start.X) // north
          {
            if (optionsConnectToTileFromNorth.Contains(c))
            {
              pathLocation[index] = new Point(x, y);
              switch (c)
              {
                case '|': heading[index] = Headings.north; break;
                case '7': heading[index] = Headings.west; break;
                case 'F': heading[index] = Headings.east; break;
              }
              index++;
            }
          }
          else if (y == start.Y && x == start.X + 1) // east
          {
            if (optionsConnectToTileFromEast.Contains(c))
            {
              pathLocation[index] = new Point(x, y);
              switch (c)
              {
                case '-': heading[index] = Headings.east; break;
                case 'J': heading[index] = Headings.north; break;
                case '7': heading[index] = Headings.south; break;
              }
              index++;
            }
          }
          else if (y == start.Y + 1 && x == start.X) // south
          {
            if (optionsConnectToTileFromSouth.Contains(c))
            {
              pathLocation[index] = new Point(x, y);
              switch (c)
              {
                case '|': heading[index] = Headings.south; break;
                case 'L': heading[index] = Headings.east; break;
                case 'J': heading[index] = Headings.west; break;
              }
              index++;
            }
          }
          else if (y == start.Y && x == start.X - 1) // west
          {
            if (optionsConnectToTileFromWest.Contains(c))
            {
              pathLocation[index] = new Point(x, y);
              switch (c)
              {
                case '-': heading[index] = Headings.west; break;
                case 'L': heading[index] = Headings.north; break;
                case 'F': heading[index] = Headings.south; break;
              }
              index++;
            }
          }
          else continue;
        }
      }

      iPath[pathLocation[0].X, pathLocation[0].Y] = 1;
      iPath[pathLocation[1].X, pathLocation[1].Y] = 1;

      cPath[pathLocation[0].X, pathLocation[0].Y] = '1';
      cPath[pathLocation[1].X, pathLocation[1].Y] = '1';
      return heading;
    }

    private enum Headings
    {
      north, east,
      south, west
    }
  }
}
