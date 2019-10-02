using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.Attempt3
{
    class ImageData
    {
        public double[] image;
        public int number;
        public double[] expected;

        public ImageData(Bitmap bitmap, int number)
        {
            image = new double[bitmap.Width * bitmap.Height];

            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    Color currentPixel = bitmap.GetPixel(i, j);

                    image[i * bitmap.Width + j] = ((currentPixel.R * 0.3f) + (currentPixel.G * 0.59f) + (currentPixel.B * 0.11f)) / 255f;
                }
            }

            this.number = number;

            expected = new double[10];

            for (int i = 0; i < expected.Length; i++)
            {
                expected[i] = 0;
                if (i == number)
                {
                    expected[i] = 1;
                }
            }
        }

        public static List<ImageData> LoadImages(string pathToImageFolder)
        {
            List<ImageData> imageDatas = new List<ImageData>();

            for (int i = 0; i < 10; i++)
            {
                string[] filePaths = Directory.GetFiles(pathToImageFolder + "/" + i + "/", "*.jpg");

                foreach (string image in filePaths)
                {
                    Bitmap bitmap = new Bitmap(image);

                    imageDatas.Add(new ImageData(bitmap, i));
                }
            }

            return imageDatas;
        }

    }
}