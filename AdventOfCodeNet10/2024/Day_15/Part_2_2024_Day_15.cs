using AdventOfCodeNet10.Extensions;
using System.Diagnostics;
using System.Text;

namespace AdventOfCodeNet10._2024.Day_15
{
  internal class Part_2_2024_Day_15 : Days
  {
    /// <summary>
    /*
    https://adventofcode.com/2024/day/15
    --- Day 15: Warehouse Woes ---
    You appear back inside your own mini submarine! Each Historian drives their
    mini submarine in a different direction; maybe the Chief has his own submarine
    down here somewhere as well?
    
    You look up to see a vast school of lanternfish swimming past you. On closer
    inspection, they seem quite anxious, so you drive your mini submarine over to
    see if you can help.
    
    Because lanternfish populations grow rapidly, they need a lot of food, and that
    food needs to be stored somewhere. That's why these lanternfish have built
    elaborate warehouse complexes operated by robots!
    
    These lanternfish seem so anxious because they have lost control of the robot
    that operates one of their most important warehouses! It is currently running
    amok, pushing around boxes in the warehouse with no regard for lanternfish
    logistics or lanternfish inventory management strategies.
    
    Right now, none of the lanternfish are brave enough to swim up to an
    unpredictable robot so they could shut it off. However, if you could anticipate
    the robot's movements, maybe they could find a safe option.
    
    The lanternfish already have a map of the warehouse and a list of movements the
    robot will attempt to make (your puzzle input). The problem is that the
    movements will sometimes fail as boxes are shifted around, making the actual
    movements of the robot difficult to predict.
    
    For example:
    
    ##########
    #..O..O.O#
    #......O.#
    #.OO..O.O#
    #..O@..O.#
    #O#..O...#
    #O..O..O.#
    #.OO.O.OO#
    #....O...#
    ##########
    
    <vv>^<v^>v>^vv^v>v<>v^v<v<^vv<<<^><<><>>v<vvv<>^v^>^<<<><<v<<<v^vv^v>^
    vvv<<^>^v^^><<>>><>^<<><^vv^^<>vvv<>><^^v>^>vv<>v<<<<v<^v>^<^^>>>^<v<v
    ><>vv>v^v^<>><>>>><^^>vv>v<^^^>>v^v^<^^>v^^>v^<^v>v<>>v^v^<v>v^^<^^vv<
    <<v<^>>^^^^>>>v^<>vvv^><v<<<>^^^vv^<vvv>^>v<^^^^v<>^>vvvv><>>v^<<^^^^^
    ^><^><>>><>^^<<^^v>>><^<v>^<vv>>v>>>^v><>^v><<<<v>>v<v<v>vvv>^<><<>^><
    ^>><>^v<><^vvv<^^<><v<<<<<><^v<<<><<<^^<v<^^^><^>>^<v^><<<^>>^v<v^v<v^
    >^>>^v>vv>^<<^v<>><<><<v<<v><>v<^vv<<<>^^v^>^^>>><<^v>>v^v><^^>>^<>vv^
    <><^^>^^^<><vvvvv^v<v<<>^v<v>v<<^><<><<><<<^^<<<^<<>><<><^^^>^^<>^>v<>
    ^^>vv<^v^v<vv>^<><v<^v>^^^>>>^^vvv^>vvv<>>>^<^>>>>>^<<^v>^vvv<>^<><<v>
    v^^>>><<^^<>>^v^<v^vv<>v^<<>^<^v^v><^<<<><<^<v><v<>vv>>v><v^<vv<>v^<<^
    As the robot (@) attempts to move, if there are any boxes (O) in the way, the
    robot will also attempt to push those boxes. However, if this action would
    cause the robot or a box to move into a wall (#), nothing moves instead,
    including the robot. The initial positions of these are shown on the map at the
    top of the document the lanternfish gave you.
    
    The rest of the document describes the moves (^ for up, v for down, < for left,
    > for right) that the robot will attempt to make, in order. (The moves form a
    single giant sequence; they are broken into multiple lines just to make
    copy-pasting easier. Newlines within the move sequence should be ignored.)
    
    Here is a smaller example to get started:
    
    ########
    #..O.O.#
    ##@.O..#
    #...O..#
    #.#.O..#
    #...O..#
    #......#
    ########
    
    <^^>>>vv<v>>v<<
    Were the robot to attempt the given sequence of moves, it would push around the
    boxes as follows:
    
    Initial state:
    ########
    #..O.O.#
    ##@.O..#
    #...O..#
    #.#.O..#
    #...O..#
    #......#
    ########
    
    Move <:
    ########
    #..O.O.#
    ##@.O..#
    #...O..#
    #.#.O..#
    #...O..#
    #......#
    ########
    
    Move ^:
    ########
    #.@O.O.#
    ##..O..#
    #...O..#
    #.#.O..#
    #...O..#
    #......#
    ########
    
    Move ^:
    ########
    #.@O.O.#
    ##..O..#
    #...O..#
    #.#.O..#
    #...O..#
    #......#
    ########
    
    Move >:
    ########
    #..@OO.#
    ##..O..#
    #...O..#
    #.#.O..#
    #...O..#
    #......#
    ########
    
    Move >:
    ########
    #...@OO#
    ##..O..#
    #...O..#
    #.#.O..#
    #...O..#
    #......#
    ########
    
    Move >:
    ########
    #...@OO#
    ##..O..#
    #...O..#
    #.#.O..#
    #...O..#
    #......#
    ########
    
    Move v:
    ########
    #....OO#
    ##..@..#
    #...O..#
    #.#.O..#
    #...O..#
    #...O..#
    ########
    
    Move v:
    ########
    #....OO#
    ##..@..#
    #...O..#
    #.#.O..#
    #...O..#
    #...O..#
    ########
    
    Move <:
    ########
    #....OO#
    ##.@...#
    #...O..#
    #.#.O..#
    #...O..#
    #...O..#
    ########
    
    Move v:
    ########
    #....OO#
    ##.....#
    #..@O..#
    #.#.O..#
    #...O..#
    #...O..#
    ########
    
    Move >:
    ########
    #....OO#
    ##.....#
    #...@O.#
    #.#.O..#
    #...O..#
    #...O..#
    ########
    
    Move >:
    ########
    #....OO#
    ##.....#
    #....@O#
    #.#.O..#
    #...O..#
    #...O..#
    ########
    
    Move v:
    ########
    #....OO#
    ##.....#
    #.....O#
    #.#.O@.#
    #...O..#
    #...O..#
    ########
    
    Move <:
    ########
    #....OO#
    ##.....#
    #.....O#
    #.#O@..#
    #...O..#
    #...O..#
    ########
    
    Move <:
    ########
    #....OO#
    ##.....#
    #.....O#
    #.#O@..#
    #...O..#
    #...O..#
    ########
    The larger example has many more moves; after the robot has finished those
    moves, the warehouse would look like this:
    
    ##########
    #.O.O.OOO#
    #........#
    #OO......#
    #OO@.....#
    #O#.....O#
    #O.....OO#
    #O.....OO#
    #OO....OO#
    ##########
    The lanternfish use their own custom Goods Positioning System (GPS for short)
    to track the locations of the boxes. The GPS coordinate of a box is equal to
    100 times its distance from the top edge of the map plus its distance from the
    left edge of the map. (This process does not stop at wall tiles; measure all
    the way to the edges of the map.)
    
    So, the box shown below has a distance of 1 from the top edge of the map and 4
    from the left edge of the map, resulting in a GPS coordinate of 100 * 1 + 4 =
    104.
    
    #######
    #...O..
    #......
    The lanternfish would like to know the sum of all boxes' GPS coordinates after
    the robot finishes moving. In the larger example, the sum of all boxes' GPS
    coordinates is 10092. In the smaller example, the sum is 2028.
    
    Predict the motion of the robot and boxes in the warehouse. After the robot is
    finished moving, what is the sum of all boxes' GPS coordinates?
    
    Your puzzle answer was 1517819.
    
    --- Part Two ---
    The lanternfish use your information to find a safe moment to swim in and turn
    off the malfunctioning robot! Just as they start preparing a festival in your
    honor, reports start coming in that a second warehouse's robot is also
    malfunctioning.
    
    This warehouse's layout is surprisingly similar to the one you just helped.
    There is one key difference: everything except the robot is twice as wide! The
    robot's list of movements doesn't change.
    
    To get the wider warehouse's map, start with your original map and, for each
    tile, make the following changes:
    
    If the tile is #, the new map contains ## instead.
    If the tile is O, the new map contains [] instead.
    If the tile is ., the new map contains .. instead.
    If the tile is @, the new map contains @. instead.
    This will produce a new warehouse map which is twice as wide and with wide
    boxes that are represented by []. (The robot does not change size.)
    
    The larger example from before would now look like this:
    
    ####################
    ##....[]....[]..[]##
    ##............[]..##
    ##..[][]....[]..[]##
    ##....[]@.....[]..##
    ##[]##....[]......##
    ##[]....[]....[]..##
    ##..[][]..[]..[][]##
    ##........[]......##
    ####################
    Because boxes are now twice as wide but the robot is still the same size and
    speed, boxes can be aligned such that they directly push two other boxes at
    once. For example, consider this situation:
    
    #######
    #...#.#
    #.....#
    #..OO@#
    #..O..#
    #.....#
    #######
    
    <vv<<^^<<^^
    After appropriately resizing this map, the robot would push around these boxes
    as follows:
    
    Initial state:
    ##############
    ##......##..##
    ##..........##
    ##....[][]@.##
    ##....[]....##
    ##..........##
    ##############
    
    Move <:
    ##############
    ##......##..##
    ##..........##
    ##...[][]@..##
    ##....[]....##
    ##..........##
    ##############
    
    Move v:
    ##############
    ##......##..##
    ##..........##
    ##...[][]...##
    ##....[].@..##
    ##..........##
    ##############
    
    Move v:
    ##############
    ##......##..##
    ##..........##
    ##...[][]...##
    ##....[]....##
    ##.......@..##
    ##############
    
    Move <:
    ##############
    ##......##..##
    ##..........##
    ##...[][]...##
    ##....[]....##
    ##......@...##
    ##############
    
    Move <:
    ##############
    ##......##..##
    ##..........##
    ##...[][]...##
    ##....[]....##
    ##.....@....##
    ##############
    
    Move ^:
    ##############
    ##......##..##
    ##...[][]...##
    ##....[]....##
    ##.....@....##
    ##..........##
    ##############
    
    Move ^:
    ##############
    ##......##..##
    ##...[][]...##
    ##....[]....##
    ##.....@....##
    ##..........##
    ##############
    
    Move <:
    ##############
    ##......##..##
    ##...[][]...##
    ##....[]....##
    ##....@.....##
    ##..........##
    ##############
    
    Move <:
    ##############
    ##......##..##
    ##...[][]...##
    ##....[]....##
    ##...@......##
    ##..........##
    ##############
    
    Move ^:
    ##############
    ##......##..##
    ##...[][]...##
    ##...@[]....##
    ##..........##
    ##..........##
    ##############
    
    Move ^:
    ##############
    ##...[].##..##
    ##...@.[]...##
    ##....[]....##
    ##..........##
    ##..........##
    ##############
    This warehouse also uses GPS to locate the boxes. For these larger boxes,
    distances are measured from the edge of the map to the closest edge of the box
    in question. So, the box shown below has a distance of 1 from the top edge of
    the map and 5 from the left edge of the map, resulting in a GPS coordinate of
    100 * 1 + 5 = 105.
    
    ##########
    ##...[]...
    ##........
    In the scaled-up version of the larger example from above, after the robot has
    finished all of its moves, the warehouse would look like this:
    
    ####################
    ##[].......[].[][]##
    ##[]...........[].##
    ##[]........[][][]##
    ##[]......[]....[]##
    ##..##......[]....##
    ##..[]............##
    ##..@......[].[][]##
    ##......[][]..[]..##
    ####################
    The sum of these boxes' GPS coordinates is 9021.

    Predict the motion of the robot and boxes in this new, scaled-up warehouse. 
    What is the sum of all boxes' final GPS coordinates?

    Your puzzle answer was 1538862.
    */
    /// </summary>
    /// <returns>
    /// 1532928 (too low) // didn't consider a '.' on the lpile next_xl
    /// 1538862
    /// 9021
    /// </returns>
    public override string Execute()
    {
      string result = "";
      long totalCount = 0;

      var tempField = new List<char[]>();
      List<char> tempMovement = new List<char>();

      #region get Data

      foreach (var line in Lines)
      {
        if (line.Contains('-')) break;
        if (line.Contains('#'))
        {
          tempField.Add(line.ToCharArray());
        }
        else
        {
          tempMovement.AddRange(line.ToCharArray().ToList());
        }
      }

      #endregion get Data

      #region TestRun

      LongPoint currentLocation = new LongPoint(0, 0);

      char[,] field = new char[tempField[0].Length * 2, tempField.Count];

      for (int y = 0; y < tempField.Count; y++)
      {
        for (int x = 0; x < tempField[0].Length; x++)
        {
          char c = tempField[y][x];
          switch (c)
          {
            case '@':
              currentLocation.x = x * 2;
              currentLocation.y = y;
              field[2 * x, y] = '.';
              field[2 * x + 1, y] = '.';
              break;
            case '#':
              field[2 * x, y] = '#';
              field[2 * x + 1, y] = '#';
              break;
            case 'O':
              field[2 * x, y] = '[';
              field[2 * x + 1, y] = ']';
              break;
            case '.':
              field[2 * x, y] = '.';
              field[2 * x + 1, y] = '.';
              break;
          }
        }
      }

      //DebugDrawMap(' ', ref field, ref currentLocation);

      foreach (char c in tempMovement)
      {
        DoMoveInDirection(c, ref currentLocation, ref field);
        //DebugDrawMap(c, ref field, ref currentLocation);
      }

      //DebugDrawMap('F', ref field, ref currentLocation);

      long factor = 100;
      for (int y = 0; y < field.GetLength(1); y++)
      {
        for (int x = 0; x < field.GetLength(0); x++)
        {
          if (field[x, y] == '[')
          {
            totalCount += (factor * y) + x;
          }
        }
      }

      #endregion TestRun

      result += totalCount.ToString();
      return result;
    }

    private void DoMoveInDirection(char pointer, ref LongPoint cl, ref char[,] field)
    {
      StringBuilder path = new StringBuilder();
      long x, firstGap;

      switch (pointer) // ^ > v <
      {
        case '^':
          // get a substring from this position, find gap between me and wall, move
          if (field[cl.x, cl.y - 1] == '#') return;
          if (field[cl.x, cl.y - 1] == '.')
          {
            cl.y--;
            return;
          }

          // new path-structure for up

          boxPile uPile = new boxPile(field[cl.x, cl.y - 1], cl.x, cl.y - 1);
          if (uPile.CreateFullPileAndReturnHasSpace(pointer, ref field))
          {
            uPile.MoveFullPile(pointer, ref field);
            field[uPile.xl, uPile.y] = '.';
            field[uPile.xr, uPile.y] = '.';
            uPile.Clear();
            cl.y--;
            return;
          }
          break;

        #region case '>': // right

        case '>': // right
          // get a substring from this position, find gap between me and wall, move
          if (field[cl.x + 1, cl.y] == '.')
          {
            cl.x++;
            return;
          }

          for (x = cl.x + 1; x < field.GetLength(0); x++)
          {
            path.Append(field[x, cl.y]);
          }

          firstGap = GetFirstGap(path.ToString());
          if (firstGap > 0)
          {
            cl.x++;

            // move all of them
            do
              field[cl.x + firstGap--, cl.y] = field[cl.x + firstGap, cl.y];
            while (firstGap > 0);

            field[cl.x, cl.y] = '.';
          }

          break;

        #endregion case '>': // right

        case 'v':
          // get a substring from this position, find gap between me and wall, move
          if (field[cl.x, cl.y + 1] == '#') return;
          if (field[cl.x, cl.y + 1] == '.')
          {
            cl.y++;
            return;
          }

          // new path-structure for down

          boxPile dPile = new boxPile(field[cl.x, cl.y + 1], cl.x, cl.y + 1);
          if (dPile.CreateFullPileAndReturnHasSpace(pointer, ref field))
          {
            dPile.MoveFullPile(pointer, ref field);
            field[dPile.xl, dPile.y] = '.';
            field[dPile.xr, dPile.y] = '.';
            dPile.Clear();
            cl.y++;
            return;
          }
          break;

        #region case '<': // left

        case '<':
          // get a substring from this position, find gap between me and wall, move
          if (field[cl.x - 1, cl.y] == '.')
          {
            cl.x--;
            return;
          }

          for (x = cl.x - 1; x >= 0; x--)
          {
            path.Append(field[x, cl.y]);
          }

          firstGap = GetFirstGap(path.ToString());
          if (firstGap > 0)
          {
            cl.x--;

            // move all of them
            long offset = 0;
            do
              field[cl.x - firstGap + offset++, cl.y] = field[cl.x - firstGap + offset, cl.y];
            while (offset < firstGap);

            field[cl.x, cl.y] = '.';
          }
          break;

          #endregion case '<': // left
      }
    }

    internal class boxPile
    {
      public boxPile lPile { get; set; }
      public boxPile rPile { get; set; }

      public bool hasSpaceInFront { get; set; }
      public long y { get; set; }
      public long xl { get; init; }
      public long xr { get; init; }

      private LongPoint next_xl;
      private LongPoint next_xr;

      public boxPile(char boxEdge, long xc, long yc)
      {
        y = yc;
        switch (boxEdge)
        {
          case '[':
            xl = xc;
            xr = xc + 1;
            break;
          case ']':
            xl = xc - 1;
            xr = xc;
            break;
        }

        lPile = null;
        rPile = null;
      }

      public bool CreateFullPileAndReturnHasSpace(char dir, ref char[,] field)
      {
        switch (dir)
        {
          case '^':
            next_xl = new LongPoint(xl, y - 1);
            next_xr = new LongPoint(xr, y - 1);
            break;
          case 'v':
            next_xl = new LongPoint(xl, y + 1);
            next_xr = new LongPoint(xr, y + 1);
            break;
        }

        var cl = field[next_xl.x, next_xl.y];
        var cr = field[next_xr.x, next_xr.y];

        hasSpaceInFront = cl == '.' && cr == '.';
        if (hasSpaceInFront) return true;

        if (cl == '#' || cr == '#') return false;

        bool hasSpace = /*assume*/ true;
        if (cl != '.' && (cl != '[' || cl != ']'))
        {
          lPile = new boxPile(cl, next_xl.x, next_xl.y);
          hasSpace &= lPile.CreateFullPileAndReturnHasSpace(dir, ref field);
        }
        if (cr == '[')
        {
          rPile = new boxPile(cr, next_xr.x, next_xr.y);
          hasSpace &= rPile.CreateFullPileAndReturnHasSpace(dir, ref field);
        }
        return hasSpace;
      }

      public void MoveFullPile(char dir, ref char[,] field)
      {
        if (lPile != null) lPile.MoveFullPile(dir, ref field);
        if (rPile != null) rPile.MoveFullPile(dir, ref field);

        // multiple branches can contain the same items 
        if (field[next_xl.x, next_xl.y] == '.' && field[next_xr.x, next_xr.y] == '.')
        {
          // only move if it hasn't already
          field[next_xl.x, next_xl.y] = field[xl, y];
          field[next_xr.x, next_xr.y] = field[xr, y];
          field[xl, y] = '.';
          field[xr, y] = '.';
        }
      }

      public void Clear()
      {
        if (lPile != null) lPile.Clear();
        else lPile = null;
        if (rPile != null) rPile.Clear();
        else rPile = null;

        next_xl = null;
        next_xr = null;
      }
    }

    private long GetFirstGap(string path)
    {
      var strPath = path.Substring(0, path.IndexOf('#'));
      if (strPath == "" || strPath.IndexOf('.') < 0) return -1; // no movement
      long firstGap = strPath.IndexOf('.');
      Debug.Assert(firstGap > 0);
      return firstGap;
    }

    private void DebugDrawMap(char move, ref char[,] field, ref LongPoint currentLocation)
    {
      var s = new StringBuilder();

      for (int y = 0; y < field.GetLength(1); y++)
      {
        for (int x = 0; x < field.GetLength(0); x++)
        {
          if (currentLocation.x == x && currentLocation.y == y)
          {
            s.Append('@');
          }
          else
          {
            s.Append(field[x, y]);
          }
        }
        s.AppendLine("");
      }

      Debug.WriteLine("");
      Debug.Write("Move: " + move);
      Debug.WriteLine("");
      Debug.WriteLine(s);
    }
  }
}