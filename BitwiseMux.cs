using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //A bitwise gate takes as input WireSets containing n wires, and computes a bitwise function - z_i=f(x_i)
    class BitwiseMux : BitwiseTwoInputGate
    {
        public Wire ControlInput { get; private set; }

        public MuxGate[] m_gMux;

        public BitwiseMux(int iSize)
            : base(iSize)
        {

            ControlInput = new Wire();

            m_gMux = new MuxGate[Size];
            for (int i = 0; i < Size; i++)
            {
                m_gMux[i].ConnectInput1(Input1[i]);
                m_gMux[i].ConnectInput2(Input2[i]);
                Output[i].ConnectInput(m_gMux[i].Output);
                m_gMux[i].ConnectControl(ControlInput);
            }

        }

        public void ConnectControl(Wire wControl)
        {
            ControlInput.ConnectInput(wControl);
        }



        public override string ToString()
        {
            return "Mux " + Input1 + "," + Input2 + ",C" + ControlInput.Value + " -> " + Output;
        }




        public override bool TestGate()
        {
            for (int i = 0; i < Size; i++)
            {
                Input1[i].Value = 0;
                Input2[i].Value = 1;
                ControlInput.Value = 0;
                if (Output[i].Value != 0)
                    return false;

                Input1[i].Value = 1;
                Input2[i].Value = 0;
                ControlInput.Value = 1;
                if (Output[i].Value != 0)
                    return false;

                Input1[i].Value = 1;
                Input2[i].Value = 0;
                ControlInput.Value = 0;
                if (Output[i].Value != 1)
                    return false;

                Input1[i].Value = 0;
                Input2[i].Value = 1;
                ControlInput.Value = 1;
                if (Output[i].Value != 1)
                    return false;
            }

            return true;
        }
    }
}
