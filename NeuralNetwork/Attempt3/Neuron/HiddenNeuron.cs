using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.Attempt3
{
    [Serializable]
    class HiddenNeuron : Neuron
    {
        public HiddenNeuron()
            :base()
        {

        }

        public override double LossFunction()
        {
            double error = 0;

            foreach (Weight weight in rightWeights)
            {
                error += weight.Error;
            }

            return error;
        }
    }
}