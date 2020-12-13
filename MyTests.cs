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
            //Wireset tests_________________________________________________________________________________::

            //Wireset.SetValue(int x) test:
            WireSet wire1 = new WireSet(5);
            Console.Write("Enter a num between 0 and 15 to convert into binary: ");
            int num = Convert.ToInt32(Console.ReadLine());
            wire1.SetValue(num);
            Console.WriteLine("Your number binary representation: " + wire1.ToString());
            Console.WriteLine("press any key to exit...");
            Console.ReadKey();
        }
        
    }
}
