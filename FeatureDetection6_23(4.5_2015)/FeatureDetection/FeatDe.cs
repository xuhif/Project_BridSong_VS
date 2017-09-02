using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MathWorks.MATLAB.NET.Arrays;
using MathWorks.MATLAB.NET.Utility;
using System.Threading;
using buffer2;
namespace FeatureDetection
{
    public partial class FeatDe : Form
    {
        private string foldPath;
        private buffer2.MyEx MyEx;
        int[] operExt = null;
        int[] operdim = null;
        public FeatDe()
        {
            InitializeComponent();
            Thread t = new Thread(init);
            t.Start();

        }
        private void init()
        {
            MyEx = new buffer2.MyEx();
        }
        //拖入框中
        private void Main_DragEnter(object sender, DragEventArgs e)
        {
            Button b = new Button(); EventArgs e1 = null;
            butReSet_Click(b, e1);
            Color[] c = new Color[] { Color.Crimson,Color.Magenta,Color.Salmon,
                                      Color.Green,Color.Blue};
            Random rd = new Random();
            foldPath = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
            textfoder.Text = foldPath;
            textfoder.ForeColor = c[rd.Next(0, 5)];
            
        }

        //选择文件夹       
        private void butSelFor_Click(object sender, EventArgs e)
        {
            
            Button btn = (Button)sender;
            FolderBrowserDialog diglog = new FolderBrowserDialog();
            diglog.Description = "请选择文件夹路径";
            if (diglog.ShowDialog() == DialogResult.OK)
            {
                foldPath = diglog.SelectedPath;
                if (btn.Name.Equals("OtherSave")) return;
                textfoder.Text = foldPath;
            }
            butReSet_Click(sender, e);
        }
        //选择文件夹
        private void butselect_Click(object sender, EventArgs e)
        {
            butSelFor_Click(sender, e);
        }
        //保存按钮
        private void save_Click(object sender, EventArgs e)
        {
            Svae(sender,e);
        }
        //另存为按钮
        private void OtherSave_Click(object sender, EventArgs e)
        {
            butSelFor_Click(sender, e);
            Svae(sender, e);
           
        }
        //保存
        private void Svae(object sender, EventArgs e) {
            operExt = new int[3] { Jud(MFCC), Jud(CELP), Jud(PITCH) };
            operdim = new int[3] { Jud(ch13), Jud(ch26), Jud(ch39) };
            string b = foldPath;
            if (operExt == null || operExt.Max() == 0 || foldPath == ""|| foldPath==null)
            {
                MessageBox.Show("请选择提取方式或者路径!");
                return;
            }

            Thread t3 = new Thread(funThre);
            t3.Start();
            MessageBox.Show("正在提取，您可以使用其他操作！");     
            //相对路劲 Application.StartupPath
            // pictureFFT.Image = Image.FromFile(@"C:\Users\asus\Pictures\001.jpg");
            //    butReSet_Click(sender, e); //清空内容
        }
        private void funThre() {
            //
            MyEx.GUImyexample_mfcc_celp2(foldPath, (MWNumericArray)operExt);     
        }
        private int Jud(CheckBox ch)
        {
            return ch.Checked ? 1 : 0;
        }
        //重置按钮窗体
        private void butReSet_Click(object sender, EventArgs e)
        {              
            Button btn = (Button)sender;
            //清空变量
            if (!btn.Name.Equals("butSelFor") && !btn.Name.Equals("butselect"))
            {
                foldPath = "";
                textfoder.Text = "";
            }
            operExt = null; operdim = null;
            //清空内容 
            MFCC.Checked = false; CELP.Checked = false; PITCH.Checked = false;
            ch13.Checked = false; ch26.Checked = false; ch39.Checked = false;
          //  if (btn.Name.Equals("save") || btn.Name.Equals("OtherSave")) return;
            pictureFFT.Image = null; pictureTime.Image = null;
            pictureVoice.Image = null;
        }      
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           /* List<string> str = new List<string>();
            str.Add("001");
            str.Add("002");
            comboBox1.DataSource = str;*/
            //像这样加载的
            string path = foldPath + "//" + comboBox1.Text + ".jpg";
            pictureFFT.Image = Image.FromFile(@path); 
        }
    }
}
