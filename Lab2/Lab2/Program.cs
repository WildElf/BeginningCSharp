using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab2
{
    class Program
    {
        static void Main(string[] args)
        {
            int age;
            age = 40;

            Console.WriteLine(age);
            Console.WriteLine();

            const int MAX_SCORE = 100;
            int score = 60;
            float percent;
            percent = (float)score*100 / (float)MAX_SCORE;

            Console.WriteLine(percent + "%");
            Console.WriteLine();
        }
    }
}
