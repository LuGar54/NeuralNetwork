using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;

namespace NeuralNetwork
{
    public class NumberRecognizer
    {
        NeuralNet net;


        List<float[]>[] trainingDataSet;

        int dataCount = 0;

        public NumberRecognizer()
        {
            net = new NeuralNet(784, 10, 10, 1);
            trainingDataSet = new List<float[]>[10];
            for (int i = 0; i < trainingDataSet.Length; i++)
            {
                trainingDataSet[i] = new List<float[]>();
                string[] filePaths = Directory.GetFiles("Images/" + i + "/", "*.jpg");
                foreach (var image in filePaths)
                {

                    Bitmap bitmap = new Bitmap(image);

                    float[] currentImage = new float[bitmap.Height * bitmap.Width];

                    for (int j = 0; j < bitmap.Width; j++)
                    {
                        for (int k = 0; k < bitmap.Height; k++)
                        {
                            Color currentPixel = bitmap.GetPixel(j, k);

                            currentImage[j * bitmap.Width + k] = ((currentPixel.R * 0.3f) + (currentPixel.G * 0.59f) + (currentPixel.B * 0.11f)) / 255f;
                        }
                    }

                    trainingDataSet[i].Add(currentImage);
                    ++dataCount;
                }
            }
        }

        public void Train(float trainingThreshold)
        {
            float successRate = 0;
            float oldSuccessRate = 0;
            int gen = 0;
            NeuralNet old = net.Clone();
            while ((successRate = GetSuccessRate()) < trainingThreshold)
            {

                if (oldSuccessRate < successRate)
                {

                    gen++;
                    Console.WriteLine("gen : " + gen + " % : " + successRate);

                    old = net.Clone();
                    net = net.Crossover(old);
                    oldSuccessRate = successRate;
                }

                net.Mutate(0.1f);
            }
        }

        public float GetSuccessRate()
        {
            float numberSuccess = 0;

            for (int i = 0; i < trainingDataSet.Length; i++)
            {
                foreach (var image in trainingDataSet[i])
                {
                    if (net.HighestOutput(image) == i)
                    {
                        numberSuccess++;
                    }
                }
            }

            return numberSuccess / dataCount * 100;
        }


    }
}