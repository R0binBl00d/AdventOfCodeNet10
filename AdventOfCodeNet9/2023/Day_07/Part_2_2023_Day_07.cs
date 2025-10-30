namespace AdventOfCodeNet10._2023.Day_07
{
  internal class Part_2_2023_Day_07 : Days
  {
    /// <summary>
    /*
    https://adventofcode.com/2023/day/7
    --- Day 7: Camel Cards ---
    Your all-expenses-paid trip turns out to be a one-way, five-minute ride in an
    airship. (At least it's a cool airship!) It drops you off at the edge of a vast
    desert and descends back to Island Island.
    
    "Did you bring the parts?"
    
    You turn around to see an Elf completely covered in white clothing, wearing
    goggles, and riding a large camel.
    
    "Did you bring the parts?" she asks again, louder this time. You aren't sure
    what parts she's looking for; you're here to figure out why the sand stopped.
    
    "The parts! For the sand, yes! Come with me; I will show you." She beckons you
    onto the camel.
    
    After riding a bit across the sands of Desert Island, you can see what look
    like very large rocks covering half of the horizon. The Elf explains that the
    rocks are all along the part of Desert Island that is directly above Island
    Island, making it hard to even get there. Normally, they use big machines to
    move the rocks and filter the sand, but the machines have broken down because
    Desert Island recently stopped receiving the parts they need to fix the
    machines.
    
    You've already assumed it'll be your job to figure out why the parts stopped
    when she asks if you can help. You agree automatically.
    
    Because the journey will take a few days, she offers to teach you the game of
    Camel Cards. Camel Cards is sort of similar to poker except it's designed to be
    easier to play while riding a camel.
    
    In Camel Cards, you get a list of hands, and your goal is to order them based
    on the strength of each hand. A hand consists of five cards labeled one of A,
    K, Q, J, T, 9, 8, 7, 6, 5, 4, 3, or 2. The relative strength of each card
    follows this order, where A is the highest and 2 is the lowest.
    
    Every hand is exactly one type. From strongest to weakest, they are:
    
    Five of a kind, where all five cards have the same label: AAAAA
    Four of a kind, where four cards have the same label and one card has a
    different label: AA8AA
    Full house, where three cards have the same label, and the remaining two cards
    share a different label: 23332
    Three of a kind, where three cards have the same label, and the remaining two
    cards are each different from any other card in the hand: TTT98
    Two pair, where two cards share one label, two other cards share a second
    label, and the remaining card has a third label: 23432
    One pair, where two cards share one label, and the other three cards have a
    different label from the pair and each other: A23A4
    High card, where all cards' labels are distinct: 23456
    Hands are primarily ordered based on type; for example, every full house is
    stronger than any three of a kind.
    
    If two hands have the same type, a second ordering rule takes effect. Start by
    comparing the first card in each hand. If these cards are different, the hand
    with the stronger first card is considered stronger. If the first card in each
    hand have the same label, however, then move on to considering the second card
    in each hand. If they differ, the hand with the higher second card wins;
    otherwise, continue with the third card in each hand, then the fourth, then the
    fifth.
    
    So, 33332 and 2AAAA are both four of a kind hands, but 33332 is stronger
    because its first card is stronger. Similarly, 77888 and 77788 are both a full
    house, but 77888 is stronger because its third card is stronger (and both hands
    have the same first and second card).
    
    To play Camel Cards, you are given a list of hands and their corresponding bid
    (your puzzle input). For example:
    
    32T3K 765
    T55J5 684
    KK677 28
    KTJJT 220
    QQQJA 483
    This example shows five hands; each hand is followed by its bid amount. Each
    hand wins an amount equal to its bid multiplied by its rank, where the weakest
    hand gets rank 1, the second-weakest hand gets rank 2, and so on up to the
    strongest hand. Because there are five hands in this example, the strongest
    hand will have rank 5 and its bid will be multiplied by 5.
    
    So, the first step is to put the hands in order of strength:
    
    32T3K is the only one pair and the other hands are all a stronger type, so it
    gets rank 1.
    KK677 and KTJJT are both two pair. Their first cards both have the same label,
    but the second card of KK677 is stronger (K vs T), so KTJJT gets rank 2 and
    KK677 gets rank 3.
    T55J5 and QQQJA are both three of a kind. QQQJA has a stronger first card, so
    it gets rank 5 and T55J5 gets rank 4.
    Now, you can determine the total winnings of this set of hands by adding up the
    result of multiplying each hand's bid with its rank (765 * 1 + 220 * 2 + 28 * 3
    + 684 * 4 + 483 * 5). So the total winnings in this example are 6440.
    
    Find the rank of every hand in your set. What are the total winnings?
    
    Your puzzle answer was 248217452.
    
    --- Part Two ---
    To make things a little more interesting, the Elf introduces one additional
    rule. Now, J cards are jokers - wildcards that can act like whatever card would
    make the hand the strongest type possible.
    
    To balance this, J cards are now the weakest individual cards, weaker even than
    2. The other cards stay in the same order: A, K, Q, T, 9, 8, 7, 6, 5, 4, 3, 2,
    J.
    
    J cards can pretend to be whatever card is best for the purpose of determining
    hand type; for example, QJJQ2 is now considered four of a kind. However, for
    the purpose of breaking ties between two hands of the same type, J is always
    treated as J, not the card it's pretending to be: JKKK2 is weaker than QQQQ2
    because J is weaker than Q.
    
    Now, the above example goes very differently:
    
    32T3K 765
    T55J5 684
    KK677 28
    KTJJT 220
    QQQJA 483
    32T3K is still the only one pair; it doesn't contain any jokers, so its
    strength doesn't increase.
    KK677 is now the only two pair, making it the second-weakest hand.
    T55J5, KTJJT, and QQQJA are now all four of a kind! T55J5 gets rank 3, QQQJA
    gets rank 4, and KTJJT gets rank 5.
    With the new joker rule, the total winnings in this example are 5905.
    
    Using the new joker rule, find the rank of every hand in your set. What are the
    new total winnings?
    
    Your puzzle answer was 245576185.
    */
    /// </summary>
    /// <returns>
    /// 245693330 answer is too high
    /// 246046768 can't be !! the last one was already too high
    /// 245576185
    /// </returns>
    public override string Execute()
    {
      string result = "";

      var cards = new SortedDictionary<int, Hand>();

      foreach (var line in Lines)
      {
        var chunks = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        var hand = new Hand(chunks[0].Trim(), Int32.Parse(chunks[1]));

        if (cards.ContainsKey(hand.Score))
        {
          Debugger.Break();
        }
        else
        {
          if (hand.Cards.Contains('J'))
          {
            hand.CorrectForJokers();
          }
          cards.Add(hand.Score, hand);
        }
      }

      int totalWin = 0;

      int rank = 1;
      foreach (var game in cards)
      //  Parallel.ForEach(cards, (game) =>
      {
        game.Value.Rank = rank++;
        totalWin += game.Value.Rank * game.Value.Bid;
      }
      //  );

      result = totalWin.ToString();
      return result;
    }

    static Dictionary<char, int> cardScores = new Dictionary<char, int>
    {
        { 'A', 13 }, { 'K', 12 }, { 'Q', 11 }, { 'J', 1 }, { 'T', 10 },
        { '9', 9 }, { '8', 8 }, { '7', 7 }, { '6', 6 }, { '5', 5 },
        { '4', 4 }, { '3', 3 }, { '2', 2 }
    };

    private enum eHandType
    {
      Five_of_a_Kind,
      Quad,
      Full_House,
      Three_of_a_Kind,
      Two_Pairs,
      One_Pair,
      High_Card
    };

    private class Hand
    {
      public string Cards { get; private set; }
      public eHandType HandType { get; private set; }
      public int Score { get; private set; }
      public int Bid { get; private set; }
      public int Rank { get; set; }

      public Hand(string hand, int bid)
      {
        Cards = hand;
        Bid = bid;
        // HandType is done in CalcPokerHand 
        Score = CalcPokerHand(hand);

        // Rank .. tbd
      }

      private int CalcPokerHand(string hand)
      {
        var counts = hand.GroupBy(c => c)
                         .Select(group => new { Card = group.Key, Count = group.Count() })
                         .OrderByDescending(c => c.Count);

        int handScore = GetCardScore(hand);
        int score = 0;

        if (counts.Any(c => c.Count == 5))
        {
          HandType = eHandType.Five_of_a_Kind;
          score = handScore + cathegory * 6;
        }
        else if (counts.Any(c => c.Count == 4))
        {
          HandType = eHandType.Quad;
          score = handScore + cathegory * 5;
        }
        else if (counts.Any(c => c.Count == 3) && counts.Any(c => c.Count == 2))
        {
          HandType = eHandType.Full_House;
          score = handScore + cathegory * 4;
        }
        else if (counts.Any(c => c.Count == 3))
        {
          HandType = eHandType.Three_of_a_Kind;
          score = handScore + cathegory * 3;
        }
        else if (counts.Count(c => c.Count == 2) == 2)
        {
          HandType = eHandType.Two_Pairs;
          score = handScore + cathegory * 2;
        }
        else if (counts.Any(c => c.Count == 2))
        {
          HandType = eHandType.One_Pair;
          score = handScore + cathegory * 1;
        }
        else
        {
          HandType = eHandType.High_Card;
          score = handScore;
        }

        Debugger.Log(1, "card", String.Format("\nScore of '{0:X6}' Cards:'{1}' HandType '{2}'",
          score, hand,
          Enum.GetName(typeof(eHandType), HandType))
          );
        return score;
      }

      private int GetCardScore(string hand)
      {
        int score = 0;
        int pow = 0;
        for (int i = hand.Length - 1; i >= 0; i--)
        {
          score += cardScores[hand[i]] * (int)Math.Pow(16, pow++);
        }
        return score;
      }

      private static int cathegory = (int)Math.Pow(16, 5);

      internal void CorrectForJokers()
      {
        int jCount = Cards.Count(c => c == 'J');
        switch (jCount)
        {
          #region 1 J
          case 1:
            switch (HandType)
            {
              case eHandType.High_Card: // 1 J
                // get the highest one
                //int highcard = Cards.Max(c => cardScores[c]);
                //if(highcard > 1) forget about it ... it will always be another higher one ...
                this.HandType = eHandType.One_Pair;
                this.Score += cathegory;
                break;
              case eHandType.One_Pair:// 1 J
                this.HandType = eHandType.Three_of_a_Kind;
                this.Score += 2 * cathegory;
                break;
              case eHandType.Two_Pairs:// 1 J
                this.HandType = eHandType.Full_House;
                this.Score += 2 * cathegory;
                break;
              case eHandType.Three_of_a_Kind:// 1 J
                this.HandType = eHandType.Quad;
                this.Score += 2 * cathegory;
                break;
              //case eHandType.Full_House:// 1 J... not possible
              //  break;
              case eHandType.Quad:// 1 J
                this.HandType = eHandType.Five_of_a_Kind;
                this.Score += cathegory;
                break;
                //case eHandType.Five_of_a_Kind:// 1 J ... not possible
                //  break;
            }
            break;
          #endregion 1 J

          #region 2 J
          case 2:
            switch (HandType)
            {
              //case eHandType.High_Card:// 2 J ... not possible
              //  break;
              case eHandType.One_Pair:// 2 J ... has to be the jokers
                this.HandType = eHandType.Three_of_a_Kind;
                this.Score += 2 * cathegory;
                break;
              case eHandType.Two_Pairs:// 2 J ... has to be the jokers involved
                this.HandType = eHandType.Quad;
                this.Score += 3 * cathegory;
                break;
              //case eHandType.Three_of_a_Kind:// 2 J ... not possible
              //  break;
              case eHandType.Full_House:// 2 J 
                this.HandType = eHandType.Five_of_a_Kind;
                this.Score += 2 * cathegory;
                break;
                //case eHandType.Quad:// 2 J ... not possible
                //  break;
                //case eHandType.Five_of_a_Kind:// 2 J ... not possible
                //  break;
            }
            break;
          #endregion 2 J

          #region 3 J
          case 3:
            switch (HandType)
            {
              //case eHandType.High_Card:// 3 J ... not possible
              //  break;
              //case eHandType.One_Pair:// 3 J ... not possible
              //  break;
              //case eHandType.Two_Pairs:// 3 J ... not possible
              //  break;
              case eHandType.Three_of_a_Kind:// 3 J ... has to be the jokers
                this.HandType = eHandType.Quad;
                this.Score += 2 * cathegory;
                break;
              case eHandType.Full_House:// 3 J
                this.HandType = eHandType.Five_of_a_Kind;
                this.Score += 2 * cathegory;
                break;
                //case eHandType.Quad:// 3 J ... not possible
                //  break;
                //case eHandType.Five_of_a_Kind:// 3 J ... not possible
                //  break;
            }
            break;
          #endregion 3 J

          #region 4 J
          case 4:
            switch (HandType)
            {
              //case eHandType.High_Card:// 4 J... not possible
              //  break;
              //case eHandType.One_Pair:// 4 J... not possible
              //  break;
              //case eHandType.Two_Pairs:// 4 J... not possible
              //  break;
              //case eHandType.Three_of_a_Kind:// 4 J... not possible
              //  break;
              //case eHandType.Full_House:// 4 J ... not possible
              //  break;
              case eHandType.Quad:// 4 J ... has to be the jokers
                this.HandType = eHandType.Five_of_a_Kind;
                this.Score += cathegory;
                break;
                //case eHandType.Five_of_a_Kind:// 4 J ... not possible
                //  break;
            }
            break;
          #endregion 4 J

          #region 5 J
          case 5:
            switch (HandType)
            {
              //case eHandType.High_Card:// 5 J ... not possible
              //  break;
              //case eHandType.One_Pair:// 5 J ... not possible
              //  break;
              //case eHandType.Two_Pairs:// 5 J ... not possible
              //  break;
              //case eHandType.Three_of_a_Kind:// 5 J ... not possible
              //  break;
              //case eHandType.Full_House:// 5 J ... not possible
              //  break;
              //case eHandType.Quad:// 5 J ... not possible
              //  break;
              case eHandType.Five_of_a_Kind:// 5 J
                // stays same
                break;
            }
            break;
            #endregion 5 J
        }

        Debugger.Log(1, "card", String.Format(" -> corrected -> \nScore of '{0:X6}' Cards:'{1}' HandType '{2}'\n",
          this.Score, this.Cards, Enum.GetName(typeof(eHandType), HandType))
          );
      }
    }
  }
}
