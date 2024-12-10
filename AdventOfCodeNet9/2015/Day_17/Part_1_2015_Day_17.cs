namespace AdventOfCodeNet9._2015.Day_17
{
  internal class Part_1_2015_Day_17 : Days
  {
    /// <summary>
    /*
    https://adventofcode.com/2015/day/17
    --- Day 17: No Such Thing as Too Much ---
    The elves bought too much eggnog again - 150 liters this time. To fit it all
    into your refrigerator, you'll need to move it into smaller containers. You
    take an inventory of the capacities of the available containers.
    
    For example, suppose you have containers of size 20, 15, 10, 5, and 5 liters.
    If you need to store 25 liters, there are four ways to do it:
    
    15 and 10
    20 and 5 (the first 5)
    20 and 5 (the second 5)
    15, 5, and 5
    Filling all containers entirely, how many different combinations of containers
    can exactly fit all 150 liters of eggnog?    */

    /// </summary>
    /// <returns>
    /// Your puzzle answer was 654.
    /// </returns>
    public override string Execute()
    {
      // if 20 bit -> "just count from 0 - FFFFF
      string result = "";

      List<int> buckets = new List<int>();

      foreach (string line in Lines)
      {
        buckets.Add(Int32.Parse(line));
      }

      List<string> combintions = new List<string>();
      long highestNumber = Convert.ToInt64(Math.Pow(2, buckets.Count));

      for (long i = 0; i < highestNumber; i++)
      {
        string UseNorUseBuckets = Convert.ToString(i, 2);

        int totalStorage = 0;
        for (int b = UseNorUseBuckets.Length - 1; b >= 0; b--)
        {
          if (UseNorUseBuckets[b] == '1')
          {
            // rightmost Character always indox 0
            totalStorage += buckets[(UseNorUseBuckets.Length - 1) - b];
          }
        }

        if (totalStorage == 150)
        {
          combintions.Add(UseNorUseBuckets);
        }

        //if (i == 1000000) Debugger.Break();
      }

      result = combintions.Count().ToString();
      // 455 is too low ? bug in 56, used [b]
      return result;
    }
  }
}
