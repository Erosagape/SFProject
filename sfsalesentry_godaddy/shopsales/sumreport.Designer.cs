namespace shopsales
{
    partial class sumreport
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

        #region Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DevExpress.XtraReports.UI.XRSummary xrSummary1 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary2 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary3 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.DataAccess.Excel.FieldInfo fieldInfo1 = new DevExpress.DataAccess.Excel.FieldInfo();
            DevExpress.DataAccess.Excel.FieldInfo fieldInfo2 = new DevExpress.DataAccess.Excel.FieldInfo();
            DevExpress.DataAccess.Excel.FieldInfo fieldInfo3 = new DevExpress.DataAccess.Excel.FieldInfo();
            DevExpress.DataAccess.Excel.FieldInfo fieldInfo4 = new DevExpress.DataAccess.Excel.FieldInfo();
            DevExpress.DataAccess.Excel.FieldInfo fieldInfo5 = new DevExpress.DataAccess.Excel.FieldInfo();
            DevExpress.DataAccess.Excel.FieldInfo fieldInfo6 = new DevExpress.DataAccess.Excel.FieldInfo();
            DevExpress.DataAccess.Excel.FieldInfo fieldInfo7 = new DevExpress.DataAccess.Excel.FieldInfo();
            DevExpress.DataAccess.Excel.FieldInfo fieldInfo8 = new DevExpress.DataAccess.Excel.FieldInfo();
            DevExpress.DataAccess.Excel.FieldInfo fieldInfo9 = new DevExpress.DataAccess.Excel.FieldInfo();
            DevExpress.DataAccess.Excel.FieldInfo fieldInfo10 = new DevExpress.DataAccess.Excel.FieldInfo();
            DevExpress.DataAccess.Excel.FieldInfo fieldInfo11 = new DevExpress.DataAccess.Excel.FieldInfo();
            DevExpress.DataAccess.Excel.FieldInfo fieldInfo12 = new DevExpress.DataAccess.Excel.FieldInfo();
            DevExpress.DataAccess.Excel.FieldInfo fieldInfo13 = new DevExpress.DataAccess.Excel.FieldInfo();
            DevExpress.DataAccess.Excel.FieldInfo fieldInfo14 = new DevExpress.DataAccess.Excel.FieldInfo();
            DevExpress.DataAccess.Excel.FieldInfo fieldInfo15 = new DevExpress.DataAccess.Excel.FieldInfo();
            DevExpress.DataAccess.Excel.FieldInfo fieldInfo16 = new DevExpress.DataAccess.Excel.FieldInfo();
            DevExpress.DataAccess.Excel.FieldInfo fieldInfo17 = new DevExpress.DataAccess.Excel.FieldInfo();
            DevExpress.DataAccess.Excel.FieldInfo fieldInfo18 = new DevExpress.DataAccess.Excel.FieldInfo();
            DevExpress.DataAccess.Excel.ExcelSourceOptions excelSourceOptions1 = new DevExpress.DataAccess.Excel.ExcelSourceOptions();
            DevExpress.DataAccess.Excel.ExcelTableSettings excelTableSettings1 = new DevExpress.DataAccess.Excel.ExcelTableSettings();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrLabel11 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel10 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel9 = new DevExpress.XtraReports.UI.XRLabel();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.lblCaption = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.xrLine3 = new DevExpress.XtraReports.UI.XRLine();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.xrLine5 = new DevExpress.XtraReports.UI.XRLine();
            this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
            this.excelDataSource1 = new DevExpress.DataAccess.Excel.ExcelDataSource();
            this.xrControlStyle1 = new DevExpress.XtraReports.UI.XRControlStyle();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel8 = new DevExpress.XtraReports.UI.XRLabel();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel11,
            this.xrLabel10,
            this.xrLabel6,
            this.xrLabel3,
            this.xrLabel9});
            this.Detail.HeightF = 26.04168F;
            this.Detail.KeepTogether = true;
            this.Detail.KeepTogetherWithDetailReports = true;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.SortFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("Row ID", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrLabel11
            // 
            this.xrLabel11.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Sales In")});
            this.xrLabel11.Font = new System.Drawing.Font("Tahoma", 10F);
            this.xrLabel11.LocationFloat = new DevExpress.Utils.PointFloat(587.4167F, 0F);
            this.xrLabel11.Name = "xrLabel11";
            this.xrLabel11.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel11.SizeF = new System.Drawing.SizeF(114.5833F, 26.04168F);
            this.xrLabel11.StylePriority.UseFont = false;
            this.xrLabel11.StylePriority.UseTextAlignment = false;
            xrSummary1.FormatString = "{0:#,#.00}";
            this.xrLabel11.Summary = xrSummary1;
            this.xrLabel11.Text = "xrLabel11";
            this.xrLabel11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrLabel11.WordWrap = false;
            // 
            // xrLabel10
            // 
            this.xrLabel10.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ราคาขาย")});
            this.xrLabel10.Font = new System.Drawing.Font("Tahoma", 10F);
            this.xrLabel10.LocationFloat = new DevExpress.Utils.PointFloat(463.4584F, 0F);
            this.xrLabel10.Name = "xrLabel10";
            this.xrLabel10.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel10.SizeF = new System.Drawing.SizeF(123.9583F, 26.04168F);
            this.xrLabel10.StylePriority.UseFont = false;
            this.xrLabel10.StylePriority.UseTextAlignment = false;
            xrSummary2.FormatString = "{0:#,#.00}";
            this.xrLabel10.Summary = xrSummary2;
            this.xrLabel10.Text = "xrLabel10";
            this.xrLabel10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrLabel10.WordWrap = false;
            // 
            // xrLabel6
            // 
            this.xrLabel6.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "จำนวน")});
            this.xrLabel6.Font = new System.Drawing.Font("Tahoma", 10F);
            this.xrLabel6.LocationFloat = new DevExpress.Utils.PointFloat(261.3748F, 0F);
            this.xrLabel6.Name = "xrLabel6";
            this.xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel6.SizeF = new System.Drawing.SizeF(75.00015F, 26.04168F);
            this.xrLabel6.StylePriority.UseFont = false;
            this.xrLabel6.StylePriority.UseTextAlignment = false;
            this.xrLabel6.Text = "xrLabel6";
            this.xrLabel6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // xrLabel3
            // 
            this.xrLabel3.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "รุ่น")});
            this.xrLabel3.Font = new System.Drawing.Font("Tahoma", 10F);
            this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrLabel3.Name = "xrLabel3";
            this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel3.SizeF = new System.Drawing.SizeF(261.3748F, 26.04168F);
            this.xrLabel3.StylePriority.UseFont = false;
            this.xrLabel3.Text = "xrLabel3";
            // 
            // xrLabel9
            // 
            this.xrLabel9.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ราคาป้าย")});
            this.xrLabel9.Font = new System.Drawing.Font("Tahoma", 10F);
            this.xrLabel9.LocationFloat = new DevExpress.Utils.PointFloat(336.375F, 0F);
            this.xrLabel9.Name = "xrLabel9";
            this.xrLabel9.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel9.SizeF = new System.Drawing.SizeF(127.0833F, 26.04168F);
            this.xrLabel9.StylePriority.UseFont = false;
            this.xrLabel9.StylePriority.UseTextAlignment = false;
            xrSummary3.FormatString = "{0:#,#.00}";
            this.xrLabel9.Summary = xrSummary3;
            this.xrLabel9.Text = "xrLabel9";
            this.xrLabel9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrLabel9.WordWrap = false;
            // 
            // TopMargin
            // 
            this.TopMargin.HeightF = 20F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // BottomMargin
            // 
            this.BottomMargin.HeightF = 10F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // ReportHeader
            // 
            this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblCaption,
            this.xrLabel1});
            this.ReportHeader.HeightF = 55.20833F;
            this.ReportHeader.Name = "ReportHeader";
            // 
            // lblCaption
            // 
            this.lblCaption.LocationFloat = new DevExpress.Utils.PointFloat(0F, 28.20834F);
            this.lblCaption.Name = "lblCaption";
            this.lblCaption.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblCaption.SizeF = new System.Drawing.SizeF(701.9999F, 23F);
            this.lblCaption.Text = "lblCaption";
            // 
            // xrLabel1
            // 
            this.xrLabel1.Font = new System.Drawing.Font("Tahoma", 12F);
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrLabel1.Multiline = true;
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel1.SizeF = new System.Drawing.SizeF(364.5833F, 28.20834F);
            this.xrLabel1.StylePriority.UseFont = false;
            this.xrLabel1.Text = "รายงานสรุปยอดขายรองเท้า Aerosoft";
            // 
            // ReportFooter
            // 
            this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLine3});
            this.ReportFooter.HeightF = 33.33333F;
            this.ReportFooter.Name = "ReportFooter";
            // 
            // xrLine3
            // 
            this.xrLine3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrLine3.Name = "xrLine3";
            this.xrLine3.SizeF = new System.Drawing.SizeF(702F, 2.083332F);
            // 
            // PageHeader
            // 
            this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel8,
            this.xrLabel7,
            this.xrLabel5,
            this.xrLabel4,
            this.xrLabel2,
            this.xrLine5});
            this.PageHeader.HeightF = 27.08333F;
            this.PageHeader.Name = "PageHeader";
            // 
            // xrLine5
            // 
            this.xrLine5.LocationFloat = new DevExpress.Utils.PointFloat(0F, 22.99999F);
            this.xrLine5.Name = "xrLine5";
            this.xrLine5.SizeF = new System.Drawing.SizeF(702F, 2.083332F);
            // 
            // PageFooter
            // 
            this.PageFooter.HeightF = 30.20833F;
            this.PageFooter.Name = "PageFooter";
            // 
            // excelDataSource1
            // 
            this.excelDataSource1.FileName = "C:\\DATABASE\\web_salesentry\\report_sample.xlsx";
            this.excelDataSource1.Name = "excelDataSource1";
            fieldInfo1.Name = "วันที่";
            fieldInfo1.Type = typeof(string);
            fieldInfo2.Name = "รุ่น";
            fieldInfo2.Type = typeof(string);
            fieldInfo3.Name = "สี";
            fieldInfo3.Type = typeof(string);
            fieldInfo4.Name = "ขนาด";
            fieldInfo4.Type = typeof(double);
            fieldInfo5.Name = "จำนวน";
            fieldInfo5.Type = typeof(double);
            fieldInfo6.Name = "ราคาป้าย";
            fieldInfo6.Type = typeof(string);
            fieldInfo7.Name = "ส่วนลด";
            fieldInfo7.Type = typeof(double);
            fieldInfo8.Name = "ราคาขาย";
            fieldInfo8.Type = typeof(string);
            fieldInfo9.Name = "Sales In";
            fieldInfo9.Type = typeof(string);
            fieldInfo10.Name = "จุดขาย";
            fieldInfo10.Type = typeof(string);
            fieldInfo11.Name = "ประเภทขาย";
            fieldInfo11.Type = typeof(string);
            fieldInfo12.Name = "กลุ่มสินค้า";
            fieldInfo12.Type = typeof(string);
            fieldInfo13.Name = "ชนิด";
            fieldInfo13.Type = typeof(string);
            fieldInfo14.Name = "ผู้บันทึก";
            fieldInfo14.Type = typeof(string);
            fieldInfo15.Name = "หมายเหตุ";
            fieldInfo15.Type = typeof(string);
            fieldInfo16.Name = "บันทึก";
            fieldInfo16.Type = typeof(string);
            fieldInfo17.Name = "Row ID";
            fieldInfo17.Type = typeof(string);
            fieldInfo18.Name = "สัมประสิทธิ์";
            fieldInfo18.Type = typeof(double);
            this.excelDataSource1.Schema.AddRange(new DevExpress.DataAccess.Excel.FieldInfo[] {
            fieldInfo1,
            fieldInfo2,
            fieldInfo3,
            fieldInfo4,
            fieldInfo5,
            fieldInfo6,
            fieldInfo7,
            fieldInfo8,
            fieldInfo9,
            fieldInfo10,
            fieldInfo11,
            fieldInfo12,
            fieldInfo13,
            fieldInfo14,
            fieldInfo15,
            fieldInfo16,
            fieldInfo17,
            fieldInfo18});
            excelTableSettings1.TableName = "Table1";
            excelSourceOptions1.ImportSettings = excelTableSettings1;
            this.excelDataSource1.SourceOptions = excelSourceOptions1;
            // 
            // xrControlStyle1
            // 
            this.xrControlStyle1.Name = "xrControlStyle1";
            this.xrControlStyle1.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            // 
            // xrLabel2
            // 
            this.xrLabel2.Font = new System.Drawing.Font("Tahoma", 11F);
            this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.xrLabel2.SizeF = new System.Drawing.SizeF(261.3748F, 23F);
            this.xrLabel2.StylePriority.UseFont = false;
            this.xrLabel2.Text = "รวมตาม";
            // 
            // xrLabel4
            // 
            this.xrLabel4.Font = new System.Drawing.Font("Tahoma", 11F);
            this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(261.3748F, 0F);
            this.xrLabel4.Name = "xrLabel4";
            this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.xrLabel4.SizeF = new System.Drawing.SizeF(75.00021F, 23F);
            this.xrLabel4.StylePriority.UseFont = false;
            this.xrLabel4.StylePriority.UseTextAlignment = false;
            this.xrLabel4.Text = "จำนวนคู่";
            this.xrLabel4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrLabel5
            // 
            this.xrLabel5.Font = new System.Drawing.Font("Tahoma", 11F);
            this.xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(336.375F, 0F);
            this.xrLabel5.Name = "xrLabel5";
            this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel5.SizeF = new System.Drawing.SizeF(127.0833F, 23F);
            this.xrLabel5.StylePriority.UseFont = false;
            this.xrLabel5.StylePriority.UseTextAlignment = false;
            this.xrLabel5.Text = "ราคาป้าย";
            this.xrLabel5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrLabel7
            // 
            this.xrLabel7.Font = new System.Drawing.Font("Tahoma", 11F);
            this.xrLabel7.LocationFloat = new DevExpress.Utils.PointFloat(463.4584F, 0F);
            this.xrLabel7.Name = "xrLabel7";
            this.xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel7.SizeF = new System.Drawing.SizeF(123.9582F, 23F);
            this.xrLabel7.StylePriority.UseFont = false;
            this.xrLabel7.StylePriority.UseTextAlignment = false;
            this.xrLabel7.Text = "ราคาขาย";
            this.xrLabel7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrLabel8
            // 
            this.xrLabel8.Font = new System.Drawing.Font("Tahoma", 11F);
            this.xrLabel8.LocationFloat = new DevExpress.Utils.PointFloat(587.4167F, 0F);
            this.xrLabel8.Multiline = true;
            this.xrLabel8.Name = "xrLabel8";
            this.xrLabel8.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel8.SizeF = new System.Drawing.SizeF(114.5831F, 23F);
            this.xrLabel8.StylePriority.UseFont = false;
            this.xrLabel8.StylePriority.UseTextAlignment = false;
            this.xrLabel8.Text = "Sales In";
            this.xrLabel8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // sumreport
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.ReportHeader,
            this.ReportFooter,
            this.PageHeader,
            this.PageFooter});
            this.ComponentStorage.AddRange(new System.ComponentModel.IComponent[] {
            this.excelDataSource1});
            this.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margins = new System.Drawing.Printing.Margins(26, 99, 20, 10);
            this.PageHeight = 1169;
            this.PageWidth = 827;
            this.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.StyleSheet.AddRange(new DevExpress.XtraReports.UI.XRControlStyle[] {
            this.xrControlStyle1});
            this.Version = "15.2";
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.ReportHeaderBand ReportHeader;
        private DevExpress.XtraReports.UI.XRLabel xrLabel1;
        private DevExpress.XtraReports.UI.ReportFooterBand ReportFooter;
        private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;
        private DevExpress.XtraReports.UI.PageFooterBand PageFooter;
        private DevExpress.XtraReports.UI.XRLabel xrLabel11;
        private DevExpress.XtraReports.UI.XRLabel xrLabel10;
        private DevExpress.XtraReports.UI.XRLabel xrLabel6;
        private DevExpress.XtraReports.UI.XRLabel xrLabel3;
        private DevExpress.XtraReports.UI.XRLabel xrLabel9;
        private DevExpress.DataAccess.Excel.ExcelDataSource excelDataSource1;
        private DevExpress.XtraReports.UI.XRLine xrLine3;
        private DevExpress.XtraReports.UI.XRLine xrLine5;
        private DevExpress.XtraReports.UI.XRControlStyle xrControlStyle1;
        private DevExpress.XtraReports.UI.XRLabel lblCaption;
        private DevExpress.XtraReports.UI.XRLabel xrLabel8;
        private DevExpress.XtraReports.UI.XRLabel xrLabel7;
        private DevExpress.XtraReports.UI.XRLabel xrLabel5;
        private DevExpress.XtraReports.UI.XRLabel xrLabel4;
        private DevExpress.XtraReports.UI.XRLabel xrLabel2;
    }
}
