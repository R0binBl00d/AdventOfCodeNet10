using System.Diagnostics;

namespace AdventOfCodeNet9._2024.Day_07
{
  internal class Part_2_2024_Day_07 : Days
  {
    /// <summary>
    /*
    https://adventofcode.com/2024/day/7
    */
    /// </summary>
    /// <returns>
    /// 3312271365652 -> day one
    /// 3317502086788 -> too low
    /// 509463489296712
    /// </returns>
    public override string Execute()
    {
      string result = "";
      long totalCount = 0;

      List<calc> allEquasions = new List<calc>();

      foreach (var line in Lines)
      {
        var chunks1 = line.Split(':');
        var chunks2 = chunks1[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);
        List<long> lNumbers = new List<long>();

        foreach (var s in chunks2)
        {
          lNumbers.Add(long.Parse(s));
        }
        var cRecord = new calc(long.Parse(chunks1[0]), lNumbers);
        var cClass2 = new Calculate2(cRecord);
        totalCount += cClass2.TestCalculation();
      }

      result = totalCount.ToString();
      return result;
    }
  }

  internal class Calculate2
  {
    private calc _calc;

    public Calculate2(calc aCalc)
    {
      _calc = aCalc;
    }

    public long TestCalculation()
    {
      List<long> numbers = new List<long>();
      numbers.AddRange(CalculatePairs(_calc.numbers[0], "+", _calc.numbers[1..]));
      numbers.AddRange(CalculatePairs(_calc.numbers[0], "*", _calc.numbers[1..]));
      numbers.AddRange(CalculatePairs(_calc.numbers[0], "|", _calc.numbers[1..]));

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
          case "|":
            tempNumbers.Add(long.Parse($"{ResultSoFar}{restNumbers[0]}"));
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
            tempNumbers.AddRange(CalculatePairs(ResultSoFar + restNumbers[0], "|", restNumbers[1..]));
            break;
          case "*":
            tempNumbers.AddRange(CalculatePairs(ResultSoFar * restNumbers[0], "+", restNumbers[1..]));
            tempNumbers.AddRange(CalculatePairs(ResultSoFar * restNumbers[0], "*", restNumbers[1..]));
            tempNumbers.AddRange(CalculatePairs(ResultSoFar * restNumbers[0], "|", restNumbers[1..]));
            break;
          case "|":
            tempNumbers.AddRange(CalculatePairs(long.Parse($"{ResultSoFar}{restNumbers[0]}"), "+", restNumbers[1..]));
            tempNumbers.AddRange(CalculatePairs(long.Parse($"{ResultSoFar}{restNumbers[0]}"), "*", restNumbers[1..]));
            tempNumbers.AddRange(CalculatePairs(long.Parse($"{ResultSoFar}{restNumbers[0]}"), "|", restNumbers[1..]));
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

