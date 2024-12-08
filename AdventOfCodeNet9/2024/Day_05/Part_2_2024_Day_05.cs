using System.Diagnostics;
using System.Security.Authentication;

namespace AdventOfCodeNet9._2024.Day_05
{
  internal class Part_2_2024_Day_05 : Days
  {
    /// <summary>
    /*
    https://adventofcode.com/2024/day/5
    --- Part Two ---
    While the Elves get to work printing the correctly-ordered updates, you have a
    little time to fix the rest of them.
    
    For each of the incorrectly-ordered updates, use the page ordering rules to put
    the page numbers in the right order. For the above example, here are the three
    incorrectly-ordered updates and their correct orderings:
    
    75,97,47,61,53 becomes 97,75,47,61,53.
    61,13,29 becomes 61,29,13.
    97,13,75,29,47 becomes 97,75,47,29,13.
    After taking only the incorrectly-ordered updates and ordering them correctly,
    their middle page numbers are 47, 29, and 47. Adding these together produces
    123.
    
    Find the updates which are not in the correct order. What do you get if you add
    up the middle page numbers after correctly ordering just those updates?

    */
    /// </summary>
    /// <returns>
    /// 6034 -> Part 1
    /// 12339 -> too High I used all of them
    /// 6305
    /// </returns>
    public override string Execute()
    {
      string result = "";

      List<KeyValuePair<int, int>> rules = new List<KeyValuePair<int, int>>();
      List<List<int>> orders = new List<List<int>>();

      #region DataAquesition
      foreach (var line in Lines)
      {
        if (line.Contains('|'))
        {
          var chunk_r = line.Split('|', StringSplitOptions.RemoveEmptyEntries);
          rules.Add(
            new KeyValuePair<int, int>
            (
              Int32.Parse(chunk_r[0]),
              Int32.Parse(chunk_r[1])
            )
          );
        }
        else
        {
          if (line.Contains(','))
          {
            var chunk_orders = line.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList();

            List<int> singleOrder = new List<int>();
            foreach (var chunk_o in chunk_orders)
            {
              singleOrder.Add(Int32.Parse(chunk_o));
            }

            orders.Add(singleOrder);
          }
        }
      }
      #endregion DataAquesition


      long sumMiddleNumbers = 0;

      foreach (var order in orders)
      {
        bool workingOrder = true;

        // Check if order is correct !!!
        int numbersOK = 0;

        Debug.Assert(order.Count % 2 == 1);
        if (order.Count % 2 == 0) Debugger.Break();

        int middleIndex = order.Count / 2;

        for (int i = 0; i < order.Count; i++)
        {
          var keylist = (
            from k in rules
            where k.Value == order[i]
            select k.Key).ToList();

          var valuelist = (
            from k in rules
            where k.Key == order[i]
            select k.Value).ToList();

          if (CheckRulesForThisNumber(i, order, keylist, valuelist, out var brokenIndex))
          {
            numbersOK++;
          }
          else
          {
            workingOrder = false;
            // exchange this number from this index with the broken index
            int brokenNumber = order[brokenIndex];
            order.RemoveAt(brokenIndex);
            order.Insert(brokenIndex, order[i]);
            order.RemoveAt(i);
            order.Insert(i, brokenNumber);

            // start the loop again
            numbersOK = 0;
            i = -1;
          }
        }

        if (numbersOK == order.Count)
        {
          if (!workingOrder)
          {
            sumMiddleNumbers += order[middleIndex];
          }
        }
      }

      result = sumMiddleNumbers.ToString();
      return result;
    }

    private bool CheckRulesForThisNumber(int index, List<int> order, List<int> keylist, List<int> valuelist, out int broken)
    {
      broken = -1;

      if (index != order.Count - 1) // search for the Keys to the right
      // The fourth update, 75,97,47,61,53, is not in the correct order:
      // it would print 75 before 97,
      // which violates the rule 97 | 75.
      {
        for (int mover = index + 1; mover < order.Count; mover++)
        {
          if (keylist.Contains(order[mover]))
          {
            broken = mover;
            return false;
          }
        }
      }

      if (index != 0) // search for the Values to the left
      {
        for (int movel = index - 1; movel >= 0; movel--)
        {
          if (valuelist.Contains(order[movel]))
          {
            broken = movel;
            return false;
          }
        }
      }

      // if we get here there was no return false
      return true;
    }
  }
}
