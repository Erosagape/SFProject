<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="masterfile.aspx.cs" Inherits="shopsales.masterfile" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>aerosoft shop master file</title>
    <meta name="viewport" content="width=device-width, initial-scale=1">

    <link rel="stylesheet" href="http://code.jquery.com/mobile/1.0.1/jquery.mobile-1.0.1.min.css" />
    <script src="http://code.jquery.com/jquery-1.6.4.min.js"></script>
    <script src="http://code.jquery.com/mobile/1.0.1/jquery.mobile-1.0.1.min.js"></script>
</head>
<body>
    <div data-role="header">
        <h1>Master File Menu</h1>
    </div><!-- /header -->
    <form id="form1" runat="server" data-ajax="false">
    <div data-role="ui-content"> 
        <asp:Button ID="btnStore" runat="server" Text="Shop and POS" OnClick="btnStore_Click" Font-Bold="True" ForeColor="Blue"/>
        <asp:Button ID="btnStaff" runat="server" Text="Staff" OnClick="btnStaff_Click" Font-Bold="True" ForeColor="Blue"/>
        <asp:Button ID="btnProduct" runat="server" Text="Goods" OnClick="btnProduct_Click" Font-Bold="True" ForeColor="Green"/>    
        <asp:Button ID="btnStockReport" runat="server" Text="Report Stock" OnClick="btnStockReport_Click" Visible="false" />
        <asp:Button ID="btnGPx" runat="server" Text="GPX" OnClick="btnGPx_Click" />
        <asp:Button ID="btnBack" runat="server" Text="Back to Main menu" OnClick="btnBack_Click" Font-Bold="True" ForeColor="Red"/>
    </div>
    </form>
    <div data-role="footer" data-theme="d">
        <h4><asp:Label ID="lblUsername" runat="server" Text="Develop by PK"></asp:Label></h4>
    </div><!-- /footer -->
</body>
</html>
