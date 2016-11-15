using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Xml;
using System.Data;
using System.IO;


namespace sfdelivery
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IITest" in both code and config file together.
    [ServiceContract]
    public interface IDataExchange
    {
        [OperationContract]
        string Login(string user, string password);
        [OperationContract]
        string ShowData(string data);
        [OperationContract]
        string GetDataXML(string dataname,bool createnew);
        [OperationContract]
        string QueryDataXML(string dataname, string cliteria);
        [OperationContract]
        string QueryDataJSON(string dataname, string cliteria);
        [OperationContract]
        string RemoveDataXML(string dataname, string cliteria);
        [OperationContract]
        string ProcessDataXML(string dataname, string xmlstr);
        [OperationContract]
        string GetXMLFileList(string filterstr = "*");
        [OperationContract]
        string GetDataJSON(string dataname);
        [OperationContract]
        string ProcessDataJSON(string dataname, string xmlstr);
        [OperationContract]
        string GetDeliveryDataXML(string dataname, string cliteria);
        [OperationContract]
        string GetCustomerListXML(string username,string dataname);
        [OperationContract]
        string GetDeliveryInfo(string docno, string doctype);
        [OperationContract]
        string UpdateDeliveryInfo(string docno, string cmdset);
        [OperationContract]
        string WriteDataUpdate(string datafile, string jsondata);
        [OperationContract]
        string UpdateData(string datafile);
        [OperationContract]
        string ClearDataJSON(string datafile);
        [OperationContract]
        string OpenDataForupdate(string datafile);
        [OperationContract]
        string CloseDataForupdate(string datafile);
        [OperationContract]
        string ReadDataForupdate(string datafile);
        [OperationContract]
        string GetDelivery(string datafile);
    }
}
