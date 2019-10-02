using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.Attempt3
{
    static class ListExtension
    {
        private static Random rng = new Random();

        public static void Shuffle<T>(this IList<T> list)
        {
            int count = list.Count;
            while (count > 1)
            {
                count--;

                int i = rng.Next(count + 1);
                T value = list[i];
                list[i] = list[count];
                list[count] = value;
            }
        }
    }
}
