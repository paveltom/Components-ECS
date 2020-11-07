using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //Multibit gates take as input k bits, and compute a function over all bits - z=f(x_0,x_1,...,x_k)
    class MultiBitAndGate : MultiBitGate
    {
        AndGate[] m_AndGates;
        protected WireSet m_wsInput;
        public Wire Output { get; protected set; }


        public MultiBitAndGate(int iInputCount)
            : base(iInputCount)
        {

            m_AndGates = new AndGate[iInputCount];
            m_AndGates[0].ConnectInput1(m_wsInput[0]);
            m_AndGates[0].ConnectInput2(m_wsInput[1]);

            for (int i = 1; i < iInputCount; i++)
            {
                m_AndGates[i].ConnectInput1(m_AndGates[i - 1].Output);
                m_AndGates[i].ConnectInput2(m_wsInput[i + 1]);
            }

            Output = m_AndGates[iInputCount - 1].Output;
            
        }


        public override bool TestGate()
        {
            throw new NotImplementedException();
        }
    }
}
