using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Astroids
{
    public class Asteroid : SpaceObject
    {
        private static Random rand = new Random();
        private Point[] points = new Point[15];
        private int version; //large, medium, small, destroy: 0, 1 , 2

        public Asteroid(float x, float y, int size) : base(x, y, size)
        {
            type = "Asteroid";
            version = 0;
            CreatePoints();
        }

        public override void Move()
        {
            if (life > 0)
            {
                int hit = TestHit();

                if (hit != -1) //if hit an object
                {
                    if (spaceObjects[hit].Type == "LaserP") //player laser hits asteroid
                    {
                        life = 0;
                        spaceObjects[hit].Life = 0;
                        CreateFromDestroyed();
                        MainForm.PlayerScore += 100;
                    }
                    else if (spaceObjects[hit].Type == "Player" && spaceObjects[hit].Life > 0) //asteroid hits living player
                    {
                        life = 0;
                        spaceObjects[hit].Life = 0;
                        CreateFromDestroyed();
                    }
                }

                if (life > 0) //test again 
                {
                    base.Move();
                }
            }
        }

        public void CreateFromDestroyed() //creates two new Astroids when the current is destroyed
        {
            Random rand = new Random();

            if (version < 2)
            {
                version += 1;
                speed += .5f;

                Asteroid a1 = new Asteroid(x, y, (int)(size / 1.5));
                a1.RotateTo(rand.Next(0, 360));
                a1.version = version;
                a1.speed = speed;

                Asteroid a2 = new Asteroid(x, y, (int)(size / 1.5));
                a2.RotateTo(rand.Next(0, 360));
                a2.version = version;
                a2.speed = speed;
            }
        }

        private void RotatePoints(ref Point points, float angle) //rotate the points around the origin
        {
            float distanceX, distanceY, radian;

            distanceX = points.X;
            distanceY = points.Y;

            radian = (float)((-angle + 90) * (Math.PI / 180)); //angle in radians, to be accurate, -angle + 90

            points.X = (int)Math.Round((points.X - distanceX) + (distanceX * Math.Cos(radian) - distanceY * Math.Sin(radian)));
            points.Y = (int)Math.Round((points.Y - distanceY) + (distanceX * Math.Sin(radian) + distanceY * Math.Cos(radian)));
        }
        private void CreatePoints() //creates the random points for the astroid
        {
            int rotateAmount = 360 / points.Length;

            for (int i = 0; i < points.Length; i++)
            {
                int length = rand.Next(size / 4, size / 2);
                points[i] = new Point(length, 0);
                RotatePoints(ref points[i], rotateAmount * i);
            }
        }
        public override void Paint(PaintEventArgs e)
        {
            Point[] paintPoints = new Point[points.Length];

            for (int i = 0; i < paintPoints.Length; i++)
            {
                paintPoints[i] = new Point((int)(points[i].X + x), (int)(points[i].Y + y));
            }

            Pen astroidPen = new Pen(Color.White);
            astroidPen.Width = 2;
            e.Graphics.DrawPolygon(astroidPen, paintPoints);

            astroidPen.Dispose();
        }
    }
}
