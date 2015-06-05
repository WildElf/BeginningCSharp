using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DieClassExample
{
    /// <summary>
    /// Demonstrates the implementation of a Die class.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Tests the die class.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            // test standard die
            Die standardDie = new Die();
            Console.WriteLine("6-Sided Die");
            Console.WriteLine("Number of sides: " + standardDie.NumSides);
            Console.WriteLine("Top side: " + standardDie.TopSide);
            Console.WriteLine();

            // roll and print the results
            Console.WriteLine("Rolling " + standardDie.NumSides + "-sided die...");
            standardDie.Roll();
            Console.WriteLine("Top side: " + standardDie.TopSide);
            Console.WriteLine();

            // test D20
            Die d20Die = new Die(20);
            Console.WriteLine("20-Sided Die");
            Console.WriteLine("Number of sides: " + d20Die.NumSides);
            Console.WriteLine("Top side: " + d20Die.TopSide);
            Console.WriteLine();

            // roll the d20
            Console.WriteLine("Rolling " + d20Die.NumSides + "-sided die...");
            d20Die.Roll();
            Console.WriteLine("Top side: " + d20Die.TopSide);
            d20Die.Roll();
            Console.WriteLine("Top side: " + d20Die.TopSide);
            d20Die.Roll();
            Console.WriteLine("Top side: " + d20Die.TopSide);
            Console.WriteLine();
        }
    }
}
