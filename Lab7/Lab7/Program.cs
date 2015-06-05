using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab7
{
    class Program
    {
        static void Main(string[] args)
        {
            string month;
            string day;
            Console.WriteLine("Enter birth month: ");
            month = Console.ReadLine();

            Console.WriteLine("Enter birth day: ");
            day = Console.ReadLine();

            int dayBefore = Convert.ToInt32(day);

            dayBefore -= dayBefore;

            Console.WriteLine("You'll receive an email reminder on " + month + " " + dayBefore);

            Console.WriteLine();

        }
    }
}
