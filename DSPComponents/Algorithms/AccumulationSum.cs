using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;


namespace DSPAlgorithms.Algorithms
{
    public class AccumulationSum : Algorithm
    {
        public Signal InputSignal { get; set; }
        public Signal OutputSignal { get; set; }

        public override void Run()
        {
            List<float> temp = new List<float>();
            temp.Add(InputSignal.Samples[0]);
            for (int i = 1; i < InputSignal.Samples.Count(); i++)
            {
                
              
               
                temp.Add(InputSignal.Samples[i] + temp[i-1]);

            }

            OutputSignal = new Signal(temp, false);
        }
    }
}
