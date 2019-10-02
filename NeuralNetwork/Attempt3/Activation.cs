using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.Attempt3
{
    static class Activation
    {
        public static double Sigmoid(this double value)
        {
            return (float)(1 / (1 + Math.Exp(-value)));
        }

        public static double SigmoidDerivative(this double value)
        {
            return value * (1 - value);
        }
    }
}
