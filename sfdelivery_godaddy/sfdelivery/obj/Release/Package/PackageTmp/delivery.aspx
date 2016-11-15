<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="delivery.aspx.cs" Inherits="sfdelivery.delivery" %>

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
                <asp:Label ID="lblUser" runat="server" Text="Welcome" Font-Bold="True"></asp:Label>    
        </td>
    </tr>
</table>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="Label1" runat="server" Font-Bold="true" ForeColor="ForestGreen" Text="บันทึกสถานะใบส่งสินค้า"></asp:Label>
<hr />
ข้อมูลประจำปี/เดือน
<asp:DropDownList ID="cboMonthYear" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cboMonthYear_SelectedIndexChanged"></asp:DropDownList>            
        <asp:Label ID="Label2" runat="server" Text="วันที่ส่งของ"></asp:Label>
        <asp:TextBox ID="txtDeliveryDate" runat="server"></asp:TextBox>
        <asp:HyperLink ID="HyperLink1" NavigateUrl="~/deliveryhd.aspx" runat="server">ค้นหาเลขที่เอกสาร</asp:HyperLink>
        <asp:TextBox ID="txtDocNo" runat="server"></asp:TextBox>
        <asp:Button ID="Button1" runat="server" Text="แสดงข้อมูล" OnClick="Button1_Click" />
        <asp:Label ID="lblRecCount" runat="server" Text="Ready"></asp:Label>
        <p style="background-color:lightblue">
        <asp:Button ID="Button3" runat="server" Text="บันทึกสถานะตามที่เลือก" OnClick="Button3_Click" Width="174px" />
        <asp:DropDownList ID="cboStatus" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboStatus_SelectedIndexChanged"></asp:DropDownList>
        <asp:Label ID="Label4" runat="server" Text="หมายเหตุ"></asp:Label>
        <asp:TextBox ID="txtRemark" runat="server"></asp:TextBox>
            <br />
        <asp:Button ID="Button2" runat="server" Text="เลือกทั้งหมด" OnClick="Button2_Click" Width="88px" />
        <asp:Button ID="Button4" runat="server" Text="ไม่เลือกทั้งหมด" OnClick="Button4_Click" Width="88px" />
        </p>
        <asp:Panel ID="panel" runat="server">
        </asp:Panel>
    </div>
    </form>
</body>
</html>
