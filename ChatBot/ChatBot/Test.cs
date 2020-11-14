//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace ChatBot
//{
//    public class Test
//    {
//        public Test()
//        {
//            NeuralNetwork network = new NeuralNetwork(2, 1, 2);
//            network.study(new double[,] {
//                { 1, 1},
//            }, 1);
//            network.study(new double[,] {
//                { 0, 1},
//            }, 1);
//            network.study(new double[,] {
//                { 1, 0},
//            }, 1);
//            network.study(new double[,] {
//                { 0, 0},
//            }, 0);
//            Console.WriteLine(network.getAnswer(new double[,] {
//                { 0, 0},
//            }));
//        }
//    }
//}
