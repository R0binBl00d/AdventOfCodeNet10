namespace AdventOfCodeNet9._2016.Day_06
{
  internal class Part_1 : Days
  {
    /// <summary>
    /*
    https://adventofcode.com/2016/day/6
    --- Day 6: Signals and Noise ---
    Something is jamming your communications with Santa. Fortunately, your signal is only partially jammed, and protocol in situations like this is to switch to a simple repetition code to get the message through.

    In this model, the same message is sent repeatedly. You've recorded the repeating message signal (your puzzle input), but the data seems quite corrupted - almost too badly to recover. Almost.

    All you need to do is figure out which character is most frequent for each position. For example, suppose you had recorded the following messages:

    eedadn
    drvtee
    eandsr
    raavrd
    atevrs
    tsrnev
    sdttsa
    rasrtv
    nssdts
    ntnada
    svetve
    tesnvt
    vntsnd
    vrdear
    dvrsen
    enarar
    The most common character in the first column is e; in the second, a; in the third, s, and so on. Combining these characters returns the error-corrected message, easter.

    Given the recording in your puzzle input, what is the error-corrected version of the message being sent?    */
    /// </summary>
    /// <returns>
    /// Your puzzle answer was qqqluigu.
    /// </returns>
    public override string Execute()
    {
      string result = "";

      List<List<char>> characters = new List<List<char>>();
      int lineLength = Lines[0].Length;

      char[] resultChars = new char[lineLength];

      for (int i = 0; i < lineLength; i++)
      {
        characters.Add(new List<char>());
      }

      foreach (var line in Lines)
      {
        for (int i = 0; i < lineLength; i++)
        {
          characters[i].Add(line[i]);
        }
      }

      for (int i = 0; i < lineLength; i++)
      {
        var distOrderedChars = characters[i]
          .Where(x => (int)x >= (int)'a' && (int)x <= (int)'z')
          .Distinct().OrderBy(c => (int)c);

        var list = new List<KeyValuePair<int, char>>();
        foreach (char c in distOrderedChars)
        {
          list.Add(new KeyValuePair<int, char>(characters[i].Count(x => x == c), c));
        }

        resultChars[i] = list.OrderByDescending(x => x.Key).First().Value;
      }

      result = new string(resultChars);
      return result;
    }
  }
}
