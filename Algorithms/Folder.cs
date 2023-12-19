using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class Folder : Algorithm
    {
        public Signal InputSignal { get; set; }
        public Signal OutputFoldedSignal { get; set; }

        public override void Run()
        {

            // throw new NotImplementedException();
            List<float> val = new List<float>();

            List<int> index = new List<int>();
            for (int i = 0; i < InputSignal.Samples.Count; i++)
            {
                int x = (InputSignal.Samples.Count) - i - 1;
                val.Add(InputSignal.Samples[x]);
                index.Add(-1 * InputSignal.SamplesIndices[InputSignal.Samples.Count - i - 1]);
            }
            /*  for (int i = 0; i < InputSignal.Samples.Count; i++)
              {
                  res.Add(InputSignal.Samples[-i]);
              }*/
            OutputFoldedSignal = new Signal(val, index, false);
        }
    }
}
