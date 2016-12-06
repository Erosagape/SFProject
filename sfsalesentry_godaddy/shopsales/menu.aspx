<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="menu.aspx.cs" Inherits="shopsales.menu" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>aerosoft shop-sales entry</title>
    <meta name="viewport" content="width=device-width, initial-scale=1">

    <link rel="stylesheet" href="http://code.jquery.com/mobile/1.0.1/jquery.mobile-1.0.1.min.css" />
    <script src="http://code.jquery.com/jquery-1.6.4.min.js"></script>
    <script src="http://code.jquery.com/mobile/1.0.1/jquery.mobile-1.0.1.min.js"></script>
</head>
<body>
    <form id="form1" runat="server" data-ajax="false">
    <div>
        <div data-role="header">
            <h1>Main Menu</h1>
        </div><!-- /header -->
        <div data-role="ui-content"> 
            <p>
<b><asp:Label ID="lblShop" runat="server" Text="Select POS :"></asp:Label></b>
<asp:DropDownList ID="cboShop" runat="server" AutoPostBack="true" EnableViewState="true" OnSelectedIndexChanged="cboShop_SelectedIndexChanged"></asp:DropDownList>           
                </p>
            <hr />
            <table>
                <tr><td>
                <asp:Label ID="lblSalesDate" runat="server" Text="Sales Date"></asp:Label>
                </td><td>
                <asp:TextBox ID="txtsalesDate" TextMode="Date" runat="server" Enabled="false"></asp:TextBox>
                </td></tr>
            </table>
            <asp:Button ID="btnSales" runat="server" Text="Sales Entry" OnClick="btnSales_Click" Font-Bold="True" ForeColor="Green" />
            <asp:Button ID="btnReport" runat="server" Text="Sales Report" OnClick="btnReport_Click" Font-Bold="True" ForeColor="Green" />            
            <asp:Button ID="btnRequestStock" runat="server" Text="Request Order" Font-Bold="True" ForeColor="Green" OnClick="btnRequestStock_Click" />
            <asp:Button ID="btnAddStock" runat="server" Text="Receive Order" Font-Bold="True" ForeColor="Blue" OnClick="btnAddStock_Click" />            
            <asp:Button ID="btnEdit" runat ="server" Text="Edit Sales" OnClick="btnEdit_Click" Font-Bold="True" ForeColor="Blue" />            
            <asp:Button ID="btnMasterMenu" runat="server" Text="Master File" OnClick="btnMasterMenu_Click" Font-Bold="True" ForeColor="Blue" />
            <asp:Button ID="btnLogout" runat="server" Text="Log out" OnClick="btnLogout_Click" Font-Bold="True" ForeColor="Red"/>
        </div>
        <div data-role="footer" data-theme="d">
            <h4><asp:Label ID="lblheader" runat="server" Text="Copyrights 2015 : Summit footwears co.,ltd"></asp:Label></h4>
        </div><!-- /footer -->
    </div>
    </form>
</body>
</html>
