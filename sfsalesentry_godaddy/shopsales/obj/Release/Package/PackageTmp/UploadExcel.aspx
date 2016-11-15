<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UploadExcel.aspx.cs" Inherits="shopsales.UploadExcel" %>

<%@ Register assembly="DevExpress.Web.v15.2, Version=15.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        </div>
        <dx:ASPxUploadControl ID="ASPxUploadControl1" runat="server" OnFileUploadComplete="ASPxUploadControl1_FileUploadComplete" ShowProgressPanel="True" ShowUploadButton="True" Theme="PlasticBlue" UploadMode="Auto" UploadStorage="FileSystem" Width="280px" AutoStartUpload="True">
            <AdvancedModeSettings EnableDragAndDrop="True">
            </AdvancedModeSettings>
        </dx:ASPxUploadControl>
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="View Data" />
        <br />
    </form>
</body>
</html>
