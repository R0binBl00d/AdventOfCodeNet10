namespace AdventOfCodeNet10._2015.Day_09
{
  internal class Part_1_2015_Day_09 : Days
  {
    /// <summary>
    /*
    https://adventofcode.com/2015/day/9
    --- Day 9: All in a Single Night ---
    Every year, Santa manages to deliver all of his presents in a single night.
    
    This year, however, he has some new locations to visit; his elves have provided
    him the distances between every pair of locations. He can start and end at any
    two (different) locations he wants, but he must visit each location exactly
    once. What is the shortest distance he can travel to achieve this?
    
    For example, given the following distances:
    
    London to Dublin = 464
    London to Belfast = 518
    Dublin to Belfast = 141
    The possible routes are therefore:
    
    Dublin -> London -> Belfast = 982
    London -> Dublin -> Belfast = 605
    London -> Belfast -> Dublin = 659
    Dublin -> Belfast -> London = 659
    Belfast -> Dublin -> London = 605
    Belfast -> London -> Dublin = 982
    The shortest of these is London -> Dublin -> Belfast = 605, and so the answer
    is 605 in this example.
    
    What is the distance of the shortest route?
    */
    /// </summary>
    /// <returns>
    /// Your puzzle answer was 207.
    /// </returns>
    public override string Execute()
    {
      string result = "";

      var routes = new Dictionary<KeyValuePair<string, string>, int>();

      // Create all Routes
      foreach (string line in Lines)
      {
        var chunks = line.Split(' ');
        routes.Add(new KeyValuePair<string, string>(chunks[0], chunks[2]), Int32.Parse(chunks[4]));
        routes.Add(new KeyValuePair<string, string>(chunks[2], chunks[0]), Int32.Parse(chunks[4]));
      }

      var cities = (from x in routes.Keys select x.Key).Distinct();

      // CreateFacultyCombinations
      var sequence = new List<string>(cities.Count());
      foreach (var city in cities)
      {
        sequence.Add(city);
      }
      Calculate += OnCalculate;

      CreateSequence(new List<string>(), new List<string>(sequence), routes);

      result = resultLengths.Min().ToString();
      return result;
    }

    public List<int> resultLengths = new List<int>();

    private void OnCalculate(List<string> combination, object itemtoworkon)
    {
      var routesDic = itemtoworkon as Dictionary<KeyValuePair<string, string>, int>;

      int lengthOfRoute = 0;
      string lastTown = "";
      foreach (string s in combination)
      {
        Console.Write(s + "\t");
        if (!String.IsNullOrEmpty(lastTown))
        {
          int routeLength = (from r in routesDic where r.Key.Key == lastTown && r.Key.Value == s select r.Value).First();
          lengthOfRoute += routeLength;
        }
        lastTown = s;
      }
      Console.WriteLine("\t" + lengthOfRoute);
      resultLengths.Add(lengthOfRoute);
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    public event DoSomethingWithTheList Calculate;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

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
