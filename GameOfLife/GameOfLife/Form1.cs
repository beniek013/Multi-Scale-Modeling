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
        public Machine2d mat;
        public Form1()
        {
            InitializeComponent();
            pictureBox1.Width = 50 * 5;
            pictureBox1.Height = 50 * 5;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            mat = new Machine2d();
            timer1.Start();
        }

        private void Bt1_Click(object sender, EventArgs e)
        {
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
    }
}
