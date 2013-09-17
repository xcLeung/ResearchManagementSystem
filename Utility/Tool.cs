/*
 * Author: xianmao
 * Date: 2013-02-19
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Web;
using System.Diagnostics;

namespace Utility
{
    public class Tool
    {
        public const char space = ' ';

        /// <summary>
        /// 获取指定个数据的普通空格
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        static public string GetSpace(int n)
        {
            return new string(space, n);
        }

        /// <summary>
        /// 获取指定个数据的Tab
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        static public string GetTab(int n)
        {
            return new string(space, n * 4);
        }

        /// <summary>
        /// 获取文件的后缀名（小写）
        /// </summary>
        /// <param name="FullPath"></param>
        /// <returns></returns>
        static public string GetFileExtension(string FullPath)
        {
            if (FullPath.IndexOf('.') < 0) return "";
            return FullPath.Substring(FullPath.LastIndexOf('.') + 1).ToLower();
        }

        /// <summary>
        /// 截取字符串
        /// </summary>
        /// <param name="str"></param>
        /// <param name="slen"></param>
        /// <param name="substitute"></param>
        /// <returns></returns>
        public static string CutString(string str, int slen, string substitute = "...")
        {
            int i = 0, j = 0;
            foreach (char chr in str)
            {
                if ((int)chr > 127)
                {
                    i += 2;
                }
                else
                {
                    i++;
                }
                if (i > slen)
                {
                    str = str.Substring(0, j) + substitute;
                    break;
                }
                j++;
            }
            return str;
        }

        /// <summary>
        /// 获取短日期
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetShortDate(string str, bool shorter = false)
        {
            DateTime date = DateTime.Parse(str);
            if (shorter) return date.ToString("MM-dd");
            return date.ToString("yyyy-MM-dd");
        }

        /// <summary>
        /// 得到SHA256加密后的字符串
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns></returns>
        public static string SHA256Encrypt(string plainText)
        {
            SHA256Managed _sha256 = new SHA256Managed();
            byte[] _cipherText = _sha256.ComputeHash(Encoding.Default.GetBytes(plainText));
            return Convert.ToBase64String(_cipherText);
        }

        /// <summary>
        /// Modle名字转换为Table名字
        /// </summary>
        /// <param name="modelName"></param>
        /// <returns></returns>
        public static string ModelNameToTableName(string modelName)
        {
            StringBuilder tableName = new StringBuilder("tb");
            tableName.Append('_');
            tableName.Append(modelName);
            return tableName.ToString();
        }

        #region By云海
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string MD5(string str)
        {
            MD5 Emailmd5 = new MD5CryptoServiceProvider();
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(str);
            bytes = Emailmd5.ComputeHash(bytes);
            Emailmd5.Clear();

            string ret = "";
            for (int i = 0; i < bytes.Length; i++)
            {
                ret += Convert.ToString(bytes[i], 16).PadLeft(2, '0');
            }

            return ret.PadLeft(32, '0');
        }
        #endregion


        #region By xcLeung
        /// <summary>
        /// JSON序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string JsonSerializer<T>(T t)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream();
            ser.WriteObject(ms, t);
            string jsonString = Encoding.UTF8.GetString(ms.ToArray());
            ms.Close();
            return jsonString;
        }


        /// <summary>
        /// JSON反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        public static T JsonDeserialize<T>(string jsonString)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
            T obj = (T)ser.ReadObject(ms);
            return obj;
        }


        /// <summary>
        /// 分割json数据
        /// </summary>
        /// <param name="DataList"></param>
        /// <returns></returns>
        public static String[,] SplitJson(String[] DataList)
        {
            String[,] data = new String[DataList.Length, DataList[0].Split('&').Length];
            for (int i = 0; i < DataList.Length; i++)
            {
                String[] Datas = DataList[i].Split('&');
                for (int j = 0; j < Datas.Length; j++)
                {
                    String[] DataPair = Datas[j].Split('=');
                    data[i, j] = HttpUtility.UrlDecode(DataPair[1]);
                }
            }
            return data;
        }


        public static void ConvertToSWF(string oldFile, string swfFile)
        {
            System.Diagnostics.Process pc = new System.Diagnostics.Process();
            pc.StartInfo.FileName = "~/Web/FlashPaper2.2/FlashPrinter.exe";
            pc.StartInfo.Arguments = string.Format("{0} -o {1}", oldFile, swfFile);
            pc.StartInfo.CreateNoWindow = true;
            pc.StartInfo.UseShellExecute = false;
            pc.StartInfo.RedirectStandardInput = false;
            pc.StartInfo.RedirectStandardOutput = false;
            pc.StartInfo.RedirectStandardError = true;
            pc.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            pc.Start();
            pc.WaitForExit();
            pc.Close();
            pc.Dispose();
        }


        #endregion
    }


    #region xcLeung
    public class PDF2Swf
    {
        #region
        //根目录
        private static string ROOT_PATH = AppDomain.CurrentDomain.BaseDirectory;
        //pdf转swf
        private static string PDF2SWF_PATH = "Web\\Shells\\SWFTools\\pdf2swf.exe";
        //合并swf
        private static string SWFCOMBINE_PATH = "Web\\Shells\\SWFTools\\swfcombine.exe";
        //导航
        private static string SWFVIEWER_PATH = "Web\\Shells\\SWF\\rfxview.swf";
        //语言包
        private static string XPDF_LANG_PATH = "Web\\Shells\\xpdf-chinese-simplified";

        private static string SWFTEMP_PATH = "Web\\Shells\\SWF\\temp.swf";
       
        
        ////保存转成功的swf文件
        //public static string SAVE_SWF_PATH = "Shells\\SWF\\preview.swf";
        ////保存FLM上的PDF文档
        //public static string SAVE_PDF_PATH = "Shells\\PDF\\preview.pdf";
        //public static string PREVIEW_PAGE_PATH = "Web\\Shells\\SWF\\preview.html";

        #endregion
        /// <summary>
        /// 传入PDF的文件路径，以及输出文件的位置，执行pdf2swf的命令
        /// </summary>preview
        /// <param name="strPDFPath"></param>
        /// <param name="strSwfPath"></param>
        public static bool DoPDF2Swf(string strPDFPath, string strSwfPath)
        {
            bool isSuccess = false;
            //如果PDF不存在
            if (!File.Exists(strPDFPath))
            {
                return false;
            }

            #region 清理之前的记录
            if (File.Exists(strSwfPath))
            {
                //已经存在记录,不删除，直接返回
                //File.Delete(strSwfPath);
                return true;
            }
            //临时文件，中间文件
            if (File.Exists(GetPath(SWFTEMP_PATH)))
            {
                File.Delete(GetPath(SWFTEMP_PATH));
            }
            #endregion

            //将pdf文档转成temp.swf文件
            string strCommand = String.Format("{0} -T 8  -s languagedir={3} {1} -o {2}",
                GetPath(PDF2SWF_PATH), strPDFPath, GetPath(SWFTEMP_PATH), GetPath(XPDF_LANG_PATH));
            double spanMilliseconds = RunShell(strCommand);

            //第一步转档失败，则返回
            if (!File.Exists(GetPath(SWFTEMP_PATH)))
            {
                return false;
            }

            //将temp.swf加入到rfxview.swf加入翻页的导航
            strCommand = String.Format("{0} {1} viewport={2} -o {3}",
                GetPath(SWFCOMBINE_PATH), GetPath(SWFVIEWER_PATH), GetPath(SWFTEMP_PATH), strSwfPath);
            spanMilliseconds = RunShell(strCommand);

            if (File.Exists(strSwfPath))
            {
                isSuccess = true;
            }
            return isSuccess;
        }

        /// <summary>
        /// 获取文件全路径
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetPath(string path)
        {
            //HttpContext.Current.Server.MapPath(path);
            return String.Format("{0}{1}", ROOT_PATH, path);
        }

        /// <summary>
        /// 运行命令
        /// </summary>
        /// <param name="strShellCommand">命令字符串</param>
        /// <returns>命令运行时间</returns>
        private static double RunShell(string strShellCommand)
        {
            double spanMilliseconds = 0;
            DateTime beginTime = DateTime.Now;

            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.UseShellExecute = false;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.Arguments = String.Format(@"/c {0}", strShellCommand);
            cmd.Start();
            cmd.WaitForExit();
            cmd.Close();

            DateTime endTime = DateTime.Now;
            TimeSpan timeSpan = endTime - beginTime;
            spanMilliseconds = timeSpan.TotalMilliseconds;

            return spanMilliseconds;
        }
    }

}
    #endregion
