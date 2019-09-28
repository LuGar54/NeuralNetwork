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

            NumberNeuralNet num = new NumberNeuralNet();

            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine(num.Train());
            }


            Console.WriteLine("end");
            Console.ReadKey();
        }
    }
}
