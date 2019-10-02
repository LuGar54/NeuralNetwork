using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.Attempt3
{
    class InputLayer : Layer
    {
        public InputLayer(int nbrNeurons)
        {
            neurons = new InputNeuron[nbrNeurons + 1];

            for (int i = 0; i < nbrNeurons; i++)
            {
                neurons[i] = new InputNeuron();
            }

            neurons[nbrNeurons] = new BiasNeuron();
        }

        public void SetInput(double[] input)
        {
            if(input.Length != neurons.Length-1)
            {
                throw new Exception("welp pas meme longueur");
            }

            for (int i = 0; i < input.Length; i++)
            {
                (neurons[i] as InputNeuron).SetValue(input[i]);
            }
        }
    }
}