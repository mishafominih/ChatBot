using System.Collections;
using System.Xml.Serialization;
using System.Xml;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ChatBot
{
    class NeuralNetwork
    {
        public Layer start;
        int count;
        int[] lenghts;
		public NeuralNetwork(int count, int[] lenghts)
        {
            this.count = count;
            this.lenghts = lenghts;
            start = newLayer();
        }

        private Layer newLayer()
        {
            var res = new Layer(lenghts[0], new Layer(0, null));
            var prevLayer = res;
            for (int i = 1; i < count; i++)
            {
                var t = new Layer(lenghts[i], prevLayer);
                prevLayer.SetNext(t);
                prevLayer = t;
            }
            return res;
        }

        public List<double> Run(List<double> start)
        {
            return this.start.ActivateNeurons(start);
        }

        public void Study(List<double> start, List<double> end, double d)
        {
            var info = new List<double>();

            var layers = Enumerable.Range(0, 10).Select(x => newLayer()).ToList();
            double v = Compare(layers.First().ActivateNeurons(start), end);
            while (v >= d)
            {
                layers = layers
                    .SelectMany(x =>
                    {
                        var list = Enumerable.Range(0, 5).Select(y => GetNewBranch()).ToList();
                        list.Add(x);
                        return list;
                    })
                    .ToList();
                layers.Add(newLayer());
                layers = layers
                    .OrderBy(x => Compare(x.ActivateNeurons(start), end))
                    .Take(10)
                    .ToList();
                v = Compare(layers.First().ActivateNeurons(start), end);

                info.Clear();
                foreach(var e in layers)
                {
                    info.Add(Compare(e.ActivateNeurons(start), end));
                }
            }
            this.start = layers.First();
        }

        private double Compare(List<double> first, List<double> second)
        {
            var d = 0.0;
            for(int i = 0; i < first.Count; i++)
            {
                d += Math.Abs(first[i] - second[i]);
            }
            return d;
        }

        private Layer GetNewBranch()
        {
            return start.GetClone(new Layer(0, null));
        }
	}
}
