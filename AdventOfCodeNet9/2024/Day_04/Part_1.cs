namespace AdventOfCodeNet9._2024.Day_04
{
  internal class Part_1 : Days
  {
    /// <summary>
    /*
    https://adventofcode.com/2024/day/4

    --- Day 4: Ceres Search ---
    "Looks like the Chief's not here. Next!" One of The Historians pulls out a
    device and pushes the only button on it. After a brief flash, you recognize the
    interior of the Ceres monitoring station!
    
    As the search for the Chief continues, a small Elf who lives on the station
    tugs on your shirt; she'd like to know if you could help her with her word
    search (your puzzle input). She only has to find one word: XMAS.
    
    This word search allows words to be horizontal, vertical, diagonal, written
    backwards, or even overlapping other words. It's a little unusual, though, as
    you don't merely need to find one instance of XMAS - you need to find all of
    them. Here are a few ways XMAS might appear, where irrelevant characters have
    been replaced with .:
    
    
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
    In this word search, XMAS occurs a total of 18 times; here's the same word
    search again, but where letters not involved in any XMAS have been replaced
    with .:
    
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


    */
    /// </summary>
    /// <returns>
    /// 2685
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
          if (Lines[y][x] == 'X')
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
      int foundForThisX = 0;
      //search in all directions
      // case right
      if (x < Lines[y].Length - 3) // for 10 chars, X need to be on 7 (= index 6)
      {
        if
        (
          Lines[y][x + 1] == 'M' &&
          Lines[y][x + 2] == 'A' &&
          Lines[y][x + 3] == 'S'
        )
        {
          foundForThisX++;
        }
      }
      // case left
      if (x > 2) // index 3 = 4th character
      {
        if
        (
          Lines[y][x - 1] == 'M' &&
          Lines[y][x - 2] == 'A' &&
          Lines[y][x - 3] == 'S'
        )
        {
          foundForThisX++;
        }
      }
      // case top
      if (y > 2) // index 3 = 4th row
      {
        if
        (
          Lines[y - 1][x] == 'M' &&
          Lines[y - 2][x] == 'A' &&
          Lines[y - 3][x] == 'S'
        )
        {
          foundForThisX++;
        }
      }
      // case bottom
      if (y < Lines.Count - 3) // for 10 rows, y need to be on 7 (= index 6)
      {
        if
        (
          Lines[y + 1][x] == 'M' &&
          Lines[y + 2][x] == 'A' &&
          Lines[y + 3][x] == 'S'
        )
        {
          foundForThisX++;
        }
      }
      // ###########################################################
      // Round the clock diagonals -- starting top right
      // case right top
      if (x < Lines[y].Length - 3 && y > 2)
      {
        if
        (
          Lines[y - 1][x + 1] == 'M' &&
          Lines[y - 2][x + 2] == 'A' &&
          Lines[y - 3][x + 3] == 'S'
        )
        {
          foundForThisX++;
        }
      }
      // case right bottom
      if (x < Lines[y].Length - 3 && y < Lines.Count - 3)
      {
        if
        (
          Lines[y + 1][x + 1] == 'M' &&
          Lines[y + 2][x + 2] == 'A' &&
          Lines[y + 3][x + 3] == 'S'
        )
        {
          foundForThisX++;
        }
      }
      // case left bottom
      if (x > 2 && y < Lines.Count - 3)
      {
        if
        (
          Lines[y + 1][x - 1] == 'M' &&
          Lines[y + 2][x - 2] == 'A' &&
          Lines[y + 3][x - 3] == 'S'
        )
        {
          foundForThisX++;
        }
      }
      // case left top
      if (x > 2 && y > 2)
      {
        if
        (
          Lines[y - 1][x - 1] == 'M' &&
          Lines[y - 2][x - 2] == 'A' &&
          Lines[y - 3][x - 3] == 'S'
        )
        {
          foundForThisX++;
        }
      }
      return foundForThisX;
    }
  }
}
