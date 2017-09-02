using FeatureDetection.modle;
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
using java.io;
using weka.core;
using java.lang;
using weka.attributeSelection;

namespace FeatureDetection
{
   
    public partial class PreTment : Form
    {
        Instances trainIns = null;
        List<string> AttrA = null;
        List<string> AttrB = null;
        List<Bridname> bnlist = new List<Bridname>();
        string AddClaCsvFoldPath="";
        public PreTment()
        {
            InitializeComponent();
        }
        private string foldPath;
        //选择文件
        private void butSelFor_Click(object sender, EventArgs e)
        {
            //选择文件
            /* OpenFileDialog fileDialog = new OpenFileDialog();
             fileDialog.Multiselect = true;
             fileDialog.Title = "请选择文件";
             fileDialog.Filter = "所有文件(*.*)|*.*";
             if (fileDialog.ShowDialog() == DialogResult.OK)
             {
                 foldPath = fileDialog.FileName;
                 textfoder.Text = foldPath;
             }*/
            //选择文件夹
            FolderBrowserDialog diglog = new FolderBrowserDialog();
            diglog.Description = "请选择文件夹路径";
            if (diglog.ShowDialog() == DialogResult.OK)
            {
                foldPath = diglog.SelectedPath;
                textfoder.Text = foldPath;
            }
         }
        //类型
        private void button3_Click(object sender, EventArgs e)
        {
            if (foldPath != null && foldPath != "")
            {
                int i = 0;
                //文件名称例如c://eas  foldname表示eas ParPath表示c://

                System.IO.DirectoryInfo path1 = new System.IO.DirectoryInfo(foldPath);
                //上级目录
                string foldname = foldPath.Substring(foldPath.LastIndexOf('\\')).Replace("\\", "");
                string ParPath = path1.Parent.FullName+"\\";
                // CsvArff文件夹目录路径
                AddClaCsvFoldPath = ParPath +  foldname+ "_AddCla" ;
                if (!System.IO.Directory.Exists(AddClaCsvFoldPath))
                {
                    System.IO.DirectoryInfo directoryInfo = new System.IO.DirectoryInfo(AddClaCsvFoldPath);
                    directoryInfo.Create();
                }
                // 文件的目录
                System.IO.DirectoryInfo Dir = new System.IO.DirectoryInfo(foldPath);
                foreach (System.IO.FileInfo FI in Dir.GetFiles())
                {
                    // 这里写文件格式
                    if (System.IO.Path.GetExtension(FI.Name) == ".csv")
                    {
                        //得到csv文件名称
                        string CsvFilePath = foldPath + "\\" + FI.Name;
                        string[] newname = null;
                        int thisid = 0;
                        if (FI.Name.IndexOf("_") > 6)
                        {
                            newname = FI.Name.Split('_');
                            int index = bnlist.FindIndex(th => th.name == newname[0]);
                            if (index == -1)
                            {
                                i++;
                                Bridname bn = new Bridname();
                                bn.id = i;
                                bn.name = newname[0];
                                bnlist.Add(bn);
                                thisid = i;                  
                            }
                            else {
                                thisid=bnlist[index].id;
                            }  
                        }
                        else {
                            i++;
                            Bridname bn = new Bridname();
                            newname = FI.Name.Split('.');
                            bn.id = i;
                            bn.name = newname[0];
                            bnlist.Add(bn);
                            thisid = i;           
                        }
                        DataTable dt = CsvDel.OpenCSV(CsvFilePath, "a"+ thisid.ToString());
                             
                        //CSV文件保存路劲
                        string CSVSavePath = AddClaCsvFoldPath + "\\" + thisid.ToString()+"_"+FI.Name;
                        //Arff文件保存路劲 itemname鸟的名称
                        string itemname = System.IO.Path.GetFileNameWithoutExtension(CsvFilePath);
                        string ArffPath = AddClaCsvFoldPath + "\\"+ itemname + ".arff";
                        CsvDel.SaveCSV(dt, CSVSavePath);
                        Write(ParPath + foldname+"_Map.txt", itemname,"a"+ thisid);
                        
                        //changeAff(CSVSavePath, ArffPath);
                    }
                }
                MessageBox.Show("成功分类！");
                bnlist = new List<Bridname>();
            }
            else
            {
                MessageBox.Show("请选择路径或输入类型！");
            }
        }
        //选择组合对象
        private void button4_Click(object sender, EventArgs e)
        {
            SelGroup sg = new SelGroup(AddClaCsvFoldPath);
            sg.Show();
        }
        //文件写入
        public void Write(string path,string name,string cla)
        {
            if (cla=="a1")
            {
                System.IO.File.WriteAllText(path, string.Empty);
            }
            //存在文件
            System.IO.StreamWriter sw = new System.IO.StreamWriter(path, true,Encoding.UTF8);
            //开始写入WriteLine(a[i].PadRight(20)+b[i]); 
            sw.WriteLine(name.PadRight(70)+ cla);
            //清空缓冲区
            sw.Flush();
            //关闭流
            sw.Close();
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
        //拖动
        private void PreTment_DragEnter(object sender, DragEventArgs e)
        {
            foldPath = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
            textfoder.Text = foldPath;
        }
        //下拉框加载
        private void PreTment_Load(object sender, EventArgs e)
        {
            numericUpDown1.DecimalPlaces = 2;
            numericUpDown1.Increment = 0.1M;
            numericUpDown1.Maximum = 1;
            AttrA = new List<string>();
            AttrA.Add("CfsSubsetEval"); AttrA.Add("CorrelationAttributeEval"); AttrA.Add("GainRatioAttributeEval");
            AttrA.Add("InfoGainAttributeEval"); AttrA.Add("OneRAttributeEval"); AttrA.Add("PrincipalComponents"); AttrA.Add("ReliefFAttributeEval");
            AttrA.Add("SymmetricalUncertAttributeEval"); AttrA.Add("WrapperSubsetEval");
            combA.DataSource = AttrA;
        }
        //选择事件联动
        private void combA_SelectedIndexChanged(object sender, EventArgs e)
        {
            int ix = combA.SelectedIndex;
            AttrB = new List<string>();
            AttrB.Add("BestFirst");
            AttrB.Add("GreedyStepwise");
            AttrB.Add("Ranker");
            if (ix == 0 || ix == 8)
            {
                AttrB.RemoveAt(2);
            }
            else
            {
                AttrB.Reverse();
                AttrB.RemoveAt(2);
                AttrB.RemoveAt(1);
            }
            combB.DataSource = AttrB;
        }
        private void butSelAttr_Click(object sender, EventArgs e)
        {
            int attr1 = combA.SelectedIndex;
            int attr2 = combB.SelectedIndex;
        }

            //属性选择
            /*private void butSelAttr_Click(object sender, EventArgs e)
            {
                int attr1 = combA.SelectedIndex;
                int attr2 = combB.SelectedIndex;
                int[] attrIndex=null;
                try
                {
                    File file = new File(@"C:\Users\asus\Desktop\技术文档\java调用weka\RS_traindata.arff");
                    ArffLoader loader = new ArffLoader();
                    loader.setFile(file);
                    trainIns = loader.getDataSet();
                    //在使用样本之前一定要首先设置instances的classIndex，否则在使用instances对象是会抛出异常           
                    trainIns.setClassIndex(trainIns.numAttributes() - 1);


                    /*Ranker rank = new Ranker();
                    CorrelationAttributeEval eval = new CorrelationAttributeEval();
                    eval.buildEvaluator(trainIns);
                    attrIndex = rank.search(eval, trainIns);*/
            /*if (attr1 == 0)
            {
                Ranker rank = new Ranker();
                CorrelationAttributeEval eval = new CorrelationAttributeEval();
                eval.buildEvaluator(trainIns);
                attrIndex = rank.search(eval, trainIns);
            }
            if (attr1 == 1) {
                Ranker rank = new Ranker();
                CorrelationAttributeEval eval = new CorrelationAttributeEval();
                eval.buildEvaluator(trainIns);
                attrIndex = rank.search(eval, trainIns);
            }
            if (attr1 == 2)
            {
                Ranker rank = new Ranker();
                GainRatioAttributeEval eval = new GainRatioAttributeEval();
                eval.buildEvaluator(trainIns);
                attrIndex = rank.search(eval, trainIns);
            }
            if (attr1 == 3)
            {
                Ranker rank = new Ranker();
                InfoGainAttributeEval eval = new InfoGainAttributeEval();
                eval.buildEvaluator(trainIns);

                attrIndex = rank.search(eval, trainIns);
            }
            if (attr1 == 4)
            {
                Ranker rank = new Ranker();
                OneRAttributeEval eval = new OneRAttributeEval();
                eval.buildEvaluator(trainIns);
                attrIndex = rank.search(eval, trainIns);
            }
            if (attr1 == 5)
            {
                Ranker rank = new Ranker();
                PrincipalComponents eval = new PrincipalComponents();
                eval.buildEvaluator(trainIns);
                attrIndex = rank.search(eval, trainIns);
            }
            if (attr1 == 6)
            {
                Ranker rank = new Ranker();
                ReliefFAttributeEval eval = new ReliefFAttributeEval();
                eval.buildEvaluator(trainIns);
                attrIndex = rank.search(eval, trainIns);
            }

            if (attr1 == 7)
            {
                Ranker rank = new Ranker();
                SymmetricalUncertAttributeEval eval = new SymmetricalUncertAttributeEval();
                eval.buildEvaluator(trainIns);
                attrIndex = rank.search(eval, trainIns);
            }
            if (attr1 == 8)
            {
                Ranker rank = new Ranker();
                InfoGainAttributeEval eval = new InfoGainAttributeEval();

                eval.buildEvaluator(trainIns);

                attrIndex = rank.search(eval, trainIns);
            }
            GetRes(attrIndex);
             StringBuffer attrInfoGainInfo = new StringBuffer();               
             attrInfoGainInfo.append("Ranked attributes:/n");

            /*for (int i = 0; i < attrIndex.Length; i++)
            {

               // attrIndexInfo.append(attrIndex[i]);

               // attrIndexInfo.append(",");

                attrInfoGainInfo.append(eval.evaluateAttribute(attrIndex[i]));

                attrInfoGainInfo.append("/t");

                attrInfoGainInfo.append((trainIns.attribute(attrIndex[i]).name()));

                attrInfoGainInfo.append("/n");

            }
            MessageBox.Show(attrInfoGainInfo.toString());*/
            /* }
             catch (java.lang.Exception en)
             {
                 MessageBox.Show(en.Message);
             }
         }*/

            /*private void GetRes(int[] attrIndex) {
                StringBuffer attrIndexInfo = new StringBuffer();
                attrIndexInfo.append("Selected attributes:");
                for (int i = 0; i < attrIndex.Length; i++)
                {

                    attrIndexInfo.append(attrIndex[i]);

                    attrIndexInfo.append(",");
                }

                MessageBox.Show(attrIndexInfo.toString());
            }*/

            //加头部和类型
            /* private void buSetHead_Click(object sender, EventArgs e)
             {
                 //  changeAff(foldPath, @"C:\Users\asus\Desktop\RS.arff");
                 //clanum类别
                 string clanum = textBox1.Text.Trim();
                 if (foldPath != null && foldPath != "" && clanum!=null && clanum!="")
                 {
                     //文件名称例如c://eas  foldname表示eas ParPath表示c://
                     string foldname = foldPath.Substring(foldPath.LastIndexOf('\\')).Replace("\\", "");
                     string ParPath = System.Text.RegularExpressions.Regex.Replace(foldPath, @foldname, "");
                     // CsvArff文件夹目录路径
                     string CsvArffFoldPath = ParPath + "Arff_" + foldname;
                     if (!System.IO.Directory.Exists(CsvArffFoldPath))
                     {
                         System.IO.DirectoryInfo directoryInfo = new System.IO.DirectoryInfo(CsvArffFoldPath);
                         directoryInfo.Create();
                     }
                     // 文件的目录
                     System.IO.DirectoryInfo Dir = new System.IO.DirectoryInfo(foldPath);
                     foreach (System.IO.FileInfo FI in Dir.GetFiles())
                     {
                         // 这里写文件格式
                         if (System.IO.Path.GetExtension(FI.Name) == ".csv")
                         {
                             //得到csv文件名称
                             string CsvFilePath = foldPath + "\\"+FI.Name;
                             DataTable dt = CsvDel.OpenCSV(CsvFilePath, clanum);
                             //CSV文件保存路劲
                             string CSVSavePath = CsvArffFoldPath + "\\" + FI.Name;
                             //Arff文件保存路劲
                             string ArffPath = CsvArffFoldPath + "\\"+ System.IO.Path.GetFileNameWithoutExtension(CsvFilePath) +".arff";
                             CsvDel.SaveCSV(dt, CSVSavePath);
                             changeAff(CSVSavePath, ArffPath);
                         }
                     }
                     MessageBox.Show("CSV、ARFF文件保存成功！");
                 }
                 else
                 {
                     MessageBox.Show("请选择路径或输入类型！");
                 }
             }*/
        }
    class Bridname {
        public int id;
        public string name;
    }
}
