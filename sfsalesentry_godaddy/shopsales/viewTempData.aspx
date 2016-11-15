<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="viewTempData.aspx.cs" Inherits="shopsales.viewTempData" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Button ID="Button1" runat="server" Text="Process" OnClick="Button1_Click" />
        <asp:Label ID="Label1" runat="server" Text="Ready"></asp:Label>
        <br />
        <asp:GridView ID="GridView1" runat="server" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
        </asp:GridView>
    
    </div>
    </form>
</body>
</html>
