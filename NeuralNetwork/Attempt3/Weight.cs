using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.Attempt3
{
    class Weight
    {
        private Neuron from;
        private Neuron to;

        float value;

        public Weight(Neuron from, Neuron to, float value)
        {
            this.from = from;
            this.to = to;
            this.value = value;
        }
    }
}