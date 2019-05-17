using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GrainGrowth_1.Classes
{
    public class Addons
    {
        public static Cell[,] FillRandomly(Cell[,] matrix, int grainAmount, int colorAmount)
        {
            Random rnd = new Random();
            int x, y;
            var iterator = colorAmount;
            while (grainAmount > 0)
            {
                if (iterator == colorAmount+1)
                    iterator = 1;
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
            var dict = new Dictionary<int,int>();
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

        private static double GetDistance(double x1, double y1, double x2, double y2)
        {
            return Math.Sqrt(Math.Pow((x2 - x1), 2) + Math.Pow((y2 - y1), 2));
        }

        public static Cell[,] FillHomogeneusly(Cell[,] matrix, int colorAmount, int space)
        {
            var width = matrix.GetLength(0);
            var height = matrix.GetLength(1);
            var iterator = colorAmount;
            for (int i = 2; i < width; i += space) {
                for (int j = 2; j < height; j += space) {
                    if (iterator == colorAmount + 1)
                        iterator = 1;
                    matrix[i, j].value = iterator++;
                }
            }
            return matrix;
        }

        public static Brush PickBrush()
        {
            Brush result = Brushes.Transparent;

            Random rnd = new Random();

            Type brushesType = typeof(Brushes);

            PropertyInfo[] properties = brushesType.GetProperties();

            int random = rnd.Next(properties.Length);
            result = (Brush)properties[random].GetValue(null, null);

            return result;
        }
    }
}
