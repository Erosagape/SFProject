namespace shopsales_tools
{
    partial class frmAddModel
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
                db.Dispose();
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mainToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadXMLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addToXMLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteFromXMLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSMId = new System.Windows.Forms.TextBox();
            this.txtSMCode = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.cboSSId = new System.Windows.Forms.ComboBox();
            this.txtMinSize = new System.Windows.Forms.TextBox();
            this.txtMaxSize = new System.Windows.Forms.TextBox();
            this.cboMoldId = new System.Windows.Forms.ComboBox();
            this.cboSTId = new System.Windows.Forms.ComboBox();
            this.cboProdCatId = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.clearScreenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mainToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(417, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // mainToolStripMenuItem
            // 
            this.mainToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadXMLToolStripMenuItem,
            this.addToXMLToolStripMenuItem,
            this.deleteFromXMLToolStripMenuItem,
            this.clearScreenToolStripMenuItem});
            this.mainToolStripMenuItem.Name = "mainToolStripMenuItem";
            this.mainToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.mainToolStripMenuItem.Text = "Main";
            // 
            // loadXMLToolStripMenuItem
            // 
            this.loadXMLToolStripMenuItem.Name = "loadXMLToolStripMenuItem";
            this.loadXMLToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.loadXMLToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.loadXMLToolStripMenuItem.Text = "Refresh Data";
            this.loadXMLToolStripMenuItem.Click += new System.EventHandler(this.loadXMLToolStripMenuItem_Click);
            // 
            // addToXMLToolStripMenuItem
            // 
            this.addToXMLToolStripMenuItem.Name = "addToXMLToolStripMenuItem";
            this.addToXMLToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F4;
            this.addToXMLToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.addToXMLToolStripMenuItem.Text = "Save Data";
            this.addToXMLToolStripMenuItem.Click += new System.EventHandler(this.addToXMLToolStripMenuItem_Click);
            // 
            // deleteFromXMLToolStripMenuItem
            // 
            this.deleteFromXMLToolStripMenuItem.Enabled = false;
            this.deleteFromXMLToolStripMenuItem.Name = "deleteFromXMLToolStripMenuItem";
            this.deleteFromXMLToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F3;
            this.deleteFromXMLToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.deleteFromXMLToolStripMenuItem.Text = "Cancel Data";
            this.deleteFromXMLToolStripMenuItem.Click += new System.EventHandler(this.deleteFromXMLToolStripMenuItem_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(13, 28);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(392, 210);
            this.dataGridView1.TabIndex = 4;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_RowEnter);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 245);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(18, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "ID";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(66, 245);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Code";
            // 
            // txtSMId
            // 
            this.txtSMId.Location = new System.Drawing.Point(12, 264);
            this.txtSMId.Name = "txtSMId";
            this.txtSMId.Size = new System.Drawing.Size(49, 20);
            this.txtSMId.TabIndex = 7;
            // 
            // txtSMCode
            // 
            this.txtSMCode.Location = new System.Drawing.Point(67, 264);
            this.txtSMCode.Name = "txtSMCode";
            this.txtSMCode.Size = new System.Drawing.Size(58, 20);
            this.txtSMCode.TabIndex = 8;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(131, 264);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(93, 20);
            this.txtName.TabIndex = 9;
            // 
            // cboSSId
            // 
            this.cboSSId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSSId.FormattingEnabled = true;
            this.cboSSId.Location = new System.Drawing.Point(230, 263);
            this.cboSSId.Name = "cboSSId";
            this.cboSSId.Size = new System.Drawing.Size(103, 21);
            this.cboSSId.TabIndex = 10;
            // 
            // txtMinSize
            // 
            this.txtMinSize.Location = new System.Drawing.Point(339, 263);
            this.txtMinSize.Name = "txtMinSize";
            this.txtMinSize.Size = new System.Drawing.Size(29, 20);
            this.txtMinSize.TabIndex = 11;
            // 
            // txtMaxSize
            // 
            this.txtMaxSize.Location = new System.Drawing.Point(374, 262);
            this.txtMaxSize.Name = "txtMaxSize";
            this.txtMaxSize.Size = new System.Drawing.Size(31, 20);
            this.txtMaxSize.TabIndex = 12;
            // 
            // cboMoldId
            // 
            this.cboMoldId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMoldId.FormattingEnabled = true;
            this.cboMoldId.Location = new System.Drawing.Point(12, 304);
            this.cboMoldId.Name = "cboMoldId";
            this.cboMoldId.Size = new System.Drawing.Size(98, 21);
            this.cboMoldId.TabIndex = 13;
            // 
            // cboSTId
            // 
            this.cboSTId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSTId.FormattingEnabled = true;
            this.cboSTId.Location = new System.Drawing.Point(116, 304);
            this.cboSTId.Name = "cboSTId";
            this.cboSTId.Size = new System.Drawing.Size(108, 21);
            this.cboSTId.TabIndex = 14;
            // 
            // cboProdCatId
            // 
            this.cboProdCatId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboProdCatId.FormattingEnabled = true;
            this.cboProdCatId.Location = new System.Drawing.Point(231, 304);
            this.cboProdCatId.Name = "cboProdCatId";
            this.cboProdCatId.Size = new System.Drawing.Size(102, 21);
            this.cboProdCatId.TabIndex = 15;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(128, 245);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Name";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(230, 247);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "Size group";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(336, 247);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(24, 13);
            this.label5.TabIndex = 18;
            this.label5.Text = "Min";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(371, 247);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(27, 13);
            this.label6.TabIndex = 19;
            this.label6.Text = "Max";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 288);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(30, 13);
            this.label7.TabIndex = 20;
            this.label7.Text = "Mold";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(116, 288);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(31, 13);
            this.label8.TabIndex = 21;
            this.label8.Text = "Type";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(230, 288);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(49, 13);
            this.label9.TabIndex = 22;
            this.label9.Text = "Category";
            // 
            // clearScreenToolStripMenuItem
            // 
            this.clearScreenToolStripMenuItem.Name = "clearScreenToolStripMenuItem";
            this.clearScreenToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.clearScreenToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.clearScreenToolStripMenuItem.Text = "Clear Screen";
            this.clearScreenToolStripMenuItem.Click += new System.EventHandler(this.clearScreenToolStripMenuItem_Click);
            // 
            // frmAddModel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(417, 338);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cboProdCatId);
            this.Controls.Add(this.cboSTId);
            this.Controls.Add(this.cboMoldId);
            this.Controls.Add(this.txtMaxSize);
            this.Controls.Add(this.txtMinSize);
            this.Controls.Add(this.cboSSId);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.txtSMCode);
            this.Controls.Add(this.txtSMId);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.menuStrip1);
            this.Name = "frmAddModel";
            this.Text = "frmAddModel";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmAddModel_FormClosed);
            this.Load += new System.EventHandler(this.frmAddModel_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mainToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadXMLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addToXMLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteFromXMLToolStripMenuItem;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSMId;
        private System.Windows.Forms.TextBox txtSMCode;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.ComboBox cboSSId;
        private System.Windows.Forms.TextBox txtMinSize;
        private System.Windows.Forms.TextBox txtMaxSize;
        private System.Windows.Forms.ComboBox cboMoldId;
        private System.Windows.Forms.ComboBox cboSTId;
        private System.Windows.Forms.ComboBox cboProdCatId;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ToolStripMenuItem clearScreenToolStripMenuItem;
    }
}