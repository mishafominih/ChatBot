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
		Neuron[] startLayer;
		List<Neuron[]> hideLayer;
		Neuron[] endLayer;
		public NeuralNetwork(int StartSize, int countHide, int[] arrCountHide, int EndSize)
		{
			startLayer = new Neuron[StartSize];
			for(int i = 0; i < countHide; i++)
            {
				hideLayer.Add(new Neuron[arrCountHide[i]]);
            }
			endLayer = new Neuron[EndSize];
			Initialise();
		}

		private void Initialise()
        {
			for(int i = 0; i < startLayer.Length; i++)
            {
				startLayer[i] = new Neuron(0);
			}
			for(int j = 0; j < hideLayer.Count; j++)
            {
				for (int i = 0; i < hideLayer[j].Length; i++)
				{
					if(i == 0)
						hideLayer[j][i] = new Neuron(startLayer.Length);
					else
						hideLayer[j][i] = new Neuron(hideLayer[j].Length);
				}
			}
			for (int i = 0; i < endLayer.Length; i++)
			{
				endLayer[i] = new Neuron(hideLayer.Last().Length);
			}
		}
	}
}
