using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleCards;

namespace Lab12
{
    class Program
    {
        static void Main(string[] args)
        {
            Deck deck = new Deck();

            Card[] hand = new Card[5];

            deck.Shuffle();

            deck.Print();

            Console.WriteLine();

            hand[0] = deck.TakeTopCard();

            hand[0].FlipOver();

            hand[0].Print();

            hand[1] = deck.TakeTopCard();

            hand[1].FlipOver();

            hand[1].Print();

            Console.WriteLine();
        }
    }
}
