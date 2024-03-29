﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.Attempt3
{
    [Serializable]
    abstract class Layer
    {
        protected Neuron[] neurons;

        public static Random random = new Random();

        public Layer()
        {

        }

        public void ConnectToLeft(Layer previousLayer)
        {
            if (previousLayer != null)
            {
                for (int i = 0; i < previousLayer.neurons.Length; i++)
                {
                    for (int j = 0; j < neurons.Length; j++)
                    {
                        new Weight(previousLayer.neurons[i], neurons[j], (random.NextDouble() * 2) - 1);
                    }

                }
            }
        }

        public void FeedForward()
        {
            for (int i = 0; i < neurons.Length; i++)
            {
                neurons[i].FeedForward();
            }
        }

        public void BackPropagate()
        {
            for (int i = 0; i < neurons.Length; i++)
            {
                neurons[i].BackPropagate();
            }
        }
    }
}