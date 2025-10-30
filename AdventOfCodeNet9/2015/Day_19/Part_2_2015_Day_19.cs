using System.Text.RegularExpressions;
namespace AdventOfCodeNet10._2015.Day_19
{
  internal class Part_2_2015_Day_19 : Days
  {
    /// <summary>
    /*
    https://adventofcode.com/2015/day/19
    --- Day 19: Medicine for Rudolph ---
    Rudolph the Red-Nosed Reindeer is sick! His nose isn't shining very brightly,
    and he needs medicine.
    
    Red-Nosed Reindeer biology isn't similar to regular reindeer biology; Rudolph
    is going to need custom-made medicine. Unfortunately, Red-Nosed Reindeer
    chemistry isn't similar to regular reindeer chemistry, either.
    
    The North Pole is equipped with a Red-Nosed Reindeer nuclear fusion/fission
    plant, capable of constructing any Red-Nosed Reindeer molecule you need. It
    works by starting with some input molecule and then doing a series of
    replacements, one per step, until it has the right molecule.
    
    However, the machine has to be calibrated before it can be used. Calibration
    involves determining the number of molecules that can be generated in one step
    from a given starting point.
    
    For example, imagine a simpler machine that supports only the following
    replacements:
    
    H => HO
    H => OH
    O => HH
    Given the replacements above and starting with HOH, the following molecules
    could be generated:
    
    HOOH (via H => HO on the first H).
    HOHO (via H => HO on the second H).
    OHOH (via H => OH on the first H).
    HOOH (via H => OH on the second H).
    HHHH (via O => HH).
    So, in the example above, there are 4 distinct molecules (not five, because
    HOOH appears twice) after one replacement from HOH. Santa's favorite molecule,
    HOHOHO, can become 7 distinct molecules (over nine replacements: six from H,
    and three from O).
    
    The machine replaces without regard for the surrounding characters. For
    example, given the string H2O, the transition H => OO would result in OO2O.
    
    Your puzzle input describes all of the possible replacements and, at the
    bottom, the medicine molecule for which you need to calibrate the machine. How
    many distinct molecules can be created after all the different ways you can do
    one replacement on the medicine molecule?
    
    Your puzzle answer was 509.
    
    The first half of this puzzle is complete! It provides one gold star: *
    
    --- Part Two ---
    Now that the machine is calibrated, you're ready to begin molecule fabrication.
    
    Molecule fabrication always begins with just a single electron, e, and applying
    replacements one at a time, just like the ones during calibration.
    
    For example, suppose you have the following replacements:
    
    e => H
    e => O
    H => HO
    H => OH
    O => HH
    If you'd like to make HOH, you start with e, and then make the following
    replacements:
    
    e => O to get O
    O => HH to get HH
    H => OH (on the second H) to get HOH
    So, you could make HOH after 3 steps. Santa's favorite molecule, HOHOHO, can be
    made in 6 steps.
    
    How long will it take to make the medicine? Given the available replacements
    and the medicine molecule in your puzzle input, what is the fewest number of
    steps to go from e to the medicine molecule?
    */
    /// </summary>
    /// <returns>
    /// Your puzzle answer was 195.
    /// </returns>

    public override string Execute()
    {
      string result = "";

      string molecule = Lines[Lines.Count - 1];

      Lines.RemoveAt(Lines.Count-1);

      var replacements = Lines
        .Select(line => line.Split(new string[]{" => "}, StringSplitOptions.RemoveEmptyEntries))
        .ToDictionary(split => split[1], split => split[0]);

      int count = 0;
      Random rnd = new Random();

      while (molecule != "e")
      {
        var randomMolecule = replacements.Keys.ElementAt(rnd.Next(replacements.Count));
        var regex = new Regex(Regex.Escape(randomMolecule));

        molecule = regex.Replace(molecule, match =>
        {
          count++;
          return replacements[match.Value];
        }, 1);

        Console.WriteLine($"{molecule} -> {count}");
      }
      return result;
    }

    /*
    public override string Execute()
    {
      string result = "";

      Dictionary<string, string> replacementsList = new Dictionary<string, string>();
      string startModecule = "";

      foreach (string line in Lines)
      {
        string[] chunks = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

        if (chunks.Length == 3)
        {
          if (replacementsList.ContainsKey(chunks[2]))
          {
            Debugger.Break();
            //replacementsList[chunks[2]]=(chunks[0]);
          }
          else
          {
            replacementsList.Add(chunks[2], chunks[0]);
          }
        }
        else
        {
          // reverse the fucker instead of trying to create it using uniqueness and modifications !!
          startModecule = chunks[0];
        }
      }

      CreateShortendCombinations(replacementsList, startModecule, 0);

      if (EndedInE.Count > 0)
      {
        var winner = (from w in EndedInE select w).Min();
        result = winner.ToString();
      }

      return result;
    }

    ConcurrentBag<int> EndedInE = new ConcurrentBag<int>();

    //void CreateShortendCombinations(Dictionary<string, string> replacementsList, string StartModecule, int stepCount)
    //{
    //  int startIndex = -1;
    //  foreach (KeyValuePair<string, string> rEntry in replacementsList)
    //  {
    //    int position = StartModecule.IndexOf(rEntry.Key, startIndex + 1);

    //    while (position > -1)
    //    {
    //      string insert = rEntry.Value;
    //      string tmp = StartModecule;
    //      tmp = tmp.Remove(position, rEntry.Key.Length);
    //      tmp = tmp.Insert(position, insert);

    //      if (tmp == "e")
    //      {
    //        EndedInE.Add(stepCount + 1);
    //      }
    //      else
    //      {
    //        CreateShortendCombinations(ref replacementsList, tmp, stepCount + 1);
    //      }
    //      position = StartModecule.IndexOf(rEntry.Key, position + 1);
    //    }
    //  }
      Parallel.ForEach(replacementsList, rEntry =>
      {
        int position = StartModecule.IndexOf(rEntry.Key, 0);

        while (position > -1)
        {
          string insert = rEntry.Value;
          string tmp = StartModecule;
          tmp = tmp.Remove(position, rEntry.Key.Length);
          tmp = tmp.Insert(position, insert);

          if (tmp == "e")
          {
            EndedInE.Add(stepCount + 1);
          }
          else
          {
            CreateShortendCombinations(replacementsList, tmp, stepCount + 1);
          }

          position = StartModecule.IndexOf(rEntry.Key, position + 1);
        }
      });
    }
    */
  }
}
