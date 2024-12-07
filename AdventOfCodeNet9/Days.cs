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

      List<string> temp = sb.ToString().Split(new char[] { '\r', '\n' },
        StringSplitOptions.RemoveEmptyEntries).ToList<string>();

      Lines = new List<string>(temp.Count);

      foreach (var line in temp)
      {
        var newTrim = line.Trim();
        newTrim = newTrim.Trim('ï', '»', '¿');
        Lines.Add(newTrim);
      }
      temp.Clear();
    }

    public abstract string Execute();
  }
}
