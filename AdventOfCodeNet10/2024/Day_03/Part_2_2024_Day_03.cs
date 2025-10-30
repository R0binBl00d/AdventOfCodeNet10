namespace AdventOfCodeNet10._2024.Day_03
{
  internal class Part_2_2024_Day_03 : Days
  {
    /// <summary>
    /*
    https://adventofcode.com/2024/day/3
--- Day 3: Mull It Over ---
"Our computers are having issues, so I have no idea if we have any Chief Historians in stock! You're welcome to check the warehouse, though," says the mildly flustered shopkeeper at the North Pole Toboggan Rental Shop. The Historians head out to take a look.

The shopkeeper turns to you. "Any chance you can see why our computers are having issues again?"

The computer appears to be trying to run a program, but its memory (your puzzle input) is corrupted. All of the instructions have been jumbled up!

It seems like the goal of the program is just to multiply some numbers. It does that with instructions like mul(X,Y), where X and Y are each 1-3 digit numbers. For instance, mul(44,46) multiplies 44 by 46 to get a result of 2024. Similarly, mul(123,4) would multiply 123 by 4.

However, because the program's memory has been corrupted, there are also many invalid characters that should be ignored, even if they look like part of a mul instruction. Sequences like mul(4*, mul(6,9!, ?(12,34), or mul ( 2 , 4 ) do nothing.

For example, consider the following section of corrupted memory:

xmul(2,4)%&mul[3,7]!@^do_not_mul(5,5)+mul(32,64]then(mul(11,8)mul(8,5))
Only the four highlighted sections are real mul instructions. Adding up the result of each instruction produces 161 (2*4 + 5*5 + 11*8 + 8*5).

Scan the corrupted memory for uncorrupted mul instructions. What do you get if you add up all of the results of the multiplications?

Your puzzle answer was 155955228.

--- Part Two ---
As you scan through the corrupted memory, you notice that some of the conditional statements are also still intact. If you handle some of the uncorrupted conditional statements in the program, you might be able to get an even more accurate result.

There are two new instructions you'll need to handle:

The do() instruction enables future mul instructions.
The don't() instruction disables future mul instructions.
Only the most recent do() or don't() instruction applies. At the beginning of the program, mul instructions are enabled.

For example:

xmul(2,4)&mul[3,7]!^don't()_mul(5,5)+mul(32,64](mul(11,8)undo()?mul(8,5))
This corrupted memory is similar to the example from before, but this time the mul(5,5) and mul(11,8) instructions are disabled because there is a don't() instruction before them. The other mul instructions function normally, including the one at the end that gets re-enabled by a do() instruction.

This time, the sum of the results is 48 (2*4 + 8*5).

Handle the new instructions; what do you get if you add up all of the results of just the enabled multiplications?

Your puzzle answer was 100189366.
    */
    /// </summary>
    /// <returns>
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
