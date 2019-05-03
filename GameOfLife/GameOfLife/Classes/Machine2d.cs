using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.Classes
{
    public class Machine2d
    {

        public static int[,] matrix;

        public Machine2d() {
            matrix = new int[50, 50];
            matrix = Addons.FillRandomly(matrix);
        }
        public void GetNextIteration()
        {
            Form1 t = new Form1();
            var width = 50;
            var height = 50;
            var newMat = (int[,])matrix.Clone();
            for (int i = 0; i < width -1 ; i++)
            {
                for (int j = 0; j < height -1 ; j++)
                {
                    int numberOfNeighbors = CountNeihbors(matrix, i, j);
                    if (matrix[i, j] == 0)
                    {
                        if (numberOfNeighbors == 3)
                            newMat[i, j] = 1;
                    }
                    if (matrix[i, j] == 1)
                    {
                        if (numberOfNeighbors > 3 || numberOfNeighbors < 2)
                            newMat[i, j] = 0;
                    }
                }
            }
            matrix = newMat;
        }

        private static int CountNeihbors(int[,] mat, int i, int j)
        {
            var counter = 0;

            if (i < 49 && i > 0 && j > 0 && j < 49)
            {
                if (mat[i + 1, j - 1] == 1)
                    counter++;
                if (mat[i + 1, j] == 1)
                    counter++;
                if (mat[i + 1, j + 1] == 1)
                    counter++;
                if (mat[i, j - 1] == 1)
                    counter++;
                if (mat[i, j + 1] == 1)
                    counter++;
                if (mat[i - 1, j - 1] == 1)
                    counter++;
                if (mat[i - 1, j] == 1)
                    counter++;
                if (mat[i - 1, j + 1] == 1)
                    counter++;
            }
            return counter;
        }
        public void Paint(Graphics g)
        {
            Brush CELL_COLOR = Brushes.Gray;
            for (int r = 0; r < matrix.GetLength(0); r++)
            {
                for (int c = 0; c < matrix.GetLength(1); c++)
                {
                    if (matrix[r, c] == 1)
                        g.FillRectangle(CELL_COLOR, c * 5, r * 5, 5, 5);
                }
            }
        }
    }
}
