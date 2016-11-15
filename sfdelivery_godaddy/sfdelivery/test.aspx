<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="test.aspx.cs" Inherits="sfdelivery.test" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ListBox ID="ListBox1" runat="server" AutoPostBack="True" Height="109px" OnSelectedIndexChanged="ListBox1_SelectedIndexChanged" Width="182px"></asp:ListBox>
        <br />
        <asp:TextBox ID="TextBox1" runat="server" Width="134px"></asp:TextBox>
        <br />
        <asp:TextBox ID="TextBox2" runat="server" TextMode="MultiLine" Height="441px" Width="456px" OnTextChanged="TextBox2_TextChanged"></asp:TextBox>
        <br />
        <asp:Button ID="Button1" runat="server" Height="33px" OnClick="Button1_Click" Text="Write JSON" Width="110px" />
        <asp:Button ID="Button2" runat="server" Height="31px" OnClick="Button2_Click" style="margin-top: 0px" Text="Update JSON" Width="111px" />
        <asp:Button ID="Button4" runat="server" Text="Process All JSON Files" OnClick="Button4_Click" />
        <asp:Button ID="Button3" runat="server" Text="Clear All JSON Files" OnClick="Button3_Click" />
        <br />
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>        
    </div>
    </form>
</body>
</html>
