namespace AdventOfCodeNet10._2023.Day_01
{
  internal class Part_2_2023_Day_01 : Days
  {
    /// <summary>
    /*
    https://adventofcode.com/2023/day/1
    --- Day 1: Trebuchet?! ---
    Something is wrong with global snow production, and you've been selected to
    take a look. The Elves have even given you a map; on it, they've used stars to
    mark the top fifty locations that are likely to be having problems.
    
    You've been doing this long enough to know that to restore snow operations, you
    need to check all fifty stars by December 25th.
    
    Collect stars by solving puzzles. Two puzzles will be made available on each
    day in the Advent calendar; the second puzzle is unlocked when you complete the
    first. Each puzzle grants one star. Good luck!
    
    You try to ask why they can't just use a weather machine ("not powerful
    enough") and where they're even sending you ("the sky") and why your map looks
    mostly blank ("you sure ask a lot of questions") and hang on did you just say
    the sky ("of course, where do you think snow comes from") when you realize that
    the Elves are already loading you into a trebuchet ("please hold still, we need
    to strap you in").
    
    As they're making the final adjustments, they discover that their calibration
    document (your puzzle input) has been amended by a very young Elf who was
    apparently just excited to show off her art skills. Consequently, the Elves are
    having trouble reading the values on the document.
    
    The newly-improved calibration document consists of lines of text; each line
    originally contained a specific calibration value that the Elves now need to
    recover. On each line, the calibration value can be found by combining the
    first digit and the last digit (in that order) to form a single two-digit
    number.
    
    For example:
    
    1abc2
    pqr3stu8vwx
    a1b2c3d4e5f
    treb7uchet
    In this example, the calibration values of these four lines are 12, 38, 15, and
    77. Adding these together produces 142.
    
    Consider your entire calibration document. What is the sum of all of the
    calibration values?
    
    Your puzzle answer was 54304.
    
    The first half of this puzzle is complete! It provides one gold star: *
    
    --- Part Two ---
    Your calculation isn't quite right. It looks like some of the digits are
    actually spelled out with letters:
    one, two, three, four, five, six, seven, eight, and nine also count as valid
    "digits".
    
    Equipped with this new information, you now need to find the real first and
    last digit on each line. For example:
    
    two1nine
    eightwothree
    abcone2threexyz
    xtwone3four
    4nineeightseven2
    zoneight234
    7pqrstsixteen
    In this example, the calibration values are 29, 83, 13, 24, 42, 14, and 76.
    Adding these together produces 281.
    
    What is the sum of all of the calibration values?
    */
    /// </summary>
    /// <returns>
    /// 54418
    /// </returns>
    public override string Execute()
    {
      string result = "";

      int sum = 0;

      foreach (var line in Lines)
      {
        int number = 0;

        // first digit
        for (int i = 0; i < line.Length; i++)
        {
          if (Int32.TryParse(line[i].ToString(), out var num))
          {
            number = num * 10;
            break;
          }
          else
          {
            string text = line.Substring(i);
            if (text.StartsWith("one"))
            {
              number = 10;
              break;
            }
            if (text.StartsWith("two"))
            {
              number = 20;
              break;
            }
            if (text.StartsWith("three"))
            {
              number = 30;
              break;
            }
            if (text.StartsWith("four"))
            {
              number = 40;
              break;
            }
            if (text.StartsWith("five"))
            {
              number = 50;
              break;
            }
            if (text.StartsWith("six"))
            {
              number = 60;
              break;
            }
            if (text.StartsWith("seven"))
            {
              number = 70;
              break;
            }
            if (text.StartsWith("eight"))
            {
              number = 80;
              break;
            }
            if (text.StartsWith("nine"))
            {
              number = 90;
              break;
            }
          }
        }

        //second digit
        for (int i = line.Length - 1; i >= 0; i--)
        {
          if (Int32.TryParse(line[i].ToString(), out var num))
          {
            number += num;
            break;
          }
          else
          {
            string text = line.Substring(i);
            if (text.StartsWith("one"))
            {
              number += 1;
              break;
            }
            if (text.StartsWith("two"))
            {
              number += 2;
              break;
            }
            if (text.StartsWith("three"))
            {
              number += 3;
              break;
            }
            if (text.StartsWith("four"))
            {
              number += 4;
              break;
            }
            if (text.StartsWith("five"))
            {
              number += 5;
              break;
            }
            if (text.StartsWith("six"))
            {
              number += 6;
              break;
            }
            if (text.StartsWith("seven"))
            {
              number += 7;
              break;
            }
            if (text.StartsWith("eight"))
            {
              number += 8;
              break;
            }
            if (text.StartsWith("nine"))
            {
              number += 9;
              break;
            }
          }
        }

        sum += number;
      }

      result = sum.ToString();
      return result;
    }
  }
}
