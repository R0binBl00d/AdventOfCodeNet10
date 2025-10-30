namespace AdventOfCodeNet10._2024.Day_02
{
  internal class Part_2_2024_Day_02 : Days
  {
    /// <summary>
    /*
    https://adventofcode.com/2024/day/2
    --- Part Two ---
    The engineers are surprised by the low number of safe reports until they
    realize they forgot to tell you about the Problem Dampener.
    
    The Problem Dampener is a reactor-mounted module that lets the reactor safety
    systems tolerate a single bad level in what would otherwise be a safe report.
    It's like the bad level never happened!
    
    Now, the same rules apply as before, except if removing a single level from an
    unsafe report would make it safe, the report instead counts as safe.
    
    More of the above example's reports are now safe:
    
    7 6 4 2 1: Safe without removing any level.
    1 2 7 8 9: Unsafe regardless of which level is removed.
    9 7 6 2 1: Unsafe regardless of which level is removed.
    1 3 2 4 5: Safe by removing the second level, 3.
    8 6 4 4 1: Safe by removing the third level, 4.
    1 3 6 7 9: Safe without removing any level.
    Thanks to the Problem Dampener, 4 reports are actually safe!
    
    Update your analysis by handling situations where the Problem Dampener can
    remove a single level from unsafe reports. How many reports are now safe?
    */
    /// </summary>
    /// <returns>
    /// 432 -> Part one
    /// 669 -> too high (did not refill the numbers-array after deleting an element !!!)
    /// 475 -> too low (WTF !!)        Exit contition is: if (removalIndex == chunks.Count)
    /// and counts are                 for (removalIndex = -1; removalIndex < chunks.Count; removalIndex++)
    /// I used "numbers.Count" instead, which is constantly changing !!!
    /// 488
    /// </returns>
    public override string Execute()
    {
      string result = "";

      int totalSafeReports = 0;
      var breakFlag = false;

      foreach (var line in Lines)
      {
        var chunks = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();

        var numbers = new List<int>();
        chunks.ForEach(n => numbers.Add(Int32.Parse(n)));

        // check if I can remove one to make it safe
        int removalIndex;
        for (removalIndex = -1; removalIndex < chunks.Count; removalIndex++)
        {
          numbers.Clear();
          chunks.ForEach(n => numbers.Add(Int32.Parse(n)));

          breakFlag = false;

          bool ascendingNumbers = false;
          if (removalIndex < 0)
          {
            ascendingNumbers = numbers[1] > numbers[0];
          }
          else
          {
            // I need to remove one number
            numbers.RemoveAt(removalIndex);
            ascendingNumbers = numbers[1] > numbers[0];
          }

          for (int i = 1; i < numbers.Count; i++)
          {
            // Any two adjacent levels differ by at least one and at most three.
            if (Math.Abs(numbers[i] - numbers[i - 1]) > 3 || Math.Abs(numbers[1] - numbers[0]) == 0)
            {
              breakFlag = true;
              break;
            }

            if (ascendingNumbers)
            {
              if (numbers[i] - numbers[i - 1] <= 0)
              {
                breakFlag = true;
                break;
              }
            }
            else
            {
              if (numbers[i] - numbers[i - 1] >= 0)
              {
                breakFlag = true;
                break;
              }
            }
          }

          if (breakFlag) continue;
          else break;
        }

        if (removalIndex == chunks.Count)
        {
          // All broken == UNSAFE !!
          continue;
        }
        else
        {
          totalSafeReports++;
        }
      }
      result = totalSafeReports.ToString();
      return result;
    }
  }
}
