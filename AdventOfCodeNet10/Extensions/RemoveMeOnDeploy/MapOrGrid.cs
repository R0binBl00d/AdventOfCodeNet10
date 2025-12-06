
using System.Collections;
using AdventOfCodeNet10.Extensions;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

/// Define the 'Card' as a global using alias for a tuple
/// This needs to be at the top of the file

//global using MapOrGrid = (int width, int height);

//internal static class MapOrGridExt
//{
//  extension(MapOrGrid map)
//  {
//    internal int Size
//    {
//      get { return map.width * map.height; }
//    }
//  }
//}

internal class MapOrGrid
{
  private int width;
  private int height;

  public int Width => width;
  public int Height => height;

  public char[,] Tiles { get; set; }

  public LongPoint Location { get; set; }

  public LongPoint StartPosition { get; }

  public LongPoint EndPosition { get; }

  #region Constructors

  public MapOrGrid(long width, long height)
  {
    this.width = (int)width;
    this.height = (int)height;

    Tiles = new char[width, height];
  }

  public MapOrGrid(long width, long height, LongPoint startPosition, LongPoint endPosition) :
    this(width, height)
  {
    this.StartPosition = startPosition;
    this.EndPosition = endPosition;
    this.Location = startPosition;
  }

  public MapOrGrid(long width, long height, LongPoint startPosition, LongPoint endPosition, LongPoint currentLocation) :
    this(width, height, startPosition, endPosition)
  {
    this.Location = currentLocation;
  }

  #endregion Constructors

  #region Controls

  public void ResetLocation()
  {
    this.Location = this.StartPosition;
  }

  public bool IsAtEnd()
  {
    return this.Location.Equals(this.EndPosition);
  }

  public bool IsValidLocation(LongPoint point)
  {
    return point.x >= 0 && point.x < this.Width && point.y >= 0 && point.y < this.Height;
  }

  public void InitializeMap(List<string> map)
  {
    if (map == null || !map.Any())
    {
      throw new ArgumentException("Map cannot be null or empty.", nameof(map));
    }

    this.height = map.Count;
    this.width = map[0].Length;

    SetTiles(map);
  }

  /// <summary>
  /// Gets the total number of units represented by the width and height.
  /// </summary>
  public long Size => this.Width * this.Height;

  public bool SetTiles(List<string> map)
  {
    if (map == null || !map.Any())
    {
      return false;
    }

    this.Tiles = new char[this.Width, this.Height];


    if (map.Count != this.Height)
    {
      // The input map does not match the specified width
      throw new ArgumentException($"List has an incorrect number of rows.\r\nExpected {this.Width}, got {map.Count}.\r\nIf this was intensionally, try InitializeMap instead",
        nameof(map));
      return false;
    }

    for (long y = 0; y < this.Height; y++)
    {
      var row = map[(int)y];
      if (row.Length != this.Width)
      {
        // The input map does not match the specified width
        throw new ArgumentException($"Row {y} has an incorrect length.\r\nExpected {this.Width}, got {row.Length}.\r\nIf this was intensionally, try InitializeMap instead",
          nameof(map));
        return false;
      }

      for (long x = 0; x < this.Width; x++)
      {
        this.Tiles[x, y] = row[(int)x];
      }
    }

    return true;
  }

  public IEnumerable<(LongPoint, char)> TilesList
  {
    get
    {
      for (long y = 0; y < this.Height; y++)
      {
        for (long x = 0; x < this.Width; x++)
        {
          yield return (new LongPoint(x, y), this.Tiles[(int)x, (int)y]);
        }
      }
    }
  }
  
  public void PlotTheMapUsingTiles()
  {
    StringBuilder sb = new StringBuilder();
    
    for (var y = 0; y < Height; y++)
    {
      for (var x = 0; x < Width; x++)
      {
        var point = new LongPoint(x, y);
        var tile = GetTile(point);
        sb.Append(tile);
      }
      sb.AppendLine();
    }
    sb.AppendLine();
    Debug.Write(sb.ToString());
    sb.Clear();
  }  

  public char GetTile(LongPoint point)
  {
    if (!IsValidLocation(point))
    {
      throw new ArgumentOutOfRangeException(nameof(point), "Point is outside the grid boundaries.");
    }

    return this.Tiles[(int)point.x, (int)point.y];
  }

  public void SetTile(LongPoint point, char tile)
  {
    if (!IsValidLocation(point))
    {
      throw new ArgumentOutOfRangeException(nameof(point), "Point is outside the grid boundaries.");
    }

    this.Tiles[(int)point.x, (int)point.y] = tile;
  }

  #endregion Controls

  #region Neighbors

  public IEnumerable<LongPoint> GetNeighbors(LongPoint point, bool PacManMode = false)
  {
    //var neighbors = new List<LongPoint>();

    //var north = new LongPoint(point.x, point.y - 1);
    //if (IsValidLocation(north))
    //{
    //  neighbors.Add(north);
    //}
    //else if (PacManMode)
    //{
    //  neighbors.Add(new LongPoint(north.x, this.Height - 1));
    //}

    //var east = new LongPoint(point.x + 1, point.y);
    //if (IsValidLocation(east))
    //{
    //  neighbors.Add(east);
    //}
    //else if (PacManMode)
    //{
    //  neighbors.Add(new LongPoint(0, east.y));
    //}

    //var south = new LongPoint(point.x, point.y + 1);
    //if (IsValidLocation(south))
    //{
    //  neighbors.Add(south);
    //}
    //else if (PacManMode)
    //{
    //  neighbors.Add(new LongPoint(south.x, 0));
    //}

    //var west = new LongPoint(point.x - 1, point.y);
    //if (IsValidLocation(west))
    //{
    //  neighbors.Add(west);
    //}
    //else if (PacManMode)
    //{
    //  neighbors.Add(new LongPoint(this.Width - 1, west.y));
    //}

    //return neighbors;
    var neighbors = new List<LongPoint>();

    var directions = new[]
    {
      new LongPoint(0, -1), // North
      new LongPoint(1, 0), // East
      new LongPoint(0, 1), // South
      new LongPoint(-1, 0) // West
    };

    foreach (var direction in directions)
    {
      var neighbor = new LongPoint(point.x + direction.x, point.y + direction.y);

      if (IsValidLocation(neighbor))
      {
        neighbors.Add(neighbor);
      }
      else if (PacManMode)
      {
        var wrappedNeighbor = new LongPoint((neighbor.x + Width) % Width, (neighbor.y + Height) % Height);
        neighbors.Add(wrappedNeighbor);
      }
    }

    // sanity check, default should be 4 cardinal
    if (PacManMode)
    {
      if (neighbors.Count != 4) Debugger.Break();
    }
    else
    {
      // it goes from 2 in corners, 3 on borders to 4 in the middle
      if (neighbors.Count < 2 || neighbors.Count > 4) Debugger.Break();
    }

    return neighbors;
  }

  public IEnumerable<LongPoint> GetNeighborsIncludingDiagonals(LongPoint point, bool PacManMode = false)
  {
    var neighbors = new List<LongPoint>();

    var directions = new[]
    {
      new LongPoint(-1, -1), // Northwest
      new LongPoint(1, -1), // Northeast
      new LongPoint(1, 1), // Southeast
      new LongPoint(-1, 1), // Southwest
    };

    foreach (var direction in directions)
    {
      var neighbor = new LongPoint(point.x + direction.x, point.y + direction.y);

      if (IsValidLocation(neighbor))
      {
        neighbors.Add(neighbor);
      }
      else if (PacManMode)
      {
        var wrappedNeighbor = new LongPoint((neighbor.x + Width) % Width, (neighbor.y + Height) % Height);
        neighbors.Add(wrappedNeighbor);
      }
    }

    // Add the cardinal neighbors as well
    neighbors.AddRange(GetNeighbors(point, PacManMode));

    // sanity check, default should be 4 cardinal + 4 diagonal = 8 total
    if (PacManMode)
    {
      if (neighbors.Count != 8) Debugger.Break();
    }
    else
    {
      // it goes from 3 in corners, 5 on borders to 8 in the middle
      if (neighbors.Count < 3 || neighbors.Count > 8) Debugger.Break();
    }

    return neighbors;
  }

  #endregion Neighbors

  #region Move Methods

  /// <summary>
  /// PacManMode makes the movement wrap around the edges
  /// otherwise it does not move if it would go out of bounds
  /// </summary>
  /// <param name="PacManMode"></param>
  /// <returns></returns>
  public LongPoint MoveNorth(bool PacManMode = false)
  {
    var current = this.Location;
    var nextLocation = new LongPoint(current.x, current.y - 1);

    if (nextLocation.y < 0)
    {
      nextLocation = PacManMode ? new LongPoint(nextLocation.x, this.Height - 1) : current;
    }

    this.Location = nextLocation;
    return this.Location;
  }

  /// <summary>
  /// PacManMode makes the movement wrap around the edges
  /// otherwise it does not move if it would go out of bounds
  /// </summary>
  /// <param name="PacManMode"></param>
  /// <returns></returns>
  public LongPoint MoveEast(bool PacManMode = false)
  {
    var current = this.Location;
    var nextLocation = new LongPoint(current.x + 1, current.y);

    if (nextLocation.x >= this.Width)
    {
      nextLocation = PacManMode ? new LongPoint(0, nextLocation.y) : current;
    }

    this.Location = nextLocation;
    return this.Location;
  }

  /// <summary>
  /// PacManMode makes the movement wrap around the edges
  /// otherwise it does not move if it would go out of bounds
  /// </summary>
  /// <param name="PacManMode"></param>
  /// <returns></returns>
  public LongPoint MoveSouth(bool PacManMode = false)
  {
    var current = this.Location;
    var nextLocation = new LongPoint(current.x, current.y + 1);

    if (nextLocation.y >= this.Height)
    {
      nextLocation = PacManMode ? new LongPoint(nextLocation.x, 0) : current;
    }

    this.Location = nextLocation;
    return this.Location;
  }

  /// <summary>
  /// PacManMode makes the movement wrap around the edges
  /// otherwise it does not move if it would go out of bounds
  /// </summary>
  /// <param name="PacManMode"></param>
  /// <returns></returns>
  public LongPoint MoveWest(bool PacManMode = false)
  {
    var current = this.Location;
    var nextLocation = new LongPoint(current.x - 1, current.y);

    if (nextLocation.x < 0)
    {
      nextLocation = PacManMode ? new LongPoint(this.Width - 1, current.y) : current;
    }

    this.Location = nextLocation;
    return this.Location;
  }

  #endregion Move Methods
}
