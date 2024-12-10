using AdventOfCodeNet9.Extensions;

namespace AdventOfCodeNet9._2024.Day_10
{
  internal class Part_2_2024_Day_10 : Days
  {
    /// <summary>
    /*
    https://adventofcode.com/2024/day/10

    --- Part Two ---
    The reindeer spends a few minutes reviewing your hiking trail map before
    realizing something, disappearing for a few minutes, and finally returning with
    yet another slightly-charred piece of paper.
    
    The paper describes a second way to measure a trailhead called its rating. A
    trailhead's rating is the number of distinct hiking trails which begin at that
    trailhead. For example:
    
    .....0.
    ..4321.
    ..5..2.
    ..6543.
    ..7..4.
    ..8765.
    ..9....
    The above map has a single trailhead; its rating is 3 because there are exactly
    three distinct hiking trails which begin at that position:
    
    .....0.   .....0.   .....0.
    ..4321.   .....1.   .....1.
    ..5....   .....2.   .....2.
    ..6....   ..6543.   .....3.
    ..7....   ..7....   .....4.
    ..8....   ..8....   ..8765.
    ..9....   ..9....   ..9....
    Here is a map containing a single trailhead with rating 13:
    
    ..90..9
    ...1.98
    ...2..7
    6543456
    765.987
    876....
    987....
    This map contains a single trailhead with rating 227 (because there are 121
    distinct hiking trails that lead to the 9 on the right edge and 106 that lead
    to the 9 on the bottom edge):
    
    012345
    123456
    234567
    345678
    4.6789
    56789.
    Here's the larger example from before:
    
    89010123
    78121874
    87430965
    96549874
    45678903
    32019012
    01329801
    10456732
    Considering its trailheads in reading order, they have ratings of 20, 24, 10,
    4, 1, 4, 5, 8, and 5. The sum of all trailhead ratings in this larger example
    topographic map is 81.
    
    You're not sure how, but the reindeer seems to have crafted some tiny flags out
    of toothpicks and bits of paper and is using them to mark trailheads on your
    topographic map. What is the sum of the ratings of all trailheads?
    
    
    */
    /// </summary>
    /// <returns>
    /// 81 (Test)
    /// the result was the wrong result from day-1
    /// 1657
    /// </returns>
    public override string Execute()
    {
      string result = "";
      int totalCount = 0;

      var yxField = new List<List<int>>();
      var startingPoints = new List<(int x, int y)>();

      foreach (var line in Lines)
        yxField.Add(line.ToCharArray().AsInt32s().ToList());

      for (int x = 0; x < yxField[0].Count; x++)
        for (int y = 0; y < yxField.Count; y++)
          if (yxField[y][x] == 0)
            startingPoints.Add((x, y));

      foreach (var startingPoint in startingPoints)
      {
        var destinations = new List<(int x, int y)>();
        totalCount += WalkAllTheWayUntil_9(startingPoint, ref yxField, ref destinations);
      }

      result = totalCount.ToString();
      return result;
    }

    private int WalkAllTheWayUntil_9((int x, int y) currentLocation, ref List<List<int>> yxField, ref List<(int x, int y)> destinations)
    {
      if (!currentLocation.x.InRange(0, yxField[0].Count, IncludeBounds.Lower) ||
          !currentLocation.y.InRange(0, yxField.Count, IncludeBounds.Lower)) return 0;

      int currentValue = yxField[currentLocation.y][currentLocation.x];
      if (currentValue == 9) return 1;

      // try to walk the next step
      int count = 0;
      (int x, int y) up = (currentLocation.x, currentLocation.y - 1);
      (int x, int y) down = (currentLocation.x, currentLocation.y + 1);
      (int x, int y) left = (currentLocation.x - 1, currentLocation.y);
      (int x, int y) right = (currentLocation.x + 1, currentLocation.y);

      if (ValidDirection(up, currentValue, ref yxField)) count += WalkAllTheWayUntil_9(up, ref yxField, ref destinations);
      if (ValidDirection(down, currentValue, ref yxField)) count += WalkAllTheWayUntil_9(down, ref yxField, ref destinations);
      if (ValidDirection(left, currentValue, ref yxField)) count += WalkAllTheWayUntil_9(left, ref yxField, ref destinations);
      if (ValidDirection(right, currentValue, ref yxField)) count += WalkAllTheWayUntil_9(right, ref yxField, ref destinations);
      return count;
    }

    private bool ValidDirection((int x, int y) loc, int currentValue, ref List<List<int>> yxField)
    {
      if (!loc.x.InRange(0, yxField[0].Count, IncludeBounds.Lower) ||
          !loc.y.InRange(0, yxField.Count, IncludeBounds.Lower)) return false;

      return (currentValue == yxField[loc.y][loc.x] - 1);
    }
  }
}
