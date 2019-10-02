using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.Attempt3
{
    [Serializable]
    abstract class Neuron
    {
        public double Value { get; protected set; }
        public double Error { get; protected set; }

        readonly public List<Weight> leftWeights = new List<Weight>();
        readonly public List<Weight> rightWeights = new List<Weight>();

        public Neuron()
        {

        }

        public void FeedForward()
        {
            double sum = 0;

            foreach (Weight weight in leftWeights)
            {
                sum += weight.GetWeightedWeight();
            }

            Value = sum.Sigmoid();
        }

        public void BackPropagate()
        {
            Error = LossFunction() * Value.SigmoidDerivative();

            foreach (Weight weight in leftWeights)
            {
                weight.BackPropagate(Error);
            }
        }

        public abstract double LossFunction();

    }
}