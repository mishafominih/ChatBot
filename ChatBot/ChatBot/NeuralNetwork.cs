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

        public void Study(double d)
        {
            var input = new List<List<double>>();
            var output = new List<List<double>>();
            GetData(ref input, ref output);

            var start = new List<double>();
            var end = new List<double>();
            while (true)
            {
                var flag = false;
                for(int i = 0; i < input.Count; i++)
                {
                    start = input[i];
                    end = output[i];
                    var res = Run(start);
                    if(Compare(res, end) > d)
                    {
                        flag = true;
                        break;
                    }
                }
                if (flag)
                {
                    Learn(d, start, end);
                }
                else
                {
                    return;
                }
            }

            Learn(d, start, end);
        }

        private void Learn(double d, List<double> start, List<double> end)
        {
            var layers = Enumerable.Range(0, 5).Select(y => GetNewBranch()).ToList();
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
            }
            this.start = layers.First();
        }

        private void GetData(ref List<List<double>> start, ref List<List<double>> end)
        {
            var strs = File.ReadAllLines("learn.txt");
            start = strs
                .Select(x => x.Split(':')[0])
                .Select(x => x.Split(' '))
                .Select(x => x.Select(y => double.Parse(y)).ToList())
                .ToList();
            end = strs
                .Select(x => x.Split(':')[1])
                .Select(x => x.Split(' '))
                .Select(x => x.Select(y => double.Parse(y)).ToList())
                .ToList();
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
