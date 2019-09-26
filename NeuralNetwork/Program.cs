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
            NumberRecognizer numberRecognizer = new NumberRecognizer();

            numberRecognizer.Train(50);

            Console.WriteLine("end");
            Console.ReadKey();
        }
    }
}
