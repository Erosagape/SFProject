using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace sfdelivery
{
    public partial class uploadData : System.Web.UI.Page
    {
        private ClsSessionUser cApp = new ClsSessionUser();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["cApp"] != null)
            {
                cApp = (ClsSessionUser)Session["cApp"];
            }
            if (cApp.user_name == "")
            {
                Response.Redirect("Default.aspx", true);
            }
            else
            {
                lblMessage.Text = cApp.user_name;
            }
            cApp.session_id = GetSession();
            cApp.user_name = lblMessage.Text;            
            Session["cApp"] = cApp;
        }
        private string GetSession()
        {
            if (Session["SessionID"] == null)
            {
                Session["SessionID"] = HttpContext.Current.Session.SessionID;
            }
            return Session["SessionID"].ToString();
        }
        protected string ReadData(string fname)
        {
            StreamReader rd = new StreamReader(fname, System.Text.UTF8Encoding.UTF8);
            string JsonString = rd.ReadToEnd();
            rd.Close();
            return JsonString;
        }
        protected bool Showdata(string fname)
        {
            bool success = false;
            try
            {
                GridView1.DataSource = ClsData.GetDataTableFromJSON(ReadData(fname));
                GridView1.DataBind();
                success = true;
            }
            catch(Exception ex)
            {
                StatusLabel.Text = ex.Message;
            }
            return success;
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            if(FileUpload1.HasFile==true)
            {
                try
                {
                    string filenameClient = Path.GetFileName(FileUpload1.FileName);
                    string FilenameServer = Server.MapPath("~/") + filenameClient;
                    FileUpload1.SaveAs(FilenameServer);
                    StatusLabel.Text = "Upload status: File uploaded!";
                    if(System.IO.File.Exists(FilenameServer)==true)
                    {
                        if(Showdata(FilenameServer)== true)
                        {
                            StatusLabel.Text = "Load Complete! =" +GetTableName(FilenameServer);
                            fileServer.Value = FilenameServer;
                        }
                    }
                }
                catch (Exception ex)
                {
                    StatusLabel.Text = "Upload status: The file could not be uploaded. The following error occured: " + ex.Message;
                }
            }
            Session["cApp"] = cApp;
        }
        protected string GetTableName(string filename)
        {
            var finfo = new System.IO.FileInfo(filename);
            string fname = finfo.Name;
            fname = fname.Replace(".json", "") + "_";
            var split = fname.Split('_');
            return split[0].ToString();

        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            if(fileServer.Value !=null)
            {
                string filename = fileServer.Value.ToString();
                string Jsonstring = ReadData(filename);
                string fname = GetTableName(filename);
                IService sv = new IService();
                string msg=sv.ProcessDataJSON(fname, Jsonstring);
                StatusLabel.Text = msg;
                if(msg.Substring(0,1)=="C")
                {
                    File.Delete(filename);
                }
            }
            Session["cApp"] = cApp;
        }
    }
}