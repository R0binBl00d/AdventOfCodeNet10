namespace AdventOfCodeNet9._2018.Day_22
{
  internal class Part_1 : Days
  {
    /// <summary>
    /*
    https://adventofcode.com/2018/day/22
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