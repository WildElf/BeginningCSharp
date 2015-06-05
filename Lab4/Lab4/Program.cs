using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab4
{
	/// <summary>
	/// Demonstrates classes and objects
	/// </summary>
    class Program
    {
		/// <summary>
		/// Demonstrates use of Deck and Card objects
		/// </summary>
		/// <param name="args">command-line args</param>
        static void Main(string[] args)
        {
            // create a new deck and print the contents of the deck
            Deck deck = new Deck();
            deck.Print();

            Console.WriteLine();
            // shuffle the deck and print the contents of the deck
            deck.Shuffle();
            deck.Print();

            Console.WriteLine();
            // take the top card from the deck and print the card rank and suit
            Card topCard = deck.TakeTopCard();

            Console.WriteLine("Card 1: " + topCard.Rank + " of " + topCard.Suit);

            Console.WriteLine();
            // take the top card from the deck and print the card rank and suit
            topCard = deck.TakeTopCard();

            Console.WriteLine("Card 2: " + topCard.Rank + " of " + topCard.Suit);

            Console.WriteLine();
        }
    }
}
