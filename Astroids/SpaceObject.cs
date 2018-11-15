using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Astroids
{
    public abstract class SpaceObject
    {
        protected static List<SpaceObject> spaceObjects = new List<SpaceObject>(); //keeps track of all objects, even child classes

        protected string type; //Astroid, Player, Alien, LaserP, LaserA
        protected float x, y, deltaX, deltaY;
        protected float speed;
        protected int size, angle;
        protected int life;

        public float X
        {
            get { return x; }
        }
        public float Y
        {
            get { return y; }
        }
        public float Size
        {
            get { return size; }
        }
        public int Life
        {
            get { return life; }
            set { life = value; }
        }
        public string Type
        {
            get { return type; }
        }

        protected SpaceObject(float x, float y, int size)
        {
            this.x = x;
            this.y = y;
            this.size = size;

            speed = 2; //default
            life = 100; //default

            spaceObjects.Add(this); //keeps track of everything
        }

        public virtual void Move() //moves an individual object
        {
            x += (deltaX * speed);
            y += (deltaY * speed);
        }
        public static void MoveAll() //moves all objects in the list
        {
            for (int i = 0; i < spaceObjects.Count; i++)
            {
                spaceObjects[i].Move();
            }
        }

        private void SetDeltas() //sets the deltas according to the angle
        {
            double Radians = angle * Math.PI / 180; //converts to radians

            deltaX = (float)Math.Cos(Radians);
            deltaY = -(float)Math.Sin(Radians); //negative so that +y is up
        }
        public void Rotate(int angle)
        {
            this.angle += angle;
            SetDeltas();
        }
        public void RotateTo(int angle)
        {
            this.angle = angle;
            SetDeltas();
        }

        protected int TestHit() //if an object hits any object
        {
            int hit = -1; //default if no contact

            for (int i = 0; i < spaceObjects.Count; i++)
            {
                if (spaceObjects[i].Life < 0) //can't hit destroyed
                {
                    continue; //skips the destroyed player or alien
                }
                if (x == spaceObjects[i].X && y == spaceObjects[i].Y)
                {
                    continue; //if exactly the same coordinates, skip, because it's the laser
                }

                //this tests in the range of a circle around the object position
                float distanceX = Math.Abs(x - spaceObjects[i].X); //gets the line distance
                float distanceY = Math.Abs(y - spaceObjects[i].Y);
                float final = (distanceX * distanceX) + (distanceY * distanceY);

                if (final <= (((spaceObjects[i].Size + size)/ 2) * ((spaceObjects[i].Size + size) / 2))) //if distance is less than the radius of the object
                {
                    hit = i;
                    break; //can only hit 1 object
                }
            }

            return hit;
        }
        public static void CheckOutOfBounds(int formWidth, int formHeight, int extraSpace) //checks if an object hits a wall, extraSpace +- all boarders
        {
            foreach (SpaceObject spaceObject in spaceObjects) //moves all objects to the opposite wall
            {
                if (spaceObject.x >= formWidth + extraSpace) //right wall
                {
                    if (spaceObject.type == "Alien") //Alien will vanish after showing up
                    {
                        spaceObject.life = 0;
                    }
                    else
                    {
                        spaceObject.x = -extraSpace;
                    }
                }
                else if (spaceObject.x <= -extraSpace) //left wall
                {
                    spaceObject.x = formWidth + extraSpace;
                }
                if (spaceObject.y >= formHeight + extraSpace) //bottom wall
                {
                    spaceObject.y = -extraSpace;
                }
                else if (spaceObject.y <= -extraSpace) //top wall
                {
                    spaceObject.y = formHeight + extraSpace;
                }
            }
        }
        public static void CheckDestroyed() //if life == 0, remove it
        {
            for (int i = 0; i < spaceObjects.Count; i++)
            {
                if (spaceObjects[i].life <= 0 && (spaceObjects[i].type == "Asteroid" || spaceObjects[i].type == "LaserP" || spaceObjects[i].type == "LaserA"))
                {
                    spaceObjects.RemoveAt(i);
                }
            }
        }

        public static void DestroyAsteroids()
        {
            for (int z = 0; z < 3; z ++) //asteroids have 3 "lives"
            {
                for (int i = 0; i < spaceObjects.Count; i++)
                {
                    if (spaceObjects[i].type == "Asteroid")
                    {
                        spaceObjects[i].life = 0;
                    }
                }
            }
        }
        public static bool ContainsAsteroids()
        {
            bool contains = false;

            for (int i = 0; i < spaceObjects.Count; i++)
            {
                if (spaceObjects[i].type == "Asteroid")
                {
                    contains = true;
                    break;
                }
            }

            return contains;
        }
        public abstract void Paint(PaintEventArgs e);
        public static void PaintAll(PaintEventArgs e)
        {
            for (int i = 0; i < spaceObjects.Count; i++)
            {
                spaceObjects[i].Paint(e);
            }
            /*
            foreach (SpaceObject spaceObject in spaceObjects) //this doesn't work with too many lasers?
            {
                spaceObject.Paint(e);
            }
            */
        }
    }
}
