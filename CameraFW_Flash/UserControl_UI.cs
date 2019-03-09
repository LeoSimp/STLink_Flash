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

namespace CameraFW_Flash
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
        /// <summary>
        /// 
        /// </summary>
        public UserControl_UI()
        {
            InitializeComponent();
            Assembly asm = Assembly.GetExecutingAssembly();
            AssemblyFileVersionAttribute asmfver = (AssemblyFileVersionAttribute)Attribute.GetCustomAttribute(asm, typeof(AssemblyFileVersionAttribute));
            UC_Tittle.Text = MoudleConnString + " Ver " + asmfver.Version;
            UC_Tittle.Parent = pictureBox1;
            lbToolName.Text = MoudleConnString + ".bat";
            mset_Load();
            ver_load();
        }

        internal void mset_Load()
        {
            try
            {
                string path = System.Windows.Forms.Application.StartupPath + @"\MoudleSettingFiles\";
                tools.pcheck(path);
                path += MoudleConnString + ".mset";
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

        internal void ver_load()
        {
            string Verpath = Environment.CurrentDirectory + "\\Version.txt";
            if (File.Exists(Verpath))
            {
                using (StreamReader sr = new StreamReader(Verpath, System.Text.Encoding.GetEncoding("GB2312")))
                {
                    tb_Version.Text = sr.ReadLine().Replace("ISP VERSION = ", "");
                }

            }
        }

        //------------------以上为通用初始化区-----------------------------
        //------------------以下为private内部函数和事件区-----------------------------

        private void btn_FileSet_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = Environment.CurrentDirectory;
            openFileDialog1.Filter = "烧录文件(*.rfw)|*.rfw|All files(*.*)|*.*";
            string TargetFileName = null, TargetFileNameDir = null;;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string[] TargetFileNameArray= openFileDialog1.FileName.Split('\\');
                TargetFileName = TargetFileNameArray[TargetFileNameArray.Length - 1];
                TargetFileNameDir = openFileDialog1.FileName.Replace("\\" + TargetFileName, "");
                if (TargetFileName != "RTS5849D_FW.rfw")
                {
                    MessageBox.Show("必须选择名为RTS5849D_FW.rfw的文件");
                    openFileDialog1.FileName = "";
                }
                if (TargetFileNameDir != Environment.CurrentDirectory)
                {
                    MessageBox.Show("必须选择当前文件夹路径下的名字为RTS5849D_FW.rfw的文件");
                    openFileDialog1.FileName = "";
                }
                if(!string.IsNullOrEmpty(openFileDialog1.FileName))
                {
                    string Verpath = Environment.CurrentDirectory + "\\Version.txt";
                    if (File.Exists(Verpath))
                    {
                        using (StreamReader sr = new StreamReader(Verpath, System.Text.Encoding.GetEncoding("GB2312")))
                        {
                            tb_Version.Text = sr.ReadLine().Replace("ISP VERSION = ","");
                        }

                    }
                }
                tb_FlashFile.Text = openFileDialog1.FileName;
                string path = System.Windows.Forms.Application.StartupPath + @"\MoudleSettingFiles\";
                tools.pcheck(path);
                path += MoudleConnString + ".mset";
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
    
        private void btn_SaveVer_Click(object sender, EventArgs e)
        {
            string Verpath = Environment.CurrentDirectory + "\\Version.txt";
            using (StreamWriter sr = new StreamWriter(Verpath, false, System.Text.Encoding.GetEncoding("GB2312")))
            {
                string rst = "";
                rst += "ISP VERSION = " + tb_Version.Text + "\r\n";
                sr.Write(rst);
            }
            if (File.Exists(Verpath))
            {
                using (StreamReader sr = new StreamReader(Verpath, System.Text.Encoding.GetEncoding("GB2312")))
                {
                    if (tb_Version.Text == sr.ReadLine().Replace("ISP VERSION = ", "")) MessageBox.Show("保存成功！");
                }

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

        private int StartProcess_BK(string filename, string argument, int timeout_ms,out string output)
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
            output = null;
            try
            {
                process.Start();
                //process.BeginOutputReadLine(); //异步
               output = process.StandardOutput.ReadToEnd(); //同步,异步和同步不可同时执行
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

        private int StartProcess(string filename, string argument, int timeout_ms, out string output)
        {

            output = null;
            argument = argument.Trim();
            Process process = new Process();//创建进程对象    
            ProcessStartInfo startInfo = new ProcessStartInfo(filename, argument); // 括号里是(程序名,参数)
            process.StartInfo = startInfo;
            process.StartInfo.UseShellExecute = false;    //是否使用操作系统的shell启动,shell启动就无法获取StandardOutput
            //startInfo.RedirectStandardInput = true;      //接受来自调用程序的输入 ，true时影响判断process.HasExited ，不能判断超时  
            startInfo.RedirectStandardOutput = true;     //由调用程序获取输出信息
            startInfo.CreateNoWindow = true;             //不显示调用程序的窗口，true=不显示
            //process.EnableRaisingEvents = true;
            //process.ErrorDataReceived += new DataReceivedEventHandler(ErrorHandler);
            //process.OutputDataReceived += new DataReceivedEventHandler(OutputHandler);

            try
            {
                process.Start();

                output = process.StandardOutput.ReadToEnd(); //同步,异步和同步不可同时执行

                int i = 0;
                while (!process.HasExited) //对同步无效
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
            UpdateText(rtb_Output, e.Data);
            WriteLog(System.Windows.Forms.Application.StartupPath + @"\" + MoudleConnString + ".log", e.Data);
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
            string rst=RunToolFLASH_AutoFWFile(out RunLog);
            Console.WriteLine(RunLog);
            Console.WriteLine("rst: " + rst);
            cb_Debug.Enabled = true;
            cb_Debug.Checked = false;
        }

        private void WriteLog(string FullLogName, string Message)
        {
            try
            {
                StreamWriter sw = new StreamWriter(FullLogName, true, Encoding.Default, 512); // 创建写入流
                sw.WriteLine(Message);
                sw.Close(); //关闭文件
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + Environment.NewLine + ex.StackTrace);
                return;
            }

        }

        //------------------以下为Public接受调用的接口区-----------------------------

        /// <summary>
        /// 获取批处理执行的Log及执行返回的ExitCode(string类型)
        /// </summary>        
        /// <returns>返回ExitCode(string类型)</returns>
        public string RunToolFLASH(out string RunLog)
        {
            rtb_Output.Text = "";
            rtb_Output.ForeColor = Color.Black;
            //RunLog = System.Windows.Forms.Application.StartupPath + @"\" + MoudleConnString + ".log";
            //if (File.Exists(RunLog)) File.Delete(RunLog);
            string output = null;
            string rst=StartProcess(Environment.CurrentDirectory + @"\"+lbToolName.Text, "\"" + tb_FileNameFullString.Text + "\"",120000,out output).ToString();
            rtb_Output.Text = output;
            if (rst == "0")
            {
                rtb_Output.ForeColor = Color.Green;
            }
            else
            {
                rtb_Output.ForeColor = Color.Red;
            }
            rtb_Output.Update();
            RunLog = output;
            return rst;

        }

        /// <summary>
        /// 获取批处理执行的Log及执行返回的ExitCode(string类型)
        /// </summary>        
        /// <returns>返回ExitCode(string类型)</returns>
        public string RunToolFLASH_AutoFWFile(out string RunLog)
        {
            RunLog = null;
            string[] TargetFileNameArray = tb_FlashFile.Text.Split('\\');
            string TargetFileName = TargetFileNameArray[TargetFileNameArray.Length - 1];
            if (!File.Exists(Environment.CurrentDirectory + "\\"+TargetFileName))
            {
                MessageBox.Show("目标烧录文件不存在，或者未选择烧录文件");
                return "-1";
            }
            string Verpath = Environment.CurrentDirectory + "\\Version.txt";
            if (!File.Exists(Verpath))
            {
                MessageBox.Show("FW文件版本设置文件(Version.txt)不存在，请设置后，点击保存到Version");
                tb_Version.Focus();
                return "-1";
            }
            rtb_Output.Clear();
            rtb_Output.ForeColor = Color.Black;
            //RunLog = System.Windows.Forms.Application.StartupPath + @"\" + MoudleConnString + ".log";
            //if (File.Exists(RunLog)) File.Delete(RunLog);
            string output = null;
            string rst = StartProcess(Environment.CurrentDirectory + @"\" + lbToolName.Text,"", 120000, out output).ToString();
            rtb_Output.Text = output;
            if (rst == "0")
            {
                rtb_Output.ForeColor = Color.Green;
            }
            else
            {
                rtb_Output.ForeColor = Color.Red;
            }
            rtb_Output.Update();
            RunLog = output;
            return rst;

        }


    }


  
}
