using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //Multibit gates take as input k bits, and compute a function over all bits - z=f(x_0,x_1,...,x_k)
    class MultiBitAndGate : MultiBitGate
    {
        private AndGate[] m_AndGates;

        public MultiBitAndGate(int iInputCount)
            : base(iInputCount)
        {
            m_AndGates = new AndGate[iInputCount - 1];
            for (int i = 0; i < m_AndGates.Length; i++)
                m_AndGates[i] = new AndGate();
            m_AndGates[0].ConnectInput1(m_wsInput[0]);
            m_AndGates[0].ConnectInput2(m_wsInput[1]);
          

            for (int i = 1; i < (iInputCount-1); i++)
            {
                m_AndGates[i].ConnectInput1(m_AndGates[i - 1].Output);
                m_AndGates[i].ConnectInput2(m_wsInput[i + 1]);
            }

            Output = m_AndGates[iInputCount - 2].Output;            
        }


        public override bool TestGate()
        {
            for(int i = 0; i < m_wsInput.Size; i++)
            {
                m_wsInput[i].Value = 1;
            }
            if (Output.Value != 1) return false;

            for (int i = 0; i < m_wsInput.Size; i++)
            {
                m_wsInput[i].Value = 0;
                if (Output.Value != 0) return false;
                m_wsInput[i].Value = 1;
            }

            for (int i = 0; i < m_wsInput.Size; i++)
            {
                m_wsInput[i].Value = 0;
            }
            if (Output.Value != 0) return false;


            return true;
        }
    }
}
