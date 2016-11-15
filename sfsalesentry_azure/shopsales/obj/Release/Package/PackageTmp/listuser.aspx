<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="listuser.aspx.cs" Inherits="shopsales.listuser" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <title>User List</title>
    <link rel="stylesheet" href="http://code.jquery.com/mobile/1.0.1/jquery.mobile-1.0.1.min.css" />
    <script src="http://code.jquery.com/jquery-1.6.4.min.js"></script>
    <script src="http://code.jquery.com/mobile/1.0.1/jquery.mobile-1.0.1.min.js"></script>

</head>
<body>
    <form id="form1" runat="server" data-ajax="false">
    <div data-role="header">
        <h1>List of Staff</h1>
    </div><!-- /header -->
    <div data-role="main" class="ui-content" style="width: 100%;overflow:scroll">   
<table>
    <tr>
        <td>
            <asp:Button ID="btnAddUser" runat="server" Text="Add/Edit User" OnClick="btnAddUser_Click"/>
        </td>
        <td>
            <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click"/>
        </td>
    </tr>
</table>         
        <asp:GridView ID="GridView1" runat="server" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" OnRowCommand="GridView1_RowCommand" OnSorting="GridView1_Sorting" AllowSorting="true">
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
