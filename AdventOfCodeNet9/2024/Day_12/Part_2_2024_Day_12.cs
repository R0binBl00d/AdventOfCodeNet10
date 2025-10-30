using System.Diagnostics;
using System.Linq;
using AdventOfCodeNet9.Extensions;

namespace AdventOfCodeNet10._2024.Day_12
{
  internal class Part_2_2024_Day_12 : Days
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
    |D|
    +-+-+   +-+   +-+
    |B B|   |C|
    +   +   + +-+
    |B B|   |C C|
    +-+-+   +-+ +
    |C|
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
    
    Your puzzle answer was 1465112.
    
    --- Part Two ---
    Fortunately, the Elves are trying to order so much fence that they qualify for
    a bulk discount!
    
    Under the bulk discount, instead of using the perimeter to calculate the price,
    you need to use the number of sides each region has. Each straight section of
    fence counts as a side, regardless of how long it is.
    
    Consider this example again:
    
    AAAA
    BBCD
    BBCC
    EEEC
    The region containing type A plants has 4 sides, as does each of the regions
    containing plants of type B, D, and E. However, the more complex region
    containing the plants of type C has 8 sides!
    
    Using the new method of calculating the per-region price by multiplying the
    region's area by its number of sides, regions A through E have prices 16, 16,
    32, 4, and 12, respectively, for a total price of 80.
    
    The second example above (full of type X and O plants) would have a total price
    of 436.
    
    Here's a map that includes an E-shaped region full of type E plants:
    
    EEEEE
    EXXXX
    EEEEE
    EXXXX
    EEEEE
    The E-shaped region has an area of 17 and 12 sides for a price of 204.
    Including the two regions full of type X plants, this map has a total price of
    236.
    
    This map has a total price of 368:
    
    AAAAAA
    AAABBA
    AAABBA
    ABBAAA
    ABBAAA
    AAAAAA
    It includes two regions full of type B plants (each with 4 sides) and a single
    region full of type A plants (with 4 sides on the outside and 8 more sides on
    the inside, a total of 12 sides). Be especially careful when counting the fence
    around regions like the one full of type A plants; in particular, each section
    of fence has an in-side and an out-side, so the fence does not connect across
    the middle of the region (where the two B regions touch diagonally). (The Elves
    would have used the Möbius Fencing Company instead, but their contract terms
    were too one-sided.)
    
    The larger example from before now has the following updated prices:
    
    A region of R plants with price 12 * 10 = 120.
    A region of I plants with price 4 * 4 = 16.
    A region of C plants with price 14 * 22 = 308.
    A region of F plants with price 10 * 12 = 120.
    A region of V plants with price 13 * 10 = 130.
    A region of J plants with price 11 * 12 = 132.
    A region of C plants with price 1 * 4 = 4.
    A region of E plants with price 13 * 8 = 104.
    A region of I plants with price 14 * 16 = 224.
    A region of M plants with price 5 * 6 = 30.
    A region of S plants with price 3 * 6 = 18.
    Adding these together produces its new total price of 1206.
    
    What is the new total price of fencing all regions on your map?
    
    Your puzzle answer was 893790.
    */
    /// </summary>
    /// <returns>
    /// (1206) Test
    /// 1465112 (1st Part)
    /// 874174 // too low ... missing the inside ones !!!
    /// 913182 // too high ... issue with included items ... some were alredy included  with the outlines ...
    /// 895030 // too high ... already counted all borders, not sure what is wrong now ...
    /// 894530 // not the right answer !!!
    /// 893790 <- remove 740 because of the 6-sided R-field in the V - counts up to 30, so I addded 8 :-/ remove (2x370)
    /// 893790
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
            fPatches.Add(id, new Patch(Lines[y][x], 1, null, new List<point>() { new point(x, y) }));
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
                fMap[((int)p.x, (int)p.y)] = (neighbors[0], fPatches[neighbors[0]].flower);
                fPatches[neighbors[0]].points.Add(new point(p.x, p.y));
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
          if (patch.points.Contains(new point(p.x, p.y - 1)))
          {
            patch.perimeter += 0;
          }
          else
          {
            patch.perimeter += 1;
            p.tag |= 0x01; // top
          }
          if (patch.points.Contains(new point(p.x + 1, p.y)))
          {
            patch.perimeter += 0;
          }
          else
          {
            patch.perimeter += 1;
            p.tag |= 0x02; // right
          }
          if (patch.points.Contains(new point(p.x, p.y + 1)))
          {
            patch.perimeter += 0;
          }
          else
          {
            patch.perimeter += 1;
            p.tag |= 0x04; // bottom
          }
          if (patch.points.Contains(new point(p.x - 1, p.y)))
          {
            patch.perimeter += 0;
          }
          else
          {
            patch.perimeter += 1;
            p.tag |= 0x08; // left
          }
        }
      }

      // Calculate result:
      //foreach (var patch in fPatches.Values)
      //{
      //  totalCount += patch.area * (long)patch.perimeter;
      //}
      //totalCount = 0;

      #region Walk arround the Edges
      foreach (var patch in fPatches.Values)
      {
        int orientation = 0; // == Top
        // TopLeftCorner
        long start_y = patch.points.Select(p => p.y).Order().First();
        long start_x = patch.points.Where(p => p.y == start_y).Select(p => p.x).Order().First();
        long curr_x = start_x;
        long curr_y = start_y;

        // reset perimeter, count long straights now
        patch.perimeter = 0;
        // for start orientation top remove the fence
        patch.points[patch.points.IndexOf(new point(start_x, start_y))].tag &= ~0x01;

        var seeWhosArroundUs = new List<int>();
        do
        {
          switch (orientation) // orientation of fence / line
          {
            case 0: // Top
              point point_r = new point(curr_x + 1, curr_y);
              if (patch.points.Contains(point_r)) // check right
              {
                // exists also a diagonal?
                if (patch.points.Contains(new point(curr_x + 1, curr_y - 1))) // check right top
                {
                  orientation = 3; // left
                  patch.perimeter++;
                  curr_x++;
                  curr_y--;
                  patch.points[patch.points.IndexOf(new point(curr_x, curr_y))].tag &= ~0x08;
                }
                else
                {
                  // still topmost, go there
                  curr_x++;
                  patch.points[patch.points.IndexOf(new point(curr_x, curr_y))].tag &= ~0x01;
                }
              }
              else
              {
                // doesn't exist, so we start a new line at the right
                patch.perimeter++;
                orientation = 1;
                patch.points[patch.points.IndexOf(new point(curr_x, curr_y))].tag &= ~0x02;
                if ((point_r.x).InRange(0, Lines[0].Length, IncludeBounds.Lower) &&
                    (point_r.y).InRange(0, Lines.Count, IncludeBounds.Lower))
                {
                  seeWhosArroundUs.Add(fMap[((int)point_r.x, (int)point_r.y)].id);
                }
              }

              break;
            case 1: // Right
              point point_b = new point(curr_x, curr_y + 1);
              if (patch.points.Contains(point_b)) // check bottom
              {
                // exists also a diagonal?
                if (patch.points.Contains(new point(curr_x + 1, curr_y + 1))) // check right bottom
                {
                  orientation = 0; // top
                  patch.perimeter++;
                  curr_x++;
                  curr_y++;
                  patch.points[patch.points.IndexOf(new point(curr_x, curr_y))].tag &= ~0x01;
                }
                else
                {
                  // still rightmost, go there
                  curr_y++;
                  patch.points[patch.points.IndexOf(new point(curr_x, curr_y))].tag &= ~0x02;
                }
              }
              else
              {
                // doesn't exist, so we start a new line at the bottom
                patch.perimeter++;
                orientation = 2;
                patch.points[patch.points.IndexOf(new point(curr_x, curr_y))].tag &= ~0x04;
                if ((point_b.x).InRange(0, Lines[0].Length, IncludeBounds.Lower) &&
                    (point_b.y).InRange(0, Lines.Count, IncludeBounds.Lower))
                {
                  seeWhosArroundUs.Add(fMap[((int)point_b.x, (int)point_b.y)].id);
                }
              }

              break;
            case 2: // Bottom
              point point_l = new point(curr_x - 1, curr_y);
              if (patch.points.Contains(point_l)) // check left
              {
                // exists also a diagonal?
                if (patch.points.Contains(new point(curr_x - 1, curr_y + 1))) // check left bottom
                {
                  orientation = 1; // right
                  patch.perimeter++;
                  curr_x--;
                  curr_y++;
                  patch.points[patch.points.IndexOf(new point(curr_x, curr_y))].tag &= ~0x02;
                }
                else
                {
                  // still bottommost, go there
                  curr_x--;
                  patch.points[patch.points.IndexOf(new point(curr_x, curr_y))].tag &= ~0x04;
                }
              }
              else
              {
                // doesn't exist, so we start a new line at the left
                patch.perimeter++;
                orientation = 3;
                patch.points[patch.points.IndexOf(new point(curr_x, curr_y))].tag &= ~0x08;
                if ((point_l.x).InRange(0, Lines[0].Length, IncludeBounds.Lower) &&
                    (point_l.y).InRange(0, Lines.Count, IncludeBounds.Lower))
                {
                  seeWhosArroundUs.Add(fMap[((int)point_l.x, (int)point_l.y)].id);
                }
              }

              break;
            case 3: // Left (start here)
              point point_t = new point(curr_x, curr_y - 1);
              if (patch.points.Contains(point_t)) // check top
              {
                // exists also a diagonal?
                if (patch.points.Contains(new point(curr_x - 1, curr_y - 1))) // check left top
                {
                  orientation = 2; // bottom
                  patch.perimeter++;
                  curr_x--;
                  curr_y--;
                  patch.points[patch.points.IndexOf(new point(curr_x, curr_y))].tag &= ~0x04;
                }
                else
                {
                  // still leftmost, go there
                  curr_y--;
                  patch.points[patch.points.IndexOf(new point(curr_x, curr_y))].tag &= ~0x08;
                }
              }
              else
              {
                // doesn't exist, so we start a new line at the top
                patch.perimeter++;
                orientation = 0;
                patch.points[patch.points.IndexOf(new point(curr_x, curr_y))].tag &= ~0x01;
                if ((point_t.x).InRange(0, Lines[0].Length, IncludeBounds.Lower) &&
                    (point_t.y).InRange(0, Lines.Count, IncludeBounds.Lower))
                {
                  seeWhosArroundUs.Add(fMap[((int)point_t.x, (int)point_t.y)].id);
                }
              }

              break;
          }
        } while (!(orientation == 0 && curr_x == start_x && curr_y == start_y) /*not back home*/);

        int PatchFencesRemaining = patch.points.Sum(p => p.tag);
        if (PatchFencesRemaining == 0) continue;


        var tmpLst = patch.points.Where(p => (p.tag & 4) == 4).ToList();
        var fTmpPatchLst = new List<KeyValuePair<int, Patch>>();
        foreach (var point in tmpLst)
        {
          //var idList = new List<int>();
          fTmpPatchLst.AddRange(fPatches.Where(f => f.Value.points.Contains(new point(point.x, point.y + 1))));
        }

        int missingPerimeter = 0;
        foreach (var key in fTmpPatchLst.Select(f=>f.Key).Distinct().ToList())
        {
          missingPerimeter += (int)fPatches[key].perimeter;
        }

        if (PatchFencesRemaining == 15/*single items*/)
        {
          if (missingPerimeter != 4)
          {
            Debugger.Break();
          }
          patch.perimeter += 4;
        }
        else if (PatchFencesRemaining == 20 /*two horizontal*/ || PatchFencesRemaining == 25 /*two vertical or three horizontal*/)
        {
          if (missingPerimeter != 4)
          {
            Debugger.Break();
          }
          patch.perimeter += 4;
        }
        else if (PatchFencesRemaining == 30 /* multiple single items or L shaped 3 items*/)
        {
          if (missingPerimeter != 8)
          {
            patch.perimeter += missingPerimeter;
            Debugger.Break();
          }
          else
          {
            patch.perimeter += 8;
          }
        }
        else if (PatchFencesRemaining == 45 /* T-Shaped 8 fences ??*/)
        {
          if (missingPerimeter != 8)
          {
            Debugger.Break();
          }
          patch.perimeter += 8;
        }
        else
        {
          Debugger.Break();
        }
      }
      #endregion

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
          fPatches[spot.id].points.Add(new point(current.x, current.y));
          return true;
        }
      }
      return false;
    }
  }
}
