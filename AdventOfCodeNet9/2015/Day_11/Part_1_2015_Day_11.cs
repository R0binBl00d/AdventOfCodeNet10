namespace AdventOfCodeNet10._2015.Day_11
{
  internal class Part_1_2015_Day_11 : Days
  {
    /// <summary>
    /*
    https://adventofcode.com/2015/day/11
    --- Day 11: Corporate Policy ---
    Santa's previous password expired, and he needs help choosing a new one.
    
    To help him remember his new password after the old one expires, Santa has
    devised a method of coming up with a password based on the previous one.
    Corporate policy dictates that passwords must be exactly eight lowercase
    letters (for security reasons), so he finds his new password by incrementing
    his old password string repeatedly until it is valid.
    
    Incrementing is just like counting with numbers: xx, xy, xz, ya, yb, and so on.
    Increase the rightmost letter one step; if it was z, it wraps around to a, and
    repeat with the next letter to the left until one doesn't wrap around.
    
    Unfortunately for Santa, a new Security-Elf recently started, and he has
    imposed some additional password requirements:
    
    Passwords must include one increasing straight of at least three letters, like
    abc, bcd, cde, and so on, up to xyz. They cannot skip letters; abd doesn't
    count.
    Passwords may not contain the letters i, o, or l, as these letters can be
    mistaken for other characters and are therefore confusing.
    Passwords must contain at least two different, non-overlapping pairs of
    letters, like aa, bb, or zz.
    For example:
    
    hijklmmn meets the first requirement (because it contains the straight hij) but
    fails the second requirement requirement (because it contains i and l).
    abbceffg meets the third requirement (because it repeats bb and ff) but fails
    the first requirement.
    abbcegjk fails the third requirement, because it only has one double letter
    (bb).
    The next password after abcdefgh is abcdffaa.
    The next password after ghijklmn is ghjaabcc, because you eventually skip all
    the passwords that start with ghi..., since i is not allowed.
    Given Santa's current password (your puzzle input), what should his next
    password be?
    */
    /// </summary>
    /// <returns>
    /// Your puzzle answer was hepxxyzz.
    /// </returns>
    public override string Execute()
    {
      string result = "";

      string password = Lines[0].Trim();
      while (PasswordIsWrong(password))
      {
        password = incrementPassword(password);
      }

      result = password;
      return result;
    }

    private bool PasswordIsWrong(string password)
    {
      if (password.Contains('i') || password.Contains('o') || password.Contains('l')) return true;

      bool notPairsFound = true;
      bool notTripletsFound = true;
      string lastPair = "  ";
      string lastTriplet = "   ";
      char lastChar = ' ';

      foreach (char c in password)
      {
        lastTriplet = lastTriplet.Substring(1) + c;

        if (
          (int)lastTriplet[0] + 1 == (int)lastTriplet[1] &&
          (int)lastTriplet[1] + 1 == (int)lastTriplet[2]
           )
        {
          notTripletsFound = false;
        }

        if (c == lastChar)
        {
          if (lastPair == "  ")
          {
            lastPair = "" + lastChar + c;
          }
          else
          {
            if (lastPair != "" + lastChar + c)
            {
              notPairsFound = false;
            }
          }
        }

        lastChar = c;
      }

      if (notTripletsFound || notPairsFound)
      {
        return true; // Password is Wrong = true !!
      }
      else return false;
    }

    string incrementPassword(string password)
    {
      char[] chars = password.ToCharArray();

      for (int i = password.Length - 1; i >= 0; i--)
      {
        int charCode = password[i];
        if (charCode == (int)'z')
        {
          chars[i] = 'a';
          continue;
        }
        else
        {
          chars[i] = (char)(charCode + 1);
          break;
        }
      }
      string result = new string(chars);
      return result;
    }
  }
}
