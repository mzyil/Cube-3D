using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace CubeMatrix
{
    public partial class CubeMatrix : Form
    {
        public CubeMatrix()
        {
            InitializeComponent();
            origin = new Math3D.Vector2D(pictureBox1.Width / 2, pictureBox1.Height / 2);


        }

        Math3D.Camera camera;
        Graphics g;
        int angle = 75;
        Math3D.Vector2D origin;
        float zoom = 1f;
        Math3D.Cube[] cubes;
        List<Math3D.Cube> cube = new List<Math3D.Cube>();
        private void CubeMatrix_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Application.Exit();
        }

        private void CubeMatrix_Load(object sender, EventArgs e)
        {
            cube.Add(new Math3D.Cube(0, 0, 0));




            for (int i = 0; i < 10; i++)
            {
                cube.Add(new Math3D.Cube(i * 40, 0, 0));
            }

            for (int i = 1; i < 10; i++)
            {
                cube.Add(new Math3D.Cube(0, 0, i * 40));
            }

            for (int i = 1; i < 10; i++)
            {
                cube.Add(new Math3D.Cube(360, 0, i * 40));
            }
            for (int i = 1; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {

                    cube.Add(new Math3D.Cube(0, i * 40, j * 40));
                }
            }

            //for (int i = 1; i < 10; i++)
            //{
            //    for (int j = 0; j < 10; j++)
            //    {

            //        cube.Add(new Math3D.Cube(360, i * 40, j*40));
            //    }
            //}

            DrawCubeOnCamera(angle, origin, zoom);
        }

        private void DrawCubeOnCamera(float angle, Math3D.Vector2D origin, float zoom)
        {
            Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics g = Graphics.FromImage(bmp);
            camera = new Math3D.Camera(zoom, angle, new Math3D.Vector3D(10, 10, 10), new Math3D.Vector3D(10, 10, 10));
            Pen p = new Pen(Brushes.GreenYellow, 1);

            cubes = cube.ToArray();
            camera.RenderArray(cubes, g, p, origin);
            pictureBox1.Image = bmp;
            g.Dispose();
        }



        // taşıma işlemi
        bool clicked = false;
        int oldLocationX;
        int oldLocationY;
        int oldMouseX;
        int oldMouseY;

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            clicked = true;
            oldLocationX = (int)origin.getX();
            oldLocationY = (int)origin.getY();

            oldMouseX = e.X;
            oldMouseY = e.Y;


        }
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            clicked = false;
        }
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (clicked == true)
            {
                origin.setX((int)(oldLocationX - (oldMouseX - e.X) / zoom));
                origin.setY((int)(oldLocationY - (oldMouseY - e.Y) / zoom));

                //for (int i = 0; i < cube.Count; i++)
                //{
                //    for (int j = 0; j < 8; j++)
                //    {
                //        cube[i].getVector3D(j).setX(cube[i].getVector3D(j).getX() - (oldMouseX - e.X) / 5);
                //        cube[i].getVector3D(j).setY(cube[i].getVector3D(j).getY() - (oldMouseY - e.Y) / 5);

                //    }
                //}
                DrawCubeOnCamera(angle, origin, zoom);
            }
        }
        private void pictureBox1_MouseWhell(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            Console.WriteLine(SystemInformation.MouseWheelScrollDelta * e.Delta);
            MessageBox.Show("Test");
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            float a = (float)(trackBar1.Value - 10) / 50f;
            zoom = a;
            label1.Text = "Zoom Level : " + zoom.ToString();
            DrawCubeOnCamera(angle, origin, zoom);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int createX = int.Parse(xBox.Text);
            int createY = int.Parse(yBox.Text);
            int createZ = int.Parse(zBox.Text);
            cube.Add(new Math3D.Cube(createX, createY, createZ));
            Render();
        }

        public void Render()
        {
            DrawCubeOnCamera(angle, origin, zoom);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            cube = new List<Math3D.Cube>();
            Render();

        }




    }
}