using DSPAlgorithms.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSPAlgorithms.Algorithms
{
    public class DCT : Algorithm
    {
        public Signal InputSignal { get; set; }
        public Signal OutputSignal { get; set; }

        public override void Run()
        {
            float a, y, result;
            List<float> output = new List<float>();
            float N = InputSignal.Samples.Count;
            for (int u = 0; u < N; u++)
            {
                
                result = 0;
               a = (float)Math.Sqrt(2.0 / N);   
                for (int k = 0; k < N; k++)
                {
                  result += InputSignal.Samples[k] * (float)Math.Cos((Math.PI/(4*N))*(2*u-1)*(2*k-1));
                }
                y = a * result;
                output.Add(y);
            }
            OutputSignal = new Signal(output, false);
        }
    }
}

