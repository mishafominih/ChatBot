using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBot
{
    public class Test
    {
        public Test()
        {
            var network = new NeuralNetwork(4, new int[] { 2, 3, 3, 1 });

            List<double> start = new List<double> { 1, 1 };
            List<double> end = new List<double> { 1 };

            network.Study(0.17);
            Console.WriteLine(network.Run(
                start).First());
        }
        private double Compare(List<double> first, List<double> second)
        {
            var d = 0.0;
            for (int i = 0; i < first.Count; i++)
            {
                d += Math.Abs(first[i] - second[i]);
            }
            return d;
        }
    }
}
