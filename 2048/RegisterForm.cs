using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace moeGame
{
    public partial class RegisterForm : Form
    {
        DirectoryInfo dir = new DirectoryInfo(Application.StartupPath).Parent.Parent;
        //DirectoryInfo dir = new DirectoryInfo(Application.StartupPath).Parent;
        public string target;

        public RegisterForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;  //隐藏系统边框
        }

        private void bQuit_Click(object sender, EventArgs e)
        {
            this.Close();
            Identify._login.Show();
        }

        private void RegisterForm_Load(object sender, EventArgs e)
        {
            target = dir.FullName;
            pictureBox1.Image = Image.FromFile(target + @"\Resources\注册1.png");

            eUser.ForeColor = Color.FromArgb(150, 150, 150);

            ePasswd.ForeColor = Color.FromArgb(150, 150, 150);

            ePasswd2.ForeColor = Color.FromArgb(150, 150, 150);
            eUser.Text = "用户名";
            ePasswd.Text = "密码";
            ePasswd2.Text = "再次输入密码";
            eUser.AutoSize = false;
            eUser.Height = 25;
            ePasswd.AutoSize = false;
            ePasswd.Height = 25;
            ePasswd2.AutoSize = false;
            ePasswd2.Height = 25;
            this.BackColor = Color.FromArgb(235, 242, 250);

            this.Opacity = 0;
            this.timer1.Interval = 10;
            this.timer1.Enabled = true;
            this.timer1.Start();

            button1.Selectable = false;
            button2.Selectable = false;

            
            
        }

        private void ePasswd2_Leave(object sender, EventArgs e)
        {
            if (ePasswd2.Text == "")
            {
                ePasswd2.ForeColor = Color.FromArgb(150, 150, 150);
                ePasswd2.PasswordChar = '\0';
                ePasswd2.Text = "再次输入密码";
            }
        }

        private void ePasswd_Leave(object sender, EventArgs e)
        {
            if (ePasswd.Text == "")
            {
                ePasswd.ForeColor = Color.FromArgb(150, 150, 150);
                ePasswd.PasswordChar = '\0';
                ePasswd.Text = "密码";
            }
        }

        private void eUser_Leave(object sender, EventArgs e)
        {
            if (eUser.Text == "")
            {
                eUser.ForeColor = Color.FromArgb(150, 150, 150);
                eUser.Text = "用户名";
            }
        }

        private void eUser_Enter(object sender, EventArgs e)
        {
            if (eUser.Text == "用户名")
            {
                eUser.Text = "";
                eUser.ForeColor = Color.FromArgb(0, 0, 0);
            }
        }

        private void ePasswd_Enter(object sender, EventArgs e)
        {
            if (ePasswd.Text == "密码")
            {
                ePasswd.Text = "";
                ePasswd.ForeColor = Color.FromArgb(0, 0, 0);
                ePasswd.PasswordChar = '*';
            }  
        }

        private void ePasswd2_Enter(object sender, EventArgs e)
        {
            if (ePasswd2.Text == "再次输入密码")
            {
                ePasswd2.Text = "";
                ePasswd2.ForeColor = Color.FromArgb(0, 0, 0);
                ePasswd2.PasswordChar = '*';
            }  
        }

        private void RegisterForm_Activated(object sender, EventArgs e)
        {
            this.panel1.Focus();
        }

        private void button2_MouseMove(object sender, MouseEventArgs e)
        {
            button2.BackColor = Color.Red;
            button2.ForeColor = Color.Black;
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            button2.ForeColor = Color.White;
            button2.BackColor = Color.Transparent;
        }

        private void button1_MouseMove(object sender, MouseEventArgs e)
        {
            button1.BackColor = Color.FromArgb(58, 149, 222);
            button1.ForeColor = Color.Black;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.ForeColor = Color.White;
            button1.BackColor = Color.Transparent;
        }

        private void button1_Click(object sender, EventArgs e)  //自定义最小化按钮
        {
            this.WindowState = FormWindowState.Minimized; 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.timer2.Interval = 10;
            this.timer2.Enabled = true;
            this.timer2.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.Opacity < 1)
            {
                this.Opacity += 0.03;
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
                this.Opacity -= 0.05;
                if (this.Opacity < 0.1)
                {                                       
                    Identify._login.timer2.Start();
                    Identify._login.Show();
                    //this.timer2.Stop();
                    this.Close();
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (eUser.Text != "用户名" && ePasswd.Text != "密码" && ePasswd2.Text != "再次输入密码" && ePasswd.Text == ePasswd2.Text)
            {

                {
                    FileStream fsRead = new FileStream("gameData", FileMode.OpenOrCreate, FileAccess.Read);
                    byte[] rbuffer = new byte[1024 * 1024];
                    int r = fsRead.Read(rbuffer, 0, rbuffer.Length);
                    string s = Encoding.Default.GetString(rbuffer, 0, r);
                    fsRead.Close();
                    fsRead.Dispose();
                    int count = 0;
                    if (s != "")
                    {
                        char[] cs = { '☆' };
                        string[] sp = s.Split(cs, StringSplitOptions.RemoveEmptyEntries);
                        for (int i = 0; i < sp.Length; i++)
                        {
                            char[] ccs = { '★' };
                            string[] ssp = sp[i].Split(ccs, StringSplitOptions.RemoveEmptyEntries);
                            if (eUser.Text == ssp[0])
                            {
                                MessageBox.Show("用户名已存在！");
                                break;
                            }
                            else
                            {
                                count++;
                            }
                            if (count == sp.Length)
                            {
                                FileStream fsWrite = new FileStream("gameData", FileMode.Append, FileAccess.Write);
                                string str = eUser.Text + "★" + ePasswd.Text + "★" + 0 + "☆";
                                byte[] wbuffer = Encoding.Default.GetBytes(str);
                                fsWrite.Write(wbuffer, 0, wbuffer.Length);
                                fsWrite.Close();
                                fsWrite.Dispose();
                                MessageBox.Show("注册成功！");
                                this.timer2.Interval = 10;
                                this.timer2.Enabled = true;
                                this.timer2.Start();

                            }

                        }
                    }
                    else
                    {
                        FileStream fsWrite = new FileStream("gameData", FileMode.Append, FileAccess.Write);
                        string str = eUser.Text + "★" + ePasswd.Text + "★" + 0 + "☆";
                        byte[] wbuffer = Encoding.Default.GetBytes(str);
                        fsWrite.Write(wbuffer, 0, wbuffer.Length);
                        fsWrite.Close();
                        fsWrite.Dispose();
                        MessageBox.Show("注册成功！");
                        this.timer2.Interval = 10;
                        this.timer2.Enabled = true;
                        this.timer2.Start();
                    }

                }
            }
            else if (eUser.Text == "用户名")
            {
                MessageBox.Show("请输入用户名");
            }
            else if (ePasswd.Text == "密码")
            {
                MessageBox.Show("请输入密码");
            }
            else if (ePasswd2.Text == "再次输入密码")
            {
                MessageBox.Show("请再次输入密码");
            }
            else if (ePasswd.Text != ePasswd2.Text)
            {
                MessageBox.Show("两次密码不一致");
            }

        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            target = dir.FullName;
            pictureBox1.Image = Image.FromFile(target + @"\Resources\注册1.png");
            //pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            target = dir.FullName;
            pictureBox1.Image = Image.FromFile(target + @"\Resources\注册2.png");
            //pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
        }
        /// <summary>
        /// 移动窗体
        /// </summary>
        private int oldX = 0;
        private int oldY = 0;

        private void RegisterForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.Location.X - this.oldX;
                this.Top += e.Location.Y - this.oldY;
            }
        }

        private void RegisterForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.oldX = e.Location.X;
                this.oldY = e.Location.Y;
            }
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.Location.X - this.oldX;
                this.Top += e.Location.Y - this.oldY;
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.oldX = e.Location.X;
                this.oldY = e.Location.Y;
            }
        }


    }
}
