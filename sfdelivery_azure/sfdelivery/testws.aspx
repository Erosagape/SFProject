<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="testws.aspx.cs" Inherits="sfdelivery.testws" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:TextBox ID="txtFilter" runat="server"></asp:TextBox>
        <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
        <br />
        <asp:ListBox ID="lstFiles" runat="server" Height="268px" Width="187px"></asp:ListBox>
<table>
    <tr>
        <td>
            <asp:Button ID="btnOpen" runat="server" Text="Open" OnClick="btnOpen_Click" />
            <asp:Button ID="btnProcess" runat="server" Text="Update" OnClick="btnProcess_Click" />
            <asp:Button ID="btnClose" runat="server" Text="Close" OnClick="btnClose_Click" />
        </td>
    </tr>
</table>
        <asp:ListBox ID="lstLog" runat="server" Height="206px" Width="444px"></asp:ListBox>
    </div>
    </form>
</body>
</html>
