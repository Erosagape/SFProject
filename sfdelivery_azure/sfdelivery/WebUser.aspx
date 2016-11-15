<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebUser.aspx.cs" Inherits="sfdelivery.WebUser" %>

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
    <asp:HyperLink ID="HyperLink1" Font-Bold="true" ForeColor="ForestGreen" NavigateUrl="~/xmltools.aspx" runat="server">Menu</asp:HyperLink> &nbsp;>>
    <asp:Label ID="Label1" runat="server" Font-Bold="true" ForeColor="ForestGreen" Text="ข้อมูลผู้ใช้งานระบบ"></asp:Label>
<hr />
    <div>
        <asp:GridView ID="GridView1" runat="server" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" OnRowCommand="GridView1_RowCommand">
            <Columns>
                <asp:TemplateField HeaderText="View">
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" Text="View" runat="server" CommandName ="View" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"/>
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
        <asp:PlaceHolder ID="pControl" runat="server" Visible="false">
            <asp:HiddenField ID="fldOID" runat="server" />
            <asp:Label ID="lblMessage" runat="server" Text="Ready"></asp:Label>
<hr />
            <table>
                <tr>
                    <td>
                        รหัสพนักงาน
                    </td>
                    <td>
                        <asp:TextBox ID="txtid" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        ชื่อพนักงาน
                    </td>
                    <td>
                        <asp:TextBox ID="txtname" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        รหัสผ่าน
                    </td>
                    <td>
                        <asp:TextBox ID="txtpassword" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        สิทธิการใช้งาน
                    </td>
                    <td>
                        <asp:DropDownList ID="cboRole" runat="server"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        รหัสเขตการขาย
                    </td>
                    <td>
                        <asp:TextBox ID="txtzonecode" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        ชื่อเขตการขาย
                    </td>
                    <td>
                        <asp:TextBox ID="txtzonename" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        ตำแหน่ง
                    </td>
                    <td>
                        <asp:TextBox ID="txtroleName" runat="server"></asp:TextBox>
                    </td>
                </tr>

                <tr>
                    <td>
                        รหัสพนักงานที่รับผิดชอบ
                    </td>
                    <td>
                        <asp:TextBox ID="txtempid" runat="server"></asp:TextBox>
                    </td>
                </tr>
            </table>
<hr />
<asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
<asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click" />
<asp:Button ID="btnClose" runat="server" Text="Close" OnClick="btnClose_Click" />
        </asp:PlaceHolder>
    </div>
    </form>
</body>
</html>
