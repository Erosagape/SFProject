<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="uploadData.aspx.cs" Inherits="sfdelivery.uploadData" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
<table style="width :100%;background-color:lightblue">
    <tr>
        <td>
                <asp:Image ID="Image1" runat="server" ImageUrl="~/logo.png" Height="66px" Width="222px" />
        </td>
        <td style="text-align:right">
                <asp:Label ID="lblMessage" runat="server" Text="Welcome" Font-Bold="True"></asp:Label>    
        </td>
    </tr>
</table>
    <form id="form1" runat="server">
    <div>
        เลือกไฟล์ JSON Data ที่นี่
        <br />
        <asp:FileUpload ID="FileUpload1" runat="server" Width="401px" />
        <br />
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Load Data" Width="86px" />
        <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Save Data" />
        <asp:Label ID="StatusLabel" runat="server" Text="Ready"></asp:Label>
        <asp:HiddenField ID="fileServer" runat="server" />
    </div>
        <asp:GridView ID="GridView1" runat="server" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical">
            <AlternatingRowStyle BackColor="#DCDCDC" />
            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
            <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
            <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
            <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#0000A9" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#000065" />
        </asp:GridView>
    </form>
</body>
</html>
