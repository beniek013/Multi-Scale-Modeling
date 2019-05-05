using GameOfLife.Classes;
using System;
using System.Windows.Forms;

namespace GameOfLife
{
    public partial class Form1 : Form
    {
        public Board board;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            board = new Board();
        }

        private void Bt1_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            board.Paint(e.Graphics);
        }
        private void Timer1_Tick(object sender, EventArgs e)
        {
            board.GetNextIteration();
            Refresh();
        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs)e;
            board.MarkOneCell(me);
            Refresh();
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
                board = new Board(width, height, cb);
                pictureBox1.Width = height * 5;
                pictureBox1.Height = width * 5;
                bt1.Visible = true;
                button4.Visible = true;
                button3.Visible = true;
                button1.Visible = true;
            }
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            if (timer1.Interval > 50)
                timer1.Interval -= 50;
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            timer1.Interval += 50;
        }
    }
}
