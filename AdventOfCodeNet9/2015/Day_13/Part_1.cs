namespace AdventOfCodeNet9._2015.Day_13
{
  internal class Part_1 : Days
  {
    /// <summary>
    /*
    https://adventofcode.com/2015/day/13
--- Day 13: Knights of the Dinner Table ---
In years past, the holiday feast with your family hasn't gone so well. Not everyone gets along! This year, you resolve, will be different. You're going to find the optimal seating arrangement and avoid all those awkward conversations.

You start by writing up a list of everyone invited and the amount their happiness would increase or decrease if they were to find themselves sitting next to each other person. You have a circular table that will be just big enough to fit everyone comfortably, and so each person will have exactly two neighbors.

For example, suppose you have only four attendees planned, and you calculate their potential happiness as follows:

Alice would gain 54 happiness units by sitting next to Bob.
Alice would lose 79 happiness units by sitting next to Carol.
Alice would lose 2 happiness units by sitting next to David.
Bob would gain 83 happiness units by sitting next to Alice.
Bob would lose 7 happiness units by sitting next to Carol.
Bob would lose 63 happiness units by sitting next to David.
Carol would lose 62 happiness units by sitting next to Alice.
Carol would gain 60 happiness units by sitting next to Bob.
Carol would gain 55 happiness units by sitting next to David.
David would gain 46 happiness units by sitting next to Alice.
David would lose 7 happiness units by sitting next to Bob.
David would gain 41 happiness units by sitting next to Carol.
Then, if you seat Alice next to David, Alice would lose 2 happiness units (because David talks so much), but David would gain 46 happiness units (because Alice is such a good listener), for a total change of 44.

If you continue around the table, you could then seat Bob next to Alice (Bob gains 83, Alice gains 54). Finally, seat Carol, who sits next to Bob (Carol gains 60, Bob loses 7) and David (Carol gains 55, David gains 41). The arrangement looks like this:

     +41 +46
+55   David    -2
Carol       Alice
+60    Bob    +54
     -7  +83
After trying every other seating arrangement in this hypothetical scenario, you find that this one is the most optimal, with a total change in happiness of 330.

What is the total change in happiness for the optimal seating arrangement of the actual guest list?    */
    /// </summary>
    /// <returns>
    /// Your puzzle answer was 618.
    /// </returns>
    public override string Execute()
    {
      // see 2015 day 9_2

      string result = "";

      var happyness = new Dictionary<KeyValuePair<string, string>, int>();

      // Create all Routes
      foreach (string line in Lines)
      {
        var chunks = line.Trim('.').Split(' ');

        if (chunks[2] == "gain")
        {
          happyness.Add(new KeyValuePair<string, string>(chunks[0], chunks[10]), Int32.Parse(chunks[3]));
        }
        else // "lose"
        {
          happyness.Add(new KeyValuePair<string, string>(chunks[0], chunks[10]), Int32.Parse(chunks[3]) * -1);
        }
      }

      var persons = (from x in happyness.Keys select x.Key).Distinct();

      // CreateFacultyCombinations
      var sequence = new List<string>(persons.Count());
      foreach (var person in persons)
      {
        sequence.Add(person);
      }
      Calculate += OnCalculate;
      OverallHappyness = new List<int>();

      CreateSequence(new List<string>(), new List<string>(sequence), happyness);

      result = OverallHappyness.Max().ToString();
      return result;
    }

    public List<int> OverallHappyness;

    private void OnCalculate(List<string> combination, object itemtoworkon)
    {
      var happyDic = itemtoworkon as Dictionary<KeyValuePair<string, string>, int>;

      int totalHappyness = 0;
      string neighbour_pre = "";
      string neighbour_nxt = "";

      for (int i = 0; i < combination.Count; i++)
      {
        Console.Write(combination[i] + "\t");
        if (String.IsNullOrEmpty(neighbour_pre))
        {
          neighbour_pre = combination[combination.Count - 1]; // runder Tisch - neben dem ersten sitzt der letzte
          neighbour_nxt = combination[i + 1];
        }
        else if (i == combination.Count - 1)
        {
          neighbour_pre = combination[i - 1];
          neighbour_nxt = combination[0];
        }
        else
        {
          neighbour_pre = combination[i - 1];
          neighbour_nxt = combination[i + 1];
        }

        int happyness_pre = (from r in happyDic where r.Key.Key == combination[i] && r.Key.Value == neighbour_pre select r.Value).First();
        int happyness_nxt = (from r in happyDic where r.Key.Key == combination[i] && r.Key.Value == neighbour_nxt select r.Value).First();

        totalHappyness += happyness_pre + happyness_nxt;
      }
      Console.WriteLine("\t" + totalHappyness);
      OverallHappyness.Add(totalHappyness);
    }

    public event DoSomethingWithTheList Calculate;

    private void CreateSequence(List<string> sequence, List<string> modify, object itemToWorkOn)
    {
      if (modify.Count == 1)
      {
        // reached end of one path
        sequence.AddRange(modify);
        Calculate?.Invoke(sequence, itemToWorkOn);
        return;
      }

      for (int i = 0; i < modify.Count; i++)
      {
        var newSequence = new List<string>(sequence);
        var lessCombinations = new List<string>(modify);
        newSequence.Add(lessCombinations[i]);
        lessCombinations.RemoveAt(i);
        CreateSequence(newSequence, lessCombinations, itemToWorkOn);
      }
    }

    public delegate void DoSomethingWithTheList(List<string> combination, object itemToWorkOn);
  }
}
