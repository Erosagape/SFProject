<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="fileadmin.aspx.cs" Inherits="shopsales.fileadmin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:TextBox ID="txtFilter" runat="server"></asp:TextBox>
        <asp:Button ID="Button5" runat="server" OnClick="Button5_Click" Text="Refresh" />
        <br />
        <asp:ListBox ID="ListBox1" runat="server" Height="313px" Width="575px"></asp:ListBox>
    </div>
        <asp:Button ID="Button1" runat="server" Text="Delete" OnClick="Button1_Click" />
        <asp:Button ID="Button2" runat="server" Text="View" OnClick="Button2_Click" />
        <asp:Button ID="Button6" runat="server" Text="Download" OnClick="Button6_Click" />
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

        <asp:Button ID="Button3" runat="server" Text="Show Locked Table" OnClick="Button3_Click" Width="89px" />

        <asp:Button ID="Button4" runat="server" Text="Clear Locked Table" Width="110px" OnClick="Button4_Click" />

        <p>
        <asp:Button ID="Button7" runat="server" Text="View User Login" OnClick="Button7_Click" Width="192px" />
        </p>

    </form>
</body>
</html>
