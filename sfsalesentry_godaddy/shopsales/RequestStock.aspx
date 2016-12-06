<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RequestStock.aspx.cs" Inherits="shopsales.RequestStock" %>

<%@ Register assembly="DevExpress.Web.v15.2, Version=15.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Factory Order</title>
<meta name="viewport" content="width=device-width, initial-scale=1"/>
    <link rel="stylesheet" href="http://code.jquery.com/mobile/1.0.1/jquery.mobile-1.0.1.min.css" />
    <script src="http://code.jquery.com/jquery-1.6.4.min.js"></script>
    <script src="http://code.jquery.com/mobile/1.0.1/jquery.mobile-1.0.1.min.js"></script>    
    <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.10.0.min.js" type="text/javascript"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/jquery-ui.min.js" type="text/javascript"></script>
    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/themes/blitzer/jquery-ui.css" rel="Stylesheet" type="text/css"/>
    <script type="text/javascript">
        function ConfirmApprove() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Saving Data,Confirm?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>
</head>
<body>
    <div data-role="header">
        <h1>
    <asp:Label ID="lblShopName" runat="server" Text="Request Product"></asp:Label></h1>
    </div><!-- /header -->    
    <form id="form1" runat="server" data-ajax="false"> 
    <div data-role="main" class="ui-content" style="width: 100%;overflow:scroll">   
        Request Date 
            <table>
            <tr>
                <td>
                    <asp:TextBox ID="txtDate" TextMode="date" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btnShow" runat="server" Text="Show Data" OnClick="btnShow_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:CheckBoxList ID="chkShowReceived" runat="server" AutoPostBack="True" OnSelectedIndexChanged="chkShowReceived_SelectedIndexChanged" >
        <asp:ListItem>Show Delivered</asp:ListItem>
    </asp:CheckBoxList>
                </td>
            </tr>
            </table>
<hr />
<table>
    <tr>
        <td>
            <asp:DropDownList ID="cboProdType" runat="server"></asp:DropDownList>
        </td>
        <td>
            <asp:Button ID="btbAdd" runat="server" Text="Add Data" OnClick="btbAdd_Click" />            
        </td>
        <td>
            <asp:Button ID="btnEdit" runat="server" Text="Edit Data" OnClick="btnEdit_Click"/>            
        </td>       
    </tr>
</table>
        <dx:ASPxGridView ID="ASPxGridView1" runat="server" OnDataBinding="ASPxGridView1_DataBinding" OnCustomButtonCallback="ASPxGridView1_CustomButtonCallback">
            <ClientSideEvents RowClick="function(s, e) {
s.SelectRowOnPage(e.visibleIndex,!s.IsRowSelectedOnPage(e.visibleIndex)) 	
}" />
            <SettingsPager Mode="ShowAllRecords">
            </SettingsPager>
            <SettingsBehavior  AllowSelectByRowClick="false" AllowSort="False" AutoExpandAllGroups="True" SortMode="DisplayText" />
            <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />
        </dx:ASPxGridView>    
<table>
    <tr>
        <td>
<asp:Button ID="btnConfirm" runat="server" Text="Confirm ALL" OnClick="btnConfirm_Click" OnClientClick="ConfirmApprove()" />                    
        </td>
    </tr>
</table>
<asp:Button ID="btnBack" runat="server" Text="Return To Menu" OnClick="btnBack_Click" />
    </div>
    <div data-role="footer" data-theme="d">
        <h4><asp:Label ID="lblUsername" runat="server" Text="Develop by PK"></asp:Label></h4>
    </div><!-- /footer -->
    </form>
</body>
</html>
