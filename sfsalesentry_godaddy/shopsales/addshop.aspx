<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="addshop.aspx.cs" Inherits="shopsales.addshop" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>Add new Shop</title>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>

    <link rel="stylesheet" href="http://code.jquery.com/mobile/1.0.1/jquery.mobile-1.0.1.min.css" />
    <script src="http://code.jquery.com/jquery-1.6.4.min.js"></script>
    <script src="http://code.jquery.com/mobile/1.0.1/jquery.mobile-1.0.1.min.js"></script>    
</head>
<body>
    <div data-role="header">
        <h1>Shop Data</h1>
    </div><!-- /header -->
    <form id="form1" runat="server" data-ajax="false">
    <div data-role="content"> 
    <asp:Label ID="lblMessage" runat="server" Text="Ready"></asp:Label>
<hr />
    <table>
    <tr>
    <td>
        <asp:Label ID="Label1" runat="server" Text="ลำดับที่"></asp:Label>
    </td>
    <td>
        <asp:DropDownList ID="cboOID" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboOID_SelectedIndexChanged"></asp:DropDownList>       
    </td>
    </tr>
    <tr>
    <td>
        <asp:Label ID="Label2" runat="server" Text="รหัสห้าง"></asp:Label>
    </td>
    <td>
        <asp:TextBox ID="txtCustGroupCode" runat="server"></asp:TextBox>        
    </td>
    </tr>
    <tr>
    <td>
        <asp:Label ID="Label3" runat="server" Text="ชื่อจุดขาย"></asp:Label>
    </td>
    <td>
        <asp:TextBox ID="txtCustGroupNameTh" runat="server"></asp:TextBox>        
    </td>
    </tr>
    <tr>
    <td>
        <asp:Label ID="Label5" runat="server" Text="Name"></asp:Label>
    </td>
    <td>
        <asp:TextBox ID="txtCustGroupNameEng" runat="server"></asp:TextBox>        
    </td>
    </tr>
    <tr>
    <td>
        <asp:Label ID="Label4" runat="server" Text="หมายเหตุ"></asp:Label>
    </td>
    <td>
        <asp:TextBox ID="txtRemark" runat="server"></asp:TextBox>        
    </td>
    </tr>
    <tr>
    <td>
        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
    </td>
    <td>
        <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click"/>
    </td>
    </tr>
    </table>
    </div>
        <asp:GridView ID="GridView1" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None">
            <AlternatingRowStyle BackColor="White" />
            <EditRowStyle BackColor="#2461BF" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#EFF3FB" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />
        </asp:GridView>
    </form>
    <div data-role="footer" data-theme="d">
        <h4><asp:Label ID="lblUsername" runat="server" Text="Develop by PK"></asp:Label></h4>
    </div><!-- /footer -->
</body>
</html>
