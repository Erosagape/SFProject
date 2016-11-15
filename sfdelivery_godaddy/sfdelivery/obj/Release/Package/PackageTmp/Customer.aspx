<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Customer.aspx.cs" Inherits="sfdelivery.Customer" %>

<%@ Register Assembly="DevExpress.Web.v15.2, Version=15.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Search by Customer</title>
    	<script type="text/javascript">
		function OnFocusedRowChanged(s, e) {
			grid.GetRowValues(grid.GetFocusedRowIndex(), 'Custname', OnGetRowValues);
		}

		function OnGetRowValues(values) {
		    var custCode = values;
		    txtCode.SetText(custCode);
		}
	</script>
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
    <asp:Label ID="Label1" runat="server" Font-Bold="true" ForeColor="ForestGreen" Text="ข้อมูลลูกค้าที่เคลื่อนไหว"></asp:Label>
<hr />
ข้อมูลประจำปี/เดือน
<asp:DropDownList ID="cboMonthYear" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cboMonthYear_SelectedIndexChanged"></asp:DropDownList>            
<hr />
    <div>
        <dx:ASPxGridView ID="GridView" runat="server" 
            OnDataBinding="GridView_DataBinding"
            OnCustomButtonCallback="GridView1_CustomButtonCallback"
            EnableTheming="True" Theme="Office2003Blue"            
            ClientInstanceName="grid">
            <SettingsPager PageSize="20">
            </SettingsPager>
            <SettingsBehavior AllowFocusedRow="True" />
            <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />
            <SettingsSearchPanel Visible="True" />
            <ClientSideEvents FocusedRowChanged="OnFocusedRowChanged" />
        </dx:ASPxGridView>
    </div>
ข้อมูลที่เลือกอยู่ :<dx:ASPxTextBox ID="txtCustcode" ClientInstanceName="txtCode" runat="server" Width="204px" Height="27px"></dx:ASPxTextBox>
    </form>
</body>
</html>
