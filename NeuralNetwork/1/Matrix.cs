using System;

namespace NeuralNetwork
{
    public class Matrix
    {
        int rows, cols;
        float[,] matrix;

        Random random;

        public static Matrix operator* (float num, Matrix matrix)
        {
            for (int i = 0; i < matrix.rows; i++)
            {
                for (int j = 0; j < matrix.cols; j++)
                {
                    matrix.matrix[i, j] *= num;
                }
            }
            return matrix;
        }

        public Matrix(int r, int c)
        {
            rows = r;
            cols = c;
            matrix = new float[rows, cols];
            random = new Random();
        }

        public Matrix(float[,] m)
        {
            matrix = m;
            rows = matrix.GetLength(0);
            cols = matrix.GetLength(1);
            random = new Random();
        }

        public void Output()
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Console.Write(matrix[i, j] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public Matrix Dot(Matrix n)
        {
            Matrix result = new Matrix(rows, n.cols);

            if (cols == n.rows)
            {
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < n.cols; j++)
                    {
                        float sum = 0;
                        for (int k = 0; k < cols; k++)
                        {
                            sum += matrix[i, k] * n.matrix[k, j];
                        }
                        result.matrix[i, j] = sum;
                    }
                }
            }
            return result;
        }

        public void Randomize()
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    matrix[i, j] = (float)random.NextDouble() * 2 - 1;
                }
            }
        }

        public Matrix SingleColumnMatrixFromArray(float[] arr)
        {
            Matrix n = new Matrix(arr.Length, 1);
            for (int i = 0; i < arr.Length; i++)
            {
                n.matrix[i, 0] = arr[i];
            }
            return n;
        }

        public float[] ToArray()
        {
            float[] arr = new float[rows * cols];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    arr[j + i * cols] = matrix[i, j];
                }
            }
            return arr;
        }

        public Matrix AddBias()
        {
            Matrix n = new Matrix(rows + 1, 1);
            for (int i = 0; i < rows; i++)
            {
                n.matrix[i, 0] = matrix[i, 0];
            }
            n.matrix[rows, 0] = 1;
            return n;
        }

        public Matrix Activate()
        {
            Matrix n = new Matrix(rows, cols);
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    n.matrix[i, j] = Sigmoid(matrix[i, j]);
                }
            }
            return n;
        }

        public Matrix GiveLog()
        {
            Matrix clone = this.Clone();

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    clone.matrix[i, j] = (float)Math.Log(clone.matrix[i, j]);
                }
            }

            return clone;
        }

        public float Sigmoid(float x)
        {
            return (float)(1 / (1 + Math.Exp(-x)));
        }

        public float Relu(float x)
        {
            return Math.Max(0, x);
        }

        public void Mutate(float mutationRate)
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    float rand = (float)random.NextDouble();
                    if (rand < mutationRate)
                    {
                        double u1 = 1.0 - random.NextDouble(); //uniform(0,1] random doubles
                        double u2 = 1.0 - random.NextDouble();
                        double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2); //random normal(0,1)

                        matrix[i, j] += (float)randStdNormal / 5;

                        if (matrix[i, j] > 1)
                        {
                            matrix[i, j] = 1;
                        }
                        if (matrix[i, j] < -1)
                        {
                            matrix[i, j] = -1;
                        }
                    }
                }
            }
        }

        public Matrix Crossover(Matrix partner)
        {
            Matrix child = new Matrix(rows, cols);

            int randC = random.Next(cols);
            int randR = random.Next(rows);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if ((i < randR) || (i == randR && j <= randC))
                    {
                        child.matrix[i, j] = matrix[i, j];
                    }
                    else
                    {
                        child.matrix[i, j] = partner.matrix[i, j];
                    }
                }
            }
            return child;
        }

        public Matrix Clone()
        {
            Matrix clone = new Matrix(rows, cols);
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    clone.matrix[i, j] = matrix[i, j];
                }
            }
            return clone;
        }
    }
}