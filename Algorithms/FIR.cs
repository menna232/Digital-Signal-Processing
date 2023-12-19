using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class FIR : Algorithm
    {
        public Signal InputTimeDomainSignal { get; set; }
        public FILTER_TYPES InputFilterType { get; set; }
        public float InputFS { get; set; }
        public float? InputCutOffFrequency { get; set; }
        public float? InputF1 { get; set; }
        public float? InputF2 { get; set; }
        public float InputStopBandAttenuation { get; set; }
        public float InputTransitionBand { get; set; }
        public Signal OutputHn { get; set; }
        public Signal OutputYn { get; set; }

        public override void Run()
        {
            //
            float deltaF = InputTransitionBand / InputFS;
            int N = 0;
            float n;

            List<float> hn = new List<float>();
            List<int> indicies = new List<int>();
            List<float> samples = new List<float>();
            List<float> w = new List<float>();

            if (InputFilterType == FILTER_TYPES.LOW)
            {
                float fCdash = (float)((InputCutOffFrequency + (InputTransitionBand / 2)) / InputFS);
                if (InputStopBandAttenuation <= 21)
                {
                    N = (int)Math.Ceiling(0.9 / deltaF);
                    n = (int)((N - 1) / 2);
                    if (N % 2 == 0)
                        N++;

                    for (int i = (int)-(n); i <= n; i++)
                    {
                        w.Add(1);
                        if (i == 0)
                        {
                            hn.Add(2 * fCdash);
                        }
                        else
                            hn.Add((float)(2 * fCdash * Math.Sin(i * 2 * Math.PI * fCdash) / (i * 2 * Math.PI * fCdash)));
                        indicies.Add(i);

                    }

                }
                else if (InputStopBandAttenuation > 21 && InputStopBandAttenuation <= 44)
                {
                    N = (int)Math.Ceiling(3.1 / deltaF);
                    if (N % 2 == 0)
                        N++;
                    n = (int)(N / 2);
                    for (int i = (int)-(n); i <= n; i++)
                    {
                        w.Add((float)(0.5 + 0.5 * Math.Cos((2 * Math.PI * i) / N)));
                        if (i == 0)
                        {
                            hn.Add(2 * fCdash);
                        }
                        else
                            hn.Add((float)(2 * fCdash * Math.Sin(i * 2 * Math.PI * fCdash) / (i * 2 * Math.PI * fCdash)));
                        indicies.Add(i);
                    }

                }

                else if (InputStopBandAttenuation > 41 && InputStopBandAttenuation <= 53)
                {
                    N = (int)Math.Ceiling(3.3 / deltaF);
                    n = (int)((N - 1) / 2);
                    if (N % 2 == 0)
                        N++;
                    for (int i = (int)-(n); i <= n; i++)
                    {
                        w.Add((float)(0.54 + 0.46 * Math.Cos((2 * Math.PI * i) / N)));
                        if (i == 0)
                        {
                            hn.Add(2 * fCdash);
                        }
                        else
                            hn.Add((float)(2 * fCdash * Math.Sin(i * 2 * Math.PI * fCdash) / (i * 2 * Math.PI * fCdash)));
                        indicies.Add(i);
                    }
                }
                else if (InputStopBandAttenuation > 57 && InputStopBandAttenuation <= 74)
                {
                    N = (int)Math.Ceiling(5.5 / deltaF);
                    n = (int)((N - 1) / 2);
                    for (int i = (int)-(n); i <= n; i++)
                    {
                        w.Add((float)(0.42 + 0.5 * Math.Cos((2 * Math.PI * i) / (N - 1)) + 0.08 * Math.Cos((4 * Math.PI * i) / (N - 1))));
                        if (i == 0)
                        {
                            hn.Add(2 * fCdash);
                        }
                        else
                         hn.Add((float)(2 * fCdash * Math.Sin(i * 2 * Math.PI * fCdash) / (i * 2 * Math.PI * fCdash)));
                        indicies.Add(i);
                    }
                }
            }
            else if (InputFilterType == FILTER_TYPES.HIGH)
            {
                //hntr7 fe el hight pass
                // (fc - (deltaF /2))/fs
                float fCdash = (float)((InputCutOffFrequency - (InputTransitionBand / 2)) / InputFS);
              
                if (InputStopBandAttenuation <= 21)
                {
                    N = (int)Math.Ceiling(0.9 / deltaF);
                    n = (int)((N - 1) / 2);
                    if (N % 2 == 0)
                        N++;
                    for (int i = (int)-(n); i <= n; i++)
                    {
                        w.Add(1);
                        if (i == 0)
                        {
                            hn.Add(1 - 2 * fCdash);
                        }
                        else
                            hn.Add((float)(-2 * fCdash * Math.Sin(i * 2 * Math.PI * fCdash) / (i * 2 * Math.PI * fCdash)));
                        indicies.Add(i);

                    }

                }
                else if (InputStopBandAttenuation > 21 && InputStopBandAttenuation <= 44)
                {
                    N = (int)Math.Ceiling(3.1 / deltaF);
                    n = (int)(N / 2);
                    if (N % 2 == 0)
                        N++;
                    for (int i = (int)-(n); i <= n; i++)
                    {
                        w.Add((float)(0.5 + 0.5 * Math.Cos((2 * Math.PI * i) / N)));
                        if (i == 0)
                        {
                            hn.Add(1 - 2 * fCdash);
                        }
                        else
                            hn.Add((float)(-2 * fCdash * Math.Sin(i * 2 * Math.PI * fCdash) / (i * 2 * Math.PI * fCdash)));
                        indicies.Add(i);
                    }

                }

                else if (InputStopBandAttenuation > 41 && InputStopBandAttenuation <= 53)
                {
                    N = (int)Math.Ceiling(3.3 / deltaF);
                    n = (int)((N - 1) / 2);
                    if (N % 2 == 0)
                        N++;
                    for (int i = (int)-(n); i <= n; i++)
                    {
                        w.Add((float)(0.54 + 0.46 * Math.Cos((2 * Math.PI * i) / N)));
                        if (i == 0)
                        {
                            hn.Add(1 - 2 * fCdash);
                        }
                        else
                            hn.Add((float)(-2 * fCdash * Math.Sin(i * 2 * Math.PI * fCdash) / (i * 2 * Math.PI * fCdash)));
                        indicies.Add(i);
                    }
                }
                else if (InputStopBandAttenuation > 57 && InputStopBandAttenuation <= 74)
                {
                    N = (int)Math.Ceiling(5.5 / deltaF);
                    if (N % 2 == 0)
                        N++;
                    n = (int)((N - 1) / 2);

                    for (int i = (int)-(n); i <= n; i++)
                    {
                        w.Add((float)(0.42 + 0.5 * Math.Cos((2 * Math.PI * i) / (N - 1)) + 0.08 * Math.Cos((4 * Math.PI * i) / (N - 1))));
                        if (i == 0)
                        {
                            hn.Add(1 - 2 * fCdash);
                        }
                        else
                            hn.Add((float)(-2 * fCdash * Math.Sin(i * 2 * Math.PI * fCdash) / (i * 2 * Math.PI * fCdash)));
                        indicies.Add(i);

                    }

                }
            }

            else if (InputFilterType == FILTER_TYPES.BAND_PASS)
            {
                float f1 = (((float)InputF1 - (InputTransitionBand / 2)) / InputFS);
                float f2 = (((float)InputF2 + (InputTransitionBand / 2)) / InputFS);
                if (InputStopBandAttenuation <= 21)
                {
                    N = (int)Math.Ceiling(0.9 / deltaF);
                    n = (int)((N - 1) / 2);
                    if (N % 2 == 0)
                        N++;
                    for (int i = (int)-(n); i <= n; i++)
                    {
                        w.Add(1);
                        if (i == 0)
                        {
                            hn.Add((float)(2 * (f2 - f1)));
                        }
                        else
                            hn.Add((float)(2 * f2 * Math.Sin((i * 2 * Math.PI * f2)) / (i * 2 * Math.PI * f2) - 2 * f1 * Math.Sin((double)(i * 2 * Math.PI * f1)) / (i * 2 * Math.PI * f1)));
                        indicies.Add(i);

                    }

                }
                else if (InputStopBandAttenuation > 21 && InputStopBandAttenuation <= 44)
                {
                    N = (int)Math.Ceiling(3.1 / deltaF);
                    n = (int)(N / 2);
                    if (N % 2 == 0)
                        N++;
                    for (int i = (int)-(n); i <= n; i++)
                    {
                        w.Add((float)(0.5 + 0.5 * Math.Cos((2 * Math.PI * i) / N)));
                        if (i == 0)
                        {
                            hn.Add((float)(2 * (f2 - f1)));
                        }
                        else
                            hn.Add((float)(2 * f2 * Math.Sin((i * 2 * Math.PI * f2)) / (i * 2 * Math.PI * f2) - 2 * f1 * Math.Sin((double)(i * 2 * Math.PI * f1)) / (i * 2 * Math.PI * f1)));
                        indicies.Add(i);
                    }

                }

                else if (InputStopBandAttenuation > 41 && InputStopBandAttenuation <= 53)
                {
                    N = (int)Math.Ceiling(3.3 / deltaF);
                    n = (int)((N - 1) / 2);
                    if (N % 2 == 0)
                        N++;
                    for (int i = (int)-(n); i <= n; i++)
                    {
                        w.Add((float)(0.54 + 0.46 * Math.Cos((2 * Math.PI * i) / N)));
                        if (i == 0)
                        {
                            hn.Add((float)(2 * (f2 - f1)));
                        }
                        else
                            hn.Add((float)(2 * f2 * Math.Sin((i * 2 * Math.PI * f2)) / (i * 2 * Math.PI * f2) - 2 * f1 * Math.Sin((i * 2 * Math.PI * f1)) / (i * 2 * Math.PI * f1)));
                        indicies.Add(i);
                    }
                }
                else if (InputStopBandAttenuation > 57 && InputStopBandAttenuation <= 74)
                {
                    N = (int)Math.Ceiling(5.5 / deltaF);
                    if (N % 2 == 0)
                        N++;
                    n = (int)((N - 1) / 2);

                    for (int i = (int)-(n); i <= n; i++)
                    {
                        w.Add((float)(0.42 + 0.5 * Math.Cos((2 * Math.PI * i) / (N - 1)) + 0.08 * Math.Cos((4 * Math.PI * i) / (N - 1))));
                        if (i == 0)
                        {
                            hn.Add((float)(2 * (f2 - f1)));
                        }
                        else
                            hn.Add((float)((2 * f2 * Math.Sin((i * 2 * Math.PI * f2)) / (i * 2 * Math.PI * f2)) - (2 * f1 * Math.Sin((i * 2 * Math.PI * f1)) / (i * 2 * Math.PI * f1))));
                        indicies.Add(i);

                    }

                }
            }

            
            else if (InputFilterType == FILTER_TYPES.BAND_STOP)
            {
                float f1 = (float)((InputF1 + (InputTransitionBand / 2)) / InputFS);
                float f2 = (float)((InputF2 - (InputTransitionBand / 2)) / InputFS);
                if (InputStopBandAttenuation <= 21)
                {
                    N = (int)Math.Ceiling(0.9 / deltaF);
                    n = (int)((N - 1) / 2);
                    if (N % 2 == 0)
                        N++;
                    for (int i = (int)-(n); i <= n; i++)
                    {
                        w.Add(1);
                        if (i == 0)
                        {
                            hn.Add((float)(1 - 2 * (f2 - f1)));
                        }
                        else
                         hn.Add((float)(-2 * f2 * Math.Sin((i * 2 * Math.PI * f2)) / (i * 2 * Math.PI * f2) + 2 * f1 * Math.Sin((double)(i * 2 * Math.PI * f1)) / (i * 2 * Math.PI * f1)));
                        indicies.Add(i);

                    }

                }
                else if (InputStopBandAttenuation > 21 && InputStopBandAttenuation <= 44)
                {
                    N = (int)Math.Ceiling(3.1 / deltaF);
                    n = (int)(N / 2);
                    if (N % 2 == 0)
                        N++;
                    for (int i = (int)-(n); i <= n; i++)
                    {
                        w.Add((float)(0.5 + 0.5 * Math.Cos((2 * Math.PI * i) / N)));
                        if (i == 0)
                        {
                            hn.Add((float)(1 - 2 * (f2 - f1)));
                        }
                        else
                            hn.Add((float)(-2 * f2 * Math.Sin((i * 2 * Math.PI * f2)) / (i * 2 * Math.PI * f2) + 2 * f1 * Math.Sin((double)(i * 2 * Math.PI * f1)) / (i * 2 * Math.PI * f1)));
                        indicies.Add(i);
                    }

                }

                else if (InputStopBandAttenuation > 41 && InputStopBandAttenuation <= 53)
                {
                    N = (int)Math.Ceiling(3.3 / deltaF);
                    n = (int)((N - 1) / 2);
                    if (N % 2 == 0)
                        N++;
                    for (int i = (int)-(n); i <= n; i++)
                    {
                        w.Add((float)(0.54 + 0.46 * Math.Cos((2 * Math.PI * i) / N)));
                        if (i == 0)
                        {
                            hn.Add((float)(1 - 2 * (f2 - f1)));
                        }
                        else
                            hn.Add((float)(-2 * f2 * Math.Sin((i * 2 * Math.PI * f2)) / (i * 2 * Math.PI * f2) + 2 * f1 * Math.Sin((i * 2 * Math.PI * f1)) / (i * 2 * Math.PI * f1)));
                        indicies.Add(i);
                    }
                }
                else if (InputStopBandAttenuation > 57 && InputStopBandAttenuation <= 74)
                {
                    N = (int)Math.Ceiling(5.5 / deltaF);
                    if (N % 2 == 0)
                        N++;
                    n = (int)((N - 1) / 2);

                    for (int i = (int)-(n); i <= n; i++)
                    {
                        w.Add((float)(0.42 + 0.5 * Math.Cos((2 * Math.PI * i) / (N - 1)) + 0.08 * Math.Cos((4 * Math.PI * i) / (N - 1))));
                        if (i == 0)
                        {
                            hn.Add((float)(1 - 2 * (f2 - f1)));
                        }
                        else
                         hn.Add((float)((-2 * f2 * Math.Sin((i * 2 * Math.PI * f2)) / (i * 2 * Math.PI * f2)) + (2 * f1 * Math.Sin((i * 2 * Math.PI * f1)) / (i * 2 * Math.PI * f1))));
                        indicies.Add(i);

                    }

                }
            }
            
            for (int i = 0; i < N; i++)
            {
                samples.Add((float)(w[i] * hn[i]));
                Console.WriteLine(w[i]);
                Console.WriteLine(hn[i]);
            }
            OutputHn = new Signal(samples, indicies, false);
            DirectConvolution con = new DirectConvolution();
            con.InputSignal1 = OutputHn;
            con.InputSignal2 = InputTimeDomainSignal;
            con.Run();
            OutputYn = con.OutputConvolvedSignal;








        }
    }
}
