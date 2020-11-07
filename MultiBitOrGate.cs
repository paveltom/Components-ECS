using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //Multibit gates take as input k bits, and compute a function over all bits - z=f(x_0,x_1,...,x_k)

    class MultiBitOrGate : MultiBitGate
    {
        private OrGate[] m_OrGates;


        public MultiBitOrGate(int iInputCount)
            : base(iInputCount)
        {
            m_OrGates = new OrGate[iInputCount - 1];
            for (int i = 0; i < m_OrGates.Length; i++)
                m_OrGates[i] = new OrGate();
            m_OrGates[0].ConnectInput1(m_wsInput[0]);
            m_OrGates[0].ConnectInput2(m_wsInput[1]);

            for (int i = 1; i < (iInputCount - 1); i++)
            {
                m_OrGates[i].ConnectInput1(m_OrGates[i - 1].Output);
                m_OrGates[i].ConnectInput2(m_wsInput[i + 1]);
            }

            Output = m_OrGates[iInputCount - 2].Output;
        }

        public override bool TestGate()
        {
            throw new NotImplementedException();
        }
    }
}
