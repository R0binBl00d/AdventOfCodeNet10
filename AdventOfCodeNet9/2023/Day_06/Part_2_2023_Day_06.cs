using System.Diagnostics;
namespace AdventOfCodeNet9._2023.Day_06
{
  internal class Part_2_2023_Day_06 : Days
  {
    /// <summary>
    /*
    https://adventofcode.com/2023/day/6
    */
    /// </summary>
    /// <returns>
    /// 27102791
    /// </returns>
    public override string Execute()
    {
      string result = "";
      var pastRacesList = new List<KeyValuePair<long, long>>();  // time distance
      #region PuzzleInput
      pastRacesList.Add(new KeyValuePair<long, long>(40_82_84_92, 233_1011_1110_1487));
      #endregion PuzzleInput
      #region Example
      //pastRacesList.Add(new KeyValuePair<long, long>(7, 9));
      //pastRacesList.Add(new KeyValuePair<long, long>(15, 40));
      //pastRacesList.Add(new KeyValuePair<long, long>(30, 200));
      #endregion Example

      long totalScore = 1;

      List<long> totalScoreRange = new List<long>();
      List<long> totalScoreList = new List<long>();


      foreach (var race in pastRacesList)
      {
        //Debugger.Log(1, "seed", String.Format("\n\nOldGame{0} : {1}\n", race.Key, race.Value));

        // cut the ranges;
        long racetotal = race.Key;
        var timeRanges = new List<KeyValuePair<long, long>>(); // Start, range

        long rangeStart = 0;

        long portion = 1_000_000;
        //long portion = 10;

        while (racetotal > 0)
        {
          long raceinterval = 0;
          if (racetotal > portion)
          {
            raceinterval = portion;
          }
          else
          {
            raceinterval = racetotal;
          }

          timeRanges.Add(new KeyValuePair<long, long>(rangeStart, raceinterval));
          rangeStart += raceinterval;
          racetotal -= raceinterval;
        }
        object winnerLock = new object();


        Parallel.ForEach(timeRanges, (tRange) =>
        {
          Debugger.Log(1, "seed", String.Format("\n\nOldGame {0} : {1}\n", race.Key, race.Value));
          Debugger.Log(1, "seed", String.Format("\nRange {0} : {1}\n", tRange.Key, tRange.Value));

          long score = 0;

          for (long i = tRange.Key; i < (tRange.Key + tRange.Value); i++) // RaceTime
          {
            // hold button for i ms
            long speed = i;

            //drive boat
            long distance = speed * (race.Key - i);
            //Debugger.Log(1, "seed", String.Format("\nDistance {0} : Speed{1}\n", distance, speed));

            if (distance > race.Value)
            {
              score++;
            }
          }
          lock (winnerLock)
          {
            totalScoreRange.Add(score);
          }
        });

        totalScoreList.Add(totalScoreRange.Sum());
        totalScoreRange.Clear();
      }

      foreach (var s in totalScoreList)
      {
        totalScore *= s;
      }

      result = totalScore.ToString();
      return result;
    }
  }
}
