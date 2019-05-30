﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GrainGrowth_1.Classes;

namespace GrainGrowth_1
{
    public partial class Form1 : Form
    {
        Board board;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            board = new Board();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            board.Paint(e.Graphics);
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (comboBox3.Text == "Sorption")
                board.GetNextIteration(false);
            else
                board.GetNextIteration(true);
            Refresh();
        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs)e;
            board.MarkOneCell(me);
            Refresh();
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            if (int.TryParse(grainAmount.Text, out int ga)
                && int.TryParse(textBox1.Text, out int size1)
                && int.TryParse(textBox2.Text, out int size2))
            {
                pictureBox1.Width = size2 * 5;
                pictureBox1.Height = size1 * 5;
                
                board = new Board(ga, size1, size2, comboBox1.Text, int.Parse(textBox3.Text), int.Parse(textBox4.Text), comboBox2.Text);
                timer1.Start();
                Refresh();
            }

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            timer1.Stop();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if (timer1.Interval > 100)
                timer1.Interval -= 100;
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            timer1.Interval += 200;
        }
    }
}
