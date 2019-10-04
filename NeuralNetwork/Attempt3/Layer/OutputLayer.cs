using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.Attempt3
{
    [Serializable]
    class OutputLayer : Layer
    {
        public OutputLayer(Layer lastHiddenLayer, int nbrNeurons)
        {
            InitiateNeurons(nbrNeurons);

            ConnectToLeft(lastHiddenLayer);
        }

        private void InitiateNeurons(int nbrNeurons)
        {
            neurons = new OutputNeuron[nbrNeurons];

            for (int i = 0; i < nbrNeurons; i++)
            {
                neurons[i] = new OutputNeuron();
            }
        }

        public void SetTargetOutput(double[] targets)
        {
            if(neurons.Length != targets.Length)
            {
                throw new Exception("rip length");
            }

            for (int i = 0; i < neurons.Length; i++)
            {
                (neurons[i] as OutputNeuron).TargetValue = targets[i];
            }
        }

        public int GetHighest()
        {
            int index = -1;
            double highestValue = double.MinValue;

            for (int i = 0; i < neurons.Length; i++)
            {
                if(neurons[i].Value > highestValue)
                {
                    index = i;
                    highestValue = neurons[i].Value;
                }
            }

            return index;
        }
    }
}