using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCodeNet8
{
  public abstract class Days
  {
    public List<string> Lines;
    private StringBuilder sb;

    public void Init(string input)
    {
      sb = new StringBuilder();

      using (FileStream fs = new FileStream(input, FileMode.Open))
      {
        while (fs.Position < fs.Length)
        {
          sb.Append((char)fs.ReadByte());
        }
      }

      Lines = sb.ToString().Split(new char[] { '\r', '\n' },
        StringSplitOptions.RemoveEmptyEntries).ToList<string>();
    }

    public abstract string Execute();
  }
}
