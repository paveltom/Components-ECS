using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //This class implements a mux with k input, each input with n wires. The output also has n wires.

    class BitwiseMultiwayMux : Gate
    {
        //Word size - number of bits in each output
        public int Size { get; private set; }

        //The number of control bits needed for k outputs
        public int ControlBits { get; private set; }

        public WireSet Output { get; private set; }
        public WireSet Control { get; private set; }
        public WireSet[] Inputs { get; private set; }

        public BitwiseMux[] m_gBitwiseMux;

        public BitwiseMultiwayMux(int iSize, int cControlBits)
        {
            Size = iSize;
            Output = new WireSet(Size);
            Control = new WireSet(cControlBits);
            Inputs = new WireSet[(int)Math.Pow(2, cControlBits)];

            for (int i = 0; i < Inputs.Length; i++)
            {
                Inputs[i] = new WireSet(Size);
            }

            int numOfInputs = (int)Math.Pow(2, cControlBits);
            m_gBitwiseMux = new BitwiseMux[numOfInputs - 1];
            for (int i = 0; i < m_gBitwiseMux.Length; i++)
                m_gBitwiseMux[i] = new BitwiseMux(Size);

            int currInputsToConnect = 0;
            int currGateToConnect = 0;
            int level = 1;
            int currNumOfGates = (int)Math.Pow(2, (numOfInputs - level));


            for (int j = 0; j < currNumOfGates; j++) //connecting all the inputs to gates
            {
                m_gBitwiseMux[currGateToConnect].ConnectInput1(Inputs[currInputsToConnect]);
                m_gBitwiseMux[currGateToConnect].ConnectInput2(Inputs[currInputsToConnect + 1]);
                currGateToConnect++;
                currInputsToConnect += 2;
            }

            int previousGatesIndex = 0;

            for (level = 2; level <= numOfInputs; level++) // connectig gates' outputs to next level gates' inputs
            {
                currNumOfGates = (int)Math.Pow(2, (numOfInputs - level));
                for (int j = 0; j < currNumOfGates; j++)
                {
                    m_gBitwiseMux[currGateToConnect].ConnectInput1(m_gBitwiseMux[previousGatesIndex].Output);
                    m_gBitwiseMux[currGateToConnect].ConnectInput2(m_gBitwiseMux[previousGatesIndex + 1].Output);
                    currGateToConnect++;
                    previousGatesIndex += 2;
                }
            }
            Output.ConnectInput(m_gBitwiseMux[currGateToConnect - 1].Output);


        }


        public void ConnectInput(int i, WireSet wsInput)
        {
            Inputs[i].ConnectInput(wsInput);
        }
        public void ConnectControl(WireSet wsControl)
        {
            Control.ConnectInput(wsControl);
        }



        public override bool TestGate()
        {

            for (int i = 0; i < Inputs.Length; i++)
            {
                for (int j = 0; j < Size; j++)
                    Inputs[i][j].Value = 0;
            }


            for (int i = 0; i < Inputs.Length; i++)
            {
                for (int j = 0; j < Size; j++) // current input becomes 'active'
                    Inputs[i][j].Value = 1;

                int deciNum = i;
                for (int k = 0; k < Control.Size; k++) //decoding control bits from decimal to binary
                { 
                    if (deciNum != 0)
                    {
                        Control[i].Value = deciNum % 2;
                        deciNum = deciNum / 2;
                    }
                    else Control[i].Value = 0;
                }

                // if () statement in the for loop, to verify output values
                for (int k = 0; k < Size; k++)
                    if (Output[k].Value != 1)
                        return false;

                for (int j = 0; j < Size; j++) // current input becomes 'not active'
                    Inputs[i][j].Value = 0;
            }

            return true;
        }
    }
}
