﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace Machines1D
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        bool flag;
        int[,] matrix;

        private void Draw2DArray(object sender, PaintEventArgs e)
        {
            if (flag)
            {
                int step = 5; //distance between the rows and columns
                int width = 5; //the width of the rectangle
                int height = 5; //the height of the rectangle

                using (Graphics g = this.CreateGraphics())
                {
                    g.Clear(SystemColors.Control); //Clear the draw area
                    using (Pen pen = new Pen(Color.Black, 1))
                    {

                        int rows = matrix.GetUpperBound(0) + 1 - matrix.GetLowerBound(0); // = 3, this value is not used
                        int columns = matrix.GetUpperBound(1) + 1 - matrix.GetLowerBound(1); // = 4

                        for (int index = 0; index < matrix.Length; index++)
                        {
                            int i = index / columns;
                            int j = index % columns;
                            if (matrix[i, j] == 1)
                            {
                                Rectangle rect = new Rectangle(new Point(200 + step * j, 50 + step * i), new Size(width, height));
                                g.DrawRectangle(pen, rect);
                                g.FillRectangle(Brushes.Black, rect);
                            }
                            else
                            {
                                Rectangle rect = new Rectangle(new Point(200 + step * j, 50 + step * i), new Size(width, height));
                                g.DrawRectangle(pen, rect);
                                g.FillRectangle(Brushes.White, rect);
                            }
                        }
                    }
                }
                flag = false;
            }
        }
        private int[,] RotateMatrixCounterClockwise(int[,] oldMatrix)
        {
            int[,] newMatrix = new int[oldMatrix.GetLength(1), oldMatrix.GetLength(0)];
            int newColumn, newRow = 0;
            for (int oldColumn = oldMatrix.GetLength(1) - 1; oldColumn >= 0; oldColumn--)
            {
                newColumn = 0;
                for (int oldRow = 0; oldRow < oldMatrix.GetLength(0); oldRow++)
                {
                    newMatrix[newRow, newColumn] = oldMatrix[oldRow, oldColumn];
                    newColumn++;
                }
                newRow++;
            }
            return newMatrix;
        }
        private void Bt_start_Click(object sender, EventArgs e)
        {
            var value1 = textBox1.Text;
            var value2 = textBox2.Text;
            var value3 = textBox3.Text;
            if (int.TryParse(value1, out int regula) 
                && int.TryParse(value2, out int size)
                && int.TryParse(value3, out int iterations))
            {
                matrix = Classes.Machine.OneDimension(regula, size, iterations);
                matrix = RotateMatrixCounterClockwise(matrix);
                matrix = RotateMatrixCounterClockwise(matrix);
                matrix = RotateMatrixCounterClockwise(matrix);
                flag = true;
                Paint += Draw2DArray;
            }
        }
    }
}