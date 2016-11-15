<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="test.aspx.cs" Inherits="shopsales.test1" ValidateRequest="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
    <div>
    
        <asp:Button ID="Button2" runat="server" Text="JSON TO XML" OnClick="Button2_Click" style="height: 26px" />
    
        <asp:Button ID="Button1" runat="server" Text="XML TO JSON" OnClick="Button1_Click" />
        <br />
        <asp:TextBox ID="TextBox1" runat="server" TextMode="MultiLine" Height="361px" Width="302px"></asp:TextBox>
    </div>
        <asp:GridView ID="GridView1" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None">
            <AlternatingRowStyle BackColor="White" />
            <EditRowStyle BackColor="#7C6F57" />
            <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#E3EAEB" />
            <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F8FAFA" />
            <SortedAscendingHeaderStyle BackColor="#246B61" />
            <SortedDescendingCellStyle BackColor="#D4DFE1" />
            <SortedDescendingHeaderStyle BackColor="#15524A" />
        </asp:GridView>
        <p>
        <asp:Button ID="Button3" runat="server" Text="tEST send mail" Width="141px" OnClick="Button3_Click" />
            <asp:Button ID="Button5" runat="server" OnClick="Button5_Click" Text="Create MAIL Data" Width="141px" />
        </p>
        <p>
            <asp:Button ID="Button4" runat="server" Text="ล้างข้อมูลไฟล์ Temp" Width="125px" OnClick="Button4_Click" />
        </p>
    </form>
</body>
</html>
