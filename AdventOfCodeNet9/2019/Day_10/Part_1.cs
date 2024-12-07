namespace AdventOfCodeNet9._2019.Day_10
{
  internal class Part_1 : Days
  {
    /// <summary>
    /*
    https://adventofcode.com/2019/day/10
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