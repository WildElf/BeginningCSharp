using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab8
{
    class Program
    {
        static void Main(string[] args)
        {
            // related to a pyramid, for some reason            
            Console.Write("Enter <slot number>,<block letter>,<true/false>: ");
            string pyramidString = Console.ReadLine();

            // extract pyramid slot
            int commaLocation = pyramidString.IndexOf(',');
            string slotEntered = pyramidString.Substring(0, commaLocation);

            Console.WriteLine("Pyramid slot: " + slotEntered);

            // Extract block letter
            string secondString = pyramidString.Substring(commaLocation + 1);
            Console.WriteLine("Block letter: " + secondString.Substring(0,1));

            // get whether to light up the block
            commaLocation = secondString.IndexOf(',');
            bool light = bool.Parse(secondString.Substring(commaLocation+1));
            Console.WriteLine("Light it? " + light);
            
        }
    }
}
