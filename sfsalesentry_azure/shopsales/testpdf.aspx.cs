using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Text;
namespace shopsales
{
    public partial class testpdf : System.Web.UI.Page
    {
        string htmltable = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadTable();
        }
        protected string LoadTable()
        {
            htmltable = "<meta charset=\"UTF-8\">";
            htmltable += "<table>";
            Table tb = new Table();
            DataTable dt= ClsData.UserData();
            TableRow head = RowHeader(dt);
            tb.Rows.Add(head);
            Int32 id = 1;
            foreach (DataRow dr in dt.Rows)
            {
                htmltable += "<tr>";
                TableRow row = new TableRow();
                row.ID = "row" + id;
                for (int i=1;i<dt.Columns.Count;i++)
                {
                    htmltable += "<td>";
                    TableCell cell = new TableCell();
                    cell.ID = "row" + id + "col" + i;
                    TextBox txt = new TextBox();
                    txt.ID = "txt" + dt.Columns[i].ColumnName + "" + id;
                    txt.Text = dr[i].ToString();
                    htmltable += txt.Text;
                    cell.Controls.Add(txt);
                    row.Cells.Add(cell);
                    htmltable += "</td>";
                }
                htmltable += "</tr>";
                tb.Rows.Add(row);
                id++;
            }
            htmltable += "</table>";
            frm1.Controls.Add(tb);
            return htmltable;
        }
        protected TableRow RowHeader(DataTable dt)
        {
            TableRow tr = new TableRow();
            htmltable += "<tr>";
            tr.ID = "rowHead";
            for(int i=1;i<dt.Columns.Count;i++)
            {
                htmltable += "<td>";
                TableCell tc = new TableCell();
                tc.ID = "head" + i;
                Label lbl = new Label();
                lbl.ID="lbl" + dt.Columns[i].ColumnName;
                lbl.Text = dt.Columns[i].ColumnName;
                htmltable += lbl.Text;
                tc.Controls.Add(lbl);
                tr.Cells.Add(tc);
                htmltable += "</td>";
            }
            htmltable += "</tr>";
            return tr;
        }

        private Byte[] PdfSharpConvert(String html)
        {
            Byte[] res = null;

            using (MemoryStream ms = new MemoryStream())
            {
                var pdf = TheArtOfDev.HtmlRenderer.PdfSharp.PdfGenerator.GeneratePdf(html, PdfSharp.PageSize.A4);
                pdf.Save(ms);
                res = ms.ToArray();                
            }
            return res;
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            Byte[] bytes = PdfSharpConvert(htmltable);
            Response.ContentType = "application/pdf";
            Response.Charset = System.Text.Encoding.UTF8.EncodingName;
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("UTF-8");
            Response.AddHeader("content-disposition", "attachment;filename=test.pdf");
            Response.OutputStream.Write(bytes, 0, bytes.Length);
            Response.End();

        }
    }
}