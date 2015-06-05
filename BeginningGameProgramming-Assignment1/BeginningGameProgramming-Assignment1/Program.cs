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
            const float GRAVITY = 9.8f;
            string angle;
            double speed;

            // print welcome message
            Console.WriteLine("Enter in the angle and force of a cannon shot \n" + 
            "and get its arc height and distance");
            Console.WriteLine();

            // prompt user for angle and force
            Console.WriteLine("Enter angle: ");
            angle = Console.ReadLine();

            double doubleTheta = double.Parse(angle) * Math.PI / 180;
            double theta = Convert.ToSingle(doubleTheta);

            Console.WriteLine("Enter speed (m/s): ");
            speed = float.Parse(Console.ReadLine());

            // Calculate x velocity (vox) and y velocity (voy)
            double vox = speed * Math.Cos(theta);

            double voy = speed * Math.Sin(theta);

            // Calculate time until apex

            double apexTime = voy / GRAVITY;

            // Calculate Heigh at apex

            double apexHeight = voy * voy / (2 * GRAVITY);

            // Calculate distance traveled

            double distanceX = vox * 2 * apexTime;

            // Print results

            Console.WriteLine("Shot height is " + apexHeight + " meters.");
            Console.WriteLine("Shot distance is " + distanceX + " meters.");

        }
    }
}
