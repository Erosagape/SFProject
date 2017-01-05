<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SendStock.aspx.cs" Inherits="shopsales.SendStock" %>

<%@ Register Assembly="DevExpress.Web.v15.2, Version=15.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Add Stock</title>
<meta name="viewport" content="width=device-width, initial-scale=1"/>
    <link rel="stylesheet" href="http://code.jquery.com/mobile/1.0.1/jquery.mobile-1.0.1.min.css" />
    <script src="http://code.jquery.com/jquery-1.6.4.min.js"></script>
    <script src="http://code.jquery.com/mobile/1.0.1/jquery.mobile-1.0.1.min.js"></script>    
    <script type = "text/javascript">
        function ConfirmDelete() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("กรุณายืนยันการทำรายการนี้")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
        function SelectGoods(idx) {
            var popup;
            popup = window.open("selectgoods.aspx?returnto=" + idx, "Popup");
            popup.focus();
        }
        function setColorByName(value, id) {
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
        function doPostBack(val, idx) {
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
        <h1>Add Stock To POS</h1>
    </div>
      <div data-role="main" class="ui-content" style="width: 100%;overflow:scroll">   
        <form id="form1" runat="server" data-ajax="false">
        <asp:DropDownList ID="cboShop" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cboShop_SelectedIndexChanged"></asp:DropDownList>          
        <asp:PlaceHolder ID="placeHeader" runat="server">
        <table>
            <tr>
                <td>
                    ประจำวันที่ : 
            <asp:TextBox ID="txtDate" runat="server" TextMode="date"></asp:TextBox>
                </td>
                <td>
            <asp:Button ID="btnShow" runat="server" Text="แสดงข้อมูล" OnClick="btnShow_Click" style="height: 26px" />
                </td>
                <td>
            <asp:CheckBox ID="chkShowAll" runat="server" Text="แสดงทั้งหมด" AutoPostBack="true" OnCheckedChanged="chkShowAll_CheckedChanged" />
                </td>
            </tr>                
            <tr>
                <td>
        <asp:DropDownList ID="cboStatus" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cboStatus_SelectedIndexChanged" ></asp:DropDownList>
                </td>
                <td>
            <asp:Button ID="btnEdit" runat="server" Text="ดูสินค้าปกติ" OnClick="btnEdit_Click" style="height: 26px" />
                </td>
                <td>
            <asp:Button ID="btnEdit2" runat="server" Text="ดูสินค้า One Price" OnClick="btnEdit2_Click" style="height: 26px" />
                </td>
            </tr>
            </table>
<hr />
            <dx:ASPxGridView ID="ASPxGridView1" runat="server" OnDataBinding="ASPxGridView1_DataBinding">
            <SettingsPager Mode="ShowAllRecords" Visible="False">
            </SettingsPager>
            <SettingsBehavior AllowSelectByRowClick="True" AllowSelectSingleRowOnly="True" AllowSort="False" AutoExpandAllGroups="True" SortMode="DisplayText" />
            <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />
        </dx:ASPxGridView>
<hr />
       
            </asp:PlaceHolder>
            <asp:PlaceHolder ID="placeDetail" runat="server" Visible="false">
                <table>
                    <tr>
                        <td>
                    <asp:DropDownList ID="cboType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cboType_SelectedIndexChanged" ></asp:DropDownList>
                        </td>
                        <td>
                            <asp:Button ID="btnCheck" runat="server" Text="ตรวจสอบราคา" OnClick="btnCheck_Click" />
                        </td>
                        <td>                            
                            <asp:Button ID="btnApprove" runat="server" Text="อนุมัติรายการ" OnClick="btnApprove_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label1" runat="server" Text="วันที่ส่งของ"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtStockDate" runat="server" TextMode="date" ></asp:TextBox>
                        </td>
                        <td>
                            <asp:Button ID="btnSetDate" runat="server" Text="กำหนดวันส่งของ" OnClick="btnSetDate_Click"  />
                        </td>
                    </tr>
                </table>
                <hr />
                <asp:Panel ID="Panel1" runat="server"></asp:Panel>
                <table>
                    <tr>
                        <td>
                            <asp:Button ID="btnSave" runat="server" Text="บันทึกรายการ" OnClick="btnSave_Click" />
                        </td>
                        <td>
                            <asp:Button ID="btnAdd" runat="server" Text="เพิ่มรายการ" OnClick="btnAdd_Click" />
                        </td>
                        <td>
                            <asp:Label ID="lblMessage" runat="server" Text="Ready"></asp:Label>
                        </td>
                    </tr>
                </table>
            <asp:Button ID="btnHide" runat="server" Text="ย้อนกลับหน้ารายการ" OnClick="btnHide_Click"/>
            </asp:PlaceHolder>
            <asp:Button ID="btnBack" runat="server" Text="กลับสู่เมนูหลัก" OnClick="btnBack_Click" />
        </form>
    </div>
    <div data-role="footer" data-theme="d">
        <h4><asp:Label ID="lblUsername" runat="server" Text="Develop by PK"></asp:Label></h4>
    </div><!-- /footer -->
</body>
</html>
