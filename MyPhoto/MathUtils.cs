using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPhoto
{
    internal static class MathUtils
    {
        public static float[][] Identity5x5
        {
            get
            {
                return new float[][]
                {
                    new float[] {1,  0,  0,  0, 0},
                    new float[] {0,  1,  0,  0, 0},
                    new float[] {0,  0,  1,  0, 0},
                    new float[] {0,  0,  0,  1, 0},
                    new float[] {0,  0,  0,  0, 1}
                };
            }
        }

        public static double[,] Mean3x3
        {
            get
            {
                return new double[,]
               { {  1, 1, 1, },
                  {  1, 1, 1, },
                  {  1, 1, 1, }, };
            }
        }

        public static double[,] Mean7x7
        {
            get
            {
                return new double[,]
                { {  1, 1, 1, 1, 1, 1, 1 },
                  {  1, 1, 1, 1, 1, 1, 1 },
                  {  1, 1, 1, 1, 1, 1, 1 },
                  {  1, 1, 1, 1, 1, 1, 1 },
                  {  1, 1, 1, 1, 1, 1, 1 },
                  {  1, 1, 1, 1, 1, 1, 1 },
                  {  1, 1, 1, 1, 1, 1, 1 }, };
            }
        }

        public static float[][] Multiply(float[][] f1, float[][] f2)
        {
            float[][] X = new float[5][];
            for (int d = 0; d < 5; d++)
                X[d] = new float[5];
            int size = 5;
            float[] column = new float[5];
            for (int j = 0; j < 5; j++)
            {
                for (int k = 0; k < 5; k++)
                {
                    column[k] = f1[k][j];
                }
                for (int i = 0; i < 5; i++)
                {
                    float[] row = f2[i];
                    float s = 0;
                    for (int k = 0; k < size; k++)
                    {
                        s += row[k] * column[k];
                    }
                    X[i][j] = s;
                }
            }
            return X;
        }

        public static float[][] MultiplyByValue(float value, float[][] f1)
        {
            float[][] X = new float[5][];
            for (int d = 0; d < 5; d++)
                X[d] = new float[5];
            int size = 5;

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    float temp = 0;

                    temp = f1[i][j] * value;

                    X[i][j] = temp;
                }
            }
            return X;
        }

        public static float[][] Add(float[][] f1, float[][] f2)
        {
            float[][] X = new float[5][];
            for (int d = 0; d < 5; d++)
                X[d] = new float[5];
            int size = 5;

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    float temp = 0;

                    temp = f1[i][j] + f2[i][j];

                    X[i][j] = temp;
                }
            }
            return X;
        }
    }
}
