using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.Attempt3
{
    [Serializable]
    class Weight
    {
        private const double LEARN_RATE = 0.001;

        private Neuron left;
        private Neuron right;

        private double value;

        public double Error { get; set; }

        public Weight(Neuron left, Neuron right, double value)
        {
            this.left = left;
            left.rightWeights.Add(this);

            this.right = right;
            right.leftWeights.Add(this);

            this.value = value;
        }

        public double GetWeightedWeight()
        {
            return left.Value * value;
        }

        public void BackPropagate(double error)
        {
            Error = error * value;

            value += LEARN_RATE * error * left.Value;
        }

    }
}