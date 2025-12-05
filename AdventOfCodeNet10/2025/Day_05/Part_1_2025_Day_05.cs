namespace AdventOfCodeNet10._2025.Day_05
{
  internal class Part_1_2025_Day_05 : Days
  {
    /// <summary>
    /*
    https://adventofcode.com/2025/day/5
    --- Day 5: Cafeteria ---
    As the forklifts break through the wall, the Elves are delighted to discover
    that there was a cafeteria on the other side after all.
    
    You can hear a commotion coming from the kitchen. "At this rate, we won't have
    any time left to put the wreaths up in the dining hall!" Resolute in your
    quest, you investigate.
    
    "If only we hadn't switched to the new inventory management system right before
    Christmas!" another Elf exclaims. You ask what's going on.
    
    The Elves in the kitchen explain the situation: because of their complicated
    new inventory management system, they can't figure out which of their
    ingredients are fresh and which are spoiled. When you ask how it works, they
    give you a copy of their database (your puzzle input).
    
    The database operates on ingredient IDs. It consists of a list of fresh
    ingredient ID ranges, a blank line, and a list of available ingredient IDs. For
    example:
    
    3-5
    10-14
    16-20
    12-18
    
    1
    5
    8
    11
    17
    32
    The fresh ID ranges are inclusive: the range 3-5 means that ingredient IDs 3,
    4, and 5 are all fresh. The ranges can also overlap; an ingredient ID is fresh
    if it is in any range.
    
    The Elves are trying to determine which of the available ingredient IDs are
    fresh. In this example, this is done as follows:
    
    Ingredient ID 1 is spoiled because it does not fall into any range.
    Ingredient ID 5 is fresh because it falls into range 3-5.
    Ingredient ID 8 is spoiled.
    Ingredient ID 11 is fresh because it falls into range 10-14.
    Ingredient ID 17 is fresh because it falls into range 16-20 as well as range
    12-18.
    Ingredient ID 32 is spoiled.
    So, in this example, 3 of the available ingredient IDs are fresh.
    
    Process the database file from the new inventory management system. How many of
    the available ingredient IDs are fresh?
    */
    /// </summary>
    /// <returns>
    /// 770
    /// </returns>
    public override string Execute()
    {
      string result = "";
      long totalCount = 0;
      
      List<(long, long)> freshRanges = new List<(long, long)>();
      List<long> availableIds = new List<long>();

      foreach (var line in Lines)
      {
        if (String.IsNullOrEmpty(line)) continue;
        
        if (line.Contains('-'))
        {
          var chunks = line.Split('-');
          var start = long.Parse(chunks[0]);
          var end = long.Parse(chunks[1]);
          freshRanges.Add((start, end));
        }
        else
        {
          availableIds.Add(long.Parse(line));
        }
      }

      foreach (var id in availableIds)
      {
        bool isFresh = false;
        foreach (var range in freshRanges)
        {
          if (id >= range.Item1 && id <= range.Item2)
          {
            isFresh = true;
            break;
          }
        }

        if (isFresh)
        {
          totalCount++;
        }
      }
      result = totalCount.ToString();
      return result;
    }
  }
}
