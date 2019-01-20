using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Diagnostics;
using System.Threading;

namespace STLink_Flash
{
    public partial class UserControl_UI : UserControl
    {
        /// <summary>
        /// 程序集名字
        /// </summary>
        public string MoudleConnString = Assembly.GetExecutingAssembly().GetName().Name;

        /// <summary>
        /// 程序集名字+后缀
        /// </summary>
        public string MoudleConnString_Ext = Path.GetFileName(Assembly.GetExecutingAssembly().Location);
        
        public UserControl_UI()
        {
            InitializeComponent();
            Assembly asm = Assembly.GetExecutingAssembly();
            AssemblyFileVersionAttribute asmfver = (AssemblyFileVersionAttribute)Attribute.GetCustomAttribute(asm, typeof(AssemblyFileVersionAttribute));
            UC_Tittle.Text = MoudleConnString + " Ver " + asmfver.Version;
            UC_Tittle.Parent = pictureBox1;
            mset_Load();         
        }

        internal void mset_Load()
        {
            try
            {
                string path = System.Windows.Forms.Application.StartupPath + @"\MoudleSettingFiles\";
                tools.pcheck(path);
                path += MoudleConnString + "_" + ".mset";
                if (File.Exists(path))
                {
                    using (StreamReader sr = new StreamReader(path, System.Text.Encoding.GetEncoding("GB2312")))
                    {
                        tb_FlashFile.Text = sr.ReadLine();
                    }

                }
            }
            catch (Exception ex)
            {
                string DialogTittle = MoudleConnString_Ext;
                MessageBox.Show("mset_Load初始化失败:" + ex.ToString(), DialogTittle);
            }

        }

        //------------------以上为通用初始化区-----------------------------
        //------------------以下为private内部函数和事件区-----------------------------

        private void btn_FileSet_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = Environment.CurrentDirectory;
            openFileDialog1.Filter = "烧录文件(*.bin,*.hex,*.srec,*.s19)|*.bin;*.hex;*.srec;*.s19|All files(*.*)|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                tb_FlashFile.Text = openFileDialog1.FileName;
                string path = System.Windows.Forms.Application.StartupPath + @"\MoudleSettingFiles\";
                tools.pcheck(path);
                path += MoudleConnString + "_"  + ".mset";
                using (StreamWriter sr = new StreamWriter(path, false, System.Text.Encoding.GetEncoding("GB2312")))
                {
                    string rst = "";                
                    rst += tb_FlashFile.Text + "\r\n";
                    sr.Write(rst);
                }
            }
        }

        private void tb_FlashFile_TextChanged(object sender, EventArgs e)
        {
            tb_FileNameFullString.Text = tb_FlashFile.Text;
        }

        private void cb_Debug_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_Debug.Checked)
            {
                btn_RunBat.Enabled = true;
            } else
            {
                btn_RunBat.Enabled = false;
            }

        }
      
        //Invoke回调函数
        private delegate void InvokeCallback(Control c,string text);
        private void UpdateText(Control c, string text)
        {
            if (c.InvokeRequired)//如果调用控件的线程和创建控件的线程不是同一个则为True
            {
                while (!c.IsHandleCreated)
                {
                    //解决窗体关闭时出现“访问已释放句柄“的异常
                    if (c.Disposing || c.IsDisposed)
                        return;
                }
                InvokeCallback d = new InvokeCallback(UpdateText); //回调
                c.Invoke(d, new object[] { c,text });
            }
            else
            {
                c.Text += text + "\r\n";
                if (c.GetType() == typeof(RichTextBox))
                {
                    RichTextBox rtb = (RichTextBox)c;
                    rtb.Select(c.Text.Length, 0);
                    rtb.ScrollToCaret();
                }
           
            }
        } 

        private int StartProcess(string filename, string argument, int timeout_ms)
        {
            argument = argument.Trim();
            Process process = new Process();//创建进程对象    
            ProcessStartInfo startInfo = new ProcessStartInfo(filename, argument); // 括号里是(程序名,参数)
            process.StartInfo = startInfo;
            process.StartInfo.UseShellExecute = false;    //是否使用操作系统的shell启动,shell启动就无法获取StandardOutput
            startInfo.RedirectStandardInput = true;      //接受来自调用程序的输入     
            startInfo.RedirectStandardOutput = true;     //由调用程序获取输出信息
            startInfo.CreateNoWindow = true;             //不显示调用程序的窗口，true=不显示
            process.EnableRaisingEvents = true;
            process.ErrorDataReceived += new DataReceivedEventHandler(ErrorHandler);
            process.OutputDataReceived += new DataReceivedEventHandler(OutputHandler);
            try
            {
                process.Start();
                process.BeginOutputReadLine(); //异步
                //string output = process.StandardOutput.ReadToEnd(); //同步,异步和同步不可同时执行
                /*
                string line = process.StandardOutput.ReadLine();//同步,每次读取一行
                while (!process.StandardOutput.EndOfStream)
                {
                    rtb_STLink.AppendText(line + "\r\n");
                    line = process.StandardOutput.ReadLine();
                }                           
                */
                int i = 0;
                while (!process.HasExited)
                {
                    ////等待程序执行完退出进程,并让窗口实时更新
                    Thread.Sleep(100);
                    i++;
                    Application.DoEvents();
                    if (i >= timeout_ms / 100)
                    {
                        process.Kill();
                        MessageBox.Show("等待超时，强制关闭调用Process进程，Timout=" + timeout_ms + "ms");
                        return -1;
                    }
                }
                return process.ExitCode;

            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("需要提升")) { MessageBox.Show(ex.Message + Environment.NewLine + "需要以管理员身份执行程序"); }
                else
                {
                    MessageBox.Show(ex.Message + Environment.NewLine + ex.StackTrace);
                }

                return -1;
            }

        }

        private void OutputHandler(object sender, DataReceivedEventArgs e)
        {
            //动态读取,以下2个委托任选一个，第一个最简单，但无法设置自动下拉文本框
            //rtb_STLink.BeginInvoke(new MethodInvoker(() => rtb_STLink.Text += e.Data + "\r\n"));
            UpdateText(rtb_STLink, e.Data);
        }

        private void ErrorHandler(object sender, DataReceivedEventArgs e)
        {
            //MessageBox.Show(e.Data);
        }


        //常用Public接受调用的接口函数，需搭配以下调试按钮，来演示调用
        private void btn_RunBat_Click(object sender, EventArgs e)
        {
            btn_RunBat.Enabled = false;
            cb_Debug.Enabled = false;
            string RunLog = null;
            RunBatFLASH(out RunLog);
            Console.WriteLine(RunLog);
            cb_Debug.Enabled = true;
            cb_Debug.Checked = false;
        }


        //------------------以下为Public接受调用的接口区-----------------------------

        /// <summary>
        /// 获取批处理执行的Log及执行返回的ExitCode(string类型)
        /// </summary>
        /// <param name="RunLog">返回批处理执行的Log</param>
        /// <returns>返回ExitCode(string类型)</returns>
        public string RunBatFLASH(out string RunLog)
        {
            rtb_STLink.Text = "";
            rtb_STLink.ForeColor = Color.Black;
            string rst=StartProcess(Environment.CurrentDirectory + @"\STLINK_FLASH.bat", "\"" + tb_FileNameFullString.Text + "\"", 120000).ToString();
            RunLog = rtb_STLink.Text;
            if (rst == "0")
            {
                rtb_STLink.ForeColor = Color.Green;
            }
            else
            {
                rtb_STLink.ForeColor = Color.Red;
            }
            return rst;

        }
  
    }


  
}
