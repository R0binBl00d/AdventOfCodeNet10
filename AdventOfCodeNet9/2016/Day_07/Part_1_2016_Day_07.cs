using System.Text;
using System.Text.RegularExpressions;
namespace AdventOfCodeNet10._2016.Day_07
{
  internal class Part_1_2016_Day_07 : Days
  {
    /// <summary>
    /*
    https://adventofcode.com/2016/day/7
    --- Day 7: Internet Protocol Version 7 ---
    While snooping around the local network of EBHQ, you compile a list of IP
    addresses
    (they're IPv7, of course; IPv6 is much too limited).
    You'd like to figure out which IPs support TLS (transport-layer snooping).
    
    An IP supports TLS if it has an Autonomous Bridge Bypass Annotation, or ABBA.
    An ABBA is any four-character sequence which consists of a pair of two
    different characters followed by
    the reverse of that pair, such as xyyx or abba. However, the IP also must not
    have an ABBA within any hypernet sequences,
    which are contained by square brackets.
    
    For example:
    
    abba[mnop]qrst supports TLS (abba outside square brackets).
    abcd[bddb]xyyx does not support TLS (bddb is within square brackets, even
    though xyyx is outside square brackets).
    aaaa[qwer]tyui does not support TLS (aaaa is invalid; the interior characters
    must be different).
    ioxxoj[asdfgh]zxcvbn supports TLS (oxxo is outside square brackets, even though
    it's within a larger string).
    
    How many IPs in your puzzle input support TLS?
    */
    /// </summary>
    /// <returns>
    /// 90 is not the correct answer
    /// 117 too high // aaaa was accepted
    /// 110 That's the right answer!
    /// </returns>
    public override string Execute()
    {
      string result = "";

      int NoOfCorrectStrings = 0;

      foreach (var line in Lines)
      {
        bool validIP = true;
        // parse strings to put them in Lists
        List<string> outsideBrackets = new List<string>();
        List<string> insideBrackets = new List<string>();

        StringBuilder sb = new StringBuilder();
        foreach (char c in line)
        {
          sb.Append(c);

          if (c == '[')
          {
            outsideBrackets.Add(sb.ToString().Trim('[', ']'));
            sb.Clear();
          }
          else if (c == ']')
          {
            insideBrackets.Add(sb.ToString().Trim('[', ']'));
            sb.Clear();
          }
        }
        if(sb.Length > 0)
        {
          outsideBrackets.Add(sb.ToString().Trim('[', ']')); 
          sb.Clear();
        }

        //var chunks = line.Split('[', ']');

        // check middle part
        foreach (string istr in insideBrackets)
        {
          if (HasABBA(istr)) // wrong, do not count
          {
            validIP = false;
          }
        }

        int foundABBA = 0;
        foreach (string ostr in outsideBrackets)
        {
          foundABBA += (HasABBA(ostr) ? 1 : 0);
        }
        if (foundABBA > 0 && validIP == true)
        {
          NoOfCorrectStrings++;
        }
      }
      result = NoOfCorrectStrings.ToString();
      return result;
    }

    private bool HasABBA(string v)
    {
      string pattern = @"(.)(.)\2\1";

      MatchCollection matches = Regex.Matches(v, pattern);

      int correctMatches = 0;
      foreach (Match match in matches)
      {
        if (match.Groups[1].Value != match.Groups[2].Value) // Ensuring the inner character is different from the outer one
        {
          correctMatches++;
        }
      }

      return (correctMatches > 0);
    }
  }
}
