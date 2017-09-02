using java.io;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using weka.core.converters;

namespace FeatureDetection
{

    public partial class SelGroup : Form
    {
        string foldPath;
        DataTable dt;
        List<string> AllPath = new List<string>();
        List<string> clanum = new List<string>();
        public SelGroup()
        {
            InitializeComponent();
            foldPath = @"C:\Users\asus\Desktop\bird1\CELP_AddCla";
            UrlJiexi(foldPath);
            //添加行     
        }
        public SelGroup(string path)
        {
            InitializeComponent();
            foldPath = path;
            UrlJiexi(foldPath);
            //添加行     
        }
        private void SelGroup_Load(object sender, EventArgs e)
        {
            listView.View = View.Details;//设置视图  
            listView.GridLines = true;
            listView.Scrollable = true;
            //设置行号
            /*ImageList il = new ImageList();
            il.ImageSize = new Size(1, 50);
            listView.SmallImageList = il;*/
            this.listBox1.HorizontalScrollbar = true;//任何时候都显示水平滚动条
            this.listBox1.ScrollAlwaysVisible = true;//任何时候都显示垂直滚动条
            //按住shift可以实现多选  
            listBox1.SelectionMode = SelectionMode.MultiExtended;
            //添加列  
            listView.Columns.Add("类名", 70, HorizontalAlignment.Left);
            listView.Columns.Add("种类名", 70, HorizontalAlignment.Left);

        }

        //删除项目
        private void butDel_Click(object sender, EventArgs e)
        {
            var s1 = listBox1.SelectedItems;
            for (int n = 0; n < s1.Count; n++)
            {
                string name = "\\" + listBox1.SelectedItems[n].ToString() + ".csv";
                int index = AllPath.FindIndex(delegate (string c) { return c.Contains(name); });


                //int index=AllPath.indexOf("")
                //int index=AllPath.findindex(delder string s{return s=str;});          
                AllPath.RemoveAt(index);
            }
            OutThePath(AllPath);
        }
        //解析单个
        private void jiexi(string sname)
        {

            string name = System.IO.Path.GetFileNameWithoutExtension(sname).Trim();
            listBox1.Items.Add(name);
            if (Convert.ToBoolean(name.IndexOf('_')) && Char.IsNumber(name[0]))
            {
                /*ListViewItem lit = listView.Items.Add(name.Split('_')[0]);
                lit.SubItems.Add(name.Split('_')[2]);*/
            }
        }
        //循环输出listpath里面的东西
        private void OutThePath(List<string> AllPath)
        {
            listBox1.Items.Clear();
            listView.Items.Clear();
            foreach (string item in AllPath)
            {
                jiexi(item);
            }
        }
        //路径解析
        private void UrlJiexi(string foldpath) {
            if (foldpath == "" || foldpath == null) return;
            if (System.IO.Directory.Exists(foldPath))
            {
                System.IO.DirectoryInfo Dir = new System.IO.DirectoryInfo(foldPath);
                foreach (System.IO.FileInfo FI in Dir.GetFiles())
                {   // 这里写文件格式
                    if (System.IO.Path.GetExtension(FI.Name) == ".csv")
                    {
                        string thisName = foldPath + "\\" + FI.Name;
                        //得到csv文件名称
                        //jiexi(thisName);
                        AllPath.Add(thisName);

                    }
                }
            }
            //判断是是否是一个文件
            else if (System.IO.File.Exists(foldPath))
            {
                AllPath.Add(foldPath);
                //jiexi(foldPath);
            }
            OutThePath(AllPath);
        }

        //选择路径
        private void butSelFlod_Click(object sender, EventArgs e)
        {
            clanum = null;
            FolderBrowserDialog diglog = new FolderBrowserDialog();
            diglog.Description = "请选择文件夹路径";
            if (diglog.ShowDialog() == DialogResult.OK)
            {
                foldPath = diglog.SelectedPath;
                textfloder.Text = foldPath;
                AllPath.Clear();
                UrlJiexi(foldPath);
            }
        }
        //拖动路径
        private void panel1_DragEnter(object sender, DragEventArgs e)
        {
            clanum = null;
            foldPath = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
            AllPath.Clear();
            UrlJiexi(foldPath);
        }
        //组合对象
        public string MergeCsvFilePatn = null;
        public string MergeArffFilePatn = null;
        //选择csv文件路径
        private void butSelCsv_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = true;
            fileDialog.Title = "请选择文件";
            fileDialog.Filter = "所有文件(*.*)|*.*";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                MergeCsvFilePatn = fileDialog.FileName;
                textCsv.Text = MergeCsvFilePatn;
            }
        }  
        private void butComment_Click(object sender, EventArgs e)
        {
            dt = new DataTable();
            foreach (string item in AllPath)
            {
                DataTable d = modle.CsvDel.ZuHe(item);
                // dt=AddDataTable(dt, d);
                dt.Merge(d);
            }
            MergeCsvFilePatn = foldPath + "\\" + "compound.csv";
            modle.CsvDel.SaveCSV(dt, @MergeCsvFilePatn);
            textCsv.Text = MergeCsvFilePatn;
        }
        //转换csv文件；
        private void button1_Click(object sender, EventArgs e)
        {
            //string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop);
            if (textCsv.Text!=null|| textCsv.Text != "")
            {
                string Afpath=System.IO.Path.GetDirectoryName(MergeCsvFilePatn);
                MergeArffFilePatn = Afpath + "\\" + "compound.arff";    
                changeAff(MergeCsvFilePatn, MergeArffFilePatn);
                MessageBox.Show("csv、arff保存路径为：" + foldPath);
            }
            else {
                MessageBox.Show("路径选择为空！");
            }     
        }

        //转Aff
        private void changeAff(string foldPath, string newfoldPath)
        {
            CSVLoader loader = new CSVLoader();
            loader.setSource(new File(@foldPath));
            weka.core.Instances data = loader.getDataSet();
            ArffSaver saver = new ArffSaver();
            saver.setInstances(data);
            saver.setFile(new File(@newfoldPath));
            saver.writeBatch();
        }  
    }
}
