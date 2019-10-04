using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.Attempt3
{
    [Serializable]
    class InputNeuron : Neuron
    {
        public InputNeuron()
        {

        }

        public virtual void SetValue(double value)
        {
            Value = value;
        }

        public override double LossFunction()
        {
            //rien
            return 0;
        }
    }
}