using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class FastCorrelation : Algorithm
    {
        public Signal InputSignal1 { get; set; }
        public Signal InputSignal2 { get; set; }
        public List<float> OutputNonNormalizedCorrelation { get; set; }
        public List<float> OutputNormalizedCorrelation { get; set; }

        public override void Run()
        {
            List<Complex> comp = new List<Complex>();
            List<float> Amplitude_List = new List<float>();
            List<float> PhaseShift_List = new List<float>();

            if (InputSignal2 == null)
            {
                InputSignal2 = InputSignal1;

            }
            List<float> Signal1 = InputSignal1.Samples;
            List<float> Signal2 = InputSignal2.Samples;

            DiscreteFourierTransform Signal_1_DFT = new DiscreteFourierTransform();
            DiscreteFourierTransform Signal_2_DFT = new DiscreteFourierTransform();

            Signal_1_DFT.InputTimeDomainSignal = InputSignal2;
            Signal_2_DFT.InputTimeDomainSignal = InputSignal1;

            Signal_1_DFT.Run();
            Signal_2_DFT.Run();

            List<float> Signal1_Amplitudes = new List<float>(Signal_2_DFT.OutputFreqDomainSignal.FrequenciesAmplitudes);
            List<float> Signal1_PhaseShift = new List<float>(Signal_2_DFT.OutputFreqDomainSignal.FrequenciesPhaseShifts);

            List<float> Signal2_Amplitudes = new List<float>(Signal_1_DFT.OutputFreqDomainSignal.FrequenciesAmplitudes);
            List<float> Signal2_PhaseShifts = new List<float>(Signal_1_DFT.OutputFreqDomainSignal.FrequenciesPhaseShifts);

            for (int i = 0; i < Signal_2_DFT.OutputFreqDomainSignal.Samples.Count; i++)
            {
                comp.Add(Complex.Multiply(Complex.Conjugate(Complex.FromPolarCoordinates(Signal1_Amplitudes[i], Signal1_PhaseShift[i])), Complex.FromPolarCoordinates(Signal2_Amplitudes[i], Signal2_PhaseShifts[i])));
                Amplitude_List.Add((float)comp[i].Magnitude);
                PhaseShift_List.Add((float)comp[i].Phase);
            }

            float Normalized_sum = 0;
            float Signal_1_Sum = 0;
            float Signal_2_Sum = 0;
            for (int i = 0; i < Signal1.Count; i++)
            {
                Signal_1_Sum += Signal1[i] * Signal1[i];
                Signal_2_Sum += Signal2[i] * Signal2[i];
            }
            Normalized_sum += Signal_1_Sum * Signal_2_Sum;
            Normalized_sum = (float)Math.Sqrt(Normalized_sum);
            Normalized_sum /= Signal1.Count;

            InverseDiscreteFourierTransform IDFT = new InverseDiscreteFourierTransform();
            IDFT.InputFreqDomainSignal = new Signal(false, new List<float>(), new List<float>(Amplitude_List), new List<float>(PhaseShift_List));
            IDFT.Run();

            OutputNormalizedCorrelation = new List<float>();

            for (int i = 0; i < Signal_1_DFT.OutputFreqDomainSignal.Samples.Count; i++)
            {
                IDFT.OutputTimeDomainSignal.Samples[i] /= Signal_1_DFT.OutputFreqDomainSignal.Samples.Count;
                OutputNormalizedCorrelation.Add(IDFT.OutputTimeDomainSignal.Samples[i] / Normalized_sum);
            }
            OutputNonNormalizedCorrelation = IDFT.OutputTimeDomainSignal.Samples;

        }
    }
}