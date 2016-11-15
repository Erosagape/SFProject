<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="selectgoods.aspx.cs" Inherits="shopsales.selectgoods" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Select Goods</title>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <link rel="stylesheet" href="http://code.jquery.com/mobile/1.0.1/jquery.mobile-1.0.1.min.css" />
    <script src="http://code.jquery.com/jquery-1.6.4.min.js"></script>
    <script src="http://code.jquery.com/mobile/1.0.1/jquery.mobile-1.0.1.min.js"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.10.0.min.js" type="text/javascript"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/jquery-ui.min.js" type="text/javascript"></script>
    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/themes/blitzer/jquery-ui.css" rel="Stylesheet" type="text/css"/>
    <script type="text/javascript">
        $(function () { 
            $.ajax({
                type: "POST",
                url: "selectgoods.aspx/GetGoods",
                data: "{}",
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (msg) {
                    var arr = msg.d;
                    var str = '';
                    $.each(arr, function (index, data) {
                        var code = data.split('|')[1];
                        var name = data.split('|')[0];
                        str += '<li data-theme="c" class="ui-li ui-li-static ui-btn-hover-c ui-btn-up-c"><a href="#" class="ui-link-inherit">' + name + '</a></li>';
                    });

                    $(str).appendTo('#lst').trigger('create');
                    $('#lst li').on('click', function () {
                        //change theme to make selected color visible
                        $('#lst li').attr("data-theme", "c").removeClass("ui-btn-up-b").removeClass('ui-btn-hover-b').addClass("ui-btn-up-c").addClass('ui-btn-hover-c');
                        $(this).attr("data-theme", "b").removeClass("ui-btn-up-c").removeClass('ui-btn-hover-c').addClass("ui-btn-up-b").addClass('ui-btn-hover-b');
                        //return value
                        var val = $(this).text();
                        $('#txt').val(val);
                        $('#lblSelect').text(val);
                    });
                    $('#lst li').on('dblclick', function () {
                        $('#btnSelect').click();
                    });
                },
                error: function (e) {
                    alert("Error " +e.responseText);
                },
                failure: function (response) {
                    alert("Failed " +response.responseText);
                },
                complete: function () {
                    //$('#lst').listview().listview('refresh');
                },
            });
            return false;
        });
    </script>
</head>
<body>
<div data-role="page">
<script type="text/javascript">
    function SetValue() {
        if (window.opener != null && !window.opener.closed) {
            var index = document.getElementById("txtReturnTo").value;
            var value = document.getElementById("txt").value;
            window.opener.doPostBack(value, index);
        }
        window.close();
    }
</script>
<form id="form1" runat="server" data-ajax="false"> 
    <div data-role="header">
        <h1>List of Goods</h1>
    </div>    
    <div data-role="content" class="ui-content">
        <asp:HiddenField ID="txt" runat="server" />
        <table>
            <tr>
                <td>
        <asp:Label ID="lblSelect" runat="server" Text=""></asp:Label>
                </td>
                <td>
        <asp:Button ID="btnSelect" runat="server" Text="Select" OnClientClick="SetValue()" />
                </td>
                <td>
        <asp:Button ID="btnClose" runat="server" Text="Close" OnClientClick="javascript:window.close();" />
                </td>
            </tr>
        </table>
<hr />
        <ul id="lst" data-role="listview" data-inset="true" data-filter="true"
        data-split-icon="star"
        data-icon="check" data-split-theme="c">
        </ul>
        <asp:HiddenField ID="txtReturnTo" runat="server" />
    </div>
</form>    
</div>
</body>
</html>
