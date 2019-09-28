using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork
{
    struct ImageWithNumber
    {
        public float[] image;
        public int number;
        public float[] expected;

        public ImageWithNumber(float[] image, int number)
        {
            this.image = image;
            this.number = number;
            expected = new float[10];

            for (int i = 0; i < expected.Length; i++)
            {
                expected[i] = 0;
                if (i == number)
                {
                    expected[i] = 1;
                }
            }
        }
    }

    static class ListExtension
    {
        private static Random rng = new Random();

        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }

    class NumberNeuralNet
    {
        private const int NBR_INPUTS = 784;
        private const int NBR_HIDDEN_LAYERS = 1;
        private const int NBR_NODES_PER_HIDDEN_LAYER = 8;
        private const int NBR_OUTPUTS = 10;

        List<ImageWithNumber> trainingDataSet;

        int dataCount = 0;

        Layer[] layers;

        public NumberNeuralNet()
        {
            layers = new Layer[2];

            layers[0] = new Layer(NBR_INPUTS, NBR_NODES_PER_HIDDEN_LAYER);

            layers[1] = new Layer(NBR_NODES_PER_HIDDEN_LAYER, NBR_OUTPUTS);

            trainingDataSet = new List<ImageWithNumber>();
            for (int i = 0; i < 10; i++)
            {

                string[] filePaths = Directory.GetFiles("Images/" + i + "/", "*.jpg");
                foreach (var image in filePaths)
                {

                    Bitmap bitmap = new Bitmap(image);

                    ImageWithNumber currentImage = new ImageWithNumber(new float[bitmap.Height * bitmap.Width], i);

                    for (int j = 0; j < bitmap.Width; j++)
                    {
                        for (int k = 0; k < bitmap.Height; k++)
                        {
                            Color currentPixel = bitmap.GetPixel(j, k);

                            currentImage.image[j * bitmap.Width + k] = ((currentPixel.R * 0.3f) + (currentPixel.G * 0.59f) + (currentPixel.B * 0.11f)) / 255f;
                        }
                    }

                    trainingDataSet.Add(currentImage);
                    ++dataCount;
                }
            }

            trainingDataSet.Shuffle();
        }

        public float Train()
        {
            float numberSuccess = 0;

            foreach (var image in trainingDataSet)
            {

                int v = HighestOutput(image.image);
                //Console.WriteLine("num:" + image.number + "  // " + v);
                if (v == image.number)
                {
                    numberSuccess++;

                }
                Backpropagate(image.expected);
            }

            return numberSuccess / dataCount * 100;
        }

        public int HighestOutput(float[] inputsArr)
        {
            float[] outputs = FeedForward(inputsArr);

            int highestIndex = 0;
            float highestValue = float.MinValue;

            for (int i = 0; i < outputs.Length; i++)
            {
                if (outputs[i] > highestValue)
                {
                    highestValue = outputs[i];
                    highestIndex = i;
                }
            }

            return highestIndex;
        }

        private float[] FeedForward(float[] inputs)
        {
            layers[0].FeedForward(inputs);

            for (int i = 1; i < layers.Length; i++)
            {
                layers[i].FeedForward(layers[i - 1].outputs);
            }

            return layers[layers.Length - 1].outputs;
        }

        private void Backpropagate(float[] expected)
        {
            layers[0].BackpropagateHiddenLayer(layers[1].BackpropagateOutputLayer(expected));
        }

    }

    class Layer
    {
        float[] inputs;
        public float[] outputs;
        float bias;
        float[,] weights;

        static Random random = new Random();

        public Layer(int nbrInputs, int nbrOutputs)
        {
            inputs = new float[nbrInputs];
            outputs = new float[nbrOutputs];

            bias = (float)random.NextDouble() * 2 - 1;

            weights = new float[nbrInputs, nbrOutputs];

            for (int i = 0; i < nbrInputs; i++)
            {
                for (int j = 0; j < nbrOutputs; j++)
                {
                    weights[i, j] = (float)random.NextDouble() * 2 - 1;
                }
            }
        }

        public float[] FeedForward(float[] inputs)
        {
            this.inputs = inputs;
            for (int i = 0; i < outputs.Length; i++)
            {
                outputs[i] = 0;
                for (int j = 0; j < inputs.Length; j++)
                {
                    outputs[i] += inputs[j] * weights[j, i];
                }
                outputs[i] = Sigmoid(outputs[i] + bias);
            }
            return outputs;
        }

        public float BackpropagateOutputLayer(float[] expected)
        {
            //float[] error = new float[outputs.Length];
            //float[] gamma = new float[outputs.Length];

            ////float loss = LossFunction(outputs, expected);

            //for (int i = 0; i < error.Length; i++)
            //{
            //    error[i] = LossFunction(outputs, expected);
            //}

            //for (int i = 0; i < gamma.Length; i++)
            //{
            //    gamma[i] = error[i] * SigmoidDerivative(outputs[i]);
            //}

            //for (int i = 0; i < weights.GetLength(0); i++)
            //{
            //    for (int j = 0; j < weights.GetLength(1); j++)
            //    {
            //        weights[i, j] -= 0.005f * gamma[j] * inputs[i];
            //    }
            //}

            float error = LossFunction(outputs, expected);

            for (int i = 0; i < weights.GetLength(0); i++)
            {
                for (int j = 0; j < weights.GetLength(1); j++)
                {
                    weights[i, j] += error * inputs[i] * 0.5f;
                }
            }

            bias += error;

            return error;
        }

        public void BackpropagateHiddenLayer(float nextLayerLoss)
        {
            //TODO: comprendre ste marde là
            //https://www.youtube.com/watch?v=0e0z28wAWfg circa 9:14

            float[] gammas = new float[outputs.Length];

            for (int i = 0; i < weights.GetLength(0); i++)
            {
                for (int j = 0; j < weights.GetLength(1); j++)
                {
                    weights[i, j] += 0.25f * nextLayerLoss * SigmoidDerivative(outputs[j]);
                }
            }
        }

        public float LossFunction(float[] result, float[] expected)
        {
            //cross-entropy Marche? aucune idée je pense que oui, mai chu pu sur
            float dotResult = 0;

            for (int i = 0; i < result.Length; i++)
            {
                dotResult += SigmoidDerivative(outputs[i]) * (expected[i] - result[i]);
            }

            return dotResult;
        }

        public float Sigmoid(float value)
        {
            return (float)(1 / (1 + Math.Exp(-value)));
        }

        public float SigmoidDerivative(float value)
        {
            return Sigmoid(value) * (1 - Sigmoid(value));
        }
    }

    /// <summary>
    /// marche pas jpense
    /// mmm
    /// ouais c'est de la pisse
    /// ne pas utiliser
    /// </summary>
    class Neuron
    {
        float value;

        float[] backWeights;

        float[] fwdWeights;

        static Random random = new Random();

        public Neuron(float[] backWeights, int nbrOfFwdWeights)
        {

            if (backWeights == null)
            {
                //c'est input
            }
            else
            {
                this.backWeights = backWeights;
            }

            if (nbrOfFwdWeights > 0)
            {
                fwdWeights = new float[nbrOfFwdWeights];
            }
            else
            {
                //c'est output
            }


            for (int i = 0; i < fwdWeights.Length; i++)
            {
                fwdWeights[i] = (float)random.NextDouble() * 2 - 1;
            }
        }

        public float GetFwdWeights(int index)
        {
            return fwdWeights[index];
        }

        public float Activate()
        {
            return (float)Math.Tanh(value);
        }
    }
}