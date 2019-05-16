using System;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using GrainGrowth_1.Classes;
using System.Collections.Generic;

namespace GrainGrowth_1.Classes
{
    public class Board
    {
        private Cell[,] matrix;
        public static Dictionary<int, Brush> dictionary;
        private int grainAmount;
        private int colorAmount;
        private int lastClicked;

        public Board()
        {
            matrix = new Cell[100, 100];
            FillWithZeros();
            colorAmount = 2;
        }

        public Board(int ga, int ca, int size1, int size2, string pattern, string space,string r)
        {
            matrix = new Cell[size1, size2];
            dictionary = new Dictionary<int, Brush>();
            grainAmount = ga;
            colorAmount = ca;
            lastClicked = 0;
            FillWithZeros();
            switch (pattern)
            {
                case "Radnom":
                    matrix = Addons.FillRandomly(matrix, grainAmount, colorAmount);
                    break;
                case "Manual":
                    break;
                case "Homogeneus":
                    matrix = Addons.FillHomogeneusly(matrix, colorAmount, int.Parse(space));
                    break;
                case "Radius":
                    //matrix = Addons.FillRadius(matrix, grainAmount, colorAmount, int.Parse(r));
                    break;
                default:
                    matrix = Addons.FillRandomly(matrix, grainAmount, colorAmount);
                    break;
            }
        }

        private void FillWithZeros()
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j] = new Cell();
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
                            newMat[i, j].value = ChoseBestGrain(i, j, period);
                        }
                    }
                }
                matrix = newMat;
            }
        }
        private Cell[,] GetMatrixCopy() {
            var newMat = new Cell[matrix.GetLength(0),matrix.GetLength(1)];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    newMat[i, j] = new Cell();
                    newMat[i, j].value = matrix[i, j].value;
                }
            }
            return newMat;
        }

        private int ChoseBestGrain(int i, int j, bool period)
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

                if(i==matrix.GetLength(0) -1)
                    neighbours.Add(new Cell());
                else
                    neighbours.Add(matrix[i + 1, j]);

                if (j == 0)
                    neighbours.Add(new Cell());
                else
                    neighbours.Add(matrix[i, j-1]);

                if (j == matrix.GetLength(1) -1 )
                    neighbours.Add(new Cell());
                else
                    neighbours.Add(matrix[i, j +1]);
            }
            
            if (neighbours.Any(x => x.value > 0))
            {
                var newList = neighbours.GroupBy(x => x.value);
                var maxKey = newList.Max(x => x.Key);
                return newList.FirstOrDefault(x => x.Key == maxKey).FirstOrDefault().value;
            }
            return 0;
        }
        public void Paint(Graphics g)
        {
            if (matrix != null)
            {
                for (int r = 0; r < matrix.GetLength(0); r++)
                {
                    for (int c = 0; c < matrix.GetLength(1); c++)
                    {
                        if (matrix[r, c].value == 1)
                        {
                            g.FillRectangle(Brushes.Green, c * 5, r * 5, 5, 5);
                            matrix[r, c].color = Brushes.Green;
                        }
                        if (matrix[r, c].value == 2)
                        {
                            g.FillRectangle(Brushes.Red, c * 5, r * 5, 5, 5);
                            matrix[r, c].color = Brushes.Red;
                        }
                        if (matrix[r, c].value == 3)
                        {
                            g.FillRectangle(Brushes.Black, c * 5, r * 5, 5, 5);
                            matrix[r, c].color = Brushes.Black;
                        }
                        if (matrix[r, c].value == 4)
                        {
                            g.FillRectangle(Brushes.White, c * 5, r * 5, 5, 5);
                            matrix[r, c].color = Brushes.White;
                        }
                        if (matrix[r, c].value == 5)
                        {
                            g.FillRectangle(Brushes.Blue, c * 5, r * 5, 5, 5);
                            matrix[r, c].color = Brushes.Blue;
                        }
                        if (matrix[r, c].value == 6)
                        {
                            g.FillRectangle(Brushes.Violet, c * 5, r * 5, 5, 5);
                            matrix[r, c].color = Brushes.Violet;
                        }
                        if (matrix[r, c].value == 7)
                        {
                            g.FillRectangle(Brushes.Pink, c * 5, r * 5, 5, 5);
                            matrix[r, c].color = Brushes.Pink;
                        }
                        if (matrix[r, c].value == 8)
                        {
                            g.FillRectangle(Brushes.Yellow, c * 5, r * 5, 5, 5);
                            matrix[r, c].color = Brushes.Yellow;
                        }
                        if (matrix[r, c].value == 9)
                        {
                            g.FillRectangle(Brushes.Silver, c * 5, r * 5, 5, 5);
                            matrix[r, c].color = Brushes.Silver;
                        }
                        if (matrix[r, c].value == 10)
                        {
                            g.FillRectangle(Brushes.Gray, c * 5, r * 5, 5, 5);
                            matrix[r, c].color = Brushes.Gray;
                        }
                        /*if (matrix[r, c].value > 4) {
                            if (dictionary.Keys.Contains(matrix[r, c].value))
                            {
                                brush = dictionary[matrix[r, c].value];
                                g.FillRectangle(brush, c * 5, r * 5, 5, 5);
                            }
                            else
                            {
                                brush = Addons.PickBrush();
                                dictionary[matrix[r, c].value] = brush;
                                g.FillRectangle(brush, c * 5, r * 5, 5, 5);
                            }
                        }*/
                    }
                }
            }
        }

        public void MarkOneCell(MouseEventArgs me)
        {
            if (lastClicked > colorAmount)
            {
                lastClicked = 1;
            }
            int x = me.X / 5;
            int y = me.Y / 5;
            if (matrix[y, x].value == 0)
                matrix[y, x].value = lastClicked++;
        }
    }
}
