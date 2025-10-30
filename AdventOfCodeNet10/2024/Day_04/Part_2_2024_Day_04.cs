namespace AdventOfCodeNet10._2024.Day_04
{
  internal class Part_2_2024_Day_04 : Days
  {
    /// <summary>
    /*
    https://adventofcode.com/2024/day/4
--- Day 4: Ceres Search ---
"Looks like the Chief's not here. Next!" One of The Historians pulls out a device and pushes the only button on it. After a brief flash, you recognize the interior of the Ceres monitoring station!

As the search for the Chief continues, a small Elf who lives on the station tugs on your shirt; she'd like to know if you could help her with her word search (your puzzle input). She only has to find one word: XMAS.

This word search allows words to be horizontal, vertical, diagonal, written backwards, or even overlapping other words. It's a little unusual, though, as you don't merely need to find one instance of XMAS - you need to find all of them. Here are a few ways XMAS might appear, where irrelevant characters have been replaced with .:


..X...
.SAMX.
.A..A.
XMAS.S
.X....
The actual word search will be full of letters instead. For example:

MMMSXXMASM
MSAMXMSMSA
AMXSXMAAMM
MSAMASMSMX
XMASAMXAMM
XXAMMXXAMA
SMSMSASXSS
SAXAMASAAA
MAMMMXMMMM
MXMXAXMASX
In this word search, XMAS occurs a total of 18 times; here's the same word search again, but where letters not involved in any XMAS have been replaced with .:

....XXMAS.
.SAMXMS...
...S..A...
..A.A.MS.X
XMASAMX.MM
X.....XA.A
S.S.S.S.SS
.A.A.A.A.A
..M.M.M.MM
.X.X.XMASX
Take a look at the little Elf's word search. How many times does XMAS appear?

Your puzzle answer was 2685.

--- Part Two ---
The Elf looks quizzically at you. Did you misunderstand the assignment?

Looking for the instructions, you flip over the word search to find that this isn't actually an XMAS puzzle; it's an X-MAS puzzle in which you're supposed to find two MAS in the shape of an X. One way to achieve that is like this:

M.S
.A.
M.S
Irrelevant characters have again been replaced with . in the above diagram. Within the X, each MAS can be written forwards or backwards.

Here's the same example from before, but this time all of the X-MASes have been kept instead:

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

Flip the word search from the instructions back over to the word search side and try again. How many times does an X-MAS appear?

Your puzzle answer was 2048.
    */
    /// </summary>
    /// <returns>
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
