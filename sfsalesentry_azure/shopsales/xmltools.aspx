<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="xmltools.aspx.cs" Inherits="shopsales.mainapp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>  
        <asp:PlaceHolder ID="PlaceUser" runat="server">
        ข้อมูลเอกสาร
        <table>
            <tr>
                <td>
<asp:LinkButton ID="LinkUser1" runat="server" PostBackUrl="~/test.aspx">ใบส่งสินค้า (Delivery)</asp:LinkButton>
                </td>
            </tr>
        </table>
        </asp:PlaceHolder>
        <asp:PlaceHolder ID="PlaceAdmin" runat="server">
        สำหรับผู้ดูแลระบบ
        <table>
            <tr>
                <td>
<asp:LinkButton ID="LinkAdmin1" runat="server" PostBackUrl="~/CreateGoods.aspx">เพิ่มสินค้า</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td>
<asp:LinkButton ID="LinkAdmin2" runat="server" PostBackUrl="~/fileadmin.aspx">จัดการไฟล์ XML</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td>
<asp:LinkButton ID="LinkAdmin3" runat="server" PostBackUrl="~/xmlquery.aspx">Query ไฟล์ XML</asp:LinkButton>
                </td>
            </tr>
        </table>
        </asp:PlaceHolder>
        <hr />        
        <asp:Button ID="btnSignOut" runat="server" Text="ออกจากระบบ" OnClick="btnSignOut_Click" />
    </div>
    </form>
</body>
</html>
