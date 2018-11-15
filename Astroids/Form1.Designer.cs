namespace Astroids
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.newGame = new System.Windows.Forms.Button();
            this.score = new System.Windows.Forms.Label();
            this.lives = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // newGame
            // 
            this.newGame.BackColor = System.Drawing.Color.White;
            this.newGame.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.newGame.Font = new System.Drawing.Font("Microsoft Sans Serif", 40F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.newGame.Location = new System.Drawing.Point(200, 350);
            this.newGame.Name = "newGame";
            this.newGame.Size = new System.Drawing.Size(400, 100);
            this.newGame.TabIndex = 0;
            this.newGame.Text = "New Game";
            this.newGame.UseVisualStyleBackColor = false;
            this.newGame.Click += new System.EventHandler(this.Click_NewGame);
            // 
            // score
            // 
            this.score.AutoSize = true;
            this.score.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.score.ForeColor = System.Drawing.Color.White;
            this.score.Location = new System.Drawing.Point(0, 0);
            this.score.Name = "score";
            this.score.Size = new System.Drawing.Size(104, 25);
            this.score.TabIndex = 1;
            this.score.Text = "Score = 0";
            // 
            // lives
            // 
            this.lives.AutoSize = true;
            this.lives.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lives.ForeColor = System.Drawing.Color.White;
            this.lives.Location = new System.Drawing.Point(0, 25);
            this.lives.Name = "lives";
            this.lives.Size = new System.Drawing.Size(78, 20);
            this.lives.TabIndex = 2;
            this.lives.Text = "Lives = 3";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(784, 761);
            this.Controls.Add(this.lives);
            this.Controls.Add(this.score);
            this.Controls.Add(this.newGame);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Astroids";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.PaintForm);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Key_Down);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Key_Up);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button newGame;
        private System.Windows.Forms.Label score;
        private System.Windows.Forms.Label lives;
    }
}

