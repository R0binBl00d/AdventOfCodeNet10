namespace AdventOfCodeNet10._2015.Day_09
{
  internal class Part_2_2015_Day_09 : Days
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
    
    Your puzzle answer was 207.
    
    The first half of this puzzle is complete! It provides one gold star: *
    
    --- Part Two ---
    The next year, just to show off, Santa decides to take the route with the
    longest distance instead.
    
    He can still start and end at any two (different) locations he wants, and he
    still must visit each location exactly once.
    
    For example, given the distances above, the longest route would be 982 via (for
    example) Dublin -> London -> Belfast.
    
    What is the distance of the longest route?
    */
    /// </summary>
    /// <returns>
    /// Your puzzle answer was 804.
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
      resultLengths = new List<int>();

      CreateSequence(new List<string>(), new List<string>(sequence), routes);

      result = resultLengths.Max().ToString();
      return result;
    }

    public List<int> resultLengths;

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
