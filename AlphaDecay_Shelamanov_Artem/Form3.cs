using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AlphaDecay_Shelamanov_Artem
{
    public partial class Form3 : Form
    {
        private bool _dragging = false;
        private Point _start_point = new Point(0, 0);
        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form3_MouseMove(object sender, MouseEventArgs e)
        {
            if (_dragging)
            {
                Point p = PointToScreen(e.Location);
                Location = new Point(p.X - this._start_point.X, p.Y - this._start_point.Y);
            }
        }

        private void Form3_MouseUp(object sender, MouseEventArgs e)
        {
            _dragging = false;
        }

        private void Form3_MouseDown(object sender, MouseEventArgs e)
        {
            _dragging = true;
            _start_point = new Point(e.X, e.Y);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 f2 = new Form2();
            f2.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            pictureBox1.Refresh();
        }

        double f(double g, double l, double k, double q1, double q2, double r)
        {
            return k * q1 * q2 / r / r - g * Math.Exp(-l * r) / r / r;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            double g = double.Parse(textBox1.Text)*Math.Pow(10, int.Parse(textBox2.Text));
            double l = double.Parse(textBox4.Text) * Math.Pow(10, int.Parse(textBox3.Text));
            double k = double.Parse(textBox8.Text) * Math.Pow(10, int.Parse(textBox7.Text));
            double q1 = double.Parse(textBox6.Text) * Math.Pow(10, int.Parse(textBox5.Text));
            double q2 = double.Parse(textBox10.Text) * Math.Pow(10, int.Parse(textBox9.Text));

            Graphics gr = pictureBox1.CreateGraphics();
            gr.DrawLine(Pens.White, 20, 10, 20, pictureBox1.Height-10);
            gr.DrawLine(Pens.White, 10, pictureBox1.Height/2, pictureBox1.Width-20, pictureBox1.Height/2);
            double x, y;
            double coef= double.Parse(textBox11.Text);
            for (x = 1; x < pictureBox1.Width-20; x++)
            {
                checked
                {
                    y = f(g, l, k, q1, q2, x / 500000) * Math.Pow(10, coef);
                    gr.DrawLine(Pens.White, (int)x + 20, pictureBox1.Height / 2 - (int)y, (int)x + 21, pictureBox1.Height / 2 - (int)(f(g, l, k, q1, q2, (x + 1) / 500000) * Math.Pow(10, coef)));
                }
                if (x % 10 == 0)
                {
                    gr.DrawLine(Pens.White, (int)x + 20, pictureBox1.Height / 2 - 3, (int)x + 20, pictureBox1.Height / 2 + 3);
                }
            }
            for(y=0; y< pictureBox1.Height; y++)
            {
                if (y % 10 == 0)
                {
                    gr.DrawLine(Pens.White, 17, (int)y, 23, (int)y);
                }
            }
            int lnx = -6;
            int lny = (int)(coef);
            label15.Text = "In 1 px of x there is ~10^" + lnx + " meters.";
            label16.Text = "In 1 px of y there is ~10^" + lny + " Newtons.";
            label17.Text = "Lines in X-axis are drawn each 10 px, it is 10^" + (lnx+1) + " meters.";
            label18.Text = "Lines in Y-axis are drawn each 10 px, it is 10^" + (lny+1) + " newtons.";
        }
    }
}
