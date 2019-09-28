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


        List<ImageWithNumber> trainingDataSet;

        int dataCount = 0;

        public NumberRecognizer()
        {
            net = new NeuralNet(784, 16, 10, 1);
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
        }

        /// <summary>
        /// Trains up to the specified training threshold.
        /// TODO: On pourrait ajouter un threshold pour notre loss function.
        /// </summary>
        /// <param name="trainingThreshold">The training threshold.</param>
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

        /// <summary>
        /// Gets the success rate.
        /// TODO: À AMÉLIORER
        /// En gros ça c'est notre cost function.
        /// La cost function prend le résultat de plein de loss function, pis check le % de réussite.
        /// En combinaison avec le loss, ça permet de vérifier que notre backpropagation n'est pas trop spécifique, 
        /// donc qu'il n'essait pas d'avoir raison sur juste un type de données.
        /// </summary>
        /// <returns></returns>
        public float GetSuccessRate()
        {


            float numberSuccess = 0;

            foreach (var image in trainingDataSet)
            {
                //En gros ça c'est notre loss function, mais c'en est une très peu modulaire(AKA 2 réponses plutôt qu'un intervale (e(0,1) vs e[0,1])).
                //Donc 2 modèles différents pourraient donner le même loss, même si un des deux est techniquement meilleur.
                //TODO: À FAIRE
                if (net.HighestOutput(image.image) == image.number)
                {
                    numberSuccess++;
                }
            }

            return numberSuccess / dataCount * 100;
        }


    }
}