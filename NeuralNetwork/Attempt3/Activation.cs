using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.Attempt3
{
    static class Activation
    {
        public static float Sigmoid(this float value)
        {
            return (float)(1 / (1 + Math.Exp(-value)));
        }

        public static float SigmoidDerivative(this float value)
        {
            return Sigmoid(value) * (1 - Sigmoid(value));
        }
    }
}
