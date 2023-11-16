using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace AdventOfCodeNet8._2016.Day_04
{
  internal class Part_2 : Days
  {
    /// <summary>
    /*
    https://adventofcode.com/2016/day/4
    --- Day 4: Security Through Obscurity ---
    Finally, you come across an information kiosk with a list of rooms. Of course, the list is encrypted and full of decoy data, but the instructions to decode the list are barely hidden nearby. Better remove the decoy data first.

    Each room consists of an encrypted name (lowercase letters separated by dashes) followed by a dash, a sector ID, and a checksum in square brackets.

    A room is real (not a decoy) if the checksum is the five most common letters in the encrypted name, in order, with ties broken by alphabetization. For example:

    aaaaa-bbb-z-y-x-123[abxyz] is a real room because the most common letters are a (5), b (3), and then a tie between x, y, and z, which are listed alphabetically.
    a-b-c-d-e-f-g-h-987[abcde] is a real room because although the letters are all tied (1 of each), the first five are listed alphabetically.
    not-a-real-room-404[oarel] is a real room.
    totally-real-room-200[decoy] is not.
    Of the real rooms from the list above, the sum of their sector IDs is 1514.

    What is the sum of the sector IDs of the real rooms?

    Your puzzle answer was 173787.

    The first half of this puzzle is complete! It provides one gold star: *

    --- Part Two ---
    With all the decoy data out of the way, it's time to decrypt this list and get moving.

    The room names are encrypted by a state-of-the-art shift cipher, 
    which is nearly unbreakable without the right software. 
    
    However, the information kiosk designers at Easter Bunny HQ were not expecting to deal 
    with a master cryptographer like yourself.

    To decrypt a room name, rotate each letter forward through the alphabet a number of times 
    equal to the room's sector ID. A becomes B, B becomes C, Z becomes A, and so on. Dashes become spaces.

    For example, the real name for qzmt-zixmtkozy-ivhz-343 is very encrypted name.

    What is the sector ID of the room where North Pole objects are stored?     */
    /// </summary>
    /// <returns>
    /// 548
    /// </returns>
    public override string Execute()
    {
      int sumSectorIds = 0;

      string result = "";
      foreach (var line in Lines)
      {
        var chunks = line.Split(new char[] { '[', ']' }, StringSplitOptions.RemoveEmptyEntries);
        if (ProcessChSum(chunks[0]) == chunks[1])
        {
          int index = chunks[0].LastIndexOf("-");
          int id = Int32.Parse(chunks[0].Substring(index + 1));

          string decrypted = DecryptName(chunks[0].Substring(0, index), id);
          if (decrypted.Contains("north") || decrypted.Contains("pole"))
          {
            sumSectorIds = id;
            break;
          }
        }
      }

      result = sumSectorIds.ToString();
      return result;
    }

    private string DecryptName(string cypher, int id)
    {
      int range = (int)'z' - (int)'a' + 1;
      var decrypted = cypher.ToCharArray();
      int i = 0;
      foreach (char c in cypher)
      {
        decrypted[i++] = c == '-' ? ' ' : (char)(((((int)c) - (int)'a' + id) % range) + (int)'a');
      }

      return new string(decrypted);
    }

    private string ProcessChSum(string s)
    {
      List<KeyValuePair<int, char>> list = new List<KeyValuePair<int, char>>();

      var distOrderedChars = s.ToCharArray()
        .Where(x => (int)x >= (int)'a' && (int)x <= (int)'z')
        .Distinct().OrderBy(c => (int)c);

      foreach (char c in distOrderedChars)
      {
        list.Add(new KeyValuePair<int, char>(s.ToCharArray().Count(x => x == c), c));
      }

      var temp = list.OrderByDescending(x => x.Key);

      string result = "";
      for (int i = 0; i < 5; i++)
      {
        result += temp.ElementAt(i).Value;
      }
      return result;
    }
  }
}
