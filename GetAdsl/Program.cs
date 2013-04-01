using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;

namespace GetAdsl
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("输入ADSL名称：");
            string adsl = " " + Console.ReadLine() + " ";
            Console.WriteLine("输入序号：");
            string head = Console.ReadLine();
            for (int i = 0; i < 20; i++) 
            {
                  int num=Convert.ToInt32(head)+i;
                  Wklist list = new Wklist(adsl, "w" + num.ToString() + "k");
                  list.Run();
            }
            Console.ReadLine();
        }
    }
}
