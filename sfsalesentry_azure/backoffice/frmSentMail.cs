using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace shopsales_tools
{
    public partial class frmSentMail : Form
    {
        public frmSentMail()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            try
            {
                using (IWebService.DataExchangeClient sv = new IWebService.DataExchangeClient())
                {
                    string msg= Environment.NewLine + sv.GetDailyReport(2,"DailySales-", DateTime.Now.ToString("yyyy-MM-dd"));
                    textBox3.Text = msg;
                    msg = Environment.NewLine+ sv.GetDailyReport(3, "DailyVolume-", DateTime.Now.ToString("yyyy-MM-dd"));
                    textBox4.Text = msg;
                }                    
            }
            catch (Exception ex)
            {
                textBox2.Text = ex.Message;
            }
            Cursor = Cursors.Default;
        }
        private string GetEMailBody()
        {
            string data = textBox1.Text;
            data += Environment.NewLine;
            data += Environment.NewLine + label1.Text;
            data += Environment.NewLine + "<a href='" + textBox6.Text+ textBox3.Text + "'>Click</a>";
            data += Environment.NewLine + label2.Text;
            data += Environment.NewLine + "<a href='" + textBox6.Text+ textBox4.Text + "'>Click</a>";
            data += Environment.NewLine;
            data += Environment.NewLine + textBox5.Text;
            return data;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            string msg = "";
            Cursor = Cursors.WaitCursor;
            try
            {
                using (IWebService.DataExchangeClient sv = new IWebService.DataExchangeClient())
                {
                    msg = sv.SendDailyReport(DateTime.Now.ToString("yyyy-MM-dd"), GetEMailBody());
                }                    
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            Cursor = Cursors.Default;
            textBox2.Text = msg;
        }
    }
}
