using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
        MC monteCarlo;
        DRX drx;
        bool energy;
        double kt;
        double step = 0.001;
        public Form1()
        {
            InitializeComponent();
            energy = false;
            label4.Text = 0.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            board = new Board();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            board.Paint(e.Graphics, board.matrix, energy);
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
                energy = false;
                board = new Board(ga, size1, size2, comboBox1.Text, int.Parse(textBox3.Text), int.Parse(textBox4.Text), comboBox2.Text);
                timer1.Start();
                timer2.Stop();
                Refresh();
            }

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            timer1.Start();
            timer2.Start();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            timer2.Stop();
            timer3.Stop();
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

        private void Button5_Click(object sender, EventArgs e)
        {
            if (double.TryParse(textBox5.Text, out kt))
            {
                monteCarlo = new MC(board, kt, comboBox2.Text);
                timer1.Stop();
                timer2.Start();
            }
        }

        private void Timer2_Tick(object sender, EventArgs e)
        {
            monteCarlo.GetNextIteration();
            Refresh();
        }

        private void Timer3_Tick(object sender, EventArgs e)
        {
            board.matrix = drx.GetNextIteration();
            label4.Text = (double.Parse(label4.Text) + step).ToString();
            Refresh();
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            energy = !energy;
            Refresh();
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            /*if (int.TryParse(grainAmount.Text, out int ga)
                && int.TryParse(textBox1.Text, out int size1)
                && int.TryParse(textBox2.Text, out int size2))
            {
                pictureBox1.Width = size2 * 5;
                pictureBox1.Height = size1 * 5;
                energy = false;
                board = new Board(ga, size1, size2, comboBox1.Text, int.Parse(textBox3.Text), int.Parse(textBox4.Text), comboBox2.Text);
                timer1.Start();
                timer2.Stop();
                Refresh();
                
            }*/
            
            drx = new DRX(board,step);
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            board.matrix = drx.GetNextIteration();
            label4.Text = (double.Parse(label4.Text) + step).ToString();
        }

        private void Button9_Click(object sender, EventArgs e)
        {
            timer3.Start();
        }

        private void Button10_Click(object sender, EventArgs e)
        {
            string path = $@"C:\Users\benie.DESKTOP-K69F1U6\source\repos\Multi-Scale-Modeling\GrainGrowth_1\density{label4.Text}.txt";
            StreamWriter sw = new StreamWriter(path);
            for (int i = 0; i < board.matrix.GetLength(0); i++)
            {
                for (int j = 0; j < board.matrix.GetLength(1); j++)
                {
                    sw.WriteLine($"[{i},{j}] {board.matrix[i, j].density}");
                }
            }
            sw.Close();
        }
    }
}
