namespace AdventOfCodeNet10._2015.Day_03
{
  internal class Part_1_2015_Day_03 : Days
  {
    /// <summary>
    /*
    https://adventofcode.com/2015/day/3
    --- Day 3: Perfectly Spherical Houses in a Vacuum ---
    Santa is delivering presents to an infinite two-dimensional grid of houses.
    
    He begins by delivering a present to the house at his starting location, and
    then an elf at the North Pole calls him via radio and tells him where to move
    next. Moves are always exactly one house to the north (^), south (v), east (>),
    or west (<). After each move, he delivers another present to the house at his
    new location.
    
    However, the elf back at the north pole has had a little too much eggnog, and
    so his directions are a little off, and Santa ends up visiting some houses more
    than once. How many houses receive at least one present?
    
    For example:
    
    > delivers presents to 2 houses: one at the starting location, and one to the
    east.
    ^>v< delivers presents to 4 houses in a square, including twice to the house at
    his starting/ending location.
    ^v^v^v^v^v delivers a bunch of presents to some very lucky children at only 2
    houses.
    */
    /// </summary>
    /// <returns>
    /// Your puzzle answer was 2572.
    /// </returns>
    public override string Execute()
    {
      string result = "";

      int total = 0;

      Dictionary<Point, int> houses = new Dictionary<Point, int>();

      Point position = new Point(0, 0);

      // Start delivering to the first house he's on
      houses.Add(position, 1);

      foreach (char c in Lines[0])
      {
        // v<> ^
        switch (c)
        {
          case '^': // North
            position.Y--;
            break;
          case 'v': // South
            position.Y++;
            break;
          case '<': // West
            position.X--;
            break;
          case '>': // East
            position.X++;
            break;
        }
        var house = 
          from p in houses.Keys 
          where p.X == position.X && p.Y == position.Y 
          select p;

        if(house.Count() == 0) houses.Add(position, 1);
        else
        {
          houses[position]++;
        }
      }

      total = houses.Count();

      result = total.ToString();
      return result;
    }
  }
}
