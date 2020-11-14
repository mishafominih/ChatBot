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
		static Random rand = new Random();
		double[] weight;
		public Neuron(int count)
		{
			weight = new double[count];
			for(int i = 0; i < count; i++)
            {
				weight[i] = rand.Next();
            }
		}


	}
}
