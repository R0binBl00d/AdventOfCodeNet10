using System.Diagnostics;

namespace AdventOfCodeNet10._2017.Day_01
{
  // 1. Define an alias for the tuple type at the top of your file (or globally if using C# 12's global using)
  using Card = (CardRank Rank, CardSuit Suit, int Number, System.Collections.Generic.List<int> DemoCollection);

  public enum CardSuit
  {
    Hearts,
    Diamonds,
    Clubs,
    Spades
  }

  public enum CardRank
  {
    Two,
    Three,
    Four,
    Five,
    Six,
    Seven,
    Eight,
    Nine,
    Ten,
    Jack,
    Queen,
    King,
    Ace
  }


  #region Alternative Definitions
  // alternative
  //public readonly record struct Card(string Rank, int Number, string Color, System.Collections.Generic.List<int> CurrentHand);
  // 2nd alternative - completely OldSchool
  //public class Card
  //{
  //  public string Rank { get; }
  //  public int Number { get; }
  //  public string Color { get; }
  //  public System.Collections.Generic.List<int> CurrentHand { get; }
  //  public Card(string rank, int number, string color, System.Collections.Generic.List<int> currentHand)
  //  {
  //    Rank = rank;
  //    Number = number;
  //    Color = color;
  //    CurrentHand = currentHand;
  //  }
  //}
  #endregion Alternative Definitions

  #region Define the extension container for the underlying type (C# 10-13) // no Properties possible !!
  //public static class CardExtension
  //{
  //  // The C# compiler sees 'Card' as '(string Rank, int Number, string Color, List<int> CurrentHand)'

  //  public static bool IsFaceCard(this Card card)
  //  {
  //    return (card.Rank is "King" or "Queen" or "Jack");
  //  }

  //  public static int GetNumberValue(this Card card)
  //  {
  //    return CardExtension.IsFaceCard(card) ? 10 : card.Number;
  //  }
  //}
  #endregion

  // Define the extension container for the underlying type (C# 14)
  internal static class MyExtTestClass // class can have any name
  {
    extension(Card card)
    {
      //internal bool IsFaceCard => card.Rank is "King" or "Queen" or "Jack";
      internal bool IsFaceCard => GetAllFaces().Contains(card.Rank);
      internal bool IsRed => card.Suit is CardSuit.Hearts or CardSuit.Diamonds;
      internal bool IsBlack => card.Suit is CardSuit.Clubs or CardSuit.Spades;

      internal int GetNumberValue()
      {
        return card.IsFaceCard ? 10 : card.Number;
      }

      internal static CardRank[] GetAllFaces() // static method inside extension container
      {
        return [CardRank.King, CardRank.Queen, CardRank.Jack];
      }

      internal string SelfmadeLowerCase => card.Suit.ToString().ToLower();
    }

    extension(String str)
    {
      internal string SelfmadeLowerCase => str.ToLower();// just to show that we can create property-extensions for other types too

      internal string SelfmadeUpperCaseRank(Card myCard) // just to show that we can pass parameters too
      {
        return myCard.IsFaceCard                         // just to show that we can use other extensions inside this extension !!
          ? myCard.Rank.ToString().ToUpper()
          : myCard.Rank.ToString(); 
      }
    }

    extension(List<Card> cards)
    {
      internal static List<Card> Generate32CardDeck()
      {
        var fullDeck = new List<Card>();
        foreach (CardSuit suit in Enum.GetValues(typeof(CardSuit)))
          foreach (var rank in
                   Enum.GetValues(typeof(CardRank)).Cast<CardRank>().Skip(5)) // Skip first 5 ranks (Two to Six)
          {
            var number = rank switch
            {
              CardRank.Seven => 7,
              CardRank.Eight => 8,
              CardRank.Nine => 9,
              CardRank.Ten => 10,
              CardRank.Jack => 10,
              CardRank.Queen => 10,
              CardRank.King => 10,
              CardRank.Ace => 11,
              _ => throw new InvalidOperationException("Invalid card rank")
            };
            fullDeck.Add((rank, suit, number, []));
          }

        return fullDeck;
      }

      internal static List<Card> Generate52CardDeck()
      {
        var fullDeck = new List<Card>();
        foreach (CardSuit suit in Enum.GetValues(typeof(CardSuit)))
          foreach (var rank in Enum.GetValues(typeof(CardRank)).Cast<CardRank>())
          {
            var number = rank switch
            {
              CardRank.Two => 2,
              CardRank.Three => 3,
              CardRank.Four => 4,
              CardRank.Five => 5,
              CardRank.Six => 6,
              CardRank.Seven => 7,
              CardRank.Eight => 8,
              CardRank.Nine => 9,
              CardRank.Ten => 10,
              CardRank.Jack => 10,
              CardRank.Queen => 10,
              CardRank.King => 10,
              CardRank.Ace => 11,
              _ => throw new InvalidOperationException("Invalid card rank")
            };
            fullDeck.Add((rank, suit, number, []));
          }

        return fullDeck;
      }

      internal void ShuffleCards()
      {
        var random = new Random();
        int n = cards.Count;
        for (int i = n - 1; i > 0; i--)
        {
          int j = random.Next(i + 1);
          // Swap cards[i] with cards[j]
          (cards[i], cards[j]) = (cards[j], cards[i]);
        }
      }

      internal Card DealCard()
      {
        if (cards.Count == 0) throw new InvalidOperationException("No cards left in the deck to deal.");

        var dealtCard = cards[0];
        cards.RemoveAt(0);
        return dealtCard;
      }
    }
  }

  internal class Part_1_2017_Day_01 : Days
  {
    public override string Execute()
    {
      // Use the alias as a type
      var deck = new List<Card>
      {
        (CardRank.Ace, CardSuit.Hearts, 11, [1, 2]),
        (CardRank.Two, CardSuit.Diamonds, 2, [3, 4, 5, 6, 7]),
        (CardRank.Ten, CardSuit.Clubs, 10, [8, 9])
      };


      deck.Sort((card1, card2) => card1.Number.CompareTo(card2.Number));
      deck.Add(new Card(CardRank.Queen, CardSuit.Spades, 10, [10, 11]));

      // Intentionally create full decks to show the extension methods
      deck = List<Card>.Generate32CardDeck(); // Cards seven until Ace
      deck.ShuffleCards(); // List<Card>Extension Function

      deck = List<Card>.Generate52CardDeck(); // Cards two until Ace
      deck.ShuffleCards(); // List<Card>Extension Function

      Card dealtCard = default;

      for (int l = 0; l < 5; l++)
      {
        // Deal a card from the deck
        dealtCard = deck.DealCard(); // List<Card>Extension Function

        // Get number of cards remaining
        var remainingCount = deck.Count; // List<Card>Extension Property

        Debug.WriteLine($"Dealt Card: {dealtCard.Rank} of {dealtCard.Suit}, Remaining cards: {remainingCount}\n");

        // Use the extension properties directly
        Debug.WriteLine($"Is the dealt card a face card? {(dealtCard.IsFaceCard ? "Yes" : "No")}");
        Debug.WriteLine($"Is the dealt card red? {(dealtCard.IsRed ? "Yes" : "No")}");
      }

      // Use the extension method that returns a value
      Debug.WriteLine($"The number value of the dealt card is: {dealtCard.GetNumberValue()}");

      // first red card
      int indexR = deck.FindIndex(card => card.IsRed);  // StringExtension Property
      Debug.WriteLine($"First red card Has a Rank: {deck[indexR].Rank}");
      Debug.WriteLine(deck[indexR].ToString());

      // first black card
      int indexB = deck.FindIndex(card => card.IsBlack); // CardExtension Property
      Debug.WriteLine($"First black card Has a Rank: {"".SelfmadeUpperCaseRank(deck[indexB])}"); // StringExtension Funktion with Parameter
      Debug.WriteLine(deck[indexB].ToString());

      // SelfmadeLowerCase Extension Static Funktion is disambiguated with CardExtension Static Funktion and StringExtension Static Funktion
      var strigLower = MyExtTestClass.get_SelfmadeLowerCase("bLuE");  // StringExtension Static Funktion
      var cardColorL = MyExtTestClass.get_SelfmadeLowerCase(deck[0]); // CardExtension Static Funktion
      Debug.WriteLine($"{strigLower}, {cardColorL}");

      foreach (var face in Card.GetAllFaces()) // CardExtension Static Funktion
      {
        Debug.WriteLine($"Valid Face: {face}");
      }

      // Check our deck for face cards
      Debug.WriteLine($"\nRemaining cards: {deck.Count}\n");
      foreach (var card in deck)
      {
        string rank = MyExtTestClass.SelfmadeUpperCaseRank(String.Empty, card); // static StringExtension Funktion with Parameters
        Debug.WriteLine($"Rank: {rank}, Color: {card.Suit}, IsFace: {(card.IsFaceCard ? "Yes" : "No")}"); // CardExtension Property
      }

      return deck.Sum(card => card.GetNumberValue()).ToString(); // CardExtension Funktion
    }
  }
}

