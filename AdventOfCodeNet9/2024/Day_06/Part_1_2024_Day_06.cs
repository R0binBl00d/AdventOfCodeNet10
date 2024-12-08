using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;

namespace AdventOfCodeNet9._2024.Day_06
{
  internal class Part_1_2024_Day_06 : Days
  {
    /// <summary>
    /*
    https://adventofcode.com/2024/day/6
    --- Day 6: Guard Gallivant ---
    The Historians use their fancy device again, this time to whisk you all away to
    the North Pole prototype suit manufacturing lab... in the year 1518! It turns
    out that having direct access to history is very convenient for a group of
    historians.
    
    You still have to be careful of time paradoxes, and so it will be important to
    avoid anyone from 1518 while The Historians search for the Chief.
    Unfortunately, a single guard is patrolling this part of the lab.
    
    Maybe you can work out where the guard will go ahead of time so that The
    Historians can search safely?
    
    You start by making a map (your puzzle input) of the situation. For example:
    
    ....#.....
    .........#
    ..........
    ..#.......
    .......#..
    ..........
    .#..^.....
    ........#.
    #.........
    ......#...
    The map shows the current position of the guard with ^ (to indicate the guard
    is currently facing up from the perspective of the map). Any obstructions -
    crates, desks, alchemical reactors, etc. - are shown as #.
    
    Lab guards in 1518 follow a very strict patrol protocol which involves
    repeatedly following these steps:
    
    If there is something directly in front of you, turn right 90 degrees.
    Otherwise, take a step forward.
    Following the above protocol, the guard moves up several times until she
    reaches an obstacle (in this case, a pile of failed suit prototypes):
    
    ....#.....
    ....^....#
    ..........
    ..#.......
    .......#..
    ..........
    .#........
    ........#.
    #.........
    ......#...
    Because there is now an obstacle in front of the guard, she turns right before
    continuing straight in her new facing direction:
    
    ....#.....
    ........>#
    ..........
    ..#.......
    .......#..
    ..........
    .#........
    ........#.
    #.........
    ......#...
    Reaching another obstacle (a spool of several very long polymers), she turns
    right again and continues downward:
    
    ....#.....
    .........#
    ..........
    ..#.......
    .......#..
    ..........
    .#......v.
    ........#.
    #.........
    ......#...
    This process continues for a while, but the guard eventually leaves the mapped
    area (after walking past a tank of universal solvent):
    
    ....#.....
    .........#
    ..........
    ..#.......
    .......#..
    ..........
    .#........
    ........#.
    #.........
    ......#v..
    By predicting the guard's route, you can determine which specific positions in
    the lab will be in the patrol path. Including the guard's starting position,
    the positions visited by the guard before leaving the area are marked with an X:
    
    ....#.....
    ....XXXXX#
    ....X...X.
    ..#.X...X.
    ..XXXXX#X.
    ..X.X.X.X.
    .#XXXXXXX.
    .XXXXXXX#.
    #XXXXXXX..
    ......#X..
    In this example, the guard will visit 41 distinct positions on your map.
    
    Predict the path of the guard. How many distinct positions will the guard visit
    before leaving the mapped area?

    */
    /// </summary>
    /// <returns>
    /// 5409
    /// </returns>
    public override string Execute()
    {
      string result = "";
      int totalCount = 0;

      char[,] field = new char[Lines[0].Length, Lines.Count];
      char[,] movement = new char[Lines[0].Length, Lines.Count];

      Point currentLocation = new Point();
      //List<string> locationsVisited = new List<string>();
      List<List<int>> locationsVisited = new List<List<int>>();

      for (int y = 0; y < Lines.Count; y++)
      {
        locationsVisited.Add(new List<int>());
        for (int x = 0; x < Lines[0].Length; x++)
        {
          field[x, y] = Lines[y][x];
          movement[x, y] = Lines[y][x];

          if (field[x, y] == '^')
          {
            currentLocation.X = x;
            currentLocation.Y = y;
            field[x, y] = '.';
          }
          locationsVisited[y].Add(0);
        }
      }

      int orientation = 0;
      // 0 Top
      // 1 Right
      // 2 Bottom
      // 3 Left
      Point newlocation = new Point();
      do // move
      {
        locationsVisited[currentLocation.Y][currentLocation.X] = 1;

        switch (orientation)
        {
          case 0: // Top
            movement[currentLocation.X, currentLocation.Y] = '^';
            break;
          case 1: // Right
            movement[currentLocation.X, currentLocation.Y] = '>';
            break;
          case 2: // Bottom
            movement[currentLocation.X, currentLocation.Y] = 'v';
            break;
          case 3: // Left
            movement[currentLocation.X, currentLocation.Y] = '<';
            break;
        }

        newlocation = new Point(currentLocation.X, currentLocation.Y);

        switch (orientation)
        {
          case 0: // Top
            newlocation.Y--;
            break;
          case 1: // Right
            newlocation.X++;
            break;
          case 2: // Bottom
            newlocation.Y++;
            break;
          case 3: // Left
            newlocation.X--;
            break;
        }

        if (!NextLocationIsInside(newlocation))
        {
          break;
        }

        if (field[newlocation.X, newlocation.Y] == '.')
        {
          currentLocation.X = newlocation.X;
          currentLocation.Y = newlocation.Y;
          continue;
        }
        else if (field[newlocation.X, newlocation.Y] == '#')
        {
          orientation++;
          orientation %= 4;

          switch (orientation)
          {
            case 0: // Top
              currentLocation.Y--;
              break;
            case 1: // Right
              currentLocation.X++;
              break;
            case 2: // Bottom
              currentLocation.Y++;
              break;
            case 3: // Left
              currentLocation.X--;
              break;
          }
        }
        else
        {
          /*
          for (int y = 0; y < Lines.Count; y++)
          {
            Debug.Write("\n");
            for (int x = 0; x < Lines[0].Length; x++)
            {
              Debug.Write($"{movement[x, y]}");
            }
          }
          */
          Debugger.Break();
        }

      } while (NextLocationIsInside(currentLocation));

      foreach (List<int> yLists in locationsVisited)
      {
        foreach (var xitem in yLists)
        {
          totalCount += xitem;
        }
      }

      /*//   Debug-Plot of the map "correct Result plot" written below Day_06_Part_2

      for (int y = 0; y < Lines.Count; y++)
      {
        Debug.Write("\n");
        for (int x = 0; x < Lines[0].Length; x++)
        {
          Debug.Write($"{movement[x, y]}");
        }
      }
      Debug.Write("\n");
      Debug.Write("\n");
      Debug.Write("\n");

      for (int y = 0; y < Lines.Count; y++)
      {
        Debug.Write("\n");
        for (int x = 0; x < Lines[0].Length; x++)
        {
          Debug.Write($"{locationsVisited[y][x]}");
        }
      }
      */
      result = totalCount.ToString();
      return result;
    }

    private bool NextLocationIsInside(Point currentLocation)
    {
      if (
        currentLocation.X < 0 ||
        currentLocation.Y < 0 ||
        currentLocation.X == Lines[0].Length ||
        currentLocation.Y == Lines.Count
      )
      {
        return false;
      }
      return true;
    }
  }
}