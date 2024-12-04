using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace AdventOfCodeNet8._2024.Day_03
{
  internal class Part_2 : Days
  {
    /// <summary>
    /*
    https://adventofcode.com/2024/day/3

    --- Part Two ---
    As you scan through the corrupted memory, you notice that some of the
    conditional statements are also still intact. If you handle some of the
    uncorrupted conditional statements in the program, you might be able to get an
    even more accurate result.
    
    There are two new instructions you'll need to handle:
    
    The do() instruction enables future mul instructions.
    The don't() instruction disables future mul instructions.
    Only the most recent do() or don't() instruction applies. At the beginning of
    the program, mul instructions are enabled.
    
    For example:
    
    xmul(2,4)&mul[3,7]!^don't()_mul(5,5)+mul(32,64](mul(11,8)undo()?mul(8,5))
    This corrupted memory is similar to the example from before, but this time the
    mul(5,5) and mul(11,8) instructions are disabled because there is a don't()
    instruction before them. The other mul instructions function normally,
    including the one at the end that gets re-enabled by a do() instruction.
    
    This time, the sum of the results is 48 (2*4 + 8*5).
    
    Handle the new instructions; what do you get if you add up all of the results
    of just the enabled multiplications?

    */
    /// </summary>
    /// <returns>
    /// 155955228 -> Part 1
    /// 100189366
    /// </returns>
    public override string Execute()
    {
      string result = "";

      long totalResult = 0;
      long num1 = 0;
      long num2 = 0;

      bool calculation_enabled = true;

      foreach (var line in Lines)
      {
        int start = 0;
        int finis = 0;

        int index_doit = 0;
        int index_dont = 0;

        do
        {
          start = line.IndexOf("mul(", start);
          index_doit = line.IndexOf("do()", index_doit);
          if (index_doit < 0) index_doit = line.Length;

          index_dont = line.IndexOf("don't()", index_dont);
          if (index_dont < 0) index_dont = line.Length;

          if (start < 0) break;

          if (start > index_dont)
          {
            calculation_enabled = false;
            index_dont += 7;
          }

          if (start > index_doit)
          {
            calculation_enabled = true;
            index_doit += 4;
          }

          // add the 4 chars I was searching for, to get the content of the string between brackets
          start += 4;
          finis = line.IndexOf(")", start);

          if (calculation_enabled)
          {
            if (finis > start + 2)
            {
              var chunks = line.Substring(start, finis - start).Split(',');

              if (
                chunks.Length == 2 &&
                Int64.TryParse(chunks[0], out num1) &&
                Int64.TryParse(chunks[1], out num2)
              )
              {
                checked
                {
                  totalResult += num1 * num2;
                }
              }
            }
          }
        }
        while (start >= 0 && finis > start);
      }

      result = totalResult.ToString();
      return result;
    }
  }
}
