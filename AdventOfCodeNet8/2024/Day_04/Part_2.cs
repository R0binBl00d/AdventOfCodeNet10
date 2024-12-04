using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace AdventOfCodeNet8._2024.Day_04
{
  internal class Part_2 : Days
  {
    /// <summary>
    /*
    https://adventofcode.com/2024/day/4

    --- Part Two ---
    The Elf looks quizzically at you. Did you misunderstand the assignment?
    
    Looking for the instructions, you flip over the word search to find that this
    isn't actually an XMAS puzzle; it's an X-MAS puzzle in which you're supposed to
    find two MAS in the shape of an X. One way to achieve that is like this:
    
    M.S
    .A.
    M.S
    Irrelevant characters have again been replaced with . in the above diagram.
    Within the X, each MAS can be written forwards or backwards.
    
    Here's the same example from before, but this time all of the X-MASes have been
    kept instead:
    
    .M.S......
    ..A..MSMS.
    .M.S.MAA..
    ..A.ASMSM.
    .M.S.M....
    ..........
    S.S.S.S.S.
    .A.A.A.A..
    M.M.M.M.M.
    ..........
    In this example, an X-MAS appears 9 times.
    
    Flip the word search from the instructions back over to the word search side
    and try again. How many times does an X-MAS appear?
    

    */
    /// </summary>
    /// <returns>
    /// 2685 -> Part one
    /// 2048
    /// </returns>
    public override string Execute()
    {
      string result = "";
      int totalXMAS = 0;

      //char[,] field = new char[Lines[0].Length, Lines.Count];

      for (int y = 0; y < Lines.Count; y++)
      {
        for (int x = 0; x < Lines[y].Length; x++)
        {
          if (Lines[y][x] == 'A')
          {
            totalXMAS += Search_Term(y, x);
          }
        }
      }

      result = totalXMAS.ToString();
      return result;
    }

    private int Search_Term(int y, int x)
    {
      int foundForThisX;
      //search in all directions
      // case right
      //if (x < Lines[y].Length - 1) // for 10 chars, A need to be on 9 (= index 8)
      // case left
      //if (x > 0) // index 1 = 2nd character
      // case top
      //if (y > 0) // index 1 = 2nd row
      // case bottom
      //if (y < Lines.Count - 1) // for 10 rows, y need to be on 9 (= index 8)

      // all of the above need to apply always !!

      if
      (
        (x < Lines[y].Length - 1) && // for 10 chars, A need to be on 9 (= index 8)
        (x > 0) && // index 1 = 2nd character
        (y > 0) && // index 1 = 2nd row
        (y < Lines.Count - 1) // for 10 rows, y need to be on 9 (= index 8)
      )
      {
        foundForThisX = 0; // initialize
      }
      else
      {
        return 0;
      }
      // ###########################################################
      // Round the clock diagonals -- starting top right
      // right top, means I can read it to the right and the text ends top
      //
      // one of each diagonal has to be in the code !!
      //
      //     S
      //    A
      //   M 
      // case right top
      if
      (
        Lines[y + 1][x - 1] == 'M' &&
        Lines[y - 1][x + 1] == 'S'
      )
      {
        foundForThisX++;
      }
      //    M
      //   A
      //  S
      // case left bottom
      if
      (
        Lines[y - 1][x + 1] == 'M' &&
        Lines[y + 1][x - 1] == 'S'
      )
      {
        foundForThisX++;
      }

      // Check here half way !!
      if (foundForThisX == 1)
      {
        foundForThisX = 0; // re-initialize for next round.
      }
      else
      {
        return 0;
      }

      // other diagonal !!
      // M
      //  A
      //   S
      // case right bottom
      if
      (
        Lines[y - 1][x - 1] == 'M' &&
        Lines[y + 1][x + 1] == 'S'
      )
      {
        foundForThisX++;
      }
      // S
      //  A
      //   M
      // case left top
      if
      (
        Lines[y + 1][x + 1] == 'M' &&
        Lines[y - 1][x - 1] == 'S'
      )
      {
        foundForThisX++;
      }
      return foundForThisX;
    }
  }
}
