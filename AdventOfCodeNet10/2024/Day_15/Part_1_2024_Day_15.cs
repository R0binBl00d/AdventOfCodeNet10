using System.Diagnostics;
using System.IO;
using System.Text;

namespace AdventOfCodeNet10._2024.Day_15
{
  internal class Part_1_2024_Day_15 : Days
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

    */
    /// </summary>
    /// <returns>
    /// (2028)  Test1
    /// (10092) Test2
    /// 1517819
    /// </returns>
    public override string Execute()
    {
      string result = "";
      long totalCount = 0;

      var tempField = new List<char[]>();
      List<char> tempMovement = new List<char>();

      #region get Data
      int section = 0;
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

      point currentLocation = new point(0, 0);

      char[,] field = new char[tempField[0].Length, tempField.Count];

      for (int y = 0; y < tempField.Count; y++)
      {
        for (int x = 0; x < tempField[0].Length; x++)
        {
          char c = tempField[y][x];
          if (c == '@')
          {
            currentLocation.x = x;
            currentLocation.y = y;
            c = '.';
          }
          field[x, y] = c;
        }
      }

      //DebugDrawMap(ref field, ref currentLocation);
      foreach (char c in tempMovement)
      {
        DoMoveInDirection(c, ref currentLocation, ref field);
        //DebugDrawMap(ref field, ref currentLocation);
      }
      //DebugDrawMap(ref field, ref currentLocation);

      long factor = 100;
      for (int y = 0; y < field.GetLength(1); y++)
      {
        for (int x = 0; x < field.GetLength(0); x++)
        {
          if (field[x, y] == 'O')
          {
            totalCount += (factor * y) + x;
          }
        }
      }

      #endregion TestRun

      result += totalCount.ToString();
      return result;
    }

    private void DoMoveInDirection(char pointer, ref point cl, ref char[,] field)
    {
      StringBuilder path = new StringBuilder();
      long x, y, firstGap;

      switch (pointer) // ^ > v <
      {
        case '^':
          // get a substring from this position, find gap between me and wall, move
          if (field[cl.x, cl.y - 1] == '.')
          {
            cl.y--;
            return;
          }
          for (y = cl.y - 1; y >= 0; y--)
          {
            path.Append(field[cl.x, y]);
          }
          firstGap = GetFirstGap(path.ToString());
          if (firstGap > 0)
          {
            cl.y--;
            field[cl.x, cl.y] = '.';
            field[cl.x, cl.y - firstGap] = 'O';
          }
          break;
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
            field[cl.x, cl.y] = '.';
            field[cl.x + firstGap, cl.y] = 'O';
          }
          break;
        case 'v':
          // get a substring from this position, find gap between me and wall, move
          if (field[cl.x, cl.y + 1] == '.')
          {
            cl.y++;
            return;
          }
          for (y = cl.y + 1; y < field.GetLength(1); y++)
          {
            path.Append(field[cl.x, y]);
          }
          firstGap = GetFirstGap(path.ToString());
          if (firstGap > 0)
          {
            cl.y++;
            field[cl.x, cl.y] = '.';
            field[cl.x, cl.y + firstGap] = 'O';
          }
          break;
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
            field[cl.x, cl.y] = '.';
            field[cl.x - firstGap, cl.y] = 'O';
          }
          break;
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

    private void DebugDrawMap(ref char[,] field, ref point currentLocation)
    {
      Debug.WriteLine("");
      Debug.WriteLine("");
      for (int y = 0; y < field.GetLength(1); y++)
      {
        for (int x = 0; x < field.GetLength(0); x++)
        {
          if (currentLocation.x == x && currentLocation.y == y)
          {
            Debug.Write('@');
          }
          else
          {
            Debug.Write(field[x, y]);
          }
        }
        Debug.WriteLine("");
      }

      Debug.WriteLine("");
      Debug.WriteLine("");
    }
  }

  class point
  {
    public point(long X, long Y, int obj = 0)
    {
      this.x = X;
      this.y = Y;
      this.tag = obj;
    }

    public long x { get; set; }
    public long y { get; set; }
    public int tag { get; set; }

    public void Deconstruct(out long X, out long Y)
    {
      X = this.x;
      Y = this.y;
    }

    public override bool Equals(object obj)
    {
      point other = obj as point;
      return other.x == this.x && other.y == this.y;
    }

    public override int GetHashCode()
    {
      return this.x.GetHashCode() ^ this.y.GetHashCode();
    }
  }
}
