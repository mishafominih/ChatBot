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

		/**
		 * Конструктор нейрона, создает веси и устанавливает случайные значения
		 */
		public Neuron()
		{

		}

		/**
		 * ответы нейронов, жесткая пороговая
		 * @param input - входной вектор
		 * @return ответ 0 или 1
		 */
		public double transferHard(double[,] input)
		{

		}

		/**
		 * ответы нейронов с вероятностями
		 * @param input - входной вектор
		 * @return n вероятность
		 */
		public double transfer(double[,] input)
		{

		}

		/**
		 * устанавливает начальные произвольные значения весам 
		 */
		void randomizeWeights()
		{

		}

		/**
		 * изменяет веса нейронов
		 * @param input - входной вектор
		 * @param d - разница между выходом нейрона и нужным выходом
		 */
		public void changeWeights(double[,] input, double d)
		{

		}

		public void prepareForSerialization()
		{

		}

		public void onDeserialize()
		{

		}
	}
}
