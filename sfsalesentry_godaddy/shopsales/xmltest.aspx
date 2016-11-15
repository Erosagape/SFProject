<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="xmltest.aspx.cs" Inherits="shopsales.xmltest"  ValidateRequest="false"  %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Label ID="Label1" runat="server" Text="Data Name"></asp:Label>
        <asp:TextBox ID="txtDataName" runat="server"></asp:TextBox>
        <asp:Button ID="btnGetData" runat="server" Text="Read" OnClick="btnGetData_Click" />
        <asp:Button ID="btnSetData" runat="server" Text="Write" OnClick ="btnSetData_Click" />
        <asp:CheckBox ID="chkJSON" runat="server" Text="Use JSON"/>
        <asp:GridView ID="GridView1" runat="server" OnRowDataBound="GridView1_RowDataBound"></asp:GridView>
        <asp:Label ID="lblMessage" runat="server" Text="Ready"></asp:Label>
        <br />
        <asp:TextBox ID="txtXML" runat="server" Height="316px" TextMode="MultiLine" Width="283px"></asp:TextBox>
        <br />
        <asp:Button ID="btnUpdateGrid" runat="server" Text="Get Datasource" OnClick="btnUpdateGrid_Click" />
        <asp:Button ID="btnLoadData" runat="server" Text="Set Datasource" OnClick="btnLoadData_Click" />
    </div>
    </form>
</body>
</html>
