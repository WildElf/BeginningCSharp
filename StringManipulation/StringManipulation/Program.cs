using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            // read in csv string
            Console.Write("Enter name and percent (name,percent): ");
            string csvString = Console.ReadLine();

            // find comma location

            int commaLocation = csvString.IndexOf(',');

            // extract name and percent

            string name = csvString.Substring(0, commaLocation);
            float percent = float.Parse(csvString.Substring(commaLocation + 1));

            // print name and percent
            Console.WriteLine("Name: " + name);
            Console.WriteLine("Percent: " + percent + "%");

            Console.WriteLine();
         }
    }
}
