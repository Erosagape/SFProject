<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="xmlquery.aspx.cs" Inherits="sfdelivery.xmlquery" ValidateRequest="false"  %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
body {
    background-color: lightblue;
}
</style>
</head>
<body>
    <p style="background-color:white">
    <asp:Image ID="Image1" runat="server" ImageUrl="~/logo.png" />
    </p>
    <form id="form1" runat="server">
    <div>
        <asp:CheckBox ID="chkJSON" runat="server" Text="Use JSON" />
        <br />
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox> 
        <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Get" />    
        <asp:Button ID="Button2" runat="server" OnClick ="Button2_Click" Text="Set" />
        <asp:Button ID="Button5" runat="server" OnClick ="Button5_Click" Text="Delete" />
        <br />
        <asp:Button ID="Button3" runat="server" Text="Lock" OnClick="Button3_Click" />
        <asp:Button ID="Button4" runat="server" Text="Unlock" OnClick="Button4_Click" />
    </div>
        <asp:TextBox ID="TextBox2" runat="server" Height="287px" TextMode="MultiLine" Width="315px"></asp:TextBox>
        <br />
        <asp:Button ID="Button6" runat="server" Text="View Data" OnClick="Button6_Click" style="height: 26px" />
        <asp:DropDownList ID="DropDownList1" runat="server">
        </asp:DropDownList>
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
        <asp:Button ID="Button7" runat="server" OnClick="Button7_Click" Text="Show Customer" Width="129px" />
        <asp:TextBox ID="TextBox4" runat="server" Width="128px"></asp:TextBox>
    </form>
</body>
</html>