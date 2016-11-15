<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="xmltools.aspx.cs" Inherits="sfdelivery.mainapp" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Main Menu</title>
    <style>
        lblMessage
        {
            justify-content:flex-end
        }
    </style>
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
        <asp:PlaceHolder ID="PlaceUser" runat="server">
        <b>ข้อมูลใบส่งสินค้า (Delivery Order)</b>
        <table>
            <tr>
                <td>
        <asp:Label ID="Label1" runat="server" Text="ประจำปี/เดือน :"></asp:Label>
                </td>
                <td>
<asp:DropDownList ID="cboMonthYear" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cboMonthYear_SelectedIndexChanged"></asp:DropDownList>            
<asp:Label ID="lblDateModified" runat="server" Text="" ForeColor="#CC0000"></asp:Label>                
                </td>
            </tr>
            <tr>
                <td colspan="2">
<asp:LinkButton ID="LinkUser1" runat="server" PostBackUrl="~/deliveryhd.aspx">ค้นหาข้อมูลใบส่งสินค้า</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td colspan="2">
<asp:LinkButton ID="LinkUser2" runat="server" PostBackUrl="~/Customer.aspx">ค้นหาตามรายชื่อข้อมูลลูกค้า</asp:LinkButton>
                </td>
            </tr>
        </table>
        </asp:PlaceHolder>
        <asp:PlaceHolder ID="PlaceAdmin" runat="server">
<br />
        <b>สำหรับแผนกจัดส่งสินค้า</b>
        <table>
            <tr>
                <td>
<asp:LinkButton ID="LinkAdmin4" runat="server" PostBackUrl="~/delivery.aspx">บันทึกสถานะ - ใบส่งสินค้า</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td>
<a href="sfdelivery.apk">Download App บันทึกสถานะใบส่งของ (Android)</a>
                </td>
            </tr>
        </table>
<br />
        <b>ข้อมูลมาตรฐาน</b>
        <table>
            <tr>
                <td>
<asp:LinkButton ID="LinkAdmin5" runat="server" PostBackUrl="~/WebUser.aspx">ข้อมูลผู้ใช้งาน</asp:LinkButton>
                </td>
            </tr>
        </table>
<br />
        <b>สำหรับผู้ดูแลระบบ</b>
        <table>
            <tr>
                <td>
<asp:LinkButton ID="LinkAdmin1" runat="server" PostBackUrl="~/fileadmin.aspx">จัดการไฟล์ XML</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td>
<asp:LinkButton ID="LinkAdmin2" runat="server" PostBackUrl="~/xmlquery.aspx">Query ไฟล์ XML</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td>
<asp:LinkButton ID="LinkAdmin3" runat="server" PostBackUrl="~/uploadData.aspx">Upload ข้อมูล Delivery</asp:LinkButton>
                </td>
            </tr>
        </table>
        </asp:PlaceHolder>
<hr />
        <asp:Label ID="lblstatus" runat="server" Text="Ready"></asp:Label>
        <p style="background-color:lightblue">
                    <asp:Button ID="btnSignOut" runat="server" Text="ออกจากระบบ" OnClick="btnSignOut_Click" />
        </p>
    </div>
    </form>
</body>
</html>
