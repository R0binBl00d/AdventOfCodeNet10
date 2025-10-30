using System.Security.Cryptography;
namespace AdventOfCodeNet10._2016.Day_05
{
  internal class Part_1_2016_Day_05 : Days
  {
    /// <summary>
    /*
    https://adventofcode.com/2016/day/5
    --- Day 5: How About a Nice Game of Chess? ---
    You are faced with a security door designed by Easter Bunny engineers that
    seem to have acquired most of their security knowledge by watching hacking
    movies.
    
    The eight-character password for the door is generated one character at a
    time by finding the MD5 hash of some Door ID (your puzzle input)
    and an increasing integer index (starting with 0).
    
    A hash indicates the next character in the password if its hexadecimal
    representation starts with five zeroes. If it does, the sixth character
    in the hash is the next character of the password.
    
    For example, if the Door ID is abc:
    
    The first index which produces a hash that starts with five zeroes is 3231929,
    which we find by hashing abc3231929;
    the sixth character of the hash, and thus the first character of the password,
    is 1.
    
    5017308 produces the next interesting hash, which starts with 000008f82..., so
    the second character of the password is 8.
    
    The third time a hash starts with five zeroes is for abc5278568, discovering
    the character f.
    In this example, after continuing this search a total of eight times, the
    password is 18f47a30.
    
    Given the actual Door ID, what is the password?    
	*/
    /// </summary>
    /// <returns>
    /// Your puzzle answer was f77a0e6e.
    /// </returns>
    public override string Execute()
    {
      string result = "";
      md5 = MD5.Create();

      int index = 0;
      foreach (string hash in GetHashes(Int32.MaxValue))
      {
        if (hash.StartsWith("00000"))
        {
          result += hash[5];
          index++;

          if (index >= 8) break;
        }
      }

      return result;
    }

    private MD5 md5;

    IEnumerable<string> GetHashes(int max)
    {
      for (int i = 0; i < max; i++)
      {

        string crack = Lines[0] + i.ToString();
        List<byte> bytes = new List<byte>();

        foreach (char c in crack)
        {
          bytes.Add((byte)c);
        }
        var computeHash = md5.ComputeHash(bytes.ToArray());

        string hexHash = "";
        foreach (byte b in computeHash)
        {
          hexHash += b.ToString("x2");
        }
        yield return hexHash;
      }
    }
  }
}
