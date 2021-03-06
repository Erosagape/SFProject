﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="addstore.aspx.cs" Inherits="shopsales.addstore" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>Add new Store</title>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>

    <link rel="stylesheet" href="http://code.jquery.com/mobile/1.0.1/jquery.mobile-1.0.1.min.css" />
    <script src="http://code.jquery.com/jquery-1.6.4.min.js"></script>
    <script src="http://code.jquery.com/mobile/1.0.1/jquery.mobile-1.0.1.min.js"></script>
    <style type="text/css">
        .auto-style1 {
            height: 26px;
        }
    </style>
</head>
<body>
    <div data-role="header">
        <h1>Point of sales Data</h1>
    </div><!-- /header -->
    <form id="form1" runat="server" data-ajax="false">
    <div data-role="content"> 
    <asp:Label ID="lblMessage" runat="server" Text="Ready"></asp:Label>
<hr />
    <table>
    <tr>
    <td>
        <asp:Label ID="Label1" runat="server" Text="ลำดับที่"></asp:Label>
    </td>
    <td>
        <asp:DropDownList ID="cboOID" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboOID_SelectedIndexChanged"></asp:DropDownList>       
    </td>
    </tr>
    <tr>
    <td>
        <asp:Label ID="Label2" runat="server" Text="รหัสจุดขาย"></asp:Label>
    </td>
    <td>
        <asp:TextBox ID="txtCustCode" runat="server"></asp:TextBox>        
    </td>
    </tr>
    <tr>
    <td>
        <asp:Label ID="Label3" runat="server" Text="ชื่อจุดขาย"></asp:Label>
    </td>
    <td>
        <asp:TextBox ID="txtCustName" runat="server"></asp:TextBox>        
    </td>
    </tr>
    <tr>
    <td>
        <asp:LinkButton ID="LinkButton1" OnClick="LinkButton1_Click" runat="server">กลุ่มห้างร้าน</asp:LinkButton>
    </td>
    <td>
        <asp:DropDownList ID="cboShopName" runat="server"></asp:DropDownList>
    </td>
    </tr>
    <tr>
    <td>
        <asp:Label ID="Label7" runat="server" Text="ชื่อกลุ่ม"></asp:Label>
    </td>
    <td>
        <asp:TextBox ID="txtShopName" runat="server"></asp:TextBox>        
    </td>
    </tr>
    <tr>
    <td>
        <asp:Label ID="Label4" runat="server" Text="สาขา"></asp:Label>
    </td>
    <td>
        <asp:TextBox ID="txtBranch" runat="server"></asp:TextBox>        
    </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="Label5" runat="server" Text="GPx (%)"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtGPx" runat="server" TextMode="number"></asp:TextBox>        
        </td>
    </tr>
    <tr>
        <td class="auto-style1">
            <asp:Label ID="Label6" runat="server" Text="แชร์ส่วนลด (%)"></asp:Label>
        </td>
        <td class="auto-style1">
            <asp:TextBox ID="txtShareDiscount" runat="server" TextMode="number"></asp:TextBox>        
        </td>
    </tr>
    <tr>
        <td class="auto-style1">
            <asp:Label ID="Label8" runat="server" Text="จังหวัด"></asp:Label>
        </td>
        <td class="auto-style1">
            <asp:DropDownList ID="cboProvince" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cboProvince_SelectedIndexChanged"></asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td class="auto-style1">
            <asp:Label ID="Label9" runat="server" Text="รหัส Sales"></asp:Label>
        </td>
        <td class="auto-style1">
            <asp:TextBox ID="txtSalesCode" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="auto-style1">
            <asp:Label ID="Label10" runat="server" Text="Supervisor"></asp:Label>
        </td>
        <td class="auto-style1">
            <asp:TextBox ID="txtSupcode" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="auto-style1">
            <asp:Label ID="Label11" runat="server" Text="Area"></asp:Label>
        </td>
        <td class="auto-style1">
            <asp:TextBox ID="txtArea" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="auto-style1">
            <asp:Label ID="Label12" runat="server" Text="รหัส Zone"></asp:Label>
        </td>
        <td class="auto-style1">
            <asp:TextBox ID="txtZone" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr>
    <td>
        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
    </td>
    <td>
        <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click"/>
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