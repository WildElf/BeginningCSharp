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
            // ask for and get user answer
            Console.Write("Pick up the shiny thing? (y/n): ");
            char answer = Console.ReadLine()[0];

            // print message

            //switch (answer)
            //{
            //    case 'y':
            //    case 'Y':
            //        Console.WriteLine("You have the shiny object.");
            //        break;
            //    case 'n':
            //    case 'N':
            //        Console.WriteLine("You left the shiny object alone.");
            //        break;
            //    default:
            //        Console.WriteLine("You are tiresome.");
            //        break;
            //}

            if (answer == 'y' ||
                answer == 'Y')
            {
                Console.WriteLine("You have the shiny object.");
            }
            else if (answer == 'n' ||
                answer == 'N')
            {
                Console.WriteLine("You left the shiny object alone.");
            }
            else
            {
                Console.WriteLine("You are tiresome.");
            }

            Console.WriteLine();
        }
    }
}
