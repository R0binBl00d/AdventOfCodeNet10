namespace AdventOfCodeNet10._2015.Day_06
{
  internal class Part_1_2015_Day_06 : Days
  {
    /// <summary>
    /*
    https://adventofcode.com/2015/day/6
    --- Day 6: Probably a Fire Hazard ---
    Because your neighbors keep defeating you in the holiday house decorating
    contest year after year, you've decided to deploy one million lights in a
    1000x1000 grid.
    
    Furthermore, because you've been especially nice this year, Santa has mailed
    you instructions on how to display the ideal lighting configuration.
    
    Lights in your grid are numbered from 0 to 999 in each direction; the lights at
    each corner are at 0,0, 0,999, 999,999, and 999,0. The instructions include
    whether to turn on, turn off, or toggle various inclusive ranges given as
    coordinate pairs. Each coordinate pair represents opposite corners of a
    rectangle, inclusive; a coordinate pair like 0,0 through 2,2 therefore refers
    to 9 lights in a 3x3 square. The lights all start turned off.
    
    To defeat your neighbors this year, all you have to do is set up your lights by
    doing the instructions Santa sent you in order.
    
    For example:
    
    turn on 0,0 through 999,999 would turn on (or leave on) every light.
    toggle 0,0 through 999,0 would toggle the first line of 1000 lights, turning
    off the ones that were on, and turning on the ones that were off.
    turn off 499,499 through 500,500 would turn off (or leave off) the middle four
    lights.
    After following the instructions, how many lights are lit?
    */
    /// </summary>
    /// <returns>
    /// Your puzzle answer was 543903.
    /// </returns>
    public override string Execute()
    {
      string result = "";

      int[,] lights = new int[1000, 1000];

      lights[0, 0] = 0;

      Point begin = new Point(0, 0);
      Point end = new Point(0, 0);

      foreach (string line in Lines)
      {
        var chunks = line.Split(' ');

        switch (chunks[0])
        {
          case "toggle":
            var coordToStart = chunks[1].Split(',');
            var coordToEnd = chunks[3].Split(',');

            for (int ox = Int32.Parse(coordToStart[0]); ox <= Int32.Parse(coordToEnd[0]); ox++)
            {
              for (int oy = Int32.Parse(coordToStart[1]); oy <= Int32.Parse(coordToEnd[1]); oy++)
              {
                lights[ox, oy] = 1 - lights[ox, oy];
              }
            }
            break;
          case "turn":
            var coordTuStart = chunks[2].Split(',');
            var coordTuEnd = chunks[4].Split(',');
            int currentAction = 0;
            if (chunks[1] == "on")
            {
              currentAction = 1;
            }
            else if (chunks[1] == "off")
            {
              currentAction = 0;
            }
            for (int ox = Int32.Parse(coordTuStart[0]); ox <= Int32.Parse(coordTuEnd[0]); ox++)
            {
              for (int oy = Int32.Parse(coordTuStart[1]); oy <= Int32.Parse(coordTuEnd[1]); oy++)
              {
                lights[ox, oy] = currentAction;
              }
            }
            break;
        }
      }

      int lightsOn = 0;
      for (int ox = 0; ox < 1000; ox++)
      {
        for (int oy = 0; oy < 1000; oy++)
        {
          if (lights[ox, oy] == 1) lightsOn++;
        }
      }

      // 542387  <- answer is too low :-)

      result = lightsOn.ToString();
      return result;
    }
  }
}
