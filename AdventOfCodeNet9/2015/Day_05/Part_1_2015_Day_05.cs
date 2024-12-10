namespace AdventOfCodeNet9._2015.Day_05
{
  internal class Part_1_2015_Day_05 : Days
  {
    /// <summary>
    /*
    https://adventofcode.com/2015/day/5
    --- Day 5: Doesn't He Have Intern-Elves For This? ---
    Santa needs help figuring out which strings in his text file are naughty or
    nice.
    
    A nice string is one with all of the following properties:
    
    It contains at least three vowels (aeiou only), like aei, xazegov, or
    aeiouaeiouaeiou.
    It contains at least one letter that appears twice in a row, like xx, abcdde
    (dd), or aabbccdd (aa, bb, cc, or dd).
    It does not contain the strings ab, cd, pq, or xy, even if they are part of one
    of the other requirements.
    For example:
    
    ugknbfddgicrmopn is nice because it has at least three vowels (u...i...o...), a
    double letter (...dd...), and none of the disallowed substrings.
    aaa is nice because it has at least three vowels and a double letter, even
    though the letters used by different rules overlap.
    jchzalrnumimnmhp is naughty because it has no double letter.
    haegwjzuvuyypxyu is naughty because it contains the string xy.
    dvszwmarrgswjxmb is naughty because it contains only one vowel.
    How many strings are nice?
    
     */
    /// </summary>
    /// <returns>
    /// Your puzzle answer was 255.
    /// </returns>
    public override string Execute()
    {
      string result = "";

      int niceStrings = 0;

      foreach (string line in Lines)
      {
        if (
          line.Contains("ab") ||
          line.Contains("cd") ||
          line.Contains("pq") ||
          line.Contains("xy")
        ) continue;

        char lastChar = ' ';
        bool notFound = true;
        foreach (char c in line)
        {
          if (c == lastChar)
          {
            notFound = false;
            break;
          }
          lastChar = c;
        }
        if (notFound) continue;

        var vowels_as = from c in line where c == 'a' select c;
        var vowels_es = from c in line where c == 'e' select c;
        var vowels_is = from c in line where c == 'i' select c;
        var vowels_os = from c in line where c == 'o' select c;
        var vowels_us = from c in line where c == 'u' select c;

        int vowels = 
          vowels_as.Count() + 
          vowels_es.Count() +
          vowels_is.Count() +
          vowels_os.Count() +
          vowels_us.Count();

        if ( /*I got here and*/ vowels >= 3)
        {
          niceStrings++;
        }
      }

      result = niceStrings.ToString();
      return result;
    }
  }
}
