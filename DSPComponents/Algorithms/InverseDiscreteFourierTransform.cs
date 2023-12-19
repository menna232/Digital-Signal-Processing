using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class InverseDiscreteFourierTransform : Algorithm
    {
        public Signal InputFreqDomainSignal { get; set; }
        public Signal OutputTimeDomainSignal { get; set; }

        public override void Run()
        {


            List<float> phase = new List<float>();
            List<float> amp = new List<float>();
            float phi = 0;
            List<float> Xlist = new List<float>();
            List<float> Ylist = new List<float>();
            //Given sequence of Numbers
            //X feha amplitudes and y feha phaseshift  
            for (int i = 0; i < InputFreqDomainSignal.FrequenciesAmplitudes.Count; i++)
            {
                float res1, res2;
                float X = InputFreqDomainSignal.FrequenciesAmplitudes[i];
                float Y = InputFreqDomainSignal.FrequenciesPhaseShifts[i];
                res1 = (X * (float)Math.Cos(Y));
                res2 = (float)Math.Ceiling(X * (float)Math.Sin(Y));
                Xlist.Add(res1);
                Ylist.Add(res2);
            }

           

            float N;
            N = InputFreqDomainSignal.FrequenciesAmplitudes.Count;


            for (int k = 0; k < N; k++)
            {
                float result = 0;
                float im = 0;
                //The discrete Fourier transform, lists of Re & Im instead of a+ib
                // phi = 1/N * x(K) * e power((j*k*2*pi*n)/N)
                //where N = Sampels count 
                for (int n = 0; n < N; n++)
                {
                    phi = (2 * (float)Math.PI * k * n) / N;
                    if (Math.Cos(phi) != 0)
                        result += Xlist[n] * (float)Math.Cos(phi);
                    if (Math.Sin(phi) != 0)
                        result -= Ylist[n] * (float)Math.Sin(phi);
                    if (Math.Cos(phi) == 0 && Math.Sin(phi) == 0)
                        result += Xlist[n];
                }

                result = result / N;
                result = (float)Math.Round(result);
                amp.Add(result);
            }
            OutputTimeDomainSignal = new Signal(amp, false);


        }
    }
}
