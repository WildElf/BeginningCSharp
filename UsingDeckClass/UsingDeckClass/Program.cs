using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UsingDeckClass
{
    class Program
    {
        static void Main(string[] args)
        {
            Deck deck = new Deck();

            // print deck empty information
            Console.WriteLine("Empty: " + deck.Empty);

            Console.WriteLine();

        }
    }
}
