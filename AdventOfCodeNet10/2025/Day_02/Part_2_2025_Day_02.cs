using System.Diagnostics;

namespace AdventOfCodeNet10._2025.Day_02
{
  internal class Part_2_2025_Day_02 : Days
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

    Your puzzle answer was 28846518423.
    
    The first half of this puzzle is complete! It provides one gold star: *
    
    --- Part Two ---
    The clerk quickly discovers that there are still invalid IDs in the ranges in
    your list. Maybe the young Elf was doing other silly patterns as well?
    
    Now, an ID is invalid if it is made only of some sequence of digits repeated at
    least twice. So, 12341234 (1234 two times), 123123123 (123 three times),
    1212121212 (12 five times), and 1111111 (1 seven times) are all invalid IDs.
    
    From the same example as before:
    
    11-22 still has two invalid IDs, 11 and 22.
    95-115 now has two invalid IDs, 99 and 111.
    998-1012 now has two invalid IDs, 999 and 1010.
    1188511880-1188511890 still has one invalid ID, 1188511885.
    222220-222224 still has one invalid ID, 222222.
    1698522-1698528 still contains no invalid IDs.
    446443-446449 still has one invalid ID, 446446.
    38593856-38593862 still has one invalid ID, 38593859.
    565653-565659 now has one invalid ID, 565656.
    824824821-824824827 now has one invalid ID, 824824824.
    2121212118-2121212124 now has one invalid ID, 2121212121.
    Adding up all the invalid IDs in this example produces 4174379265.
    
    What do you get if you add up all of the invalid IDs using these new rules?
    */
    /// </summary>
    /// <returns>
    /// 31578210022
    /// </returns>
    public override string Execute()
    {
      string result = "";
      long totalCount = 0;
      wrongIDs.Clear();

      var ranges = Lines[0].Split(',', StringSplitOptions.RemoveEmptyEntries);

      //int overallMax = 0;
      //int overallMin = int.MaxValue;
      foreach (var entry in ranges)
      {
        var chunks = entry.Split('-', StringSplitOptions.RemoveEmptyEntries);

        totalCount += CountDoubledDigitNumbersInRange(chunks[0], chunks[1], ref wrongIDs);

        //int maxDigits = Math.Max(chunks[0].Length, chunks[1].Length);
        //int minDigits = Math.Min(chunks[0].Length, chunks[1].Length);
        //overallMax = Math.Max(overallMax, maxDigits);
        //overallMin = Math.Min(overallMin, minDigits);
        //Debug.WriteLine($"S:{chunks[0]} - F:{chunks[1]}; max digits:{maxDigits}");
        //Debug.WriteLine($"S:{chunks[0]} - F:{chunks[1]}; min digits:{minDigits}");
      }
      //Debug.WriteLine($"");
      //Debug.WriteLine($"Overall max digits:{overallMax}");
      //Debug.WriteLine($"Overall min digits:{overallMin}");
      //Debug.WriteLine($"");

      result = wrongIDs.Sum().ToString();
      Debugger.Break();
      return result;
    }

    private List<long> wrongIDs = new List<long>();
    private List<string> list = new List<string>();

    private long CountDoubledDigitNumbersInRange(string start, string end, ref List<long> wrongIDs)
    {
      long count = 0;

      if (start[0] == '0' || end[0] == '0') return 0;

      for (long id = long.Parse(start); id <= long.Parse(end); id++)
      {
        if (CheckForRepeatingPattern(id) > 0L)
        {
          wrongIDs.Add(id);
          count++;
        }
      }
      return count;
    }

    private long CheckForRepeatingPattern(long id)
    {
      bool found = false;
      // Check for 1,2,3,4,5 long repeatblocks (max 10 digits)

      // return if only one digit
      string strID = id.ToString();
      if (strID.Length < 2) return 0L;

      for (int digitRepeat = 1; digitRepeat <= 5; digitRepeat++)
      {
        switch (digitRepeat)
        {
          case 1: // search for 1 digit repeat (all same digit)
            if ((from c in strID select c).Distinct().Count() == 1)
              return 1L;
            break;
          case 2: // search for 2 digit repeat (1212 or 575757 or 95959595 or 7373737373)
            if (strID.Length % 2 != 0 || strID.Length < 4) break;
            list = SplitIntoParts(strID, 2);
            if ((from p in list select p).Distinct().Count() == 1)
              found = true;
            break;
          case 3:
            if (strID.Length % 3 != 0 || strID.Length < 6) break;
            list = SplitIntoParts(strID, 3);
            if ((from p in list select p).Distinct().Count() == 1)
              found = true;
            break;
          case 4:
            if (strID.Length % 4 != 0 || strID.Length < 8) break;
            list = SplitIntoParts(strID, 4);
            if ((from p in list select p).Distinct().Count() == 1)
              found = true;
            break;
          case 5:
            if (strID.Length % 5 != 0 || strID.Length < 10) break;
            list = SplitIntoParts(strID, 5);
            if ((from p in list select p).Distinct().Count() == 1)
              found = true;
            break;
        }

        if (found) break;
      }

      if (found)
      {
        //Debug.WriteLine($"Found repeating ID:{id}");
        return 1L;
      }
      else
      {
        return 0L;
      }
    }

    private List<string> SplitIntoParts(string strId, int partLength)
    {
      if (string.IsNullOrEmpty(strId)) throw new ArgumentException("Input string cannot be null or empty.", nameof(strId));

      if (partLength <= 0) throw new ArgumentException("Part length must be greater than zero.", nameof(partLength));

      var parts = new List<string>();
      for (var i = 0; i < strId.Length; i += partLength)
        if (i + partLength <= strId.Length)
          parts.Add(strId.Substring(i, partLength));

      return parts;
    }
  }
}
