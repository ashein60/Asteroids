using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Astroids
{
    public class Player : SpaceObject
    {
        private bool moving; //draws a flame if moving
        private bool canShoot; //can shoot a laser
        private float maxSpeed, accelerator; //speeds up, slows down, and drifts

        public bool CanShoot
        {
            get { return canShoot; }
            set { canShoot = value; }
        }
        public Player(float x, float y, int size) : base(x, y, size)
        {
            type = "Player";

            RotateTo(90); //90 is up
            speed = 0;
            maxSpeed = 6;
            accelerator = .1f;
            canShoot = true;
            moving = false;
        }

        public override void Move()
        {
            if (life > 0)
            {
                base.Move();
            }
        }

        public void Start() //starts movement
        {
            if (speed < maxSpeed) //speed up
            {
                speed += accelerator;
            }
            else if (speed > maxSpeed) //limiter
            {
                speed = maxSpeed;
            }

            moving = true;
        }
        public void Stop() //stops movement
        {
            if (speed > 0) //slow down
            {
                speed -= accelerator;
            }
            else if (speed < 0) //complete stop
            {
                speed = 0;
            }

            moving = false;
        }
        public void MoveTo(float x, float y) //for respawning the alien
        {
            this.x = x;
            this.y = y;
        } 

        public void Shoot() //can only shoot while not running Start()
        {
            if (canShoot && life > 0)
            {
                Laser laser1 = new Laser(x, y, 4, angle, "LaserP");
            }
        }
        private void RotatePoints(Point[] points) //rotates all points around the coordinates of the object for painting
        {
            float distanceX, distanceY, radian;

            for (int i = 0; i < points.Length; i++)
            {
                distanceX = points[i].X - this.x;
                distanceY = points[i].Y - this.y;

                radian = (float)((-angle + 90) * (Math.PI / 180)); //angle in radians, to be accurate, -angle + 90

                points[i].X = (int)Math.Round((points[i].X - distanceX) + (distanceX * Math.Cos(radian) - distanceY * Math.Sin(radian)));
                points[i].Y = (int)Math.Round((points[i].Y - distanceY) + (distanceX * Math.Sin(radian) + distanceY * Math.Cos(radian)));
            }
        }
        public override void Paint(PaintEventArgs e)
        {
            if (life > 0)
            {
                Pen playerPen = new Pen(Color.White);
                playerPen.Width = 2;
                Point[] points = new Point[7]; //all points
                Point[] body = new Point[3]; //trinagle part of ship
                Point[] arc = new Point[3]; //bottom part of ship

                points[0] = new Point((int)(x - size / 3), (int)(y + size / 2)); //left
                points[1] = new Point((int)(x), (int)(y - size / 2)); //up
                points[2] = new Point((int)(x + size / 3), (int)(y + size / 2)); //right
                points[3] = new Point((int)x, (int)y + (size / 3)); //arc point

                points[4] = new Point((int)x, (int)(y + (size / 1.5))); //thruster point left

                RotatePoints(points); //rotates the points around the ship position

                body[0] = points[0];
                body[1] = points[1];
                body[2] = points[2];

                arc[0] = points[0]; //arc to look more like a ship
                arc[1] = points[3];
                arc[2] = points[2];

                e.Graphics.DrawLines(playerPen, body);
                e.Graphics.DrawCurve(playerPen, arc);

                if (moving == true) //if moving, draw thrusters
                {
                    e.Graphics.DrawLine(playerPen, points[3].X, points[3].Y, points[4].X, points[4].Y);
                }

                playerPen.Dispose();
            }
        }
    }
}
