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

namespace CopyApp
{
    public partial class Form1 : Form
    {
        string pathSrc;
        string pathDst;
        public Form1()
        {
            InitializeComponent();
        }

        private void pathLoad()
        {
            pathSrc = @"C:\Temp\Src";
            pathDst = @"C:\Temp\Dst";
            try
            {
                string[] paths = File.ReadAllText("PATH.TXT").Split(',');
                pathSrc = paths[0];
                pathDst = paths[1];
            }
            catch (Exception ex)
            {

            }
            finally
            {
                textBox1.Text = pathSrc;
                textBox2.Text = pathDst;
            }
        }

        private void pathSave()
        {
            pathSrc = textBox1.Text;
            pathDst = textBox2.Text;

            StreamWriter sw = new StreamWriter("PATH.TXT");
            sw.Write(textBox1.Text);
            sw.Write(",");
            sw.Write(textBox2.Text);
            sw.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pathLoad();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            pathSave();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pathSave();
            pathLoad();

            string output = "";

            for (int i = 1; i <= 5; i++)
            {
                string srcPath = string.Format("{0}\\SUB{1}\\src\\com\\lgcns\\test", pathSrc, i);
                string dstPath = string.Format("{0}\\SUB{1}\\src\\com\\lgcns\\test", pathDst, i);

                if (!Directory.Exists(srcPath))
                    continue;

                string[] dstDirNames = Directory.GetDirectories(dstPath);
                foreach(var item in dstDirNames)
                {
                    Directory.Delete(item, true);
                    output += (item + " folder deleted" + Environment.NewLine);
                }

                string[] dstFileNames = Directory.GetFiles(dstPath);
                foreach (var item in dstFileNames)
                {
                    File.Delete(item);
                    output += (item + " deleted" + Environment.NewLine);
                }
            }

            output += Environment.NewLine;

            for (int i = 1; i <= 5; i++) {
                string srcPath = string.Format("{0}\\SUB{1}\\src\\com\\lgcns\\test", pathSrc, i);
                string dstPath = string.Format("{0}\\SUB{1}\\src\\com\\lgcns\\test", pathDst, i);

                if (!Directory.Exists(srcPath))
                    continue;

                DirectoryInfo di = new DirectoryInfo(srcPath);
                FileSystemInfo[] fsi = di.GetFileSystemInfos("*.*", SearchOption.AllDirectories);
                foreach(var item in fsi)
                {
                    if (item.Attributes == FileAttributes.Directory)
                    {
                        string dstDir = dstPath + "\\" + item.FullName.Replace(srcPath, "");
                        Directory.CreateDirectory(dstDir);
                        output += (dstDir + " created" + Environment.NewLine);
                    }
                    else
                    {
                        Console.WriteLine(Path.GetFileName(item.FullName)); // 경로 제거  
                        string srcFile = srcPath + "\\" + item.FullName.Replace(srcPath, "");
                        string dstFile = dstPath + "\\" + item.FullName.Replace(srcPath, "");
                        File.Copy(srcFile, dstFile, true);
                        output += (srcFile + " copied" + Environment.NewLine);
                    }
                }

                //string[] fileNames = Directory.GetFiles(srcPath);
                //foreach (var item in fileNames)
                //{
                //    Console.WriteLine(Path.GetFileName(item)); // 경로 제거  
                //    string srcFile = srcPath + "\\" + Path.GetFileName(item);
                //    string dstFile = dstPath + "\\" + Path.GetFileName(item);
                //    File.Copy(srcFile, dstFile, true);
                //    output += (srcFile + " copied" + Environment.NewLine);
                //}
            }
            textBox3.Text = output;
        }
    }
}
