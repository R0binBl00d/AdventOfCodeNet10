using System.Diagnostics;
using System.Text;

namespace AdventOfCodeNet10._2024.Day_15
{
  internal class Part_2_2024_Day_15 : Days
  {
    /// <summary>
    /*
    https://adventofcode.com/2024/day/15
    */
    /// </summary>
    /// <returns>
    /// 1532928 (too low) // didn't consider a '.' on the lpile next_xl
    /// 1538862
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

    private void DoMoveInDirection(char pointer, ref point cl, ref char[,] field)
    {
      StringBuilder path = new StringBuilder();
      long x, y, firstGap;

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

      private point next_xl;
      private point next_xr;

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
            next_xl = new point(xl, y - 1);
            next_xr = new point(xr, y - 1);
            break;
          case 'v':
            next_xl = new point(xl, y + 1);
            next_xr = new point(xr, y + 1);
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

    private void DebugDrawMap(char move, ref char[,] field, ref point currentLocation)
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