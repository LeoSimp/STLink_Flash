using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace STLink_Flash
{
    internal class tools
    {
        public static void pcheck(string path)
        {
            bool flag;
            flag = System.IO.Directory.Exists(path);
            if (!flag)
            {
                System.IO.Directory.CreateDirectory(path);
            }
        }

        /// <summary>
        /// 判断文件是否存在，如果不存在，创建
        /// </summary>
        /// <param name="path"></param>
        public static void fcheck(string path)
        {
            bool flag = System.IO.File.Exists(path);
            if (!flag)
            {
                try
                {
                    using (System.IO.StreamWriter sr = new System.IO.StreamWriter(path, false, System.Text.Encoding.GetEncoding("GB2312")))
                    {
                        sr.Write("");
                    }
                }
                catch { }//(Exception ex) { Errlog("fcheck 过程出现错误:" + ex.ToString()); }
            }
        }
    }
}
