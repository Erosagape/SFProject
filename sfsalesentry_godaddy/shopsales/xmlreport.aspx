
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="xmlreport.aspx.cs" Inherits="shopsales.xmlreport" %>

<%@ Register Assembly="DevExpress.Web.v15.2, Version=15.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.ASPxPivotGrid.v15.2, Version=15.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxPivotGrid" TagPrefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Sales Report</title>
    </head>
<body>
    <form id="form1" runat="server">
    <div>
        <table style="width:100%">
            <tr>
                <td colspan="2">
                    <asp:Label ID="Label1" runat="server" Text="รายงานสรุปยอดขายรองเท้า Aerosoft" Font-Bold="true"></asp:Label>
                </td>       
                <td style="text-align:right">
                    <asp:LinkButton ID="btnPreview" runat="server" OnClick="btnPreview_Click">Print Data</asp:LinkButton>
                    &nbsp;&nbsp;
                    <asp:LinkButton ID="btnExport" runat="server" OnClick="btnExport_Click">Download Data</asp:LinkButton>
                </td>         
            </tr>
            <tr>
                <td colspan="2">
                    <b>เงื่อนไข : </b><asp:Label ID="labCliteria" runat="server" Text="ไม่ระบุ"></asp:Label>
                </td>
                <td style="text-align:right">
                    <asp:HyperLink ID="lblMessage" NavigateUrl="~/report.aspx" runat="server">Ready</asp:HyperLink>
                </td>
            </tr>
        </table>
        <dx:ASPxGridView ID="ASPxGridView1" runat="server" EnableTheming="True" Theme="Office2010Blue" OnDataBinding="ASPxGridView1_DataBinding" OnHtmlDataCellPrepared="ASPxGridView1_HtmlDataCellPrepared" >
            <Styles><Cell Wrap="false"/></Styles>
            <ClientSideEvents RowDblClick="function(s, e) {
    var rowkey=s.GetRowKey(e.visibleIndex);
    var chk='0123456789';
    var str=rowkey.substring(0,1);  
    if(chk.indexOf(str)>=0)
    {
       window.location='editsales.aspx?rowid=' + rowkey; 
    }
                } "/>
            <SettingsPager Mode="ShowAllRecords">
            </SettingsPager>
            <SettingsBehavior AllowFocusedRow="True" />
            <SettingsSearchPanel Visible="True" />
        </dx:ASPxGridView>
    </div>            
        <hr />    
        <asp:Panel ID="Panel1" runat="server">
        แชร์ไปยัง Line App กด Click -&gt; :
        <asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_Click"><img src="line.png" alt="ส่งรายงานไปยัง LINE"/></asp:LinkButton> OR 
        </asp:Panel>
        <asp:LinkButton ID="LinkButton3" runat="server" OnClick="LinkButton3_Click">Send E-Mail To -></asp:LinkButton>
        <asp:TextBox ID="txtEMail" runat="server"></asp:TextBox>
        <asp:Label ID="lblStatus" runat="server" Text="Ready"></asp:Label>
    </form>
</body>
</html>
