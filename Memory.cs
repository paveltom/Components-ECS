using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //This class implements a memory unit, containing k registers, each of size n bits.
    class Memory : SequentialGate
    {
        //The address size determines the number of registers
        public int AddressSize { get; private set; }
        //The word size determines the number of bits in each register
        public int WordSize { get; private set; }

        //Data input and output - a number with n bits
        public WireSet Input { get; private set; }
        public WireSet Output { get; private set; }
        //The address of the active register
        public WireSet Address { get; private set; }
        //A bit setting the memory operation to read or write
        public Wire Load { get; private set; }

        //your code here
        private MultiBitRegister[] m_rMultiBit;
        private BitwiseMultiwayMux m_gBWMultiMux;
        private BitwiseMultiwayDemux m_gBWMultiDemux;


        public Memory(int iAddressSize, int iWordSize)
        {
            AddressSize = iAddressSize;
            WordSize = iWordSize;

            Input = new WireSet(WordSize);
            Output = new WireSet(WordSize);
            Address = new WireSet(AddressSize);
            Load = new Wire();

            //your code here
            m_rMultiBit = new MultiBitRegister[(int)Math.Pow(2, AddressSize)];

            m_gBWMultiMux = new BitwiseMultiwayMux(WordSize, AddressSize); //output 
            m_gBWMultiMux.ConnectControl(Address);
            //connect all the inputs in loop

            m_gBWMultiDemux = new BitwiseMultiwayDemux(1, AddressSize); // Read/Write address
            m_gBWMultiDemux.ConnectControl(Address);
            WireSet connection = new WireSet(1); //because BWMultiwayDemux receives WireSet
            connection[0].ConnectInput(Load);
            m_gBWMultiDemux.ConnectInput(connection);
            //connect all the outputs in loop


            for (int i = 0; i < m_rMultiBit.Length; i++)
            {
                m_rMultiBit[i] = new MultiBitRegister(WordSize); //init
                m_rMultiBit[i].Load.ConnectInput(m_gBWMultiDemux.Outputs[i][0]); //connect Load from Demux output
                m_rMultiBit[i].ConnectInput(Input); // connect input
                m_gBWMultiMux.Inputs[i].ConnectInput(m_rMultiBit[i].Output); //connect output to Mux
            }

            Output.ConnectInput(m_gBWMultiMux.Output);
                                 
        }

        public void ConnectInput(WireSet wsInput)
        {
            Input.ConnectInput(wsInput);
        }
        public void ConnectAddress(WireSet wsAddress)
        {
            Address.ConnectInput(wsAddress);
        }


        public override void OnClockUp()
        {
        }

        public override void OnClockDown()
        {
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }

        public override bool TestGate()
        {
            Random rand = new Random();
            WireSet randInWire = new WireSet(WordSize);
            for (int i = 0; i < m_rMultiBit.Length; i++)
            {
                randInWire.Set2sComplement((int)rand.Next(-(int)Math.Pow(2, WordSize - 1), ((int)Math.Pow(2, WordSize - 1) - 1))); //generate random valid input
                Input.Set2sComplement(randInWire.Get2sComplement()); // connect input to randomized wire
                Address.Set2sComplement((int)rand.Next(0, (int)Math.Pow(2, AddressSize) + 1)); //generate random valid address
                Load.Value = 1;

                Clock.ClockDown();
                Clock.ClockUp();

                Load.Value = 0;
                int tempChange = randInWire.Get2sComplement();
                if (tempChange < 0)
                    tempChange++;
                else
                    tempChange--;
                Input.Set2sComplement(tempChange);
                if (Output.Get2sComplement() != randInWire.Get2sComplement())
                    return false;

                Clock.ClockDown();
                Clock.ClockUp();
                if (Output.Get2sComplement() != randInWire.Get2sComplement())
                    return false;

                Load.Value = 1;
                Clock.ClockDown();
                Clock.ClockUp();
                if (Output.Get2sComplement() != Input.Get2sComplement())
                    return false;

                tempChange = Address.Get2sComplement();
                if (tempChange != 0)
                    tempChange--;
                else tempChange++;
                Address.Set2sComplement(tempChange);
                Clock.ClockDown();
                Clock.ClockUp();
                if (Output.Get2sComplement() != Input.Get2sComplement())
                    return false;
            }

            return true;
        }
    }
}
