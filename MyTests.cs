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


            //Console.WriteLine("Wireset.Set2sComplement() test:");
            //Console.Write("Enter the num of bits: ");
            //int numOfBits2 = Convert.ToInt32(Console.ReadLine());
            //WireSet wire2 = new WireSet(numOfBits2);
            //Console.Write("Enter a num between -" + (int)Math.Pow(2, numOfBits2 - 1) + " and " + ((int)Math.Pow(2, numOfBits2 - 1) - 1) + " to convert into binary: ");
            //string input = Console.ReadLine();
            //int userNum2 = 1;
            //if (input[0] == '-')
            //{
            //    input = input.TrimStart('-');
            //    userNum2 = -1;
            //}        
            //userNum2 = userNum2 * (Convert.ToInt32(input));
            //wire2.Set2sComplement(userNum2);
            //Console.WriteLine("Your number binary representation: " + wire2.ToString());
            //Console.WriteLine();

            //Console.WriteLine("Wireset.Get2sComplement() test:");
            //Console.WriteLine("Your number decimal representation (expected value: " + userNum2 + "): " + wire2.Get2sComplement());
            //Console.WriteLine("============================================================================");
            //Console.WriteLine();
            //Console.WriteLine("============================================================================");
            //Console.WriteLine();
            //Console.WriteLine("HalfAdder tests:");
            //HalfAdder ha = new HalfAdder();
            //Wire in1 = new Wire();
            //Wire in2 = new Wire();
            //in1.Value = 0;
            //in2.Value = 0;
            //ha.Input1.ConnectInput(in1);
            //ha.Input2.ConnectInput(in2);
            //Console.WriteLine(ha.ToString());
            //ha = new HalfAdder();
            //in1.Value = 1;
            //ha.Input1.ConnectInput(in1);
            //ha.Input2.ConnectInput(in2);
            //Console.WriteLine(ha.ToString());
            //ha = new HalfAdder();
            //in2.Value = 1;
            //ha.Input1.ConnectInput(in1);
            //ha.Input2.ConnectInput(in2);
            //Console.WriteLine(ha.ToString());
            //ha = new HalfAdder();
            //in1.Value = 0;
            //ha.Input1.ConnectInput(in1);
            //ha.Input2.ConnectInput(in2);
            //Console.WriteLine(ha.ToString());
            //Console.WriteLine();
            //Console.WriteLine("Inner test result: " + ha.TestGate());
            //Console.WriteLine("============================================================================");
            //Console.WriteLine();
            //Console.WriteLine("============================================================================");
            //Console.WriteLine();
            //Console.WriteLine("FullAdder tests:");
            //FullAdder fa = new FullAdder();
            //Console.WriteLine("Inner test result: " + fa.TestGate());
            //Console.WriteLine("============================================================================");
            //Console.WriteLine();
            //Console.WriteLine("MultiBitAdder tests:");
            //bool loop = true;
            //while (loop)
            //{
            //    Console.Write("Enter the num of bits (hit 'e' to exit): ");
            //    string strIn = Console.ReadLine();
            //    if (strIn == "e")
            //        loop = false;
            //    else
            //    {
            //        int numOfBits4 = Convert.ToInt32(strIn);
            //        Console.Write("Enter first num between -" + (int)Math.Pow(2, numOfBits4 - 1) + " and " + ((int)Math.Pow(2, numOfBits4 - 1) - 1) + " to compute: ");
            //        int userNum41 = Convert.ToInt32(Console.ReadLine());
            //        Console.Write("Enter second num between -" + (int)Math.Pow(2, numOfBits4 - 1) + " and " + ((int)Math.Pow(2, numOfBits4 - 1) - 1) + " to compute: ");
            //        int userNum42 = Convert.ToInt32(Console.ReadLine());


            //        MultiBitAdder mba = new MultiBitAdder(numOfBits4);
            //        WireSet in1 = new WireSet(numOfBits4);
            //        WireSet in2 = new WireSet(numOfBits4);
            //        in1.Set2sComplement(userNum41);
            //        in2.Set2sComplement(userNum42);

            //        mba.ConnectInput1(in1);
            //        mba.ConnectInput2(in2);
            //        Console.WriteLine("Computation: (" + mba.Input1.Get2sComplement() + ") + (" + mba.Input2.Get2sComplement() + ") = (" + mba.Output.Get2sComplement() + ")");

            //        Console.WriteLine(mba);
            //    }
            //}

            //Console.WriteLine("============================================================================");
            //Console.WriteLine();
            //Console.WriteLine("ALU tests:");
            //bool loop = true;
            //while (loop)
            //{
            //    Console.Write("Enter the num of bits ('e' + 'enter' to exit the test loop): ");
            //    string strIn = Console.ReadLine();
            //    if (strIn == "e")
            //        loop = false;
            //    else
            //    {
            //        int numOfBits5 = Convert.ToInt32(strIn);
            //        Console.Write("Enter X input between -" + (int)Math.Pow(2, numOfBits5 - 1) + " and " + ((int)Math.Pow(2, numOfBits5 - 1) - 1) + " to compute: ");
            //        int userNum51 = Convert.ToInt32(Console.ReadLine());
            //        Console.Write("Enter Y input between -" + (int)Math.Pow(2, numOfBits5 - 1) + " and " + ((int)Math.Pow(2, numOfBits5 - 1) - 1) + " to compute: ");
            //        int userNum52 = Convert.ToInt32(Console.ReadLine());


            //        WireSet in1 = new WireSet(numOfBits5);
            //        WireSet in2 = new WireSet(numOfBits5);
            //        in1.Set2sComplement(userNum51);
            //        in2.Set2sComplement(userNum52);

            //        Console.WriteLine();
            //        Console.WriteLine("your binary num1: " + in1);
            //        Console.WriteLine("your binary num2: " + in2);
            //        Console.WriteLine();

            //        ALU alu = new ALU(numOfBits5);
            //        //setting ALU properties:

            //        alu.InputX.ConnectInput(in1); 
            //        alu.InputY.ConnectInput(in2);
            //        Wire userWire1 = new Wire();
            //        Wire userWire2 = new Wire();
            //        Wire userWire3 = new Wire();
            //        Wire userWire4 = new Wire();
            //        Wire userWire5 = new Wire();
            //        Wire userWire6 = new Wire();
            //        Wire userWire7 = new Wire();


            //        Console.Write("Enter ZX value: ");
            //        userWire1.Value = Convert.ToInt32(Console.ReadLine());
            //        alu.ZeroX.ConnectInput(userWire1);

            //        Console.Write("Enter NX value: ");
            //        userWire2.Value = Convert.ToInt32(Console.ReadLine());
            //        alu.NotX.ConnectInput(userWire2);

            //        Console.Write("Enter ZY value: ");
            //        userWire3.Value = Convert.ToInt32(Console.ReadLine());
            //        alu.ZeroY.ConnectInput(userWire3);

            //        Console.Write("Enter NY value: ");
            //        userWire4.Value = Convert.ToInt32(Console.ReadLine());
            //        alu.NotY.ConnectInput(userWire4);

            //        Console.Write("Enter F value: ");
            //        userWire5.Value = Convert.ToInt32(Console.ReadLine());
            //        alu.F.ConnectInput(userWire5);

            //        Console.Write("Enter N value: ");
            //        userWire6.Value = Convert.ToInt32(Console.ReadLine());
            //        alu.NotOutput.ConnectInput(userWire6);

            //        Console.WriteLine();
            //        Console.Write("Data: ");
            //        Console.WriteLine(" ZX = " + alu.ZeroX + ";  NX = " + alu.NotX + ";  ZY = " + alu.ZeroY + ";  NY = " + alu.NotY + ";  F = " + alu.F + ";  N = " + alu.NotOutput + ".");
            //        Console.WriteLine();
            //        Console.WriteLine("output is Zero (no = 0/yes = 1): " + alu.Zero);
            //        Console.WriteLine("output is Negative (no = 0/yes = 1): " + alu.Negative);
            //        Console.WriteLine("output binary: " + alu.Output);
            //        Console.WriteLine("output decimal: " + alu.Output.Get2sComplement());
            //        Console.WriteLine("============================================================================");

            //    }
            //}


            SingleBitRegister sbr = new SingleBitRegister();
            Console.WriteLine("single bit register testGate: " + sbr.TestGate());










            //exit point if needed:
            //Console.WriteLine("press any key to exit...");
            Console.ReadKey();
        }

    }
}
