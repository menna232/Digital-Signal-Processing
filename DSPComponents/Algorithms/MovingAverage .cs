using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class MovingAverage : Algorithm
    {
        public Signal InputSignal { get; set; }
        public int InputWindowSize { get; set; }
        public Signal OutputAverageSignal { get; set; }

        public override void Run()
        {
            List<float> Samples_values = new List<float>();
            // inputwindow size -1 3shan ngeb awl index n3rf ngeblo avrage 
            int average = ((InputWindowSize - 1) / 2);
            for (int i = average; i < InputSignal.Samples.Count - average; i++)
            {
                Samples_values.Add(InputSignal.Samples[i]);
            }

            OutputAverageSignal = new Signal(Samples_values, false);
        }
    }
}