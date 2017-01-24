using KinectPong.Kinect;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace KinectPong
{
    public partial class PlayForm : Form
    {
        #region properties

        private KinectHelper _kinectHelper;

        private const int _speedLimit = 64;
        private const int _defaultSpeed = 10;

        private const int _paddle2Speed = 5;

        private const int _paddleBaseHeight = 256;

        private int _xSpeed;
        private int _ySpeed;
        private int _lastYPlayer1;
        private int _lastYPlayer2;
        private int _scorePlayer1;
        private int _scorePlayer2;
        private int _topBounds;
        private int _bottomBounds;
        private int _leftBounds;
        private int _rightBounds;
        private bool _paused;

        #endregion

        #region handlers

        private void KinectHelper_PositionDetermined(object sender, PaddlePositionEventArgs e)
        {
            if (e.PlayerNumber == 1)
                MovePlayer(e.YPercentage * Height / 100);
            else
                MoveCpu(e.YPercentage * Height / 100);
        }

        private void MovePaddles(object sender, EventArgs e)
        {
            //Properly position the paddles
            paddle.Location = new Point(_leftBounds + 46, paddle.Location.Y);
            paddle2.Location = new Point(_rightBounds - 12, paddle2.Location.Y);
            pause_txt.Location = new Point(_rightBounds / 2 - pause_txt.Width / 2, _bottomBounds / 2 - pause_txt.Height / 2);
        }

        private void Pause(object sender, EventArgs e)
        {
            _paused = !_paused;
            pause_txt.Visible = _paused;
        }

        //Double Buffer (required function)
        protected override void OnPaint(PaintEventArgs pe)
        {
        }

        private void Update(object sender, EventArgs e)
        {
            if (!_paused)
            {
                MovePlayer();

                MoveCpu();

                MoveBall();

                MeasurePlayerCollision();

                MeasureCpuCollision();
            }
        }

        #endregion

        #region methods

        private void MoveBall()
        {
            ball.Location = new Point(ball.Location.X + _xSpeed, ball.Location.Y + _ySpeed);
           
            if (ball.Location.Y <= _topBounds)  //Ball Control:  Top Wall
            {
                _ySpeed *= -1;
                while (ball.Location.Y - 1 < _topBounds)
                    ball.Location = new Point(ball.Location.X, ball.Location.Y + 1);
            }
            else if (ball.Location.Y + ball.Height > _bottomBounds)   //Ball Control:  Bottom Wall
            {
                _ySpeed *= -1;
                while (ball.Location.Y + 1 + ball.Height > _bottomBounds)
                    ball.Location = new Point(ball.Location.X, ball.Location.Y - 1);
            }

            if (ball.Location.X > _rightBounds) //Ball Control: Player Scoring
            {
                _scorePlayer1++;
                score1.Text = _scorePlayer1.ToString();
                InsertBall();
            }
            else if (ball.Location.X < _leftBounds) //Ball Control - Player 2 Scoring
            {
                _scorePlayer2++;
                score2.Text = _scorePlayer2.ToString();
                InsertBall();
            }

            _lastYPlayer1 = MousePosition.Y;
            _lastYPlayer2 = paddle2.Location.Y;
        }

        private void MovePlayer()
        {
            MovePlayer(MousePosition.Y);
        }

        private void MovePlayer(int y)
        {
            paddle.Location = new Point(paddle.Location.X, (y - paddle.Height));
            if (paddle.Location.Y + paddle.Height > Height)
                paddle.Location = new Point(paddle.Location.X, Height - paddle.Height);
            if (paddle.Location.Y < 0)
                paddle.Location = new Point(paddle.Location.X, 0);
        }

        private void MoveCpu()
        {
            if (Program.IsTwoPlayerMode)
                return;
            if (ball.Location.Y > paddle2.Location.Y)
                paddle2.Location = new Point(paddle2.Location.X, paddle2.Location.Y + _paddle2Speed);
            else
                paddle2.Location = new Point(paddle2.Location.X, paddle2.Location.Y - _paddle2Speed);
        }

        private void MoveCpu(int y)
        {
            paddle2.Location = new Point(paddle2.Location.X, (y - paddle2.Height));
            if (paddle2.Location.Y + paddle2.Height > Height)
                paddle2.Location = new Point(paddle2.Location.X, Height - paddle2.Height);
            if (paddle2.Location.Y < 0)
                paddle2.Location = new Point(paddle2.Location.X, 0);
        }

        private void SetDimensions()
        {
            _topBounds = 0;
            _bottomBounds = Height;
            _leftBounds = 0;
            _rightBounds = Width;

            paddle2.Location = new Point(Width - paddle2.Width*2 - 64, Height/2 - paddle2.Height/2);
            score2.Location = new Point(Width - score2.Width * 2 - 200, Height / 2 - score2.Height / 2);
            paddle.Location = new Point(paddle.Width * 2 + 64, Height / 2 - paddle.Height / 2);
            score1.Location = new Point(score1.Width * 2 + 200, Height / 2 - score1.Height / 2);

            ball.Location = new Point(Width/2 - ball.Width/2, Height/2 - ball.Height/2);
            pause_txt.Location = new Point(Width / 2 - ball.Width / 2, Height / 2 - ball.Height / 2);
            _scorePlayer1 = 0;
            _scorePlayer2 = 0;
            score1.Text = _scorePlayer1.ToString();
            score2.Text = _scorePlayer2.ToString();
        }

        private void InsertBall()
        {
            ball.Location = new Point(Width/2 - ball.Width/2, Height/2 - ball.Height/2);
            _ySpeed = new Random().NextDouble() > .5 ? _defaultSpeed : -_defaultSpeed;
            _xSpeed = -_defaultSpeed;
        }

        private void MeasureCpuCollision()
        {
            if (ball.Location.X + ball.Width > paddle2.Location.X &&
                ball.Location.X < paddle2.Location.X + paddle2.Width &&
                ball.Location.Y > (paddle2.Location.Y - ball.Height / 2) &&
                ball.Location.Y + ball.Height < paddle2.Location.Y + paddle.Height + ball.Height / 2
                )
            {
                SetBallSpeed();
                ShrinkPaddle(paddle2);

                while (ball.Location.X + 1 > paddle2.Location.X + paddle2.Width)
                    ball.Location = new Point(ball.Location.X - 1, ball.Location.Y);
            }
        }

        private void MeasurePlayerCollision()
        {
            if (ball.Location.X > paddle.Location.X &&
                ball.Location.X < (paddle.Location.X + paddle.Width) &&
                ball.Location.Y > paddle.Location.Y &&
                ball.Location.Y - ball.Height < paddle.Location.Y + paddle.Height
                )
            {
                SetBallSpeed();
                ShrinkPaddle(paddle);

                while (ball.Location.X + 1 + ball.Width < paddle.Location.X)
                    ball.Location = new Point(ball.Location.X - 1, ball.Location.Y);
            }
        }

        private void ShrinkPaddle(PictureBox p)
        {
            if (p.Height == _paddleBaseHeight / 4)
                return;

            p.Height = Convert.ToInt32(p.Height * 0.9);
            if (p.Height < _paddleBaseHeight / 4)
                p.Height = _paddleBaseHeight / 2;
        }

        public void SetBallSpeed()
        {
            _xSpeed *= -1;

            if (_xSpeed < 0)
                _xSpeed -= 1;
            else
                _xSpeed += 1;

            if (_xSpeed < 0)
                _ySpeed -= 1;
            else
                _ySpeed += 1;

            if (_ySpeed > _speedLimit)
                _ySpeed = _speedLimit;
            else if (_ySpeed < -_speedLimit)
                _ySpeed = -_speedLimit;
            else if (_ySpeed == 0)
                _ySpeed = new Random().NextDouble() > .5 ? _defaultSpeed : -_defaultSpeed;
        }

        #endregion

        public PlayForm()
        {
            InitializeComponent();

            SizeChanged += (s, a) => SetDimensions();
            WindowState = FormWindowState.Normal;
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;

            _xSpeed = _defaultSpeed;
            _ySpeed = _defaultSpeed;
            _scorePlayer1 = 0;
            _scorePlayer2 = 0;

            score1.RotationAngle = 90;
            score2.RotationAngle = 270;

            ball.Enabled = false;
            paddle.Enabled = false;
            pause_txt.Visible = false;

            //Last mouse X position stored here (so we can add curve based on how fast the mouse was moved)
            _lastYPlayer1 = MousePosition.Y;
            _lastYPlayer2 = paddle2.Location.Y;

            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);

            _kinectHelper = new KinectHelper();
            _kinectHelper.PositionDetermined += KinectHelper_PositionDetermined;
            _kinectHelper.Start();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            _kinectHelper.Dispose();
        }
    }
}