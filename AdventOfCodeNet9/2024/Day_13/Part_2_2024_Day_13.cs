using System.Diagnostics;

namespace AdventOfCodeNet9._2024.Day_13
{
  internal class Part_2_2024_Day_13 : Days
  {
    /// <summary>
    /*
    https://adventofcode.com/2024/day/13

    --- Part Two ---
    As you go to win the first prize, you discover that the claw is nowhere near
    where you expected it would be. Due to a unit conversion error in your
    measurements, the position of every prize is actually 10000000000000 higher on
    both the X and Y axis!
    
    Add 10000000000000 to the X and Y position of every prize. After making this
    change, the example above would now look like this:
    
    Button A: X+94, Y+34
    Button B: X+22, Y+67
    Prize: X=10000000008400, Y=10000000005400
    
    Button A: X+26, Y+66
    Button B: X+67, Y+21
    Prize: X=10000000012748, Y=10000000012176
    
    Button A: X+17, Y+86
    Button B: X+84, Y+37
    Prize: X=10000000007870, Y=10000000006450
    
    Button A: X+69, Y+23
    Button B: X+27, Y+71
    Prize: X=10000000018641, Y=10000000010279
    Now, it is only possible to win a prize on the second and fourth claw machines.
    Unfortunately, it will take many more than 100 presses to do so.
    
    Using the corrected prize coordinates, figure out how to win as many prizes as
    possible. What is the fewest tokens you would have to spend to win all possible
    prizes?

    */
    /// </summary>
    /// <returns>
    /// 
    /// </returns>
    public override string Execute()
    {
      string result = "";
      long totalCount = 0;

      var machines =
        new List<(point Move_A, point Move_B, point prize, List<(long a, long b)> candidates)>();

      for (int line = 0; line < Lines.Count; line += 3)
      {
        var moveA = Lines[line + 0].Split(new[] { ',', '+', '=' });
        var moveB = Lines[line + 1].Split(new[] { ',', '+', '=' });
        var prize = Lines[line + 2].Split(new[] { ',', '+', '=' });
        machines.Add((
          new point(Int64.Parse(moveA[1]), Int64.Parse(moveA[3])),
          new point(Int64.Parse(moveB[1]), Int64.Parse(moveB[3])),
          //                                 10000000000000                             10000000000000
          new point(Int64.Parse(prize[1])+ 10000000000000L, Int64.Parse(prize[3])+ 10000000000000L),
          new List<(long a, long b)>()
        ));
      }

      foreach (var machine in machines)
      {
        var pos = new point(0, 0);
        var max_a_x = machine.prize.X / machine.Move_A.X + 1;
        var max_b_x = machine.prize.X / machine.Move_B.X + 1;

        Debug.WriteLine($"A.X:{machine.Move_A.X}, A.Y:{machine.Move_A.Y}");
        Debug.WriteLine($"B.X:{machine.Move_B.X}, B.Y:{machine.Move_B.Y}");
        Debug.WriteLine($"P.X:{machine.prize.X}, P.Y:{machine.prize.Y}");

        for (long a = 0; a < max_a_x; a++) // press A
        {
          for (long b = 0; b < max_b_x; b++) // press B
          {
            pos.X = a * machine.Move_A.X + b * machine.Move_B.X;
            pos.Y = a * machine.Move_A.Y + b * machine.Move_B.Y;

            if ((pos.X > machine.prize.X) || (pos.Y > machine.prize.Y)) continue;
            else if ((pos.X == machine.prize.X) && (pos.Y == machine.prize.Y))
            {
              machine.candidates.Add((a, b));
            }
          }

          if ((pos.X > machine.prize.X) || (pos.Y > machine.prize.Y)) continue;
        }
      }

      foreach (var machine in machines)
      {
        if (machine.candidates.Count > 0)
          totalCount += machine.candidates.Select(c => (long)c.a * 3 + c.b * 1).Order().First();
      }

      result = totalCount.ToString();
      return result;
    }
  }

  class point
  {
    public point(long X, long Y)
    {
      this.X = X;
      this.Y = Y;
    }

    public long X { get; set; }
    public long Y { get; set; }

    public void Deconstruct(out long X, out long Y)
    {
      X = this.X;
      Y = this.Y;
    }
  }
}