using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //This class implements an adder, receving as input two n bit numbers, and outputing the sum of the two numbers
    class MultiBitAdder : Gate
    {
        //Word size - number of bits in each input
        public int Size { get; private set; }

        public WireSet Input1 { get; private set; }
        public WireSet Input2 { get; private set; }
        public WireSet Output { get; private set; }
        //An overflow bit for the summation computation
        public Wire Overflow { get; private set; }


        public MultiBitAdder(int iSize)
        {
            Size = iSize;
            Input1 = new WireSet(Size);
            Input2 = new WireSet(Size);
            Output = new WireSet(Size);
            //your code here
            Overflow = new Wire();
            Wire tempCarry = new Wire();

            FullAdder[] tempFullAdder = new FullAdder[Size];
            for (int i = 0; i < Size; i++)
                tempFullAdder[i] = new FullAdder();

            tempCarry.Value = 0;
            tempFullAdder[0].CarryInput.ConnectInput(tempCarry);
            tempFullAdder[0].ConnectInput1(Input1[0]);
            tempFullAdder[0].ConnectInput2(Input2[0]);
            Output[0].ConnectInput(tempFullAdder[0].Output);
            for (int i = 1; i < Size; i++)
            {
                tempCarry = tempFullAdder[i - 1].CarryOutput;
                tempFullAdder[i].CarryInput.ConnectInput(tempCarry);
                tempFullAdder[i].ConnectInput1(Input1[i]);
                tempFullAdder[i].ConnectInput2(Input2[i]);
                Output[i].ConnectInput(tempFullAdder[i].Output);
            }
            Overflow.ConnectInput(tempFullAdder[Size - 1].CarryOutput);

        }

        public override string ToString()
        {
            return Input1 + "(" + Input1.Get2sComplement() + ")" + " + " + Input2 + "(" + Input2.Get2sComplement() + ")" + " = " + Output + "(" + Output.Get2sComplement() + ")";
        }

        public void ConnectInput1(WireSet wInput)
        {
            Input1.ConnectInput(wInput);
        }
        public void ConnectInput2(WireSet wInput)
        {
            Input2.ConnectInput(wInput);
        }


        public override bool TestGate()
        {
            for (int i = 0; i < 10; i++)
            {
                Random rand = new Random();
                int num1 = rand.Next((-(int)Math.Pow(2, Size - 1)), ((int)Math.Pow(2, Size - 1) - 1) + 1);
                int num2 = rand.Next((-(int)Math.Pow(2, Size - 1)), ((int)Math.Pow(2, Size - 1) - 1) + 1);
                int sum = num1 + num2;
                Input1.Set2sComplement(num1);
                Input2.Set2sComplement(num2);
                int result = Output.Get2sComplement();
                if (result != sum)
                    return false;
            }
            return true;
        }
    }
}
