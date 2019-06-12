using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Linq;

namespace GrainGrowth_1.Classes
{
    public class Addons
    {
        public static Cell[,] FillRandomly(Cell[,] matrix, int grainAmount)
        {
            Random rnd = new Random();
            int x, y;
            int iterator = 0;
            while (grainAmount >= 0)
            {
                x = rnd.Next(1, matrix.GetLength(0));
                y = rnd.Next(1, matrix.GetLength(1));
                if (matrix[x, y].value == 0)
                {
                    matrix[x, y].value = iterator++;
                    grainAmount--;
                }
            }
            return matrix;
        }

        public static Cell[,] FillRadius(Cell[,] matrix, int grainAmount, int colorAmount, int r)
        {
            Random rnd = new Random();
            int x, y;
            var dict = new Dictionary<int, int>();
            var iterator = colorAmount;
            while (grainAmount > 0)
            {
                if (iterator == colorAmount + 1)
                    iterator = 1;
                x = rnd.Next(1, matrix.GetLength(0));
                y = rnd.Next(1, matrix.GetLength(1));
                matrix[rnd.Next(1, matrix.GetLength(0)), rnd.Next(1, matrix.GetLength(1))].value = iterator++;
                grainAmount--;
            }
            return matrix;
        }

        public static List<Brush> FillBrush(int grainAmount)
        {
            var brushes = new List<Brush>();
            for (int i = 0; i < grainAmount +1; i++)
            {
                brushes.Add(PickBrush());
            }
            return brushes;
        }

        private static double GetDistance(double x1, double y1, double x2, double y2)
        {
            return Math.Sqrt(Math.Pow((x2 - x1), 2) + Math.Pow((y2 - y1), 2));
        }

        public static Cell[,] FillHomogeneusly(Cell[,] matrix, int grainAmount, int amount1, int amount2)
        {
            var height = matrix.GetLength(0);
            var width = matrix.GetLength(1);
            var step1 = height / amount1;
            var step2 = width / amount2;
            var iterator = 1;
            for (int i = step1/2; i < height; i += step1)
            {
                for (int j = step2/2; j < width; j += step2)
                {
                    matrix[i, j].value = iterator++;
                }
            }
            return matrix;
        }

        public static Random rnd = new Random();
        public static Brush PickBrush()
        {

            Type brushesType = typeof(Brushes);

            PropertyInfo[] properties = brushesType.GetProperties();

            int random = rnd.Next(properties.Length);
            var result = (Brush)properties[random].GetValue(null, null);

            return result;
        }

        public static bool IsLimit(Cell cell, List<Cell> cells)
        {
            return cells.Any(x => x.value != cell.value);
        }
    }
}
