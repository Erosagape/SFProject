using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace shopsales
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IDataExchange
    {
        [OperationContract]
        string ShowData(string data);
        [OperationContract]
        string GetDataXML(string dataname);
        [OperationContract]
        string QueryDataXML(string dataname, string cliteria);
        [OperationContract]
        string RemoveDataXML(string dataname, string cliteria);
        [OperationContract]
        string ProcessDataXML(string dataname, string xmlstr);
        [OperationContract]
        string GetXMLFileList(string filterstr="*");
        [OperationContract]
        string GetDataJSON(string dataname);
        [OperationContract]
        string ProcessDataJSON(string dataname, string xmlstr);
        [OperationContract]
        string Login(string user, string password);
        [OperationContract]
        string GetDailyReport(int index,string uid, string ondate);
        [OperationContract]
        string SendDailyReport(string ondate,string data);
    }
}
