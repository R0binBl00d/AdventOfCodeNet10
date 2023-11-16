using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace AdventOfCodeNet8._2015.Day_22
{
  internal class Part_2 : Days
  {
    /// <summary>
    /*
    https://adventofcode.com/2015/day/1
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
