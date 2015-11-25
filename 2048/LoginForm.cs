using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Threading;

namespace moeGame
{

    public partial class LoginForm : Form
    {
        public static string UserName, Password, Idcoed;
        public static string UserContact;
        Identify te = new Identify();
        DirectoryInfo dir = new DirectoryInfo(Application.StartupPath).Parent.Parent;
        //DirectoryInfo dir = new DirectoryInfo(Application.StartupPath).Parent;
        
        public string target;
        public string startPath;
        bool loFlag = false;        //判断窗口保持原样还是登录中的样子   
        bool yFlag = true;          //再次判断是否登录取消，若取消则登录界面回到原样
        bool suFlag = false;        //判断是否登录成功(用户名、密码、验证码均正确)
        bool aFlag = true;          //用来判断执行登录操作还是取消登录操作       
        private int oldX=0;
        private int oldY=0;

        public LoginForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;//居中
            Form.CheckForIllegalCrossThreadCalls = false;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;     //隐藏默认关闭、最大最小化  

           
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

            Identify._login = this;
            pictureBox2.Hide();         //登录时用来遮挡上方图片
            pictureBox2.BackColor = Color.FromArgb(235, 242, 250);

            this.timer2.Enabled = true;
            this.timer1.Start();

            this.Opacity = 0;
            this.timer2.Interval = 10;
            this.timer2.Enabled = true;
            this.timer2.Start();           

            #region 输入框初始文字
            eUser.ForeColor = Color.FromArgb(150, 150, 150);

            ePasswd.ForeColor = Color.FromArgb(150, 150, 150);

            eUser.Text = "用户名";
            ePasswd.Text = "密码";
            eUser.AutoSize = false;
            eUser.Height = 25;
            ePasswd.AutoSize = false;
            ePasswd.Height = 25;
            #endregion

            linkLabel2.LinkBehavior = LinkBehavior.NeverUnderline;//取消下划线
            linkLabel3.LinkBehavior = LinkBehavior.NeverUnderline;
            this.BackColor = Color.FromArgb(235, 242, 250);

            target = dir.FullName;
            pictureBox3.Image = Image.FromFile(target + @"\Resources\登录1.png");
            

            //重写button与linklabel，让其在登录时不会出现边框
            button1.Selectable = false;
            button2.Selectable = false;

            linkLabel2.Selectable = false;
            linkLabel3.Selectable = false;

        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)//注册
        {
            RegisterForm register = new RegisterForm();
            register.Show();
            Identify._login.Hide();
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("据说金鱼的记忆只有5秒~");
        }

        #region 文本框点中点出，显示默认文字
        private void eUser_Leave(object sender, EventArgs e)  //文本框如果没有输入，就会显示回去
        {
            if (eUser.Text == "")
            {
                eUser.ForeColor = Color.FromArgb(150, 150, 150);
                eUser.Text = "用户名";
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

        #endregion

        private void LoginForm_Activated(object sender, EventArgs e) //焦点放在label上，文本框不会被选中
        {
            this.linkLabel2.Focus();
        }

        #region 自定义关闭与最小化按钮
        private void button2_Click(object sender, EventArgs e)//自定义关闭按钮
        {
            this.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)//自定义最小化按钮
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button1_MouseMove(object sender, MouseEventArgs e)
        {
            button1.BackColor = Color.FromArgb(58, 149, 222);
            //button1.ForeColor = Color.White;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            //button1.ForeColor = Color.FromArgb(134, 137, 152);
            button1.BackColor = Color.Transparent;
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            //button2.ForeColor = Color.FromArgb(134, 137, 152);
            button2.BackColor = Color.Transparent;
        }

        private void button2_MouseMove(object sender, MouseEventArgs e)
        {
            button2.BackColor = Color.Red;
            //button2.ForeColor = Color.White;
        }
        #endregion

        #region 仿QQ式登录，通过timer实现
        private void timer1_Tick(object sender, EventArgs e)  //登录时，图片移动到中间，仿QQ式登录
        {
            if (loFlag) //判断窗口保持原样还是登录中的样子
            {
                if (pictureBox1.Left <= 148)
                {
                    pictureBox1.Left += 10;
                }
                if (pictureBox1.Top >= 152)
                {
                    pictureBox1.Top -= 2;
                    if (pictureBox1.Top == 152)
                    {
                        if (suFlag)    //判断登录信息是否验证成功                   
                        {                            
                            this.timer4.Interval = 1000; //为了让窗口停留在正在登陆的状态
                            this.timer4.Enabled = true;
                            this.timer4.Start();
                            
                        }
                    }
                }
            }
        }

        private void timer2_Tick(object sender, EventArgs e)    //登录窗口贱贱出现    
        {
            if (this.Opacity < 0.9)
            {
                this.Opacity += 0.03;
                if (this.Opacity >= 0.95)
                {
                    this.timer2.Stop();
                }                
            }
        }

        private void timer3_Tick(object sender, EventArgs e)    //登录窗口贱贱消失，游戏窗口贱贱出现 
        {
            if (this.Opacity > 0)
            {
                this.Opacity -= 0.05;
                if (this.Opacity < 0.1)
                {
                    this.Hide();
                    MainForm mf = new MainForm();
                    mf.Opacity = 0;
                    mf.Show();
                    mf.timer1.Interval = 10;
                    mf.timer1.Enabled = true;
                    mf.timer1.Start();
                    this.timer3.Stop();
                }
            }
        }

        private void timer4_Tick(object sender, EventArgs e)    //仿QQ登录中的状态
        {
            if (this.Opacity > 0)
            {
                this.Opacity -= 0.05;
                if (this.Opacity < 0.9)
                {
                    if (yFlag)  //再次判断是否登录取消，若取消则登录界面回到原样
                    {
                        this.timer3.Interval = 10;
                        this.timer3.Enabled = true;
                        this.timer3.Start();
                    }                 
                    else if(this.Opacity < 0.8)
                        this.Opacity = 0.9;
                    this.timer4.Stop();
                }
            }
        }
        #endregion

        #region 登录按钮
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            target = dir.FullName;            
            #region 登录
            int count = 0;
            if (aFlag)
            {
                UserName = eUser.Text.Trim();
                Password = ePasswd.Text.Trim();


                if (UserName != "用户名" && Password != "密码")
                {
                    
                    FileStream fsRead = new FileStream("gameData", FileMode.OpenOrCreate, FileAccess.Read);
                    byte[] rbuffer = new byte[1024 * 1024];
                    int r = fsRead.Read(rbuffer, 0, rbuffer.Length);
                    string s = Encoding.Default.GetString(rbuffer, 0, r);  //所有的游戏数据
                    fsRead.Close();
                    fsRead.Dispose();
                    if (s != "")
                    {
                        char[] cs = { '☆' };
                        string[] sp = s.Split(cs, StringSplitOptions.RemoveEmptyEntries);//每个人的游戏数据
                        for (int i = 0; i < sp.Length; i++)
                        {
                            char[] ccs = { '★' };
                            string[] ssp = sp[i].Split(ccs, StringSplitOptions.RemoveEmptyEntries);//个人的数据
                            if (eUser.Text == ssp[0])
                            {
                                if (ePasswd.Text == ssp[1])
                                {
                                    aFlag = false;  //用来判断执行登录操作还是取消登录操作
                                    eUser.Hide();
                                    ePasswd.Hide();
                                    linkLabel2.Hide();
                                    linkLabel3.Hide();
                                    pictureBox2.Show();
                                    pictureBox3.Image = Image.FromFile(target + @"\Resources\取消1.png");
                                    loFlag = true;  //判断窗口保持原样还是登录中的样子
                                    suFlag = true;  //判断是否登录成功(用户名、密码、验证码均正确)
                                    yFlag = true;   //再次判断是否登录取消，若取消则登录界面回到原样
                                    break;
                                }
                                else
                                {
                                    MessageBox.Show("密码错误");
                                    break;
                                }
                            }
                            else
                            {
                                count++;
                                if (count == sp.Length)
                                {
                                    MessageBox.Show("账号不存在,请注册");
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("账号不存在,请注册");
                    }
                }
                else if (UserName == "用户名")
                {
                    MessageBox.Show("请输入用户名后再登录");
                    
                }
                else if (Password == "密码")
                {
                    MessageBox.Show("请输入密码后再登录");
                    
                }
            }
            #endregion

            #region 取消登陆
            else
            {
                aFlag = true;
                eUser.Show();
                ePasswd.Show();
                linkLabel2.Show();
                linkLabel3.Show();
                pictureBox2.Hide();
                pictureBox3.Image = Image.FromFile(target + @"\Resources\登录1.png");
                loFlag = false;
                yFlag = false;
                suFlag = false;
                pictureBox1.Location = new Point(28, 178);

            }
            #endregion
        }

        //鼠标离开登陆按钮的时候，登陆按钮变色
        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            if (aFlag)
            {
                target = dir.FullName;
                pictureBox3.Image = Image.FromFile(target + @"\Resources\登录1.png");
            }
            else
            {
                target = dir.FullName;
                pictureBox3.Image = Image.FromFile(target + @"\Resources\取消1.png");
            }
        }
        //鼠标放在登陆按钮上的时候，登陆按钮变色
        private void pictureBox3_MouseMove(object sender, MouseEventArgs e)
        {
            if (aFlag)
            {
                target = dir.FullName;
                pictureBox3.Image = Image.FromFile(target + @"\Resources\登录2.png");
            }
            else
            {
                target = dir.FullName;
                pictureBox3.Image = Image.FromFile(target + @"\Resources\取消2.png");
            }
        }
        #endregion

        private void LoginForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.oldX = e.Location.X;        
                this.oldY = e.Location.Y;
            }
        }

        private void LoginForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.Location.X - this.oldX;        
                this.Top += e.Location.Y - this.oldY;
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
