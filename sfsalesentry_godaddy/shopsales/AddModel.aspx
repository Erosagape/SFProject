<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddModel.aspx.cs" Inherits="shopsales.AddModel" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>Add new Model</title>
</head>
<body>
    <div data-role="header">
        <h1>Model Data</h1>
    </div>
    <hr />
    <form id="form1" runat="server">
    <div data-role="content"> 
    <table>
    <tr>
    <td>
        <asp:Label ID="Label3" runat="server" Text="ชื่อแบบ"></asp:Label>
    </td>
    <td>
        <asp:TextBox ID="txtSMName" runat="server"></asp:TextBox>        
    </td>
    </tr>
    <tr>
        <td>
            <asp:Button ID="BtnSearch" runat="server" Text="ค้นหา" Height="26px" OnClick="BtnSearch_Click" Width="69px" />        
        </td>
    </tr>
    <tr>
    <td>
        <asp:Label ID="Label1" runat="server" Text="ชื่อแบบ"></asp:Label>
    </td>
    <td>
        <asp:DropDownList ID="cboOID" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboOID_SelectedIndexChanged"></asp:DropDownList>       
    </td>
    </tr>
    <tr>
    <td>
        <asp:Label ID="Label2" runat="server" Text="รหัสแบบ"></asp:Label>
    </td>
    <td>
        <asp:TextBox ID="txtSMCode" runat="server"></asp:TextBox>        
        <asp:LinkButton ID="LinkButton1" OnClick="btnGenNew_Click" runat="server">เพิ่มรหัสใหม่</asp:LinkButton>
    </td>
    </tr>
    <tr>
    <td>
        <asp:Label ID="Label5" runat="server" Text="ชื่อ Mold"></asp:Label>
    </td>
    <td>
        <asp:DropDownList ID="cboMold" runat="server"></asp:DropDownList>
        <asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_Click">เพิ่ม Mold ใหม่</asp:LinkButton>
    </td>
    </tr>
    <tr>
    <td>
        <asp:Label ID="Label4" runat="server" Text="ประเภทสินค้า"></asp:Label>
    </td>
    <td>
        <asp:DropDownList ID="cboShoeType" runat="server"></asp:DropDownList>
    </td>
    </tr>
    <tr>
    <td>
        <asp:Label ID="Label6" runat="server" Text="ชนิดสินค้า"></asp:Label>
    </td>
    <td>
        <asp:DropDownList ID="cboProdCat" runat="server"></asp:DropDownList>
    </td>
    </tr>
    <tr>
    <td>
        <asp:Label ID="Label7" runat="server" Text="กลุ่มขนาด"></asp:Label>
    </td>
    <td>
        <asp:DropDownList ID="cboSize" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboSize_SelectedIndexChanged"></asp:DropDownList>
    </td>
    </tr>
    <tr>
    <td>
        <asp:Label ID="Label8" runat="server" Text="ไซส์เริ่มต้น/สิ้นสุด"></asp:Label>
    </td>
    <td>
        <asp:TextBox ID="txtMinSize" runat="server"></asp:TextBox>
        <asp:TextBox ID="txtMaxSize" runat="server"></asp:TextBox>
    </td>
    </tr>
    </table>
    </div>
<hr />
        <asp:Button ID="btnSave" runat="server" Text="บันทึกข้อมูล" OnClick="btnSave_Click" Width="89px" />        
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="สร้างสินค้าอัตโนมัติ" Width="122px" />
        <p>
        <asp:Label ID="lblMessage" runat="server" Text="Ready"></asp:Label>
        </p>
    </form>
</body>
</html>
