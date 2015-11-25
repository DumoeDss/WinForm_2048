using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace moeGame
{
    public partial class GameOver : Form
    {
        public GameOver()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            Identify._login.Close();
        }

        private void GameOver_Load(object sender, EventArgs e)
        {
            label3.Hide();
            label6.Hide();
            if (MainForm.newBestGrade)
            {
                label3.Show();
            }
            if (Game.perGrade)
            {
                label6.Show();
            }
            //int grade =Convert.ToInt32(MainForm.strGrade);
            label2.Text = "";
            label2.Text += Game.lastGrade;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Identify._mainform.Close();
            Identify._mainform.Dispose();
            this.timer2.Interval = 10;
            this.timer2.Enabled = true;
            this.timer2.Start();
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.Opacity < 1)
            {
                this.Opacity += 0.02;
                if (this.Opacity > 0.9)
                {
                    this.timer1.Stop();
                }
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (this.Opacity > 0)
            {
                this.Opacity -= 0.02;
                if (this.Opacity < 0.1)
                {
                    MainForm mf = new MainForm();
                    mf.Opacity = 0;
                    mf.Show();
                    mf.timer1.Interval = 10;
                    mf.timer1.Enabled = true;
                    mf.timer1.Start();
                    this.Close();

                }
            }
        }

        private int oldX = 0;
        private int oldY = 0;

        private void GameOver_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.Location.X - this.oldX;
                this.Top += e.Location.Y - this.oldY;
            }
        }

        private void GameOver_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.oldX = e.Location.X;
                this.oldY = e.Location.Y;
            }
        }
    }
}
