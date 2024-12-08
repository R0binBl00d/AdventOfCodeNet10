namespace AdventOfCodeNet9
{

  public static class StringArrayExtensions
  {
    /// <summary>
    /// Converts an array of strings to an array of longs, using long.Parse.
    /// If any of the strings cannot be parsed, this will throw a FormatException.
    /// </summary>
    public static long[] AsInt64s(this string[] source)
    {
      return source.Select(long.Parse).ToArray();
    }

    /// <summary>
    /// Converts an array of strings to a List<long>, using long.Parse.
    /// If any of the strings cannot be parsed, this will throw a FormatException.
    /// </summary>
    public static List<long> AsInt64List(this string[] source)
    {
      return source.Select(long.Parse).ToList();
    }

    /// <summary>
    /// Converts an array of strings to a List<long>, using long.TryParse.
    /// Values that cannot be parsed default to the specified defaultValue.
    /// </summary>
    public static List<long> AsInt64ListOrDefault(this string[] source, long defaultValue = 0)
    {
      var result = new List<long>(source.Length);
      foreach (var s in source)
      {
        if (long.TryParse(s, out long val))
        {
          result.Add(val);
        }
        else
        {
          result.Add(defaultValue);
        }
      }
      return result;
    }
  }
}
