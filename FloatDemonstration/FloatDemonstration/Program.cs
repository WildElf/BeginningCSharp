using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    class Program
    {
        // Floating point demonstration
        static void Main(string[] args)
        {
            // declare variables
            int score = 1356;
            int totalSecondsPlayed = 10000;

            // Calculations
            float scorePerSeconds = (float)score / totalSecondsPlayed;

            // Write results
            Console.WriteLine("Points per second: " + scorePerSeconds);

            Console.WriteLine();
        }
    }
}
