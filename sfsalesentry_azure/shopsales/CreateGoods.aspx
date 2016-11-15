<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateGoods.aspx.cs" Inherits="shopsales.CreateGoods" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>Create Goods</title>
</head>
<body>
    <div data-role="header">
        <h1>Generate Goods</h1>
    </div>
    <hr />
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="Label1" runat="server" Text="เลือกสินค้า"></asp:Label>
        <br />
        <asp:DropDownList ID="cboGoods" runat="server" Width="177px">
        </asp:DropDownList>
        <asp:Button ID="btnShow" runat="server" Text="แสดง" OnClick="btnShow_Click" />
    </div>
        <asp:GridView ID="GridView1" runat="server">
        </asp:GridView>
        <hr />
        <asp:Label ID="Label2" runat="server" Text="รหัสใหม่"></asp:Label>
        &nbsp;<asp:TextBox ID="txtItem" runat="server" Width="100px"></asp:TextBox>
                <br />
        <asp:Label ID="Label5" runat="server" Text="รุ่น"></asp:Label>
                &nbsp;<asp:DropDownList ID="cboModel" runat="server" Height="16px" Width="139px" AutoPostBack="True" OnSelectedIndexChanged="cboModel_SelectedIndexChanged">
        </asp:DropDownList>
                <asp:Button ID="btnAddModel" runat="server" Text="เพิ่มรุ่น" OnClick="btnAddModel_Click" />
                <br />
        <asp:Label ID="Label6" runat="server" Text="สี"></asp:Label>
        &nbsp;<asp:DropDownList ID="cboColor" runat="server"></asp:DropDownList><asp:Button ID="btnAddcolor" runat="server" Text="เลือกสี" OnClick="btnAddcolor_Click" Width="75px" />
                <br />
        <asp:ListBox ID="LstColor" runat="server" Width="158px" OnSelectedIndexChanged="LstColor_SelectedIndexChanged"></asp:ListBox>
                <br />
                <asp:Button ID="btnRemoveColor" runat="server" Text="ลบสี" OnClick="btnRemoveColor_Click" Width="88px" />
                <br />
        <asp:Label ID="Label3" runat="server" Text="ขนาดเริ่มต้น/สิ้นสุด"></asp:Label>
        <br />
        <asp:TextBox ID="txtSizeFrom" runat="server" Width="100px"></asp:TextBox>
        <asp:TextBox ID="txtSizeTo" runat="server" Width="100px"></asp:TextBox>
        <br />
        <asp:Label ID="Label4" runat="server" Text="ราคา"></asp:Label>
        &nbsp;<asp:TextBox ID="txtPrice" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="Label7" runat="server" Text="ประเภท"></asp:Label>
            &nbsp;<asp:DropDownList ID="cboShoeType" runat="server"></asp:DropDownList>
        <hr />
        <asp:Button ID="btnCreate" runat="server" Text="สร้างข้อมูล" OnClick="btnCreate_Click" />
        <br />
        <asp:ListBox ID="listLog" runat="server" Width="584px" Height="289px"></asp:ListBox>
    </form>
</body>
</html>
