using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace moeGame
{
    class Identify
    {
        public static LoginForm _login;  //对应登陆窗口，便于隐藏与显示，同时用来在关闭游戏界面时关闭此进程
        public static MainForm _mainform;
        public string identify()
        {
            Random r = new Random();           
            char[] ix = new char[4];
            string str = "";
            for (int i = 0; i < 4; i++)
            {
                int rx = r.Next(0, 3);
                switch (rx)
                {
                    case 0:
                        ix[i] = (char)r.Next(48, 58);
                        break;
                    case 1:
                        ix[i] = (char)r.Next(65, 91);
                        break;
                    case 2:
                        ix[i] = (char)r.Next(97, 123);
                        break;
                }
                str += ix[i];
            }

            return str;
        }
    }
}
