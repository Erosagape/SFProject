<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="listgoods.aspx.cs" Inherits="shopsales.listgoods" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Product Lists</title>
    <meta name="viewport" content="width=device-width, initial-scale=1">

    <link rel="stylesheet" href="http://code.jquery.com/mobile/1.0.1/jquery.mobile-1.0.1.min.css" />
    <script src="http://code.jquery.com/jquery-1.6.4.min.js"></script>
    <script src="http://code.jquery.com/mobile/1.0.1/jquery.mobile-1.0.1.min.js"></script>
</head>
<body>
    <form id="form1" runat="server" data-ajax="false">
    <div data-role="header">
        <h1>List of Goods</h1>
    </div><!-- /header -->
    <div data-role="main" class="ui-content" data-ajax="false" style="width: 100%;overflow:scroll">
        <table>
            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="Group :"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="cboGroup" runat="server"></asp:DropDownList>
                </td>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Type :"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="cboShoeType" runat="server"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label3" runat="server" Text="Model :"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtModel" runat="server"></asp:TextBox>
                </td>
                <td></td>
                <td>
                    <asp:LinkButton ID="LinkButton2" OnClick="LinkButton2_Click" runat="server">Add New Model</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label4" runat="server" Text="Color :"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtColor" runat="server" Width="85px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label5" runat="server" Text="Size :"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtSize" runat="server" Width="73px"></asp:TextBox>
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td>
<asp:Button ID="btnShow" runat="server" Text="Show Data" OnClick="btnShow_Click" />
                </td>
                <td>
<asp:Button ID="btnAdd" runat="server" Text="Add/Edit Goods" OnClick="btnAdd_Click"/>
                </td>
                <td>
<asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click"/>
                </td>
            </tr>
        </table>       
        <asp:GridView ID="GridView1" runat="server" OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" OnSorting="GridView1_Sorting" AllowSorting="true">
            <Columns>
                <asp:TemplateField HeaderText="View">
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" Text="Click" runat="server" CommandName ="View" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"/>
                </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
            <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
            <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F7F7F7" />
            <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
            <SortedDescendingCellStyle BackColor="#E5E5E5" />
            <SortedDescendingHeaderStyle BackColor="#242121" />
        </asp:GridView>    
    </div>
    <div data-role="footer" data-theme="d">
    <h4><asp:Label ID="lblUsername" runat="server" Text="Develop by PK"></asp:Label></h4>
    </div><!-- /footer -->
    </form>
</body>
</html>
