using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace FeatureDetection
{
    public partial class Main : Form
    {   
        public Main()
        {
            InitializeComponent();
            
        }
       
        private void Main_Load(object sender, EventArgs e)
        {
            Form Fe = new FeatDe();
            SetnewForm(Fe);
        }

        private void 特征提取_Click(object sender, EventArgs e)
        {         
            Form Fe = new FeatDe();
            SetnewForm(Fe);
        }
        private void 预处理_Click_1(object sender, EventArgs e)
        {
            Form Fe = new PreTment();
            SetnewForm(Fe);
        }
        private void 数据分析ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form Fe = new DataAna();
            SetnewForm(Fe);
        }
        private void SetnewForm(Form Fe) {
            foreach (Form frm in this.MdiChildren)
            {
                frm.Close();
            }         
            Fe.MdiParent = this;
            Fe.WindowState = FormWindowState.Maximized;
            splitContainer2.Panel1.Controls.Add(Fe);
            Fe.Show();
        }

      
    }
}
