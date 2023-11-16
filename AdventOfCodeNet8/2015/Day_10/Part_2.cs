using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;
using static System.Windows.Forms.LinkLabel;

namespace AdventOfCodeNet8._2015.Day_10
{
  internal class Part_2 : Days
  {
    /// <summary>
    /*
    https://adventofcode.com/2015/day/10
--- Day 10: Elves Look, Elves Say ---
Today, the Elves are playing a game called look-and-say. They take turns making sequences by reading aloud the previous sequence and using that reading as the next sequence. For example, 211 is read as "one two, two ones", which becomes 1221 (1 2, 2 1s).

Look-and-say sequences are generated iteratively, using the previous value as input for the next step. For each step, take the previous value, and replace each run of digits (like 111) with the number of digits (3) followed by the digit itself (1).

For example:

1 becomes 11 (1 copy of digit 1).
11 becomes 21 (2 copies of digit 1).
21 becomes 1211 (one 2 followed by one 1).
1211 becomes 111221 (one 1, one 2, and two 1s).
111221 becomes 312211 (three 1s, two 2s, and one 1).
Starting with the digits in your puzzle input, apply this process 40 times. What is the length of the result?

Your puzzle answer was 360154.

The first half of this puzzle is complete! It provides one gold star: *

--- Part Two ---
Neat, right? You might also enjoy hearing John Conway talking about this sequence (that's Conway of Conway's Game of Life fame).

Now, starting again with the digits in your puzzle input, apply this process 50 times. What is the length of the new result?    */
    /// </summary>
    /// <returns>
    /// Your puzzle answer was 5103798.
    /// </returns>
    public override string Execute()
    {
      string result = "";

      String line = Lines[0].Trim();
      //Console.WriteLine(line);
      for (int i = 0; i < 50; i++)
      {
        line = ProcessLine(line);
        //Console.WriteLine(line);
      }

      result = line.Length.ToString();
      return result;
    }

    private string ProcessLine(string line)
    {
      char lastChar = ' ';
      int charCount = 0;

      StringBuilder sb = new StringBuilder();

      foreach (char c in line)
      {
        if (lastChar == c)
        {
          // same char
          charCount++;
          continue;
        }
        else
        {
          // new char
          if (lastChar == ' ')
          {
            lastChar = c;
            charCount = 1;
            continue;
          }
          sb.AppendFormat("{0}{1}", charCount, lastChar);

          lastChar = c;
          charCount = 1;
        }
      }

      sb.AppendFormat("{0}{1}", charCount, lastChar);
      return sb.ToString();
    }
  }
}
