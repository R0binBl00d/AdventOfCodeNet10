namespace AdventOfCodeNet10._2016.Day_03
{
  internal class Part_2_2016_Day_03 : Days
  {
    /// <summary>
    /*
    https://adventofcode.com/2016/day/3
    --- Day 3: Squares With Three Sides ---
    Now that you can think clearly, you move deeper into the labyrinth of hallways
    and office furniture that makes up this part of Easter Bunny HQ. This must be a
    graphic design department; the walls are covered in specifications for
    triangles.
    
    Or are they?
    
    The design document gives the side lengths of each triangle it describes,
    but... 5 10 25? Some of these aren't triangles. You can't help but mark the
    impossible ones.
    
    In a valid triangle, the sum of any two sides must be larger than the remaining
    side. For example, the "triangle" given above is impossible, because 5 + 10 is
    not larger than 25.
    
    In your puzzle input, how many of the listed triangles are possible?
    
    Your puzzle answer was 917.
    
    The first half of this puzzle is complete! It provides one gold star: *
    
    --- Part Two ---
    Now that you've helpfully marked up their design documents, it occurs to you
    that triangles are specified in groups of three vertically. Each set of three
    numbers in a column specifies a triangle. Rows are unrelated.
    
    For example, given the following specification, numbers with the same hundreds
    digit would be part of the same triangle:
    
    101 301 501
    102 302 502
    103 303 503
    201 401 601
    202 402 602
    203 403 603
    In your puzzle input, and instead reading by columns, how many of the listed
    triangles are possible?
    */
    /// </summary>
    /// <returns>
    /// 1058 too low
    /// </returns>
    public override string Execute()
    {
      string result = "";

      int count = 0;

      int rollover = 0;
      int[,] numbers = new int[3, 3];

      foreach (var line in Lines)
      {
        var chunks = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

        int a = Int32.Parse(chunks[0]);
        int b = Int32.Parse(chunks[1]);
        int c = Int32.Parse(chunks[2]);

        numbers[rollover, 0] = a;
        numbers[rollover, 1] = b;
        numbers[rollover, 2] = c;

        rollover++;
        if (rollover == 3)
        {
          // check three triangls
          for (int i = 0; i < 3; i++)
          {
            int longest = Math.Max(numbers[0, i], Math.Max(numbers[1, i], numbers[2, i]));

            if (longest < (numbers[0, i] + numbers[1, i] + numbers[2, i] - longest))
            {
              count++;
            }
          }
          rollover = 0;
        }
      }

      result = count.ToString();
      return result;
    }
  }
}
