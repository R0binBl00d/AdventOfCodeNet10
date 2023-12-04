using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCodeNet8._2016.Day_07
{
  internal class Part_2 : Days
  {
    /// <summary>
    /*
    https://adventofcode.com/2016/day/7

    --- Day 7: Internet Protocol Version 7 ---
    While snooping around the local network of EBHQ, you compile a list of IP addresses (they're IPv7, of course; IPv6 is much too limited). You'd like to figure out which IPs support TLS (transport-layer snooping).

    An IP supports TLS if it has an Autonomous Bridge Bypass Annotation, or ABBA. An ABBA is any four-character sequence which consists of a pair of two different characters followed by the reverse of that pair, such as xyyx or abba. However, the IP also must not have an ABBA within any hypernet sequences, which are contained by square brackets.

    For example:

    abba[mnop]qrst supports TLS (abba outside square brackets).
    abcd[bddb]xyyx does not support TLS (bddb is within square brackets, even though xyyx is outside square brackets).
    aaaa[qwer]tyui does not support TLS (aaaa is invalid; the interior characters must be different).
    ioxxoj[asdfgh]zxcvbn supports TLS (oxxo is outside square brackets, even though it's within a larger string).
    How many IPs in your puzzle input support TLS?

    Your puzzle answer was 110.

    The first half of this puzzle is complete! It provides one gold star: *

    --- Part Two ---
    You would also like to know which IPs support SSL (super-secret listening).

    An IP supports SSL if it has an Area-Broadcast Accessor, or ABA, 
    anywhere in the supernet sequences (outside any square bracketed sections), 
    and a corresponding Byte Allocation Block, or BAB, anywhere in the hypernet sequences. 
    
    An ABA is any three-character sequence which consists of the same character twice with a different character between them, 
    such as xyx or aba. 
    
    A corresponding BAB is the same characters but in reversed positions: 
    yxy and bab, respectively.

    For example:

    aba[bab]xyz supports SSL (aba outside square brackets with corresponding bab within square brackets).
    xyx[xyx]xyx does not support SSL (xyx, but no corresponding yxy).
    aaa[kek]eke supports SSL (eke in supernet with corresponding kek in hypernet; the aaa sequence is not related, because the interior character must be different).
    zazbz[bzb]cdb supports SSL (zaz has no corresponding aza, but zbz has a corresponding bzb, even though zaz and zbz overlap).

    How many IPs in your puzzle input support SSL?
    */
    /// </summary>
    /// <returns>
    /// 473 is too high
    /// 385 is too high
    /// 377 is too high
    /// 243 not correct f**k !! (including examples)
    /// 240 not correct f**k !! (excluding examples)
    /// 
    /// </returns>
    public override string Execute()
    {
      string result = "";

      int NoOfCorrectStrings = 0;

      foreach (var line in Lines)
      {
        bool validSLS = false;
        // parse strings to put them in Lists
        List<KeyValuePair<string, int>> outsideBrackets = new List<KeyValuePair<string, int>>();
        List<KeyValuePair<string, int>> insideBrackets = new List<KeyValuePair<string, int>>();

        StringBuilder sb = new StringBuilder();
        int blockIndex = 0;
        foreach (char c in line)
        {
          sb.Append(c);

          if (c == '[')
          {
            string text = sb.ToString().Trim('[', ']');
            outsideBrackets.Add(new KeyValuePair<string, int>(text, blockIndex));
            blockIndex++;
            sb.Clear();
          }
          else if (c == ']')
          {
            string text = sb.ToString().Trim('[', ']');
            insideBrackets.Add(new KeyValuePair<string, int>(text, blockIndex));
            blockIndex++;
            sb.Clear();
          }
        }
        if (sb.Length > 0)
        {
          string text = sb.ToString().Trim('[', ']');
          outsideBrackets.Add(new KeyValuePair<string, int>(text, blockIndex));
          sb.Clear();
        }

        //var chunks = line.Split('[', ']');

        // check outside
        foreach (var istr in outsideBrackets)
        {
          if (HasABA(istr, out var babList))
          {
            foreach (var bab in babList)
            {
              foreach (var str in insideBrackets) // Hypertext is inside !!
              {
                if (str.Key.Contains(bab.Key))
                {
                  string testStr = str.Key;
                  // doubleCheck Problem was  "kiki" kik found, searched for iki and also found overlapping :-/
                  if (str.Value == istr.Value) // indication for overlapping !! same textblock !
                  {
                    int matchIndex = bab.Value.Index; // Length always 3
                    testStr = testStr.Remove(matchIndex, 3);
                    // actual doubeCheck
                    validSLS = (testStr.Contains(bab.Key));
                    if (validSLS) break;
                    else continue;
                  }

                  validSLS = true;
                  break;
                }
              }
              if (validSLS) break;
            }
            if (validSLS) break;
          }
        }

        if (validSLS == true)
        {
          NoOfCorrectStrings++;
        }
      }
      result = NoOfCorrectStrings.ToString();
      return result;
    }

    private bool HasABA(KeyValuePair<string, int> input, out List<KeyValuePair<string, Match>> bab)
    {
      string pattern = @"(.)(.)\1";

      int correctMatches = 0;
      bab = new List<KeyValuePair<string, Match>>();

      for (int i = 0; i < input.Key.Length - 2;)
      {
        var match = Regex.Match(input.Key.Substring(i), pattern);


        if (match.Success && match.Groups[1].Value != match.Groups[2].Value) // Ensuring the inner character is different from the outer one
        {
          string bybString = String.Format("{0}{1}{0}", match.Value[1], match.Value[0]);
          var res = from kvp in bab where kvp.Key == bybString select kvp;
          if (res == null || res.Count() == 0)
          {
            bab.Add(new KeyValuePair<string, Match>(bybString, match));
            correctMatches++;
          }

          i += match.Index + 1;
        }
        else
        {
          break;
        }
      }

      return (correctMatches > 0);
    }
  }
}
