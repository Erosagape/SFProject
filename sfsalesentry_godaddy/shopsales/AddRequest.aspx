<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddRequest.aspx.cs" Inherits="shopsales.addrequest" %>

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
    <script type = "text/javascript">
    $(function () {
        $("[id$=txtModel]").autocomplete({
            source: function (request, response) {
                $.ajax({
                    url: '<%=ResolveUrl("~/AddRequest.aspx/GetGoods") %>',
                    data: "{ 'prefix': '" + request.term + "'}",
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {
                        response($.map(data.d, function (item) {
                            return {
                                label: item.split('|')[0],
                                val: item.split('|')[1]
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
                $("[id$=txtModel]").val(i.item.label);
                $("[id$=lblMessage]").value(i.item.val);
            },
            minLength: 1
        });
    });  
    function ConfirmDelete() {
        var confirm_value = document.createElement("INPUT");
        confirm_value.type = "hidden";
        confirm_value.name = "confirm_value";
        if (confirm("Save Data, Confirm?")) {
            confirm_value.value = "Yes";
        } else {
            confirm_value.value = "No";
        }
        document.forms[0].appendChild(confirm_value);
    }
    function setColorByName(value,id) {
        var select = document.getElementById(id);
        var options = select.options;
        for (var i = 0, len = options.length; i < len; i++) {
            if (options[i].text === value) {
                select.selectedIndex = i;
                return true; //Return so it breaks the loop and also lets you know if the function found an option by that value
            }
        }
        return false; //Just to let you know it didn't find any option with that value.
    }
    function SelectGoods(idx) {
        var popup;
        popup = window.open("selectgoods.aspx?returnto=" + idx, "Popup");
        popup.focus();
    }
    function doPostBack(val,idx) {
        var model = val.split(' ')[0].trim();
        var color = val.split(' ')[1].trim();
        var size = val.split(' ')[2].trim();
                        
        $('#fldD_ModelCode' + idx).val(model);
        $('#fldD_SizeNo' + idx).val(size);
        setColorByName(color, 'fldD_Color' + idx);
        $('#fldD_Color' + idx).selectmenu('refresh');
        return false;
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
        Transaction Date 
            <table>
            <tr>
                <td>
                    <asp:TextBox ID="txtDate" TextMode="date" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btnShow" runat="server" Text="Show by Selected Data" OnClick="btnShow_Click" />
                </td>
                <td>
                    <asp:CheckBoxList ID="chkShowReceived" runat="server" AutoPostBack="True" OnSelectedIndexChanged="chkShowReceived_SelectedIndexChanged">
        <asp:ListItem>Display Delivered</asp:ListItem>
    </asp:CheckBoxList>
                </td>
            </tr>
            </table>
<hr />
        <table>
            <tr>                    
                <td>
                    <asp:TextBox ID="txtModel" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btnLoadGoods" runat="server" Text="Load Data" OnClick="btnLoadGoods_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnConfirm" runat="server" Text="Confirm" OnClick="btnConfirm_Click" />                    
                </td>
            </tr>
        </table>
        <asp:Panel ID="Panel1" runat="server">
        </asp:Panel>
        <table>
        <tr>
            <td>
                <asp:Button ID="btnAddRow" runat="server" Text="Add New" OnClick="btnAddRow_Click" />
            </td>
            <td>
                <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
            </td>
            <td>
                <asp:Label ID="lblMessage" runat="server" Text="Ready"></asp:Label>
            </td>
        </tr>
        </table>
        <asp:Button ID="btnReturn" runat="server" Text="Back to List" OnClick="btnReturn_Click" />
        <asp:Button ID="btnBack" runat="server" Text="Return To Menu" OnClick="btnBack_Click" />
    </div>
    <div data-role="footer" data-theme="d">
        <h4><asp:Label ID="lblUsername" runat="server" Text="Develop by PK"></asp:Label></h4>
    </div><!-- /footer -->
    </form>
</body>
</html>
