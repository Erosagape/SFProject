<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="deliverydt.aspx.cs" Inherits="sfdelivery.deliverydt" %>

<%@ Register Assembly="DevExpress.Web.v15.2, Version=15.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
<table style="width :100%;background-color:lightblue">
    <tr>
        <td>
                <asp:Image ID="Image1" runat="server" ImageUrl="~/logo.png" Height="66px" Width="222px" />
        </td>
        <td style="text-align:right">
                <asp:Label ID="lblUser" runat="server" Text="Welcome" Font-Bold="True"></asp:Label>    
        </td>
    </tr>
</table>
    <form id="form1" runat="server">
    <asp:HyperLink ID="HyperLink1" Font-Bold="true" ForeColor="ForestGreen" NavigateUrl="~/xmltools.aspx" runat="server">Menu</asp:HyperLink> &nbsp;>>
    <asp:HyperLink ID="HyperLink2" Font-Bold="true" ForeColor="ForestGreen" NavigateUrl="~/deliveryhd.aspx" runat="server">ข้อมูลใบขนส่งสินค้า (Delivery)</asp:HyperLink>
    &nbsp; >> <asp:Label ID="Label1" runat="server" Forecolor="ForestGreen" Text="รายการสินค้าในใบส่งสินค้า" Font-Bold="True"></asp:Label>     
        <hr />
    <div>
    <table>
        <tr>
            <td>
                <asp:Label ID="lblDocNo" runat="server" Text="เลขที่เอกสาร"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtID" runat="server" Enabled="False"></asp:TextBox>
            </td>
            <td>
                <asp:Button ID="btnRefresh" runat="server" Text="Refresh" OnClick="btnRefresh_Click" Height="26px" />
            </td>
        </tr>
    </table>
    </div>
        <dx:ASPxGridView ID="GridView1" runat="server" Theme="DevEx" OnDataBinding="GridView_DataBinding">
            <SettingsPager PageSize="20">
            </SettingsPager>
            <SettingsBehavior AllowFocusedRow="true" />
            <SettingsSearchPanel Visible="True" />
        </dx:ASPxGridView>
        <p>
            <asp:Label ID="lblFileName" runat="server" Text="Ready"></asp:Label>
        </p>
    </form>
</body>
</html>
