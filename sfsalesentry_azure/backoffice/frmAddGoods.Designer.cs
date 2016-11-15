namespace shopsales_tools
{
    partial class frmAddGoods
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
            this.autoGenerateGoodsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.txtModelSearch = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtColorSearch = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtSizeSearch = new System.Windows.Forms.TextBox();
            this.txtOID = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cboSMid = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cboColId = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cboSTId = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.cboSSid = new System.Windows.Forms.ComboBox();
            this.txtSizeNo = new System.Windows.Forms.TextBox();
            this.txtGoodsCode = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cboProductGroup = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.cboProductCategory = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.cboProductKind = new System.Windows.Forms.ComboBox();
            this.txtGoodsName = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtStdSellPrice = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txtStdCostPrice = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.txtMinSalePrice = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.txtCmd = new System.Windows.Forms.TextBox();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mainToolStripMenuItem,
            this.autoGenerateGoodsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(483, 24);
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
            this.deleteFromXMLToolStripMenuItem.Enabled = false;
            this.deleteFromXMLToolStripMenuItem.Name = "deleteFromXMLToolStripMenuItem";
            this.deleteFromXMLToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F3;
            this.deleteFromXMLToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.deleteFromXMLToolStripMenuItem.Text = "Cancel Data";
            this.deleteFromXMLToolStripMenuItem.Click += new System.EventHandler(this.deleteFromXMLToolStripMenuItem_Click);
            // 
            // autoGenerateGoodsToolStripMenuItem
            // 
            this.autoGenerateGoodsToolStripMenuItem.Name = "autoGenerateGoodsToolStripMenuItem";
            this.autoGenerateGoodsToolStripMenuItem.Size = new System.Drawing.Size(132, 20);
            this.autoGenerateGoodsToolStripMenuItem.Text = "Auto Generate Goods";
            this.autoGenerateGoodsToolStripMenuItem.Click += new System.EventHandler(this.autoGenerateGoodsToolStripMenuItem_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 60);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(459, 195);
            this.dataGridView1.TabIndex = 4;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_RowEnter);
            // 
            // txtModelSearch
            // 
            this.txtModelSearch.Location = new System.Drawing.Point(105, 31);
            this.txtModelSearch.Name = "txtModelSearch";
            this.txtModelSearch.Size = new System.Drawing.Size(100, 20);
            this.txtModelSearch.TabIndex = 5;
            this.txtModelSearch.TextChanged += new System.EventHandler(this.txtModelSearch_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(63, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Model";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Find By:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(215, 34);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(13, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "สี";
            // 
            // txtColorSearch
            // 
            this.txtColorSearch.Location = new System.Drawing.Point(234, 31);
            this.txtColorSearch.Name = "txtColorSearch";
            this.txtColorSearch.Size = new System.Drawing.Size(81, 20);
            this.txtColorSearch.TabIndex = 8;
            this.txtColorSearch.TextChanged += new System.EventHandler(this.txtColorSearch_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(321, 34);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(27, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Size";
            // 
            // txtSizeSearch
            // 
            this.txtSizeSearch.Location = new System.Drawing.Point(355, 31);
            this.txtSizeSearch.Name = "txtSizeSearch";
            this.txtSizeSearch.Size = new System.Drawing.Size(81, 20);
            this.txtSizeSearch.TabIndex = 10;
            this.txtSizeSearch.TextChanged += new System.EventHandler(this.txtSizeSearch_TextChanged);
            // 
            // txtOID
            // 
            this.txtOID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtOID.Location = new System.Drawing.Point(12, 274);
            this.txtOID.Name = "txtOID";
            this.txtOID.Size = new System.Drawing.Size(36, 20);
            this.txtOID.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 258);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(18, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "ID";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            this.label5.DoubleClick += new System.EventHandler(this.label5_DoubleClick);
            // 
            // cboSMid
            // 
            this.cboSMid.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cboSMid.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSMid.FormattingEnabled = true;
            this.cboSMid.Location = new System.Drawing.Point(53, 274);
            this.cboSMid.Name = "cboSMid";
            this.cboSMid.Size = new System.Drawing.Size(70, 21);
            this.cboSMid.TabIndex = 14;
            this.cboSMid.SelectedIndexChanged += new System.EventHandler(this.cboSMid_SelectedIndexChanged);
            this.cboSMid.DropDownClosed += new System.EventHandler(this.cboSMid_DropDownClosed);
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(50, 258);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(21, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "รุ่น";
            // 
            // cboColId
            // 
            this.cboColId.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cboColId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboColId.FormattingEnabled = true;
            this.cboColId.Location = new System.Drawing.Point(129, 274);
            this.cboColId.Name = "cboColId";
            this.cboColId.Size = new System.Drawing.Size(99, 21);
            this.cboColId.TabIndex = 16;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(126, 258);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(13, 13);
            this.label7.TabIndex = 17;
            this.label7.Text = "สี";
            // 
            // cboSTId
            // 
            this.cboSTId.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cboSTId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSTId.FormattingEnabled = true;
            this.cboSTId.Location = new System.Drawing.Point(234, 275);
            this.cboSTId.Name = "cboSTId";
            this.cboSTId.Size = new System.Drawing.Size(84, 21);
            this.cboSTId.TabIndex = 18;
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(231, 259);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(44, 13);
            this.label8.TabIndex = 19;
            this.label8.Text = "ประเภท";
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(321, 259);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(27, 13);
            this.label9.TabIndex = 21;
            this.label9.Text = "Size";
            // 
            // cboSSid
            // 
            this.cboSSid.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cboSSid.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSSid.FormattingEnabled = true;
            this.cboSSid.Location = new System.Drawing.Point(324, 275);
            this.cboSSid.Name = "cboSSid";
            this.cboSSid.Size = new System.Drawing.Size(85, 21);
            this.cboSSid.TabIndex = 20;
            // 
            // txtSizeNo
            // 
            this.txtSizeNo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtSizeNo.Location = new System.Drawing.Point(415, 275);
            this.txtSizeNo.Name = "txtSizeNo";
            this.txtSizeNo.Size = new System.Drawing.Size(54, 20);
            this.txtSizeNo.TabIndex = 22;
            // 
            // txtGoodsCode
            // 
            this.txtGoodsCode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtGoodsCode.Location = new System.Drawing.Point(12, 322);
            this.txtGoodsCode.Name = "txtGoodsCode";
            this.txtGoodsCode.Size = new System.Drawing.Size(111, 20);
            this.txtGoodsCode.TabIndex = 23;
            this.txtGoodsCode.TextChanged += new System.EventHandler(this.txtGoodsCode_TextChanged);
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(11, 306);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(35, 13);
            this.label10.TabIndex = 24;
            this.label10.Text = "Code.";
            this.label10.Click += new System.EventHandler(this.label10_Click);
            this.label10.DoubleClick += new System.EventHandler(this.label10_DoubleClick);
            // 
            // cboProductGroup
            // 
            this.cboProductGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cboProductGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboProductGroup.FormattingEnabled = true;
            this.cboProductGroup.Location = new System.Drawing.Point(129, 322);
            this.cboProductGroup.Name = "cboProductGroup";
            this.cboProductGroup.Size = new System.Drawing.Size(74, 21);
            this.cboProductGroup.TabIndex = 25;
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(126, 306);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(27, 13);
            this.label11.TabIndex = 26;
            this.label11.Text = "กลุ่ม";
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(208, 305);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(38, 13);
            this.label12.TabIndex = 28;
            this.label12.Text = "สำหรับ";
            // 
            // cboProductCategory
            // 
            this.cboProductCategory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cboProductCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboProductCategory.FormattingEnabled = true;
            this.cboProductCategory.Location = new System.Drawing.Point(208, 321);
            this.cboProductCategory.Name = "cboProductCategory";
            this.cboProductCategory.Size = new System.Drawing.Size(140, 21);
            this.cboProductCategory.TabIndex = 27;
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(350, 306);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(33, 13);
            this.label13.TabIndex = 30;
            this.label13.Text = "สต็อก";
            // 
            // cboProductKind
            // 
            this.cboProductKind.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cboProductKind.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboProductKind.FormattingEnabled = true;
            this.cboProductKind.Location = new System.Drawing.Point(354, 322);
            this.cboProductKind.Name = "cboProductKind";
            this.cboProductKind.Size = new System.Drawing.Size(115, 21);
            this.cboProductKind.TabIndex = 29;
            // 
            // txtGoodsName
            // 
            this.txtGoodsName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtGoodsName.Location = new System.Drawing.Point(15, 362);
            this.txtGoodsName.Name = "txtGoodsName";
            this.txtGoodsName.Size = new System.Drawing.Size(188, 20);
            this.txtGoodsName.TabIndex = 31;
            // 
            // label14
            // 
            this.label14.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(12, 346);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(75, 13);
            this.label14.TabIndex = 32;
            this.label14.Text = "Product Name";
            // 
            // txtStdSellPrice
            // 
            this.txtStdSellPrice.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtStdSellPrice.Location = new System.Drawing.Point(206, 362);
            this.txtStdSellPrice.Name = "txtStdSellPrice";
            this.txtStdSellPrice.Size = new System.Drawing.Size(74, 20);
            this.txtStdSellPrice.TabIndex = 33;
            // 
            // label15
            // 
            this.label15.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(203, 346);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(70, 13);
            this.label15.TabIndex = 34;
            this.label15.Text = "Std Sell Price";
            // 
            // txtStdCostPrice
            // 
            this.txtStdCostPrice.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtStdCostPrice.Location = new System.Drawing.Point(286, 362);
            this.txtStdCostPrice.Name = "txtStdCostPrice";
            this.txtStdCostPrice.Size = new System.Drawing.Size(74, 20);
            this.txtStdCostPrice.TabIndex = 35;
            // 
            // label16
            // 
            this.label16.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(286, 346);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(74, 13);
            this.label16.TabIndex = 36;
            this.label16.Text = "Std Cost Price";
            // 
            // label17
            // 
            this.label17.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(366, 346);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(55, 13);
            this.label17.TabIndex = 38;
            this.label17.Text = "Sale Price";
            // 
            // txtMinSalePrice
            // 
            this.txtMinSalePrice.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtMinSalePrice.Location = new System.Drawing.Point(366, 362);
            this.txtMinSalePrice.Name = "txtMinSalePrice";
            this.txtMinSalePrice.Size = new System.Drawing.Size(74, 20);
            this.txtMinSalePrice.TabIndex = 37;
            // 
            // label18
            // 
            this.label18.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(412, 259);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(31, 13);
            this.label18.TabIndex = 39;
            this.label18.Text = "เบอร์";
            // 
            // txtCmd
            // 
            this.txtCmd.Location = new System.Drawing.Point(47, 81);
            this.txtCmd.Multiline = true;
            this.txtCmd.Name = "txtCmd";
            this.txtCmd.Size = new System.Drawing.Size(374, 133);
            this.txtCmd.TabIndex = 41;
            this.txtCmd.Visible = false;
            // 
            // frmAddGoods
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(483, 394);
            this.Controls.Add(this.txtCmd);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.txtMinSalePrice);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.txtStdCostPrice);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.txtStdSellPrice);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.txtGoodsName);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.cboProductKind);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.cboProductCategory);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.cboProductGroup);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtGoodsCode);
            this.Controls.Add(this.txtSizeNo);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.cboSSid);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.cboSTId);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cboColId);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cboSMid);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtOID);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtSizeSearch);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtColorSearch);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtModelSearch);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.menuStrip1);
            this.Name = "frmAddGoods";
            this.Text = "frmAddGoods";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.cboModel_FormClosed);
            this.Load += new System.EventHandler(this.frmAddGoods_Load);
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
        private System.Windows.Forms.TextBox txtModelSearch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtColorSearch;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtSizeSearch;
        private System.Windows.Forms.TextBox txtOID;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cboSMid;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cboColId;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cboSTId;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cboSSid;
        private System.Windows.Forms.TextBox txtSizeNo;
        private System.Windows.Forms.TextBox txtGoodsCode;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cboProductGroup;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox cboProductCategory;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox cboProductKind;
        private System.Windows.Forms.TextBox txtGoodsName;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtStdSellPrice;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtStdCostPrice;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txtMinSalePrice;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.ToolStripMenuItem autoGenerateGoodsToolStripMenuItem;
        private System.Windows.Forms.TextBox txtCmd;
    }
}