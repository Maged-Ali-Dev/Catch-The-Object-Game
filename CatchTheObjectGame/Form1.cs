using System;
using System.Drawing;
using System.Windows.Forms;
using WinFormsTimer = System.Windows.Forms.Timer;

namespace CatchTheObjectGame
{
    public partial class Form1 : Form
    {
        private Panel paddle;
        private PictureBox fallingObject;
        private Label scoreLabel;
        private WinFormsTimer gameTimer;
        private int score = 0;
        private int objectSpeed = 5;

        public Form1()
        {
            InitializeComponent();
            InitializeGame();
        }

        private void InitializeGame()
        {
            // Form properties
            this.Text = "Catch the Falling Object";
            this.Size = new Size(800, 600);
            this.KeyDown += new KeyEventHandler(OnKeyDown);

            // Paddle setup
            paddle = new Panel
            {
                Size = new Size(100, 20),
                BackColor = Color.Blue,
                Location = new Point(350, 500)
            };
            this.Controls.Add(paddle);

            // Falling object setup
            fallingObject = new PictureBox
            {
                Size = new Size(20, 20),
                BackColor = Color.Red,
                Location = new Point(400, 0)
            };
            this.Controls.Add(fallingObject);

            // Score label setup
            scoreLabel = new Label
            {
                Text = "Score: 0",
                Font = new Font("Arial", 16),
                Location = new Point(10, 10),
                AutoSize = true
            };
            this.Controls.Add(scoreLabel);

            // Timer setup
            gameTimer = new System.Windows.Forms.Timer
            {
                Interval = 20 // 20 milliseconds
            };
            gameTimer.Tick += new EventHandler(GameLoop);
            gameTimer.Start();
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            // Move paddle left or right
            if (e.KeyCode == Keys.Left && paddle.Left > 0)
            {
                paddle.Left -= 20;
            }
            else if (e.KeyCode == Keys.Right && paddle.Right < this.ClientSize.Width)
            {
                paddle.Left += 20;
            }
        }

        private void GameLoop(object sender, EventArgs e)
        {
            // Move falling object
            fallingObject.Top += objectSpeed;

            // Check for collision with paddle
            if (fallingObject.Bounds.IntersectsWith(paddle.Bounds))
            {
                score++;
                scoreLabel.Text = $"Score: {score}";
                ResetFallingObject();
            }

            // Check if object misses the paddle
            if (fallingObject.Top > this.ClientSize.Height)
            {
                ResetFallingObject();
            }
        }

        private void ResetFallingObject()
        {
            Random rnd = new Random();
            fallingObject.Location = new Point(rnd.Next(0, this.ClientSize.Width - fallingObject.Width), 0);
        }
    }
}
