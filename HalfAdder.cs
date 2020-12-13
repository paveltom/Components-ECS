﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //This class implements a HalfAdder, taking as input 2 bits - 2 numbers and computing the result in the output, and the carry out.

    class HalfAdder : TwoInputGate
    {
        public Wire CarryOutput { get; private set; }

        //your code here
        private XorGate m_xorGate;
        private AndGate m_andGate;


        public HalfAdder()
        {
            CarryOutput = new Wire();
            m_xorGate = new XorGate();
            m_andGate = new AndGate();

            m_xorGate.Input1.ConnectInput(Input1);
            m_xorGate.Input2.ConnectInput(Input2);
            Output = m_xorGate.Output;

            m_andGate.Input1.ConnectInput(Input1);
            m_andGate.Input2.ConnectInput(Input2);
            CarryOutput = m_andGate.Output;
        }


        public override string ToString()
        {
            return "HA " + Input1.Value + "," + Input2.Value + " -> " + Output.Value + " (C" + CarryOutput + ")";
        }

        public override bool TestGate()
        {
            Input1.Value = 0;
            Input2.Value = 0;
            if (Output.Value != 0 && CarryOutput.Value != 0)
                return false;

            Input1.Value = 0;
            Input2.Value = 1;
            if (Output.Value != 1 && CarryOutput.Value != 0)
                return false;

            Input1.Value = 1;
            Input2.Value = 0;
            if (Output.Value != 1 && CarryOutput.Value != 0)
                return false;

            Input1.Value = 1;
            Input2.Value = 1;
            if (Output.Value != 0 && CarryOutput.Value != 1)
                return false;

            return true;

        }
    }
}
