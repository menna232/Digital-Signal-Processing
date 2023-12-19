using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class SinCos: Algorithm
    {
        public string type { get; set; }
        public float A { get; set; }
        public float PhaseShift { get; set; }
        public float AnalogFrequency { get; set; }
        public float SamplingFrequency { get; set; }
        public List<float> samples { get; set; }
        public override void Run()
        {
            samples = new List<float>();
            for (int i = 0; i < SamplingFrequency; i++)
            {
                if (type == "sin")
                {
                    //x(n)= A sin(2*pi*n*f/fs + phaseShift )
                    // where f =  AnalogFrequency  and fs = SamplingFrequency
                    samples.Add((float)(A* Math.Sin(2* Math.PI*i* AnalogFrequency/ SamplingFrequency+ PhaseShift)));

                }

                else if (type == "cos") 
                {

                    //x(n)= A cos(2*pi*n*f/fs + phaseShift )
                    // where f =  AnalogFrequency  and fs = SamplingFrequency
                    samples.Add((float)(A * Math.Cos(2 * Math.PI * i * AnalogFrequency / SamplingFrequency + PhaseShift)));


                }



            }
        }
    }
}
