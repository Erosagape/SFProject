﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SfWebBackoffice.DeliveryAzure {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="DeliveryAzure.IDataExchange")]
    public interface IDataExchange {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataExchange/Login", ReplyAction="http://tempuri.org/IDataExchange/LoginResponse")]
        string Login(string user, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataExchange/Login", ReplyAction="http://tempuri.org/IDataExchange/LoginResponse")]
        System.Threading.Tasks.Task<string> LoginAsync(string user, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataExchange/ShowData", ReplyAction="http://tempuri.org/IDataExchange/ShowDataResponse")]
        string ShowData(string data);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataExchange/ShowData", ReplyAction="http://tempuri.org/IDataExchange/ShowDataResponse")]
        System.Threading.Tasks.Task<string> ShowDataAsync(string data);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataExchange/GetDataXML", ReplyAction="http://tempuri.org/IDataExchange/GetDataXMLResponse")]
        string GetDataXML(string dataname, bool createnew);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataExchange/GetDataXML", ReplyAction="http://tempuri.org/IDataExchange/GetDataXMLResponse")]
        System.Threading.Tasks.Task<string> GetDataXMLAsync(string dataname, bool createnew);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataExchange/QueryDataXML", ReplyAction="http://tempuri.org/IDataExchange/QueryDataXMLResponse")]
        string QueryDataXML(string dataname, string cliteria);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataExchange/QueryDataXML", ReplyAction="http://tempuri.org/IDataExchange/QueryDataXMLResponse")]
        System.Threading.Tasks.Task<string> QueryDataXMLAsync(string dataname, string cliteria);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataExchange/QueryDataJSON", ReplyAction="http://tempuri.org/IDataExchange/QueryDataJSONResponse")]
        string QueryDataJSON(string dataname, string cliteria);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataExchange/QueryDataJSON", ReplyAction="http://tempuri.org/IDataExchange/QueryDataJSONResponse")]
        System.Threading.Tasks.Task<string> QueryDataJSONAsync(string dataname, string cliteria);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataExchange/RemoveDataXML", ReplyAction="http://tempuri.org/IDataExchange/RemoveDataXMLResponse")]
        string RemoveDataXML(string dataname, string cliteria);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataExchange/RemoveDataXML", ReplyAction="http://tempuri.org/IDataExchange/RemoveDataXMLResponse")]
        System.Threading.Tasks.Task<string> RemoveDataXMLAsync(string dataname, string cliteria);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataExchange/ProcessDataXML", ReplyAction="http://tempuri.org/IDataExchange/ProcessDataXMLResponse")]
        string ProcessDataXML(string dataname, string xmlstr);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataExchange/ProcessDataXML", ReplyAction="http://tempuri.org/IDataExchange/ProcessDataXMLResponse")]
        System.Threading.Tasks.Task<string> ProcessDataXMLAsync(string dataname, string xmlstr);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataExchange/GetXMLFileList", ReplyAction="http://tempuri.org/IDataExchange/GetXMLFileListResponse")]
        string GetXMLFileList(string filterstr);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataExchange/GetXMLFileList", ReplyAction="http://tempuri.org/IDataExchange/GetXMLFileListResponse")]
        System.Threading.Tasks.Task<string> GetXMLFileListAsync(string filterstr);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataExchange/GetDataJSON", ReplyAction="http://tempuri.org/IDataExchange/GetDataJSONResponse")]
        string GetDataJSON(string dataname);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataExchange/GetDataJSON", ReplyAction="http://tempuri.org/IDataExchange/GetDataJSONResponse")]
        System.Threading.Tasks.Task<string> GetDataJSONAsync(string dataname);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataExchange/ProcessDataJSON", ReplyAction="http://tempuri.org/IDataExchange/ProcessDataJSONResponse")]
        string ProcessDataJSON(string dataname, string xmlstr);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataExchange/ProcessDataJSON", ReplyAction="http://tempuri.org/IDataExchange/ProcessDataJSONResponse")]
        System.Threading.Tasks.Task<string> ProcessDataJSONAsync(string dataname, string xmlstr);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataExchange/GetDeliveryDataXML", ReplyAction="http://tempuri.org/IDataExchange/GetDeliveryDataXMLResponse")]
        string GetDeliveryDataXML(string dataname, string cliteria);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataExchange/GetDeliveryDataXML", ReplyAction="http://tempuri.org/IDataExchange/GetDeliveryDataXMLResponse")]
        System.Threading.Tasks.Task<string> GetDeliveryDataXMLAsync(string dataname, string cliteria);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataExchange/GetCustomerListXML", ReplyAction="http://tempuri.org/IDataExchange/GetCustomerListXMLResponse")]
        string GetCustomerListXML(string username, string dataname);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataExchange/GetCustomerListXML", ReplyAction="http://tempuri.org/IDataExchange/GetCustomerListXMLResponse")]
        System.Threading.Tasks.Task<string> GetCustomerListXMLAsync(string username, string dataname);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataExchange/GetDeliveryInfo", ReplyAction="http://tempuri.org/IDataExchange/GetDeliveryInfoResponse")]
        string GetDeliveryInfo(string docno, string doctype);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataExchange/GetDeliveryInfo", ReplyAction="http://tempuri.org/IDataExchange/GetDeliveryInfoResponse")]
        System.Threading.Tasks.Task<string> GetDeliveryInfoAsync(string docno, string doctype);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataExchange/UpdateDeliveryInfo", ReplyAction="http://tempuri.org/IDataExchange/UpdateDeliveryInfoResponse")]
        string UpdateDeliveryInfo(string docno, string cmdset);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataExchange/UpdateDeliveryInfo", ReplyAction="http://tempuri.org/IDataExchange/UpdateDeliveryInfoResponse")]
        System.Threading.Tasks.Task<string> UpdateDeliveryInfoAsync(string docno, string cmdset);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataExchange/WriteDataUpdate", ReplyAction="http://tempuri.org/IDataExchange/WriteDataUpdateResponse")]
        string WriteDataUpdate(string datafile, string jsondata);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataExchange/WriteDataUpdate", ReplyAction="http://tempuri.org/IDataExchange/WriteDataUpdateResponse")]
        System.Threading.Tasks.Task<string> WriteDataUpdateAsync(string datafile, string jsondata);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataExchange/UpdateData", ReplyAction="http://tempuri.org/IDataExchange/UpdateDataResponse")]
        string UpdateData(string datafile);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataExchange/UpdateData", ReplyAction="http://tempuri.org/IDataExchange/UpdateDataResponse")]
        System.Threading.Tasks.Task<string> UpdateDataAsync(string datafile);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataExchange/ClearDataJSON", ReplyAction="http://tempuri.org/IDataExchange/ClearDataJSONResponse")]
        string ClearDataJSON(string datafile);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataExchange/ClearDataJSON", ReplyAction="http://tempuri.org/IDataExchange/ClearDataJSONResponse")]
        System.Threading.Tasks.Task<string> ClearDataJSONAsync(string datafile);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataExchange/OpenDataForupdate", ReplyAction="http://tempuri.org/IDataExchange/OpenDataForupdateResponse")]
        string OpenDataForupdate(string datafile);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataExchange/OpenDataForupdate", ReplyAction="http://tempuri.org/IDataExchange/OpenDataForupdateResponse")]
        System.Threading.Tasks.Task<string> OpenDataForupdateAsync(string datafile);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataExchange/CloseDataForupdate", ReplyAction="http://tempuri.org/IDataExchange/CloseDataForupdateResponse")]
        string CloseDataForupdate(string datafile);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataExchange/CloseDataForupdate", ReplyAction="http://tempuri.org/IDataExchange/CloseDataForupdateResponse")]
        System.Threading.Tasks.Task<string> CloseDataForupdateAsync(string datafile);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataExchange/ReadDataForupdate", ReplyAction="http://tempuri.org/IDataExchange/ReadDataForupdateResponse")]
        string ReadDataForupdate(string datafile);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataExchange/ReadDataForupdate", ReplyAction="http://tempuri.org/IDataExchange/ReadDataForupdateResponse")]
        System.Threading.Tasks.Task<string> ReadDataForupdateAsync(string datafile);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataExchange/GetDelivery", ReplyAction="http://tempuri.org/IDataExchange/GetDeliveryResponse")]
        string GetDelivery(string datafile);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataExchange/GetDelivery", ReplyAction="http://tempuri.org/IDataExchange/GetDeliveryResponse")]
        System.Threading.Tasks.Task<string> GetDeliveryAsync(string datafile);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IDataExchangeChannel : SfWebBackoffice.DeliveryAzure.IDataExchange, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class DataExchangeClient : System.ServiceModel.ClientBase<SfWebBackoffice.DeliveryAzure.IDataExchange>, SfWebBackoffice.DeliveryAzure.IDataExchange {
        
        public DataExchangeClient() {
        }
        
        public DataExchangeClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public DataExchangeClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public DataExchangeClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public DataExchangeClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string Login(string user, string password) {
            return base.Channel.Login(user, password);
        }
        
        public System.Threading.Tasks.Task<string> LoginAsync(string user, string password) {
            return base.Channel.LoginAsync(user, password);
        }
        
        public string ShowData(string data) {
            return base.Channel.ShowData(data);
        }
        
        public System.Threading.Tasks.Task<string> ShowDataAsync(string data) {
            return base.Channel.ShowDataAsync(data);
        }
        
        public string GetDataXML(string dataname, bool createnew) {
            return base.Channel.GetDataXML(dataname, createnew);
        }
        
        public System.Threading.Tasks.Task<string> GetDataXMLAsync(string dataname, bool createnew) {
            return base.Channel.GetDataXMLAsync(dataname, createnew);
        }
        
        public string QueryDataXML(string dataname, string cliteria) {
            return base.Channel.QueryDataXML(dataname, cliteria);
        }
        
        public System.Threading.Tasks.Task<string> QueryDataXMLAsync(string dataname, string cliteria) {
            return base.Channel.QueryDataXMLAsync(dataname, cliteria);
        }
        
        public string QueryDataJSON(string dataname, string cliteria) {
            return base.Channel.QueryDataJSON(dataname, cliteria);
        }
        
        public System.Threading.Tasks.Task<string> QueryDataJSONAsync(string dataname, string cliteria) {
            return base.Channel.QueryDataJSONAsync(dataname, cliteria);
        }
        
        public string RemoveDataXML(string dataname, string cliteria) {
            return base.Channel.RemoveDataXML(dataname, cliteria);
        }
        
        public System.Threading.Tasks.Task<string> RemoveDataXMLAsync(string dataname, string cliteria) {
            return base.Channel.RemoveDataXMLAsync(dataname, cliteria);
        }
        
        public string ProcessDataXML(string dataname, string xmlstr) {
            return base.Channel.ProcessDataXML(dataname, xmlstr);
        }
        
        public System.Threading.Tasks.Task<string> ProcessDataXMLAsync(string dataname, string xmlstr) {
            return base.Channel.ProcessDataXMLAsync(dataname, xmlstr);
        }
        
        public string GetXMLFileList(string filterstr) {
            return base.Channel.GetXMLFileList(filterstr);
        }
        
        public System.Threading.Tasks.Task<string> GetXMLFileListAsync(string filterstr) {
            return base.Channel.GetXMLFileListAsync(filterstr);
        }
        
        public string GetDataJSON(string dataname) {
            return base.Channel.GetDataJSON(dataname);
        }
        
        public System.Threading.Tasks.Task<string> GetDataJSONAsync(string dataname) {
            return base.Channel.GetDataJSONAsync(dataname);
        }
        
        public string ProcessDataJSON(string dataname, string xmlstr) {
            return base.Channel.ProcessDataJSON(dataname, xmlstr);
        }
        
        public System.Threading.Tasks.Task<string> ProcessDataJSONAsync(string dataname, string xmlstr) {
            return base.Channel.ProcessDataJSONAsync(dataname, xmlstr);
        }
        
        public string GetDeliveryDataXML(string dataname, string cliteria) {
            return base.Channel.GetDeliveryDataXML(dataname, cliteria);
        }
        
        public System.Threading.Tasks.Task<string> GetDeliveryDataXMLAsync(string dataname, string cliteria) {
            return base.Channel.GetDeliveryDataXMLAsync(dataname, cliteria);
        }
        
        public string GetCustomerListXML(string username, string dataname) {
            return base.Channel.GetCustomerListXML(username, dataname);
        }
        
        public System.Threading.Tasks.Task<string> GetCustomerListXMLAsync(string username, string dataname) {
            return base.Channel.GetCustomerListXMLAsync(username, dataname);
        }
        
        public string GetDeliveryInfo(string docno, string doctype) {
            return base.Channel.GetDeliveryInfo(docno, doctype);
        }
        
        public System.Threading.Tasks.Task<string> GetDeliveryInfoAsync(string docno, string doctype) {
            return base.Channel.GetDeliveryInfoAsync(docno, doctype);
        }
        
        public string UpdateDeliveryInfo(string docno, string cmdset) {
            return base.Channel.UpdateDeliveryInfo(docno, cmdset);
        }
        
        public System.Threading.Tasks.Task<string> UpdateDeliveryInfoAsync(string docno, string cmdset) {
            return base.Channel.UpdateDeliveryInfoAsync(docno, cmdset);
        }
        
        public string WriteDataUpdate(string datafile, string jsondata) {
            return base.Channel.WriteDataUpdate(datafile, jsondata);
        }
        
        public System.Threading.Tasks.Task<string> WriteDataUpdateAsync(string datafile, string jsondata) {
            return base.Channel.WriteDataUpdateAsync(datafile, jsondata);
        }
        
        public string UpdateData(string datafile) {
            return base.Channel.UpdateData(datafile);
        }
        
        public System.Threading.Tasks.Task<string> UpdateDataAsync(string datafile) {
            return base.Channel.UpdateDataAsync(datafile);
        }
        
        public string ClearDataJSON(string datafile) {
            return base.Channel.ClearDataJSON(datafile);
        }
        
        public System.Threading.Tasks.Task<string> ClearDataJSONAsync(string datafile) {
            return base.Channel.ClearDataJSONAsync(datafile);
        }
        
        public string OpenDataForupdate(string datafile) {
            return base.Channel.OpenDataForupdate(datafile);
        }
        
        public System.Threading.Tasks.Task<string> OpenDataForupdateAsync(string datafile) {
            return base.Channel.OpenDataForupdateAsync(datafile);
        }
        
        public string CloseDataForupdate(string datafile) {
            return base.Channel.CloseDataForupdate(datafile);
        }
        
        public System.Threading.Tasks.Task<string> CloseDataForupdateAsync(string datafile) {
            return base.Channel.CloseDataForupdateAsync(datafile);
        }
        
        public string ReadDataForupdate(string datafile) {
            return base.Channel.ReadDataForupdate(datafile);
        }
        
        public System.Threading.Tasks.Task<string> ReadDataForupdateAsync(string datafile) {
            return base.Channel.ReadDataForupdateAsync(datafile);
        }
        
        public string GetDelivery(string datafile) {
            return base.Channel.GetDelivery(datafile);
        }
        
        public System.Threading.Tasks.Task<string> GetDeliveryAsync(string datafile) {
            return base.Channel.GetDeliveryAsync(datafile);
        }
    }
}
