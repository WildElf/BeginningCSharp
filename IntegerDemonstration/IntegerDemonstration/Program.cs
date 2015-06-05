using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    class Program
    {
        // Integer demonstration
        static void Main(string[] args)
        {
            int totalSecondsPlayed = 100;
            const int SECONDS_PER_MINUTE = 60;

            // Calculations
            int minutes = totalSecondsPlayed / SECONDS_PER_MINUTE;
            int seconds = totalSecondsPlayed % SECONDS_PER_MINUTE;

            // Print results

            Console.WriteLine("Time Played: " + minutes + ":" + seconds);

            Console.WriteLine();
        }
    }
}
