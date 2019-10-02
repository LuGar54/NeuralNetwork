using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.Attempt3
{
    [Serializable]
    class HiddenLayer : Layer
    {
        public HiddenLayer(Layer previousLayer, int nbrNeurons)
        {
            neurons = new HiddenNeuron[nbrNeurons];

            for (int i = 0; i < nbrNeurons; i++)
            {
                neurons[i] = new HiddenNeuron();
            }

            ConnectToLeft(previousLayer);
        }
    }
}