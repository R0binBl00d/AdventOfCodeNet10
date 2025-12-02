using System.Diagnostics;

namespace AdventOfCodeNet10._2025.Day_02
{
  internal class Part_1_2025_Day_02 : Days
  {
    /// <summary>
    /*
    https://adventofcode.com/2025/day/2
    --- Day 2: Gift Shop ---
    You get inside and take the elevator to its only other stop: the gift shop.
    "Thank you for visiting the North Pole!" gleefully exclaims a nearby sign. You
    aren't sure who is even allowed to visit the North Pole, but you know you can
    access the lobby through here, and from there you can access the rest of the
    North Pole base.
    
    As you make your way through the surprisingly extensive selection, one of the
    clerks recognizes you and asks for your help.
    
    As it turns out, one of the younger Elves was playing on a gift shop computer
    and managed to add a whole bunch of invalid product IDs to their gift shop
    database! Surely, it would be no trouble for you to identify the invalid
    product IDs for them, right?
    
    They've even checked most of the product ID ranges already; they only have a
    few product ID ranges (your puzzle input) that you'll need to check. For
    example:
    
    11-22,95-115,998-1012,1188511880-1188511890,222220-222224,
    1698522-1698528,446443-446449,38593856-38593862,565653-565659,
    824824821-824824827,2121212118-2121212124
    (The ID ranges are wrapped here for legibility; in your input, they appear on a
    single long line.)
    
    The ranges are separated by commas (,); each range gives its first ID and last
    ID separated by a dash (-).
    
    Since the young Elf was just doing silly patterns, you can find the invalid IDs
    by looking for any ID which is made only of some sequence of digits repeated
    twice. So, 55 (5 twice), 6464 (64 twice), and 123123 (123 twice) would all be
    invalid IDs.
    
    None of the numbers have leading zeroes; 0101 isn't an ID at all. (101 is a
    valid ID that you would ignore.)
    
    Your job is to find all of the invalid IDs that appear in the given ranges. In
    the above example:
    
    - 11-22 has two invalid IDs, 11 and 22.
    - 95-115 has one invalid ID, 99.
    - 998-1012 has one invalid ID, 1010.
    - 1188511880-1188511890 has one invalid ID, 1188511885.
    - 222220-222224 has one invalid ID, 222222.
    - 1698522-1698528 contains no invalid IDs.
    - 446443-446449 has one invalid ID, 446446.
    - 38593856-38593862 has one invalid ID, 38593859.
    - The rest of the ranges contain no invalid IDs.
    
    Adding up all the invalid IDs in this example produces 1227775554.
    
    What do you get if you add up all of the invalid IDs?
    */
    /// </summary>
    /// <returns>
    /// 28846518423
    /// </returns>
    public override string Execute()
    {
      string result = "";
      long totalCount = 0;
      wrongIDs.Clear();

      var ranges = Lines[0].Split(',', StringSplitOptions.RemoveEmptyEntries);

        int overallMax = 0;
      foreach (var entry in ranges)
      {
        var chunks = entry.Split('-', StringSplitOptions.RemoveEmptyEntries);

        totalCount += CountDoubledDigitNumbersInRange(chunks[0], chunks[1], ref wrongIDs);

        int maxDigits = Math.Max(chunks[0].Length, chunks[1].Length);
        overallMax = Math.Max(overallMax, maxDigits);
        //Debug.WriteLine($"S:{chunks[0]} - F:{chunks[1]}; max digits:{maxDigits}");
      }
      //Debug.WriteLine($"");
      //Debug.WriteLine($"Overall max digits:{overallMax}");
      //Debug.WriteLine($"");

      result = wrongIDs.Sum().ToString();
      //Debugger.Break();
      return result;
    }

    private List<long> wrongIDs = new List<long>();

    private long CountDoubledDigitNumbersInRange(string start, string end, ref List<long> wrongIDs)
    {
      long count = 0;

      if (start[0] == '0' || end[0] == '0') return 0;

      for (long id = long.Parse(start); id <= long.Parse(end); id++)
      {
        count += CheckForRepeatingPattern(id, ref wrongIDs);
      }
      return count;
    }

    private long CheckForRepeatingPattern(long id, ref List<long> wrongIDs)
    {
      bool found = false;
      // Check for 1,2,3,4,5 long repeatblocks (max 10 digits)

      for (int digitRepeat = 1; digitRepeat <= 5; digitRepeat++)
      {
        string strID = id.ToString();
        if (strID.Length % 2 != 0) return 0L;

        var part1 = strID.Substring(0, strID.Length / 2);
        var part2 = strID.Substring(strID.Length / 2);

        if (part1 == part2) found = true;

        /*
        switch (digitRepeat)
        {
          case 1:
            var ch1 = (from c in strID select c).Distinct();
            if (ch1.Count() == 1)
              return 1L;
            break;
          case 2:
            break;
          case 3:
            break;
          case 4:
            break;
          case 5:
            break;
        }
        */
      }

      if (found)
      {
        wrongIDs.Add(id);
        Debug.WriteLine($"Found repeating ID:{id}");
        return 1L;
      }
      else
      {
        return 0L;
      }
    }
  }
}
