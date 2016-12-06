<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="dailyreport.aspx.cs" Inherits="shopsales.dailiyreport" ValidateRequest="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Calendar ID="Calendar1" runat="server" OnSelectionChanged="Calendar1_SelectionChanged"></asp:Calendar>
        At Date <asp:TextBox ID="txtDate" runat="server"></asp:TextBox>
    <table>
        <tr>
            <td>
                <asp:Button ID="Button1" runat="server" Text="Calculate" OnClick="Button1_Click" />
            </td>
            <td>
                Summary value
            </td>
            <td>
                <asp:TextBox ID="TextBox1" runat="server" Width="535px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="Button2" runat="server" Text="Calculate" OnClick="Button2_Click" />
            </td>
            <td>
                Summary volume
            </td>
            <td>
                <asp:TextBox ID="TextBox2" runat="server" Width="535px"></asp:TextBox>
            </td>
        </tr>
    </table>
        <hr />
        <asp:Button ID="Button4" runat="server" Text="Calculate All" OnClick="Button4_Click" />
        <asp:Button ID="Button3" runat="server" Text="Send E-Mail" OnClick="Button3_Click" />
        <asp:Label ID="lblMessage" runat="server" Text="Ready"></asp:Label>
    </div>
        <asp:HyperLink ID="HyperLink1" NavigateUrl="~/testemail.aspx" runat="server">Config Email</asp:HyperLink>
    </form>
</body>
</html>
