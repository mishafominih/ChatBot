using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ChatBot
{
    public class Neuron
    {
        private static Random rand = new Random();
        public List<double> weights = new List<double>();
        public Neuron(int count)
        {
            for(int i = 0; i < count; i++)
            {
                weights.Add(rand.NextDouble() * 2 - 1);
            }
        }

        private Neuron(List<double> w)
        {
            weights = w;
        }

        public double Activate(List<double> values)
        {
            var z = 0.0;
            for(int i = 0; i < weights.Count; i++)
                z += values[i] * weights[i];
            double v = 1 / (1 + Math.Exp(-z));
            return v;
        }

        public Neuron GetClone()
        {
            var newWeight = new List<double>();
            foreach(var e in weights)
            {
                if(rand.Next(0, 2) == 0)
                {
                    double weight = e + 0.1 * rand.Next(1, 3) * Math.Pow(-1, rand.Next(1, 3));
                    newWeight.Add(Control(weight));
                }
                else
                {
                    newWeight.Add(e);
                }
            }
            return new Neuron(newWeight);
        }

        private double Control(double weight)
        {
            if (weight > 1) return 1;
            if (weight < -1) return -1;
            return weight;
        }
	}
}
