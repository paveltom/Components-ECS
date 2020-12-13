using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    class MyTests
    {
        static void Main(string[] args)
        {
            Console.WriteLine("============================================================================");
            Console.WriteLine("Wireset tests:");
            Console.WriteLine();
            Console.WriteLine("Wireset.SetValue(int x) test:");
            WireSet wire1 = new WireSet(8);
            Console.Write("Enter a num between 0 and 15 to convert into binary: ");
            int userNum = Convert.ToInt32(Console.ReadLine());
            wire1.SetValue(userNum);
            Console.WriteLine("Your number binary representation: " + wire1.ToString());
            Console.WriteLine();
            Console.WriteLine("Wireset.GetValue() test:");
            Console.WriteLine("Your number decimal representation (expected value: " + userNum + "): " + wire1.GetValue());
            Console.WriteLine("============================================================================");
            Console.WriteLine();







            //exit point if needed:
            //Console.WriteLine("press any key to exit...");
            //Console.ReadKey();
        }
        
    }
}
