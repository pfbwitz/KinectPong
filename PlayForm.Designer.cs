namespace KinectPong
{
    partial class PlayForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlayForm));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.pause_txt = new System.Windows.Forms.Label();
            this.paddle2 = new System.Windows.Forms.PictureBox();
            this.paddle = new System.Windows.Forms.PictureBox();
            this.ball = new System.Windows.Forms.PictureBox();
            this.score2 = new KinectPong.Label.OrientedTextLabel();
            this.score1 = new KinectPong.Label.OrientedTextLabel();
            ((System.ComponentModel.ISupportInitialize)(this.paddle2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.paddle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ball)).BeginInit();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 10;
            this.timer1.Tick += new System.EventHandler(this.Update);
            // 
            // pause_txt
            // 
            this.pause_txt.AutoSize = true;
            this.pause_txt.ForeColor = System.Drawing.Color.White;
            this.pause_txt.Location = new System.Drawing.Point(259, 176);
            this.pause_txt.Name = "pause_txt";
            this.pause_txt.Size = new System.Drawing.Size(43, 13);
            this.pause_txt.TabIndex = 5;
            this.pause_txt.Text = "Paused";
            // 
            // paddle2
            // 
            this.paddle2.BackColor = System.Drawing.Color.Transparent;
            this.paddle2.Image = global::KinectPong.Properties.Resources.paddle;
            this.paddle2.Location = new System.Drawing.Point(539, 128);
            this.paddle2.Name = "paddle2";
            this.paddle2.Size = new System.Drawing.Size(32, 256);
            this.paddle2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.paddle2.TabIndex = 9;
            this.paddle2.TabStop = false;
            // 
            // paddle
            // 
            this.paddle.BackColor = System.Drawing.Color.Transparent;
            this.paddle.Image = global::KinectPong.Properties.Resources.paddle;
            this.paddle.Location = new System.Drawing.Point(12, 128);
            this.paddle.Name = "paddle";
            this.paddle.Size = new System.Drawing.Size(32, 256);
            this.paddle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.paddle.TabIndex = 6;
            this.paddle.TabStop = false;
            // 
            // ball
            // 
            this.ball.BackColor = System.Drawing.Color.Transparent;
            this.ball.Image = global::KinectPong.Properties.Resources.BallWithAlpha;
            this.ball.Location = new System.Drawing.Point(279, 304);
            this.ball.Margin = new System.Windows.Forms.Padding(0);
            this.ball.Name = "ball";
            this.ball.Size = new System.Drawing.Size(80, 80);
            this.ball.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ball.TabIndex = 8;
            this.ball.TabStop = false;
            // 
            // score2
            // 
            this.score2.AutoSize = true;
            this.score2.BackColor = System.Drawing.Color.Transparent;
            this.score2.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.score2.ForeColor = System.Drawing.Color.White;
            this.score2.Location = new System.Drawing.Point(447, 238);
            this.score2.Name = "score2";
            this.score2.RotationAngle = 0D;
            this.score2.Size = new System.Drawing.Size(27, 29);
            this.score2.TabIndex = 11;
            this.score2.Text = "0";
            this.score2.TextDirection = KinectPong.Label.Direction.Clockwise;
            this.score2.TextOrientation = KinectPong.Label.Orientation.Rotate;
            // 
            // score1
            // 
            this.score1.AutoSize = true;
            this.score1.BackColor = System.Drawing.Color.Transparent;
            this.score1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.score1.ForeColor = System.Drawing.Color.White;
            this.score1.Location = new System.Drawing.Point(106, 238);
            this.score1.Name = "score1";
            this.score1.RotationAngle = 0D;
            this.score1.Size = new System.Drawing.Size(27, 29);
            this.score1.TabIndex = 10;
            this.score1.Text = "0";
            this.score1.TextDirection = KinectPong.Label.Direction.Clockwise;
            this.score1.TextOrientation = KinectPong.Label.Orientation.Rotate;
            // 
            // PlayForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(567, 398);
            this.Controls.Add(this.score2);
            this.Controls.Add(this.score1);
            this.Controls.Add(this.paddle2);
            this.Controls.Add(this.paddle);
            this.Controls.Add(this.pause_txt);
            this.Controls.Add(this.ball);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "PlayForm";
            this.Text = "Pong";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResizeEnd += new System.EventHandler(this.MovePaddles);
            this.Click += new System.EventHandler(this.Pause);
            ((System.ComponentModel.ISupportInitialize)(this.paddle2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.paddle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ball)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label pause_txt;
        private System.Windows.Forms.PictureBox paddle;
        private System.Windows.Forms.PictureBox ball;
        private System.Windows.Forms.PictureBox paddle2;
        private Label.OrientedTextLabel score1;
        private Label.OrientedTextLabel score2;
    }
}

