using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //A two input bitwise gate takes as input two WireSets containing n wires, and computes a bitwise function - z_i=f(x_i,y_i)
    class BitwiseAndGate : BitwiseTwoInputGate
    {
        private AndGate[] m_gAnd;

        public BitwiseAndGate(int iSize)
            : base(iSize)
        {
            m_gAnd = new AndGate[iSize];
            for (int i = 0; i < iSize; i++)
                m_gAnd[i] = new AndGate();
            for (int i = 0; i < iSize; i++)
            {
                m_gAnd[i].ConnectInput1(Input1[i]);
                m_gAnd[i].ConnectInput2(Input2[i]);
                Output[i].ConnectInput(m_gAnd[i].Output);                
            }

        }

        //an implementation of the ToString method is called, e.g. when we use Console.WriteLine(and)
        //this is very helpful during debugging
        public override string ToString()
        {
            return "And " + Input1 + ", " + Input2 + " -> " + Output;
        }

        public override bool TestGate()
        {
            for (int i = 0; i < m_gAnd.Length; i++)
            {
                m_gAnd[i].Input1.Value = 1;
                m_gAnd[i].Input1.Value = 1;
                if (Output[i].Value != 1)
                    return false;

                m_gAnd[i].Input1.Value = 0;
                m_gAnd[i].Input1.Value = 0;
                if (Output[i].Value != 0)
                    return false;

                m_gAnd[i].Input1.Value = 1;
                m_gAnd[i].Input1.Value = 0;
                if (Output[i].Value != 0)
                    return false;

                m_gAnd[i].Input1.Value = 0;
                m_gAnd[i].Input1.Value = 1;
                if (Output[i].Value != 0)
                    return false;
            }

            return true;
        }
    }
}
