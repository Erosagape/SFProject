<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="addGoods.aspx.cs" Inherits="shopsales.addGoods" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Add new goods</title>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <link rel="stylesheet" href="http://code.jquery.com/mobile/1.0.1/jquery.mobile-1.0.1.min.css" />
    <script src="http://code.jquery.com/jquery-1.6.4.min.js"></script>
    <script src="http://code.jquery.com/mobile/1.0.1/jquery.mobile-1.0.1.min.js"></script>
</head>
<body>
    <div data-role="header">
        <h1>Goods Data</h1>
    </div><!-- /header -->
    <form id="form1" runat="server" data-ajax="false">
    <div data-role="content"> 
    <table>
    <tr>
        <td>
            <asp:Label ID="Label1" runat="server" Text="แบบ/รุ่น"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtModelName" runat="server"></asp:TextBox>            
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="Label2" runat="server" Text="สี"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="cboColNameTh" runat="server" AutoPostBack ="true" OnSelectedIndexChanged="cboColNameTh_SelectedIndexChanged"></asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="Label3" runat="server" Text="Size"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtSizeNo" TextMode="Number" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
        </td>
        <td>
            <asp:Label ID="lblMessage" runat="server" Text="Ready"></asp:Label>
        </td>
        <td>
            <asp:HiddenField ID="txtColTypeID" runat="server" />
            <asp:HiddenField ID="txtOID" runat="server" />
            <asp:HiddenField ID="txtColId" runat="server" />
        </td>
    </tr>
    </table>
<hr />
    <table>
    <tr>
        <td>
            <asp:Label ID="Label4" runat="server" Text="รหัสสินค้า"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtGoodsCode" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="Label5" runat="server" Text="ชื่อสินค้า"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtGoodsName" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
            สี <asp:TextBox ID="txtColNameInit" runat="server" Width="52px"></asp:TextBox>
        </td>
        <td>
            <asp:TextBox ID="txtColNameTh" runat="server"></asp:TextBox>
            <asp:TextBox ID="txtColNameEng" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="Label8" runat="server" Text="กลุ่มสินค้า"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="cboGroupCode" runat="server" ></asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="Label6" runat="server" Text="ชนิดสินค้า"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="cboSTcode" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboSTcode_SelectedIndexChanged"></asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="Label7" runat="server" Text="ประเภทสินค้า"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="cboProdCatCode" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboProdCatCode_SelectedIndexChanged"></asp:DropDownList>
            <asp:HiddenField ID="txtProdCatCode" runat ="server" />
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="Label9" runat="server" Text="ราคาป้าย"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtStdSellPrice" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
            <asp:HiddenField ID="txtSTCode" runat="server" />
            <asp:HiddenField ID="txtSTName" runat="server" />
            <asp:HiddenField ID="txtSTName2" runat="server" />
        </td>        
    </tr>
    <tr>
        <td>
            &nbsp;</td>        
    </tr>
    <tr>
        <td>
            <asp:HiddenField ID="txtProdCatID" runat="server" />
        </td>        
    </tr>
    <tr>
        <td>
            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
        </td>
        <td>
            <asp:Button ID="btnBack" runat ="server" Text="Goods List" OnClick="btnBack_Click" />
        </td>
    </tr>
    </table>
        <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" >ปรับปรุงข้อมูลยอดขาย</asp:LinkButton>
    </div>
    </form>
    <div data-role="footer" data-theme="d">
        <h4><asp:Label ID="lblUsername" runat="server" Text="Develop by PK"></asp:Label></h4>
    </div>
</body>
</html>
