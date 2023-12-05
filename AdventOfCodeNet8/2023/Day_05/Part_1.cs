using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace AdventOfCodeNet8._2023.Day_05
{
  internal class Part_1 : Days
  {
    /// <summary>
    /*
    https://adventofcode.com/2023/day/5
    --- Day 5: If You Give A Seed A Fertilizer ---
    You take the boat and find the gardener right where you were told he would be:
    managing a giant "garden" that looks more to you like a farm.

    "A water source? Island Island is the water source!" You point out that Snow
    Island isn't receiving any water.

    "Oh, we had to stop the water because we ran out of sand to filter it with!
    Can't make snow with dirty water. Don't worry, I'm sure we'll get more sand
    soon; we only turned off the water a few days... weeks... oh no." His face
    sinks into a look of horrified realization.

    "I've been so busy making sure everyone here has food that I completely forgot
    to check why we stopped getting more sand! There's a ferry leaving soon that is
    headed over in that direction - it's much faster than your boat. Could you
    please go check it out?"

    You barely have time to agree to this request when he brings up another. "While
    you wait for the ferry, maybe you can help us with our food production problem.
    The latest Island Island Almanac just arrived and we're having trouble making
    sense of it."

    The almanac (your puzzle input) lists all of the seeds that need to be planted.
    It also lists what type of soil to use with each kind of seed, what type of
    fertilizer to use with each kind of soil, what type of water to use with each
    kind of fertilizer, and so on. Every type of seed, soil, fertilizer and so on
    is identified with a number, but numbers are reused by each category - that is,
    soil 123 and fertilizer 123 aren't necessarily related to each other.

    For example:

    seeds: 79 14 55 13

    seed-to-soil map:
    50 98 2
    52 50 48

    soil-to-fertilizer map:
    0 15 37
    37 52 2
    39 0 15

    fertilizer-to-water map:
    49 53 8
    0 11 42
    42 0 7
    57 7 4

    water-to-light map:
    88 18 7
    18 25 70

    light-to-temperature map:
    45 77 23
    81 45 19
    68 64 13

    temperature-to-humidity map:
    0 69 1
    1 0 69

    humidity-to-location map:
    60 56 37
    56 93 4
    The almanac starts by listing which seeds need to be planted: seeds 79, 14, 55,
    and 13.

    The rest of the almanac contains a list of maps which describe how to convert
    numbers from a source category into numbers in a destination category. That is,
    the section that starts with seed-to-soil map: describes how to convert a seed
    number (the source) to a soil number (the destination). This lets the gardener
    and his team know which soil to use with which seeds, which water to use with
    which fertilizer, and so on.

    Rather than list every source number and its corresponding destination number
    one by one, the maps describe entire ranges of numbers that can be converted.
    Each line within a map contains three numbers: the destination range start, the
    source range start, and the range length.

    Consider again the example seed-to-soil map:

    50 98 2
    52 50 48
    The first line has a destination range start of 50, a source range start of 98,
    and a range length of 2. This line means that the source range starts at 98 and
    contains two values: 98 and 99. The destination range is the same length, but
    it starts at 50, so its two values are 50 and 51. With this information, you
    know that seed number 98 corresponds to soil number 50 and that seed number 99
    corresponds to soil number 51.

    The second line means that the source range starts at 50 and contains 48
    values: 50, 51, ..., 96, 97. This corresponds to a destination range starting
    at 52 and also containing 48 values: 52, 53, ..., 98, 99. So, seed number 53
    corresponds to soil number 55.

    Any source numbers that aren't mapped correspond to the same destination
    number. So, seed number 10 corresponds to soil number 10.

    So, the entire list of seed numbers and their corresponding soil numbers looks
    like this:

    seed  soil
    0     0
    1     1
    ...   ...
    48    48
    49    49
    50    52
    51    53
    ...   ...
    96    98
    97    99
    98    50
    99    51
    With this map, you can look up the soil number required for each initial seed
    number:

    Seed number 79 corresponds to soil number 81.
    Seed number 14 corresponds to soil number 14.
    Seed number 55 corresponds to soil number 57.
    Seed number 13 corresponds to soil number 13.
    The gardener and his team want to get started as soon as possible, so they'd
    like to know the closest location that needs a seed. Using these maps, find the
    lowest location number that corresponds to any of the initial seeds. To do
    this, you'll need to convert each seed number through other categories until
    you can find its corresponding location number. In this example, the
    corresponding types are:

    Seed 79, soil 81, fertilizer 81, water 81, light 74, temperature 78, humidity
    78, location 82.
    Seed 14, soil 14, fertilizer 53, water 49, light 42, temperature 42, humidity
    43, location 43.
    Seed 55, soil 57, fertilizer 57, water 53, light 46, temperature 82, humidity
    82, location 86.
    Seed 13, soil 13, fertilizer 52, water 41, light 34, temperature 34, humidity
    35, location 35.
    So, the lowest location number in this example is 35.

    What is the lowest location number that corresponds to any of the initial seed
    numbers?

    */
    /// </summary>
    /// <returns>
    /// 
    /// </returns>
    public override string Execute()
    {
      string result = "";
      List<long> seeds = new List<long>();

      Dictionary<Enum, FullMap> mapDict = new Dictionary<Enum, FullMap>();
      Emaps currentMap = Emaps.seed_to_soil;

      foreach (string s in Enum.GetNames(typeof(Emaps)))
      {
        mapDict.Add((Emaps)Enum.Parse(typeof(Emaps), s), new FullMap(s));
      }

      #region Read Input
      foreach (var line in Lines)
      {
        if (String.IsNullOrEmpty(line)) continue;

        if (line.StartsWith("seeds:"))
        {
          var tmpLine = line.Substring(line.IndexOf(":") + 1);
          var chunks = tmpLine.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
          foreach (var chunk in chunks)
          {
            seeds.Add(long.Parse(chunk));
          }
        }
        else if (line.StartsWith("seed-to-soil"))
        {
          currentMap = Emaps.seed_to_soil;
        }
        else if (line.StartsWith("soil-to-fertilizer"))
        {
          currentMap = Emaps.soil_to_fertilizer;
        }
        else if (line.StartsWith("fertilizer-to-water"))
        {
          currentMap = Emaps.fertilizer_to_water;
        }
        else if (line.StartsWith("water-to-light"))
        {
          currentMap = Emaps.water_to_light;
        }
        else if (line.StartsWith("light-to-temperature"))
        {
          currentMap = Emaps.light_to_temperature;
        }
        else if (line.StartsWith("temperature-to-humidity"))
        {
          currentMap = Emaps.temperature_to_humidity;
        }
        else if (line.StartsWith("humidity-to-location"))
        {
          currentMap = Emaps.humidity_to_location;
        }
        else
        {
          // this is an actual entry !!
          var chunks = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
          long[] cValues = new long[chunks.Length];

          for (int i = 0; i < chunks.Length; i++)
          {
            cValues[i] = long.Parse(chunks[i]);
          }
          mapDict[currentMap].AddMapElement(cValues[0], cValues[1], cValues[2]);
        }
      }
      #endregion Read Input

      List<long> locationValues = new List<long>();

      foreach (var seed in seeds)
      {
        var soil =
          mapDict[Emaps.seed_to_soil].Convert(seed);
        var fertilizer =
          mapDict[Emaps.soil_to_fertilizer].Convert(soil);
        var water =
          mapDict[Emaps.fertilizer_to_water].Convert(fertilizer);
        var light =
          mapDict[Emaps.water_to_light].Convert(water);
        var temperature =
          mapDict[Emaps.light_to_temperature].Convert(light);
        var humidity =
          mapDict[Emaps.temperature_to_humidity].Convert(temperature);
        var location =
          mapDict[Emaps.humidity_to_location].Convert(humidity);

        locationValues.Add(location);
      }
      result = locationValues.Min().ToString();
      return result;
    }

    public enum Emaps
    {
      seed_to_soil,
      soil_to_fertilizer,
      fertilizer_to_water,
      water_to_light,
      light_to_temperature,
      temperature_to_humidity,
      humidity_to_location
    };

    public class FullMap
    {
      private List<MapElement> _map;
      public string Name { get; private set; }

      public FullMap(string name)
      {
        Name = name;
        _map = new List<MapElement>();
      }

      public void AddMapElement(long destination, long source, long range)
      {
        MapElement me = new MapElement(destination, source, range);
        _map.Add(me);
      }

      public long Convert(long value)
      {
        long result = Int32.MinValue;

        bool applied = false;
        foreach (MapElement me in _map)
        {
          if (me.IsApplicable(value))
          {
            result = me.Convert(value);
            applied |= true;
          }
        }
        if (applied)
        {
          return result;
        }
        else
        {
          return value;
        }
      }
    }

    public class MapElement
    {
      private readonly long source;
      private readonly long destination;
      private readonly long range;

      public MapElement(long destination, long source, long range)
      {
        this.source = source;
        this.destination = destination;
        this.range = range;
      }

      public bool IsApplicable(long value)
      {
        bool isApplicable = (value >= source && value < source + range);
        return isApplicable;
      }

      public long Convert(long value)
      {
        if (!IsApplicable(value)) { Debugger.Break(); }

        long retValue = destination + (value - source)/*delta*/;
        return retValue;
      }
    }
  }
}
