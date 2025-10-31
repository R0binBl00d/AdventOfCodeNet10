using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCodeNet10.Extensions
{
  internal class LongPoint
  {
    public LongPoint(long X, long Y, int obj = 0)
    {
      this.x = X;
      this.y = Y;
      this.tag = obj;
    }

    public long x { get; set; }
    public long y { get; set; }
    public int tag { get; set; }

    public void Deconstruct(out long X, out long Y)
    {
      X = this.x;
      Y = this.y;
    }

    public override bool Equals(object? obj)
    {
      if (obj is LongPoint other) return other.x == this.x && other.y == this.y;
      return false;

      //LongPoint? other = obj as LongPoint;
      //if (other == null) return false;
      //return other.x == this.x && other.y == this.y;
    }

    public override int GetHashCode()
    {
      return this.x.GetHashCode() ^ this.y.GetHashCode();
    }

    public new Type GetType()
    {
      return typeof(LongPoint);
    }

    public override string ToString()
    {
      return $"x:'{x}' y:'{y}'";
    }
  }
}
