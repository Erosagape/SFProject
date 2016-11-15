<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="mdelivery.aspx.cs" Inherits="sfdelivery.mdelivery" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <title>delivery data</title>
    <meta name="HandheldFriendly" content="true" />
    <meta name="viewport"
          content="width=device-width,
                 height=device-height, user-scalable=no" />
    <meta charset="utf-8" />
    <link rel="stylesheet" href="http://code.jquery.com/mobile/1.0.1/jquery.mobile-1.0.1.min.css" />
    <script src="http://code.jquery.com/jquery-1.6.4.min.js"></script>
    <script src="http://code.jquery.com/mobile/1.0.1/jquery.mobile-1.0.1.min.js"></script>
</head>
<body>
    <div data-role="header">
    <h1>
        Delivery Data
        </h1>
    </div>
    <form id="form1" runat="server" data-ajax="false">
    <div>
        <table>
            <tr>
                <td>
                    <b>เลขที่เอกสาร</b>
                </td>
                <td>
                    <asp:TextBox ID="txtID" runat="server"></asp:TextBox>                    
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnSearch" runat="server" Text="แสดง" OnClick="btnSearch_Click" />
                </td>
                <td>
                    <asp:Button ID="btnBack" runat="server" Text="ย้อนกลับ" OnClick="btnBack_Click"/>
                </td>
            </tr>
            <tr>
                <td>
                    <b>วันที่ส่งของ</b>
                </td>
                <td>
                    <asp:Label ID="lblMark6" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <b>สถานที่ส่งของ</b>
                </td>
                <td>
                    <asp:Label ID="lblShipTo" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <b>ขนส่ง</b>
                </td>
                <td>
                    <asp:Label ID="lblTransport" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <b>คนขับ</b>
                </td>
                <td>
                    <asp:Label ID="lblDriver" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <b>หมายเหตุ</b>
                </td>
                <td>
                    <asp:Label ID="lblRemark1" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <b>จำนวนสินค้า</b>
                </td>
                <td>
                    <asp:Label ID="lblQty" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <b>สถานะการขนส่ง</b>
                </td>
                <td>
                    <asp:RadioButtonList ID="optStatus" runat="server">
                        <asp:ListItem Selected="True" Value="1">ขนส่งแล้ว</asp:ListItem>
                        <asp:ListItem Value="0">ยังไม่ขนส่ง</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
        </table>
        <asp:GridView ID="GridView1" runat="server" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal">
            <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
            <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
            <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F7F7F7" />
            <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
            <SortedDescendingCellStyle BackColor="#E5E5E5" />
            <SortedDescendingHeaderStyle BackColor="#242121" />
        </asp:GridView>   
    </div>
    <div data-role="footer">
        <asp:Label ID="lblUser" runat="server" Text="Welcome"></asp:Label>
    </div>
    </form>
</body>
</html>
