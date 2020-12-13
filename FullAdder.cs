using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //This class implements a FullAdder, taking as input 3 bits - 2 numbers and a carry, and computing the result in the output, and the carry out.
    class FullAdder : TwoInputGate
    {
        public Wire CarryInput { get; private set; }
        public Wire CarryOutput { get; private set; }

        //your code here
        private HalfAdder m_halfAdder1;
        private HalfAdder m_halfAdder2;
        private XorGate m_xorGate;



        public FullAdder()
        {
            CarryInput = new Wire();
            //your code here
            CarryOutput = new Wire();
            m_halfAdder1 = new HalfAdder();
            m_halfAdder2 = new HalfAdder();
            m_xorGate = new XorGate();

            m_halfAdder1.ConnectInput1(Input1);
            m_halfAdder1.ConnectInput2(Input2);

            m_halfAdder2.ConnectInput1(m_halfAdder1.Output);
            m_halfAdder2.ConnectInput2(CarryInput);

            m_xorGate.ConnectInput1(m_halfAdder1.CarryOutput);
            m_xorGate.ConnectInput2(m_halfAdder2.CarryOutput);

            Output.ConnectInput(m_halfAdder2.Output);
            CarryOutput.ConnectInput(m_xorGate.Output);
        }


        public override string ToString()
        {
            return Input1.Value + "+" + Input2.Value + " (C" + CarryInput.Value + ") = " + Output.Value + " (C" + CarryOutput.Value + ")";
        }

        public override bool TestGate()
        {
            Input1.Value = 0;
            Input2.Value = 0;
            CarryInput.Value = 0;
            if (CarryOutput.Value != 0 && Output.Value != 0)
                return false;

            Input1.Value = 0;
            Input2.Value = 1;
            CarryInput.Value = 0;
            if (CarryOutput.Value != 0 && Output.Value != 1)
                return false;

            Input1.Value = 1;
            Input2.Value = 0;
            CarryInput.Value = 0;
            if (CarryOutput.Value != 0 && Output.Value != 1)
                return false;

            Input1.Value = 1;
            Input2.Value = 1;
            CarryInput.Value = 0;
            if (CarryOutput.Value != 1 && Output.Value != 0)
                return false;




            Input1.Value = 0;
            Input2.Value = 0;
            CarryInput.Value = 1;
            if (CarryOutput.Value != 0 && Output.Value != 1)
                return false;

            Input1.Value = 0;
            Input2.Value = 1;
            CarryInput.Value = 1;
            if (CarryOutput.Value != 1 && Output.Value != 0)
                return false;

            Input1.Value = 1;
            Input2.Value = 0;
            CarryInput.Value = 1;
            if (CarryOutput.Value != 1 && Output.Value != 0)
                return false;

            Input1.Value = 1;
            Input2.Value = 1;
            CarryInput.Value = 1;
            if (CarryOutput.Value != 1 && Output.Value != 1)
                return false;

            return true;
        }
    }
}
