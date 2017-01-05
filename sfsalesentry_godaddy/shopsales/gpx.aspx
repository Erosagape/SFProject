<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="gpx.aspx.cs" Inherits="shopsales.gpx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>GPX Master Files</title>
</head>
<body>
    <div data-role="header">
        <h1>ข้อมูลมาตรฐาน GPX</h1>
    </div>
    <hr />
    <div>
        <form id="form1" runat="server">
            <asp:Panel ID="Panel1" runat="server">
            เลือกห้างร้าน <asp:DropDownList ID="cboGroupCode" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboGroupCode_SelectedIndexChanged"></asp:DropDownList>
            <br />
            <asp:Button ID="btnAddNew" runat="server" Text="เพิ่มข้อมูล" OnClick="btnAddNew_Click" />
            <asp:Button ID="btnUpdate" runat="server" Text="ปรับปรุงยอดขาย" OnClick="btnUpdate_Click" />
            <asp:GridView ID="GridView1" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowCommand="GridView1_RowCommand">
            <Columns>
                <asp:TemplateField HeaderText="View">
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" Text="Click" runat="server" CommandName ="View" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"/>
                </ItemTemplate>
                </asp:TemplateField>
            </Columns>
                <AlternatingRowStyle BackColor="White" />
                <EditRowStyle BackColor="#7C6F57" />
                <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#E3EAEB" />
                <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#F8FAFA" />
                <SortedAscendingHeaderStyle BackColor="#246B61" />
                <SortedDescendingCellStyle BackColor="#D4DFE1" />
                <SortedDescendingHeaderStyle BackColor="#15524A" />
            </asp:GridView>
                <asp:Button ID="btnBack" runat="server" Text="กลับไปเมนูหนัก" OnClick="btnBack_Click" />
                <asp:Label ID="lblReady" runat="server" Text="Ready"></asp:Label>
            </asp:Panel>
            <asp:PlaceHolder ID="PlaceHolder1" runat="server">
                <asp:Button ID="btnSave" runat="server" Text="บันทึกข้อมูล" OnClick="btnSave_Click" />
                <asp:Button ID="btnClear" runat="server" Text="ล้างหน้าจอ" OnClick="btnClear_Click"/>
                <asp:Button ID="btnCopy" runat="server" Text="เพิ่มข้อมูลใหม่" OnClick="btnCopy_Click" />
                <asp:Label ID="lblMessage" runat="server" Text="Ready"></asp:Label>
                <hr />
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="Label9" runat="server" Text="สำหรับ"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblShopName" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label1" runat="server" Text="วันที่เริ่มต้น"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtStartDate" runat="server" TextMode="date"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label2" runat="server" Text="วันที่สิ้นสุด"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtEndDate" runat="server" TextMode="date"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label3" runat="server" Text="ประเภทการขาย"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="cboSalesType" runat="server"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label4" runat="server" Text="อัตรา GPX"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtGPxRate" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label8" runat="server" Text="อัตราส่วนลด"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtDiscountRate" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label5" runat="server" Text="อัตราแชร์ส่วนลด"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtShareDiscount" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label11" runat="server" Text="วิธีคำนวน"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="cboCalType" runat="server"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label10" runat="server" Text="สัมประสิทธิ์"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtGPCal" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Button ID="btnCal" runat="server" Text="คำนวณ" OnClick="btnCal_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label6" runat="server" Text="สถานะ"></asp:Label>
                        </td>
                        <td>
                            <asp:CheckBox ID="chkActive" runat="server" Text="ใช้งาน" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label7" runat="server" Text="ID"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblOID" runat="server" Text="(New)"></asp:Label>
                        </td>
                    </tr>
                </table>
                <asp:Button ID="btnHide" runat="server" Text="กลับไปหน้าจอค้นหา" OnClick="btnHide_Click" />
            </asp:PlaceHolder>     
        </form>
    </div>
<hr />
            <div>
            <asp:Label ID="lblUserName" runat="server" Text="Label"></asp:Label>       
            </div>
</body>
</html>
