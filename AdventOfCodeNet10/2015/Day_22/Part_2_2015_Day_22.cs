namespace AdventOfCodeNet10._2015.Day_22
{
  internal class Part_2_2015_Day_22 : Days
  {
    /// <summary>
    /*
    https://adventofcode.com/2015/day/22
    --- Day 22: Wizard Simulator 20XX ---
    Little Henry Case decides that defeating bosses with swords and stuff is
    boring. Now he's playing the game with a wizard. Of course, he gets stuck on
    another boss and needs your help again.
    
    In this version, combat still proceeds with the player and the boss taking
    alternating turns. The player still goes first. Now, however, you don't get any
    equipment; instead, you must choose one of your spells to cast. The first
    character at or below 0 hit points loses.
    
    Since you're a wizard, you don't get to wear armor, and you can't attack
    normally. However, since you do magic damage, your opponent's armor is ignored,
    and so the boss effectively has zero armor as well. As before, if armor (from a
    spell, in this case) would reduce damage below 1, it becomes 1 instead - that
    is, the boss' attacks always deal at least 1 damage.
    
    On each of your turns, you must select one of your spells to cast. If you
    cannot afford to cast any spell, you lose. Spells cost mana; you start with 500
    mana, but have no maximum limit. You must have enough mana to cast a spell, and
    its cost is immediately deducted when you cast it. Your spells are Magic
    Missile, Drain, Shield, Poison, and Recharge.
    
    Magic Missile costs 53 mana. It instantly does 4 damage.
    Drain costs 73 mana. It instantly does 2 damage and heals you for 2 hit points.
    Shield costs 113 mana. It starts an effect that lasts for 6 turns. While it is
    active, your armor is increased by 7.
    Poison costs 173 mana. It starts an effect that lasts for 6 turns. At the start
    of each turn while it is active, it deals the boss 3 damage.
    Recharge costs 229 mana. It starts an effect that lasts for 5 turns. At the
    start of each turn while it is active, it gives you 101 new mana.
    Effects all work the same way. Effects apply at the start of both the player's
    turns and the boss' turns. Effects are created with a timer (the number of
    turns they last); at the start of each turn, after they apply any effect they
    have, their timer is decreased by one. If this decreases the timer to zero, the
    effect ends. You cannot cast a spell that would start an effect which is
    already active. However, effects can be started on the same turn they end.
    
    For example, suppose the player has 10 hit points and 250 mana, and that the
    boss has 13 hit points and 8 damage:
    
    -- Player turn --
    - Player has 10 hit points, 0 armor, 250 mana
    - Boss has 13 hit points
    Player casts Poison.
    
    -- Boss turn --
    - Player has 10 hit points, 0 armor, 77 mana
    - Boss has 13 hit points
    Poison deals 3 damage; its timer is now 5.
    Boss attacks for 8 damage.
    
    -- Player turn --
    - Player has 2 hit points, 0 armor, 77 mana
    - Boss has 10 hit points
    Poison deals 3 damage; its timer is now 4.
    Player casts Magic Missile, dealing 4 damage.
    
    -- Boss turn --
    - Player has 2 hit points, 0 armor, 24 mana
    - Boss has 3 hit points
    Poison deals 3 damage. This kills the boss, and the player wins.
    Now, suppose the same initial conditions, except that the boss has 14 hit
    points instead:
    
    -- Player turn --
    - Player has 10 hit points, 0 armor, 250 mana
    - Boss has 14 hit points
    Player casts Recharge.
    
    -- Boss turn --
    - Player has 10 hit points, 0 armor, 21 mana
    - Boss has 14 hit points
    Recharge provides 101 mana; its timer is now 4.
    Boss attacks for 8 damage!
    
    -- Player turn --
    - Player has 2 hit points, 0 armor, 122 mana
    - Boss has 14 hit points
    Recharge provides 101 mana; its timer is now 3.
    Player casts Shield, increasing armor by 7.
    
    -- Boss turn --
    - Player has 2 hit points, 7 armor, 110 mana
    - Boss has 14 hit points
    Shield's timer is now 5.
    Recharge provides 101 mana; its timer is now 2.
    Boss attacks for 8 - 7 = 1 damage!
    
    -- Player turn --
    - Player has 1 hit point, 7 armor, 211 mana
    - Boss has 14 hit points
    Shield's timer is now 4.
    Recharge provides 101 mana; its timer is now 1.
    Player casts Drain, dealing 2 damage, and healing 2 hit points.
    
    -- Boss turn --
    - Player has 3 hit points, 7 armor, 239 mana
    - Boss has 12 hit points
    Shield's timer is now 3.
    Recharge provides 101 mana; its timer is now 0.
    Recharge wears off.
    Boss attacks for 8 - 7 = 1 damage!
    
    -- Player turn --
    - Player has 2 hit points, 7 armor, 340 mana
    - Boss has 12 hit points
    Shield's timer is now 2.
    Player casts Poison.
    
    -- Boss turn --
    - Player has 2 hit points, 7 armor, 167 mana
    - Boss has 12 hit points
    Shield's timer is now 1.
    Poison deals 3 damage; its timer is now 5.
    Boss attacks for 8 - 7 = 1 damage!
    
    -- Player turn --
    - Player has 1 hit point, 7 armor, 167 mana
    - Boss has 9 hit points
    Shield's timer is now 0.
    Shield wears off, decreasing armor by 7.
    Poison deals 3 damage; its timer is now 4.
    Player casts Magic Missile, dealing 4 damage.
    
    -- Boss turn --
    - Player has 1 hit point, 0 armor, 114 mana
    - Boss has 2 hit points
    Poison deals 3 damage. This kills the boss, and the player wins.
    You start with 50 hit points and 500 mana points. The boss's actual stats are
    in your puzzle input. What is the least amount of mana you can spend and still
    win the fight? (Do not include mana recharge effects as "spending" negative
    mana.)
    
    Your puzzle answer was 953.
    
    --- Part Two ---
    On the next run through the game, you increase the difficulty to hard.
    
    At the start of each player turn (before any other effects apply), you lose 1
    hit point. If this brings you to or below 0 hit points, you lose.
    
    With the same starting stats for you and the boss, what is the least amount of
    mana you can spend and still win the fight?
    
    Your puzzle answer was 1289.
    */
    /// </summary>
    /// <returns>     3,4,3,3,0,0 = 854 = 4,3,3,3,0,0
    /// 967 answer is too low !!
    /// 987 answer is too low !!
    /// 
    /// 1269 answer is wrong !!
    /// 
    /// 1289 taken from node.js :-/ could not solve it yet
    ///
    /// 1295 answer is wrong !!
    /// 1309 answer is wrong !!
    /// 
    /// 1329 answer is too high !!
    /// </returns>
    public override string Execute()
    {
      string returns = "";
      Player me = new Player(new Player.PlayerInitial { Hp = 50, Mana = 500 }, true);
      Player boss = new Player(new Player.PlayerInitial { Hp = 55, DamageAmt = 8 });

      PlayAllGames(me, boss);
      returns = cheapestSpent.ToString();
      return returns;
    }

    public int cheapestSpent = int.MaxValue;
    public SortedList<int, int> spendings = new SortedList<int, int>();

    public void PlayAllGames(Player me, Player boss)
    {
      for (int i = 0; i < me.Spells.Count; i++)
      {
        // Check if the spell is already active
        bool spellMatch = me.ActiveSpells.Any(activeSpell => activeSpell.RemainingDuration > 1 && i == me.Spells.IndexOf(activeSpell));

        // Skip if the spell is already active or if there's not enough mana to cast it
        if (spellMatch || me.Spells[i].Cost > me.Mana)
        {
          continue;
        }

        Player newMe = me.Duplicate();
        Player newBoss = boss.Duplicate();

        // PART2 Change <BEGIN>
        newMe.Hp--;
        // PART2 Change <END>
        newMe.TakeTurn(newBoss);
        newBoss.TakeTurn(newMe);
        newMe.Attack(newBoss, i);

        newMe.TakeTurn(newBoss);
        newBoss.TakeTurn(newMe);
        newBoss.Attack(newMe, 0); // Assuming boss always uses a default attack

        if (newBoss.Hp <= 0)
        {
          cheapestSpent = Math.Min(cheapestSpent, newMe.Spent);
          if (!spendings.ContainsKey(newMe.Spent)) spendings.Add(newMe.Spent, newMe.Spent);
          return;
        }

        if (newMe.Hp > 0 && newBoss.Hp > 0 && newMe.Spent < cheapestSpent)
        {
          PlayAllGames(newMe, newBoss);
        }
      }
    }

    public class Player
    {
      public List<int> History { get; private set; }
      public PlayerInitial Initial { get; private set; }
      public bool IsWizard { get; private set; }
      public List<Spell> Spells { get; private set; }
      public int Hp { get; set; }
      public int Mana { get; set; }
      public int Armor { get; set; }
      public int Spent { get; set; }
      public int Turn { get; set; }
      public List<Spell> ActiveSpells { get; set; }
      public int DamageAmt { get; set; }

      public Player(PlayerInitial initial, bool isWizard = false)
      {
        History = new List<int>();
        Initial = initial;
        IsWizard = isWizard;
        ActiveSpells = new List<Spell>();

        if (IsWizard)
        {
          Spells = new List<Spell>
          {
            new Spell(53, (m, o) => o.Damage(4)),
            new Spell(73, (m, o) =>
            {
              o.Damage(2);
              m.Hp += 2;
            }),
            new Spell(113, null,
              start: (m, o) => m.Armor += 7,
              end: (m, o) => m.Armor -= 7,
              duration: 6),
            new Spell(173, (m, o) => o.Damage(3), duration: 6),
            new Spell(229, (m, o) => m.Mana += 101, duration: 5)
          };
        }

        Start();
      }

      public void Attack(Player opponent, int spellIdx)
      {
        if (!IsWizard)
        {
          opponent.Damage(DamageAmt);
        }
        else
        {
          Spell spell = Spells[spellIdx];
          History.Add(spellIdx);
          Spent += spell.Cost;
          Mana -= spell.Cost;

          if (spell.Duration > 0)
          {
            Spell newSpell = new Spell(spell.Cost, spell.Effect, spell.Start, spell.End, spell.Duration,
              spell.Duration);
            spell.Start?.Invoke(this, opponent);
            ActiveSpells.Add(newSpell);
          }
          else
          {
            spell.Effect?.Invoke(this, opponent);
          }
        }
      }


      public void Damage(int n)
      {
        Hp -= Math.Max(1, n - Armor);
      }

      public Player Duplicate()
      {
        var newPlayer = new Player(Initial, IsWizard)
        {
          Hp = Hp,
          Spent = Spent,
          Armor = Armor,
          Turn = Turn,
          ActiveSpells = ActiveSpells.Select(a => new Spell(a)).ToList(),
          History = new List<int>(History)
        };

        if (IsWizard)
        {
          newPlayer.Mana = Mana;
        }
        else
        {
          newPlayer.DamageAmt = DamageAmt;
        }

        return newPlayer;
      }

      public void TakeTurn(Player opponent)
      {
        Turn++;

        foreach (var spell in ActiveSpells.ToList()) // ToList() is used to create a copy for safe iteration
        {
          if (spell.RemainingDuration > 0)
          {
            spell.Effect?.Invoke(this, opponent);
            spell.RemainingDuration--;

            if (spell.RemainingDuration == 0 && spell.End != null)
            {
              spell.End?.Invoke(this, opponent);
            }
          }
        }

        ActiveSpells.RemoveAll(s => s.RemainingDuration == 0);
      }

      public void Start()
      {
        Hp = Initial.Hp;
        Spent = 0;
        Armor = 0;
        Turn = 0;
        ActiveSpells = new List<Spell>();
        if (IsWizard)
        {
          Mana = Initial.Mana;
        }
        else
        {
          DamageAmt = Initial.DamageAmt;
        }
      }

      public class Spell
      {
        public int Cost { get; set; }
        public Action<Player, Player> Effect { get; set; }
        public Action<Player, Player> Start { get; set; }
        public Action<Player, Player> End { get; set; }
        public int Duration { get; set; }

        // Additional properties for active spells
        public int RemainingDuration { get; set; }

        public Spell(int cost,
          Action<Player, Player> effect,
          Action<Player, Player> start = null,
          Action<Player, Player> end = null,
          int duration = 0,
          int remainingDuration = 0)
        {
          Cost = cost;
          Effect = effect;
          Start = start;
          End = end;
          Duration = duration;
          RemainingDuration = remainingDuration;
        }

        public Spell(Spell s) : this(s.Cost, s.Effect, s.Start, s.End, s.Duration, s.RemainingDuration)
        {
        }
      }

      public struct PlayerInitial
      {
        public int Hp;
        public int Mana;
        public int DamageAmt;
      }
    }

    /*
    public override string Execute()
    {
      string result = "";

      List<int> moneySpentForWinning = new List<int>();

      List<Spell> spells = new List<Spell>()
      {
        new Spell() { Name = "Magic Missile", IsEffect = false, CostMana = 53, Damage = 4, Armor = 0, ManaRecharge = 0, Healing = 0, LastsRounds = 0},
        new Spell() { Name = "Drain", IsEffect = false, CostMana = 73, Damage = 2, Armor = 0, ManaRecharge = 0, Healing = 2, LastsRounds = 0 },
        new Spell() { Name = "Shield", IsEffect = true, CostMana = 113, Damage = 0, Armor = 7, ManaRecharge = 0, Healing = 0, LastsRounds = 6 },
        new Spell() { Name = "Poison", IsEffect = true, CostMana = 173, Damage = 3, Armor = 0, ManaRecharge = 0, Healing = 0, LastsRounds = 6 },
        new Spell() { Name = "Recharge", IsEffect = true, CostMana = 229, Damage = 0, Armor = 0, ManaRecharge = 101, Healing = 0, LastsRounds = 5 }
      };

      #region GameLoop

      var player = new Fighter()
      {
        HitPoints = 50,
        Damage = 0,
        Armor = 0,
        Mana = 500,
        ManaSpent = 0
      };

      var boss = new Fighter()
      {
        HitPoints = 55,
        Damage = 8,
        Armor = 0,
      };

      int moneySpent = 0;
      // buy stuff !! see equiptment

      bool isGameOver = false;
      do // fight !!
      {
        player.castsSpells(boss);
        if (boss.HitPoints <= 0) break;   // boss looses

        boss.attacks(player);
        if (player.HitPoints <= 0) break; // player looses
      } while (!isGameOver);

      // Did the player win?
      if (player.HitPoints > 0 && boss.HitPoints <= 0)
      {
        // save the amount of money !!
        moneySpentForWinning.Add(moneySpent);
      }

      #endregion GameLoop

      var leastAmountOfMoney =
        (from m in moneySpentForWinning select m).Min();

      result = leastAmountOfMoney.ToString();
      return result;
    }

    private class Fighter
    {
      public int HitPoints { get; set; }
      public int Damage { get; set; }
      public int Armor { get; set; }
      public int Mana { get; set; }
      public int ManaSpent { get; set; }
      public List<Spell> Spells { get; set; }

      public Fighter()
      {
        Spells = new List<Spell>();
      }

      public void attacks(Fighter player)
      {
        // player = defender
        int loss = Math.Max(this.Damage - player.Armor, 1);
        player.HitPoints -= loss;
      }

      public void castsSpells(Fighter boss)
      {
        for (int i = 0; i < this.Spells.Count; i++)
        {
          if (this.Spells[i].IsEffect)
          {

          }
          else
          {

          }
        }
      }
    }

    private class Spell
    {
      public string Name { get; set; }
      public bool IsEffect { get; set; }
      public int CostMana { get; set; }
      public int Damage { get; set; }
      public int Armor { get; set; }
      public int ManaRecharge { get; set; }
      public int Healing { get; set; }
      public int LastsRounds { get; set; }
    }
    */
  }
}
