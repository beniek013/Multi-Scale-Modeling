using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace GrainGrowth_1.Classes
{
    public class Board
    {
        private Cell[,] matrix;
        public static Dictionary<int, Brush> dictionary;
        private int grainAmount;
        private int lastClicked;
        public static List<Brush> brushes;
        Random rnd;
        private string kind; // rodzaj sąsiedztwa;
        public Board()
        {
            //matrix = new Cell[100, 100];
            //FillWithZeros();
        }

        public Board(int ga, int size1, int size2, string pattern, int width, int height, string kind)
        {
            matrix = new Cell[size1, size2];
            dictionary = new Dictionary<int, Brush>();
            grainAmount = ga;
            rnd = new Random();
            lastClicked = 0;
            this.kind = kind;
            FillWithZeros();
            brushes = Addons.FillBrush(ga > width * height ? ga : width * height);
            switch (pattern)
            {
                case "Random":
                    matrix = Addons.FillRandomly(matrix, grainAmount);
                    break;
                case "Manual":
                    break;
                case "Homogeneus":
                    matrix = Addons.FillHomogeneusly(matrix, grainAmount, width, height);
                    break;
                case "Radius":
                    //matrix = Addons.FillRadius(matrix, grainAmount, colorAmount, int.Parse(r));
                    break;
                default:
                    matrix = Addons.FillRandomly(matrix, grainAmount);
                    break;
            }
        }

        public Cell[,] GetMatrix() {
            return matrix;
        }

        public List<Brush> GetBrushes()
        {
            return brushes;
        }

        private void FillWithZeros()
        {
            var id = 1;
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j] = new Cell
                    {
                        id = id++,
                        x = i,
                        y = j
                    };
                }
            }
        }
        public void GetNextIteration(bool period)
        {
            if (matrix != null)
            {
                var newMat = GetMatrixCopy();
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    for (int j = 0; j < matrix.GetLength(1); j++)
                    {
                        if (matrix[i, j].value == 0)
                        {
                            switch (kind)
                            {
                                case "Von Neumann":
                                    newMat[i, j].value = VonNeumann(i, j, period);
                                    break;
                                case "Moore":
                                    newMat[i, j].value = Moore(i, j, period);
                                    break;
                                case "Pentagonal":
                                    newMat[i, j].value = Pentagonal(i, j, period);
                                    break;
                                case "Hexagonal":
                                    newMat[i, j].value = Hexagonal(i, j, period);
                                    break;
                                case "Hexagonal(L)":
                                    newMat[i, j].value = Hexagonal(i, j, period,1);
                                    break;
                                case "Hexagonal(R)":
                                    newMat[i, j].value = Hexagonal(i, j, period, 2);
                                    break;
                            }
                        }
                        newMat[i, j].id = matrix[i, j].id;
                    }
                }
                matrix = newMat;
            }
        }

        private Cell[,] GetMatrixCopy()
        {
            var newMat = new Cell[matrix.GetLength(0), matrix.GetLength(1)];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    newMat[i, j] = new Cell();
                    newMat[i, j].value = matrix[i, j].value;
                    newMat[i, j].x = matrix[i, j].x;
                    newMat[i, j].y = matrix[i, j].y;
                }
            }
            return newMat;
        }

        private int VonNeumann(int i, int j, bool period)
        {
            int temp1, temp2, temp3, temp4;
            var neighbours = new List<Cell>();
            if (period)
            {
                temp1 = i == 0 ? matrix.GetLength(0) - 1 : i - 1;      //i-1
                temp2 = j == 0 ? matrix.GetLength(1) - 1 : j - 1;      //j-1
                temp3 = i == matrix.GetLength(0) - 1 ? 0 : i + 1;      //i+1
                temp4 = j == matrix.GetLength(1) - 1 ? 0 : j + 1;      //j+1

                neighbours.Add(matrix[temp1, j]);
                neighbours.Add(matrix[temp3, j]);
                neighbours.Add(matrix[i, temp2]);
                neighbours.Add(matrix[i, temp4]);
            }
            else
            {
                if (i == 0)
                    neighbours.Add(new Cell());
                else
                    neighbours.Add(matrix[i - 1, j]);

                if (i == matrix.GetLength(0) - 1)
                    neighbours.Add(new Cell());
                else
                    neighbours.Add(matrix[i + 1, j]);

                if (j == 0)
                    neighbours.Add(new Cell());
                else
                    neighbours.Add(matrix[i, j - 1]);

                if (j == matrix.GetLength(1) - 1)
                    neighbours.Add(new Cell());
                else
                    neighbours.Add(matrix[i, j + 1]);
            }

            if (neighbours.Any(x => x.value > 0))
            {
                var newList = neighbours.GroupBy(x => x.value);
                var maxKey = newList.Max(x => x.Key);
                return newList.FirstOrDefault(x => x.Key == maxKey).FirstOrDefault().value;
            }
            return 0;
        }

        private int Moore(int i, int j, bool period)
        {
            int temp1, temp2, temp3, temp4;
            var neighbours = new List<Cell>();
            if (period)
            {
                temp1 = i == 0 ? matrix.GetLength(0) - 1 : i - 1;      //i-1
                temp2 = j == 0 ? matrix.GetLength(1) - 1 : j - 1;      //j-1
                temp3 = i == matrix.GetLength(0) - 1 ? 0 : i + 1;      //i+1
                temp4 = j == matrix.GetLength(1) - 1 ? 0 : j + 1;      //j+1

                neighbours.Add(matrix[temp1, j]);
                neighbours.Add(matrix[temp3, j]);
                neighbours.Add(matrix[i, temp2]);
                neighbours.Add(matrix[i, temp4]);

                neighbours.Add(matrix[temp1, temp2]);
                neighbours.Add(matrix[temp1, temp4]);
                neighbours.Add(matrix[temp3, temp2]);
                neighbours.Add(matrix[temp3, temp4]);
            }
            else
            {
                if (i == 0)
                    neighbours.Add(new Cell());
                else
                    neighbours.Add(matrix[i - 1, j]);

                if (i == matrix.GetLength(0) - 1)
                    neighbours.Add(new Cell());
                else
                    neighbours.Add(matrix[i + 1, j]);

                if (j == 0)
                    neighbours.Add(new Cell());
                else
                    neighbours.Add(matrix[i, j - 1]);

                if (j == matrix.GetLength(1) - 1)
                    neighbours.Add(new Cell());
                else
                    neighbours.Add(matrix[i, j + 1]);
            }

            /*
             //pentagonalne losowe
                var rand= rnd.Next(1, 5);
                switch (rand) {
                    case 1:
                        neighbours = FirstVariant(i,j);
                        break;
                    case 2:
                        neighbours = SecondVariant(i, j);
                        break;
                    case 3:
                        neighbours = ThirdVariant(i, j);
                        break;
                    case 4:
                        neighbours = FourthVariant(i, j);
                        break;
             */

            if (neighbours.Any(x => x.value > 0))
            {
                var newList = neighbours.GroupBy(x => x.value);
                var maxKey = newList.Max(x => x.Key);
                return newList.FirstOrDefault(x => x.Key == maxKey).FirstOrDefault().value;
            }
            return 0;
        }

        private int Pentagonal(int i, int j, bool period)
        {
            var neighbours = new List<Cell>();
            if (period)
            {
                var rand = rnd.Next(1, 5);
                switch (rand)
                {
                    case 1:
                        neighbours = FirstVariant(i, j);
                        break;
                    case 2:
                        neighbours = SecondVariant(i, j);
                        break;
                    case 3:
                        neighbours = ThirdVariant(i, j);
                        break;
                    case 4:
                        neighbours = FourthVariant(i, j);
                        break;
                }
            }
            else
            {
                if (i == 0)
                    neighbours.Add(new Cell());
                else
                    neighbours.Add(matrix[i - 1, j]);

                if (i == matrix.GetLength(0) - 1)
                    neighbours.Add(new Cell());
                else
                    neighbours.Add(matrix[i + 1, j]);

                if (j == 0)
                    neighbours.Add(new Cell());
                else
                    neighbours.Add(matrix[i, j - 1]);

                if (j == matrix.GetLength(1) - 1)
                    neighbours.Add(new Cell());
                else
                    neighbours.Add(matrix[i, j + 1]);
            }

            if (neighbours.Any(x => x.value > 0))
            {
                var newList = neighbours.GroupBy(x => x.value);
                var maxKey = newList.Max(x => x.Key);
                return newList.FirstOrDefault(x => x.Key == maxKey).FirstOrDefault().value;
            }
            return 0;
        }

        private int Hexagonal(int i, int j, bool period, int c = 0)
        {
            var neighbours = new List<Cell>();
            int variant;
            if (period)
            {
                if (c == 0)
                    variant = rnd.Next(1, 3);
                else
                    variant = c;
                switch (variant)
                {
                    case 1:
                        neighbours = LeftVariant(i, j);
                        break;
                    case 2:
                        neighbours = RightVariant(i, j);
                        break;
                }
            }
            else
            {
                if (i == 0)
                    neighbours.Add(new Cell());
                else
                    neighbours.Add(matrix[i - 1, j]);

                if (i == matrix.GetLength(0) - 1)
                    neighbours.Add(new Cell());
                else
                    neighbours.Add(matrix[i + 1, j]);

                if (j == 0)
                    neighbours.Add(new Cell());
                else
                    neighbours.Add(matrix[i, j - 1]);

                if (j == matrix.GetLength(1) - 1)
                    neighbours.Add(new Cell());
                else
                    neighbours.Add(matrix[i, j + 1]);
            }

            if (neighbours.Any(x => x.value > 0))
            {
                var newList = neighbours.GroupBy(x => x.value);
                var maxKey = newList.Max(x => x.Key);
                return newList.FirstOrDefault(x => x.Key == maxKey).FirstOrDefault().value;
            }
            return 0;
        }

        private List<Cell> RightVariant(int i, int j)
        {
            int temp1, temp2, temp3, temp4;
            temp1 = i == 0 ? matrix.GetLength(0) - 1 : i - 1;      //i-1
            temp2 = j == 0 ? matrix.GetLength(1) - 1 : j - 1;      //j-1
            temp3 = i == matrix.GetLength(0) - 1 ? 0 : i + 1;      //i+1
            temp4 = j == matrix.GetLength(1) - 1 ? 0 : j + 1;      //j+1

            var neighbours = new List<Cell>();

            neighbours.Add(matrix[temp1, j]);
            neighbours.Add(matrix[temp3, j]);
            neighbours.Add(matrix[i, temp2]);
            neighbours.Add(matrix[i, temp4]);

            neighbours.Add(matrix[temp1, temp4]);
            neighbours.Add(matrix[temp3, temp2]);

            return neighbours;
        }

        private List<Cell> LeftVariant(int i, int j)
        {
            int temp1, temp2, temp3, temp4;
            temp1 = i == 0 ? matrix.GetLength(0) - 1 : i - 1;      //i-1
            temp2 = j == 0 ? matrix.GetLength(1) - 1 : j - 1;      //j-1
            temp3 = i == matrix.GetLength(0) - 1 ? 0 : i + 1;      //i+1
            temp4 = j == matrix.GetLength(1) - 1 ? 0 : j + 1;      //j+1

            var neighbours = new List<Cell>();

            neighbours.Add(matrix[temp1, j]);
            neighbours.Add(matrix[temp3, j]);
            neighbours.Add(matrix[i, temp2]);
            neighbours.Add(matrix[i, temp4]);

            neighbours.Add(matrix[temp1, temp2]);
            neighbours.Add(matrix[temp3, temp4]);

            return neighbours;
        }

        private List<Cell> FirstVariant(int i, int j)
        {
            int temp1, temp2, temp3, temp4;
            temp1 = i == 0 ? matrix.GetLength(0) - 1 : i - 1;      //i-1
            temp2 = j == 0 ? matrix.GetLength(1) - 1 : j - 1;      //j-1
            temp3 = i == matrix.GetLength(0) - 1 ? 0 : i + 1;      //i+1
            temp4 = j == matrix.GetLength(1) - 1 ? 0 : j + 1;      //j+1

            var neighbours = new List<Cell>();

            neighbours.Add(matrix[temp1, temp4]);
            neighbours.Add(matrix[i, temp4]);
            neighbours.Add(matrix[temp1, j]);
            neighbours.Add(matrix[temp1, temp2]);
            neighbours.Add(matrix[i, temp4]);

            return neighbours;
        }

        private List<Cell> SecondVariant(int i, int j)
        {
            int temp1, temp2, temp3, temp4;
            temp1 = i == 0 ? matrix.GetLength(0) - 1 : i - 1;      //i-1
            temp2 = j == 0 ? matrix.GetLength(1) - 1 : j - 1;      //j-1
            temp3 = i == matrix.GetLength(0) - 1 ? 0 : i + 1;      //i+1
            temp4 = j == matrix.GetLength(1) - 1 ? 0 : j + 1;      //j+1

            var neighbours = new List<Cell>();

            neighbours.Add(matrix[temp3, temp4]);
            neighbours.Add(matrix[i, temp4]);
            neighbours.Add(matrix[temp3, j]);
            neighbours.Add(matrix[temp3, temp2]);
            neighbours.Add(matrix[i, temp4]);

            return neighbours;
        }

        private List<Cell> ThirdVariant(int i, int j)
        {
            int temp1, temp2, temp3, temp4;
            temp1 = i == 0 ? matrix.GetLength(0) - 1 : i - 1;      //i-1
            temp2 = j == 0 ? matrix.GetLength(1) - 1 : j - 1;      //j-1
            temp3 = i == matrix.GetLength(0) - 1 ? 0 : i + 1;      //i+1
            temp4 = j == matrix.GetLength(1) - 1 ? 0 : j + 1;      //j+1

            var neighbours = new List<Cell>();

            neighbours.Add(matrix[temp1, j]);
            neighbours.Add(matrix[temp3, j]);
            neighbours.Add(matrix[temp1, temp2]);
            neighbours.Add(matrix[i, temp2]);
            neighbours.Add(matrix[temp3, temp2]);

            return neighbours;
        }

        private List<Cell> FourthVariant(int i, int j)
        {
            int temp1, temp2, temp3, temp4;
            temp1 = i == 0 ? matrix.GetLength(0) - 1 : i - 1;      //i-1
            temp2 = j == 0 ? matrix.GetLength(1) - 1 : j - 1;      //j-1
            temp3 = i == matrix.GetLength(0) - 1 ? 0 : i + 1;      //i+1
            temp4 = j == matrix.GetLength(1) - 1 ? 0 : j + 1;      //j+1

            var neighbours = new List<Cell>();

            neighbours.Add(matrix[temp1, j]);
            neighbours.Add(matrix[temp3, j]);
            neighbours.Add(matrix[temp1, temp4]);
            neighbours.Add(matrix[i, temp4]);
            neighbours.Add(matrix[temp3, temp4]);

            return neighbours;
        }

        public void Paint(Graphics g, bool energy = false)
        {
            if (matrix != null)
            {
                for (int r = 0; r < matrix.GetLength(0); r++)
                {
                    for (int c = 0; c < matrix.GetLength(1); c++)
                    {
                        if (energy)
                        {
                            g.FillRectangle(new SolidBrush(Color.FromArgb(0, 0, 255 / 5 * matrix[r, c].energy)), c * 5, r * 5, 5, 5);
                        }
                        else
                        {
                            if (matrix[r, c].value != 0)
                            {
                                g.FillRectangle(brushes[matrix[r, c].value], c * 5, r * 5, 5, 5);
                                matrix[r, c].color = brushes[matrix[r, c].value];
                            }
                        }
                    }
                }
            }
        }

        public void MarkOneCell(MouseEventArgs me)
        {
            int x = me.X / 5;
            int y = me.Y / 5;
            if (matrix[y, x].value == 0)
                matrix[y, x].value = lastClicked++;
        }


    }
}
