using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab9
{
    class Program
    {
        static void Main(string[] args)
        {
            // display menu            
            Menu();
            Console.Write("Select Menu (1, 2, 3, 4): ");
            char selection = Console.ReadLine()[0];

            // check for proper selection
            if (selection == '1' || selection == '2' || selection == '3' || selection == '4')
            {
                Console.WriteLine("You selected " + selection);
                // display choice

                switch (selection)
                {
                    case '1':
                        Console.WriteLine("New Game selected.");
                        break;

                    case '2':
                        Console.WriteLine("Load Game selected.");
                        break;

                    case '3':
                        Console.WriteLine("Options selected.");
                        break;

                    case '4':
                        Console.WriteLine("Quit selected.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Invalid selection made.");
            }

        }

        static void Menu()
        {
            Console.WriteLine("********************");
            Console.WriteLine("Menu:");
            Console.WriteLine();
            Console.WriteLine("1 - New Game");
            Console.WriteLine("2 - Load Game");
            Console.WriteLine("3 - Options");
            Console.WriteLine("4 - Quit");
            Console.WriteLine("********************");

        }
    }
}
