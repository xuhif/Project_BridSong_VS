using FeatureDetection.modle;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace FeatureDetection
{
    //委托声明
    public delegate void ClickDelegateHander(string message);
    public partial class ClassT : Form
    {
       List<TreeNode> lt = new List<TreeNode>();
       public string[] intCla = new string[6];
       public string cla=null;
       public  string inte=null;
       int n = 0; 
        //委托事件
       public static event ClickDelegateHander ClickEvent;//声明一个事件

       DataAna Dob=null;// 
        public ClassT()
        {
            InitializeComponent();
            SetTree s = new SetTree(treeView1);
        }
        public ClassT(DataAna ob)
        {
            InitializeComponent();
            Dob = ob;
        }
        #region
        //初始化数
        private void getTNote() {
            for (int i = 0; i < 30; i++) {
                lt.Add(new TreeNode("J48")); lt.Add(new TreeNode("MultilayerPerceptron")); lt.Add(new TreeNode("SMO"));
                lt.Add(new TreeNode("RandomForest")); lt.Add(new TreeNode("BayesNet")); lt.Add(new TreeNode("NaiveBayes"));
            }         
        }
        //数据分析树
        private void ClassT_Load(object sender, EventArgs e)
        {
            getTNote();
            TreeNode tn1 = treeView1.Nodes.Add("单分类");
            TreeNode tn2 = treeView1.Nodes.Add("集成");
            TreeNode tn21 = new TreeNode("Bagging");
            TreeNode tn22 = new TreeNode("AdaBoostM1");
            TreeNode tn23 = new TreeNode("Vote");
            TreeNode tn24 = new TreeNode("Stacking");
            tn2.Nodes.Add(tn21); tn2.Nodes.Add(tn22);
            tn2.Nodes.Add(tn23); tn2.Nodes.Add(tn24);            
            for (int i = 0; i < 6; i++)  tn1.Nodes.Add(lt[i]);
            for (int i = 6; i < 12; i++) tn21.Nodes.Add(lt[i]);
            for (int i = 12; i < 18; i++) tn22.Nodes.Add(lt[i]);
            for (int i = 18; i < 24; i++) tn23.Nodes.Add(lt[i]);
            for (int i = 24; i < 30; i++) tn24.Nodes.Add(lt[i]);
        }
        #endregion
        //选择项
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {           
            if (treeView1.SelectedNode != null)
            {              
                TreeNode note= e.Node.Parent;
                if (note != null)
                {                   
                    if (note.Text.Equals("单分类"))
                    {
                        cla= treeView1.SelectedNode.Text;
                        inte = null;
                    }
                    if (note.Text.Equals("AdaBoostM1") || note.Text.Equals("Bagging")) {
                        inte = note.Text;
                        intCla[0] = treeView1.SelectedNode.Text;
                        cla = null;
                    }
                }             
            }
        }
        private void treeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {
            inte = null;
            intCla = new string[6];
            cla = null;
        }
        //确定按钮
        private void but_Click(object sender, EventArgs e)
        {
            getCheckedNode(treeView1.Nodes);
            if (inte == null && cla == null) {

                MessageBox.Show("选择出错，请重新选择！");
                return;
            }
            //DataAna静态变量赋值
            DataAna.fcla = cla;
            DataAna.finte = inte;
            DataAna.fintecla = intCla;
            string str = null;
            //委托事件传送消息
            if (cla != null)
            {
                str = "单分类: " + cla;
            }
            else {
                str= "集成 :" + inte;
            }
            ClickEvent?.Invoke(str);
            this.Close();
        }
        //看选项框中内容是否选择针对vote
        private void getCheckedNode(TreeNodeCollection nodes)
        {
            foreach (TreeNode node in nodes)
            {
                if (node.Checked) {
                    intCla[n] = node.Text;
                    if (inte!=null&& inte!= node.Parent.Text) {
                        MessageBox.Show("不能同时选择两种类型！");
                        inte = null;
                        intCla = new string[6];
                        cla = null;
                        return;
                    }
                    inte = node.Parent.Text;
                    n++;
                }
                //处理该节点.
                if (node.Nodes.Count > 0)
                {
                    getCheckedNode(node.Nodes);
                }
            }
        }

        
    }


}
