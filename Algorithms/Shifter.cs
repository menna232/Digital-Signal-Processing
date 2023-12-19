using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class Shifter : Algorithm
    {
        public Signal InputSignal { get; set; }
        public int ShiftingValue { get; set; }
        public Signal OutputShiftedSignal { get; set; }

        public override void Run()
        {
            //throw new NotImplementedException();
            List<int> val = new List<int>();
            List<float> res = new List<float>();
            for (int i = 0; i < InputSignal.Samples.Count(); i++)
            {
                val.Add(InputSignal.SamplesIndices[i] + ShiftingValue);
                res.Add(InputSignal.Samples[i]);

            }
            OutputShiftedSignal = new Signal(res, val, true);
        }
    }
}
