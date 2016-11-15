<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="deliveryhd.aspx.cs" Inherits="sfdelivery.deliveryhd" %>

<%@ Register Assembly="DevExpress.Web.v15.2, Version=15.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Delivery Data</title>
    <script type="text/javascript">
		function OnFocusedRowChanged(s, e) {
		    grid.GetRowValues(grid.GetFocusedRowIndex(), 'เลขที่เอกสาร;วันที่ออกเอกสาร', OnGetRowValues);
		}

		function OnGetRowValues(values) {
		    var id = values[0];
		    var dt = values[1];
		    txtID.SetText(id);
		    txtDate.SetText(dt);
		}
	</script>
    <script src="http://code.jquery.com/jquery-1.6.4.min.js"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.10.0.min.js" type="text/javascript"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/jquery-ui.min.js" type="text/javascript"></script>
    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/themes/blitzer/jquery-ui.css" rel="Stylesheet" type="text/css"/>
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
        <div runat="server">
    <script type="text/javascript">
        $(function () {
        $("[id$=txtCustSearch]").autocomplete({
            source: function (request, response) {
                $.ajax({
                    url: '<%=ResolveUrl("~/deliveryhd.aspx/GetCustomer") %>',
                    data: "{ 'searchstr': '" + request.term + "'}",
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {
                        response($.map(data.d, function (item) {
                            return {
                                val:item,
                                label: item.split('|')[0] +":"+ item.split('|')[1]
                            }
                        }))
                    },
                    error: function (response) {
                        //alert(response.responseText);
                    },
                    failure: function (response) {
                        //alert(response.responseText);
                    }
                });
            },
            select: function (e, i) {
                $("[id$=txtCustCode]").val(i.item.label.split(':')[0]);
                $("[id$=txtCustName]").val(i.item.label.split(':')[1]);
            },
            minLength: 1
        });
    });  
</script>
        </div>
    <asp:HyperLink ID="HyperLink1" Font-Bold="true" ForeColor="ForestGreen" NavigateUrl="~/xmltools.aspx" runat="server">Menu</asp:HyperLink> &nbsp;>>
    <asp:Label ID="Label1" runat="server" Font-Bold="true" ForeColor="ForestGreen" Text="ข้อมุลใบส่งสินค้า (Delivery)"></asp:Label>
<hr />
    <div>
    <table style="width:100%">
        <tr>
            <td>พนักงานขาย</td>
            <td>
                <asp:TextBox ID="txtSalesCode" runat="server"></asp:TextBox></td>
            <td>
                <asp:Label ID="Label2" runat="server" ForeColor="Red" Text="ค้นหาลูกค้า"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtCustSearch" ForeColor="Red" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>รหัสลูกค้า</td>
            <td>
                <asp:TextBox ID="txtCustCode" runat="server" AutoPostBack="true" OnTextChanged="txtCustCode_TextChanged"></asp:TextBox></td>
            <td>ชื่อลูกค้า</td>
            <td>
                <asp:TextBox ID="txtCustName" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label3" runat="server" Text="วันที่ส่งของ" ForeColor="red"></asp:Label></td>
            <td>
                <asp:TextBox ID="txtDeliverDate" runat="server" ForeColor="red"></asp:TextBox></td>
            <td>ชื่อคนขับ</td>
            <td>
                <asp:TextBox ID="txtDriver" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>สถานที่ส่ง</td>
            <td>
                <asp:TextBox ID="txtDeliverTo" runat="server"></asp:TextBox></td>
            <td>ผู้ขนส่ง</td>
            <td>
                <asp:TextBox ID="txtTransport" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>เลขที่ใบสั่งขาย</td>
            <td>
                <asp:TextBox ID="txtSNo" runat="server"></asp:TextBox></td>
            <td>เลขที่ใบส่งของ</td>
            <td>
                <asp:TextBox ID="txtINo" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>
                สถานะการขนส่ง
            </td>
            <td>
                <asp:CheckBox ID="CheckBox1" Text="ขนส่งแล้ว" runat="server" Checked="true" />
                <asp:CheckBox ID="CheckBox2" Text="ยังไม่ขนส่ง" runat="server" Checked="true"/>
            </td>
            <td>
                สถานะการส่งของ
            </td>
            <td>
                <asp:RadioButtonList ID="RadioButtonList1" runat="server" Width="172px">
                    <asp:ListItem Selected="True" Value="0">ทั้งหมด</asp:ListItem>
                    <asp:ListItem Value="1">ยังไม่มีวันส่งของ</asp:ListItem>
                    <asp:ListItem Value="2">มีวันส่งของแล้ว</asp:ListItem>
                    <asp:ListItem Value="3">บันทึกสถานะแล้ว</asp:ListItem>
                </asp:RadioButtonList>
            &nbsp;<asp:DropDownList ID="cboStatus" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboStatus_SelectedIndexChanged" Width="159px">
                </asp:DropDownList>
            </td>
        </tr>
    </table>
<p style="background-color:lightblue;text-align:left;align-content:center">
   <asp:Button ID="btnSearch" runat="server" Text="ค้นหาตามเงื่อนไข" OnClick="btnSearch_Click" Width="116px" />      
    <asp:CheckBox ID="ChkAllData" runat="server" ForeColor="Red" Text="ค้นหาจากข้อมูลทั้งหมด" Checked="true" />
</p>
<table>
    <tr>
        <td>
            รายการที่เลือก <dx:ASPxTextBox ID="txtID" ClientInstanceName="txtID" runat="server" Width="170px"></dx:ASPxTextBox>
        </td>
        <td>
            วันที่ <dx:ASPxTextBox ID="txtDate" ClientInstanceName="txtDate" runat="server" Width="170px"></dx:ASPxTextBox>        
        </td>
        <td>
            <asp:Button ID="Button2" runat="server" Text="ดูรายละเอียด" OnClick="Button2_Click" />
        </td>
        <td>
<asp:Label ID="lblMessage" runat="server" Text="Ready"></asp:Label>                   
        </td>
    </tr>
</table>
    <p>
        <dx:ASPxGridView ID="GridView1" runat="server" EnableTheming="True" Theme="Office2003Blue" OnDataBinding="GridView_DataBinding" 
            OnCustomButtonCallBack="GridView_CustomButtonCallback" 
            ClientInstanceName="grid">
            <SettingsPager PageSize="15">
            </SettingsPager>
            <SettingsBehavior AllowFocusedRow="True"/>
            <Styles><Cell Wrap="False"></Cell></Styles>
            <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />
            <SettingsSearchPanel Visible="True" />
            <ClientSideEvents FocusedRowChanged="OnFocusedRowChanged" />
        </dx:ASPxGridView>
</p>
    </div>
<hr />        
<table>
    <tr>
        <td>
    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Download Data" Width="116px" />
        </td>
        <td>
<asp:Label ID="lblfileName" runat="server" Text=""></asp:Label>
        </td>
    </tr>
</table>            
    </form>
</body>
</html>
