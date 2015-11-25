using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace moeGame
{
    class Game
    {
        public static bool perGrade = false;
        public int[,] iMap = new int[4, 4];           
        private Random ra = new Random();
        public int grade { get; private set; }
        public static int lastGrade = 0;     
        bool reGrade=false;     
        public void GameStart()
        {
            for (int x = 0; x < 4; x++)
                for (int y = 0; y < 4; y++)
                    iMap[x, y] = 0;
            grade = 0;
            Add();
            Add();
        }
        public void Add()
        {
            int x = ra.Next(0, 4);
            int y = ra.Next(0, 4);
            if (iMap[x, y] == 0)
            {
                if (ra.Next(1, 101) >= 90)
                    iMap[x, y] = 4;
                else iMap[x, y] = 2;
                IsGameOver();
            }
            else Add();
        }

        public void IsGameOver()
        {
            bool die = true;  //判断游戏是否结束
            bool flag = false; //用来跳双层循环
            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y < 4; y++)
                {
                    //如果新要赋值的元素为0，则游戏未结束，如果所有的元素都不为0，判断是否有元素与上下左右相同，如果木有则游戏结束。
                    if (iMap[x, y] == 0 || (x > 0 && iMap[x, y] == iMap[x - 1, y]) || (x < 3 && iMap[x, y] == iMap[x + 1, y]) || (y > 0 && iMap[x, y] == iMap[x, y - 1]) || (y < 3 && iMap[x, y] == iMap[x, y + 1]))
                    {
                        die = false;
                        flag = true; //满足上面一个条件即游戏未结束，直接跳出双循环
                        break;
                    }
                }
                if (flag)  
                    break;
            }
            if (die)
            {
                perGrade = false;
                int count=0;    //用来找到刷新纪录的玩家
                int icount = 0; //用来找到刷新纪录的玩家的分数
                int c = 0;
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
                    if (LoginForm.UserName == ssp[0]) //找到当前的玩家
                    {
                        if (Convert.ToInt32(ssp[2]) < grade) //如果已存的分数比现在的分数小，则写入新的分数
                        {
                            reGrade = true;
                            string strs = s;
                            int ix = 0;
                            int[] a = new int[i + 1];
                            int[] b = new int[2];
                            for (ix = 0; ix < i; ix++)  
                            {
                                a[ix] = strs.IndexOf("☆");
                                strs = strs.Substring(a[ix] + 1);
                                count = count + a[ix] + 1;  //通过不断截取玩家信息分隔符☆，直到到当前玩家的信息分隔符☆，获取当前玩家的信息分割符☆所在位置
                                if (ix == i - 1) //当循环到当前玩家的信息时，对当前玩家的信息进行截取★，来获取当前玩家的姓名+密码共占用多少字符
                                {
                                    int d = strs.IndexOf("☆");//获取“当前玩家的信息，从姓名开始到分隔符☆截止”共有多少个字符
                                    for (int iy = 0; iy < 2; iy++)
                                    {                                        
                                        b[iy]=strs.IndexOf("★");
                                        strs = strs.Substring(b[iy] + 1);
                                        icount = icount + b[iy] + 1; //获取“当前玩家的信息，从姓名开始到分数前面一个分隔符★为止”共有多少个字符                                      
                                    }
                                    c = d - icount;//获取“当前玩家在gameData中 已存的 分数的字符长度” 分数为个位数则为1,十位数则为2.....
                                }
                            }
                            break;
                        }
                        else break;
                    }
                }
                if (reGrade) //已存的分数比现在的分数小，写入新的分数
                {
                    perGrade = true;
                    FileStream fsWrite = new FileStream("gameData", FileMode.Create, FileAccess.Write);
                    int a = s.IndexOf("☆", count + 1);//找到当前玩家的信息分割符☆
                    //int b = s.IndexOf("★",icount);
                    string aa = s.Substring(0, a - c);//截取s的“从开头到当前玩家的密码与分数之间的那个分隔符★为止”，赋值给aa
                    string bb = s.Substring(a); //截取s的“从当前玩家的信息分割符☆开始，到结尾”，赋值给bb
                    s = aa + grade + bb;  //s=aa+分数+bb
                    byte[] wbuffer = Encoding.Default.GetBytes(s);
                    fsWrite.Write(wbuffer, 0, wbuffer.Length);
                    fsWrite.Close();
                    fsWrite.Dispose();
                }
                lastGrade = grade;//将最终成绩返回给GameOver窗体
                GameOver gg = new GameOver();
                gg.Opacity = 0;
                gg.Show();
                gg.timer1.Interval = 10;
                gg.timer1.Enabled = true;
                gg.timer1.Start();
            }
        }

        #region 上下左右
        public void MoveDown()
        {
            bool change = false;  //用来判断是否新给一个元素赋值，每次操作都需要新给一个元素赋值
            for (int y = 0; y < 4; y++)     //对每一列进行循环
            {
                for (int x = 3; x >= 0; x--)  //从下往上对每个元素循环
                {
                    for (int ix = x - 1; ix >= 0; ix--)  //当前操作元素的上面每个元素
                    {
                        if (iMap[ix, y] > 0)    //如果当前操作元素上面的元素大于0
                        {
                            if (iMap[x, y] == 0) //如果当前操作元素为0，让当前操作元素等于上面一个元素，上面一个元素清0
                            {
                                iMap[x, y] = iMap[ix, y];
                                iMap[ix, y] = 0;
                                x++;  //让x回滚，可以解决“当前元素不在最底下，但是最底下还有空白元素”的情况
                                change = true;                           
                            }
                            else if (iMap[x, y] == iMap[ix, y])//如果当前操作元素与上面一个元素相等，当前操作元素*2，上面那个清0
                            {
                                iMap[x, y] *= 2;
                                iMap[ix, y] = 0;
                                grade += iMap[x, y];  //分数增加
                                change = true;                                                      
                            }
                            IsGameOver();  //检测游戏是否接受
                            break;
                        }
                    }
                }
            }
            if (change)
                Add(); //新随机给一个元素赋值
        }
        public void MoveUp()
        {
            bool change = false;
            for (int y = 0; y < 4; y++)
            {
                for (int x = 0; x < 4; x++)
                {
                    for (int ix = x + 1; ix < 4; ix++)
                    {
                        if (iMap[ix, y] > 0)
                        {
                            if (iMap[x, y] == 0)
                            {
                                iMap[x, y] = iMap[ix, y];
                                iMap[ix, y] = 0;
                                x--;
                                change = true;                                             
                            }
                            else if (iMap[x, y] == iMap[ix, y])
                            {
                                iMap[x, y] *= 2;
                                iMap[ix, y] = 0;
                                grade += iMap[x, y];
                                change = true; 
                            }
                            IsGameOver();
                            break;
                        }
                    }
                }
            }
            if (change)
                Add();              
        }
        public void MoveLeft()
        {
            bool change = false;
            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y < 4; y++)
                {
                    for (int iy = y + 1; iy < 4; iy++)
                    {
                        if (iMap[x, iy] > 0)
                        {
                            if (iMap[x, y] == 0)
                            {
                                iMap[x, y] = iMap[x, iy];
                                iMap[x, iy] = 0;
                                y--;
                                change = true;                              
                            }
                            else if (iMap[x, y] == iMap[x, iy])
                            {
                                iMap[x, y] *= 2;
                                iMap[x, iy] = 0;
                                grade += iMap[x, y];
                                change = true;  
                            }
                            IsGameOver();
                            break;
                        }
                    }
                }
            }
            if (change)
                Add();
        }
        public void MoveRight()
        {
            bool change = false;
            for (int x = 0; x < 4; x++)
            {
                for (int y = 3; y >= 0; y--)
                {
                    for (int iy = y - 1; iy >= 0; iy--)
                    {
                        if (iMap[x, iy] > 0)
                        {
                            if (iMap[x, y] == 0)
                            {
                                iMap[x, y] = iMap[x, iy];
                                iMap[x, iy] = 0;
                                y++;
                                change = true;
                            }
                            else if (iMap[x, y] == iMap[x, iy])
                            {
                                iMap[x, y] *= 2;
                                iMap[x, iy] = 0;
                                grade += iMap[x, y];
                                change = true;
                            }
                            IsGameOver();
                            break;
                        }
                    }
                }
            }
            if (change)
                Add();
        }       
        #endregion 
    }
}
