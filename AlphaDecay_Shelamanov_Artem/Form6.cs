using System;
using System.Windows.Forms;
using Tao.OpenGl;
using Tao.FreeGlut;

namespace AlphaDecay_Shelamanov_Artem
{
    public partial class Form6 : Form
    {
        double rotateZ = 225;
        double rotateL = 330;
        double zoom = -20;

        double vector_l_x;
        double vector_l_y;
        double st, g, l, k, q1, q2, r, x1, x2, y1, y2, z1, z2, Vx1, Vy1, Vz1, scale;
        public Form6()
        {
            InitializeComponent();
            Screen.InitializeContexts();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);
        }

        double F0, Fx, Fy, Fz, m, ax, ay, az, dt;

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            //Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);
        }


        private void button5_Click(object sender, EventArgs e)
        {
            double zoomValue = Convert.ToDouble(textBox6.Text);
            if (zoomValue > 1)
            {
                if (zoomValue != 1)
                    textBox6.Text = Convert.ToString(zoomValue + 1);
            }
            else
            {
                textBox6.Text = Convert.ToString(zoomValue * 2);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            double zoomValue = Convert.ToDouble(textBox6.Text);
            if (zoomValue > 1)
            {
                if (zoomValue != 1)
                    textBox6.Text = Convert.ToString(zoomValue - 1);
            }
            else
            {
                textBox6.Text = Convert.ToString(zoomValue/2);
            }
            
        }

        double DegreesToRad(double grad)
        {
            return grad * Math.PI / 180;
        }
        private void Form6_Load(object sender, EventArgs e)
        {
            Glut.glutInit();                                                                        
            Glut.glutInitDisplayMode(Glut.GLUT_RGB | Glut.GLUT_DOUBLE);                          
            Gl.glClearColor(255, 255, 255, 1);                                                      
            Gl.glViewport(0, 0, Screen.Width, Screen.Height);                                             
            Gl.glMatrixMode(Gl.GL_PROJECTION);                                                      
            Gl.glLoadIdentity();                                                                    
            Glu.gluPerspective(45, (float)Screen.Width / (float)Screen.Height, 0.1, 200);                 
            Gl.glMatrixMode(Gl.GL_MODELVIEW);                                                       
            Gl.glLoadIdentity();
            Gl.glTranslated(0, 0, zoom);
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

        private void button2_Click(object sender, EventArgs e)
        {
            g = double.Parse(textBox11.Text) * Math.Pow(10, int.Parse(textBox12.Text));
            l = double.Parse(textBox13.Text) * Math.Pow(10, int.Parse(textBox14.Text));
            k = double.Parse(textBox15.Text) * Math.Pow(10, int.Parse(textBox16.Text));
            q1 = double.Parse(textBox17.Text) * Math.Pow(10, int.Parse(textBox18.Text));
            q2 = double.Parse(textBox19.Text) * Math.Pow(10, int.Parse(textBox20.Text));
            st = double.Parse(textBox21.Text);

            x1 = double.Parse(textBox7.Text);
            y1 = double.Parse(textBox8.Text);
            z1 = double.Parse(textBox2.Text);
            Vx1 = double.Parse(textBox9.Text);
            Vy1 = double.Parse(textBox10.Text);
            Vz1 = double.Parse(textBox3.Text);

            m = double.Parse(textBox4.Text);
            dt= double.Parse(textBox1.Text);
            //Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);
            timer1.Stop();
            timer1.Interval = (int)dt;
            timer1.Start();
        }
        double force(double x1, double y1, double z1)
        {
            checked
            {
                r = Math.Sqrt(x1 * x1 + y1 * y1 + z1 * z1);
                return -k * q1 * q2 / r / r + g * Math.Exp(-l * r) / r / r;
            }
            return 0;
        }

        void DrawAxis()
        {
            Gl.glPushMatrix();
            //X axis
            Gl.glColor3d(255, 0, 0);
            Gl.glBegin(Gl.GL_LINES);
            Gl.glVertex3d(0, 0, 0);
            Gl.glVertex3d(32, 0, 0);
            Gl.glEnd();
            //Y axis
            Gl.glColor3d(0, 255, 0);
            Gl.glBegin(Gl.GL_LINES);
            Gl.glVertex3d(0, 0, 0);
            Gl.glVertex3d(0, 32, 0);
            Gl.glEnd();
            //Z axis
            Gl.glColor3d(0, 0, 255);
            Gl.glBegin(Gl.GL_LINES);
            Gl.glVertex3d(0, 0, 0);
            Gl.glVertex3d(0, 0, 32);
            Gl.glEnd();
            //Additional axis -rotateZ
            Gl.glColor3d(0, 0, 0);
            Gl.glBegin(Gl.GL_LINES);
            Gl.glVertex3d(0, 0, 0);
            vector_l_x = Math.Cos(DegreesToRad(-rotateZ)) * 8;
            vector_l_y = Math.Sin(DegreesToRad(-rotateZ)) * 8;
            Gl.glVertex3d(vector_l_x, vector_l_y, 0);
            Gl.glEnd();
            Gl.glPopMatrix();
        }
        void DrawTrajectory(double x1, double y1, double z1)
        {
            rotateL = Convert.ToDouble(trackBar2.Value);
            rotateZ = Convert.ToDouble(trackBar1.Value);
            zoom = Convert.ToDouble(textBox6.Text);
            scale = Convert.ToDouble(textBox1.Text);

            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);
            Gl.glPushMatrix();
            Gl.glRotated(rotateZ, 0, 0, 1);
            Gl.glRotated(360 - rotateL, vector_l_x, vector_l_y, 0);
            Gl.glScaled(zoom/5, zoom/5, zoom/5);

            DrawAxis();

            //Trajectory drawing
            for(double t = 0; t<st; t += dt / 1000)
            {
                F0 = force(x1, y1, z1) / (x1 * x1 + y1 * y1 + z1 * z1);
                Fx = F0 * x1;
                Fy = F0 * y1;
                Fz = F0 * z1;
                ax = Fx / m;
                ay = Fy / m;
                az = Fz / m;
                Vx1 += ax * dt;
                Vy1 += ay * dt;
                Vz1 += az * dt;
                x2 = x1 + Vx1 * dt + ax * dt * dt / 2;
                y2 = y1 + Vy1 * dt + ay * dt * dt / 2;
                z2 = z1 + Vz1 * dt + az * dt * dt / 2;
                Gl.glBegin(Gl.GL_LINES);
                Gl.glVertex3f((int)(x1 / scale), (int)(y1 / scale), (int)(z1 / scale));
                Gl.glVertex3f((int)(x2 / scale), (int)(y2 / scale), (int)(z2 / scale));
                Gl.glEnd();
                if ((int)x1 == 0 && (int)y1 == 0 && (int)z1 == 0) 
                {
                    x2 = 0;y2 = 0;z2 = 0;
                }
                x1 = x2;
                y1 = y2;
                z1 = z2;
            }
            Gl.glPopMatrix();
            Gl.glFlush();
            Screen.Invalidate();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            x1 = double.Parse(textBox7.Text);
            y1 = double.Parse(textBox8.Text);
            z1 = double.Parse(textBox2.Text);

            Vx1 = double.Parse(textBox9.Text);
            Vy1 = double.Parse(textBox10.Text);
            Vz1 = double.Parse(textBox3.Text);
            DrawTrajectory(x1, y1, z1);
        }
    }
}
