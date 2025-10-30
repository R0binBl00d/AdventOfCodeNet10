namespace AdventOfCodeNet10._2023.Day_10
{
  internal class Part_1_2023_Day_10 : Days
  {
    /// <summary>
    /*
    https://adventofcode.com/2023/day/10
    --- Day 10: Pipe Maze ---
    You use the hang glider to ride the hot air from Desert Island all the way up
    to the floating metal island. This island is surprisingly cold and there
    definitely aren't any thermals to glide on, so you leave your hang glider
    behind.
    
    You wander around for a while, but you don't find any people or animals.
    However, you do occasionally find signposts labeled "Hot Springs" pointing in a
    seemingly consistent direction; maybe you can find someone at the hot springs
    and ask them where the desert-machine parts are made.
    
    The landscape here is alien; even the flowers and trees are made of metal. As
    you stop to admire some metal grass, you notice something metallic scurry away
    in your peripheral vision and jump into a big pipe! It didn't look like any
    animal you've ever seen; if you want a better look, you'll need to get ahead of
    it.
    
    Scanning the area, you discover that the entire field you're standing on is
    densely packed with pipes; it was hard to tell at first because they're the
    same metallic silver color as the "ground". You make a quick sketch of all of
    the surface pipes you can see (your puzzle input).
    
    The pipes are arranged in a two-dimensional grid of tiles:
    
    | is a vertical pipe connecting north and south.
    - is a horizontal pipe connecting east and west.
    L is a 90-degree bend connecting north and east.
    J is a 90-degree bend connecting north and west.
    7 is a 90-degree bend connecting south and west.
    F is a 90-degree bend connecting south and east.
    . is ground; there is no pipe in this tile.
    S is the starting position of the animal; there is a pipe on this tile, but
    your sketch doesn't show what shape the pipe has.
    Based on the acoustics of the animal's scurrying, you're confident the pipe
    that contains the animal is one large, continuous loop.
    
    For example, here is a square loop of pipe:
    
    .....
    .F-7.
    .|.|.
    .L-J.
    .....
    If the animal had entered this loop in the northwest corner, the sketch would
    instead look like this:
    
    .....
    .S-7.
    .|.|.
    .L-J.
    .....
    In the above diagram, the S tile is still a 90-degree F bend: you can tell
    because of how the adjacent pipes connect to it.
    
    Unfortunately, there are also many pipes that aren't connected to the loop!
    This sketch shows the same loop as above:
    
    -L|F7
    7S-7|
    L|7||
    -L-J|
    L|-JF
    In the above diagram, you can still figure out which pipes form the main loop:
    they're the ones connected to S, pipes those pipes connect to, pipes those
    pipes connect to, and so on. Every pipe in the main loop connects to its two
    neighbors (including S, which will have exactly two pipes connecting to it, and
    which is assumed to connect back to those two pipes).
    
    Here is a sketch that contains a slightly more complex main loop:
    
    ..F7.
    .FJ|.
    SJ.L7
    |F--J
    LJ...
    Here's the same example sketch with the extra, non-main-loop pipe tiles also
    shown:
    
    7-F7-
    .FJ|7
    SJLL7
    |F--J
    LJ.LJ
    If you want to get out ahead of the animal, you should find the tile in the
    loop that is farthest from the starting position. Because the animal is in the
    pipe, it doesn't make sense to measure this by direct distance. Instead, you
    need to find the tile that would take the longest number of steps along the
    loop to reach from the starting point - regardless of which way around the loop
    the animal went.
    
    In the first example with the square loop:
    
    .....
    .S-7.
    .|.|.
    .L-J.
    .....
    You can count the distance each tile in the loop is from the starting point
    like this:
    
    .....
    .012.
    .1.3.
    .234.
    .....
    In this example, the farthest point from the start is 4 steps away.
    
    Here's the more complex loop again:
    
    ..F7.
    .FJ|.
    SJ.L7
    |F--J
    LJ...
    Here are the distances for each tile on that loop:
    
    ..45.
    .236.
    01.78
    14567
    23...
    Find the single giant loop starting at S. How many steps along the loop does it
    take to get from the starting position to the point farthest from the starting
    position?
    */
    /// </summary>
    /// <returns>
    /// 7006 too high ??
    /// 7005 // need to remove the plus1 after der while is done :-/
    /// </returns>
    public override string Execute()
    {
      string result = "";
      int width = Lines[0].Length;
      int height = Lines.Count;
      input = new char[width, height]; // X, Y
      cPath = new char[width, height]; // X, Y
      iPath = new int[width, height]; // X, Y

      Point start = new Point(int.MinValue, int.MinValue);
      Point[] pathLocation = new Point[2];

      for (int y = 0; y < width; y++)
      {
        for (int x = 0; x < height; x++)
        {
          char c = Lines[y][x];
          input[x, y] = c;
          if (c == 'S')
          {
            start.X = x;
            start.Y = y;
            cPath[x, y] = '0';
            iPath[x, y] = 0;
          }
          else
          {
            cPath[x, y] = '.';
            iPath[x, y] = int.MinValue;
          }
        }
      }

      // search first segments (paths)
      Headings[] heading = GetPathsFirstStep(start, ref pathLocation, width, height);

      int step = 2;
      while (!pathLocation[0].Equals(pathLocation[1]))
      {
        GoStep(ref pathLocation[0], step, ref heading[0], width, height);
        GoStep(ref pathLocation[1], step, ref heading[1], width, height);
        step++;
      }

      #region print map
      //Debugger.Log(1, "", "\n");
      //for (int y = 0; y < height; y++)
      //{
      //  Debugger.Log(1, "", "\n");
      //  for (int x = 0; x < width; x++)
      //  {
      //    Debugger.Log(1, "", String.Format("{0} ", cPath[x, y]));
      //  }
      //}
      //Debugger.Log(1, "", "\n\n");
      #endregion print map

      result = (step - 1).ToString();
      return result;
    }

    private char[,] input;
    private char[,] cPath;
    private int[,] iPath;

    private void GoStep(ref Point pathLocation, int step, ref Headings heading, int width, int height)
    {
      char c;

      switch (heading)
      {
        case Headings.north: // optionsConnectToTileFromNorth
          c = input[pathLocation.X, pathLocation.Y - 1];
          pathLocation.Y--;
          switch (c)
          {
            case '|': heading = Headings.north; break;
            case '7': heading = Headings.west; break;
            case 'F': heading = Headings.east; break;
          }
          break;
        case Headings.east:  // optionsConnectToTileFromEast
          c = input[pathLocation.X + 1, pathLocation.Y];
          pathLocation.X++;
          switch (c)
          {
            case '-': heading = Headings.east; break;
            case 'J': heading = Headings.north; break;
            case '7': heading = Headings.south; break;
          }
          break;
        case Headings.south: // optionsConnectToTileFromSouth
          c = input[pathLocation.X, pathLocation.Y + 1];
          pathLocation.Y++;
          switch (c)
          {
            case '|': heading = Headings.south; break;
            case 'L': heading = Headings.east; break;
            case 'J': heading = Headings.west; break;
          }
          break;
        case Headings.west: // optionsConnectToTileFromWest
          c = input[pathLocation.X - 1, pathLocation.Y];
          pathLocation.X--;
          switch (c)
          {
            case '-': heading = Headings.west; break;
            case 'L': heading = Headings.north; break;
            case 'F': heading = Headings.south; break;
          }
          break;
      }

      iPath[pathLocation.X, pathLocation.Y] = step;

      int seqStep = step % 36;
      char charInt = seqStep < 10 ? seqStep.ToString()[0] : (char)(seqStep - 10 + (int)'a');
      cPath[pathLocation.X, pathLocation.Y] = charInt;
    }

    private List<char> optionsConnectToTileFromNorth = new List<char>()
            {
              '|', // is a vertical pipe  connecting north and south.
              '7', // is a 90-degree bend connecting south and west.
              'F'  // is a 90-degree bend connecting south and east.
            };

    private List<char> optionsConnectToTileFromEast = new List<char>()
            {
              '-', // is a horizontal pipe connecting east and west.
              'J', // is a 90-degree bend connecting north and west
              '7'  // is a 90-degree bend connecting south and west.
            };

    private List<char> optionsConnectToTileFromSouth = new List<char>()
            {
              '|', // is a vertical pipe  connecting north and south.
              'L', // is a 90-degree bend connecting north and east
              'J'  // is a 90-degree bend connecting north and west.
            };

    private List<char> optionsConnectToTileFromWest = new List<char>()
            {
              '-', // is a horizontal pipe connecting east and west.
              'L', // is a 90-degree bend connecting north and east.
              'F'  // is a 90-degree bend connecting south and east.
            };

    private Headings[] GetPathsFirstStep(Point start, ref Point[] pathLocation, int width, int height)
    {
      // create surrounding
      int startLine = start.Y - 1;
      int endLine = start.Y + 1;
      int startColumn = start.X - 1;
      int endColumn = start.X + 1;

      Headings[] heading = new Headings[2];
      int index = 0;

      for (int y = startLine; y <= endLine; y++)
      {
        if (y < 0 || y >= height) continue;

        for (int x = startColumn; x <= endColumn; x++)
        {
          if (x < 0 || x >= width) continue;

          char c = input[x, y];

          // check north, east, south, west
          if (y == start.Y - 1 && x == start.X) // north
          {
            if (optionsConnectToTileFromNorth.Contains(c))
            {
              pathLocation[index] = new Point(x, y);
              switch (c)
              {
                case '|': heading[index] = Headings.north; break;
                case '7': heading[index] = Headings.west; break;
                case 'F': heading[index] = Headings.east; break;
              }
              index++;
            }
          }
          else if (y == start.Y && x == start.X + 1) // east
          {
            if (optionsConnectToTileFromEast.Contains(c))
            {
              pathLocation[index] = new Point(x, y);
              switch (c)
              {
                case '-': heading[index] = Headings.east; break;
                case 'J': heading[index] = Headings.north; break;
                case '7': heading[index] = Headings.south; break;
              }
              index++;
            }
          }
          else if (y == start.Y + 1 && x == start.X) // south
          {
            if (optionsConnectToTileFromSouth.Contains(c))
            {
              pathLocation[index] = new Point(x, y);
              switch (c)
              {
                case '|': heading[index] = Headings.south; break;
                case 'L': heading[index] = Headings.east; break;
                case 'J': heading[index] = Headings.west; break;
              }
              index++;
            }
          }
          else if (y == start.Y && x == start.X - 1) // west
          {
            if (optionsConnectToTileFromWest.Contains(c))
            {
              pathLocation[index] = new Point(x, y);
              switch (c)
              {
                case '-': heading[index] = Headings.west; break;
                case 'L': heading[index] = Headings.north; break;
                case 'F': heading[index] = Headings.south; break;
              }
              index++;
            }
          }
          else continue;
        }
      }

      iPath[pathLocation[0].X, pathLocation[0].Y] = 1;
      iPath[pathLocation[1].X, pathLocation[1].Y] = 1;

      cPath[pathLocation[0].X, pathLocation[0].Y] = '1';
      cPath[pathLocation[1].X, pathLocation[1].Y] = '1';
      return heading;
    }

    private enum Headings
    {
      north, east,
      south, west
    }
  }
}

/*
. . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 
. . . . . . . . . . . . . . . . . . . . . . . . . . . . f e . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 
. . . . . . . . . . . . . . . . . . . . . . . . . . m l g d . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 
. . . . . . . . . . . . . . . . . . . . . . . . . o n k h c b a . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 
. . . . . . . . . . . . . . . . . . . . . . . . . p q j i 7 8 9 . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 
. . . . . . . . . . . . . . . . . . . . . . . . . . r u v 6 . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 
. . . . . . . . . . . . . . . . . . . . . . . . . . s t w 5 4 . . . . . . . . . . . . . . 5 4 . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . h g . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 
. . . . . . . . . . . . . . . . . . . . . . . . . . 3 2 x y 3 2 . . . . . . . . . . . . . 6 3 2 1 . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . j i f e . . . . j i f . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 
. . . . . . . . . . . . . . . . . . . . . . . . . 5 4 1 0 z 0 1 . . . . . f e . . l k j 8 7 y z 0 p o n . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . l k h g d c 5 4 l k d e . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 
. . . . . . . . . . . . . . . . . . . . . . 9 8 7 6 j k l y z s r s r q . g d q p m h i 9 a x u t q l m . . . . . . . . . . . . . . . . . o n m . . . . . . . . . . . . . . . m n o p a b 6 3 m n c b a . . . . z y 9 8 7 4 3 . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 
. . . . . . . . . . . . . . . . . . . . . . a b e f i n m x w t q t u p o h c r o n g d c b w v s r k . . . g f . . . . . . . . a 9 . . . p q l k j . . . . . . . . . . . . . . . r q 9 8 7 2 1 o 7 8 9 . . 6 5 0 x a b 6 5 2 1 . . . . . . . . . . . . . . . . . . . . . . . . . . . . 
. . . . . . . . . . . . . . . . . . . . . . . c d g h o p q v u p o v m n i b s t u f e d e f g h i j . . i h e . a 9 . . . . . b 8 5 4 x w r s h i . . . . . . . . . . . . . . . s t w x y z 0 p 6 . . . . 7 4 1 w . c d y z 0 . . . . . . . . . . . . . . . . . . . . . . . . . . . . 
. . . . . . . . . . . . . . . . . . . . . . . . 2 1 0 z y r s l m n w l k j a 9 8 v w x c b a 9 8 p o j i j k d c b 8 7 . . e d c 7 6 3 y v u t g . c b . . . z y n m . . b a . 0 z u v 0 z y x q 5 4 3 a 9 8 3 2 v k j e x w v . . . . . . . . . . . . . . . . . . . . . . . . . . . . 
. . . . . . . . . . . . . . . . . . . . . 4 3 2 3 4 5 6 x w t k . y x 4 5 6 9 a 7 6 . y 3 4 5 6 7 q n k h m l 2 3 4 5 6 . . f g h i j 2 z u v w f e d a 9 4 3 0 x o l g f c 9 2 1 y x w 1 2 3 w r 0 1 2 b c d s t u l i f s t u z y x . . . . . . . . . . . . . . . . . . . . . . . . . 
. . . . . . . . . . . . . . . . . . . . . 5 6 1 k j . 7 8 v u j i z 0 3 2 7 8 b c 5 0 z 2 1 0 z y r m l g n o 1 w v u . . v u p o l k 1 0 t s x 0 1 2 7 8 5 2 1 w p k h e d 8 3 4 t u v 6 5 4 v s z y h g f e r q n m h g r q 1 0 v w . . . . . . . . . . . . . . . . . . . . . . . . . 
. . . . . . . . . . . . . . . . . . . . b a 7 0 l i . . 9 a b g h 2 1 0 1 q p . d 4 1 w x y z 0 x s t e f q p 0 x s t . x w t q n m n o p q r y z . 3 6 . 6 7 8 v q j i 5 6 7 . 5 s r q 7 a b u t w x i j k l o p o p q n o p 2 3 u t q p . . . . . . . . . . . . . . . . . . . . . . . 
. . . . . . . . . . . . . . . . . . . d c 9 8 z m h g f e d c f e 3 4 z . r o n e 3 2 v m l k 1 w v u d c r s z y r q z y r s r s t m l . 7 6 5 4 . 4 5 . b a 9 u r i j 4 3 c b 6 7 o p 8 9 c d e v u . q p m n k j s r m f e . 4 5 s r o n m . . . . . . . . . . . . . . . . . . . . . 
. . . . . . . . . . . . . . . . . . . e f g h y n o f g h i j c d . 5 y t s l m f g t u n i j 2 3 4 7 8 b a t m n o p 0 p q 7 6 v u j k 9 8 1 2 3 2 1 6 5 c d e t s h k l 2 d a 9 8 n a 9 . j i f s t s r o n m l i t k l g d a 9 6 h i j k l . . . b a . . . . . . . . . . . . . . . . 
. . . . . . . . . . . . . . . . w v q p m l i x q p e n m l k b a 9 6 x u j k j i h s . o h q p . 5 6 9 8 9 u l k j i 1 o . 8 5 w h i . a z 0 7 6 3 0 7 4 z y f g h g n m 1 e f k l m b 8 l k h g r q t u v w f g h u j i h c b 8 7 g f s r . . g f c 9 . . . . . . . . . . . . . . . . 
. . . . . . . . . . . . 9 8 7 . x u r o n k j w r c d o p q r s t 8 7 w v i h k l m r q p g r o j i b a 7 w v e f g h 2 n m 9 4 x g f e b y . 8 5 4 z 8 3 0 x k j i f o z 0 h g j g f c 7 m n o p q p 2 1 0 x e 1 0 v 6 7 8 9 a b c d e t q p i h e d 8 . . . . . . . . . . . . . . . . 
. . . . . . . . . . . . a 5 6 z y t s t u v w v s b 8 7 6 x w v u 5 6 9 a f g 3 2 n o p e f s n k h c 5 6 x y d . 9 8 3 4 l a 3 y b c d c x w 9 a x y 9 2 1 w l m n e p y x i j i h e d 6 5 4 3 . r o 3 4 z y d 2 z w 5 . 3 2 1 6 5 0 z u n o j k 5 6 7 . . . . . . . . . . . . . . . . 
. . . . . . . . . . . c b 4 3 0 r s 1 0 z y x u t a 9 4 5 y z 0 1 4 7 8 b e 5 4 1 . r q d u t m l g d 4 1 0 z c b a 7 . 5 k b 2 z a 9 8 d e v c b w . a b c v s r o d q v w l k 1 2 3 4 5 6 7 2 t s n . 5 6 b c 3 y x 4 3 4 z 0 7 4 1 y v m r q l 4 3 . . . . . . . . . . . . . . . . . 
. . . . . . . . . . . d e f 2 1 q 3 2 z 0 1 2 3 4 5 2 3 u t . 3 2 3 2 . c d 6 7 0 t s b c v w x y f e 3 2 1 2 3 4 5 6 7 6 j c 1 0 5 6 7 . f u d e v 0 z e d u t q p c r u n m z 0 5 4 z y 9 8 1 u v m l 8 7 a 9 4 5 0 1 2 5 y x 8 3 2 x w l s p m 1 2 . . . . . . . . . . . . . . . . . 
. . . . . . . . . . . . . g j k p 4 5 y x c b a 9 6 1 w v s 5 4 v w 1 g f c b 8 z u v a 9 8 7 0 z 4 5 6 7 0 z y n m . 8 9 i d 2 3 4 p o h g t g f u 1 y f g h i j k b s t o x y . 6 3 0 x a b 0 . w j k 9 a b 8 7 6 z y 7 6 v w 9 g h i j k t o n 0 . . . . . . . . . . . . . . . . . . 
. . . . . . . . . . . 7 6 h i l o n 6 7 w d e f 8 7 0 x q r 6 t u x 0 h e d a 9 y x w 3 4 5 6 1 2 3 . 9 8 v w x o l c b a h e 1 . r q n i r s h i t 2 x u t q p m l a t s p w t s 7 2 1 w d c z y x i f e d c t u v w x 8 t u . a f 8 7 y x u x y z s r . n m . . . . . . . . . . . . . 
. . . . . . . . . . . 8 5 4 3 m n m . 8 v u h g l m z y p o 7 s r y z i j k l m r s t 2 1 0 7 6 5 q p a b u t s p k d e f g f 0 z s t m j q p k j s 3 w v s r o n 8 9 u r q v u r 8 t u v e f g h i h g h q r s d c b a 9 s r q b e 9 6 z w v w v u t q p o l . . . . . . . . . . . . . 
. . . . . . . . . . . 9 a b 2 p o l k 9 a t i j k n o p q n 8 9 q p o . m l k n q v u x y z 8 3 4 r o n c p q r q j a 9 g h i j y x u l k l o l q r 4 5 6 7 8 b c 7 6 v w x o p q 9 s h g f . n m j g j i p k j e n o p q r s p c d a 5 0 1 c d e f g h i j k . . . . . . . . . . . . . 
. . . . . . . . . k j g f c 1 q r s j c b s x w v u t s r m b a l m n o n i j o p w x w b a 9 2 1 s t m d o n m r i b 8 . q p k l w v k n m n m p 8 7 a 9 8 9 a d e 5 0 z y n m l a r i d e p o l k f k l o l i f m l k z y t o d c b 4 3 2 b a 9 6 5 4 3 2 . . . . . . . . . . . . . . 
. . . . . . . . m l i h e d 0 x w t i d e r y z 0 1 2 5 6 l c j k 9 8 p q h w v 0 z y v c f g z 0 v u l e j k l s h c 7 s r o n m h i j o p q n o 9 6 b c 7 . j i f 4 1 g h i j k b q j c b q r c d e n m n m h g h i j 0 x u n e f g p q r s t 8 7 y z 0 1 . . . . . . . . . . . . . . 
. . . . . . . . n o p q x y z y v u h g f q p . l k 3 4 7 k d i b a 7 6 r g x u 1 2 t u d e h y x w j k f i h g t g d 6 t u v w f g . g f . r m l a 5 e d 6 l k h g 3 2 f c b a d c p k 9 a . s b a 9 o p q r s t g d c 1 w v m . i h o n m l u v w x 0 z w v . f e . . . . . . . . . . 
. . . . . . . . . . s r w z 0 1 a b g f 6 5 o n m j a 9 8 j e h c 3 4 5 s f y t 4 3 s 9 8 j i x y z i h g d e f u f e 5 2 1 y x e d i h e t s j k b 4 f g 5 m n o p s t e d 8 9 e f o l 8 7 6 t u v 8 5 4 3 2 v u f e b 2 7 8 l k j e f i j k 9 8 3 2 1 y x u . g d . . . . . . . . . . 
. . . . . . . . . . t u v 4 3 2 9 c h e 7 4 z y x i b c d i f g d 2 . u t e z s 5 6 r a 7 k l w v 0 1 2 b c f e v w x 4 3 0 z 8 9 c j c d u v i h c 3 i h 4 3 2 1 q r u v 6 7 . h g n m 3 4 5 y x w 7 6 z 0 1 w x y 9 a 3 6 9 a b c d g h . b a 7 4 p q r s t . h c 9 8 . . . . . . . . 
. . . . . . . . . . . 1 2 5 6 7 8 d i d 8 3 0 v w h g f e h g f e 1 0 v c d 0 r 8 7 q b 6 5 m n u b a 3 a h g d 2 1 y z 0 1 6 7 a b k b e d w f g d 2 j k l m n 0 x w . w 5 2 1 i j k 1 2 l k z 0 5 6 7 y x 2 1 0 z 8 5 4 5 4 3 g f c b 8 7 c d 6 5 o n y x q p i b a 7 . . . . . . . . 
. . . . . . h i . . . 0 3 4 h g f e j c 9 2 1 u t s p o n m l k j i z w b 2 1 q 9 o p c d 4 3 o t c 9 4 9 i j c 3 0 z u t 2 5 w v m l a f c x e d e 1 0 j i p o z y v u x 4 3 0 z m l 0 z m j 2 1 4 3 8 v w 3 4 5 6 7 6 z 0 1 2 h e d a 9 6 5 e j k l m z w r o j k 5 6 . . v u . . . . 
. . . . . f g j o p . z y 5 i j k[l]k b a . k l m r q 3 4 5 6 7 8 h y x a 3 4 p a n . . e 1 2 p s d 8 5 8 . k b 4 5 y v s 3 4 x u n 8 9 g b y z c f y z k h q x y z 0 t y z 0 x y n o x y n i 3 0 1 2 9 u l k b a 7 w x y . k j i z 0 1 2 3 4 f i . 8 7 0 v s n m l 4 3 . . w t m l . . 
. . . . . e d k n q t u x 6 f g . 0 1 8 9 a j i n q r 2 1 0 z y 9 g f e 9 6 5 o b m l g f 0 r q r e 7 6 7 m l a 9 6 x w r 4 3 y t o 7 i h a 9 0 b g x m l g r w v 2 1 s 7 6 1 w t s p w p o h 4 z y b a t m j c 9 8 v o n m l w x y . q p m l g h a 9 6 1 u t y z 0 1 2 . . x s n k . . 
. . . 9 a b c l m r s v w 7 e h i z 2 7 6 b . h o p s t u v w x a b c d 8 7 m n c d k h y z s p q f g h 6 n o p 8 7 . p q 5 2 z s p 6 j . 7 8 1 a h w n e f s . u 3 4 r 8 5 2 v u r q v q f g 5 . x c r s n i d e t u p w x y v u t s r o n k h g b c 5 2 l m x w v . . 2 1 y r o j . . 
d e . 8 7 6 5 4 3 2 1 0 z 8 d c j y 3 4 5 c d g f c b a 9 y x w v o n m l k l k f e j i x w t o n 6 5 i 5 4 3 q r s n o 7 6 1 0 r q 5 k l 6 3 2 9 i v o d c t u t 6 5 q 9 4 3 u v w x u r e d 6 v w d q p o h g f s r q v u z 2 3 a b c j k j i f e d 4 3 k n o p u t . 3 0 z q p i h g 
c f i j m n q r . v w x y 9 a b k x w v u t e f e d . 7 8 z 0 t u p g h i j i j g j k n o v u . m 7 4 j k l 2 z y t m 9 8 . 0 1 2 3 4 n m 5 4 7 8 j u p a b w v s 7 o p a b c t q p y t s b c 7 u t e f g . m n o p q r s t 0 1 4 9 8 d i l 8 9 a b e f g j i . q r s . 4 5 a b c d e f 
b g h k l o p s t u x w v u t s l m n o p s h g p q r 6 5 2 1 s r q f e d c h g h i l m p i j k l 8 3 u t m 1 0 x u l a b c z y x u t o p q . 6 5 k t q 9 y x . r 8 n m d c d s r o z 0 1 a 9 8 r s r q h k l a 9 8 7 6 v u r q 5 6 7 e h m 7 6 5 c d . h i h g f e . a 9 6 9 8 7 . . . 
a 9 8 7 m l a 9 8 z y v w x y r q p k j q r i j o n s . 4 3 y z 6 7 8 9 a b . f w v u t q h g b a 9 2 v s n o p w v k j e d k l w v s r s r 2 3 4 l s r 8 z 0 p q 9 a l e b e f . n m 3 2 n o p q t s p i j i b 2 3 4 5 w t s p o n m f g n s t 4 3 2 1 k j q r s d c b 8 7 4 5 6 . . . 
. . . 6 n k b 6 7 0 t u n m z 0 1 o l i f e d k l m t u v w x 0 5 4 3 2 1 0 z e x y . s r . f c 9 a 1 w r k j q r s t i f i j m n o p q t u 1 0 z m n 6 7 2 1 o n m b k f a h g j k l 4 5 m l k v u n o j g h c 1 0 z y x 2 3 4 5 6 l k j o r u x y z 0 l m p u t y z 0 1 2 3 q p o . . 
2 3 4 5 o j c 5 4 1 s p o l k 3 2 n m h g b c b a 9 6 5 2 1 2 1 . 9 a v w x y d c z 0 1 2 3 e d 8 b 0 x q l i f e v u h g h g f q p g f e v w x y x o 5 4 3 g h k l c j g 9 i j i h g 7 6 h i j w . m l k f e d . n o r s 1 0 z y 7 g h i p q v w 1 0 z y n o v w x w v u t s r m n . . 
1 y x w p i d . 3 2 r q f g j 4 5 6 7 8 9 a d c . 8 7 4 3 0 3 6 7 8 b u t s r q b a 7 6 5 4 5 6 7 c z y p m h g d w x y b c d e r o h c d 0 z y v w p . d e f i j e d i h 8 7 k l m f 8 9 g f y x 4 5 6 n o p s t m p q t u v w x 8 f e d 6 5 4 3 2 v w x 0 z y x 8 9 6 7 8 d e l k . . 
0 z u v q h e . . b c d e h i f e v u t k j e r s v w x y z 4 5 k j c d e h i p . 9 8 f e d 4 1 0 d e f o n . b c 3 2 z a 7 6 5 s n i b a 1 2 3 u r q b c 7 6 5 4 f . j k . 6 p o n e d a . e z . 3 2 7 m l q r u l k j i h g f e 9 a b c 7 y z 4 5 u r q 1 2 3 6 7 a 5 4 9 c f g j . . 
. . t s r g f . . a v u p o h g d w r s l i f q t u h g d c 9 8 l i d c f g j o n k j g b c 3 2 z y x g h i j a 5 4 1 0 9 8 3 4 t m j . 9 8 5 4 t s . a 9 8 1 2 3 g h i l m 5 q r s t c b c d 0 z 0 1 8 9 k h g v w x y z 2 3 c d e d a 9 8 x 0 3 6 t s p o . 4 5 . b 2 3 a b . h i . . 
. . . . . . . . 8 9 w t q n i b c x q p m h g p m l i f e b a 7 m h e b a 9 k l m l i h a 9 8 n o v w n m l k 9 6 x y z 0 1 2 v u l k n o 7 6 t u v w x y z 0 l k j a 9 o n 4 3 0 z u v a b 2 1 y x w v a j i f o n k j 0 1 4 b a f c b o p w 1 2 7 8 9 a n m l e d c 1 . . . v u . . . 
. . . . . . . 6 7 y x s r m j a z y . o n 4 5 o n k j . 1 2 5 6 n g f 2 3 8 . m n o p y z 6 7 m p u t o p q . 8 7 w v m l k j w x k l m p q . s r q p o t s r m h i b 8 p s t 2 1 y x w 9 8 3 . p q t u b c d e p m l i h g 5 8 9 g l m n q v u . 4 3 . b e f k f g h 0 z y x w t . . . 
. . . . . . . 5 4 z . . . l k 9 0 x y 1 2 3 6 . c d y z 0 3 4 p o z 0 1 4 7 c b 8 7 q x 0 5 . l q r s x w r s 1 2 3 u n g h i z y j i h g r i j k l m n u v q n g f c 7 q r u v w x y z 0 7 4 n o r s z y t s r q 9 a d e f 6 7 . h k j . r s t 6 5 2 1 c d g j . . i j m n o p s . . . 
. . . . . . . . 3 0 . . . 6 7 8 1 w z 0 r q 7 8 b e x w v u t q x y t s 5 6 d a 9 6 r w 1 4 j k 9 8 7 y v u t 0 z 4 t o f 8 7 0 1 2 3 4 f s h a 9 4 3 2 1 w p o . e d 6 5 q p o j i 3 2 1 6 5 m l a 9 0 x u 5 6 7 8 b c v u r q p i h i b a 9 8 7 u v 0 z . h i . . . k l . . q r . . . 
. . . . . . . . 2 1 . . . 5 4 3 2 v u t s p o 9 a f g h i j s r w v u r q . e 3 4 5 s v 2 3 i h a 5 6 z u v w x y 5 s p e 9 6 z y v u 5 e t g b 8 5 2 3 0 x y z w x 0 1 4 r s n k h 4 7 8 9 i j k b 8 1 w v 4 3 0 z y x w t s n o j g d c p q r s t w x y 3 2 z y . . . . . . . . . . . 
. . . . . . . . . . n o p q . w x k l m r s n m f e d c b k r s t m n o p g f 2 1 0 t u p o f g b 4 3 0 t k j 8 7 6 r q d a 5 0 x w t 6 d u f c 7 6 1 4 z y 1 0 v y z 2 3 u t m l g 5 6 5 a h e d c 7 2 3 a b 2 1 g h s t u v m l k f e n o b a 9 8 7 6 5 4 1 0 x . . . . . . . . . . . 
. . . . . . . . . . m l k r u v y j i n q t k l g 7 8 9 a l q p u l k j i h 2 3 y z . r q n e d c . 2 1 s l i 9 a d e . c b 4 1 q r s 7 c v e d y z 0 5 w x 2 . u t 8 7 w v c d e f 6 3 4 b g f g h 6 5 4 9 c d e f i r q p w 5 6 7 k l m . c n o p q r s t u v w b a . . . . . . . . . 
. . . . . . . . . . h i j s t 0 z g h o p u j i h 6 5 2 1 m n o v w x y z 0 1 4 x u t s l m . 0 z u t q r m h g b c f g j k 3 2 p c b 8 b w x . x w 7 6 v u 3 4 r s 9 6 x y b a 9 8 7 2 1 c d e f i x y z 8 7 6 b a j k l o x 4 3 8 j i f e d m l k j i h g f e d c 9 . . . . . . . . . 
. . . . . . . . . . g f e b a 1 2 f e d c v w z 0 3 4 3 0 z s r o n m l k j 6 5 w v c d k j i 1 y v s p o n . f e 9 8 h i l m n o d a 9 a 9 y z u v 8 9 . t 6 5 q p a 5 4 z 0 t u x y z 0 r q p o j w v 0 1 4 5 c 9 6 5 m n y z 2 9 a h g t u v 4 5 m n o t u v 0 1 8 . . . . . . . . . 
. . . . . . . . . . . . d c 9 8 3 4 5 . b a x y 1 2 5 4 x y t q p c d g h i 7 8 9 a b e f g h 2 x w r g f e d c d a 7 w v u t o n e f 6 7 8 1 0 t s b a r s 7 8 9 o b c 3 2 1 s v w v u t s l m n k . u t 2 3 e d 8 7 4 3 o n 0 1 c b q r s . w 3 6 l k p s . w z 2 7 6 . . . . . . . . 
. . . . . . . . . . . l m n . 7 6 5 6 7 8 9 i h 8 7 6 . w v u . a b e f . 3 2 . w v q p k j . 3 o p q h 8 9 a b c b 6 x y . s p m h g 5 4 3 2 n o r c d q p c b a n e d o p q r w x 0 1 6 7 k j i l q r s j i f y z 0 1 2 p m l k d e p o n m x 2 7 i j q r . x y 3 4 5 . . . . . . . . 
. . . . . . . . . . . k j o p y z 4 3 q p k j g 9 e f g p q r s 9 8 7 6 5 4 1 0 x u r o l i 5 4 n m . i 7 6 5 4 3 2 5 4 z 0 r q l i h i j k l m p q f e n o d e f m f g n m l k j y z 2 5 8 . g h m p o n k h g x w v u r q h i j . f i j k l y 1 8 h g . . . . . e d . . . . . . . . . 
. . . . . . . . . . . . i h q x 0 1 2 r o l e f a d c h o n m t w x y z 0 3 4 z y t s n m h 6 7 8 l k j m n s t 0 1 . 3 2 1 4 5 k j g f e d c b 0 z g h m l k h g l i h . f g h i 7 6 3 4 9 a f e n q r m l . 5 6 9 a t s f g . 4 3 g h y x . z 0 9 a f e . . . g f c b . . . . . . . . 
. . . . . . . . . . . e f g r w v u t s n m d c b a b i j k l u v s r q 1 2 5 8 9 a d e f g d c 9 g h i l o r u z y . 0 1 2 3 6 7 8 . . 7 8 9 a 1 y x i j k j i . k j y z e d c 9 8 5 4 1 0 b c d o p s . 2 3 4 7 8 b c d e 9 8 5 2 1 0 z w v u t s b c d 6 5 . h i j a 9 . . . . . . . 
. . . . . . . . . . . d c b s . w x 0 1 . h i j . 9 8 7 6 5 2 1 0 t u p m l 6 7 . b c p o n e b a f e j k p q v w x . z u t s b a 9 4 5 6 r q p 2 3 w v m l s t u v w x 0 1 2 b a . . 3 2 z y r q p o t u 1 0 v u t s r c b a 7 6 d e f g h k l m r q . . 7 4 n m l k 7 8 . . . . . . . 
. . . . . . . . . . 5 6 7 a t u v y z 2 3 g f k p q v w x 4 3 . z y v o n k j 0 z u t q l m f g b c d 6 5 4 x w v o n y v q r c d e 3 2 1 s . o n 4 t u n q r g f e d c b a 3 6 7 . b c . w x s l m n w v . z w n o p q d 8 9 a b c z y x i j . n o p e d 8 3 o p 4 5 6 . . . . . . . . 
. . . . . . . . . . 4 3 8 9 s r q p o n 4 d e l o r u z y 3 4 7 8 x w f g h i 1 y v s r k j i h a 9 8 7 2 3 y t u p m x w p k j g f . z 0 t k l m 5 s p o p o h i 5 6 7 8 9 4 5 8 9 a d e v u t k 7 6 x 0 1 y x m l i h e 7 6 5 4 3 0 v w 1 0 r q l k f c 9 2 . q 3 0 z . . . . . . . . 
. . . . . . . . . . . 2 1 u t i j k l m 5 c b m n s t 0 1 2 5 6 9 c d e 7 6 . 2 x w r s v w x y z m n o 1 0 z s r q l k j o l i h . . y x u j i h 6 r q . . n m j 4 1 0 x w t s r q n m f g h i j 8 5 y z 2 3 6 7 k j g f i j m n 2 1 u t 2 z s p m j g b a 1 s r 2 1 y . . . . . . . . 
. . . . . . . . . . . z 0 v . h g f e d 6 9 a 9 8 7 q p o j i h a b a 9 8 5 4 3 m n q t u h g f 0 l . p w x c d e f g h i n m . . . . . w v e f g 7 . p q . . l k 3 2 z y v u 9 a p o l k j i h g 9 4 3 0 z 4 5 8 9 a d e h k l o p q r s 3 y t o n i h y z 0 t u v w x . . . . . . . . 
. . . . . . . . . . . y x w 5 6 7 8 b c 7 8 b a 5 6 r . n k f g b c d e f g h k l o p o n i d e 1 k j q v y b a 9 8 7 w v o n . h g f . . . d c b 8 . o r s . w x . 1 2 5 6 7 8 b c d e f g h i f a b 2 1 y x w v u b c f g l k j c b 8 7 4 x u 7 8 9 a x w v u t s r q p . . . . . . . 
. . . . . . . . . . s t w x 4 3 2 9 a h g f c . 4 3 s t m l e d 0 z w v s r i j . p 0 1 m j c . 2 h i r u z 0 1 4 5 6 x u p m j i d e . . . . . a 9 . n m t u v y z 0 3 4 5 4 x w v u t s n m j e d c r s t u v w t s r q p m h i d a 9 6 5 w v 6 5 4 b k l o p k l m n o . . . . . . . 
. . . . . . . . . . r u v y z 0 1 e d i j e d 0 1 2 v u . a b c 1 y x u t q p o n q z 2 l k b a 3 g . s t a 9 2 3 2 1 y t q l k b c . . . . . . . . . . l k h g 9 8 5 4 3 6 3 y . o p q r o l k . o p q 1 0 z y x s t u v o n g f e 9 a x y z 0 1 2 3 c j m n q j i h . d c . . . . . . 
. . . . . . . . . . q p o l k h g f c l k d e z y x w r s 9 6 5 2 d e f g h k l m r y 3 4 5 8 9 4 f e d c b 8 7 . 3 0 z s r 8 9 a . . . . . . . . . . . . j i f a 7 6 1 2 7 2 z m n y x q p k l m n 6 5 2 n o p q r g f w x y z 0 7 8 b w v u t q p o d i h s r w x g f e b a . . . . . 
. . . . . . . . . . . . n m j i 9 a b m n c f g h i j q t 8 7 4 3 c b a 9 i j u t s x w . 6 7 . 5 8 9 c d g h 6 5 4 t u z 0 7 6 5 . . . . . . . . . . . . . . e b y z 0 9 8 1 0 l k z w r s j i b a 7 4 3 m l k j i h e d c b a 1 6 5 c f g h s r m n e f g t u v y 5 6 7 8 9 . . . . . 
. . . . . . . . u v . z 0 3 4 7 8 z y p o b 4 3 2 1 k p u v . 3 4 5 6 7 8 . w v s t u v i h e d 6 7 a b e f i j m n s v y 1 2 3 4 . . . . . . . . . . . . . . d c x w v a b . f g j 0 v u t g h c 9 8 . m n . 1 2 3 4 5 6 7 8 9 2 3 4 d e . i j k l . r q p o n m z 4 3 . . . . . . . . 
. . . . . . . s t w x y 1 2 5 6 1 0 x q r a 5 w x 0 l o x w 1 2 7 6 3 2 1 0 x q r m l k j g f c b u t o n i h k l o r w x . j i . . . . . . . . . . . . . . p q r s t u d c d e h i 1 u v . f e d i j k l o z 0 v u t s r q p o n m l k j i h g . u t s h i j k l 0 1 2 . . . . . . . . 
. . . . . . . r q p k j i h g 3 2 . w t s 9 6 v y z m n y z 0 9 8 5 4 v w z y p o n 4 5 6 7 8 9 a v s p m j g f e p q . q p k h g . . . . . . . . . . . . . o n m l i h e b c 7 6 5 2 t w x y 3 4 h g f e p y x w v w z 0 3 4 5 6 7 8 9 a d e f w v m n g f e d c 9 8 5 4 . . . . . . . 
. . . . . . . . . o l c d e f 4 7 8 v u . 8 7 u t s r k j i h a r s t u x y z 0 1 2 3 a 9 8 3 2 x w r q l k b c d 0 z s r o l e f . . . . . . . . . . . . . . o p k j g f a 9 8 . 4 3 s r q z 2 5 8 9 c d q r s t u x y 1 2 l k h g f e b c z y x k l o p q r s b a 7 6 3 . . . . . . . 
. . . . . . . . . n m b a 9 8 5 6 9 c d e f 0 1 2 p q l e f g b q p o n m l k j i h g b c 7 4 1 y z 0 1 2 9 a 5 4 1 y t u n m d c . . . . . . . . . . . . . . n q r s t . f g h i j k l o p 0 1 6 7 a b 0 z u t s r q p o n m j i b c d 4 3 0 h i j c b a 9 8 t u v w x 2 . . . . . . . 
. . . . . . . . . . r s v w 7 6 5 a b i h g z y 3 o n m d c b c f g h i j k n o r s f e d 6 5 0 z y x w 3 8 7 6 3 2 x w v 4 5 a b . . . . . . . . . . . . . . m l k j u d e 9 8 7 6 5 m n g f a 9 8 5 4 1 y v u v w x y z 0 1 2 3 a 7 6 5 2 1 g f e d i j k 7 6 5 4 3 y 1 . . . . . . . 
. . . . . . . . . . q t u x y . 4 3 k j u v w x 4 5 6 7 8 9 a d e d c b a l m p q t u x y z a b c j k v 4 7 8 l m n q r 2 3 6 9 . . . . . . . . . . . . . . . b c . i v c b a x y 3 4 l k h e b . 7 6 3 2 x w t s r q p o n m l 4 9 8 h i p q v w b c h g l u v w 1 2 z 0 . . . . . . . 
. . . . . . . . . . p o n m z 0 1 2 l . t s p o n m l i h g f e 5 6 7 8 9 g f e d c v w . 0 9 8 d i l u 5 6 9 k j o p s 1 . 7 8 . . . . . . . . . . . . . . . a d g h w 7 8 9 w z 2 n m j i d c x y 1 2 3 4 5 6 7 8 9 c d g h k 5 a b g j o r u x a d e f m t s x 0 . . . . . . . . . . 
. . . . . d e f g h i j k l s r q p m n . r q . o p k j u v y z 4 3 o n . h 8 9 a b . 1 0 1 2 7 e h m t s r a b i h g t 0 z . . . . . . . . . . . . . . . 5 6 9 e f y x 6 5 a v 0 1 o p . t u v w z 0 f e b a 9 8 7 a b e f i j 6 9 c f k n s t y 9 8 7 6 n q r y z . . . . . . . . . . 
. . . . . c b 8 7 6 5 y x u t m n o p o j k l m n q r s t w x 0 1 2 p m j i 7 6 5 4 3 2 z y 3 6 f g n o p q . c d e f u v y . . . . . . . . . . . . . . 3 4 7 8 v u z 0 3 4 b u t s r q r s n m j i h g d c x y . 6 5 4 3 0 z . 7 8 d e l m l k z 2 3 4 5 o p . . . . . . . . . . . . . 
. . . . . . a 9 . 3 4 z w v . l k j q r i h g d c b a 7 6 5 0 z u t q l k l m n o p s t w x 4 5 4 . y x w v u t s r q p w x . . . . . . . . . . . . . . 2 1 y x w t s 1 2 d c h i . m n q p o l k 3 4 t u v w z 0 1 2 3 2 1 y x w . s r m n i j 0 1 c b a 9 . . . . . . . . . . . . . . 
. . . . . . . . . 2 1 0 3 4 7 8 . i h s t u f e d e 9 8 . 4 1 y v s r y z k j g f q r u v . 5 6 3 2 z g h i j k l m n o . . . . . . . . . . . . . . . . . 0 z . p q r c b e f g j k l o p q t u 1 2 5 s r o n m l k j 4 5 6 9 a v u t q p o h g f e d o p 8 7 . . . . . . . . . . . . . 
. . . . . . . . . . 0 1 2 5 6 9 a b g f w v a b c f y z 0 3 2 x w v w x 0 1 i h e d a 9 6 5 4 7 8 1 0 f . b a 9 8 7 6 . . . . . . . . . . . 0 z y x . . . . m n o h g d a 9 q p o l k j i r s v 0 z 6 . q p e f g h i 1 0 7 8 b c d i j o p u v w l m n q r 6 5 4 3 . . . . . . . . . . 
. . . . . . . . . . z y x u t g f c d e x y 9 8 7 g x w 1 . r s t u l k . 2 3 4 5 c b 8 7 2 3 a 9 g h e d c z 0 1 4 5 . . . . . . . . . . . 1 2 v w p o r s l k j i f e . 8 r . n m f g h 4 3 w x y 7 a b c d 6 5 4 3 2 z y x w v e h k n q t y x k j . . s t 0 1 2 . . . . . . . . . . 
. . . . . . . . . . 0 1 w v s h e d 8 7 6 z 2 3 6 h i v 2 3 q p o n m j i h g f 6 n o r s 1 0 b c f i l m n y x 2 3 . f e . . . . . . . . . . 3 u t q n q t . x y . 2 3 6 7 s t c d e 7 6 5 2 1 0 z 8 9 c b 8 7 . n o p q r s t u f g l m r s z 0 h i . . . u z . . . . . . . . . . . . 
. . . . . . . . t u z 2 p q r i j c 9 4 5 0 1 4 5 k j u t 4 5 6 7 8 9 a b c d e 7 m p q t u z y d e j k p o v w v u t g d s r 2 1 . . . . . . 4 5 s r m p u v w z 0 1 4 5 w v u b a 9 8 t u v w x y f e d a 9 q r m l k j i h g d c b a z y x w 1 g f e d . v y . . . . . . . . . . . . 
. . . . . . . . s v y 3 o n m l k b a 3 2 1 0 x w l q r s r q p o n m l . h g . 8 l k j i v w x u t s r q t u . w r s h c t q 3 0 z . . . . . . 6 r s l o n m j i h g 5 4 x y p q r s t s p o l k h g 9 a n o p s t u x y 1 2 f e 7 8 9 0 t u v 2 5 6 b c . w x . . . . . . . . . . . . 
. . . . . . . q r w x 4 5 6 b c h i . o p . z y v m p s . w x 0 1 6 7 k j i f e 9 c d g h . x w v o p q r s 3 2 x q p i b u p 4 x y d c z y . . 7 q t k j . l k d e f 6 3 0 z o l k v u r q n m j i . 8 b m l k j i v w z 0 3 4 5 6 . 2 1 s r q 3 4 7 a . . . . . . . . . . . . . . . . 
. . . . . . . p o n m l k 7 a d g j m n q r s t u n o t u v y z 2 5 8 9 a b c d a b e f 4 3 y l m n c b 8 7 4 1 y z o j a v o 5 w v e b 0 x g f 8 p u v i h c b c b a 7 2 1 . n m j w r s v w z 0 3 4 7 c f g j k h g f e d c b a 7 6 3 m n o p g f 8 9 . . . . . . . . . . . . . . . . 
. . . . . . . . d e . i j 8 9 e f k l w v o n m f e d c 3 2 1 0 3 4 t s r q p o l k 9 8 5 2 z k j i d a 9 6 5 0 1 0 n k 9 w n 6 7 u f a 1 w h e 9 o n w x g d a 7 6 9 8 p o l k h i x q t u x y 1 2 5 6 d e h i l m n o p q r s 9 8 5 4 l k j i h e d c . . . . . . . . . . . . . . . . 
. . . . . . . b c f g h q p e d 8 7 y x u p k l g 9 a b 4 t u z y v u b c d e n m j a 7 6 1 0 f g h e v w x y z 2 3 m l 8 x m 9 8 t g 9 2 v i d a l m z y f e 9 8 5 s r q n m j g f y p m l k j i h g f k j i d c b a 9 8 7 . t u x y z 2 3 4 7 8 9 a b m l . . . . . . . . . . . . . . 
. . . . . 8 9 a 1 0 z s r o f c 9 6 z 0 t q j i h 8 7 6 5 s v w x w x a 9 8 f g h i b a b c d e . g f u t q p . 5 4 5 6 7 y l a r s h 8 3 u j c b k j 0 1 2 3 4 3 4 t u v m n i d e z o n . 9 a b c d e l m h e 1 2 3 4 5 6 p o v w . 0 1 . 5 6 x w v o n k j . . . . . . . . . . . . . 
. . . . . 7 6 5 2 x y t m n g b a 5 2 1 s r g h i j m n q r 2 1 0 z y 1 2 7 6 d c d c 9 8 7 o n m h i j s r o n 6 3 4 7 6 z k b q p i 7 4 t k l m n i h . b a 5 2 1 . x w l o h c b 0 3 4 7 8 x w t s r o n g f 0 z w v s r q n e d c 5 4 . 0 z y t u p g h i . . . . . . . . . . . . . 
. . . . . . . 4 3 w v u l k h 2 3 4 3 4 5 6 f e d k l o p 4 3 w x y z 0 3 4 5 e b e f 4 5 6 p k l o n k j k l m 7 2 1 8 5 0 j c n o j 6 5 s r q p o f g d c 9 6 z 0 z y j k p g 9 a 1 2 5 6 z y v u . q p 6 7 a b y x u t k l m f a b 6 3 2 1 q r s r q f e d . . . . . . . . . . . . . 
. . . . . . . . . . s t . j i 1 0 b a 9 8 7 2 3 c b a 9 6 5 u v q p m l k j . f a h g 3 s r q j i p m l i b a 9 8 z 0 9 4 1 i d m l k n o p q r s t e . e f 8 7 y x 0 1 i r q f 8 7 6 5 4 3 0 v w 1 2 3 4 5 8 9 c f g h i j i h g 9 8 7 m n o p y x s t a b c . . . . . . . . . . . . . 
. . . . . . . . . . r u v w x y z c d y z 0 1 4 5 6 . 8 7 . t s r o n 2 3 i h g 9 i j 2 t u f g h q r s h c v w x y b a 3 2 h e h i j m r q p w v u d c h g t u v w 3 2 h s t e d c . q p 2 1 u x 0 z w v u t s d e n m l k j g h i j k l . 1 0 z w v u 9 8 7 . . . . . . . . . . . . . 
. . . . . . . . . . q p m l k j i f e x w v u t s 7 e f e f . n o p 0 1 4 5 6 7 8 l k 1 w v e d c . u t g d u t s r c d e f g f g f k l s n o x y z a b i j s r m l 4 5 g f u v a b s r o n s t y z y x k l . r q p o b c d e f m l e d 8 7 2 x y z 0 1 2 5 6 . . . . . . . . . . . . . 
. . . . . . . . . . . o n . 9 a h g f g j k n o r 8 d g d g h m l q z y x w z y . m n 0 x 4 5 6 b a v w f e n o p q t s . g b c d e v u t m l k 1 0 9 s r k l q n k 7 6 d e . w 9 u t k l m r q p 0 h i j m 5 6 7 8 9 a z y t s n k f c 9 6 3 w t s p o 3 4 . . . . . . . . . . . . . . 
. . . . . . . . . . . . . 7 8 b c d e h i l m p q 9 c h c b i j k r s t u v 0 x w p o z y 3 . 7 8 9 y x 2 3 m l i h u r i h a 9 8 7 w x g h i j 2 3 8 t q n m p o j 8 9 c b y x 8 v w j i b a 9 o 1 g f e n 4 3 2 1 0 z 0 x u r o j g b a 5 4 v u r q n m r q . . . . . . . . . . . . . 
. . . . . . . . . . . . 5 6 z y p o l k j i h g f a b i . a 9 8 7 6 5 4 3 2 1 u v q r s 1 2 l k j i z 0 1 4 5 k j g v q j k 3 4 5 6 z y f y x a 9 4 7 u p o p q r i h a 9 a z 0 7 . x y h c d 8 n 2 . c d o p s t w x y 1 w v q p i h 6 7 a b e f i j k l s p o . g f e . . . . . . . . 
. . . . . . . . . . . . 4 3 0 x q n m . w x 0 1 e d c j k l m n u v o p q r s t e d u t 0 n m f g h u t a 9 6 d e f w p m l 2 1 e d 0 1 e z w b 8 5 6 v o . 8 7 s f g b 8 7 2 1 6 3 2 z g f e 7 m 3 4 b a 9 q r u v 4 3 2 z 0 1 2 3 4 5 8 9 c d g h w v u t m n i h c d . . . . . . . . 
. . . . . . . . . . . 4 5 2 1 w r s t u v y z 2 3 . b 8 7 6 5 o t w n m l e d c f c v w z o d e 1 0 v s b 8 7 c v u x o n y z 0 f c b 2 d 0 v c 7 6 5 w n a 9 6 t e d c 5 6 3 4 5 4 1 0 1 2 3 6 l k 5 6 7 8 d c 9 8 5 w x y f e . a 9 6 5 4 3 2 . y x 2 3 4 l k j a b . . . . . . . . . 
. . . . . . . . . . 2 3 6 x y v s r q p o n 6 5 4 5 a 9 2 3 4 p s x y j k f a b g b y x y p c 3 2 z w r c d a b w t y v w x i h g 9 a 3 c 1 u d e 3 4 x m b c 5 u z 0 1 4 1 0 5 6 z 0 7 6 5 4 5 4 j i h g f e b a 7 6 v i h g d c b 8 7 2 1[0]1 0 z 0 1 . 5 6 7 8 9 q p . . . . . . . . 
. . . . . . n o p q 1 0 7 w z u t i j k l m 7 4 3 6 7 0 1 u t q r 0 z i h g 9 8 h a z 0 x q b 4 5 y x q p e 9 y x s z u t k j 6 7 8 5 4 b 2 t g f 2 z y l e d 4 v y x 2 3 2 z 8 7 y x 8 9 a b 2 3 . 1 0 z y d c r s t u j k v u t s 5 4 3 . 1 8 9 a z y x w v u t s r o . . . . . . . . 
. . . . . . m l k r y z 8 v 0 1 g h c b a 9 8 . 2 9 8 z w v s r q 1 2 3 4 5 6 7 i 9 . 1 w r a . 6 l m n o f 8 z 0 r 0 r s l 4 5 q p 6 9 a 3 s h 0 1 0 j k f g 3 w v w h g 3 y 9 a b w j i . c 1 q p 2 3 4 x e b q p o n m l w x y r 6 7 8 9 2 7 c b 2 3 6 7 a b e f g n m . . . . . . . 
. . . . . . f g j s x a 9 u t 2 f e d k l y z 0 1 a b y x k l m p o n 6 5 2 1 k j 8 7 2 v s 9 8 7 k j i h g 7 2 1 q 1 q p m 3 s r o 7 8 7 4 r i z y 1 i h i h 2 x u t i f 4 x w d c v k h g d 0 r o n 6 5 w f a 3 2 1 6 5 4 3 0 z q f e d a 3 6 d e 1 4 5 8 9 c d . h k l . . . . . . . 
. . . . c d e h i t w b k l s 3 . d e j m x w v u t c d e j i n o p m 7 4 3 0 l m n 6 3 u t . 7 8 . g h i j 6 3 . p 2 . o n 2 t m n 8 9 6 5 q j . x 2 3 g j k 1 y r s j e 5 u v e f u l m f e z s . m 7 . v g 9 4 . 0 7 a b 2 1 o p g h c b 4 5 g f 0 z y x u t q p i j . . . . . . . . 
. q r . b a 9 8 7 u v c j m r 4 b c f i n o p q r s t s f g h u t q l 8 9 y z 0 z o 5 4 3 4 5 6 9 a f e l k 5 4 n o 3 4 5 . 1 u l k b a . o p k v w 5 4 f m l 0 z q p k d 6 t s r g t o n w x y t u l 8 9 u h 8 5 y z 8 9 c d e n k j i x y 1 2 h i 1 2 3 w v s r o n . . . . . . . . . 
o p s v w z 0 . 6 5 e d i n q 5 a 9 g h 0 z y x w v u r q p o v s r k j a x w 1 y p q r 2 z y d c b c d m n o . m l 8 7 6 z 0 v w j c d e n m l u 7 6 d e n o r s . o l c 7 8 p q h s p . v i h g v k j a t i 7 6 x w 5 4 1 0 f m l . v w z 0 3 4 j 0 z 4 5 i j k l m . . . . . . . . . 
n m t u x y 1 2 3 4 f g h o p 6 7 8 7 6 1 . d e h i j k l m n w x y h i b u v 2 x u t s 1 0 x e f g b a r q p w x k 9 a b y x w x i h g f q r s t 8 9 c 3 2 p q t m n m b a 9 o n i r q t u j k f w x i b s j s t u v 6 3 2 z g h i j u t 8 7 6 5 k x y . 6 h g f . . . . . . . . . . . 
. l k h g d c 7 6 1 0 3 2 t s l k j 8 5 2 b c f g x w v u t s 3 2 z g f c t s 3 w v q r s . w j i h . 9 s t u v y j g f c t u v y z 0 1 2 p o j i b a b 4 1 w v u l k n o p q r m j q r s p o l e z y h c r k r i h g 7 8 9 y x m l k r s 9 a b m l w v u 7 a b e . . . . . . . . . . . 
. . j i f e b 8 5 2 z 4 1 u r m h i 9 4 3 a 9 0 z y r s t u r 4 1 0 . e d . r 4 5 6 p o t u v k 5 6 7 8 d c 9 8 z i h e d s r o n 8 7 6 3 4 n k h c 9 a 5 0 x . h i j 2 1 u t s l k p o r q n m d 0 1 g d q l q j k f c b a v w n o p q n m d c n o p q t 8 9 c d . . . . . . . . . . . 
. . . . . . a 9 4 3 y 5 0 v q n g f a b 6 7 8 1 o p q 3 2 v q 5 6 j k . o p q 9 8 7 . n m n m l 4 1 0 z e b a 7 0 1 m n o p q p m 9 a 5 4 5 m l g d 8 7 6 z y f g 5 4 3 0 v w j k l m n s . w x c b 2 f e p m p o l e d e f u t s r q p o l e f g j k r s t u v . . . . . . . . . . . . 
. . . . . . . u v w x 6 z w p o . e d c 5 4 3 2 n m l 4 1 w p 8 7 i l m n u t a b c d e l o p q 3 2 x y f g h 6 3 2 l k j i h q l k b 2 3 6 7 . f e 9 a b c d e 7 6 b c z y x i h g d c t u v y z a 3 4 5 o n . n m d i h g r s t u v w . k h g h i l m . s x w . . . . . . . . . . . . 
. . . . . . s t o n m 7 y x . 7 8 b c d e f g h i j k 5 0 x o 9 a h g d c v s r 6 5 g f k h g r s t w v k j i 5 4 . c d e f g r s j c 1 s r 8 9 a b 8 7 6 5 4 3 8 9 a d g h . d e f e b a 9 8 1 0 9 8 7 6 . 6 7 a b c j k l q p m l k x y j i f e b a n o r y z . . . . . . . . . . . . 
. . . . . . r q p . l 8 9 a b 6 9 a h g f e d c b a 7 6 z y n m b c f e b w p q 7 4 h . j i f w v u t u l m n o p q b a 9 w v u t i d 0 t q j i d c v w . 0 1 2 3 2 1 e f i j c 9 8 f g h 6 7 2 3 4 5 6 7 8 5 8 9 u t w v m n o n . j i z 0 1 . d c 9 8 p q 1 0 . . . . . . . . . . . . 
. . . . . . . . . . k j e d c 5 4 j i 3 4 5 6 7 8 9 8 9 g h k l e d 8 9 a x o 9 8 3 i j k l e x y r s l k v u t s r 6 7 8 x y f g h e z u p k h e t u x y z 4 3 4 . 0 t s r k b a 7 k j i 5 4 p o h g f a 9 4 1 0 v s x u t o p s t g h c b 2 3 4 5 6 7 6 5 2 . . . . . . . . . . . . . 
. . . . . . . . . . . i f y z 2 3 k l 2 1 0 z o n m b a f i j g f . 7 6 z y n a b 2 p o n m d 0 z q n m j w x y z 0 5 4 3 0 z e d g f y v o l g f s r q 9 8 5 2 5 6 z u v q l 4 5 6 l . 1 2 3 q n i j e b . 3 2 z w r y z s r q r u f e d a 9 8 9 8 7 . . 4 3 . . . . . . . . . . . . . 
. . . . . . . . . . . h g x 0 1 o n m t u x y p q l c d e d c h 2 3 4 5 0 1 m l c 1 q r 8 9 c 1 . p o h i b a 9 8 1 2 3 2 1 . b c h i x w n m l m n o p a 7 6 1 0 7 y x w p m 3 2 n m z 0 t s r m l k d c 1 2 . y x q 1 0 p q 7 6 v w x y z . 7 a b c d e f . . . . . . . . . . . . . . 
. . . . . . . . . . . . . w v q p . r s v w t s r k f e 9 a b i 1 w v a 9 2 j k d 0 t s 7 a b 2 3 4 5 g f c d e 7 6 5 4 7 8 9 a h g j k n o p k j 6 5 4 b c x y z 8 9 a b o n . 1 o p y x u r s t u v w . 0 3 4 5 6 p 2 . o 9 8 5 0 z i h 0 1 6 5 k j i h g . . . . . . . . . . . . . . 
. . . . . . . . . . . . . . u r . p q h g v u h i j g 7 8 n m j 0 x u b 8 3 i f e z u . 6 5 e d c 7 6 . e h g f m n q r 6 5 4 j i f e l m r q h i 7 2 3 e d w v o n e d c x y z 0 r q . w v q f e 9 8 x y z c b a 7 o 3 4 n a b 4 1 y j g 3 2 . 4 l m n o p . . . . . . . . . . . . . . 
. . . . . . . . . . . . . . t s . o n i f w x g f e h 6 5 o l k z y t c 7 4 h g x y v w 3 4 f g b 8 b c d i j k l o p s t 2 3 k l . d u t s . g f 8 1 0 f g h u p m f g . w v u t s j k n o p g d a 7 y x e d . 9 8 n 6 5 m d c 3 2 x k f 4 . 2 3 w v s r q . . . . . . . . . . . . . . 
. . . . . . . . . . . . . . . . . . m j e d y b c d i 3 4 p q h i r s d 6 5 . v w n m x 2 1 i h a 9 a 9 8 d c 9 8 5 4 v u 1 0 n m b c v w x y . e 9 y z m l i t q l k h a b e f g h i l m 5 4 h c b 6 z w f g h i l m 7 8 l e f u v w l e 5 6 1 y x u t . . . . . . . . . . . . . . . . 
. . . . . . . . . . . . . . . . . . l k . c z a 9 8 j 2 1 s r g j q p e r s t u p o l y z 0 j k l 2 3 4 7 e b a 7 6 3 w x y z o p a 9 y x w z 0 d a x w n k j s r . j i 9 c d q p i h g 7 6 3 i j k 5 0 v m l k j k j a 9 k h g t s r m d 8 7 0 z . . . . . . . . . . . . . . . . . . . 
. . . . . . . . . . . . . . . . . . . . a b 0 1 . 7 k z 0 t e f k l o f q 7 6 5 q . k z y x o n m 1 0 5 6 f g h i j 2 z y x s r q 7 8 z 0 v u 1 c b u v o p q r y z 2 3 8 7 s r o j e f 8 1 2 n m l 4 1 u n o p q r i b c j i . . . q n c 9 . . . . . . . . . . . . . . . . . . . . . . 
. . . . . . . . . . . . . . . . . . . . 9 8 3 2 5 6 l y v u d 4 3 m n g p 8 9 4 r i j 0 v w p i j . z y x g f e l k 1 0 . w t 2 3 6 5 4 1 s t 2 3 s t 2 1 w v s x 0 1 4 5 6 t u n k d a 9 0 z o p q 3 2 t s 7 6 5 s h g d . . . . . p o b a . . . . . . . . . . . . . . . . . . . . . . 
. . . . . . . . . . . . . . . . . . . . . 7 4 3 4 n m x w . c 5 2 1 0 h o b a 3 s h g 1 u t q h k l . v w h i d m . q r s v u 1 4 5 6 3 2 r 6 5 4 r q 3 0 x u t w v 6 5 2 1 w v m l c b w x y t s r o p q r 8 9 4 t u f e . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 
. . . . . . . . . . . . . . . . . . . . . 6 5 2 1 o p . . a b 6 x y z i n c 1 2 t e f 2 3 s r g n m t u n m j c n o p u t y z 0 9 8 7 . p q 7 8 . o p 4 z y r s t u 7 4 3 0 x q r s t u v m l u v w n m l k j a 3 w v . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 
. . . . . . . . . . . . . . . . . . . . . . y z 0 r q . . 9 8 7 w v k j m d 0 z u d c 5 4 d e f o r s p o l k b y x w v . x w b a l m n o j i 9 a n m 5 6 . q p m l 8 9 a z y p o n m 5 4 n k b a x y z 0 h i b 2 x y . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 
. . . . . . . . . . . . . . . . . . . . . . x w t s . . . . . s t u l k l e f y v a b 6 7 c 9 8 p q r q . w x a z 0 1 2 t u v c d k h g l k h c b k l 8 7 . . o n k j c b g h i j k l 6 3 o j c 9 m l k 1 g d c 1 0 z . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 
. . . . . . . . . . . . . . . . . . . . . . . v u . . . . . . r q p m j i h g x w 9 8 9 8 b a 7 y x s t u v y 9 8 5 4 3 s r q p e j i f m n g d . j i 9 a b . . . . i d e f 8 7 6 5 8 7 2 p i d 8 n . j 2 f e . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 
. . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . o n i j . z 0 1 . 7 a . . 5 6 z w r q d c z 0 7 6 7 8 9 a n o f . . e d o f e . . h e d c . . . . h e d c 9 . . 4 9 . 1 q h e 7 o h i 3 4 . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 
. . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . f g h k l y x 2 5 6 b c 3 4 1 0 v s p e b a 1 6 5 0 z c b m l g h a b c p q r . . g f . . . . . . g f . b a 1 2 3 a b 0 r g f 6 p g f 6 5 . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 
. . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . e d c n m v w 3 4 f e d 2 3 2 . u t o f 8 9 2 3 4 1 y d e f k j i 9 8 7 u t s . . . . . . . . . . . . . . . 0 z y d c z s t 4 5 q r e 7 8 . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 
. . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 9 a b o p u t q p g z 0 1 4 5 8 9 m n g 7 6 5 4 3 2 x w h g . . . . 5 6 v w . . . . . . . . . . . . . . . . t u x e f y x u 3 2 t s d c 9 . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 
. . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 8 7 6 r q . s r o h y n m l 6 7 a l k h 4 5 6 . a b . v i . . . . . 4 1 0 x . . . . . . . . . . . . . . . . s v w h g . w v 0 1 u . . b a . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 
. . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 4 5 s t u . m n i x o p k j c b . j i 3 2 7 8 9 c t u j k . . . . 3 2 z y . . . . . . . . . . . . . . . . r q p i . . . . z y v . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 
. . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 3 2 x w v . l k j w v q r i d . . . z 0 1 k j i d s p o l . . . . . . . . . . . . . . . . . . . . . . . . . . o j . . . . . x w . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 
. . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 1 y . . . . . . . u t s h e v w x y p o l . h e r q n m . . . . . . . . . . . . . . . . . . . . . . . . . . n k . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 
. . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 0 z . . . . . . . . . . g f u t s r q n m . g f . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . m l . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . .  */