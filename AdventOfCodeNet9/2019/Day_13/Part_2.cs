namespace AdventOfCodeNet9._2019.Day_13
{
  internal class Part_2 : Days
  {
    /// <summary>
    /*
    https://adventofcode.com/2019/day/13
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