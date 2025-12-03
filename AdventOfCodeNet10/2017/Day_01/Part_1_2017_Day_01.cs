using System.Diagnostics;

namespace AdventOfCodeNet10._2017.Day_01
{
  // 1. Define an alias for the tuple type at the top of your file (or globally if using C# 12's global using)

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

