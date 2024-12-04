namespace AdventOfCodeNet9._2016.Day_03
{
  internal class Part_1 : Days
  {
    /// <summary>
    /*
    https://adventofcode.com/2016/day/3
--- Day 3: Squares With Three Sides ---
Now that you can think clearly, you move deeper into the labyrinth of hallways and office furniture that makes up this part of Easter Bunny HQ. This must be a graphic design department; the walls are covered in specifications for triangles.

Or are they?

The design document gives the side lengths of each triangle it describes, but... 5 10 25? Some of these aren't triangles. You can't help but mark the impossible ones.

In a valid triangle, the sum of any two sides must be larger than the remaining side. For example, the "triangle" given above is impossible, because 5 + 10 is not larger than 25.

In your puzzle input, how many of the listed triangles are possible?
    */
    /// </summary>
    /// <returns>
    /// Your puzzle answer was 917.
    /// </returns>
    public override string Execute()
    {
      string result = "";

      int count = 0;

      foreach (var line in Lines)
      {
        var chunks = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

        int a = Int32.Parse(chunks[0]);
        int b = Int32.Parse(chunks[1]); 
        int c = Int32.Parse(chunks[2]);

        int longest = Math.Max(a, Math.Max(b, c));

        if (longest < a + b + c - longest)
        {
          count++;
        }
      }

      result = count.ToString();
      return result;
    }
  }
}
