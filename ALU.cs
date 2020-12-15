using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //This class is used to implement the ALU
    //The specification can be found at https://b1391bd6-da3d-477d-8c01-38cdf774495a.filesusr.com/ugd/56440f_2e6113c60ec34ed0bc2035c9d1313066.pdf slides 48,49
    class ALU : Gate
    {
        //The word size = number of bit in the input and output
        public int Size { get; private set; }

        //Input and output n bit numbers
        public WireSet InputX { get; private set; }
        public WireSet InputY { get; private set; }
        public WireSet Output { get; private set; }

        //Control bit 
        public Wire ZeroX { get; private set; }
        public Wire ZeroY { get; private set; }
        public Wire NotX { get; private set; }
        public Wire NotY { get; private set; }
        public Wire F { get; private set; }
        public Wire NotOutput { get; private set; }

        //Bit outputs
        public Wire Zero { get; private set; }
        public Wire Negative { get; private set; }


        //your code here

        public ALU(int iSize)
        {
            Size = iSize;
            InputX = new WireSet(Size);
            InputY = new WireSet(Size);
            ZeroX = new Wire();
            ZeroY = new Wire();
            NotX = new Wire();
            NotY = new Wire();
            F = new Wire();
            NotOutput = new Wire();
            Negative = new Wire();            
            Zero = new Wire();


            //Create and connect all the internal components
            Output = new WireSet(Size);

            BitwiseMux m_gZX = new BitwiseMux(Size);
            BitwiseMux m_gZY = new BitwiseMux(Size);
            WireSet zeroForXY = new WireSet(Size);
            zeroForXY.Set2sComplement(0);

            BitwiseNotGate m_gNotX = new BitwiseNotGate(Size); // reverse X
            BitwiseNotGate m_gNotY = new BitwiseNotGate(Size); // reverse Y
            BitwiseMux m_gNX = new BitwiseMux(Size);
            BitwiseMux m_gNY = new BitwiseMux(Size);

            BitwiseAndGate m_gBAnd = new BitwiseAndGate(Size); // X & Y
            MultiBitAdder mbAdder = new MultiBitAdder(Size); // X + Y

            BitwiseMux m_gF = new BitwiseMux(Size);

            BitwiseNotGate m_gNotFOut = new BitwiseNotGate(Size); // reverse F output
            BitwiseMux m_gN = new BitwiseMux(Size);

            MultiBitAndGate m_gZeroAnd = new MultiBitAndGate(Size);
            BitwiseNotGate m_gZeroNot = new BitwiseNotGate(Size);


            // connect inputX and input0 to ZX, connect ZXControl
            m_gZX.ConnectInput1(InputX);
            m_gZX.ConnectInput2(zeroForXY);
            m_gZX.ConnectControl(ZeroX);

            // connect bitwiseNotXIn to ZX output, connect ZXOut to NXIn1, connect bitwiseNotOut to NXIn2
            m_gNotX.ConnectInput(m_gZX.Output);
            m_gNX.ConnectInput1(m_gZX.Output);
            m_gNX.ConnectInput2(m_gNotX.Output);
            m_gNX.ConnectControl(NotX);

            // connect inputY and input0 to ZY, connect ZYControl
            m_gZY.ConnectInput1(InputY);
            m_gZY.ConnectInput2(zeroForXY);
            m_gZY.ConnectControl(ZeroY);

            // connect bitwiseNotYIn to ZY output, connect ZYOut to NYIn1, connect bitwiseNotYOut to NYIn2
            m_gNotY.ConnectInput(m_gZY.Output);
            m_gNY.ConnectInput1(m_gZY.Output);
            m_gNY.ConnectInput2(m_gNotY.Output);
            m_gNY.ConnectControl(NotY);

            // connect NXOut to bitwiseAndIn1, connect NYOut to bitwiseAndIn2
            m_gBAnd.ConnectInput1(m_gNX.Output);
            m_gBAnd.ConnectInput2(m_gNY.Output);
            // connect NXOut to mbAdderIn1, connect NYOut to mbAdderIn2
            mbAdder.ConnectInput1(m_gNX.Output);
            mbAdder.ConnectInput2(m_gNY.Output);
            // connect bitwiseAndOut to F_In1, connect mbAdderOut to F_In2
            m_gF.ConnectInput1(m_gBAnd.Output);
            m_gF.ConnectInput2(mbAdder.Output);
            m_gF.ConnectControl(F);

            //connect F_Out to bitwiseNotFIn
            m_gNotFOut.ConnectInput(m_gF.Output);
            // connect F_Out to N_In1, connect bitwiseNotFOut to N_In2
            m_gN.ConnectInput1(m_gF.Output);
            m_gN.ConnectInput2(m_gNotFOut.Output);
            m_gN.ConnectControl(NotOutput);

            // connect N_Out to Output
            Output.ConnectInput(m_gN.Output);     

            // connect negativeIndicator to last Output wire
            Negative.ConnectInput(Output[Size - 1]);
            // connect ZeroIndicator depending on zeroNot and zeroAnd concatenation 
            m_gZeroNot.ConnectInput(Output);
            m_gZeroAnd.ConnectInput(m_gZeroNot.Output);
            Zero.ConnectInput(m_gZeroAnd.Output);


        }

        public override bool TestGate()
        {
            int[] arrZX = new int[] { 1, 1, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 0, 0, 0, 0 };
            int[] arrNX = new int[] { 0, 1, 1, 0, 1, 0, 1, 0, 1, 1, 1, 0, 1, 0, 1, 0, 0, 1 };
            int[] arrZY = new int[] { 1, 1, 1, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0 };
            int[] arrNY = new int[] { 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 1, 1, 0, 0, 0, 1, 0, 1 };
            int[] arrF = new int[] { 1, 1, 1, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0 };
            int[] arrNO = new int[] { 0, 1, 0, 0, 0, 1, 1, 1, 1, 1, 1, 0, 0, 0, 1, 1, 0, 1 };

            InputX = new WireSet(Size);
            InputY = new WireSet(Size);
            Random rand = new Random();

            WireSet[] answer = new WireSet[arrZX.Length];
            for (int i = 0; i < answer.Length; i++)
                answer[i] = new WireSet(Size);
            answer[0].Set2sComplement(0);
            answer[1].Set2sComplement(1);
            answer[2].Set2sComplement(-1);

            for (int i = 0; i < arrZX.Length; i++)
            {
                int num1 = rand.Next((-(int)Math.Pow(2, Size - 1)), ((int)Math.Pow(2, Size - 1) - 1) + 1);
                int num2 = rand.Next((-(int)Math.Pow(2, Size - 1)), ((int)Math.Pow(2, Size - 1) - 1) + 1);                
                InputX.Set2sComplement(num1);
                InputY.Set2sComplement(num2);

                ZeroX.Value = arrZX[i];
                NotX.Value = arrNX[i];
                ZeroY.Value = arrZY[i];
                NotY.Value = arrNY[i];
                F.Value = arrF[i];
                NotOutput.Value = arrNO[i];

                if (Negative.Value != Output[Size - 1].Value)
                    return false;
                if (Output.Get2sComplement() == 0 && Zero.Value != 1)
                    return false;
                if (Output.Get2sComplement() != 0 && Zero.Value != 0)
                    return false;


                answer[3].Set2sComplement(num1);
                answer[4].Set2sComplement(num2);
                BitwiseNotGate tempGX = new BitwiseNotGate(Size);
                tempGX.ConnectInput(InputX);
                answer[5].ConnectInput(tempGX.Output);
                BitwiseNotGate tempGY = new BitwiseNotGate(Size);
                tempGY.ConnectInput(InputY);
                answer[6].ConnectInput(tempGY.Output);
                answer[7].Set2sComplement(-num1);
                answer[8].Set2sComplement(-num2);
                answer[9].Set2sComplement(num1 + 1);
                answer[10].Set2sComplement(num2 + 1);
                answer[11].Set2sComplement(num1 - 1);
                answer[12].Set2sComplement(num2 - 1);
                answer[13].Set2sComplement(num1 + num2);
                answer[14].Set2sComplement(num1 - num2);
                answer[15].Set2sComplement(num2 - num1);
                BitwiseAndGate tempGAnd = new BitwiseAndGate(Size);
                tempGAnd.ConnectInput1(InputX);
                tempGAnd.ConnectInput2(InputY);
                answer[16].ConnectInput(tempGAnd.Output);
                answer[17].ConnectInput(Output);

                for (int j = 0; j < Size; j++)
                {
                    if (answer[i][j].Value != Output[j].Value)
                        return false;
                }
            }
            return true;
                                          
        }
    }
}
