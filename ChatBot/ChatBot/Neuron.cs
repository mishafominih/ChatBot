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
		[XmlAttribute("weight")]
		public string data;

		[XmlIgnore]
		public int[,] weight; // веса нейронов

		[XmlIgnore]
		public int minimum = 50; // порог

		[XmlIgnore]
		public int row = 64, column = 64;

		/**
		 * Конструктор нейрона, создает веси и устанавливает случайные значения
		 */
		public Neuron(int r, int col)
		{
			row = r; column = col;
			weight = new int[row, column];
			randomizeWeights();
		}

		/**
		 * ответы нейронов, жесткая пороговая
		 * @param input - входной вектор
		 * @return ответ 0 или 1
		 */
		public int transferHard(int[,] input)
		{
			int power = 0;
			for (int r = 0; r < row; r++)
			{
				for (int c = 0; c < column; c++)
				{
					power += weight[r, c] * input[r, c];
				}
			}
			//Debug.Log("Power: " + power);
			return power >= minimum ? 1 : 0;
		}

		/**
		 * ответы нейронов с вероятностями
		 * @param input - входной вектор
		 * @return n вероятность
		 */
		public int transfer(int[,] input)
		{
			int power = 0;
			for (int r = 0; r < row; r++)
				for (int c = 0; c < column; c++)
					power += weight[r, c] * input[r, c];

			//Debug.Log("Power: " + power);
			return power;
		}

		/**
		 * устанавливает начальные произвольные значения весам 
		 */
		void randomizeWeights()
		{
			for (int r = 0; r < row; r++)
				for (int c = 0; c < column; c++)
					weight[r, c] = new Random().Next(0, 10);
		}

		/**
		 * изменяет веса нейронов
		 * @param input - входной вектор
		 * @param d - разница между выходом нейрона и нужным выходом
		 */
		public void changeWeights(int[,] input, int d)
		{
			for (int r = 0; r < row; r++)
				for (int c = 0; c < column; c++)
					weight[r, c] += d * input[r, c];
		}

		public void prepareForSerialization()
		{
			data = "";
			for (int r = 0; r < row; r++)
			{
				for (int c = 0; c < column; c++)
				{
					data += weight[r, c] + " ";
				}
				data += "\n";
			}
		}

		public void onDeserialize()
		{
			weight = new int[row, column];

			string[] rows = data.Split(new char[] { '\n' });
			for (int r = 0; r < row; r++)
			{
				string[] columns = rows[r].Split(new char[] { ' ' });
				for (int c = 0; c < column; c++)
				{
					weight[r, c] = int.Parse(columns[c]);
				}
			}
		}
	}
}
