namespace AdventOfCodeNet10.Extensions
{
  /// <summary>
  /*
  - Adding record simplifies your code by automatically generating common methods
    (Equals, GetHashCode, ToString, Deconstruct) and enabling features like with
  expressions.
    
  - If you need custom behavior (e.g., overriding ToString or Equals), you can
  still define those methods explicitly.
    
  - Records are ideal for scenarios where you need immutable, value-based data
  models.If mutability is required, you can still use record but must explicitly
  define set accessors for properties.
  */
  /// </summary>
  /// <param name="x"></param>
  /// <param name="y"></param>
  /// <param name="tag"></param>
  public record class LongPoint(long x, long y, int tag = 0)
  {
    public long x { get; set; } = x;
    public long y { get; set; } = y;
    public int tag { get; set; } = tag;

    // add an additional Deconstruct method to ignore the tag field
    // one already exists automatically that includes all fields because this is a record
    public void Deconstruct(out long X, out long Y)
    {
      X = this.x;
      Y = this.y;
    }

    // override of System.Object.Equals(object obj) because record classes implement IEquatable<T>
    // need a specific one that ignores the tag field
    public virtual bool Equals(LongPoint? other)
    {
      if (other is null) return false;
      if (ReferenceEquals(this, other)) return true;
      return other.x == this.x && other.y == this.y;
    }

    public override int GetHashCode() => HashCode.Combine(this.x, this.y);

    //public override string ToString() => $"x:'{this.x}' y:'{this.y}'";
  }

}
