using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace sfdelivery
{
    public class ClsSessionUser
    {
        private string userid = "";
        private string empid = "";
        private string username = "";
        private string userrole = "";
        private string userpass = "";
        private string workdate = "";
        private string groupid = "";
        private string groupname = "";
        private string sessionid = "";
        private bool fromMobile = false;
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
        public string emp_id
        {
            get
            {
                return empid;
            }
            set
            {
                empid = value;
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
        public string group_id
        {
            get
            {
                return groupid;
            }
            set
            {
                groupid = value;
            }
        }
        public string group_name
        {
            get
            {
                return groupname;
            }
            set
            {
                groupname = value;
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
        public bool from_mobile
        {
            get
            {
                return fromMobile;
            }
            set
            {
                fromMobile = value;
            }
        }
        public bool SaveLogin()
        {
            IService sv = new IService();
            string msg =sv.ProcessDataXML("LoginHistory", GetUserLoginData());
            return (msg.Substring(0, 1) == "C");
        }

        private string GetUserLoginData()
        {
            string data = "";
            data = @"<?xml version=""1.0"" standalone=""yes""?>";
            data += @"<LoginHistory>";
            data += @"<LoginDelivery>";
            data += @"<UserName>" + username + "</UserName>";
            data += @"<DateTimeLogIn>" + ClsData.CurrentDateTH().ToString("yyyy-MM-dd HH:mm:ss") + "</DateTimeLogIn>";
            data += @"<FromMobile>" + fromMobile + "</FromMobile>";
            data += @"</LoginDelivery>";
            data += @"</LoginHistory>";
            return data;
        }
    }
}