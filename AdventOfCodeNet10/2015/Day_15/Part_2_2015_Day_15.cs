namespace AdventOfCodeNet10._2015.Day_15
{
  internal class Part_2_2015_Day_15 : Days
  {
    /// <summary>
    /*
    https://adventofcode.com/2015/day/15
    --- Day 15: Science for Hungry People ---
    Today, you set out on the task of perfecting your milk-dunking cookie recipe.
    All you have to do is find the right balance of ingredients.
    
    Your recipe leaves room for exactly 100 teaspoons of ingredients. You make a
    list of the remaining ingredients you could use to finish the recipe (your
    puzzle input) and their properties per teaspoon:
    
    capacity (how well it helps the cookie absorb milk)
    durability (how well it keeps the cookie intact when full of milk)
    flavor (how tasty it makes the cookie)
    texture (how it improves the feel of the cookie)
    calories (how many calories it adds to the cookie)
    You can only measure ingredients in whole-teaspoon amounts accurately, and you
    have to be accurate so you can reproduce your results in the future. The total
    score of a cookie can be found by adding up each of the properties (negative
    totals become 0) and then multiplying together everything except calories.
    
    For instance, suppose you have these two ingredients:
    
    Butterscotch: capacity -1, durability -2, flavor 6, texture 3, calories 8
    Cinnamon: capacity 2, durability 3, flavor -2, texture -1, calories 3
    Then, choosing to use 44 teaspoons of butterscotch and 56 teaspoons of cinnamon
    (because the amounts of each ingredient must add up to 100) would result in a
    cookie with the following properties:
    
    A capacity of 44*-1 + 56*2 = 68
    A durability of 44*-2 + 56*3 = 80
    A flavor of 44*6 + 56*-2 = 152
    A texture of 44*3 + 56*-1 = 76
    Multiplying these together (68 * 80 * 152 * 76, ignoring calories for now)
    results in a total score of 62842880, which happens to be the best score
    possible given these ingredients. If any properties had produced a negative
    total, it would have instead become zero, causing the whole score to multiply
    to zero.
    
    Given the ingredients in your kitchen and their properties, what is the total
    score of the highest-scoring cookie you can make?
    
    Your puzzle answer was 21367368.
    
    The first half of this puzzle is complete! It provides one gold star: *
    
    --- Part Two ---
    Your cookie recipe becomes wildly popular! Someone asks if you can make another
    recipe that has exactly 500 calories per cookie (so they can use it as a meal
    replacement). Keep the rest of your award-winning process the same (100
    teaspoons, same ingredients, same scoring system).
    
    For example, given the ingredients above, if you had instead selected 40
    teaspoons of butterscotch and 60 teaspoons of cinnamon (which still adds to
    100), the total calorie count would be 40*8 + 60*3 = 500. The total score would
    go down, though: only 57600000, the best you can do in such trying
    circumstances.
    
    Given the ingredients in your kitchen and their properties, what is the total
    score of the highest-scoring cookie you can make with a calorie total of 500?
    */
    /// </summary>
    /// <returns>
    /// Your puzzle answer was 1766400.
    /// </returns>
    public override string Execute()
    {
      string result = "";
      // total 100

      List<Ingredient> ingredients = new List<Ingredient>();
      foreach (string line in Lines)
      {
        var chunks = line.Split(' ');
        ingredients.Add(new Ingredient(
          chunks[0].Trim(':'),
          Int32.Parse(chunks[2].Trim(',')),
          Int32.Parse(chunks[4].Trim(',')),
          Int32.Parse(chunks[6].Trim(',')),
          Int32.Parse(chunks[8].Trim(',')),
          Int32.Parse(chunks[10])
          ));
      }

      Dictionary<string, int> scores = new Dictionary<string, int>();

      for (int i0 = 0; i0 <= 100; i0++)
      {
        for (int i1 = 0; i1 <= 100; i1++)
        {
          for (int i2 = 0; i2 <= 100; i2++)
          {
            for (int i3 = 0; i3 <= 100; i3++)
            {
              if (i0 + i1 + i2 + i3 == 100)
              {
                int capacity = Math.Max(0,
                  ingredients[0].Capacity * i0 +
                  ingredients[1].Capacity * i1 +
                  ingredients[2].Capacity * i2 +
                  ingredients[3].Capacity * i3);
                int durability = Math.Max(0,
                  ingredients[0].Durability * i0 +
                  ingredients[1].Durability * i1 +
                  ingredients[2].Durability * i2 +
                  ingredients[3].Durability * i3);
                int flavor = Math.Max(0,
                  ingredients[0].Flavor * i0 +
                  ingredients[1].Flavor * i1 +
                  ingredients[2].Flavor * i2 +
                  ingredients[3].Flavor * i3);
                int texture = Math.Max(0,
                  ingredients[0].Texture * i0 +
                  ingredients[1].Texture * i1 +
                  ingredients[2].Texture * i2 +
                  ingredients[3].Texture * i3);

                int totalScore =
                  capacity * durability * flavor * texture;

                int calories = Math.Max(0,
                    ingredients[0].Calories * i0 +
                    ingredients[1].Calories * i1 +
                    ingredients[2].Calories * i2 +
                    ingredients[3].Calories * i3);

                if (calories == 500)
                {
                  scores.Add(String.Format("{0};{1};{2};{3}", i0, i1, i2, i3), totalScore);
                }
              }
              else
              {
                continue;
              }
            }
          }
        }
      }

      var winnerScore = (from w in scores select w.Value).Max();

      result = winnerScore.ToString();
      return result;
    }

    private class Ingredient
    {
      public string Name { get; private set; }
      public int Capacity { get; private set; }
      public int Durability { get; private set; }
      public int Flavor { get; private set; }
      public int Texture { get; private set; }
      public int Calories { get; private set; }

      public Ingredient(string name, int capacity, int durability, int flavor, int texture, int calories)
      {
        Name = name;
        Capacity = capacity;
        Durability = durability;
        Flavor = flavor;
        Texture = texture;
        Calories = calories;
      }
    }
  }
}
