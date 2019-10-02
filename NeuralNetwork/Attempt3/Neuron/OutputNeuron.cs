using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.Attempt3
{
    [Serializable]
    class OutputNeuron : Neuron
    {
        public double TargetValue { get; set; }

        public OutputNeuron() 
            : base()
        {

        }

        public override double LossFunction()
        {
            return TargetValue - Value;
        }
    }
}
