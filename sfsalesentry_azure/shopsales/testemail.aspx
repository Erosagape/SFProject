<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="testemail.aspx.cs" Inherits="shopsales.testemail" ValidateRequest ="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td>
                    Mail Host
                </td>
                <td>
                    <asp:TextBox ID="txtHost" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Mail Account
                </td>
                <td>
                    <asp:TextBox ID="txtAccount" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Mail Password
                </td>
                <td>
                    <asp:TextBox ID="txtPassword" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Mail Port
                </td>
                <td>
                    <asp:TextBox ID="txtPort" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Mail to
                </td>
                <td>
                    <asp:TextBox ID="txtTo" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Mail Subject
                </td>
                <td>
                    <asp:TextBox ID="txtSubject" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Mail Body
                </td>
                <td>
                    <asp:TextBox ID="txtBody" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Use SSL
                </td>
                <td>
                    <asp:CheckBox ID="chkSSL" runat="server" />
                </td>
            </tr>
        </table>
        <asp:Button ID="Button1" runat="server" Text="Sent Test mail" OnClick="Button1_Click" />
        <asp:Button ID="Button2" runat="server" Text="Save Config" OnClick="Button2_Click" />
        <asp:Label ID="Label1" runat="server" Text="Ready"></asp:Label>
        <br />
        <asp:TextBox ID="txtXMLMailTo" runat="server" TextMode="MultiLine" Height="235px" Width="274px"></asp:TextBox>
        <br />
        <asp:Button ID="Button3" runat="server" Text="Save Mail List" OnClick ="Button3_Click" />
    </div>
    </form>
</body>
</html>
