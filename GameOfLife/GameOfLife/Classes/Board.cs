using System.Drawing;

namespace GameOfLife.Classes
{
    public class Board
    {
        public static int[,] matrix;

        public Board() { }

        public Board(int width, int height, string pattern)
        {
            matrix = new int[width, width];
            switch (pattern)
            {
                case "Constant":
                    matrix = Addons.FillWithConstants(matrix);
                    break;
                case "Glider":
                    matrix = Addons.FillWithGlider(matrix);
                    break;
                case "Manual":
                    break;
                case "Oscillator":
                    matrix = Addons.FillWithOscillator(matrix);
                    break;
                case "Random":
                    matrix = Addons.FillRandomly(matrix);
                    break;
            }
        }
        public void GetNextIteration()
        {
            Form1 t = new Form1();
            var width = matrix.GetLength(0);
            var height = matrix.GetLength(1);
            var newMat = (int[,])matrix.Clone();
            for (int i = 0; i < width - 1; i++)
            {
                for (int j = 0; j < height - 1; j++)
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
            var temp1 = i == 0 ? mat.GetLength(0) - 1 : i - 1;      //i-1
            var temp2 = j == 0 ? mat.GetLength(1) - 1 : j - 1;      //j-1
            var temp3 = i == mat.GetLength(0) - 1 ? 0 : i + 1;      //i+1
            var temp4 = j == mat.GetLength(1) - 1 ? 0 : j + 1;      //j+1

            if (mat[temp3, temp2] == 1)
                counter++;
            if (mat[temp3, j] == 1)
                counter++;
            if (mat[temp3, temp4] == 1)
                counter++;
            if (mat[i, temp2] == 1)
                counter++;
            if (mat[i, temp4] == 1)
                counter++;
            if (mat[temp1, temp2] == 1)
                counter++;
            if (mat[temp1, j] == 1)
                counter++;
            if (mat[temp1, temp4] == 1)
                counter++;

            return counter;
        }
        public void Paint(Graphics g)
        {
            Brush CELL_COLOR = Brushes.Gray;
            if (matrix != null)
            {
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
}
