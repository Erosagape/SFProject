<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="report.aspx.cs" Inherits="shopsales.report" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>aerosoft shop-sales report</title>
    <meta name="HandheldFriendly" content="true" />
    <meta name="viewport"
          content="width=device-width,
                 height=device-height, user-scalable=no" />
    <link rel="stylesheet" href="http://code.jquery.com/mobile/1.0.1/jquery.mobile-1.0.1.min.css" />
    <script src="http://code.jquery.com/jquery-1.6.4.min.js"></script>
    <script src="http://code.jquery.com/mobile/1.0.1/jquery.mobile-1.0.1.min.js"></script>
    <style type="text/css">
        .auto-style1 {
            height: 26px;
        }
        .auto-style2 {
            height: 27px;
        }
    </style>
</head>
<body>
    <div data-role="header">
        <h1>Report Menu</h1>
    </div><!-- /header -->
    <form id="form1" runat="server" data-ajax="false">
    <div data-role="main" class="ui-content" style="width: 100%;overflow:scroll">  
        <b><asp:Label ID="lblShop" runat="server" Text=""></asp:Label></b>
        <asp:DropDownList ID="cboShop" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cboShop_SelectedIndexChanged"></asp:DropDownList>           
        <hr />
        <table>
            <tr>
                <td>
                    <asp:Label ID="lblCustGroup" runat="server" Text="เลือกห้างร้าน"></asp:Label></td>
                <td>
                    <asp:DropDownList ID="cboCustGroup" runat="server"/>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label6" runat="server" Text="Type :"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="cboShoeType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboShoeType_SelectedIndexChanged" Width="125px"></asp:DropDownList>
                </td>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="Category :"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="cboprodGroup" runat="server" Width="150px" Height="16px"></asp:DropDownList>   
                </td>

            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label8" runat="server" Text="ปี"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtYear" runat="server" Width="115px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label9" runat="server" Text="เดือน"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="cboMonth" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboMonth_SelectedIndexChanged"></asp:DropDownList>
                </td>
            </tr>
            <tr><td class="auto-style2">
            <asp:Label ID="lblSalesDate" runat="server" Text="Date From"></asp:Label>
            </td><td class="auto-style2">
            <asp:TextBox ID="txtDateF" TextMode="Date" runat="server" Width="174px"></asp:TextBox>
            </td><td class="auto-style2">
            <asp:Label ID="Label1" runat="server" Text="Date To"></asp:Label>
            </td><td class="auto-style2">
            <asp:TextBox ID="txtDateT" TextMode="Date" runat="server" Width="179px"></asp:TextBox>
            </td></tr>
            <tr>
                <td>
                    <asp:Label ID="Label3" runat="server" Text="Model :"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtModel" runat="server" Width="175px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label4" runat="server" Text="Color :"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtColor" runat="server" Width="126px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label5" runat="server" Text="Size :"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtSize" runat="server" Width="66px"></asp:TextBox>
                </td>
                <td class="auto-style1"><asp:Label ID="Label7" runat="server" Text="ประเภทการขาย :"></asp:Label></td>
                <td class="auto-style1"><asp:DropDownList ID="cbosalesType" runat="server" Width="187px"></asp:DropDownList>    
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label10" runat="server" Text="เค้าท์เตอร์พื้นที่ขาย"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="cboCounter" runat="server"></asp:DropDownList>
                </td>
                <td>
                    <asp:Label ID="Label11" runat="server" Text="ภาค"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="cboArea" runat="server"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label12" runat="server" Text="เซลล์ผู้รับผิดชอบ"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="cboSalesCode" runat="server"></asp:DropDownList>
                </td>
                <td>
                    <asp:Label ID="Label13" runat="server" Text="Supervisor PC"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="cboSupCode" runat="server"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="auto-style1">                    
                    รูปแบบรายงาน
                <td class="auto-style1">
                    <asp:CheckBox ID="ChkSummaryOnly" runat="server" Text="แบบสรุป"/></td>
                <td class="auto-style1">                                        
                    รวมยอดตาม
                    </td>
                <td class="auto-style1">
                    <asp:DropDownList ID="cboSumType" runat="server"></asp:DropDownList></td>
            </tr>
            <tr><td>
                </td>
                <td>
                <asp:Button ID="btnRefresh" runat ="server" Text="Show" OnClick="btnRefresh_Click" />
            </td><td>
                <asp:Button ID="btnExport" runat ="server" Text="Export" OnClick="btnExport_Click" />                                
                </td>
            <td>
                <asp:Button ID="btnPrint" runat="server" Text="Preview" OnClick="btnPrint_Click" />
            </td>
            </tr>
        </table>
        <asp:Button ID="btnBack" runat="server" Text="Back to menu" OnClick="btnBack_Click"/>
    </div>
    </form>
    <div data-role="footer" data-theme="d">
        <h4>
            <asp:Label ID="lblUsername" runat="server" Text="Develop by PK"></asp:Label>
        </h4>
    </div><!-- /footer -->
</body>
</html>
