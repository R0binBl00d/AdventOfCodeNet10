namespace AdventOfCodeNet9._2017.Day_02
{
  internal class Part_2 : Days
  {
    /// <summary>
    /*
    https://adventofcode.com/2017/day/2
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