using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AlphaDecay_Shelamanov_Artem
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 f2 = new Form2();
            f2.ShowDialog();
        }
        public Image[] a = new Image[24];
        int current = 0;
        Image load(int a)
        {
            Image aa;
            if (a > 9) a = 9;
            if (a < 1) a = 1;
            progressBar1.Value = a * 100 / 9;
            numericUpDown1.Value = a;
            return aa = Image.FromFile(System.IO.Path.GetFullPath("res\\1 (" + Convert.ToString(a) + ").JPG"));
        }
        private void Form5_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = load(1);
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            current = Convert.ToInt32(numericUpDown1.Value);
            pictureBox1.Image = load(current);
        }
    }
}
