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
        <asp:DropDownList ID="cboCounter" runat="server"></asp:DropDownList>
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        Sheet Name :
        <asp:TextBox ID="TextBox1" runat="server">Template</asp:TextBox>
        </div>
        <dx:ASPxUploadControl ID="ASPxUploadControl1" ClientInstanceName="uc" runat="server" OnFileUploadComplete="ASPxUploadControl1_FileUploadComplete" ShowProgressPanel="True" ShowUploadButton="True" Theme="PlasticBlue" UploadMode="Auto" UploadStorage="FileSystem" Width="280px" AutoStartUpload="True" OnFilesUploadComplete="ASPxUploadControl1_FilesUploadComplete" >
            <ClientSideEvents FileUploadComplete="function(s, e) {
                /*
                var separator = '|';
                if(e.callbackData.indexOf('redirect') != -1) {
                    var url = e.callbackData.split(separator)[1];
                    document.location.href = url;
                }
                */
                alert('Upload Complete! ' + e.callbackData);
                var txt=document.getElementById('TextBox2');
                txt.value=e.callbackData;
                var btn=document.getElementById('Button1');
                btn.disabled=false;
            }" />
            <AdvancedModeSettings EnableDragAndDrop="True">
            </AdvancedModeSettings>
        </dx:ASPxUploadControl>
        <asp:Timer ID="Timer1" runat="server" Interval ="1000" Enabled="false" OnTick="Timer1_Tick"></asp:Timer>
        <br />
        Data File Name :
        <asp:TextBox ID="TextBox2" runat="server" Enabled="false"></asp:TextBox>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
            <ContentTemplate>
                <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                    <ProgressTemplate>
                        Processing ... Please wait
                    </ProgressTemplate>
                </asp:UpdateProgress>
                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="View Data" />                
                <asp:Label ID="Label1" runat="server" Text="Ready"></asp:Label>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
            </Triggers>
        </asp:UpdatePanel>
        <br />
    </form>
</body>
</html>
