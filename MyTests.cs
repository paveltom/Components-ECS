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
            //Console.WriteLine("============================================================================");
            //Console.WriteLine("Wireset tests:");
            //Console.WriteLine();
            //Console.WriteLine("Wireset.SetValue(int x) test:");
            //Console.Write("Enter the num of bits: ");
            //int numOfBits = Convert.ToInt32(Console.ReadLine());
            //WireSet wire1 = new WireSet(numOfBits);
            //Console.Write("Enter a num between 0 and " + (int)Math.Pow(2, numOfBits) + " to convert into binary: ");
            //int userNum = Convert.ToInt32(Console.ReadLine());
            //wire1.SetValue(userNum);
            //Console.WriteLine("Your number binary representation: " + wire1.ToString());
            //Console.WriteLine();

            //Console.WriteLine("Wireset.GetValue() test:");
            //Console.WriteLine("Your number decimal representation (expected value: " + userNum + "): " + wire1.GetValue());
            //Console.WriteLine();


            Console.WriteLine("Wireset.Set2sComplement() test:");
            Console.Write("Enter the num of bits: ");
            int numOfBits2 = Convert.ToInt32(Console.ReadLine());
            WireSet wire2 = new WireSet(numOfBits2);
            Console.Write("Enter a num between -" + (int)Math.Pow(2, numOfBits2 - 1) + " and " + ((int)Math.Pow(2, numOfBits2 - 1) - 1) + " to convert into binary: ");
            string input = Console.ReadLine();
            int userNum2 = 1;
            if (input[0] == '-')
            {
                input = input.TrimStart('-');
                userNum2 = -1;
            }        
            userNum2 = userNum2 * (Convert.ToInt32(input));
            wire2.Set2sComplement(userNum2);
            Console.WriteLine("Your number binary representation: " + wire2.ToString());
            Console.WriteLine();

            Console.WriteLine("Wireset.Get2sComplement() test:");
            Console.WriteLine("Your number decimal representation (expected value: " + userNum2 + "): " + wire2.Get2sComplement());
            Console.WriteLine("============================================================================");
            Console.WriteLine();






            //exit point if needed:
            //Console.WriteLine("press any key to exit...");
            //Console.ReadKey();
        }

    }
}
