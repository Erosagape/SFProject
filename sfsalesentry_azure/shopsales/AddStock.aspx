<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddStock.aspx.cs" Inherits="shopsales.AddStock" EnableEventValidation="true"  %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Add Stock</title>
<meta name="viewport" content="width=device-width, initial-scale=1"/>
    <link rel="stylesheet" href="http://code.jquery.com/mobile/1.0.1/jquery.mobile-1.0.1.min.css" />
    <script src="http://code.jquery.com/jquery-1.6.4.min.js"></script>
    <script src="http://code.jquery.com/mobile/1.0.1/jquery.mobile-1.0.1.min.js"></script>    
</head>
<body>
    <div data-role="header">
        <h1>Add Stock To POS</h1>
    </div>
    <div data-role="main" class="ui-content" style="width: 100%;overflow:scroll">   
        <form id="form1" runat="server" data-ajax="false">
        <asp:Panel ID="Panel1" runat="server">
        <table>
            <tr>
                <td>
                    ประจำปี
                </td>
                <td>
                    เดือน
                </td>
            </tr>
            <tr>
                <td>
        <asp:TextBox ID="txtYear" runat="server"></asp:TextBox>        
                </td>
                <td>
        <asp:TextBox ID="txtMonth" runat="server"></asp:TextBox>
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td>
                    <asp:Label ID="lblShop" runat="server" Text="กรุณาเลือกจุดขาย"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:DropDownList ID="cboShop" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboShop_SelectedIndexChanged"></asp:DropDownList>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
            <asp:RadioButtonList ID="optDataselect" runat="server">
                <asp:ListItem Value="0">PC สั่งของ</asp:ListItem>
                <asp:ListItem Value="1">เติมของแล้ว</asp:ListItem>
                <asp:ListItem Value="2">ทั้งหมด</asp:ListItem>
            </asp:RadioButtonList>
                </td>
                <td>
            <asp:Button ID="Button1" runat="server" Text="แสดงข้อมูล" OnClick="Button1_Click" style="height: 26px" />
            <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" Text="เพิ่มเอกสาร" />
                </td>
            </tr>
            </table>
        <asp:GridView ID="GridView1" runat="server" OnRowCommand="GridView1_RowCommand" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3">            
            <Columns>
                <asp:TemplateField HeaderText="View">
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" Text="Click" runat="server" CommandName ="View" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"/>
                </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <FooterStyle BackColor="White" ForeColor="#000066" />
            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
            <RowStyle ForeColor="#000066" />
            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#007DBB" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#00547E" />
        </asp:GridView>
        </asp:Panel>
        <asp:Panel ID="Panel2" runat="server">
        <asp:Label ID="lblMessage" runat="server" Text="Ready"></asp:Label>
        <table>
            <tr>
                <td>
                    เลขที่อ้างอิง
                </td>
                <td>
                    <asp:TextBox ID="txtRefNo" runat="server"></asp:TextBox>

                    <asp:Button ID="btnSearch" runat="server" Text="แสดงข้อมูล" OnClick="btnSearch_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    วันที่ทำรายการ
                </td>
                <td>
                    <asp:TextBox ID="txtTransDate" TextMode="date" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    รุ่นสินค้า
                </td>
                <td>
                    <asp:TextBox ID="txtModelCode" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    สี
                </td>
                <td>
                    <asp:DropDownList ID="cboColor" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboColor_SelectedIndexChanged"></asp:DropDownList> 
                </td>
                <td>
                    <asp:Label ID="lblColor" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    ขนาด
                </td>
                <td>
                    <asp:TextBox ID="txtSizeNo" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    ประเภท
                </td>
                <td>
                    <asp:DropDownList ID="cboSTName" runat="server"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    จำนวน
                </td>
                <td>
                    <asp:TextBox ID="txtQty" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:LinkButton ID="lnkCheckPrice" runat="server" OnClick="lnkCheckPrice_Click">ราคาขาย</asp:LinkButton>
                </td>
                <td>
                    <asp:TextBox ID="txtTagPrice" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    วันที่ส่งของ
                </td>
                <td>
                    <asp:TextBox ID="txtStockDate" runat="server" TextMode="date"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="บันทึก" />        
                </td>
                <td>
                    <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="กลับไปหน้าจอค้นหา" />        
                </td>
            </tr>
        </table>        
        </asp:Panel>
        <asp:Button ID="btnBack" runat="server" Text="กลับเมนูหลัก" OnClick="btnBack_Click" />
        </form>
    </div>
    <div data-role="footer" data-theme="d">
        <h4><asp:Label ID="lblUsername" runat="server" Text="Develop by PK"></asp:Label></h4>
    </div><!-- /footer -->
</body>
</html>
