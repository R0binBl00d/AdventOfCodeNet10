using System.Text;

namespace AdventOfCodeNet9
{
  public abstract class Days
  {
    public required List<string> Lines;
    private StringBuilder sb = null!;

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
