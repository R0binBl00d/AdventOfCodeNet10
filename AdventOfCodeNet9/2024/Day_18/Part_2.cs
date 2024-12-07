namespace AdventOfCodeNet9._2024.Day_18
{
  internal class Part_2 : Days
  {
    /// <summary>
    /*
    https://adventofcode.com/2024/day/18
    */
    /// </summary>
    /// <returns>
    /// 
    /// </returns>
    public override string Execute()
    {
      string result = "";
      int totalCount = 0;
      foreach (var line in Lines)
      {
        totalCount++;
      }
      result = totalCount.ToString();
      return result;
    }
  }
}