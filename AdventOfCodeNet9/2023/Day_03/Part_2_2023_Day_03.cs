using System.Text.RegularExpressions;

namespace AdventOfCodeNet9._2023.Day_03
{
  internal class Part_2_2023_Day_03 : Days
  {
    /// <summary>
    /*
    https://adventofcode.com/2023/day/3
    --- Day 3: Gear Ratios ---
    You and the Elf eventually reach a gondola lift station; he says the gondola lift will take you up to the water source, but this is as far as he can bring you. You go inside.

    It doesn't take long to find the gondolas, but there seems to be a problem: they're not moving.

    "Aaah!"

    You turn around to see a slightly-greasy Elf with a wrench and a look of surprise. "Sorry, I wasn't expecting anyone! The gondola lift isn't working right now; it'll still be a while before I can fix it." You offer to help.

    The engineer explains that an engine part seems to be missing from the engine, but nobody can figure out which one. If you can add up all the part numbers in the engine schematic, it should be easy to work out which part is missing.

    The engine schematic (your puzzle input) consists of a visual representation of the engine. There are lots of numbers and symbols you don't really understand, but apparently any number adjacent to a symbol, even diagonally, is a "part number" and should be included in your sum. (Periods (.) do not count as a symbol.)

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
    In this schematic, two numbers are not part numbers because they are not adjacent to a symbol: 114 (top right) and 58 (middle right). Every other number is adjacent to a symbol and so is a part number; their sum is 4361.

    Of course, the actual engine schematic is much larger. What is the sum of all of the part numbers in the engine schematic?

    Your puzzle answer was 519444.

    The first half of this puzzle is complete! It provides one gold star: *

    --- Part Two ---
    The engineer finds the missing part and installs it in the engine! As the engine springs to life, 
    you jump in the closest gondola, finally ready to ascend to the water source.

    You don't seem to be going very fast, though. Maybe something is still wrong? 
    Fortunately, the gondola has a phone labeled "help", so you pick it up and the engineer answers.

    Before you can explain the situation, she suggests that you look out the window. 
    There stands the engineer, holding a phone in one hand and waving with the other. 
    You're going so slowly that you haven't even left the station. You exit the gondola.

    The missing part wasn't the only issue - one of the gears in the engine is wrong. 
    A gear is any * symbol that is adjacent to exactly two part numbers. Its gear ratio 
    is the result of multiplying those two numbers together.

    This time, you need to find the gear ratio of every gear and add them all up so that 
    the engineer can figure out which gear needs to be replaced.

    Consider the same engine schematic again:

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
    In this schematic, there are two gears. 
    
    The first is in the top left; it has part numbers 467 and 35, so its gear ratio is 16345. 
    The second gear is in the lower right; its gear ratio is 451490. 
    
    (The * adjacent to 617 is not a gear because it is only adjacent to one part number.) 
    
    Adding up all of the gear ratios produces 467835.

    What is the sum of all of the gear ratios in your engine schematic?
    */
    /// </summary>
    /// <returns>
    /// 74528807
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

      // save all numbers in a List (with locations and lengths)
      List<KeyValuePair<int, Match>> allNumersAndLocations = new List<KeyValuePair<int, Match>>();

      string pattern = @"\b\d+\b"; // negative numbers removed -> old ->  @"-?\b\d+\b";
      for (int y = 0; y < height; y++)
      {
        MatchCollection matches = Regex.Matches(Lines[y], pattern);

        foreach (Match match in matches)
        {
          //if (HasSybolAdjacend(match, matrix, y, width, height))
          //{
          //  sum += Int32.Parse(match.Value);
          //}
          allNumersAndLocations.Add(new KeyValuePair<int, Match>(y, match));
        }
      }

      // Now find all '*'
      pattern = "\\*"; // negative numbers removed -> old ->  @"-?\b\d+\b";
      for (int y = 0; y < height; y++)
      {
        MatchCollection matches = Regex.Matches(Lines[y], pattern);

        foreach (Match match in matches)
        {
          if (HasNumbersAdjacend(match, allNumersAndLocations, y, width, height, out var gearRatio))
          {
            sum += gearRatio;
          }
        }
      }

      result = sum.ToString();
      return result;
    }

    private bool HasNumbersAdjacend(Match match, List<KeyValuePair<int, Match>> allNumNlocations, int lineNo, int width, int height, out int GearRatio)
    {
      GearRatio = 0;
      // create surrounding
      int startLine = lineNo - 1;
      int endLine = lineNo + 1;
      int startColumn = match.Index - 1;
      int endColumn = match.Index + match.Length;

      List <int> NumbersArround = new List<int>();

      foreach (var nal in allNumNlocations)
      {
        // skip the wrong lines
        if (nal.Key < startLine || nal.Key > endLine) continue;

        // check X Location
        if (
          ((nal.Value.Index + nal.Value.Length) > startColumn) && // right side of the number must be left to Star
          (nal.Value.Index <= endColumn)                           // left side of the number must be right to Star
          )
        {
          NumbersArround.Add(Int32.Parse(nal.Value.Value));
        }
      }

      if(NumbersArround.Count == 2) 
      {
        GearRatio = NumbersArround[0] * NumbersArround[1];
        return true;
      }

      return false;
    }
  }
}
