using System.Diagnostics;
using ResolveEventArgs = System.ResolveEventArgs;

namespace AdventOfCodeNet9._2024.Day_07
{
  internal class Part_1_2024_Day_07 : Days
  {
    /// <summary>
    /*
    https://adventofcode.com/2024/day/7

    --- Day 7: Bridge Repair ---
    The Historians take you to a familiar rope bridge over a river in the middle of
    a jungle. The Chief isn't on this side of the bridge, though; maybe he's on the
    other side?
    
    When you go to cross the bridge, you notice a group of engineers trying to
    repair it. (Apparently, it breaks pretty frequently.) You won't be able to
    cross until it's fixed.
    
    You ask how long it'll take; the engineers tell you that it only needs final
    calibrations, but some young elephants were playing nearby and stole all the
    operators from their calibration equations! They could finish the calibrations
    if only someone could determine which test values could possibly be produced by
    placing any combination of operators into their calibration equations (your
    puzzle input).
    
    For example:
    
    190: 10 19
    3267: 81 40 27
    83: 17 5
    156: 15 6
    7290: 6 8 6 15
    161011: 16 10 13
    192: 17 8 14
    21037: 9 7 18 13
    292: 11 6 16 20
    Each line represents a single equation. The test value appears before the colon
    on each line; it is your job to determine whether the remaining numbers can be
    combined with operators to produce the test value.
    
    Operators are always evaluated left-to-right, not according to precedence
    rules. Furthermore, numbers in the equations cannot be rearranged. Glancing
    into the jungle, you can see elephants holding two different types of
    operators: add (+) and multiply (*).
    
    Only three of the above equations can be made true by inserting operators:
    
    190: 10 19 has only one position that accepts an operator: between 10 and 19.
    Choosing + would give 29, but choosing * would give the test value (10 * 19 =
    190).
    3267: 81 40 27 has two positions for operators. Of the four possible
    configurations of the operators, two cause the right side to match the test
    value: 81 + 40 * 27 and 81 * 40 + 27 both equal 3267 (when evaluated
    left-to-right)!
    292: 11 6 16 20 can be solved in exactly one way: 11 + 6 * 16 + 20.
    The engineers just need the total calibration result, which is the sum of the
    test values from just the equations that could possibly be true. In the above
    example, the sum of the test values for the three equations listed above is
    3749.
    
    Determine which equations could possibly be true. What is their total
    calibration result?

    */
    /// </summary>
    /// <returns>
    /// 3312271365652
    /// </returns>
    public override string Execute()
    {
      string result = "";
      long totalCount = 0;

      List<calc> allEquasions = new List<calc>();

      foreach (var line in Lines)
      {
        var chunks1 = line.Split(':');
        var chunks2 = chunks1[1].Split(' ',StringSplitOptions.RemoveEmptyEntries);
        List<long> lNumbers = new List<long>();

        foreach (var s in chunks2)
        {
          lNumbers.Add(long.Parse(s));
        }
        var cRecord = new calc(long.Parse(chunks1[0]),lNumbers);
        var cClass = new Calculate(cRecord);
        totalCount += cClass.TestCalculation();
      }

      result = totalCount.ToString();
      return result;
    }
  }

  record calc(long res, List<long> numbers);
  internal class Calculate
  {
    private calc _calc;

    public Calculate(calc aCalc)
    {
      _calc = aCalc;
    }

    public long TestCalculation()
    {
      List<long> numbers = new List<long>();
      numbers.AddRange(CalculatePairs(_calc.numbers[0], "+", _calc.numbers[1..]));
      numbers.AddRange(CalculatePairs(_calc.numbers[0], "*", _calc.numbers[1..]));

      if (numbers.Contains(_calc.res))
        return _calc.res;
      else
        return 0L;
    }

    private List<long> CalculatePairs(long ResultSoFar, string strOperator, List<long> restNumbers)
    {
      var tempNumbers = new List<long>();

      if (restNumbers.Count == 1)
      {
        switch (strOperator)
        {
          case "+":
            tempNumbers.Add(ResultSoFar + restNumbers[0]);
            break;
          case "*":
            tempNumbers.Add(ResultSoFar * restNumbers[0]);
            break;
        }
      }
      else if (restNumbers.Count > 1)
      {
        switch (strOperator)
        {
          case "+":
            tempNumbers.AddRange(CalculatePairs(ResultSoFar + restNumbers[0], "+", restNumbers[1..]));
            tempNumbers.AddRange(CalculatePairs(ResultSoFar + restNumbers[0], "*", restNumbers[1..]));
            break;
          case "*":
            tempNumbers.AddRange(CalculatePairs(ResultSoFar * restNumbers[0], "+", restNumbers[1..]));
            tempNumbers.AddRange(CalculatePairs(ResultSoFar * restNumbers[0], "*", restNumbers[1..]));
            break;
        }
      }
      else
      {
        Debugger.Break();
      }
      return tempNumbers;
    }
  }
}

