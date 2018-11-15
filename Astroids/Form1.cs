using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace Astroids
{
    public partial class MainForm : Form
    {
        private System.Timers.Timer frameTimer;

        private bool thruster, rotateRight, rotateLeft, shoot;
        private Point center;

        private Player player1;
        private Alien alien1;

        private int playerSpawnDelay = 100;
        private int alienSpawnDelay = 1000;
        private int playerSpawnTime = 0;
        private int alienSpawnTime = 0;
        private int playerLives = 3;
        private static int playerScore = 0;

        private int minNumAstroids = 4;
        private int numAstroids;
        private int asteroidSize = 60;

        public static int PlayerScore
        {
            get { return playerScore; }
            set { playerScore = value; }
        }

        public MainForm()
        {
            InitializeComponent();
            center = new Point((this.Width - 16) / 2, (this.Height - 39) / 2);

            player1 = new Player(center.X, center.Y, 25);
            alien1 = new Alien(0, 200, 40);
            alien1.Life = 0;

            CreateTimer();
        }

        public void CreateTimer()
        {
            frameTimer = new System.Timers.Timer();
            frameTimer.Interval = 10; //milliseconds
            frameTimer.Elapsed += Update;
            frameTimer.AutoReset = true;
            frameTimer.Enabled = true;
            frameTimer.Stop(); //only works when click the button
        }
        private void Update(object sender, ElapsedEventArgs e) //code runs every frame
        {
            SpaceObject.CheckDestroyed(); //removes objects that life is zero
            SpaceObject.CheckOutOfBounds(this.Width - 16, this.Height - 39, 20);
            SpaceObject.MoveAll(); //moves all objects that exist
            ControlPlayer();
            alien1.Shoot(player1.X, player1.Y);
            RespawnPlayer();
            RespawnAlien();
            NewRound(); //checks for new round

            lives.Text = "Lives = " + Convert.ToString(playerLives);
            score.Text = "Score = " + Convert.ToString(playerScore);

            this.Invalidate(); //repaints the form
        }
        public void RespawnPlayer()
        {
            if (player1.Life <= 0 && playerLives > 0)
            {
                if (playerSpawnTime >= playerSpawnDelay)
                {
                    player1.Life = 100;
                    player1.MoveTo(center.X, center.Y);
                    playerSpawnTime = 0;
                    playerLives--;
                }
                else
                {
                    playerSpawnTime += 1;
                }
            }

            if (playerLives <= 0)
            {
                EndGame();
            }
        }
        public void RespawnAlien()
        {
            if (alien1.Life <= 0 && playerLives > 0)
            {
                if (alienSpawnTime >= alienSpawnDelay)
                {
                    alien1.Life = 1000;
                    alien1.MoveTo(0, 200);
                    alienSpawnTime = 0;
                }
                else
                {
                    alienSpawnTime += 1;
                }
            }
        }
        public void SpawnAstroids(int amount, int size) //spawns a number of asteroids off screen
        {
            Random rand = new Random();
            int posX, posY, angle;

            for (int i = 0; i < amount; i++) 
            {
                angle = rand.Next(0, 360); //random angle to move

                if (rand.Next(0, 2) == 0) //random x
                {
                    posX = rand.Next(0, this.Width - 16 + 1);
                    posY = -20; //outside map
                }
                else //random y
                {
                    posY = rand.Next(0, this.Height - 39 + 1);
                    posX = -20; //outside map
                }

                Asteroid a1 = new Asteroid(posX, posY, size);
                a1.RotateTo(angle);
            }
        }

        private void ControlPlayer() //checks each key bool every frame
        {
            if (thruster) //thrust
            {
                player1.Start();
            }
            else
            {
                player1.Stop();
            }

            if (!(rotateRight && rotateLeft)) //if both are pressed, don't rotate
            {
                if (rotateRight) //rotate right
                {
                    player1.Rotate(-4);
                }
                if (rotateLeft == true) //rotate left
                {
                    player1.Rotate(4);
                }
            }

            if (shoot)
            {
                player1.Shoot();
                player1.CanShoot = false; //can only shoot 1 time per shoot press
            }
        }
        private void Key_Down(object sender, KeyEventArgs e) //sets bools to allow multiple key inputs
        {
            if (e.KeyCode == Keys.Up) //thrust
            {
                thruster = true;
            }
            if (e.KeyCode == Keys.Right) //right
            {
                rotateRight = true;
            }
            if (e.KeyCode == Keys.Left) //left
            {
                rotateLeft = true;
            }
            if (e.KeyCode == Keys.F) //shoot
            {
                shoot = true;
            }
        }
        private void Key_Up(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up) //thrust
            {
                thruster = false;
            }
            if (e.KeyCode == Keys.Right) //right
            {
                rotateRight = false;
            }
            if (e.KeyCode == Keys.Left) //left
            {
                rotateLeft = false;
            }
            if (e.KeyCode == Keys.F) //shoot
            {
                shoot = false;
                player1.CanShoot = true; 
            }
        }

        private void NewGame()
        {
            playerLives = 3;
            playerScore = 0;
            alienSpawnTime = 0;
            alien1.Life = 0;
            numAstroids = minNumAstroids;

            SpawnAstroids(numAstroids, asteroidSize);

            frameTimer.Start();

            newGame.Hide();
            newGame.Enabled = false;
        }
        private void NewRound()
        {
            if (SpaceObject.ContainsAsteroids() == false)
            {
                numAstroids += 1;
                SpawnAstroids(numAstroids, asteroidSize);
            }
        }
        private void EndGame()
        {
            SpaceObject.DestroyAsteroids(); //Destroys all asteroids

            frameTimer.Stop();
            newGame.Enabled = true;
            newGame.Show();
        }
        private void Click_NewGame(object sender, EventArgs e)
        {
            NewGame();
        }

        private void PaintForm(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias; //smoothing

            SpaceObject.PaintAll(e); //paints all objects
        }
    }
}
