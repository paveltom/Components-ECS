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

            int numOfInputs = (int)Math.Pow(2, cControlBits);
            m_gBitwiseDemux = new BitwiseDemux[numOfInputs - 1];
            for (int i = 0; i < m_gBitwiseDemux.Length; i++)
                m_gBitwiseDemux[i] = new BitwiseDemux(Size);

            int currOutputsToConnect = 0;
            int currGateToConnect = 0;
            int level = 1;
            int currNumOfGates = (int)Math.Pow(2, (numOfInputs - level));


            for (int j = 0; j < currNumOfGates; j++) //connecting all the inputs to gates
            {
                m_gBitwiseDemux[currGateToConnect].ConnectOutput1(Inputs[currOutputsToConnect]);
                m_gBitwiseDemux[currGateToConnect].ConnectInput2(Inputs[currOutputsToConnect + 1]);
                currGateToConnect++;
                currOutputsToConnect += 2;
            }

            int previousGatesIndex = 0;

            for (level = 2; level <= numOfInputs; level++) // connectig gates' outputs to next level gates' inputs
            {
                currNumOfGates = (int)Math.Pow(2, (numOfInputs - level));
                for (int j = 0; j < currNumOfGates; j++) //connecting all the inputs to gates
                {
                    m_gBitwiseDemux[currGateToConnect].ConnectInput1(m_gBitwiseDemux[previousGatesIndex].Output);
                    m_gBitwiseDemux[currGateToConnect].ConnectInput2(m_gBitwiseDemux[previousGatesIndex + 1].Output);
                    currGateToConnect++;
                    previousGatesIndex += 2;
                }
            }

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
            throw new NotImplementedException();
        }
    }
}
