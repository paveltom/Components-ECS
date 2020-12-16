using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //This class represents an n bit register that can maintain an n bit number
    class MultiBitRegister : Gate
    {
        public WireSet Input { get; private set; }
        public WireSet Output { get; private set; }
        //A bit setting the register operation to read or write
        public Wire Load { get; private set; }

        //Word size - number of bits in the register
        public int Size { get; private set; }

        private SingleBitRegister[] m_rSingle;

        public MultiBitRegister(int iSize)
        {
            Size = iSize;
            Input = new WireSet(Size);
            Output = new WireSet(Size);
            Load = new Wire();
            //your code here
            m_rSingle = new SingleBitRegister[Size];
            for (int i = 0; i < Size; i++)
            {
                m_rSingle[i] = new SingleBitRegister();
                m_rSingle[i].ConnectLoad(Load);
                m_rSingle[i].ConnectInput(Input[i]);
                Output[i].ConnectInput(m_rSingle[i].Output);
            }
        }

        public void ConnectInput(WireSet wsInput)
        {
            Input.ConnectInput(wsInput);
        }

        
        public override string ToString()
        {
            return Output.ToString();
        }


        public override bool TestGate()
        {
            Random rand = new Random();
            WireSet randWire = new WireSet(Size);
            WireSet maxWire = new WireSet(Size);
            maxWire.Set2sComplement((int)Math.Pow(2, Size - 1) - 1);

            for (int i = 0; i < 1000; i++)
            {
                int num = (int)rand.Next(-(int)Math.Pow(2, Size - 1), ((int)Math.Pow(2, Size - 1) - 1));
                randWire.Set2sComplement(num);

                Input.Set2sComplement(randWire.Get2sComplement());
                Load.Value = 1;

                Clock.ClockDown();
                Clock.ClockUp();

                Load.Value = 0;
                Input.Set2sComplement(maxWire.Get2sComplement());
                if (randWire.Get2sComplement() != Output.Get2sComplement())
                    return false;

                Clock.ClockDown();
                Clock.ClockUp();

                if (randWire.Get2sComplement() != Output.Get2sComplement())
                    return false;

                Load.Value = 1;

                Clock.ClockDown();
                Clock.ClockUp();

                if (Input.Get2sComplement() != Output.Get2sComplement())
                    return false;
            }

            return true;

        }
    }
}
