<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="testdx.aspx.cs" Inherits="shopsales.testdx" %>

<%@ Register Assembly="DevExpress.XtraReports.v15.2.Web, Version=15.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>

<%@ Register assembly="DevExpress.Web.v15.2, Version=15.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <dx:ASPxDocumentViewer ID="ASPxDocumentViewer1" runat="server">
        </dx:ASPxDocumentViewer>
    
    </div>
    </form>
</body>
</html>
