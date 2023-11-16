using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace AdventOfCodeNet8._2015.Day_14
{
  internal class Part_2 : Days
  {
    /// <summary>
    /*
    https://adventofcode.com/2015/day/14
--- Day 14: Reindeer Olympics ---
This year is the Reindeer Olympics! Reindeer can fly at high speeds, but must rest occasionally to recover their energy. Santa would like to know which of his reindeer is fastest, and so he has them race.

Reindeer can only either be flying (always at their top speed) or resting (not moving at all), and always spend whole seconds in either state.

For example, suppose you have the following Reindeer:

Comet can fly 14 km/s for 10 seconds, but then must rest for 127 seconds.
Dancer can fly 16 km/s for 11 seconds, but then must rest for 162 seconds.
After one second, Comet has gone 14 km, while Dancer has gone 16 km. After ten seconds, Comet has gone 140 km, while Dancer has gone 160 km. On the eleventh second, Comet begins resting (staying at 140 km), and Dancer continues on for a total distance of 176 km. On the 12th second, both reindeer are resting. They continue to rest until the 138th second, when Comet flies for another ten seconds. On the 174th second, Dancer flies for another 11 seconds.

In this example, after the 1000th second, both reindeer are resting, and Comet is in the lead at 1120 km (poor Dancer has only gotten 1056 km by that point). So, in this situation, Comet would win (if the race ended at 1000 seconds).

Given the descriptions of each reindeer (in your puzzle input), after exactly 2503 seconds, what distance has the winning reindeer traveled?

Your puzzle answer was 2655.

The first half of this puzzle is complete! It provides one gold star: *

--- Part Two ---
Seeing how reindeer move in bursts, Santa decides he's not pleased with the old scoring system.

Instead, at the end of each second, he awards one point to the reindeer currently in the lead. (If there are multiple reindeer tied for the lead, they each get one point.) He keeps the traditional 2503 second time limit, of course, as doing otherwise would be entirely ridiculous.

Given the example reindeer from above, after the first second, Dancer is in the lead and gets one point. He stays in the lead until several seconds into Comet's second burst: after the 140th second, Comet pulls into the lead and gets his first point. Of course, since Dancer had been in the lead for the 139 seconds before that, he has accumulated 139 points by the 140th second.

After the 1000th second, Dancer has accumulated 689 points, while poor Comet, our old champion, only has 312. So, with the new scoring system, Dancer would win (if the race ended at 1000 seconds).

Again given the descriptions of each reindeer (in your puzzle input), after exactly 2503 seconds, how many points does the winning reindeer have?
    */
    /// </summary>
    /// <returns>
    /// Your puzzle answer was 1059.
    /// </returns>
    public override string Execute()
    {
      string result = "";

      List<Reindeer> reindeers = new List<Reindeer>();
      foreach (string line in Lines)
      {
        var chunks = line.Split(' ');
        reindeers.Add(new Reindeer(chunks[0], Int32.Parse(chunks[3]), Int32.Parse(chunks[6]), Int32.Parse(chunks[13])));
      }

      for (int i = 0; i < 2503; i++)
      {
        foreach (Reindeer reindeer in reindeers)
        {
          reindeer.AdvandOneSec();
        }

        var distance =
          (from r in reindeers select r.Distance).Max();

        var winners = 
          from r in reindeers 
          where r.Distance == distance
          select r;

        foreach (Reindeer reindeer in winners)
        {
          reindeer.AddOnePoint();  
        }
      }

      var points =
        (from r in reindeers select r.Points).Max();

      result = points.ToString();
      return result;
    }

    private class Reindeer
    {
      public string Name { get; private set; }
      public int MaxSpeed { get; private set; }
      public int Duration { get; private set; }
      public int Resttime { get; private set; }

      public Reindeer(string Name, int maxSpeed, int duration, int resttime)
      {
        this.Name = Name;
        MaxSpeed = maxSpeed;
        Duration = duration;
        Resttime = resttime;

        //
        moving = true;
        timeleft = Duration;
        distance = 0;

        Points = 0;
      }

      private int distance;
      private int timeleft;
      private bool moving;

      public void AdvandOneSec()
      {
        timeleft--;

        if (moving)
        {
          distance += MaxSpeed;
          if (timeleft == 0)
          {
            moving = false;
            timeleft = Resttime;
          }
        }
        else
        {
          if (timeleft == 0)
          {
            moving = true;
            timeleft = Duration;
          }
        }
      }

      public int Points { get; private set; }

      public void AddOnePoint()
      {
        Points++;
      }
      
      public int Distance
      { get { return distance; } }
    }
  }
}
