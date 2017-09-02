using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using FeatureDetection.modle;
using weka.core.converters;
using java.io;
using myJavaDel;
namespace FeatureDetection
{
    public partial class DataAna : Form
    {
        private string trainPath;
        private string testPath;
        private myJavaDel.Main mj;
        private DataModel dm;
        public static string fcla;
        public static string finte;
        public static string[] fintecla;
        public DataAna()
        {
            InitializeComponent();
            mj = new myJavaDel.Main();
          //* List<string> str = new List<string>();
          /*  str.Add("SMO"); 
            str.Add("MultilayerPerceptron");
            str.Add("J48");
            str.Add("RandomForest");
            str.Add("BayesNet");
            str.Add("NaiveBayes");
            comboBox1.DataSource = str;*/
        }
        //类型选择
        private void butSplit_Click(object sender, EventArgs e)
        {
            Form fe = new ClassT();
            fe.StartPosition = FormStartPosition.Manual;
            fe.Location= new System.Drawing.Point(500, 300);
            //注册委托事件
            ClassT.ClickEvent += new ClickDelegateHander(getMessage);//调用方法
            fe.Show();

        }
        //给委托事件赋值
        public void getMessage(string message)
        {
            textBox1.Text = message;
        }
        private void butStart_Click(object sender, EventArgs e)
        {
            if (fcla == null && finte == null && fintecla == null) {
                MessageBox.Show("选择出错，请重新选择！");
                return;
            }
            string TrFn=null, TeFn=null, clas=null; dm = null;
            if (trainPath != null && testPath != null && trainPath != " " && testPath != "")
            {
                labmat.Text = ""; labval.Text = ""; labshow.Text = "";
                 TrFn = @trainPath;
                 TeFn = @testPath;
                 clas = fcla;             
            } else { MessageBox.Show("路径不可为空！");return; }
            if (fcla != null)
            {
                labshow.Text = "单分类"+fcla;
                dm = mj.GetOneRes(TrFn, TeFn, clas);            
            }
            else {
                labshow.Text = "集成"+ finte;
                dm = mj.GetInteRes(TrFn, TeFn, finte, fintecla);
            }
            TraContent(dm);
        }
        //遍历内容
        private void TraContent(DataModel dm) {
            if (dm != null)
            {
                
                labval.Text = "Kp:    " + dm.kp + "\r\n\r\n" + "Error: " + dm.er;
                string[] mat = dm.reMatrix.Split(' ');
                for (int i = 0; i < mat.Length; i++)
                {
                    if (i % dm.claNum == 0 && i != 0) labmat.Text += "\r\n\r\n";
                    labmat.Text += string.Format("{0,-7}", mat[i]);
                }
            }
            else {
                MessageBox.Show("出错了");
                this.Invalidate();
            }
        }

        //选择train
        private void buttrain_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = true;
            fileDialog.Title = "请选择文件";
            fileDialog.Filter = "所有文件(*.*)|*.*";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                trainPath = fileDialog.FileName;
                texttrain.Text = trainPath;
            }
        }
        //选择test
        private void buttest_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = true;
            fileDialog.Title = "请选择文件";
            fileDialog.Filter = "所有文件(*.*)|*.*";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                testPath = fileDialog.FileName;
                textteat.Text = testPath;
            }
        }      
    }
}
