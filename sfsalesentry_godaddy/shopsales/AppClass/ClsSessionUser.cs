using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace shopsales
{
    public class ClsSessionUser
    {
        private string userid="";
        private string username="";
        private string userrole = "";
        private string userpass = "";
        private string workdate="";
        private string shopid = "";
        private string shopname = "";
        private string shopbranch = "";
        private string shopgroup = "";
        private string shopnote = "";
        private string sessionid = "";
        private string sharediscount = "";
        private string gpx = "";
        private string salescode = "";
        private string zonecode = "";
        private string area = "";
        private string supcode = "";
        #region default shared properties          
        public string user_id
        {
            get
            {
                return userid;
            }
            set
            {
                userid = value;
            }
        }
        public string user_name
        {
            get
            {
                return username;
            }
            set
            {
                username = value;
            }
        }
        public string user_password
        {
            get
            {
                return userpass;
            }
            set
            {
                userpass = value;
            }
        }
        public string user_role
        {
            get
            {
                return userrole;
            }
            set
            {
                userrole = value;
            }
        }
        public string working_date
        {
            get
            {
                return workdate;
            }
            set
            {
                workdate = value;
            }
        }
        public string shop_id
        {
            get
            {
                return shopid;
            }
            set
            {
                shopid = value;
            }
        }
        public string shop_name
        {
            get
            {
                return shopname;
            }
            set
            {
                shopname = value;
            }
        }
        public string shop_branch
        {
            get
            {
                return shopbranch;
            }
            set
            {
                shopbranch = value;
            }
        }
        public string shop_note
        {
            get
            {
                return shopnote;
            }
            set
            {
                shopnote = value;
            }
        }
        public string shop_group
        {
            get
            {
                return shopgroup;
            }
            set
            {
                shopgroup = value;
            }
        }
        public string session_id
        {
            get
            {
                return sessionid;
            }
            set
            {
                sessionid = value;
            }
        }
        public string share_discount
        {
            get
            {
                return sharediscount;
            }
            set
            {
                sharediscount = value;
            }
        }
        public string gpx_rate
        {
            get
            {
                return gpx;
            }
            set
            {
                gpx = value;
            }
        }
        public string shop_supcode { get { return supcode; } set { supcode = value; } }
        public string shop_salescode { get { return salescode; } set { salescode = value; } }
        public string shop_zonecode { get { return zonecode; } set { zonecode = value; } }
        public string shop_areacode { get { return area; } set { area = value; } }

        #endregion
        #region this project function
        public string GetXMLFileName(string datein="")
        {
            if (datein == "") datein = workdate;
            string strDate = datein.Substring(0, 7).Replace("-", "");
            string fname = shopid + "_" + strDate + "_" + userid + ".xml";
            return fname.ToUpper();
        }
        public bool SaveLogin(bool fromMobile)
        {
            IService sv = new IService();
            string msg = sv.ProcessDataXML("LoginHistory", GetUserLoginData(fromMobile));
            return (msg.Substring(0, 1) == "C");
        }
        private string GetUserLoginData(bool isFromMobile)
        {
            string data = "";
            data = @"<?xml version=""1.0"" standalone=""yes""?>";
            data += @"<LoginHistory>";
            data += @"<LoginSalesEntry>";
            data += @"<UserName>" + username + "</UserName>";
            data += @"<DateTimeLogIn>" + DateTime.Now.AddHours(7).ToString("yyyy-MM-dd HH:mm:ss") + "</DateTimeLogIn>";
            data += @"<FromMobile>" + isFromMobile + "</FromMobile>";
            data += @"</LoginSalesEntry>";
            data += @"</LoginHistory>";
            return data;
        }
        #endregion
        #region utility function
        public string iif(bool cliteria, string iftrue, string iffalse)
        {
            if (cliteria)
            {
                return iftrue;
            }
            else
            {
                return iffalse;
            }
        }
        public string CurrentDate()
        {
            DateTime date = DateTime.Today;
            string dd = date.Day.ToString("00");
            string mm = date.Month.ToString("00");
            string yy = date.Year.ToString("0000");
            return yy + "-" + mm + "-" + dd;
        }
        public string RPNull(object val,object str)
        {
            if(val.Equals(null)==true||val.Equals(""))
            {
                return str.ToString();
            }
            else
            {
                return val.ToString();
            }
        }
        public double CPercent(string val, bool divide = true)
        {
            try
            {
                if (divide == true)
                {
                    return Convert.ToDouble(val) / 100;
                }
                else
                {
                    return Convert.ToDouble(val) * 100;
                }
            }
            catch
            {
                return 0;
            }
        }
        #endregion
    }
}