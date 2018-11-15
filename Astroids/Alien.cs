using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Astroids
{
    public class Alien : SpaceObject
    {
        private static int whenToShoot = 100; //frames to wait to shoot
        private int shootTimer; //current frame

        public Alien(float x, float y, int size) : base(x, y, size)
        {
            type = "Alien";
            RotateTo(0);
        }

        public override void Move()
        {
            if (life > 0)
            {
                base.Move();
            }
        }
        public void MoveTo(float x, float y) //for respawning the alien
        {
            this.x = x;
            this.y = y;
        }
        public void Shoot(float x, float y)
        {
            if (shootTimer >= whenToShoot && life > 0)
            {
                //aims at the current position of the player
                float relativeX = x - this.x; 
                float relativeY = y - this.y;
                double radian = Math.Atan(relativeY / relativeX);
                double angle = radian * (180 / Math.PI); //convert to angle
                int angleInt = (int)Math.Round(angle);

                if (relativeX < 0) //fixes angle aim
                {
                    angleInt += 180;
                }

                Laser laser1 = new Laser(this.x, this.y, 4, -angleInt, "LaserA"); //-angleInt fixes angle aim
                shootTimer = 0;
            }
            else
            {
                shootTimer += 1;
            }
        }
        public override void Paint(PaintEventArgs e)
        {
            if (life > 0)
            {
                Pen alienPen = new Pen(Color.White);
                alienPen.Width = 2;

                Point[] outline = new Point[8];
                outline[0] = new Point((int)(x - size / 4), (int)(y + size / 5)); //bottom left
                outline[1] = new Point((int)(x - size / 2), (int)(y)); //center left
                outline[2] = new Point((int)(x - size / 4), (int)(y - size / 5)); //top left
                outline[3] = new Point((int)(x - size / 6), (int)(y - size / 2.5)); //head left
                outline[4] = new Point((int)(x + size / 6), (int)(y - size / 2.5)); //head right
                outline[5] = new Point((int)(x + size / 4), (int)(y - size / 5)); //top right
                outline[6] = new Point((int)(x + size / 2), (int)(y)); //center right
                outline[7] = new Point((int)(x + size / 4), (int)(y + size / 5)); //bottom right

                e.Graphics.DrawLine(alienPen, x - size / 2, y, x + size / 2, y); //center body
                e.Graphics.DrawLine(alienPen, x - size / 4, y - size / 5, x + size / 4, y - size / 5); //between head and body

                e.Graphics.DrawPolygon(alienPen, outline);

                alienPen.Dispose();
            }
        }
    }
}
