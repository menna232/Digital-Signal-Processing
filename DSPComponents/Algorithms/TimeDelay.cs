using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class TimeDelay : Algorithm
    {
        public Signal InputSignal1 { get; set; }
        public Signal InputSignal2 { get; set; }
        public float InputSamplingPeriod { get; set; }
        public float OutputTimeDelay { get; set; }

        public override void Run()
        {
            DirectCorrelation dc = new DirectCorrelation();
            dc.InputSignal1 = InputSignal1;
            dc.InputSignal2 = InputSignal2;
            dc.Run();
            int index = -1;
            float max = -100000;
           
            for (int i = 0; i < dc.OutputNormalizedCorrelation.Count; i++)
            {
                
                if (Math.Abs(dc.OutputNormalizedCorrelation[i]) > max)
                {
                    max = dc.OutputNormalizedCorrelation[i];
                    index = i;
                }
            
              OutputTimeDelay = index * InputSamplingPeriod;
          
            }
    
        }
   
    }

}