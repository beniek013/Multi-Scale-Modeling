using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace GameOfLife.Classes
{
    public class Addons
    {
        public static int[,] FillRandomly(int[,] matrix) {
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

        public static int[,] FillWithOscillator(int[,] matrix) {
            matrix[10, 12] = 1;
            matrix[10, 13] = 1;
            matrix[10, 14] = 1;
            return matrix;
        }

        public static int[,] FillWithGlider(int[,] matrix)
        {
            matrix[20, 20] = 1;
            matrix[20, 21] = 1;
            matrix[21, 19] = 1;
            matrix[21, 20] = 1;
            matrix[22, 21] = 1;
            return matrix;
        }

        public static int[,] FillWithConstants(int[,] matrix) {
            matrix[20, 20] = 1;
            matrix[20, 21] = 1;
            matrix[21, 19] = 1;
            matrix[21, 22] = 1;
            matrix[22, 20] = 1;
            matrix[22, 21] = 1;

            return matrix;
        }
    }
}
