<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="menu.aspx.cs" Inherits="sfdelivery.menu" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>aerosoft delivery tracking</title>
    <meta name="HandheldFriendly" content="true" />
    <meta name="viewport"
          content="width=device-width,
                 height=device-height, user-scalable=no" />
    <meta charset="utf-8" />
    <link rel="stylesheet" href="http://code.jquery.com/mobile/1.0.1/jquery.mobile-1.0.1.min.css" />
    <script src="http://code.jquery.com/jquery-1.6.4.min.js"></script>
    <script src="http://code.jquery.com/mobile/1.0.1/jquery.mobile-1.0.1.min.js"></script>
</head>
<body>
    <form id="form1" runat="server" data-ajax="false">
    <div data-role="header">
    <h1>
        Main Menu</h1>
    </div>
    <div data-role="ui-content">        
        <asp:Calendar ID="selDate" runat="server" OnSelectionChanged="selDate_SelectionChanged"></asp:Calendar>    
        <asp:Label ID="Label1" runat="server" Text="ค้นหาตามชื่อลูกค้า"></asp:Label>
        <asp:TextBox ID="txtCustomer" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="Label2" runat="server" Text="ประจำวันที่"></asp:Label>
        <asp:TextBox ID="txtDate" runat="server"></asp:TextBox>
        <br />
        <asp:Button ID="btnView" runat="server" Text="แสดงใบส่งของ" OnClick="btnView_Click" />
        <br />
        <asp:GridView ID="GridView1" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowCommand="GridView1_RowCommand">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:TemplateField HeaderText="View">
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" Text="View" runat="server" CommandName ="View" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                </ItemTemplate>
                </asp:TemplateField>
            </Columns>
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
    </div>
    <div data-role="footer" data-theme="d">
        <asp:Label ID="lblUser" runat="server" Text="Welcome"></asp:Label>
    </div>
    </form>
</body>
</html>
