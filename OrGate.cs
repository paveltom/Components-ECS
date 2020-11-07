using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //This gate implements the or operation. To implement it, follow the example in the And gate.
    class OrGate : TwoInputGate
    {
        private NAndGate m_gNand;
        private NotGate m_gNotX;
        private NotGate m_gNotY;

        public OrGate()
        {
            m_gNand = new NAndGate();
            m_gNotX = new NotGate();
            m_gNotY = new NotGate();

            m_gNand.ConnectInput1(m_gNotX.Output);
            m_gNand.ConnectInput2(m_gNotY.Output);

            Input1 = m_gNotX.Input;
            Input2 = m_gNotY.Input;
            Output = m_gNand.Output;
        }


        public override string ToString()
        {
            return "Or " + Input1.Value + "," + Input2.Value + " -> " + Output.Value;
        }

        public override bool TestGate()
        {
            Input1.Value = 0;
            Input2.Value = 0;
            if (Output.Value != 0)
                return false;
            Input1.Value = 0;
            Input2.Value = 1;
            if (Output.Value != 1)
                return false;
            Input1.Value = 1;
            Input2.Value = 0;
            if (Output.Value != 1)
                return false;
            Input1.Value = 1;
            Input2.Value = 1;
            if (Output.Value != 1)
                return false;
            return true;
        }
    }

}
