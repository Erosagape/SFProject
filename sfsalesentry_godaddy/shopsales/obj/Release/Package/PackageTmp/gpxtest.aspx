<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="gpxtest.aspx.cs" Inherits="shopsales.gpxtest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        Date
        <asp:TextBox ID="TextBox1" runat="server" TextMode="date"></asp:TextBox>
        <br />
        Sales Type
        <asp:DropDownList ID="cboSalesType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboSalesType_SelectedIndexChanged"></asp:DropDownList>
        <asp:TextBox ID="TextBox2" runat="server" TextMode="number"></asp:TextBox>
        <br />
        Shop ID
        <asp:DropDownList ID="cboShop" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboShop_SelectedIndexChanged"></asp:DropDownList>
        <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
        <br />
        Discount
        <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
        <br />
        <asp:Button ID="Button1" runat="server" Text="Get Promotion" OnClick="Button1_Click" />
        <br />
        <asp:Label ID="Label2" runat="server" Text="Sales Out"></asp:Label>
        <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
        <br />
        <asp:Button ID="Button2" runat="server" Text="Calculate GPX" OnClick="Button2_Click" />
        <br />
        <asp:Label ID="Label3" runat="server" Text="Sales In"></asp:Label>
        <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="Label1" runat="server" Text="Result"></asp:Label>
        <br />
        <asp:Button ID="Button3" runat="server" Text="Patch GPX" Width="87px"  OnClick="Button3_Click"/>
    </div>
    </form>
</body>
</html>
