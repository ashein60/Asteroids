using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Astroids
{
    public class Laser : SpaceObject
    {
        public Laser(float x, float y, int size, int angle, string type) : base(x, y, size) //type = LaserP, LaserA
        {
            this.type = type;
            RotateTo(angle); //allows immediate movement
            speed = 8; //default
        }

        public override void Move()
        {
            if (life > 0)
            {
                int hit = TestHit();

                if (hit != -1) //if hit an object
                {
                    if (spaceObjects[hit].Type == "Alien" && type == "LaserP") //alien hits player laser
                    {
                        life = 0;
                        spaceObjects[hit].Life = 0;
                        MainForm.PlayerScore += 500;
                    }
                    else if (spaceObjects[hit].Type == "Player" && type == "LaserA") //Player hits alien laser
                    {
                        life = 0;
                        spaceObjects[hit].Life = 0;
                    }
                    else if (spaceObjects[hit].Type == "LaserA" && type == "LaserP") //enemy laser hits player laser
                    {
                        life = 0;
                        spaceObjects[hit].Life = 0;
                    }
                    //Astroid and player laser handled in astroid class
                }

                if (life > 0) //test again 
                {
                    base.Move();
                    life -= 2;
                }
            }
        }
        
        public override void Paint(PaintEventArgs e)
        {
            Brush laserBrush = new SolidBrush(Color.White);

            e.Graphics.FillEllipse(laserBrush, x - size / 2, y - size / 2, size, size);

            laserBrush.Dispose();
        }
    }
}
