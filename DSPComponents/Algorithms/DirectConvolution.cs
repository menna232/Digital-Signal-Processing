using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class DirectConvolution : Algorithm
    {
        public Signal InputSignal1 { get; set; }
        public Signal InputSignal2 { get; set; }
        public Signal OutputConvolvedSignal { get; set; }

        /// <summary>
        /// Convolved InputSignal1 (considered as X) with InputSignal2 (considered as H)
        /// </summary>
        public override void Run()
        {
            int m = InputSignal1.Samples.Count;
            int n = InputSignal2.Samples.Count;
            int k = m + n - 1;
            var convolution = new List<float>();

            float sum = 0;
            for (int i = 0; i < k; i++)
            {
                sum = 0;

                for (int j = 0; j < n; j++)
                {
                    if (i - j >= 0 && i - j < m)
                    {
                        sum += InputSignal1.Samples[i - j] * InputSignal2.Samples[j];
                    }

                }

                convolution.Add(sum);
            }
            var indexies = new List<int>();
            indexies.Add(InputSignal1.SamplesIndices[0]);
            for (int i = 1; i < k; i++)
            {
                indexies.Add(indexies[i - 1] + 1);
            }

            OutputConvolvedSignal = new Signal(convolution, indexies, false);
        }
    }
}

