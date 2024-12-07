namespace AdventOfCodeNet9._2016.Day_19
{
  internal class Part_1 : Days
  {
    /// <summary>
    /*
    https://adventofcode.com/2016/day/19
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