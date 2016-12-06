<%@ Page Language="C#" EnableViewState="true"  AutoEventWireup="true" CodeBehind="salesentry.aspx.cs" Inherits="shopsales.salesentry1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Sales Entry</title>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>

    <link rel="stylesheet" href="http://code.jquery.com/mobile/1.0.1/jquery.mobile-1.0.1.min.css" />
    <script src="http://code.jquery.com/jquery-1.6.4.min.js"></script>
    <script src="http://code.jquery.com/mobile/1.0.1/jquery.mobile-1.0.1.min.js"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.10.0.min.js" type="text/javascript"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/jquery-ui.min.js" type="text/javascript"></script>
    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/themes/blitzer/jquery-ui.css" rel="Stylesheet" type="text/css"/>
<script type="text/javascript">
    $(function () {
        $("[id$=txtGoods]").val("");
        $("[id$=txtSearch]").autocomplete({
            source: function (request, response) {
                $.ajax({
                    url: '<%=ResolveUrl("~/salesentry.aspx/GetGoods") %>',
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
                $("[id$=txtGoods]").val(i.item.val);
            },
            minLength: 1
        });
    });  
</script>
    <style type="text/css">
        .auto-style1 {
            height: 26px;
        }
    </style>

</head> 
<body>
    <form id="form1" runat="server" data-ajax="false">
    <div data-role="header">
        <h1>Aerosoft Sales-Entry System</h1>
    </div><!-- /header -->
    <div data-role="content" style="width: 100%;overflow:scroll"> 
        <b>
        <asp:Label ID="lblHead" runat="server" Text="Ready"></asp:Label>
        </b>   <hr />
        <table>
        <tr><td>
        <asp:Label ID="lblSalesDate" runat="server" Text="Sales Date" Font-Bold="True" ForeColor="Green"></asp:Label>
        </td><td>            
        <asp:TextBox ID="txtDate" TextMode="Date" runat="server" Width="163px" Font-Bold="True" ForeColor="Green" Enabled="false"></asp:TextBox>
        </td></tr>
        <tr><td>
        <asp:Label ID="Label1" runat="server" Text="Search Goods"></asp:Label>        
        </td><td>
                    <asp:TextBox ID="txtSearch" runat="server" Height="22px" Width="162px" />  
        </td><td>
                    <asp:Button ID="Button1" runat="server" Text="Show" OnClick="Button1_Click"/>
        </td></tr>
        <tr>
            <td>
            <asp:HyperLink ID="linkAddGoods" runat="server" NavigateUrl="~/addGoods.aspx">Add Goods</asp:HyperLink>    
            </td><td>
                <asp:HiddenField ID="txtGoods" runat="server" />        
                <asp:HiddenField ID="txtOID" runat="server" />        
        </td>
        </tr>
        </table>
        <hr />
        <b><asp:Label ID="lblMessage" runat="server" Text="Ready"></asp:Label></b>
        <table>
            <tr>
                <td>
                    <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click"/>
                </td>
                <td>
                    <asp:Button ID="btnReset" runat ="server" Text="Clear" OnClick="btnReset_Click" />
                </td>
                <td>
                    <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click"/>
                </td>
            </tr>
        </table>                
        <table>
        <tr><td><asp:Label ID="Label7" runat="server" Text="Sales Type" Font-Bold="True" ForeColor="Blue"></asp:Label></td>
        <td>
        <asp:DropDownList ID="cbosalesType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cbosalesType_SelectedIndexChanged" Width="187px" Font-Bold="True" ForeColor="Blue"></asp:DropDownList>    
        </td></tr>
        <tr><td>
            <asp:CheckBox ID="chkDiscouht" Text="Cash Discount" runat="server" Visible="False" />
        </td><td>
        <asp:TextBox ID="txtsalesDiscountPerc" runat="server" TextMode="Number" Width="52px" Font-Bold="True" ForeColor="Blue"></asp:TextBox>
        </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr><td>
        <asp:Label ID="Label3" runat="server" Text="Model" Font-Bold="True" ForeColor="#CC0000"></asp:Label>
        </td><td>
        <asp:TextBox ID="txtModel" runat="server" Width="188px" Font-Bold="True" ForeColor="#CC0000" />
        </td></tr>
        <tr><td>
        <asp:Label ID="Label10" runat="server" Text="color" Font-Bold="True" ForeColor="Red"></asp:Label>
        </td><td>        
        <asp:DropDownList ID="cboColor" runat="server" Font-Bold="True" ForeColor="Red"></asp:DropDownList>        
        </td></tr>
        <tr><td>                
        <asp:Label ID="Label2" runat="server" Text="Size" Font-Bold="True" ForeColor="Red"></asp:Label>
        </td><td>        
        <asp:TextBox ID="txtSize" TextMode="Number" runat="server" Width="67px" Font-Bold="True" ForeColor="Red"></asp:TextBox>        
        </td></tr>
        <tr><td class="auto-style1">
        <asp:Label ID="lblTagPrice" runat="server" Text="Tag Price" Font-Bold="True" ForeColor="Blue" Visible="False"></asp:Label>
        </td><td class="auto-style1">
        <asp:TextBox ID="txtsalesTagPrice" runat="server" Width="99px" Font-Bold="True" ForeColor="Blue" Enabled="False" Visible="False"></asp:TextBox>        
        </td></tr>
        <tr><td class="auto-style1">
        <asp:Label ID="Label6" runat="server" Text="Sales Price" Font-Bold="True" ForeColor="Red"></asp:Label>
        </td><td class="auto-style1">
        <asp:TextBox ID="txtsalesBuyPrice" runat="server" Width="76px" Font-Bold="True" ForeColor="Red" Enabled="false"></asp:TextBox>
        </td></tr>
        <tr><td>
        <asp:Label ID="Label4" runat="server" Text="Qty"></asp:Label>
        </td>
        <td>
        <asp:TextBox ID="txtsalesQty" TextMode="Number" Text="1" runat="server" Width="68px"></asp:TextBox>
        </td>
        <td>        
        </td>
        </tr>
        <tr><td>
        <asp:Label ID="Label9" runat="server" Text="Note"></asp:Label>
        </td><td>
        <asp:TextBox ID="txtsalesRemark"  runat="server" Width="216px"></asp:TextBox>
        </td></tr>
        </table>
        <hr />
        <asp:HiddenField ID="txtProdType" runat="server"/>
        <asp:HiddenField ID="txtProdCat" runat="server"/>
        <asp:HiddenField ID="txtProdGroup" runat ="server" />
    </div>
    <div data-role="footer" data-theme="d">
        <h4><asp:Label ID="lblUsername" runat="server" Text="Develop by PK"></asp:Label></h4>
    </div>
    </form>
</body>
</html>
