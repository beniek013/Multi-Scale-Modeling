using System;

namespace GameOfLife.Classes
{
    public class Addons
    {
        public static int[,] FillRandomly(int[,] matrix)
        {
            Random rnd = new Random();
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j] = rnd.Next(0, 2);
                }
            }
            return matrix;
        }

        public static int[,] FillWithOscillator(int[,] matrix)
        {
            var halfWidth = matrix.GetLength(0) / 2;
            var halfHeight = matrix.GetLength(1) / 2;
            matrix[halfWidth, halfHeight - 1] = 1;
            matrix[halfWidth, halfHeight] = 1;
            matrix[halfWidth, halfHeight + 1] = 1;
            return matrix;
        }

        public static int[,] FillWithGlider(int[,] matrix)
        {
            var halfWidth = matrix.GetLength(0) / 2;
            var halfHeight = matrix.GetLength(1) / 2;
            matrix[halfWidth - 1, halfHeight] = 1;
            matrix[halfWidth - 1, halfHeight + 1] = 1;
            matrix[halfWidth, halfHeight - 1] = 1;
            matrix[halfWidth, halfHeight] = 1;
            matrix[halfWidth + 1, halfHeight + 1] = 1;
            return matrix;
        }



        public static int[,] FillWithConstants(int[,] matrix)
        {
            var halfWidth = matrix.GetLength(0) / 2;
            var halfHeight = matrix.GetLength(1) / 2;
            matrix[halfWidth - 1, halfHeight] = 1;
            matrix[halfWidth - 1, halfHeight + 1] = 1;
            matrix[halfWidth, halfHeight - 1] = 1;
            matrix[halfWidth, halfHeight + 2] = 1;
            matrix[halfWidth + 1, halfHeight] = 1;
            matrix[halfWidth + 1, halfHeight + 1] = 1;
            return matrix;
        }
    }
}
