using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.Attempt3
{
    class Weight
    {
        private const double LEARN_RATE = 0.001;

        private static int weightCount = 0;

        private int id;

        private Neuron from;
        private Neuron to;

        private double value;

        public double Error { get; set; }

        public Weight(Neuron from, Neuron to, double value)
        {
            weightCount++;

            id = weightCount;

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