using System.Diagnostics;
using System.Text.Json;
namespace AdventOfCodeNet10._2015.Day_12
{
  internal class Part_2_2015_Day_12 : Days
  {
    /// <summary>
    /*
    https://adventofcode.com/2015/day/12
    --- Day 12: JSAbacusFramework.io ---
    Santa's Accounting-Elves need help balancing the books after a recent order.
    Unfortunately, their accounting software uses a peculiar storage format. That's
    where you come in.
    
    They have a JSON document which contains a variety of things: arrays ([1,2,3]),
    objects ({"a":1, "b":2}), numbers, and strings. Your first job is to simply
    find all of the numbers throughout the document and add them together.
    
    For example:
    
    [1,2,3] and {"a":2,"b":4} both have a sum of 6.
    [[[3]]] and {"a":{"b":4},"c":-1} both have a sum of 3.
    {"a":[-1,1]} and [-1,{"a":1}] both have a sum of 0.
    [] and {} both have a sum of 0.
    You will not encounter any strings containing numbers.
    
    What is the sum of all numbers in the document?
    
    Your puzzle answer was 156366.
    
    The first half of this puzzle is complete! It provides one gold star: *
    
    --- Part Two ---
    Uh oh - the Accounting-Elves have realized that they double-counted everything
    red.
    
    Ignore any object (and all of its children) which has any property with the
    value "red". Do this only for objects ({...}), not arrays ([...]).
    
    [1,2,3] still has a sum of 6.
    [1,{"c":"red","b":2},3] now has a sum of 4, because the middle object is
    ignored.
    {"d":"red","e":[1,2,3,4],"f":5} now has a sum of 0, because the entire
    structure is ignored.
    [1,"red",5] has a sum of 6, because "red" in an array has no effect.    
	*/
    /// </summary>
    /// <returns>
    /// Your puzzle answer was 96852
    /// </returns>
    public override string Execute()
    {
      string result = "";

      string temp = Lines[0];

      //JsonDocumentOptions options = new JsonDocumentOptions();
      var jdoc = JsonDocument.Parse(temp);

      int total = CountElement(jdoc.RootElement);

      result = total.ToString();
      return result;  // 30598 too low
    }

    public int CountElement(JsonElement je)
    {
      switch (je.ValueKind)
      {
        case JsonValueKind.Array:
          int total = 0;
          foreach (JsonElement element in je.EnumerateArray())
          {
            total += CountElement(element);
          }
          return total;
        case JsonValueKind.Number:
          return je.GetInt32();
        case JsonValueKind.Object:
          int value = CheckObject(je, out var redfound);
          return redfound ? 0 : value;
        case JsonValueKind.String:
          // don't care in Arrays !!
          //if (je.GetString() == "red")
          //{
          //  redfound = true;
          //}
          break;
        default:
          Debugger.Break();
          break;
      }
      return 0;
    }

    private int CheckObject(JsonElement je, out bool redfound)
    {
      redfound = false;
      int total = 0;

      foreach (JsonProperty property in je.EnumerateObject())
      {
        if (property.Name == "red")
        {
          redfound = true;
        }

        switch (property.Value.ValueKind)
        {
          case JsonValueKind.Array:
            foreach (JsonElement element in property.Value.EnumerateArray())
            {
              total += CountElement(element);
            }
            break;
          case JsonValueKind.Object:
            int value = CheckObject(property.Value, out var subredFound);
            total += subredFound ? 0 : value;
            break;
          case JsonValueKind.Number:
            total += property.Value.GetInt32();
            break;
          case JsonValueKind.String:
            if (property.Value.GetString() == "red")
            {
              redfound = true;
            }
            break;
          default:
            Debugger.Break();
            break;
        }
      }

      return total;
    }
  }
}
