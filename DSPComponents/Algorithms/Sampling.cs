﻿using DSPAlgorithms.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSPAlgorithms.Algorithms
{
    public class Sampling : Algorithm
    {
        public int L { get; set; } //upsampling factor
        public int M { get; set; } //downsampling factor
        public Signal InputSignal { get; set; }
        public Signal OutputSignal { get; set; }

        public Signal OutputSignal12 { get; set; }



        public override void Run()
        {
            // throw new NotImplementedException();

           // Signal OutputSignal1 = new Signal();

            //creating lists to store the new indx and value of the sampel 
            List<int> index = new List<int>();
            List<float> value = new List<float>();

            List<int> index1 = new List<int>();
            List<float> value1 = new List<float>();
            int count = InputSignal.Samples.Count();

            //1'st upsampling [If M =0 & L ≠ 0 then up sample by L factor and then apply low pass filter.]

            if (M == 0 && L != 0)
            {
                int id = InputSignal.SamplesIndices[0];

                for (int i = 0; i < count; i++)
                {
                    value.Add(InputSignal.Samples[i]);
                    index.Add(id);
                    id++;
                    if (i < (count - 1))
                    {
                        for (int j = 0; j < L - 1; j++)
                        {
                            value.Add(0);
                            index.Add(id);
                            id++;

                        }
                    }

                }



                Signal OutputSignal1 = new Signal(value, index, false);
                // OutputSignal1 = new Signal();


                //create object from low pass filter 
                FIR Low_filter = new FIR();
                Low_filter.InputFilterType = DSPAlgorithms.DataStructures.FILTER_TYPES.LOW;
                Low_filter.InputFS = 8000;
                Low_filter.InputStopBandAttenuation = 50;
                Low_filter.InputCutOffFrequency = 1500;
                Low_filter.InputTransitionBand = 500;
                Low_filter.InputTimeDomainSignal = OutputSignal1;
                Low_filter.Run();
                OutputSignal = Low_filter.OutputYn;



            }

            //If M ≠ 0 & L = 0 then apply filter first and thereafter down sample by M factor.

            else if (M != 0 && L == 0)
            {

                // Signal OutputSignal1 ;
                // OutputSignal1 = new Signal();


                //create object from low pass filter 
                FIR Low_filter = new FIR();
                Low_filter.InputFilterType = DSPAlgorithms.DataStructures.FILTER_TYPES.LOW;
                Low_filter.InputFS = 8000;
                Low_filter.InputStopBandAttenuation = 50;
                Low_filter.InputCutOffFrequency = 1500;
                Low_filter.InputTransitionBand = 500;
                Low_filter.InputTimeDomainSignal = InputSignal;
                Low_filter.Run();
                //OutputSignal12 = Low_filter.OutputYn;

                int id = Low_filter.OutputYn.SamplesIndices[0];
                for (int i = 0; i < Low_filter.OutputYn.Samples.Count(); i += M)
                {

                    value.Add(Low_filter.OutputYn.Samples[i]);

                    index.Add(id);
                    id++;

                }

                OutputSignal = new Signal(value, index, false);
            }
            //If M ≠ 0 & L ≠ 0 this means we want to change sample rate by fraction. Thus, first up sample by L factor,
            //apply low pass filter and then down sample by M factor.

            else if (M != 0 && L !=0)
            {
                int id = InputSignal.SamplesIndices[0];

                for (int i = 0; i < count; i++)
                {
                    value.Add(InputSignal.Samples[i]);
                    index.Add(id);
                    id++;
                    if (i < (count - 1))
                    {
                        for (int j = 0; j < L - 1; j++)
                        {
                            value.Add(0);
                            index.Add(id);
                            id++;
                        }
                    }
                }
                Signal OutputSignal1 = new Signal(value, index, false);
                FIR Low_filter = new FIR();
                Low_filter.InputFilterType = DSPAlgorithms.DataStructures.FILTER_TYPES.LOW;
                Low_filter.InputFS = 8000;
                Low_filter.InputStopBandAttenuation = 50;
                Low_filter.InputCutOffFrequency = 1500;
                Low_filter.InputTransitionBand = 500;
                Low_filter.InputTimeDomainSignal = OutputSignal1;
                Low_filter.Run();

                int id1 = Low_filter.OutputYn.SamplesIndices[0];
                for (int i = 0; i < Low_filter.OutputYn.Samples.Count(); i += M)
                {

                    value1.Add(Low_filter.OutputYn.Samples[i]);

                    index1.Add(id1);
                    id1++;

                }

                OutputSignal = new Signal(value1, index1, false);
            }
        }
    }

}