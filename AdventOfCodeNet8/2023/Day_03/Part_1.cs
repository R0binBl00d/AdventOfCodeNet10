using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCodeNet8._2023.Day_03
{
  internal class Part_1 : Days
  {
    /// <summary>
    /*
    https://adventofcode.com/2023/day/3
    --- Day 3: Gear Ratios ---
    You and the Elf eventually reach a gondola lift station; he says the 
    gondola lift will take you up to the water source, but this is as far 
    as he can bring you. You go inside.

    It doesn't take long to find the gondolas, but there seems to be a problem: 
    they're not moving.

    "Aaah!"

    You turn around to see a slightly-greasy Elf with a wrench and a look of surprise. 
    "Sorry, I wasn't expecting anyone! The gondola lift isn't working right now; 
    it'll still be a while before I can fix it." You offer to help.

    The engineer explains that an engine part seems to be missing from the engine, 
    but nobody can figure out which one. If you can add up all the part numbers 
    in the engine schematic, it should be easy to work out which part is missing.

    The engine schematic (your puzzle input) consists of a visual representation 
    of the engine. There are lots of numbers and symbols you don't really understand, 
    but apparently any number adjacent to a symbol, even diagonally, is a "part number" 
    and should be included in your sum. (Periods (.) do not count as a symbol.)

    Here is an example engine schematic:

    467..114..
    ...*......
    ..35..633.
    ......#...
    617*......
    .....+.58.
    ..592.....
    ......755.
    ...$.*....
    .664.598..

    In this schematic, two numbers are not part numbers because they are not adjacent to a symbol: 
    
    114 (top right) and 58 (middle right). 
    
    Every other number is adjacent to a symbol and so is a 
    part number; their sum is 4361.

    Of course, the actual engine schematic is much larger. 
    
    What is the sum of all of the part numbers in the engine schematic?
    */
    /// </summary>
    /// <returns>
    /// 515332 (with negative numbers, removing from the text)
    /// 519444 !!
    /// </returns>
    public override string Execute()
    {
      string result = "";

      int width = Lines[0].Length;
      int height = Lines.Count;

      // create Matrix // obsolete but shows the method
      char[,] matrix = new char[width, height];
      for (int y = 0; y < height; y++)
      {
        for (int x = 0; x < width; x++)
        {
          matrix[x, y] = Lines[y][x];
        }
      }

      int sum = 0;

      string pattern = @"\d+"; // negative numbers removed -> old ->  @"-?\b\d+\b";
      for (int y = 0; y < height; y++)
      {
        MatchCollection matches = Regex.Matches(Lines[y], pattern);

        foreach (Match match in matches)
        {
          if (HasSybolAdjacend(match, matrix, y, width, height))
          {
            sum += Int32.Parse(match.Value);
          }
        }
      }
      result = sum.ToString();
      return result;
    }

    private bool HasSybolAdjacend(Match match, char[,] matrix, int lineNo, int width, int height)
    {
      // create surrounding
      int startLine = lineNo - 1;
      int endLine = lineNo + 1;
      int startColumn = match.Index - 1;
      int endColumn = match.Index + match.Length;

      bool isSymbol = false;

      for (int y = startLine; y <= endLine; y++)
      {
        if (y < 0 || y >= height) continue;

        for (int x = startColumn; x <= endColumn; x++)
        {
          if (x < 0 || x >= width) continue;
          if (y == lineNo && (x > startColumn && x < endColumn)) continue;

          if (matrix[x, y] != '.')
          {
            isSymbol = true;
            break;
          }
        }
        if (isSymbol)
        {
          break;
        }
      }

      return isSymbol;
    }
  }
}
