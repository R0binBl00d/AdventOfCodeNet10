namespace AdventOfCodeNet9._2015.Day_21
{
  internal class Part_2_2015_Day_21 : Days
  {
    /// <summary>
    /*
    https://adventofcode.com/2015/day/21
    --- Day 21: RPG Simulator 20XX ---
    Little Henry Case got a new video game for Christmas. It's an RPG, and he's
    stuck on a boss. He needs to know what equipment to buy at the shop. He hands
    you the controller.
    
    In this game, the player (you) and the enemy (the boss) take turns attacking.
    The player always goes first. Each attack reduces the opponent's hit points by
    at least 1. The first character at or below 0 hit points loses.
    
    Damage dealt by an attacker each turn is equal to the attacker's damage score
    minus the defender's armor score. An attacker always does at least 1 damage.
    So, if the attacker has a damage score of 8, and the defender has an armor
    score of 3, the defender loses 5 hit points. If the defender had an armor score
    of 300, the defender would still lose 1 hit point.
    
    Your damage score and armor score both start at zero. They can be increased by
    buying items in exchange for gold. You start with no items and have as much
    gold as you need. Your total damage or armor is equal to the sum of those stats
    from all of your items. You have 100 hit points.
    
    Here is what the item shop is selling:
    
    Weapons:    Cost  Damage  Armor
    Dagger        8     4       0
    Shortsword   10     5       0
    Warhammer    25     6       0
    Longsword    40     7       0
    Greataxe     74     8       0
    
    Armor:      Cost  Damage  Armor
    Leather      13     0       1
    Chainmail    31     0       2
    Splintmail   53     0       3
    Bandedmail   75     0       4
    Platemail   102     0       5
    
    Rings:      Cost  Damage  Armor
    Damage +1    25     1       0
    Damage +2    50     2       0
    Damage +3   100     3       0
    Defense +1   20     0       1
    Defense +2   40     0       2
    Defense +3   80     0       3
    You must buy exactly one weapon; no dual-wielding. Armor is optional, but you
    can't use more than one. You can buy 0-2 rings (at most one for each hand). You
    must use any items you buy. The shop only has one of each item, so you can't
    buy, for example, two rings of Damage +3.
    
    For example, suppose you have 8 hit points, 5 damage, and 5 armor, and that the
    boss has 12 hit points, 7 damage, and 2 armor:
    
    The player deals 5-2 = 3 damage; the boss goes down to 9 hit points.
    The boss deals 7-5 = 2 damage; the player goes down to 6 hit points.
    The player deals 5-2 = 3 damage; the boss goes down to 6 hit points.
    The boss deals 7-5 = 2 damage; the player goes down to 4 hit points.
    The player deals 5-2 = 3 damage; the boss goes down to 3 hit points.
    The boss deals 7-5 = 2 damage; the player goes down to 2 hit points.
    The player deals 5-2 = 3 damage; the boss goes down to 0 hit points.
    In this scenario, the player wins! (Barely.)
    
    You have 100 hit points. The boss's actual stats are in your puzzle input. What
    is the least amount of gold you can spend and still win the fight?
    
    Your puzzle answer was 91.
    
    The first half of this puzzle is complete! It provides one gold star: *
    
    --- Part Two ---
    Turns out the shopkeeper is working with the boss, and can persuade you to buy
    whatever items he wants. The other rules still apply, and he still only has one
    of each item.
    
    What is the most amount of gold you can spend and still lose the fight?
    
    */
    /// </summary>
    /// <returns>
    /// 
    /// </returns>
    public override string Execute()
    {
      string result = "";

      List<int> moneySpentForLoosing = new List<int>();

      List<Item> weapons = new List<Item>()
      {
        new Item() { Name = "Dagger", Cost = 8, Damage = 4, Armor = 0 },
        new Item() { Name = "Shortsword", Cost = 10, Damage = 5, Armor = 0 },
        new Item() { Name = "Warhammer", Cost = 25, Damage = 6, Armor = 0 },
        new Item() { Name = "Longsword", Cost = 40, Damage = 7, Armor = 0 },
        new Item() { Name = "Greataxe", Cost = 74, Damage = 8, Armor = 0 }
      };

      List<Item> armor = new List<Item>()
      {
        new Item() { Name = "none", Cost = 0, Damage = 0, Armor = 0 },
        new Item() { Name = "Leather", Cost = 13, Damage = 0, Armor = 1 },
        new Item() { Name = "Chainmail", Cost = 31, Damage = 0, Armor = 2 },
        new Item() { Name = "Splintmail", Cost = 53, Damage = 0, Armor = 3 },
        new Item() { Name = "Bandedmail", Cost = 75, Damage = 0, Armor = 4 },
        new Item() { Name = "Platemail", Cost = 102, Damage = 0, Armor = 5 }
      };

      List<Item> rings = new List<Item>()
      {
        new Item() { Name = "none1", Cost = 0, Damage = 0, Armor = 0 },
        new Item() { Name = "none2", Cost = 0, Damage = 0, Armor = 0 },
        new Item() { Name = "Damage +1", Cost = 25, Damage = 1, Armor = 0 },
        new Item() { Name = "Damage +2", Cost = 50, Damage = 2, Armor = 0 },
        new Item() { Name = "Damage +3", Cost = 100, Damage = 3, Armor = 0 },
        new Item() { Name = "Defense +1", Cost = 20, Damage = 0, Armor = 1 },
        new Item() { Name = "Defense +2", Cost = 40, Damage = 0, Armor = 2 },
        new Item() { Name = "Defense +3", Cost = 80, Damage = 0, Armor = 3 }
      };

      #region GameLoop

      foreach (var weapon in weapons) // always one of them
      {
        foreach (var armorItem in armor) // 0 or one of them
        {
          // 0,1,2 rings - two loops never the same item !!
          for (int x = 0; x < rings.Count; x++)
          {
            for (int y = x + 1; y < rings.Count; y++)
            {
              List<Item> equiptment = new List<Item>()
              {
                weapon,
                armorItem,
                rings[x], rings[y]
              };

              var player = new Fighter()
              {
                HitPoints = 100,
                Damage = 0,
                Armor = 0
              };

              var boss = new Fighter()
              {
                HitPoints = 100,
                Damage = 8,
                Armor = 2
              };

              int moneySpent = 0;
              // buy stuff !! see equiptment
              foreach (var e in equiptment)
              {
                player.Damage += e.Damage;
                player.Armor += e.Armor;
                // calculate money
                moneySpent += e.Cost;
              }

              bool isGameOver = false;
              do // fight !!
              {
                player.attacks(boss);
                if (boss.HitPoints <= 0) break;   // boss looses

                boss.attacks(player);
                if (player.HitPoints <= 0) break; // player looses
              } while (!isGameOver);

              // Did the player win?
              if (player.HitPoints <= 0 && boss.HitPoints > 0)
              {
                // save the amount of money !!
                moneySpentForLoosing.Add(moneySpent);
              }
            }
          }
        }
      }

      #endregion GameLoop

      var mostAmountOfMoney =
        (from m in moneySpentForLoosing select m).Max();

      result = mostAmountOfMoney.ToString();
      return result;
    }

    private class Fighter
    {
      public int HitPoints { get; set; }
      public int Damage { get; set; }
      public int Armor { get; set; }

      public void attacks(Fighter oponent)
      {
        // oponent = defender
        int loss = Math.Max(this.Damage - oponent.Armor, 1);
        oponent.HitPoints -= loss;
      }
    }

    private class Item
    {
      public string Name { get; set; }
      public int Cost { get; set; }
      public int Damage { get; set; }
      public int Armor { get; set; }
    }
  }
}
