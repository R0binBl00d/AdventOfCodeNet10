using System.Security.Cryptography;

namespace AdventOfCodeNet9._2015.Day_04
{
  internal class Part_1_2015_Day_04 : Days
  {
    /// <summary>
    /*
    https://adventofcode.com/2015/day/4
    --- Day 4: The Ideal Stocking Stuffer ---
    Santa needs help mining some AdventCoins (very similar to bitcoins) to use as
    gifts for all the economically forward-thinking little girls and boys.
    
    To do this, he needs to find MD5 hashes which, in hexadecimal, start with at
    least five zeroes. The input to the MD5 hash is some secret key (your puzzle
    input, given below) followed by a number in decimal. To mine AdventCoins, you
    must find Santa the lowest positive number (no leading zeroes: 1, 2, 3, ...)
    that produces such a hash.
    
    For example:
    
    If your secret key is abcdef, the answer is 609043, because the MD5 hash of
    abcdef609043 starts with five zeroes (000001dbbfa...), and it is the lowest
    such number to do so.
    If your secret key is pqrstuv, the lowest number it combines with to make an
    MD5 hash starting with five zeroes is 1048970; that is, the MD5 hash of
    pqrstuv1048970 looks like 000006136ef....
    
     */
    /// </summary>
    /// <returns>
    /// Your puzzle answer was 117946.
    /// </returns>
    public override string Execute()
    {
      string result = "";

      MD5 md5 = MD5.Create();

      byte[] computeHash = new byte[0];

      for (int i = 1; i < 123456789; i++)
      {
        // ckczppom

        string crack = Lines[0] + i.ToString();
        List<byte> bytes = new List<byte>();

        foreach (char c in crack)
        {
          bytes.Add((byte)c);
        }
        computeHash = md5.ComputeHash(bytes.ToArray());

        if (
          computeHash[0] == 0 &&
          computeHash[1] == 0 &&
          computeHash[2] < 16 
          )
        {
          result = i.ToString();
          break;
        }
      }

      return result;
    }
  }
}
