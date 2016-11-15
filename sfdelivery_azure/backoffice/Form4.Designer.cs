namespace SfDeliverTracking
{
    partial class Form4
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
            this.components = new System.ComponentModel.Container();
            this.button3 = new System.Windows.Forms.Button();
            this.lblTimes = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtMonth = new System.Windows.Forms.TextBox();
            this.txtYear = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtAppSrc = new System.Windows.Forms.TextBox();
            this.txtWebSrc = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button4 = new System.Windows.Forms.Button();
            this.lblFileName = new System.Windows.Forms.Label();
            this.dlg = new System.Windows.Forms.OpenFileDialog();
            this.button5 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataViewerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.xMLViewerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.jSONViewerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.openLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearLogFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.todayLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.allLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.scheduleRunNowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.downloadFromWebToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.compareXMLDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uploadCustomerDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testConnectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uploadJSONToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.txtRows = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.clearJSONDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.Location = new System.Drawing.Point(370, 399);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(107, 23);
            this.button3.TabIndex = 28;
            this.button3.Text = "Check With Web";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // lblTimes
            // 
            this.lblTimes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTimes.AutoSize = true;
            this.lblTimes.Location = new System.Drawing.Point(108, 404);
            this.lblTimes.Name = "lblTimes";
            this.lblTimes.Size = new System.Drawing.Size(38, 13);
            this.lblTimes.TabIndex = 27;
            this.lblTimes.Text = "Ready";
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(264, 78);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(70, 22);
            this.button2.TabIndex = 26;
            this.button2.Text = "Copy files";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(184, 78);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(74, 23);
            this.button1.TabIndex = 25;
            this.button1.Text = "Create XML";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // listBox2
            // 
            this.listBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBox2.FormattingEnabled = true;
            this.listBox2.Location = new System.Drawing.Point(279, 129);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(198, 264);
            this.listBox2.TabIndex = 24;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(109, 82);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 13);
            this.label4.TabIndex = 23;
            this.label4.Text = "Month";
            // 
            // txtMonth
            // 
            this.txtMonth.Location = new System.Drawing.Point(153, 79);
            this.txtMonth.Name = "txtMonth";
            this.txtMonth.Size = new System.Drawing.Size(25, 20);
            this.txtMonth.TabIndex = 22;
            // 
            // txtYear
            // 
            this.txtYear.Location = new System.Drawing.Point(47, 78);
            this.txtYear.Name = "txtYear";
            this.txtYear.Size = new System.Drawing.Size(56, 20);
            this.txtYear.TabIndex = 21;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 82);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 20;
            this.label3.Text = "Year";
            // 
            // listBox1
            // 
            this.listBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(341, 29);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(136, 69);
            this.listBox1.TabIndex = 19;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged_1);
            this.listBox1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBox1_MouseDoubleClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(22, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "DB";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 17;
            this.label1.Text = "Web";
            // 
            // txtAppSrc
            // 
            this.txtAppSrc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAppSrc.Location = new System.Drawing.Point(46, 55);
            this.txtAppSrc.Name = "txtAppSrc";
            this.txtAppSrc.Size = new System.Drawing.Size(288, 20);
            this.txtAppSrc.TabIndex = 16;
            // 
            // txtWebSrc
            // 
            this.txtWebSrc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtWebSrc.Location = new System.Drawing.Point(46, 29);
            this.txtWebSrc.Name = "txtWebSrc";
            this.txtWebSrc.Size = new System.Drawing.Size(288, 20);
            this.txtWebSrc.TabIndex = 15;
            this.txtWebSrc.Text = "C:\\Users\\Puttipong\\Documents\\Visual Studio 2015\\Projects\\sfdelivery\\sfdelivery";
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(11, 129);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(262, 266);
            this.dataGridView1.TabIndex = 30;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(11, 100);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(35, 23);
            this.button4.TabIndex = 31;
            this.button4.Text = "...";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // lblFileName
            // 
            this.lblFileName.AutoSize = true;
            this.lblFileName.Location = new System.Drawing.Point(52, 105);
            this.lblFileName.Name = "lblFileName";
            this.lblFileName.Size = new System.Drawing.Size(97, 13);
            this.lblFileName.TabIndex = 32;
            this.lblFileName.Text = "Browse JSON Files";
            this.lblFileName.Click += new System.EventHandler(this.lblFileName_Click);
            // 
            // dlg
            // 
            this.dlg.FileOk += new System.ComponentModel.CancelEventHandler(this.dlg_FileOk);
            // 
            // button5
            // 
            this.button5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button5.Location = new System.Drawing.Point(257, 398);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(107, 23);
            this.button5.TabIndex = 33;
            this.button5.Text = "Upload To Web";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(11, 129);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(462, 266);
            this.textBox1.TabIndex = 34;
            this.textBox1.Visible = false;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            this.textBox1.DoubleClick += new System.EventHandler(this.textBox1_DoubleClick);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuToolStripMenuItem,
            this.testConnectToolStripMenuItem,
            this.uploadJSONToolStripMenuItem,
            this.clearJSONDataToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(489, 24);
            this.menuStrip1.TabIndex = 35;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuToolStripMenuItem
            // 
            this.menuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dataViewerToolStripMenuItem,
            this.xMLViewerToolStripMenuItem,
            this.jSONViewerToolStripMenuItem,
            this.toolStripMenuItem1,
            this.openLogToolStripMenuItem,
            this.clearLogFilesToolStripMenuItem,
            this.toolStripMenuItem2,
            this.scheduleRunNowToolStripMenuItem,
            this.toolStripMenuItem3,
            this.downloadFromWebToolStripMenuItem,
            this.compareXMLDataToolStripMenuItem,
            this.uploadCustomerDataToolStripMenuItem});
            this.menuToolStripMenuItem.Name = "menuToolStripMenuItem";
            this.menuToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.menuToolStripMenuItem.Text = "Menu";
            // 
            // dataViewerToolStripMenuItem
            // 
            this.dataViewerToolStripMenuItem.Name = "dataViewerToolStripMenuItem";
            this.dataViewerToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.dataViewerToolStripMenuItem.Text = "Database Viewer";
            this.dataViewerToolStripMenuItem.Click += new System.EventHandler(this.dataViewerToolStripMenuItem_Click);
            // 
            // xMLViewerToolStripMenuItem
            // 
            this.xMLViewerToolStripMenuItem.Name = "xMLViewerToolStripMenuItem";
            this.xMLViewerToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.xMLViewerToolStripMenuItem.Text = "XML Viewer";
            this.xMLViewerToolStripMenuItem.Click += new System.EventHandler(this.xMLViewerToolStripMenuItem_Click);
            // 
            // jSONViewerToolStripMenuItem
            // 
            this.jSONViewerToolStripMenuItem.Name = "jSONViewerToolStripMenuItem";
            this.jSONViewerToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.jSONViewerToolStripMenuItem.Text = "JSON Viewer";
            this.jSONViewerToolStripMenuItem.Click += new System.EventHandler(this.jSONViewerToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(191, 6);
            // 
            // openLogToolStripMenuItem
            // 
            this.openLogToolStripMenuItem.Name = "openLogToolStripMenuItem";
            this.openLogToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.openLogToolStripMenuItem.Text = "Open Log Files";
            this.openLogToolStripMenuItem.Click += new System.EventHandler(this.openLogToolStripMenuItem_Click);
            // 
            // clearLogFilesToolStripMenuItem
            // 
            this.clearLogFilesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.todayLogToolStripMenuItem,
            this.allLogToolStripMenuItem});
            this.clearLogFilesToolStripMenuItem.Name = "clearLogFilesToolStripMenuItem";
            this.clearLogFilesToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.clearLogFilesToolStripMenuItem.Text = "Clear Log Files";
            this.clearLogFilesToolStripMenuItem.Click += new System.EventHandler(this.clearLogFilesToolStripMenuItem_Click);
            // 
            // todayLogToolStripMenuItem
            // 
            this.todayLogToolStripMenuItem.Name = "todayLogToolStripMenuItem";
            this.todayLogToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.todayLogToolStripMenuItem.Text = "Today Log";
            this.todayLogToolStripMenuItem.Click += new System.EventHandler(this.todayLogToolStripMenuItem_Click);
            // 
            // allLogToolStripMenuItem
            // 
            this.allLogToolStripMenuItem.Name = "allLogToolStripMenuItem";
            this.allLogToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.allLogToolStripMenuItem.Text = "All Log";
            this.allLogToolStripMenuItem.Click += new System.EventHandler(this.allLogToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(191, 6);
            // 
            // scheduleRunNowToolStripMenuItem
            // 
            this.scheduleRunNowToolStripMenuItem.Name = "scheduleRunNowToolStripMenuItem";
            this.scheduleRunNowToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.scheduleRunNowToolStripMenuItem.Text = "Schedule Run Now!";
            this.scheduleRunNowToolStripMenuItem.Click += new System.EventHandler(this.scheduleRunNowToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(191, 6);
            // 
            // downloadFromWebToolStripMenuItem
            // 
            this.downloadFromWebToolStripMenuItem.Name = "downloadFromWebToolStripMenuItem";
            this.downloadFromWebToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.downloadFromWebToolStripMenuItem.Text = "Download From Web";
            this.downloadFromWebToolStripMenuItem.Click += new System.EventHandler(this.downloadFromWebToolStripMenuItem_Click);
            // 
            // compareXMLDataToolStripMenuItem
            // 
            this.compareXMLDataToolStripMenuItem.Name = "compareXMLDataToolStripMenuItem";
            this.compareXMLDataToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.compareXMLDataToolStripMenuItem.Text = "Compare XML Data";
            this.compareXMLDataToolStripMenuItem.Click += new System.EventHandler(this.compareXMLDataToolStripMenuItem_Click);
            // 
            // uploadCustomerDataToolStripMenuItem
            // 
            this.uploadCustomerDataToolStripMenuItem.Name = "uploadCustomerDataToolStripMenuItem";
            this.uploadCustomerDataToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.uploadCustomerDataToolStripMenuItem.Text = "Upload Customer Data";
            this.uploadCustomerDataToolStripMenuItem.Click += new System.EventHandler(this.uploadCustomerDataToolStripMenuItem_Click);
            // 
            // testConnectToolStripMenuItem
            // 
            this.testConnectToolStripMenuItem.Name = "testConnectToolStripMenuItem";
            this.testConnectToolStripMenuItem.Size = new System.Drawing.Size(89, 20);
            this.testConnectToolStripMenuItem.Text = "Test Connect";
            this.testConnectToolStripMenuItem.Click += new System.EventHandler(this.testConnectToolStripMenuItem_Click);
            // 
            // uploadJSONToolStripMenuItem
            // 
            this.uploadJSONToolStripMenuItem.Name = "uploadJSONToolStripMenuItem";
            this.uploadJSONToolStripMenuItem.Size = new System.Drawing.Size(88, 20);
            this.uploadJSONToolStripMenuItem.Text = "Upload JSON";
            this.uploadJSONToolStripMenuItem.Click += new System.EventHandler(this.uploadJSONToolStripMenuItem_Click);
            // 
            // txtRows
            // 
            this.txtRows.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtRows.Location = new System.Drawing.Point(69, 402);
            this.txtRows.Name = "txtRows";
            this.txtRows.Size = new System.Drawing.Size(34, 20);
            this.txtRows.TabIndex = 36;
            this.txtRows.Text = "60";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 403);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 13);
            this.label5.TabIndex = 37;
            this.label5.Text = "Row/Files";
            // 
            // clearJSONDataToolStripMenuItem
            // 
            this.clearJSONDataToolStripMenuItem.Name = "clearJSONDataToolStripMenuItem";
            this.clearJSONDataToolStripMenuItem.Size = new System.Drawing.Size(104, 20);
            this.clearJSONDataToolStripMenuItem.Text = "Clear JSON Data";
            this.clearJSONDataToolStripMenuItem.Click += new System.EventHandler(this.clearJSONDataToolStripMenuItem_Click);
            // 
            // Form4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(489, 425);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtRows);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.lblFileName);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.lblTimes);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.listBox2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtMonth);
            this.Controls.Add(this.txtYear);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtAppSrc);
            this.Controls.Add(this.txtWebSrc);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form4";
            this.Load += new System.EventHandler(this.Form4_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label lblTimes;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtMonth;
        private System.Windows.Forms.TextBox txtYear;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtAppSrc;
        private System.Windows.Forms.TextBox txtWebSrc;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label lblFileName;
        private System.Windows.Forms.OpenFileDialog dlg;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dataViewerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem xMLViewerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem jSONViewerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openLogToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearLogFilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem scheduleRunNowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem downloadFromWebToolStripMenuItem;
        private System.Windows.Forms.TextBox txtRows;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ToolStripMenuItem compareXMLDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem uploadCustomerDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem testConnectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uploadJSONToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem todayLogToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem allLogToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearJSONDataToolStripMenuItem;
    }
}