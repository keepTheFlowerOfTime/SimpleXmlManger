using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
* Author: gxw
* Time: 2017/7/11 22:10:42
*
*/
namespace MyLib.Xml
{
    public static class ConsoleLog
    {
        static bool s_flag = true;
        public static void Close()
        {
            s_flag = false;
        }

        public static void Open()
        {
            s_flag = true;
        }
        public static void WriteLine(string s)
        {
            if (!s_flag) return;
            Console.WriteLine(s);
        }
    }
}
