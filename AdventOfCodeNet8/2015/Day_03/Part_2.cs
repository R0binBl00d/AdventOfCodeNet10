using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace AdventOfCodeNet8._2015.Day_03
{
  internal class Part_2 : Days
  {
    /// <summary>
    /*
https://adventofcode.com/2015/day/3
--- Day 3: Perfectly Spherical Houses in a Vacuum ---
Santa is delivering presents to an infinite two-dimensional grid of houses.

He begins by delivering a present to the house at his starting location, and then an elf at the North Pole calls him via radio and tells him where to move next. Moves are always exactly one house to the north (^), south (v), east (>), or west (<). After each move, he delivers another present to the house at his new location.

However, the elf back at the north pole has had a little too much eggnog, and so his directions are a little off, and Santa ends up visiting some houses more than once. How many houses receive at least one present?

For example:

> delivers presents to 2 houses: one at the starting location, and one to the east.
^>v< delivers presents to 4 houses in a square, including twice to the house at his starting/ending location.
^v^v^v^v^v delivers a bunch of presents to some very lucky children at only 2 houses.
Your puzzle answer was 2572.

--- Part Two ---
The next year, to speed up the process, Santa creates a robot version of himself, Robo-Santa, to deliver presents with him.

Santa and Robo-Santa start at the same location (delivering two presents to the same starting house), then take turns moving based on instructions from the elf, who is eggnoggedly reading from the same script as the previous year.

This year, how many houses receive at least one present?

For example:

^v delivers presents to 3 houses, because Santa goes north, and then Robo-Santa goes south.
^>v< now delivers presents to 3 houses, and Santa and Robo-Santa end up back where they started.
^v^v^v^v^v now delivers presents to 11 houses, with Santa going one direction and Robo-Santa going the other.     */
    /// </summary>
    /// <returns>
    /// Your puzzle answer was 2631.
    /// </returns>
    public override string Execute()
    {
      string result = "";

      int total = 0;

      Dictionary<Point, int> houses = new Dictionary<Point, int>();

      Point position_Santa = new Point(0, 0);
      Point position_Robot = new Point(0, 0);

      // Start delivering to the first house he's on
      houses.Add(position_Santa, 2);
      bool toggle = false;

      foreach (char c in Lines[0])
      {
        // v<> ^

        toggle = !toggle;
        switch (c)
        {
          case '^': // North
            if (toggle) position_Santa.Y--;
            else position_Robot.Y--;
            break;
          case 'v': // South
            if (toggle) position_Santa.Y++;
            else position_Robot.Y++;
            break;
          case '<': // West
            if (toggle) position_Santa.X--;
            else position_Robot.X--;
            break;
          case '>': // East
            if (toggle) position_Santa.X++;
            else position_Robot.X++;
            break;
        }

        if (toggle)
        {
          var house =
          from p in houses.Keys
          where p.X == position_Santa.X && p.Y == position_Santa.Y
          select p;

          if (house.Count() == 0) houses.Add(position_Santa, 1);
          else
          {
            houses[position_Santa]++;
          }
        }
        else
        {
          var house =
            from p in houses.Keys
            where p.X == position_Robot.X && p.Y == position_Robot.Y
            select p;

          if (house.Count() == 0) houses.Add(position_Robot, 1);
          else
          {
            houses[position_Robot]++;
          }
        }
      }

      total = houses.Count();

      result = total.ToString();
      return result;
    }
  }
}
