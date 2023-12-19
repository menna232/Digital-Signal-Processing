using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;


namespace DSPAlgorithms.Algorithms
{
    public class DiscreteFourierTransform : Algorithm
    {
        public Signal InputTimeDomainSignal { get; set; }
        public float InputSamplingFrequency { get; set; }
        public Signal OutputFreqDomainSignal { get; set; }
        //public List<Complex> sampels { get; set; }


        public override void Run()
        {

            List<float> phase = new List<float>();
            List<float> amplitude = new List<float>();
          //  sampels = new List<Complex>();

            float phi = 0;

            List<float> X = InputTimeDomainSignal.Samples; //the sampels 
            float N = InputTimeDomainSignal.Samples.Count; // عدد السامبل 


            for (int k = 0; k < N; k++)
            {
                float re = 0;
                float im = 0;
                for (int n = 0; n < N; n++)
                {
                    phi = (2 * (float)Math.PI * k * n) / N;
                    re += X[n] * (float)Math.Cos(phi);
                    im += -X[n] * (float)Math.Sin(phi);
                }
                var ampp = Math.Sqrt(re * re + im * im);
                var phasse = Math.Atan2(im, re);

                phase.Add((float)phasse);
                amplitude.Add((float)ampp);
            }
            OutputFreqDomainSignal = new Signal(X, false);
            OutputFreqDomainSignal.FrequenciesAmplitudes = amplitude;
            OutputFreqDomainSignal.FrequenciesPhaseShifts = phase;
           // sampels.Add(new Complex((float)phasse, (float)phase));
        }
    }

}