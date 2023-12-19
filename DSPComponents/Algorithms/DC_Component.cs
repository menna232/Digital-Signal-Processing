using DSPAlgorithms.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSPAlgorithms.Algorithms
{
    public class DC_Component: Algorithm
    {
        public Signal InputSignal { get; set; }
        public Signal OutputSignal { get; set; }

        public override void Run()
        {
            float sum = 0, res = 0, avrage, count = InputSignal.Samples.Count;
            List<float> output = new List<float>();
            for (int i = 0; i < count; i++)
            {
                sum += InputSignal.Samples[i];

            }
            avrage = sum / count;

            for (int i = 0; i < count; i++)
            {
                res = InputSignal.Samples[i] - avrage;
                output.Add(res);

            }

            OutputSignal = new Signal(output, false);
        }
    }
}
