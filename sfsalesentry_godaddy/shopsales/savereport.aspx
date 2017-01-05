<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="savereport.aspx.cs" Inherits="shopsales.savereport" %>

<%@ Register Assembly="DevExpress.Web.v15.2, Version=15.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.ASPxPivotGrid.v15.2, Version=15.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxPivotGrid" TagPrefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>รายงานยอดขาย</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click"><asp:Image ID="Image1" ImageUrl="~/lineit.png" runat="server"/></asp:LinkButton>
    <div>
        รหัสรายงาน : <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
        <br />
        <dx:ASPxGridView ID="ASPxGridView1" runat="server" EnableTheming="True" Theme="Office2010Blue" OnHtmlDataCellPrepared="ASPxGridView1_HtmlDataCellPrepared" OnDataBinding="ASPxGridView1_DataBinding">
            <Styles><Cell Wrap="false"/></Styles>
            <SettingsPager Mode="ShowAllRecords">
            </SettingsPager>
            <SettingsBehavior AllowFocusedRow="True" />
            <SettingsSearchPanel Visible="True" />
        </dx:ASPxGridView>
    </div>
        <asp:Button ID="btnDelete" runat="server" Text="ลบข้อมูลรายงานนี้" OnClick="btnDelete_Click" />
    </form>
</body>
</html>
