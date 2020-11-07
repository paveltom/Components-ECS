using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //A mux has 2 inputs. There is a single output and a control bit, selecting which of the 2 inpust should be directed to the output.
    class MuxGate : TwoInputGate
    {
        public Wire ControlInput { get; private set; }
        private AndGate m_gAndX;
        private AndGate m_gAndY;
        private OrGate m_gOr;
        private NotGate m_gNotC;
        

        public MuxGate()
        {
            ControlInput = new Wire();
            m_gAndX = new AndGate();
            m_gAndY = new AndGate();
            m_gOr = new OrGate();
            m_gNotC = new NotGate();

            m_gAndY.ConnectInput1(ControlInput);
            m_gAndY.ConnectInput2(Input2);

            m_gNotC.ConnectInput(ControlInput);
            m_gAndX.ConnectInput2(m_gNotC.Output);
            m_gAndX.ConnectInput1(Input1);

            m_gOr.ConnectInput1(m_gAndX.Output);
            m_gOr.ConnectInput2(m_gAndY.Output);
            Output = m_gOr.Output;

        }

        public void ConnectControl(Wire wControl)
        {
            ControlInput.ConnectInput(wControl);
        }


        public override string ToString()
        {
            return "Mux " + Input1.Value + "," + Input2.Value + ",C" + ControlInput.Value + " -> " + Output.Value;
        }



        public override bool TestGate()
        {
            Input1.Value = 0;
            Input2.Value = 0;
            ControlInput.Value = 0;
            if (Output.Value != 0)
                return false;

            Input1.Value = 0;
            Input2.Value = 0;
            ControlInput.Value = 1;
            if (Output.Value != 0)
                return false;

            Input1.Value = 0;
            Input2.Value = 1;
            ControlInput.Value = 0;
            if (Output.Value != 0)
                return false;

            Input1.Value = 0;
            Input2.Value = 1;
            ControlInput.Value = 1;
            if (Output.Value != 1)
                return false;

            Input1.Value = 1;
            Input2.Value = 0;
            ControlInput.Value = 0;
            if (Output.Value != 1)
                return false;

            Input1.Value = 1;
            Input2.Value = 0;
            ControlInput.Value = 1;
            if (Output.Value != 0)
                return false;

            Input1.Value = 1;
            Input2.Value = 1;
            ControlInput.Value = 0;
            if (Output.Value != 1)
                return false;

            Input1.Value = 1;
            Input2.Value = 1;
            ControlInput.Value = 1;
            if (Output.Value != 1)
                return false;

            return true;
        }
    }
}
