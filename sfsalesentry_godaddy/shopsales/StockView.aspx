<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StockView.aspx.cs" Inherits="shopsales.StockView" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Stock Report By Monthly</title>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <link rel="stylesheet" href="http://code.jquery.com/mobile/1.0.1/jquery.mobile-1.0.1.min.css" />
    <script src="http://code.jquery.com/jquery-1.6.4.min.js"></script>
    <script src="http://code.jquery.com/mobile/1.0.1/jquery.mobile-1.0.1.min.js"></script>    
</head>
<body>
    <div data-role="header">
        <h1>Stock Report</h1>
    </div><!-- /header -->
<div data-role="ui-content" style="width: 100%;overflow:scroll">
    <form id="form1" runat="server">
        <table>
            <tr>
                <td>
                    Year
                </td>
                <td>
                    Month
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txtYear" runat="server" Width="57px"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="txtMonth" runat="server" Width="32px"></asp:TextBox>
                </td>
            </tr>
            <tr>
            <td colspan="2">
                <asp:DropDownList ID="cboCust" runat="server"></asp:DropDownList>
            </td>
            </tr>
            <tr>
                <td>        
                    <asp:Button ID="btnCalculate" runat="server" Text="Stock Movement" OnClick="btnCalculate_Click" />
                </td>
                <td>
                    <asp:Button ID="btnOnhand" runat="server" Text="Stock Onhand" OnClick="btnOnhand_Click" />
                </td>
            </tr>
        </table>
        <hr />
        <table>
            <tr>
                <td>
                    View By Model : <asp:DropDownList ID="cboGoods" runat="server"></asp:DropDownList>
                </td>
                <td>
                    <asp:Button ID="btnProcess" runat="server" Text="Show" OnClick="btnProcess_Click"/>
                </td>
            </tr>
        </table>                
        <asp:GridView ID="GridView1" runat="server" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3">
            <FooterStyle BackColor="White" ForeColor="#000066" />
            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
            <RowStyle ForeColor="#000066" />
            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#007DBB" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#00547E" />
        </asp:GridView>
        <asp:Button ID="btnBack" runat="server" Text="กลับเมนูหลัก" OnClick="btnBack_Click" />
        <asp:Label ID="lblMessage" runat="server" Text="Label"></asp:Label>
    </form>
    </div>
    <div data-role="footer" data-theme="d">
        <h4><asp:Label ID="lblUsername" runat="server" Text="Develop by PK"></asp:Label></h4>
    </div><!-- /footer -->
</body>
</html>
