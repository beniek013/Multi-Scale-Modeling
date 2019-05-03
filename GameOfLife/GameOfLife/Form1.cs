using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GameOfLife.Classes;

namespace GameOfLife
{
    public partial class Form1 : Form
    {
        public Board mat;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            mat = new Board();
        }

        private void Bt1_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            mat.Paint(e.Graphics);
        }
        private void Timer1_Tick(object sender, EventArgs e)
        {
            mat.GetNextIteration();
            Refresh();
        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs)e;
            Point coordinates = me.Location;
            textBox1.Text += me.X;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            timer1.Stop();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            var tb1 = textBox1.Text;
            var tb2 = textBox2.Text;
            var cb = comboBox1.Text;
            if (int.TryParse(tb1, out int width) && int.TryParse(tb2, out int height) && cb != "Choose pattern")
            {
                timer1.Start();
                mat = new Board(width, height, cb);
                pictureBox1.Width = width * 5;
                pictureBox1.Height = height * 5;
                timer1.Start();
                bt1.Visible = true;
                button1.Visible = true;
            }
        }
    }
}
