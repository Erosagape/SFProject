<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="adduser.aspx.cs" Inherits="shopsales.adduser" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Add new user</title>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>

    <link rel="stylesheet" href="http://code.jquery.com/mobile/1.0.1/jquery.mobile-1.0.1.min.css" />
    <script src="http://code.jquery.com/jquery-1.6.4.min.js"></script>
    <script src="http://code.jquery.com/mobile/1.0.1/jquery.mobile-1.0.1.min.js"></script>
</head>
<body>
    <div data-role="header">
        <h1>User Data</h1>
    </div><!-- /header -->
    <form id="form1" runat="server" data-ajax="false">
    <div data-role="content"> 
    <table>
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Text="รหัสประจำตัว"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtID" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnSearch" runat="server" Text="ค้นหา" OnClick="btnSearch_Click" />
            </td>
            <td>
                <asp:Label ID="lblMessage" runat="server" Text="Ready"></asp:Label>
            </td>
        </tr>
        </table>
        <hr />
        <table>
        <tr>
            <td>
<asp:Button ID="btnSave" runat="server" Text="Save User" OnClick="btnSave_Click" />        
            </td>
            <td>
<asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click"/>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label2" runat="server" Text="ชื่อ"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label3" runat="server" Text="รหัสผ่าน"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtPassword" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label4" runat="server" Text="กลุ่ม User"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="cboRole" runat="server"></asp:DropDownList>               
            </td>
        </tr>
        <tr>
            <td>
                <asp:LinkButton ID="LinkButton1" OnClick="LinkButton1_Click" runat="server">ประจำจุดขาย</asp:LinkButton>
            </td>
            <td>
                <asp:DropDownList ID="cboShopID" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboShopID_SelectedIndexChanged"></asp:DropDownList>               
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label6" runat="server" Text="ชื่อห้าง"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtStore" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label7" runat="server" Text="สาขา"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtBranch" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label8" runat="server" Text="แผนก"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtDepartment" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label9" runat="server" Text="กลุ่มการขาย"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtCustGroup" runat="server"></asp:TextBox>
            </td>
        </tr>
            <tr>
                <td></td>
                <td>    
                    <asp:Label ID="lblCustGroup" runat="server" Text=""></asp:Label>
                </td>
            </tr>
    </table>        
    </div>
    </form>
    <div data-role="footer" data-theme="d">
        <h4><asp:Label ID="lblUsername" runat="server" Text="Develop by PK"></asp:Label></h4>
    </div><!-- /footer -->
</body>
</html>
