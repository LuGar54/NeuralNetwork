using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.Attempt3
{
    class NetworkEvaluator
    {
        private Network network;

        private List<ImageData> images;

        public NetworkEvaluator()
        {
            network = new Network();

            images = ImageData.LoadImages("Images");
        }

        public Tuple<float[], int[]> Train()
        {
            Tuple<float[], int[]> successPerNumber = new Tuple<float[], int[]>(new float[11], new int[11]);

            images.Shuffle();

            foreach (var image in images)
            {
                network.FeedForward(image.image);

                successPerNumber.Item2[10]++;

                if(network.GetAnswer() == image.number)
                {
                    successPerNumber.Item1[10]++;
                    successPerNumber.Item1[image.number]++;
                }

                successPerNumber.Item2[image.number]++;

                network.PropagateBack(image.expected);
            }

            return successPerNumber;
        }

    }
}