using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.Attempt3
{
    class NetworkEvaluator
    {
        private const string DATA_FILENAME = "save.dat";

        private Network network;

        private List<ImageData> images;

        private BinaryFormatter formatter;

        public NetworkEvaluator()
        {
            formatter = new BinaryFormatter();

            if (File.Exists(DATA_FILENAME))
            {
                Load();
            }
            else
            {
                network = new Network();
            }

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

                if (network.GetAnswer() == image.number)
                {
                    successPerNumber.Item1[10]++;
                    successPerNumber.Item1[image.number]++;
                }

                successPerNumber.Item2[image.number]++;

                network.PropagateBack(image.expected);
            }

            Save();

            return successPerNumber;
        }

        private void Save()
        {
            try
            {
                FileStream writerFileStream = new FileStream(DATA_FILENAME, FileMode.Create, FileAccess.Write);

                formatter.Serialize(writerFileStream, network);

                writerFileStream.Close();

                Console.WriteLine("Saved successfully");
            }
            catch (Exception e)
            {
                Console.WriteLine("Saving error");
                Console.WriteLine(e.Message);
            }
        }

        public void Load()
        {
            try
            {
                FileStream readerFileStream = new FileStream(DATA_FILENAME, FileMode.Open, FileAccess.Read);

                network = (Network)formatter.Deserialize(readerFileStream);

                readerFileStream.Close();

                Console.WriteLine("Loaded successfully");
            }
            catch (Exception)
            {
                Console.WriteLine("Loading error");
            }
        }

    }
}