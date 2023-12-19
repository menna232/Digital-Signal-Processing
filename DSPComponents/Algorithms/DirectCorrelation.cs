using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class DirectCorrelation : Algorithm
    {
        public Signal InputSignal1 { get; set; }
        public Signal InputSignal2 { get; set; }
        public List<float> OutputNonNormalizedCorrelation { get; set; }
        public List<float> OutputNormalizedCorrelation { get; set; }

        public override void Run()
        {
            OutputNonNormalizedCorrelation = new List<float>();
            OutputNormalizedCorrelation = new List<float>();

            List<float> signal2 = new List<float> { };

            //auto if the 2nd signal = 0
            // fe hagten Periodic and non-Periodic
            // in case of Periodic we shif with the samee values of the signal 
            //in case of non-Periodic we shif with 0

            //AUTO

            if (InputSignal2 == null)
            {
                //initialize the list 
                for (int i = 0; i < InputSignal1.Samples.Count; i++)
                {
                    signal2.Add(InputSignal1.Samples[i]);

                }
                float sqrsum1 = 0;
                //summation of signla with it self 
                for (int i = 0; i < InputSignal1.Samples.Count; i++)
                {
                    sqrsum1 += InputSignal1.Samples[i] * signal2[i];

                }
                //correaltion computation 
                double P = sqrsum1 / InputSignal1.Samples.Count;

                for (int i = 0; i < InputSignal1.Samples.Count; i++)
                {
                    float temp = 0;
                    for (int j = 0; j < InputSignal1.Samples.Count; j++)
                    {
                        if (InputSignal1.Periodic)
                        {
                            temp += (InputSignal1.Samples[j] * signal2[(i + j) % InputSignal1.Samples.Count]);

                        }
                        else
                        {
                            if (i + j >= InputSignal1.Samples.Count)
                            {
                                temp += 0;
                            }
                            else
                            {
                                temp += (InputSignal1.Samples[j] * signal2[i + j]);
                            }

                        }
                    }

                    OutputNonNormalizedCorrelation.Add(temp / InputSignal1.Samples.Count);
                    OutputNormalizedCorrelation.Add((float)((temp / InputSignal1.Samples.Count) / P));

                }
            }
            //crros correaltion 
            else
            {
                //initialize the  list with the 2nd signal values 
                for (int i = 0; i < InputSignal1.Samples.Count; i++)
                {
                    signal2.Add(InputSignal2.Samples[i]);

                }
                //summation on signal squer
                float sqrsum1 = 0;
                float sqrsum2 = 0;
                for (int i = 0; i < InputSignal1.Samples.Count; i++)
                {
                    sqrsum1 += InputSignal1.Samples[i] * InputSignal1.Samples[i];
                    sqrsum2 += signal2[i] * signal2[i];

                }
                //applay the rule 
                double P = (Math.Sqrt(sqrsum1 * sqrsum2)) / InputSignal1.Samples.Count;

                for (int i = 0; i < InputSignal1.Samples.Count; i++)
                {
                    float temp = 0;
                    for (int j = 0; j < InputSignal1.Samples.Count; j++)
                    {
                        if (InputSignal2.Periodic)
                        {
                            temp += (InputSignal1.Samples[j] * signal2[(i + j) % InputSignal1.Samples.Count]);
                        }
                        else
                        {
                            if (i + j >= InputSignal1.Samples.Count)
                            {
                                temp += 0;
                            }
                            else
                            {
                                temp += (InputSignal1.Samples[j] * signal2[i + j]);
                            }

                        }
                    }
                    OutputNonNormalizedCorrelation.Add(temp / InputSignal1.Samples.Count);
                    OutputNormalizedCorrelation.Add((float)((temp / InputSignal1.Samples.Count) / P));

                }
            }
        }
    }
}