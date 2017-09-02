namespace FeatureDetection
{
    partial class SelGroup
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.butSelFlod = new System.Windows.Forms.Button();
            this.listView = new System.Windows.Forms.ListView();
            this.textfloder = new System.Windows.Forms.TextBox();
            this.butDel = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.textCsv = new System.Windows.Forms.TextBox();
            this.butSelCsv = new System.Windows.Forms.Button();
            this.butComment = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AllowDrop = true;
            this.panel1.Controls.Add(this.listBox1);
            this.panel1.Location = new System.Drawing.Point(12, 47);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(423, 378);
            this.panel1.TabIndex = 2;
            this.panel1.DragEnter += new System.Windows.Forms.DragEventHandler(this.panel1_DragEnter);
            // 
            // listBox1
            // 
            this.listBox1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.IntegralHeight = false;
            this.listBox1.ItemHeight = 14;
            this.listBox1.Location = new System.Drawing.Point(13, 8);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(393, 364);
            this.listBox1.TabIndex = 5;
            // 
            // butSelFlod
            // 
            this.butSelFlod.Location = new System.Drawing.Point(442, 18);
            this.butSelFlod.Name = "butSelFlod";
            this.butSelFlod.Size = new System.Drawing.Size(75, 23);
            this.butSelFlod.TabIndex = 3;
            this.butSelFlod.Text = "选择路径";
            this.butSelFlod.UseVisualStyleBackColor = true;
            this.butSelFlod.Click += new System.EventHandler(this.butSelFlod_Click);
            // 
            // listView
            // 
            this.listView.Location = new System.Drawing.Point(547, 47);
            this.listView.Name = "listView";
            this.listView.Size = new System.Drawing.Size(156, 364);
            this.listView.TabIndex = 4;
            this.listView.UseCompatibleStateImageBehavior = false;
            // 
            // textfloder
            // 
            this.textfloder.Location = new System.Drawing.Point(25, 18);
            this.textfloder.Multiline = true;
            this.textfloder.Name = "textfloder";
            this.textfloder.Size = new System.Drawing.Size(393, 25);
            this.textfloder.TabIndex = 5;
            // 
            // butDel
            // 
            this.butDel.Location = new System.Drawing.Point(452, 197);
            this.butDel.Name = "butDel";
            this.butDel.Size = new System.Drawing.Size(75, 23);
            this.butDel.TabIndex = 6;
            this.butDel.Text = "删除";
            this.butDel.UseVisualStyleBackColor = true;
            this.butDel.Click += new System.EventHandler(this.butDel_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(537, 433);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(101, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "确定转换Arff";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textCsv
            // 
            this.textCsv.Location = new System.Drawing.Point(25, 431);
            this.textCsv.Multiline = true;
            this.textCsv.Name = "textCsv";
            this.textCsv.Size = new System.Drawing.Size(393, 25);
            this.textCsv.TabIndex = 8;
            // 
            // butSelCsv
            // 
            this.butSelCsv.Location = new System.Drawing.Point(442, 433);
            this.butSelCsv.Name = "butSelCsv";
            this.butSelCsv.Size = new System.Drawing.Size(75, 23);
            this.butSelCsv.TabIndex = 9;
            this.butSelCsv.Text = "选择路径";
            this.butSelCsv.UseVisualStyleBackColor = true;
            this.butSelCsv.Click += new System.EventHandler(this.butSelCsv_Click);
            // 
            // butComment
            // 
            this.butComment.Location = new System.Drawing.Point(452, 242);
            this.butComment.Name = "butComment";
            this.butComment.Size = new System.Drawing.Size(75, 23);
            this.butComment.TabIndex = 10;
            this.butComment.Text = "确定组合";
            this.butComment.UseVisualStyleBackColor = true;
            this.butComment.Click += new System.EventHandler(this.butComment_Click);
            // 
            // SelGroup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(724, 479);
            this.Controls.Add(this.butComment);
            this.Controls.Add(this.butSelCsv);
            this.Controls.Add(this.textCsv);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.butDel);
            this.Controls.Add(this.textfloder);
            this.Controls.Add(this.listView);
            this.Controls.Add(this.butSelFlod);
            this.Controls.Add(this.panel1);
            this.Name = "SelGroup";
            this.Text = "SelGroup";
            this.Load += new System.EventHandler(this.SelGroup_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button butSelFlod;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.ListView listView;
        private System.Windows.Forms.TextBox textfloder;
        private System.Windows.Forms.Button butDel;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textCsv;
        private System.Windows.Forms.Button butSelCsv;
        private System.Windows.Forms.Button butComment;
    }
}