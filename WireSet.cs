using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //This class represents a set of n wires (a cable)
    class WireSet
    {
        //Word size - number of bits in the register
        public int Size { get; private set; }
        
        public bool InputConected { get; private set; }

        //An indexer providing access to a single wire in the wireset
        public Wire this[int i]
        {
            get
            {
                return m_aWires[i];
            }
        }
        private Wire[] m_aWires;
        
        public WireSet(int iSize)
        {
            Size = iSize;
            InputConected = false;
            m_aWires = new Wire[iSize];
            for (int i = 0; i < m_aWires.Length; i++)
                m_aWires[i] = new Wire();
        }
        public override string ToString()
        {
            string s = "[";
            for (int i = m_aWires.Length - 1; i >= 0; i--)
                s += m_aWires[i].Value;
            s += "]";
            return s;
        }

        //Transform a positive integer value into binary and set the wires accordingly, with 0 being the LSB
        public void SetValue(int iValue)
        {
            for (int k = 0; k < Size; k++) 
            {
                if (iValue != 0)
                {
                    m_aWires[k].Value = iValue % 2;
                    iValue = iValue / 2;
                }
                else m_aWires[k].Value = 0;
            }
        }

        //Transform the binary code into a positive integer
        public int GetValue()
        {
            int output = 0;
            for (int i = 0; i < Size; i++)
            {
                output = output + (int)Math.Pow((2 * m_aWires[i].Value), i);
            }

            return output;
        }

        //Transform an integer value into binary using 2`s complement and set the wires accordingly, with 0 being the LSB
        public void Set2sComplement(int iValue)
        {
            SetValue(Math.Abs(iValue));
            if (iValue < 0)
            {
                for(int i = 0; i < Size; i++) //reversing the bits
                {
                    if (m_aWires[i].Value == 0)
                        m_aWires[i].Value = 1;
                    else m_aWires[i].Value = 0;
                }

                if (m_aWires[0].Value == 0) // adding 1
                    m_aWires[0].Value = 1;
                else
                {
                    for (int i = 0; i < Size; i++)
                    {
                        if (m_aWires[i].Value == 1)
                            m_aWires[i].Value = 0;
                        else
                        {
                            m_aWires[i].Value = 1;
                            i = Size; //break
                        }

                    }
                }

            }
        }

        //Transform the binary code in 2`s complement into an integer
        public int Get2sComplement()
        {
            if (m_aWires[Size - 1].Value == 0)
                return GetValue();
            WireSet tempSet = new WireSet(Size);
            if (m_aWires[0].Value == 1)
            {
                tempSet[0].Value = 1;
                for (int i = 1; i < Size; i++) //reversing the bits
                {
                    if (m_aWires[i].Value == 0)
                        tempSet[i].Value = 1;
                    else tempSet[i].Value = 0;
                }
            }
            else
            {
                int i = 0;
                while (m_aWires[i].Value == 0)
                    i++;
                i++;
                for (; i < Size; i++)  //reversing the bits
                {
                    if (m_aWires[i].Value == 0)
                        tempSet[i].Value = 1;
                    else tempSet[i].Value = 0;
                }
            }

            return ( -tempSet.GetValue() );
        }

        public void ConnectInput(WireSet wIn)
        {
            if (InputConected)
                throw new InvalidOperationException("Cannot connect a wire to more than one inputs");
            if(wIn.Size != Size)
                throw new InvalidOperationException("Cannot connect two wiresets of different sizes.");
            for (int i = 0; i < m_aWires.Length; i++)
                m_aWires[i].ConnectInput(wIn[i]);

            InputConected = true;
            
        }

    }
}
