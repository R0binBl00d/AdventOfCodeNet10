
/// Define the 'Card' as a global using alias for a tuple
/// This needs to be at the top of the file

global using MapOrGrid = (int width, int height);

internal static class MapOrGridExt
{
  extension(MapOrGrid map)
  {
    internal int Size
    {
      get { return map.width * map.height; }
    }
  }
}
