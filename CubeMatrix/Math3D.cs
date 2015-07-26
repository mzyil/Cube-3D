using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace CubeMatrix
{
    public class Math3D
    {

        private const float cubeSize = 20f;

        public class Vector3D
        {
            float x;
            float y;
            float z;

            public Vector3D(float x, float y, float z)
            {
                this.x = -x;
                this.y = -y;
                this.z = z;
            }
            public Vector3D(int x, int y, int z)
            {
                this.x = -x;
                this.y = -y;
                this.z = z;
            }
            public Vector3D(double x, double y, double z)
            {
                this.x = -(float)x;
                this.y = -(float)y;
                this.z = (float)z;
            }
            public float getX() { return this.x; }
            public float getY() { return this.y; }
            public float getZ() { return this.z; }

            public void setX(float x) { this.x = x; }
            public void setY(float y) { this.y = y; }
            public void setZ(float z) { this.z = z; }

            public Vector2D Convert2D(float angle)
            {
                //float xxx = (2 / (r - 1)) * n * x - (r + 1) / (r - 1);
                //float yyy = (2 * n / (t - b)) * n * y / x - (t + b) / (t - b);

                //float lastX = (2 * n * x / (r - 1) - (r + 1) * z / (r - 1));
                //float lastY = ((2 * n / (t - b)) * y - (t + b) / (t - b) * z);

                float scale = 250 / (250 + z);
                //float lastX = x * scale + 500 / 2 + 500;
                //float lastY = y * scale + 300 / 2;

                double lastX = (x - z) * Math.Cos(angle) * scale;
                double lastY = (y + (x + z) * Math.Sin(angle)) * scale;

                

                Vector2D vector2d = new Vector2D(lastX, lastY);
                return vector2d;
            }

            public string toString()
            {
                return "X=" + getX() + " | Y=" + getY() + " | " + getZ();
            }

        }
        public class Vector2D
        {
            float x;
            float y;

            public Vector2D(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
            public Vector2D(double x, double y)
            {
                this.x = (float)x;
                this.y = (float)y;
            }
            public Vector2D(float x, float y)
            {
                this.x = x;
                this.y = y;
            }

            public float getX()
            {
                return this.x;
            }
            public float getY()
            {
                return this.y;
            }

            public void setX(int x)
            {
                this.x = x;
            }
            public void setY(int y)
            {
                this.y = y;
            }

            public string toString()
            {
                return "X:" + getX() + " | Y:" + getY();
            }

        }
        public class Line
        {
            Vector3D v1;
            Vector3D v2;

            public Line(Vector3D v1, Vector3D v2)
            {
                this.v1 = v1;
                this.v2 = v2;
            }

            public string toString()
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(" X | Y | Z");
                sb.AppendLine(" " + v1.getX() + " | " + v1.getY() + " | " + v1.getZ());
                sb.AppendLine(" " + v2.getX() + " | " + v2.getY() + " | " + v2.getZ());
                return sb.ToString();
            }
        }
        public class Plane
        {
            Vector3D v1;
            Vector3D v2;
            Vector3D v3;
            Vector3D v4;

            public Plane(Vector3D v1, Vector3D v2, Vector3D v3, Vector3D v4)
            {
                this.v1 = v1;
                this.v2 = v2;
                this.v3 = v3;
                this.v4 = v4;
            }

            public string toString()
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(" X | Y | Z");
                sb.AppendLine(" " + v1.getX() + " | " + v1.getY() + " | " + v1.getZ());
                sb.AppendLine(" " + v2.getX() + " | " + v2.getY() + " | " + v2.getZ());
                sb.AppendLine(" " + v3.getX() + " | " + v3.getY() + " | " + v3.getZ());
                sb.AppendLine(" " + v4.getX() + " | " + v4.getY() + " | " + v4.getZ());
                return sb.ToString();

            }
        }

        public class Cube
        {
            // 8 Vector3D tüm köşe noktaları
            Vector3D[] vertexes = new Vector3D[8];

            // varsayılan küp orta noktası verilirse
            // diğer köşelere olan uzaklığı eşit olduğu için 
            // 1 v3d için tüm 8 nokta hesaplanır
            public Cube(float x, float y, float z)
            {
                vertexes[0] = new Vector3D(x - cubeSize, y - cubeSize, z - cubeSize);
                vertexes[1] = new Vector3D(x + cubeSize, y - cubeSize, z - cubeSize);
                vertexes[2] = new Vector3D(x + cubeSize, y - cubeSize, z + cubeSize);
                vertexes[3] = new Vector3D(x - cubeSize, y - cubeSize, z + cubeSize);
                vertexes[4] = new Vector3D(x - cubeSize, y + cubeSize, z - cubeSize);
                vertexes[5] = new Vector3D(x + cubeSize, y + cubeSize, z - cubeSize);
                vertexes[6] = new Vector3D(x + cubeSize, y + cubeSize, z + cubeSize);
                vertexes[7] = new Vector3D(x - cubeSize, y + cubeSize, z + cubeSize);
            }

            public Vector3D getVector3D(int i)
            {
                if (i > 8 || i < 0)
                {
                    throw new System.IndexOutOfRangeException("You can use only [0-8] paramaters");
                }
                return vertexes[i];
            }

            public string toString()
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(" #    | X     | Y     | Z    ");
                for (int i = 0; i < 8; i++)
                {
                    sb.AppendLine(" " + i + "    | " + getVector3D(i).getX() + "     | " + getVector3D(i).getY() + "     | " + getVector3D(i).getZ());
                }
                return sb.ToString();
            }
        }
        public class Camera
        {
            float zoom;
            float angle;
            Vector3D location;
            Vector3D rotation; // emin değilim rotasyon 2 adet vector den oluşabilir

            public Camera(float zoom, float angle, Vector3D location, Vector3D rotation)
            {
                this.zoom = zoom;
                this.location = location; // taşıma algoritması için kullanılacak
                this.rotation = rotation; // 
                this.angle = angle;
            }
            public int getX()
            {
                return (int)location.getX();
            }
            public int getY()
            {
                return (int)location.getY();
            }
            public int getZ()
            {
                return (int)location.getZ();
            }
            public void Render(Cube cube, Graphics g, Pen p, Vector2D origin)
            {
                //
                Point tempP1 = new Point();
                Point tempP2 = new Point();
                float x1, y1;
                float x2, y2;
                for (int i = 0; i <= 3; i++)
                {
                    x1 = cube.getVector3D(i * 2).Convert2D(angle).getX() + (int)origin.getX() - zoom * 50;
                    y1 = cube.getVector3D(i * 2).Convert2D(angle).getY() + (int)origin.getY() - zoom * 100;

                    x2 = cube.getVector3D(i * 2 + 1).Convert2D(angle).getX() + (int)origin.getX() - zoom * 50;
                    y2 = cube.getVector3D(i * 2 + 1).Convert2D(angle).getY() + (int)origin.getY() - zoom * 100;

                    x1 *= zoom;
                    x2 *= zoom;
                    y1 *= zoom;
                    y2 *= zoom;



                    Point p1 = new Point((int)x1, (int)y1);
                    Point p2 = new Point((int)x2, (int)y2);

                    Point p3 = new Point((int)x1, (int)x2);
                    Point p4 = new Point((int)x2, (int)y2);

                    if (i > 0)
                    {
                        g.DrawLine(p, p2, tempP1);
                        g.DrawLine(p, p1, tempP2);
                    }

                    tempP1 = p1;
                    tempP2 = p2;
                    g.DrawLine(p, p1, p2);
                }

            }
            public void RenderArray(Cube[] cube, Graphics g, Pen p, Vector2D origin)
            {
                for (int i = 0; i < cube.Length; i++)
                {
                    Render(cube[i], g, p, origin);
                }
            }
        }
    }
}