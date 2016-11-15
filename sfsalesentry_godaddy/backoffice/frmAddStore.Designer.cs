namespace shopsales_tools
{
    partial class frmAddStore
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
                xml.Dispose();
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
            this.txtOID = new System.Windows.Forms.TextBox();
            this.txtCustCode = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtBranch = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtCustName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtShopName = new System.Windows.Forms.ComboBox();
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
            this.menuStrip1.Size = new System.Drawing.Size(477, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // mainToolStripMenuItem
            // 
            this.mainToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadXMLToolStripMenuItem,
            this.addToXMLToolStripMenuItem,
            this.deleteFromXMLToolStripMenuItem});
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
            this.deleteFromXMLToolStripMenuItem.Name = "deleteFromXMLToolStripMenuItem";
            this.deleteFromXMLToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F3;
            this.deleteFromXMLToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.deleteFromXMLToolStripMenuItem.Text = "Delete Data";
            this.deleteFromXMLToolStripMenuItem.Click += new System.EventHandler(this.deleteFromXMLToolStripMenuItem_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 37);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(453, 195);
            this.dataGridView1.TabIndex = 3;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_RowEnter);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 239);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(18, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "ID";
            // 
            // txtOID
            // 
            this.txtOID.Location = new System.Drawing.Point(12, 256);
            this.txtOID.Name = "txtOID";
            this.txtOID.Size = new System.Drawing.Size(42, 20);
            this.txtOID.TabIndex = 5;
            // 
            // txtCustCode
            // 
            this.txtCustCode.Location = new System.Drawing.Point(60, 256);
            this.txtCustCode.Name = "txtCustCode";
            this.txtCustCode.Size = new System.Drawing.Size(54, 20);
            this.txtCustCode.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(60, 239);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Code";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(117, 239);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Shop";
            // 
            // txtBranch
            // 
            this.txtBranch.Location = new System.Drawing.Point(219, 256);
            this.txtBranch.Name = "txtBranch";
            this.txtBranch.Size = new System.Drawing.Size(74, 20);
            this.txtBranch.TabIndex = 10;
            this.txtBranch.TextChanged += new System.EventHandler(this.txtBranch_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(216, 240);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Branch";
            // 
            // txtCustName
            // 
            this.txtCustName.Location = new System.Drawing.Point(299, 256);
            this.txtCustName.Name = "txtCustName";
            this.txtCustName.Size = new System.Drawing.Size(166, 20);
            this.txtCustName.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(299, 240);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "POS Name";
            // 
            // txtShopName
            // 
            this.txtShopName.FormattingEnabled = true;
            this.txtShopName.Location = new System.Drawing.Point(120, 256);
            this.txtShopName.Name = "txtShopName";
            this.txtShopName.Size = new System.Drawing.Size(93, 21);
            this.txtShopName.TabIndex = 14;
            this.txtShopName.SelectedIndexChanged += new System.EventHandler(this.txtShopName_SelectedIndexChanged);
            this.txtShopName.TextUpdate += new System.EventHandler(this.txtShopName_TextUpdate);
            // 
            // frmAddStore
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(477, 288);
            this.Controls.Add(this.txtShopName);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtCustName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtBranch);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtCustCode);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtOID);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.menuStrip1);
            this.Name = "frmAddStore";
            this.Text = "frmAddStore";
            this.Load += new System.EventHandler(this.frmAddStore_Load);
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
        private System.Windows.Forms.TextBox txtOID;
        private System.Windows.Forms.TextBox txtCustCode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtBranch;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtCustName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox txtShopName;
    }
}