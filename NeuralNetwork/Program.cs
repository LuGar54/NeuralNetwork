using NeuralNetwork.Attempt3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork
{
    class Program
    {
        static void Main(string[] args)
        {
            //NumberRecognizer numberRecognizer = new NumberRecognizer();

            //numberRecognizer.Train(90);

            //Console.WriteLine("end");
            //Console.ReadKey();

            //NumberNeuralNet num = new NumberNeuralNet();

            //for (int i = 0; i < 100; i++)
            //{
            //    Console.WriteLine(num.Train());
            //}            

            NetworkEvaluator networkEvaluator = new NetworkEvaluator();

            for (int i = 0; i < 1000; i++)
            {
                Tuple<float[], int[]> results = networkEvaluator.Train();
                Console.WriteLine(i + " = Total: {0}%, 0: {1}%, 1: {2}%, 2: {3}%, 3: {4}%, 4: {5}%, 5: {6}%, 6: {7}%, 8: {9}%, 9: {10}%", results.Item1[10] / results.Item2[10] * 100, results.Item1[0] / results.Item2[0] * 100, results.Item1[1] / results.Item2[1] * 100, results.Item1[2] / results.Item2[2] * 100, results.Item1[3] / results.Item2[3] * 100, results.Item1[4] / results.Item2[4] * 100, results.Item1[5] / results.Item2[5] * 100, results.Item1[6] / results.Item2[6] * 100, results.Item1[7] / results.Item2[7] * 100, results.Item1[8] / results.Item2[8] * 100, results.Item1[9] / results.Item2[9] * 100);
            }

            Console.WriteLine("end");
            Console.ReadKey();
        }
    }
}
