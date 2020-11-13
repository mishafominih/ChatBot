using System.Collections;
using System.Xml.Serialization;
using System.Xml;
using System.IO;
using System;

namespace ChatBot
{
    class NeuralNetwork
    {
		[XmlArray("Neurons")]
		public Neuron[] neurons;

		static int l, r, c;
		/**
		 * Конструктор сети создает нейроны
		 */
		public NeuralNetwork(int len, int r, int c)
		{
			l = len;
			NeuralNetwork.r = r;
			NeuralNetwork.c = c;
			neurons = new Neuron[len];

			for (int i = 0; i < neurons.Length; i++)
				neurons[i] = new Neuron(r, c);
		}

		/**
		 * функция распознавания символа, используется для обучения
		 * @param input - входной вектор
		 * @return массив из нулей и единиц, ответы нейронов
		 */
		double[] handleHard(double[,] input)
		{
			double[] output = new double[neurons.Length];
			for (int i = 0; i < output.Length; i++)
				output[i] = neurons[i].transferHard(input);

			return output;
		}

		/**
		 * функция распознавания символа, используется для конечного ответа
		 * @param input -  входной вектор
		 * @return массив из вероятностей, ответы нейронов
		 */
		double[] handle(double[,] input)
		{
			double[] output = new double[neurons.Length];
			for (int i = 0; i < output.Length; i++)
				output[i] = neurons[i].transfer(input);

			return output;
		}

		/**
		 * ответ сети
		 * @param input - входной вектор
		 * @return индекс нейронов предназначенный для конкретного символа
		 */
		public double getAnswer(double[,] input)
		{
			double[] output = handle(input);
			int maxIndex = 0;
			for (int i = 1; i < output.Length; i++)
				if (output[i] > output[maxIndex])
					maxIndex = i;

			return maxIndex;
		}

		/**
		 * функция обучения
		 * @param input - входной вектор
		 * @param correctAnswer - правильный ответ
		 */
		public void study(double[,] input, int correctAnswer)
		{
			double[] correctOutput = new double[neurons.Length];
			correctOutput[correctAnswer] = 1;

			double[] output = handleHard(input);
			while (!compareArrays(correctOutput, output))
			{
				for (int i = 0; i < neurons.Length; i++)
				{
					double dif = correctOutput[i] - output[i];
					neurons[i].changeWeights(input, dif);
				}
				output = handleHard(input);
			}
		}

		/**
		 * сравнение двух вектор
		 * @param true - если массивы одинаковые, false - если нет
		 */
		bool compareArrays(double[] a, double[] b)
		{
			if (a.Length != b.Length)
				return false;

			for (int i = 0; i < a.Length; i++)
				if (Math.Abs(a[i] - b[i]) <= 0.00001)
					return false;

			return true;
		}

		void prepareForSerialization()
		{
			foreach (Neuron n in neurons)
				n.prepareForSerialization();
		}

		void onDeserialize()
		{
			foreach (Neuron n in neurons)
				n.onDeserialize();
		}

		public void saveLocal()
		{
			prepareForSerialization();

			XmlSerializer serializer = new XmlSerializer(this.GetType());
			FileStream stream = new FileStream("NeuralNetwork.txt", FileMode.Create);
			XmlWriter writer = new XmlTextWriter(stream, new System.Text.ASCIIEncoding());
			using (writer)
			{
				serializer.Serialize(writer, this);
			}
		}

		public static NeuralNetwork fromXml()
		{
			string xml = "";
			FileStream fStream = new FileStream("NeuralNetwork.txt",
												FileMode.OpenOrCreate);
			if (fStream.Length > 0)
			{
				byte[] tempData = new byte[fStream.Length];
				fStream.Read(tempData, 0, tempData.Length);

				xml = System.Text.Encoding.ASCII.GetString(tempData);
			}
			fStream.Close();

			if (string.IsNullOrEmpty(xml))
				return new NeuralNetwork(l, r, c);


			NeuralNetwork data;

			XmlSerializer serializer = new XmlSerializer(typeof(NeuralNetwork));
			using (TextReader reader = new StringReader(xml))
			{
				data = serializer.Deserialize(reader) as NeuralNetwork;
			}

			data.onDeserialize();

			return data;
		}
	}
}
