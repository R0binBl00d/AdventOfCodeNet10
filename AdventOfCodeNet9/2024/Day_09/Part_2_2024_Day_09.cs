using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace AdventOfCodeNet9._2024.Day_09
{
  internal class Part_2_2024_Day_09 : Days
  {
    /// <summary>
    /*
    https://adventofcode.com/2024/day/9

--- Part Two ---
Upon completion, two things immediately become clear. First, the disk definitely has a lot more contiguous free space, just like the amphipod hoped. Second, the computer is running much more slowly! Maybe introducing all of that file system fragmentation was a bad idea?

The eager amphipod already has a new plan: rather than move individual blocks, he'd like to try compacting the files on his disk by moving whole files instead.

This time, attempt to move whole files to the leftmost span of free space blocks that could fit the file. Attempt to move each file exactly once in order of decreasing file ID number starting with the file with the highest file ID number. If there is no span of free space to the left of a file that is large enough to fit the file, the file does not move.

The first example from above now proceeds differently:

00...111...2...333.44.5555.6666.777.888899
0099.111...2...333.44.5555.6666.777.8888..
0099.1117772...333.44.5555.6666.....8888..
0099.111777244.333....5555.6666.....8888..
00992111777.44.333....5555.6666.....8888..
The process of updating the filesystem checksum is the same; now, this example's checksum would be 2858.

Start over, now compacting the amphipod's hard drive using this new method instead. What is the resulting filesystem checksum?

    */
    /// </summary>
    /// <returns>
    /// (2858) Test
    /// 6334655979668 -> Part_1
    /// 8518174061514 -> too high :-/  line 98 ... he optimized files to the end of the file => needed " && spaces[si].pos < lastFile.pos"
    /// 6349492251099
    /// </returns>
    public override string Execute()
    {
      string result = "";
      long totalCount = 0;

      var files = new List<(int pos, int id, int size)>();
      var spaces = new List<(int pos, int size)>();

      bool isFile = false; // free and File
      int fileID = 0;
      int filePos = 0;

      #region Read Input
      foreach (var lchar in Lines[0])
      {
        int digit = Int32.Parse(lchar.ToString());
        isFile = !isFile;

        if (isFile)
        {
          if (digit > 0)
          {
            files.Add((filePos, fileID, digit));
            fileID++;
          }
          else
          {
            Debugger.Break();
          }
        }
        else
        {
          if (digit > 0)
          {
            spaces.Add((filePos, digit));
          }
        }
        filePos += digit;
      }
      #endregion Read Input

      #region Sort Data

      //DebugGenerateDataLine(files, spaces);

      var newFiles = new List<(int pos, int id, int size)>(files);

      files.Clear();

      // get last Files
      for (int fi = newFiles.Count()-1; fi > 0; fi--)
      {
        var lastFile = newFiles[fi];

        for (int si = 0; si < spaces.Count; si++)
        {
          int delta = spaces[si].size - lastFile.size;
          if (delta >= 0 && spaces[si].pos < lastFile.pos)
          {
            lastFile.pos = spaces[si].pos;

            (int pos, int size) newSpace = (spaces[si].pos + lastFile.size, delta);
            spaces.RemoveAt(si);
            if (delta > 0)
            {
              spaces.Insert(si, newSpace);
            }
            break;
          }
        }
        files.Add(lastFile);
      }

      #endregion Sort Data

      #region calculate result

      //var combined = files
      //  .Select(f => (f.pos, (int?)f.id, f.size))
      //  .Concat<(int pos, int? id, int size)>(spaces.Select(s => (s.pos, (int?)null, s.size)))
      //  .OrderBy(item => item.pos)
      //  .ToList();

      filePos = 0;
      foreach (var block in files)
      {
        filePos = block.pos;
        for (int i = 0; i < block.size; i++)
        {
          totalCount += (long)filePos * (long)block.id;
          filePos++;
        }
      }
      #endregion calculate result

      result = totalCount.ToString();
      return result;
    }

    private void DebugGenerateDataLine(List<(int pos, int id, int size)> filess, List<(int pos, int size)> spacess)
    {
      var files = new List<(int pos, int id, int size)>(filess);
      var spaces = new List<(int pos, int size)>(spacess);

      var combined = files
        .Select(f => (f.pos, (int?)f.id, f.size))
        .Concat<(int pos, int? id, int size)>(spaces.Select(s => (s.pos, (int?)null, s.size)))
        .OrderBy(item => item.pos)
        .ToList();

      StringBuilder sb = new StringBuilder();
      int tempPosition = 0;
      foreach (var chunks in combined)
      {
        if (tempPosition != chunks.pos) Debugger.Break();

        for (int i = 0; i < chunks.size; i++)
        {
          if (chunks.id == null)
          {
            Debug.Write(".");
          }
          else
          {
            Debug.Write($"{chunks.id}");
          }
          tempPosition++;
        }
      }
      Debug.WriteLine("");
    }
  }
}
