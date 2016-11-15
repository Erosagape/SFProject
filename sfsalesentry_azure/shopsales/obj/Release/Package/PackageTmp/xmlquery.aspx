<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="xmlquery.aspx.cs" Inherits="shopsales.test" ValidateRequest="false"  %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox> 
        <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Get" />    
        <asp:Button ID="Button2" runat="server" OnClick ="Button2_Click" Text="Set" />
        <asp:Button ID="Button5" runat="server" OnClick ="Button5_Click" Text="Delete" />
        <asp:Button ID="Button6" runat="server" OnClick="Button6_Click" Text="Update" />
        <br />
        <asp:Button ID="Button3" runat="server" Text="Lock" OnClick="Button3_Click" />
        <asp:Button ID="Button4" runat="server" Text="Unlock" OnClick="Button4_Click" />
    </div>
        <asp:TextBox ID="TextBox2" runat="server" Height="287px" TextMode="MultiLine" Width="315px"></asp:TextBox>
        <br />
    </form>
</body>
</html>