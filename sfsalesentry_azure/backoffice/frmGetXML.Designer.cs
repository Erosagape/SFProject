namespace shopsales_tools
{
    partial class frmGetXML
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.addUserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addStoreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addModelMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.addGoodsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.updateDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.queryDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadSalesDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateDataToServerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updatePriceListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.regenerateXMLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.customerxmlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.goodsxmlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.shoeTypexmlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.shoeCategoryxmlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.colorxmlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listuserxmlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.shoeGroupxmlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.salesTypexmlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.customerGroupcmlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.shoeModelxmlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.shoeMoldxmlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.shoeSizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.utilToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.sendDailyReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 55);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(547, 242);
            this.dataGridView1.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuToolStripMenuItem,
            this.utilToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(571, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // menuToolStripMenuItem
            // 
            this.menuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectToolStripMenuItem,
            this.queryDataToolStripMenuItem,
            this.toolStripMenuItem1,
            this.addUserToolStripMenuItem,
            this.addStoreToolStripMenuItem,
            this.addModelMenuItem3,
            this.addGoodsToolStripMenuItem,
            this.toolStripMenuItem2,
            this.updateDataToolStripMenuItem,
            this.loadSalesDataToolStripMenuItem,
            this.updateDataToServerToolStripMenuItem,
            this.updatePriceListToolStripMenuItem,
            this.regenerateXMLToolStripMenuItem,
            this.toolStripMenuItem3,
            this.sendDailyReportToolStripMenuItem});
            this.menuToolStripMenuItem.Name = "menuToolStripMenuItem";
            this.menuToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.menuToolStripMenuItem.Text = "Menu";
            // 
            // connectToolStripMenuItem
            // 
            this.connectToolStripMenuItem.Name = "connectToolStripMenuItem";
            this.connectToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.connectToolStripMenuItem.Text = "Connect";
            this.connectToolStripMenuItem.Click += new System.EventHandler(this.connectToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(191, 6);
            // 
            // addUserToolStripMenuItem
            // 
            this.addUserToolStripMenuItem.Name = "addUserToolStripMenuItem";
            this.addUserToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.addUserToolStripMenuItem.Text = "Add User";
            this.addUserToolStripMenuItem.Click += new System.EventHandler(this.addUserToolStripMenuItem_Click);
            // 
            // addStoreToolStripMenuItem
            // 
            this.addStoreToolStripMenuItem.Name = "addStoreToolStripMenuItem";
            this.addStoreToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.addStoreToolStripMenuItem.Text = "Add Store";
            this.addStoreToolStripMenuItem.Click += new System.EventHandler(this.addStoreToolStripMenuItem_Click);
            // 
            // addModelMenuItem3
            // 
            this.addModelMenuItem3.Name = "addModelMenuItem3";
            this.addModelMenuItem3.Size = new System.Drawing.Size(194, 22);
            this.addModelMenuItem3.Text = "Add Model";
            this.addModelMenuItem3.Click += new System.EventHandler(this.toolStripMenuItem3_Click);
            // 
            // addGoodsToolStripMenuItem
            // 
            this.addGoodsToolStripMenuItem.Name = "addGoodsToolStripMenuItem";
            this.addGoodsToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.addGoodsToolStripMenuItem.Text = "Add Goods";
            this.addGoodsToolStripMenuItem.Click += new System.EventHandler(this.addGoodsToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(191, 6);
            // 
            // updateDataToolStripMenuItem
            // 
            this.updateDataToolStripMenuItem.Name = "updateDataToolStripMenuItem";
            this.updateDataToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.updateDataToolStripMenuItem.Text = "Sync Database  to Web";
            this.updateDataToolStripMenuItem.Click += new System.EventHandler(this.updateDataToolStripMenuItem_Click);
            // 
            // queryDataToolStripMenuItem
            // 
            this.queryDataToolStripMenuItem.Name = "queryDataToolStripMenuItem";
            this.queryDataToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.queryDataToolStripMenuItem.Text = "Query Data from Web";
            this.queryDataToolStripMenuItem.Click += new System.EventHandler(this.queryDataToolStripMenuItem_Click);
            // 
            // loadSalesDataToolStripMenuItem
            // 
            this.loadSalesDataToolStripMenuItem.Name = "loadSalesDataToolStripMenuItem";
            this.loadSalesDataToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.loadSalesDataToolStripMenuItem.Text = "Load Sales Data";
            this.loadSalesDataToolStripMenuItem.Click += new System.EventHandler(this.loadSalesDataToolStripMenuItem_Click);
            // 
            // updateDataToServerToolStripMenuItem
            // 
            this.updateDataToServerToolStripMenuItem.Name = "updateDataToServerToolStripMenuItem";
            this.updateDataToServerToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.updateDataToServerToolStripMenuItem.Text = "Update Data to Server";
            this.updateDataToServerToolStripMenuItem.Click += new System.EventHandler(this.updateDataToServerToolStripMenuItem_Click);
            // 
            // updatePriceListToolStripMenuItem
            // 
            this.updatePriceListToolStripMenuItem.Name = "updatePriceListToolStripMenuItem";
            this.updatePriceListToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.updatePriceListToolStripMenuItem.Text = "Update Price List";
            this.updatePriceListToolStripMenuItem.Click += new System.EventHandler(this.updatePriceListToolStripMenuItem_Click);
            // 
            // regenerateXMLToolStripMenuItem
            // 
            this.regenerateXMLToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.customerxmlToolStripMenuItem,
            this.goodsxmlToolStripMenuItem,
            this.shoeTypexmlToolStripMenuItem,
            this.shoeCategoryxmlToolStripMenuItem,
            this.colorxmlToolStripMenuItem,
            this.listuserxmlToolStripMenuItem,
            this.shoeGroupxmlToolStripMenuItem,
            this.salesTypexmlToolStripMenuItem,
            this.customerGroupcmlToolStripMenuItem,
            this.shoeModelxmlToolStripMenuItem,
            this.shoeMoldxmlToolStripMenuItem,
            this.shoeSizeToolStripMenuItem});
            this.regenerateXMLToolStripMenuItem.Name = "regenerateXMLToolStripMenuItem";
            this.regenerateXMLToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.regenerateXMLToolStripMenuItem.Text = "Regenerate XML";
            this.regenerateXMLToolStripMenuItem.Click += new System.EventHandler(this.regenerateXMLToolStripMenuItem_Click);
            // 
            // customerxmlToolStripMenuItem
            // 
            this.customerxmlToolStripMenuItem.Name = "customerxmlToolStripMenuItem";
            this.customerxmlToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.customerxmlToolStripMenuItem.Text = "Customer.xml";
            this.customerxmlToolStripMenuItem.Click += new System.EventHandler(this.customerxmlToolStripMenuItem_Click);
            // 
            // goodsxmlToolStripMenuItem
            // 
            this.goodsxmlToolStripMenuItem.Name = "goodsxmlToolStripMenuItem";
            this.goodsxmlToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.goodsxmlToolStripMenuItem.Text = "Goods.xml";
            this.goodsxmlToolStripMenuItem.Click += new System.EventHandler(this.goodsxmlToolStripMenuItem_Click);
            // 
            // shoeTypexmlToolStripMenuItem
            // 
            this.shoeTypexmlToolStripMenuItem.Name = "shoeTypexmlToolStripMenuItem";
            this.shoeTypexmlToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.shoeTypexmlToolStripMenuItem.Text = "ShoeType.xml";
            this.shoeTypexmlToolStripMenuItem.Click += new System.EventHandler(this.shoeTypexmlToolStripMenuItem_Click);
            // 
            // shoeCategoryxmlToolStripMenuItem
            // 
            this.shoeCategoryxmlToolStripMenuItem.Name = "shoeCategoryxmlToolStripMenuItem";
            this.shoeCategoryxmlToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.shoeCategoryxmlToolStripMenuItem.Text = "ShoeCategory.xml";
            this.shoeCategoryxmlToolStripMenuItem.Click += new System.EventHandler(this.shoeCategoryxmlToolStripMenuItem_Click);
            // 
            // colorxmlToolStripMenuItem
            // 
            this.colorxmlToolStripMenuItem.Name = "colorxmlToolStripMenuItem";
            this.colorxmlToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.colorxmlToolStripMenuItem.Text = "Color.xml";
            this.colorxmlToolStripMenuItem.Click += new System.EventHandler(this.colorxmlToolStripMenuItem_Click);
            // 
            // listuserxmlToolStripMenuItem
            // 
            this.listuserxmlToolStripMenuItem.Name = "listuserxmlToolStripMenuItem";
            this.listuserxmlToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.listuserxmlToolStripMenuItem.Text = "listuser.xml";
            this.listuserxmlToolStripMenuItem.Click += new System.EventHandler(this.listuserxmlToolStripMenuItem_Click);
            // 
            // shoeGroupxmlToolStripMenuItem
            // 
            this.shoeGroupxmlToolStripMenuItem.Name = "shoeGroupxmlToolStripMenuItem";
            this.shoeGroupxmlToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.shoeGroupxmlToolStripMenuItem.Text = "ShoeGroup.xml";
            this.shoeGroupxmlToolStripMenuItem.Click += new System.EventHandler(this.shoeGroupxmlToolStripMenuItem_Click);
            // 
            // salesTypexmlToolStripMenuItem
            // 
            this.salesTypexmlToolStripMenuItem.Name = "salesTypexmlToolStripMenuItem";
            this.salesTypexmlToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.salesTypexmlToolStripMenuItem.Text = "SalesType.xml";
            this.salesTypexmlToolStripMenuItem.Click += new System.EventHandler(this.salesTypexmlToolStripMenuItem_Click);
            // 
            // customerGroupcmlToolStripMenuItem
            // 
            this.customerGroupcmlToolStripMenuItem.Name = "customerGroupcmlToolStripMenuItem";
            this.customerGroupcmlToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.customerGroupcmlToolStripMenuItem.Text = "CustomerGroup.xml";
            this.customerGroupcmlToolStripMenuItem.Click += new System.EventHandler(this.customerGroupcmlToolStripMenuItem_Click);
            // 
            // shoeModelxmlToolStripMenuItem
            // 
            this.shoeModelxmlToolStripMenuItem.Name = "shoeModelxmlToolStripMenuItem";
            this.shoeModelxmlToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.shoeModelxmlToolStripMenuItem.Text = "ShoeModel.xml";
            this.shoeModelxmlToolStripMenuItem.Click += new System.EventHandler(this.shoeModelxmlToolStripMenuItem_Click);
            // 
            // shoeMoldxmlToolStripMenuItem
            // 
            this.shoeMoldxmlToolStripMenuItem.Name = "shoeMoldxmlToolStripMenuItem";
            this.shoeMoldxmlToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.shoeMoldxmlToolStripMenuItem.Text = "ShoeMold.xml";
            this.shoeMoldxmlToolStripMenuItem.Click += new System.EventHandler(this.shoeMoldxmlToolStripMenuItem_Click);
            // 
            // shoeSizeToolStripMenuItem
            // 
            this.shoeSizeToolStripMenuItem.Name = "shoeSizeToolStripMenuItem";
            this.shoeSizeToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.shoeSizeToolStripMenuItem.Text = "ShoeSize.xml";
            this.shoeSizeToolStripMenuItem.Click += new System.EventHandler(this.shoeSizeToolStripMenuItem_Click);
            // 
            // utilToolStripMenuItem
            // 
            this.utilToolStripMenuItem.Name = "utilToolStripMenuItem";
            this.utilToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.utilToolStripMenuItem.Text = "Util";
            this.utilToolStripMenuItem.Click += new System.EventHandler(this.utilToolStripMenuItem_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(13, 28);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(132, 21);
            this.comboBox1.TabIndex = 4;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(191, 6);
            // 
            // sendDailyReportToolStripMenuItem
            // 
            this.sendDailyReportToolStripMenuItem.Name = "sendDailyReportToolStripMenuItem";
            this.sendDailyReportToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.sendDailyReportToolStripMenuItem.Text = "Send Daily Report";
            this.sendDailyReportToolStripMenuItem.Click += new System.EventHandler(this.sendDailyReportToolStripMenuItem_Click);
            // 
            // frmGetXML
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(571, 307);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmGetXML";
            this.Text = "SfSalesShopMall Tools";
            this.Activated += new System.EventHandler(this.frmGetXML_Activated);
            this.Load += new System.EventHandler(this.frmGetXML_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem connectToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem addUserToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addStoreToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addGoodsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem regenerateXMLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem customerxmlToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem goodsxmlToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem shoeTypexmlToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem shoeCategoryxmlToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addModelMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem colorxmlToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem listuserxmlToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem shoeGroupxmlToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem salesTypexmlToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem updateDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem customerGroupcmlToolStripMenuItem;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ToolStripMenuItem queryDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadSalesDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem updateDataToServerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem shoeModelxmlToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem shoeMoldxmlToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem shoeSizeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem updatePriceListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem utilToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem sendDailyReportToolStripMenuItem;
    }
}

