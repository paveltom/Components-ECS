﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //This bitwise gate takes as input one WireSet containing n wires, and computes a bitwise function - z_i=f(x_i)
    class BitwiseNotGate : Gate
    {
        public WireSet Input { get; private set; }
        public WireSet Output { get; private set; }
        public int Size { get; private set; }

        private NotGate[] m_gNot;

        public BitwiseNotGate(int iSize)
        {
            Size = iSize;
            Input = new WireSet(Size);
            Output = new WireSet(Size);

            m_gNot = new NotGate[Size];
            for (int i = 0; i < Size; i++)
            {
                m_gNot[i] = new NotGate();
                m_gNot[i].ConnectInput(Input[i]);
                Output[i].ConnectInput(m_gNot[i].Output);
            }
        }

        public void ConnectInput(WireSet ws)
        {
            Input.ConnectInput(ws);
        }

        //an implementation of the ToString method is called, e.g. when we use Console.WriteLine(not)
        //this is very helpful during debugging
        public override string ToString()
        {
            return "Not " + Input + " -> " + Output;
        }

        public override bool TestGate()
        {
            for (int i = 0; i < Size; i++)
            {
                Input[i].Value = 1;
                if (Output[i].Value != 0)
                    return false;

                Input[i].Value = 0;
                if (Output[i].Value != 1)
                    return false;

            }

            return true;
        }
    }
}
