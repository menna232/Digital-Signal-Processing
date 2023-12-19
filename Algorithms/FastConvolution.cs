using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class FastConvolution : Algorithm
    {
        public Signal InputSignal1 { get; set; }
        public Signal InputSignal2 { get; set; }
        public Signal OutputConvolvedSignal { get; set; }

        /// <summary>
        /// Convolved InputSignal1 (considered as X) with InputSignal2 (considered as H)
        /// </summary>
        public override void Run()
        {
            List<float> Amplitudes_List = new List<float>();
            List<float> PhaseShifts_List = new List<float>();


            int N = InputSignal1.Samples.Count + InputSignal2.Samples.Count - 1;
            for (int i = InputSignal1.Samples.Count; i < N; i++)
            {
                InputSignal1.Samples.Add(0);
            }
            for (int j = InputSignal2.Samples.Count; j < N; j++)
            {
                InputSignal2.Samples.Add(0);
            }

            DiscreteFourierTransform Signal1_DFT = new DiscreteFourierTransform();
            DiscreteFourierTransform Signal2_DFT = new DiscreteFourierTransform();

            Signal1_DFT.InputTimeDomainSignal = InputSignal1;
            Signal2_DFT.InputTimeDomainSignal = InputSignal2;

            Signal1_DFT.Run();
            Signal2_DFT.Run();

            List<float> Signal_1_Amplitudes = new List<float>(Signal1_DFT.OutputFreqDomainSignal.FrequenciesAmplitudes);
            List<float> Signal_1_PhaseShifts1 = new List<float>(Signal1_DFT.OutputFreqDomainSignal.FrequenciesPhaseShifts);

            List<float> Signal_2_Amplitudes = new List<float>(Signal2_DFT.OutputFreqDomainSignal.FrequenciesAmplitudes);
            List<float> Signal_2_PhaseShifts = new List<float>(Signal2_DFT.OutputFreqDomainSignal.FrequenciesPhaseShifts);

            List<Complex> comp = new List<Complex>();

            for (int i = 0; i < Signal1_DFT.OutputFreqDomainSignal.Samples.Count; i++)
            {
                comp.Add(Complex.Multiply(Complex.FromPolarCoordinates(Signal_1_Amplitudes[i], Signal_1_PhaseShifts1[i]), Complex.FromPolarCoordinates(Signal_2_Amplitudes[i], Signal_2_PhaseShifts[i])));
                Amplitudes_List.Add((float)comp[i].Magnitude);
                PhaseShifts_List.Add((float)comp[i].Phase);
            }
            InverseDiscreteFourierTransform IDFT = new InverseDiscreteFourierTransform();
            IDFT.InputFreqDomainSignal = new Signal(false, new List<float>(), new List<float>(Amplitudes_List), new List<float>(PhaseShifts_List));
            IDFT.Run();
            OutputConvolvedSignal = IDFT.OutputTimeDomainSignal;
        }
    }
}