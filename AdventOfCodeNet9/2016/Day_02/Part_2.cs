namespace AdventOfCodeNet9._2016.Day_02
{
  internal class Part_2 : Days
  {
    /// <summary>
    /*
    https://adventofcode.com/2016/day/2
--- Day 2: Bathroom Security ---
You arrive at Easter Bunny Headquarters under cover of darkness. However, you left in such a rush that you forgot to use the bathroom! Fancy office buildings like this one usually have keypad locks on their bathrooms, so you search the front desk for the code.

"In order to improve security," the document you find says, "bathroom codes will no longer be written down. Instead, please memorize and follow the procedure below to access the bathrooms."

The document goes on to explain that each button to be pressed can be found by starting on the previous button and moving to adjacent buttons on the keypad: U moves up, D moves down, L moves left, and R moves right. Each line of instructions corresponds to one button, starting at the previous button (or, for the first line, the "5" button); press whatever button you're on at the end of each line. If a move doesn't lead to a button, ignore it.

You can't hold it much longer, so you decide to figure out the code as you walk to the bathroom. You picture a keypad like this:

1 2 3
4 5 6
7 8 9
Suppose your instructions are:

ULL
RRDDD
LURDL
UUUUD
You start at "5" and move up (to "2"), left (to "1"), and left (you can't, and stay on "1"), so the first button is 1.
Starting from the previous button ("1"), you move right twice (to "3") and then down three times (stopping at "9" after two moves and ignoring the third), ending up with 9.
Continuing from "9", you move left, up, right, down, and left, ending with 8.
Finally, you move up four times (stopping at "2"), then down once, ending with 5.
So, in this example, the bathroom code is 1985.

Your puzzle input is the instructions from the document you found at the front desk. What is the bathroom code?

Your puzzle answer was 56983.

The first half of this puzzle is complete! It provides one gold star: *

--- Part Two ---
You finally arrive at the bathroom (it's a several minute walk from the lobby so visitors can behold the many fancy conference rooms and water coolers on this floor) and go to punch in the code. Much to your bladder's dismay, the keypad is not at all like you imagined it. Instead, you are confronted with the result of hundreds of man-hours of bathroom-keypad-design meetings:

    1
  2 3 4
5 6 7 8 9
  A B C
    D
You still start at "5" and stop when you're at an edge, but given the same instructions as above, the outcome is very different:

You start at "5" and don't move at all (up and left are both edges), ending at 5.
Continuing from "5", you move right twice and down three times (through "6", "7", "B", "D", "D"), ending at D.
Then, from "D", you move five more times (through "D", "B", "C", "C", "B"), ending at B.
Finally, after five more moves, you end at 3.
So, given the actual keypad layout, the code would be 5DB3.

Using the same instructions in your puzzle input, what is the correct bathroom code?
    */
    /// </summary>
    /// <returns>
    /// Your puzzle answer was 8B8B1.
    /// </returns>
    public override string Execute()
    {
      int[,] numbers = new int[5, 5]
      {
        {0,0,1,0,0},
        {0,2,3,4,0},
        {5,6,7,8,9},
        {0,10,11,12,0},
        {0,0,13,0,0}
      };

      var fingerOn = new Point(0, 2);
      //int heading = 0; // 0 North -y, 1 East +x, 2 South +y, 3 West -x

      string result = "";
      foreach (var line in Lines)
      {
        // find button
        foreach (var c in line)
        {
          switch (c)
          {
            case 'U':
              GoStep(ref fingerOn, ref numbers, 0, 1);
              break;
            case 'D':
              GoStep(ref fingerOn, ref numbers, 2, 1);
              break;
            case 'L':
              GoStep(ref fingerOn, ref numbers, 3, 1);
              break;
            case 'R':
              GoStep(ref fingerOn, ref numbers, 1, 1);
              break;
          }
        }
        result += String.Format("{0:X}", numbers[fingerOn.Y, fingerOn.X]);
      }

      return result;
    }

    private void GoStep(ref Point p, ref int[,] numbers, int heading, int distance)
    {
      int steps = distance;
      // 0 North -y, 1 East +x, 2 South +y, 3 West -x

      Point start = new Point(p.X, p.Y);

      switch (heading)
      {
        case 0:
          p.Y = p.Y - steps;
          if (p.Y < 0) p.Y = 0;
          break;
        case 1:
          p.X = p.X + steps;
          if (p.X > 4) p.X = 4;
          break;
        case 2:
          p.Y = p.Y + steps;
          if (p.Y > 4) p.Y = 4;
          break;
        case 3:
          p.X = p.X - steps;
          if (p.X < 0) p.X = 0;
          break;
      }
      if (numbers[p.Y, p.X] == 0) p = start;
    }
  }
}
