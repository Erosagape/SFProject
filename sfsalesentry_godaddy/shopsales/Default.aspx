<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="shopsales.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>aerosoft web system</title>
</head>
<body>
    <form id="form1" runat="server" method="post" action="xmltools.aspx">
    <div>
        <div data-role="main" class="ui-content">
            <h2>
                <asp:Image ID="Image1" runat="server" ImageUrl="~/logo.png" />
            </h2>  
            <hr />          
            <table>
                <tr>
                    <td>
                        <label for="uname">ใส่รหัสพนักงาน</label>
                    </td>
                    <td>
                        <asp:TextBox ID="uname" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label for="pword">ใส่รหัสผ่าน</label>
                    </td>
                    <td>
                        <asp:TextBox ID="pword" TextMode="Password" runat="server"></asp:TextBox>
                    </td>
                </tr>
            </table>            
<asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Log in" />
<h3></h3>
        </div><!-- /content -->
    </div>
    </form>
</body>
</html>
