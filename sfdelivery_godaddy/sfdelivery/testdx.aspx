<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="testdx.aspx.cs" Inherits="sfdelivery.WebForm1" %>

<%@ Register Assembly="DevExpress.Web.v15.2, Version=15.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Customer Data</title>
	<script type="text/javascript">
		function OnFocusedRowChanged(s, e) {
			grid.GetRowValues(grid.GetFocusedRowIndex(), 'customer;CustomerName', OnGetRowValues);
		}

		function OnGetRowValues(values) {
		    var custName = values[1];
		    var custCode = values[0];
		    txtName.SetText(custName);
		    txtCode.SetText(custCode);
		}
	</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <dx:ASPxGridView ID="GridView1" runat="server" 
            OnDataBinding="GridView1_DataBinding" 
            EnableTheming="True" Theme="Glass" 
            ClientInstanceName="grid" 
            OnCustomButtonCallback="GridView1_CustomButtonCallback">
            <SettingsPager PageSize="20">
            </SettingsPager>
            <SettingsBehavior AllowFocusedRow="True"/>
            <SettingsSearchPanel Visible="True" />            
            <ClientSideEvents FocusedRowChanged="OnFocusedRowChanged" />
        </dx:ASPxGridView>
    </div>
        <asp:Label ID="lblMessage" runat="server" Text="Ready"></asp:Label>
<hr />
<table>
    <tr>
        <td>รหัส</td>
        <td>
            <dx:ASPxTextBox ID="txtCustomerCode" ClientInstanceName="txtCode" runat="server" Width="170px"></dx:ASPxTextBox>
        </td>
    </tr>
    <tr>
        <td>ชื่อ</td>
        <td>
            <dx:ASPxTextBox ID="txtCustomerName" ClientInstanceName="txtName" runat="server" Width="170px"></dx:ASPxTextBox>
        </td>
    </tr>
</table>
        <asp:Button ID="Button1" runat="server" Text="Save Customer" OnClick="Button1_Click"/>
    </form>
</body>
</html>
