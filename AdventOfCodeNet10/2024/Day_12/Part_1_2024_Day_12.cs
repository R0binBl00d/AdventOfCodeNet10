using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using AdventOfCodeNet10.Extensions;

namespace AdventOfCodeNet10._2024.Day_12
{
  internal class Part_1_2024_Day_12 : Days
  {
    /// <summary>
    /*
    https://adventofcode.com/2024/day/12
    --- Day 12: Garden Groups ---
    Why not search for the Chief Historian near the gardener and his massive farm?
    There's plenty of food, so The Historians grab something to eat while they
    search.
    
    You're about to settle near a complex arrangement of garden plots when some
    Elves ask if you can lend a hand. They'd like to set up fences around each
    region of garden plots, but they can't figure out how much fence they need to
    order or how much it will cost. They hand you a map (your puzzle input) of the
    garden plots.
    
    Each garden plot grows only a single type of plant and is indicated by a single
    letter on your map. When multiple garden plots are growing the same type of
    plant and are touching (horizontally or vertically), they form a region. For
    example:
    
    AAAA
    BBCD
    BBCC
    EEEC
    This 4x4 arrangement includes garden plots growing five different types of
    plants (labeled A, B, C, D, and E), each grouped into their own region.
    
    In order to accurately calculate the cost of the fence around a single region,
    you need to know that region's area and perimeter.
    
    The area of a region is simply the number of garden plots the region contains.
    The above map's type A, B, and C plants are each in a region of area 4. The
    type E plants are in a region of area 3; the type D plants are in a region of
    area 1.
    
    Each garden plot is a square and so has four sides. The perimeter of a region
    is the number of sides of garden plots in the region that do not touch another
    garden plot in the same region. The type A and C plants are each in a region
    with perimeter 10. The type B and E plants are each in a region with perimeter
    8. The lone D plot forms its own region with perimeter 4.
    
    Visually indicating the sides of plots in each region that contribute to the
    perimeter using - and |, the above map's regions' perimeters are measured as
    follows:
    
    +-+-+-+-+
    |A A A A|
    +-+-+-+-+     +-+
    .             |D|
    +-+-+   +-+   +-+
    |B B|   |C|
    +   +   + +-+
    |B B|   |C C|
    +-+-+   +-+ +
    .         |C|
    +-+-+-+   +-+
    |E E E|
    +-+-+-+
    Plants of the same type can appear in multiple separate regions, and regions
    can even appear within other regions. For example:
    
    OOOOO
    OXOXO
    OOOOO
    OXOXO
    OOOOO
    The above map contains five regions, one containing all of the O garden plots,
    and the other four each containing a single X plot.
    
    The four X regions each have area 1 and perimeter 4. The region containing 21
    type O plants is more complicated; in addition to its outer edge contributing a
    perimeter of 20, its boundary with each X region contributes an additional 4 to
    its perimeter, for a total perimeter of 36.
    
    Due to "modern" business practices, the price of fence required for a region is
    found by multiplying that region's area by its perimeter. The total price of
    fencing all regions on a map is found by adding together the price of fence for
    every region on the map.
    
    In the first example, region A has price 4 * 10 = 40, region B has price 4 * 8
    = 32, region C has price 4 * 10 = 40, region D has price 1 * 4 = 4, and region
    E has price 3 * 8 = 24. So, the total price for the first example is 140.
    
    In the second example, the region with all of the O plants has price 21 * 36 =
    756, and each of the four smaller X regions has price 1 * 4 = 4, for a total
    price of 772 (756 + 4 + 4 + 4 + 4).
    
    Here's a larger example:
    
    RRRRIICCFF
    RRRRIICCCF
    VVRRRCCFFF
    VVRCCCJFFF
    VVVVCJJCFE
    VVIVCCJJEE
    VVIIICJJEE
    MIIIIIJJEE
    MIIISIJEEE
    MMMISSJEEE
    It contains:
    
    A region of R plants with price 12 * 18 = 216.
    A region of I plants with price 4 * 8 = 32.
    A region of C plants with price 14 * 28 = 392.
    A region of F plants with price 10 * 18 = 180.
    A region of V plants with price 13 * 20 = 260.
    A region of J plants with price 11 * 20 = 220.
    A region of C plants with price 1 * 4 = 4.
    A region of E plants with price 13 * 18 = 234.
    A region of I plants with price 14 * 22 = 308.
    A region of M plants with price 5 * 12 = 60.
    A region of S plants with price 3 * 8 = 24.
    So, it has a total price of 1930.
    
    What is the total price of fencing all regions on your map?

    */
    /// </summary>
    /// <returns>
    /// (1930) Test
    /// 1465112
    /// </returns>
    public override string Execute()
    {
      string result = "";
      long totalCount = 0;

      var fMap = new Dictionary<(int x, int y), (int id, char flower)>();
      var fPatches = new Dictionary<int, Patch>();

      int id = 0;

      #region creates more patches than required
      for (int y = 0; y < Lines.Count; y++)
      {
        for (int x = 0; x < Lines[0].Length; x++)
        {
          //Debug.WriteLine($"y: '{y}' x: '{x}'");
          if
          (!(
            // check Top
            TryInsertIntoExistingPatch((x, y), (x, y - 1), ref fMap, ref fPatches) ||
            // check Left
            TryInsertIntoExistingPatch((x, y), (x - 1, y), ref fMap, ref fPatches)
          ))
          {
            fMap.Add((x, y), (id, Lines[y][x]));
            fPatches.Add(id, new Patch(Lines[y][x], 1, null, new List<LongPoint>() { new LongPoint(x, y) }));
            id++;
          }
        }
      }
      #endregion creates more patches than required

      #region filter for connected patches
      bool neighboringPatchesWithSameLetter = true;
      while (neighboringPatchesWithSameLetter)
      {
        neighboringPatchesWithSameLetter = false;
        bool breaking = false;

        for (int y = 0; y < Lines.Count; y++)
        {
          for (int x = 0; x < Lines[0].Length; x++)
          {
            //Debug.WriteLine($"y: '{y}' x: '{x}'");
            List<int> neighbors = GetIDsFromNeighborsDistict((x, y), ref fMap);
            if (neighbors.Count > 1)
            {
              fPatches[neighbors[0]].area += fPatches[neighbors[1]].area;
              foreach (var p in fPatches[neighbors[1]].points)
              {
                fMap[((int)p.x, (int) p.y)] = (neighbors[0], fPatches[neighbors[0]].flower);
                fPatches[neighbors[0]].points.Add(new LongPoint(p.x, p.y));
              }

              fPatches.Remove(neighbors[1]);
              neighboringPatchesWithSameLetter = true;
              breaking = true;
              break;
            }
          }
          if (breaking) break;
        }
      }
      #endregion filter for connected patches

      foreach (var patch in fPatches.Values)
      {
        patch.perimeter = 0;
        foreach (var p in patch.points)
        {
          patch.perimeter += patch.points.Contains(new LongPoint(p.x, p.y - 1)) ? 0 : 1;
          patch.perimeter += patch.points.Contains(new LongPoint(p.x + 1, p.y)) ? 0 : 1;
          patch.perimeter += patch.points.Contains(new LongPoint(p.x - 1, p.y)) ? 0 : 1;
          patch.perimeter += patch.points.Contains(new LongPoint(p.x, p.y + 1)) ? 0 : 1;
        }
      }

      // Calculate result:
      foreach (var patch in fPatches.Values)
      {
        totalCount += patch.area * (long)patch.perimeter;
      }

      result = totalCount.ToString();
      return result;
    }

    private List<int> GetIDsFromNeighborsDistict((int x, int y) cl/*Current Location*/, ref Dictionary<(int x, int y), (int id, char flower)> fMap)
    {
      List<int> ids = new List<int>() { fMap[(cl.x, cl.y)].id };

      (int x, int y) spot;

      for (int index = 0; index < 4; index++)
      {
        switch (index)
        {
          case 0: // Top
            spot = (cl.x, cl.y - 1);
            break;
          case 1: // Right
            spot = (cl.x + 1, cl.y);
            break;
          case 2: // Left
            spot = (cl.x - 1, cl.y);
            break;
          default: // Bottom
            spot = (cl.x, cl.y + 1);
            break;
        }
        if ((spot.x.InRange(0, Lines[0].Length, IncludeBounds.Lower)) &&
          spot.y.InRange(0, Lines.Count, IncludeBounds.Lower))
        {
          if (
            ids[0] != fMap[(spot.x, spot.y)].id && // different id
            fMap[(cl.x, cl.y)].flower == fMap[(spot.x, spot.y)].flower // same flower
          )
          {
            ids.Add(fMap[(spot.x, spot.y)].id);
          }
        }
      }

      return ids.Distinct().ToList();
    }

    private bool TryInsertIntoExistingPatch(
      (int x, int y) current,
      (int x, int y) reference,
      ref Dictionary<(int x, int y), (int id, char flower)> fMap,
      ref Dictionary<int, Patch> fPatches)
    {
      if (reference.x < 0 || //.InRange(0, Lines[0].Length, IncludeBounds.Lower))
          reference.y < 0)   //.InRange(0, Lines.Count, IncludeBounds.Lower))
        return false;

      if (fMap.ContainsKey((reference.x, reference.y)))
      {
        var spot = fMap[(reference.x, reference.y)];
        if (spot.flower == Lines[current.y][current.x])
        {
          if (!fMap.ContainsKey((current.x, current.y))) fMap.Add((current.x, current.y), (spot.id, spot.flower));
          fPatches[spot.id].area++;
          fPatches[spot.id].points.Add(new LongPoint(current.x, current.y));
          return true;
        }
      }
      return false;
    }
  }

  class Patch
  {
    public Patch(char flower, int area, int? perimeter, List<LongPoint> points)
    {
      this.flower = flower;
      this.area = area;
      this.perimeter = perimeter;
      this.points = points;
    }

    public char flower { get; init; }
    public int area { get; set; }
    public int? perimeter { get; set; }
    public List<LongPoint> points { get; set; }

    public void Deconstruct(out char flower, out int area, out int? perimeter, out List<LongPoint> points)
    {
      flower = this.flower;
      area = this.area;
      perimeter = this.perimeter;
      points = this.points;
    }
  }
}
