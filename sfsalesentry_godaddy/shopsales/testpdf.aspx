<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="testpdf.aspx.cs" Inherits="shopsales.testpdf" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:PlaceHolder ID="frm1" runat="server"></asp:PlaceHolder>
        <br />
        <asp:TextBox ID="TextBox1" runat="server" Height="218px" TextMode="MultiLine" Width="292px"></asp:TextBox>
        <br />
        <asp:Button ID="Button1" runat="server" Text="Generate PDF" OnClick="Button1_Click" />
    </div>
    </form>
</body>
</html>
