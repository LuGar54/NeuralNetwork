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

        private Neuron from;
        private Neuron to;

        private double value;

        public double Error { get; set; }

        public Weight(Neuron from, Neuron to, double value)
        {
            this.from = from;
            from.rightWeights.Add(this);

            this.to = to;
            to.leftWeights.Add(this);

            this.value = value;
        }

        public double GetWeightedWeight()
        {
            return from.Value * value;
        }

        public void BackPropagate(double error)
        {
            Error = error * value;

            value += LEARN_RATE * error * from.Value;
        }

    }
}