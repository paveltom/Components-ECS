using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //This class implements a demux with k outputs, each output with n wires. The input also has n wires.

    class BitwiseMultiwayDemux : Gate
    {
        //Word size - number of bits in each output
        public int Size { get; private set; }

        //The number of control bits needed for k outputs
        public int ControlBits { get; private set; }

        public WireSet Input { get; private set; }
        public WireSet Control { get; private set; }
        public WireSet[] Outputs { get; private set; }

        public BitwiseDemux[] m_gBitwiseDemux;

        public BitwiseMultiwayDemux(int iSize, int cControlBits)
        {
            Size = iSize;
            Input = new WireSet(Size);
            Control = new WireSet(cControlBits);
            Outputs = new WireSet[(int)Math.Pow(2, cControlBits)];
            for (int i = 0; i < Outputs.Length; i++)
            {
                Outputs[i] = new WireSet(Size);
            }
            //your code here

            int numOfOutputs = Outputs.Length;
            m_gBitwiseDemux = new BitwiseDemux[numOfOutputs - 1];
            for (int i = 0; i < m_gBitwiseDemux.Length; i++)
                m_gBitwiseDemux[i] = new BitwiseDemux(Size);

            int currOutputsToConnect = Outputs.Length - 1;
            int currGateToConnect = m_gBitwiseDemux.Length - 1;
            int level = 1;
            int currNumOfGates = numOfOutputs / 2;


            for (int j = 0; j < currNumOfGates; j++) //connecting all the last level gates to outputs
            {
                Outputs[currOutputsToConnect].ConnectInput(m_gBitwiseDemux[currGateToConnect].Output2);
                Outputs[currOutputsToConnect - 1].ConnectInput(m_gBitwiseDemux[currGateToConnect].Output1);
                m_gBitwiseDemux[currGateToConnect].ConnectControl(Control[cControlBits-level]);
                currGateToConnect--;
                currOutputsToConnect -= 2;
            }

            int previousGatesIndex = numOfOutputs - 2;

            for (level = 2; level <= numOfOutputs; level++) // connectig gates' outputs to next level gates' inputs
            {
                currNumOfGates = currNumOfGates / 2;
                for (int j = 0; j < currNumOfGates; j++) //connecting all the inputs to gates
                {
                    m_gBitwiseDemux[previousGatesIndex].ConnectInput(m_gBitwiseDemux[currGateToConnect].Output2);
                    m_gBitwiseDemux[previousGatesIndex-1].ConnectInput(m_gBitwiseDemux[currGateToConnect].Output1);
                    m_gBitwiseDemux[currGateToConnect].ConnectControl(Control[cControlBits - level]);
                    currGateToConnect--;
                    previousGatesIndex -= 2;
                }
            }

            m_gBitwiseDemux[currGateToConnect + 1].ConnectInput(Input);

        }


        public void ConnectInput(WireSet wsInput)
        {
            Input.ConnectInput(wsInput);
        }
        public void ConnectControl(WireSet wsControl)
        {
            Control.ConnectInput(wsControl);
        }


        public override bool TestGate()
        {
            for (int i = 0; i < Input.Size; i++)           
                Input[i].Value = 0;
                       
            for (int i = 0; i < Outputs.Length; i++)
            {
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

                //verifying output values
                for (int j = 0; j < Size; j++)
                {
                    Input[j].Value = 1;
                    if (Outputs[i][j].Value != 1)
                        return false;
                    Input[j].Value = 0;
                    if (Outputs[i][j].Value != 0)
                        return false;
                }
            }

            return true;
        }
    }
}
