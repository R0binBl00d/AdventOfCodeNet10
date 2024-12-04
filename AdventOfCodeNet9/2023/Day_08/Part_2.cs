using System.Diagnostics;

namespace AdventOfCodeNet9._2023.Day_08
{
  internal class Part_2 : Days
  {
    /// <summary>
    /*
    https://adventofcode.com/2023/day/8
    --- Day 8: Haunted Wasteland ---
    You're still riding a camel across Desert Island when you spot a sandstorm
    quickly approaching. When you turn to warn the Elf, she disappears before your
    eyes! To be fair, she had just finished warning you about ghosts a few minutes
    ago.
    
    One of the camel's pouches is labeled "maps" - sure enough, it's full of
    documents (your puzzle input) about how to navigate the desert. At least,
    you're pretty sure that's what they are; one of the documents contains a list
    of left/right instructions, and the rest of the documents seem to describe some
    kind of network of labeled nodes.
    
    It seems like you're meant to use the left/right instructions to navigate the
    network. Perhaps if you have the camel follow the same instructions, you can
    escape the haunted wasteland!
    
    After examining the maps for a bit, two nodes stick out: AAA and ZZZ. You feel
    like AAA is where you are now, and you have to follow the left/right
    instructions until you reach ZZZ.
    
    This format defines each node of the network individually. For example:
    
    RL
    
    AAA = (BBB, CCC)
    BBB = (DDD, EEE)
    CCC = (ZZZ, GGG)
    DDD = (DDD, DDD)
    EEE = (EEE, EEE)
    GGG = (GGG, GGG)
    ZZZ = (ZZZ, ZZZ)
    Starting with AAA, you need to look up the next element based on the next
    left/right instruction in your input. In this example, start with AAA and go
    right (R) by choosing the right element of AAA, CCC. Then, L means to choose
    the left element of CCC, ZZZ. By following the left/right instructions, you
    reach ZZZ in 2 steps.
    
    Of course, you might not find ZZZ right away. If you run out of left/right
    instructions, repeat the whole sequence of instructions as necessary: RL really
    means RLRLRLRLRLRLRLRL... and so on. For example, here is a situation that
    takes 6 steps to reach ZZZ:
    
    LLR
    
    AAA = (BBB, BBB)
    BBB = (AAA, ZZZ)
    ZZZ = (ZZZ, ZZZ)
    Starting at AAA, follow the left/right instructions. How many steps are
    required to reach ZZZ?
    
    Your puzzle answer was 16409.
    
    The first half of this puzzle is complete! It provides one gold star: *
    
    --- Part Two ---
    The sandstorm is upon you and you aren't any closer to escaping the wasteland.
    You had the camel follow the instructions, but you've barely left your starting
    position. It's going to take significantly more steps to escape!
    
    What if the map isn't for people - what if the map is for ghosts? Are ghosts
    even bound by the laws of spacetime? Only one way to find out.
    
    After examining the maps a bit longer, your attention is drawn to a curious
    fact: the number of nodes with names ending in A is equal to the number ending
    in Z! If you were a ghost, you'd probably just start at every node that ends
    with A and follow all of the paths at the same time until they all
    simultaneously end up at nodes that end with Z.
    
    For example:
    
    LR
    
    11A = (11B, XXX)
    11B = (XXX, 11Z)
    11Z = (11B, XXX)
    22A = (22B, XXX)
    22B = (22C, 22C)
    22C = (22Z, 22Z)
    22Z = (22B, 22B)
    XXX = (XXX, XXX)
    Here, there are two starting nodes, 11A and 22A (because they both end with A).
    As you follow each left/right instruction, use that instruction to
    simultaneously navigate away from both nodes you're currently on. Repeat this
    process until all of the nodes you're currently on end with Z. (If only some of
    the nodes you're on end with Z, they act like any other node and you continue
    as normal.) In this example, you would proceed as follows:
    
    Step 0: You are at 11A and 22A.
    Step 1: You choose all of the left paths, leading you to 11B and 22B.
    Step 2: You choose all of the right paths, leading you to 11Z and 22C.
    Step 3: You choose all of the left paths, leading you to 11B and 22Z.
    Step 4: You choose all of the right paths, leading you to 11Z and 22B.
    Step 5: You choose all of the left paths, leading you to 11B and 22C.
    Step 6: You choose all of the right paths, leading you to 11Z and 22Z.
    So, in this example, you end up entirely on nodes that end in Z after 6 steps.
    
    Simultaneously start on every node that ends with A. How many steps does it
    take before you're only on nodes that end with Z?
    */
    /// </summary>
    /// <returns>
    /// https://www.calculatorsoup.com/calculators/math/lcm.php
    /// 
    /// LCM = calcLCM(12643 14257 15871 18023 19637 16409)
    /// 11795205644011
    /// </returns>
    public override string Execute()
    {
      string result = "";

      var instructions = "";
      var locations = new Dictionary<string, KeyValuePair<string, string>>();

      long lcm = 0;

      foreach (var line in Lines)
      {
        if (String.IsNullOrEmpty(instructions))
        {
          instructions = line;
          continue;
        }
        if (String.IsNullOrEmpty(line)) continue;

        var chunks = line.Split(new char[] { ' ', '=', ',', '(', ')' }, StringSplitOptions.RemoveEmptyEntries);
        locations.Add(chunks[0], new KeyValuePair<string, string>(chunks[1], chunks[2]));
      }

      int reachedDestination = 0;
      //long steps = 3993734991;
      long steps = 0;

      var startPoins = from l in locations where l.Key[2] == 'A' select l;
      var endPoins = from l in locations where l.Key[2] == 'Z' select l;

      List<string> currentLocation = new List<string>(startPoins.Count());
      bool[] bReachZ = new bool[startPoins.Count()];
      var Cycles = new List<long>(startPoins.Count());

      for (int i = 0; i < startPoins.Count(); i++)
      {
        currentLocation.Add(startPoins.ElementAt(i).Key);
        Cycles.Add(0);
        bReachZ[i] = false;
      }
      //currentLocation.Add("VBT");
      //currentLocation.Add("LMQ");
      //currentLocation.Add("KLJ");
      //currentLocation.Add("MVT");
      //currentLocation.Add("MND");
      //currentLocation.Add("FJS");

      while (bReachZ.Any(cy => cy == false))
      //while (reachedDestination < currentLocation.Count)
      {
        foreach (char c in instructions)
        {
          reachedDestination = 0;

          // doing this here, I know its meant to be in the end
          steps++;

          for (int cl = 0; cl < currentLocation.Count; cl++)
          {
            //if (bReachZ[cl] == true) continue;

            switch (c)
            {
              case 'L':
                currentLocation[cl] = locations[currentLocation[cl]].Key;
                break;
              case 'R':
                currentLocation[cl] = locations[currentLocation[cl]].Value;
                break;
            }
            if (currentLocation[cl][2] == 'Z')
            {
              if (endPoins.Any(ep => ep.Key == currentLocation[cl]))
              {
                bReachZ[cl] = true;

                if (Cycles.All(cy => cy != 0) && steps % Cycles[cl] != 0)
                {
                  Debugger.Break();
                }
                else
                {
                  if (Cycles[cl] == 0) Cycles[cl] = steps;
                }
                reachedDestination++;
              }
            }
          }

          // steps actually are only finished here. to find the cycle, I will alredy count at the start
          //steps++;
          if (steps % 10_000_000 == 0)
          {
            Debugger.Log(1, "none", String.Format("\nSteps: '{0}'\n", steps));
          }
        }
      }
      // Calculate LCM
      /// LCM = calcLCM(12643 14257 15871 18023 19637 16409)
      /// 11795205644011

      lcm = Cycles.Aggregate((a, b) => LCM(a, b));

      result = lcm.ToString();
      return result;
    }

    /// <summary>
    /// method uses the GCD to calculate the least common multiple of two numbers.
    /// </summary>
    private long LCM(long a, long b)
    {
      long gcd = GCD(a, b);
      long ret = a / gcd * b;
      Debugger.Log(1, "lcm", String.Format(
        "\na '{0}', b '{1}', gcd '{2}', a/gcd '{3}', a/gcd*b '{4}'\n",
        a, b, gcd, a/gcd, ret));
      return ret;
    }

    /// <summary>
    /// method implements the Euclidean algorithm to find the greatest common divisor.
    /// 
    ///  For two numbers, the GCD is the largest number that divides both of them 
    ///  without leaving a remainder. The Euclidean algorithm is a common method 
    ///  for finding the GCD.
    /// </summary>
    private long GCD(long a, long b)
    {
      while (a != 0)
      {
        long temp = a;
        a = b % a;
        b = temp;
      }
      return b;
    }
  }
}
