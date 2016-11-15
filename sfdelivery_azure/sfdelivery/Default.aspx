<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="sfdelivery.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>aerosoft web system</title>
    <meta name="HandheldFriendly" content="true" />
    <meta name="viewport"
          content="width=device-width,
                 height=device-height, user-scalable=no" />
    <meta charset="utf-8" />
    <script src="http://code.jquery.com/jquery-1.6.4.min.js"></script>
    <script>
        $(function(){
            $('#submitbtn').click(function(){
                var chk = $('#chkremember').prop('checked');
                if (chk==true) {
                    var $username = $('#uname').val();
                    var $password = $('#pword').val();
                    localStorage.setItem('usrnme', $username);
                    localStorage.setItem('password', $password);                    
                }
                else {
                    localStorage.setItem('usrnme', '');
                    localStorage.setItem('password', '');
                }; // end rememberme
            }); //end my-form

            var username = localStorage.usrnme;
            if (username != '') {
                var password = localStorage.password;
                $('#uname').val(username);
                $('#pword').val(password);
                $('#chkremember').prop('checked', true).checkboxradio('refresh');
            }
            else {
                $('#uname').val('');
                $('#pword').val('');
                $('#chkremember').prop('checked', false).checkboxradio('refresh');
            };
        });
    </script>
</head>
<body>
    <p style="background-color:lightblue">
        <asp:Image ID="Image1" runat="server" ImageUrl="~/logo.png" />
    </p>
    <form id="form1" runat="server" method="post" action="xmltools.aspx">
    <div style="background-color:white">
        <div data-role="main" class="ui-content" margin-bottom: 19px;">
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
<hr />                    
<asp:Button ID="submitbtn" runat="server" OnClick="Button1_Click" Text="Log in" />
<input type="checkbox" id="chkremember" />
<label for="chkremember">จดจำรหัสผ่าน</label>                        
        </div><!-- /content -->
<p>
    <asp:HyperLink ID="HyperLink1" NavigateUrl="~/mlogin.html" runat="server">Mobile Version</asp:HyperLink>
</p>
    </div>
    </form>
    <p style="background-color:lightblue">
        Copyrights (2016) by Summit footwear co.,ltd
    </p>
</body>
</html>
