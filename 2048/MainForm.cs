using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace moeGame
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            initialMap();           
        }
        Game newGame=new Game();
        public static string strGrade;
        public static int iBestGrade = 0;
        public static bool newBestGrade = false;
        
        DirectoryInfo dir = new DirectoryInfo(Application.StartupPath).Parent.Parent;
        //DirectoryInfo dir = new DirectoryInfo(Application.StartupPath).Parent;
        public string target;  
        List<PictureBox> list_1 = new List<PictureBox>();//4个含4个pictureBox的list来对应16个小格子
        List<PictureBox> list_2 = new List<PictureBox>();
        List<PictureBox> list_3 = new List<PictureBox>();
        List<PictureBox> list_4 = new List<PictureBox>();
        PictureBox pb01 = new PictureBox();
        PictureBox pb02 = new PictureBox();
        PictureBox pb03 = new PictureBox();
        PictureBox pb04 = new PictureBox();
        PictureBox pb05 = new PictureBox();
        PictureBox pb06 = new PictureBox();
        PictureBox pb07 = new PictureBox();
        PictureBox pb08 = new PictureBox();
        PictureBox pb09 = new PictureBox();
        PictureBox pb10 = new PictureBox();
        PictureBox pb11 = new PictureBox();
        PictureBox pb12 = new PictureBox();
        PictureBox pb13 = new PictureBox();
        PictureBox pb14 = new PictureBox();
        PictureBox pb15 = new PictureBox();
        PictureBox pb16 = new PictureBox();

        private void MainForm_Load(object sender, EventArgs e)
        {
            KeyPreview = true;
            newGame.GameStart();
            drawMap();
            laBestScore.Text = "";
            button1.Selectable = false;
            button2.Selectable = false;


            Identify._mainform = this;

            FileStream fsRead = new FileStream("gameData", FileMode.Open, FileAccess.Read);
            byte[] rbuffer = new byte[1024 * 1024];
            int r = fsRead.Read(rbuffer, 0, rbuffer.Length);
            string s = Encoding.Default.GetString(rbuffer, 0, r);
            fsRead.Close();
            fsRead.Dispose();
            char[] cs = { '☆' };
            string[] sp = s.Split(cs, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < sp.Length; i++)
            {
                char[] ccs = { '★' };
                string[] ssp = sp[i].Split(ccs, StringSplitOptions.RemoveEmptyEntries);
                if (Convert.ToInt32(ssp[2]) > iBestGrade)
                {
                    iBestGrade = Convert.ToInt32(ssp[2]);
                }
            }
            laBestScore.Text += iBestGrade;        
        }

        #region 初始化地图
        /// <summary>
        /// 初始化地图
        /// </summary>       
        public void initialMap()
        {
            target = dir.FullName;
            list_1.AddRange(new[] { pb01, pb02, pb03, pb04 });
            int x=list_1.Count;
            for (int i = 0; i < list_1.Count; i++)
            {
                list_1[i].SizeMode = PictureBoxSizeMode.StretchImage;               
                list_1[i].Size = new Size(100, 100);
                list_1[i].Location = new Point(30+i * 105, 155);
                list_1[i].Image = Image.FromFile(target + @"\Resources\0.jpg");
                this.Controls.Add(list_1[i]);
            }
            list_2.AddRange(new[] { pb05, pb06, pb07, pb08 });
            for (int i = 0; i < list_2.Count; i++)
            {
                list_2[i].SizeMode = PictureBoxSizeMode.StretchImage;
                list_2[i].Size = new Size(100, 100);
                list_2[i].Location = new Point(30+i * 105, 260);
                list_2[i].Image = Image.FromFile(target + @"\Resources\0.jpg");
                this.Controls.Add(list_2[i]);
            }
            list_3.AddRange(new[] { pb09, pb10, pb11, pb12 });
            for (int i = 0; i < list_3.Count; i++)
            {
                list_3[i].SizeMode = PictureBoxSizeMode.StretchImage;
                list_3[i].Size = new Size(100, 100);
                list_3[i].Location = new Point(30+i * 105, 365);
                list_3[i].Image = Image.FromFile(target + @"\Resources\0.jpg");
                this.Controls.Add(list_3[i]);
            }
            list_4.AddRange(new[] { pb13, pb14, pb15, pb16 });
            for (int i = 0; i < list_4.Count; i++)
            {
                list_4[i].SizeMode = PictureBoxSizeMode.StretchImage;
                list_4[i].Size = new Size(100, 100);
                list_4[i].Location = new Point(30+i * 105, 470);
                list_4[i].Image = Image.FromFile(target + @"\Resources\0.jpg");
                this.Controls.Add(list_4[i]);
            }
            
        }
        #endregion

        #region 绘制地图
        public void drawMap()
        {
            target = dir.FullName; 
                   
            for (int i = 0; i < 4; i++)
            {
                switch (newGame.iMap[0, i])
                {
                    case 0:
                        list_1[i].Image = Image.FromFile(target + @"\Resources\0.jpg");
                        break;
                    case 2:
                        list_1[i].Image = Image.FromFile(target + @"\Resources\2.jpg");
                        break;
                    case 4:
                        list_1[i].Image = Image.FromFile(target + @"\Resources\4.jpg");
                        break;
                    case 8:
                        list_1[i].Image = Image.FromFile(target + @"\Resources\8.jpg");
                        break;
                    case 16:
                        list_1[i].Image = Image.FromFile(target + @"\Resources\16.jpg");
                        break;
                    case 32:
                        list_1[i].Image = Image.FromFile(target + @"\Resources\32.jpg");
                        break;
                    case 64:
                        list_1[i].Image = Image.FromFile(target + @"\Resources\64.jpg");
                        break;
                    case 128:
                        list_1[i].Image = Image.FromFile(target + @"\Resources\128.jpg");
                        break;
                    case 256:
                        list_1[i].Image = Image.FromFile(target + @"\Resources\256.jpg");
                        break;
                    case 512:
                        list_1[i].Image = Image.FromFile(target + @"\Resources\512.jpg");
                        break;
                    case 1024:
                        list_1[i].Image = Image.FromFile(target + @"\Resources\1024.jpg");
                        break;
                    case 2048:
                        list_1[i].Image = Image.FromFile(target + @"\Resources\2048.jpg");
                        break;
                    case 4096:
                        list_1[i].Image = Image.FromFile(target + @"\Resources\4096.jpg");
                        break;
                    case 8192:
                        list_1[i].Image = Image.FromFile(target + @"\Resources\8192.jpg");
                        break;
                }
                switch (newGame.iMap[1, i])
                {
                    case 0:
                        list_2[i].Image = Image.FromFile(target + @"\Resources\0.jpg");
                        break;
                    case 2:
                        list_2[i].Image = Image.FromFile(target + @"\Resources\2.jpg");
                        break;
                    case 4:
                        list_2[i].Image = Image.FromFile(target + @"\Resources\4.jpg");
                        break;
                    case 8:
                        list_2[i].Image = Image.FromFile(target + @"\Resources\8.jpg");
                        break;
                    case 16:
                        list_2[i].Image = Image.FromFile(target + @"\Resources\16.jpg");
                        break;
                    case 32:
                        list_2[i].Image = Image.FromFile(target + @"\Resources\32.jpg");
                        break;
                    case 64:
                        list_2[i].Image = Image.FromFile(target + @"\Resources\64.jpg");
                        break;
                    case 128:
                        list_2[i].Image = Image.FromFile(target + @"\Resources\128.jpg");
                        break;
                    case 256:
                        list_2[i].Image = Image.FromFile(target + @"\Resources\256.jpg");
                        break;
                    case 512:
                        list_2[i].Image = Image.FromFile(target + @"\Resources\512.jpg");
                        break;
                    case 1024:
                        list_2[i].Image = Image.FromFile(target + @"\Resources\1024.jpg");
                        break;
                    case 2048:
                        list_2[i].Image = Image.FromFile(target + @"\Resources\2048.jpg");
                        break;
                    case 4096:
                        list_2[i].Image = Image.FromFile(target + @"\Resources\4096.jpg");
                        break;
                    case 8192:
                        list_2[i].Image = Image.FromFile(target + @"\Resources\8192.jpg");
                        break;
                }
                switch (newGame.iMap[2, i])
                {
                    case 0:
                        list_3[i].Image = Image.FromFile(target + @"\Resources\0.jpg");
                        break;
                    case 2:
                        list_3[i].Image = Image.FromFile(target + @"\Resources\2.jpg");
                        break;
                    case 4:
                        list_3[i].Image = Image.FromFile(target + @"\Resources\4.jpg");
                        break;
                    case 8:
                        list_3[i].Image = Image.FromFile(target + @"\Resources\8.jpg");
                        break;
                    case 16:
                        list_3[i].Image = Image.FromFile(target + @"\Resources\16.jpg");
                        break;
                    case 32:
                        list_3[i].Image = Image.FromFile(target + @"\Resources\32.jpg");
                        break;
                    case 64:
                        list_3[i].Image = Image.FromFile(target + @"\Resources\64.jpg");
                        break;
                    case 128:
                        list_3[i].Image = Image.FromFile(target + @"\Resources\128.jpg");
                        break;
                    case 256:
                        list_3[i].Image = Image.FromFile(target + @"\Resources\256.jpg");
                        break;
                    case 512:
                        list_3[i].Image = Image.FromFile(target + @"\Resources\512.jpg");
                        break;
                    case 1024:
                        list_3[i].Image = Image.FromFile(target + @"\Resources\1024.jpg");
                        break;
                    case 2048:
                        list_3[i].Image = Image.FromFile(target + @"\Resources\2048.jpg");
                        break;
                    case 4096:
                        list_3[i].Image = Image.FromFile(target + @"\Resources\4096.jpg");
                        break;
                    case 8192:
                        list_3[i].Image = Image.FromFile(target + @"\Resources\8192.jpg");
                        break;
                }
                switch (newGame.iMap[3, i])
                {
                    case 0:
                        list_4[i].Image = Image.FromFile(target + @"\Resources\0.jpg");
                        break;
                    case 2:
                        list_4[i].Image = Image.FromFile(target + @"\Resources\2.jpg");
                        break;
                    case 4:
                        list_4[i].Image = Image.FromFile(target + @"\Resources\4.jpg");
                        break;
                    case 8:
                        list_4[i].Image = Image.FromFile(target + @"\Resources\8.jpg");
                        break;
                    case 16:
                        list_4[i].Image = Image.FromFile(target + @"\Resources\16.jpg");
                        break;
                    case 32:
                        list_4[i].Image = Image.FromFile(target + @"\Resources\32.jpg");
                        break;
                    case 64:
                        list_4[i].Image = Image.FromFile(target + @"\Resources\64.jpg");
                        break;
                    case 128:
                        list_4[i].Image = Image.FromFile(target + @"\Resources\128.jpg");
                        break;
                    case 256:
                        list_4[i].Image = Image.FromFile(target + @"\Resources\256.jpg");
                        break;
                    case 512:
                        list_4[i].Image = Image.FromFile(target + @"\Resources\512.jpg");
                        break;
                    case 1024:
                        list_4[i].Image = Image.FromFile(target + @"\Resources\1024.jpg");
                        break;
                    case 2048:
                        list_4[i].Image = Image.FromFile(target + @"\Resources\2048.jpg");
                        break;
                    case 4096:
                        list_4[i].Image = Image.FromFile(target + @"\Resources\4096.jpg");
                        break;
                    case 8192:
                        list_4[i].Image = Image.FromFile(target + @"\Resources\8192.jpg");
                        break;
                }
            }
        }
        #endregion
                  

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            Identify._login.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void MainForm_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    newGame.MoveUp();
                    break;
                case Keys.Down:
                    newGame.MoveDown();
                    break;
                case Keys.Left:
                    newGame.MoveLeft();
                    break;
                case Keys.Right:
                    newGame.MoveRight();                    
                    break;
            }
            drawMap();
            strGrade = "";
            strGrade+=newGame.grade;
            laScore.Text = strGrade;
            if (newGame.grade > iBestGrade)
            {
                iBestGrade = newGame.grade;
                laBestScore.Text = "";
                laBestScore.Text += iBestGrade;
                newBestGrade = true;
            }           
        }

        public void timer1_Tick(object sender, EventArgs e)
        {
            if (this.Opacity < 1)
            {
                this.Opacity += 0.02;
                if (this.Opacity >= 0.95)
                {
                    this.timer1.Stop();
                }
            }
        }

        public void timer2_Tick(object sender, EventArgs e)
        {
            if (this.Opacity > 0)
            {
                this.Opacity -= 0.02;
            }
        }

        private int oldX = 0;
        private int oldY = 0;

        private void MainForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.Location.X - this.oldX;
                this.Top += e.Location.Y - this.oldY;
            }
        }

        private void MainForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.oldX = e.Location.X;
                this.oldY = e.Location.Y;
            }
        }


    }
}
