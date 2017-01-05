<%@ Page Language="C#" EnableViewState="true" AutoEventWireup="true" CodeBehind="editsales.aspx.cs" Inherits="shopsales.editsales" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Edit Sales Entry</title>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <link rel="stylesheet" href="http://code.jquery.com/mobile/1.0.1/jquery.mobile-1.0.1.min.css" />
    <script src="http://code.jquery.com/jquery-1.6.4.min.js"></script>
    <script src="http://code.jquery.com/mobile/1.0.1/jquery.mobile-1.0.1.min.js"></script>
</head>
<body>
    <form id="form1" runat="server" data-ajax="false">
    <div data-role="header">
        <h1>Aerosoft Sales-Entry System</h1>
    </div><!-- /header -->
    <div data-role="content"> 
 <b>
        <asp:Label ID="lblHead" runat="server" Text="Ready"></asp:Label>
        </b>   <hr />
        <table>
        <tr>
        <td>
        <asp:Label ID="Label10" runat="server" Text="Row ID" Font-Bold="True" ForeColor="Red"></asp:Label>
        </td>
        <td>
        <asp:TextBox ID="txtOID" runat="server" Width="252px"></asp:TextBox>
        </td>
        </tr>
        <tr>
        <td>
        <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" Width="84px" />
        </td>
        <td>
            <b><asp:Label ID="lblMessage" runat="server" Text="Ready"></asp:Label></b>
        </td>
        </tr>

        </table>
       <hr />
        <table>
            <tr>
                <td>
                    <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click"/>
                </td>
                <td>
                    <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click"/>
                </td>
                <td>
                    <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click"/>
                </td>
            </tr>
        </table>    
        <table>
        <tr><td>
        <asp:Label ID="lblSalesDate" runat="server" Text="วันที่ขาย" Font-Bold="True" ForeColor="Red"></asp:Label>
        </td><td>            
        <asp:TextBox ID="txtSaleDate" TextMode="date" runat="server" Width="137px"></asp:TextBox>
        </td></tr>
        <tr><td>
        <asp:Label ID="Label1" runat="server" Text="Product ID"></asp:Label>      
        </td>
        <td>              
        <asp:TextBox ID="txtProductID" runat ="server" Width="44px"></asp:TextBox>
        </td>
        </tr>
        <tr>
        <td>
        <asp:Label ID="Label2" runat="server" Text="รุ่น " Font-Bold="True" ForeColor="Red"></asp:Label>      
        </td>
        <td>
        <asp:TextBox ID="txtModelCode" runat="server" Width="122px"></asp:TextBox>
        </td></tr>
        <tr><td>
        <asp:Label ID="Label11" runat="server" Text="ชื่อสินค้า"></asp:Label>      
        </td><td>
        <asp:TextBox ID="txtProductName" runat ="server" Width="255px"></asp:TextBox>
        </td>
        </tr>
        <tr>
        <td>
        <asp:Label ID="Label3" runat="server" Text="Color Code" Font-Bold="True" ForeColor="Red"></asp:Label>
        </td>
        <td>
        <asp:TextBox ID="txtColorCode" runat ="server" Width="80px"></asp:TextBox>
        </td>
        </tr>
        <tr>
        <td>
        <asp:Label ID="Label12" runat="server" Text="สี" Font-Bold="True" ForeColor="Red"></asp:Label>
        </td>
        <td>        
        <asp:TextBox ID="txtColorName" runat="server" Width="122px"></asp:TextBox>
        </td>
        </tr>
        <tr>
        <td>
        <asp:Label ID="Label4" runat="server" Text="ขนาด Size" Font-Bold="True" ForeColor="Red"></asp:Label>
        </td>
        <td>
        <asp:TextBox ID="txtSizeNo" runat="server" Width="73px"></asp:TextBox>
        </td>
        </tr>
        <tr>
        <td>
        <asp:Label ID="Label5" runat="server" Text="ประเภทขาย"></asp:Label>
        </td>
        <td>
        <asp:TextBox ID="txtSaleType" runat="server" Width="59px" ></asp:TextBox>
             (1=ไม่ลด 2=One Price 3=ส่วนลด)
        </td>
        </tr>
        <tr>
        <td>
        <asp:Label ID="Label6" runat="server" Text="อัตราส่วนลด  "></asp:Label>
        </td>
        <td>
        <asp:TextBox ID="txtDiscountRate" runat="server" Width="58px"></asp:TextBox>
        </td>
        </tr>
        <tr>
        <td>
        <asp:Label ID="Label7" runat="server" Text="จำนวนขาย "></asp:Label>
        </td>
        <td>
        <asp:TextBox ID="txtSalesQty" runat="server" Width="73px"></asp:TextBox>
        </td>
        </tr>
        <tr>
        <td>
        <asp:Label ID="Label8" runat="server" Text="ราคาตามป้าย"></asp:Label>
        </td>
        <td>
        <asp:TextBox ID="txtTagPrice" runat="server" Width="100px"></asp:TextBox>
        </td>
        </tr>
        <tr>
        <td>
        <asp:Label ID="Label9" runat="server" Text="   ราคาขาย  "></asp:Label>
        </td>
        <td>
        <asp:TextBox ID="txtSalesPrice" runat="server" Width="101px"></asp:TextBox>
        </td>
        </tr>
        <tr>
        <td>
        <asp:Label ID="Label20" runat="server" Text="เค้าท์เตอร์"></asp:Label>
        </td>
        <td>
        <asp:TextBox ID="txtCounterType" runat="server" Width="268px"></asp:TextBox>
        </td>
        </tr>
        <tr>
        <td>
        <asp:Label ID="Label13" runat="server" Text="หมายเหตุ"></asp:Label>
        </td>
        <td>
        <asp:TextBox ID="txtRemark" runat="server" Width="268px"></asp:TextBox>
        </td>
        </tr>
            <tr>
                <td>
                    <asp:Label ID="Label14" runat="server" Text="ประเภท"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtprodType" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label15" runat="server" Text="ชนิดสินค้า"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtprodCat" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label16" runat="server" Text="กลุ่มสินค้า"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtprodGroup" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label18" runat="server" Text="% แชร์ส่วนลด"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtShareDiscount" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label19" runat="server" Text="GPx"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtGPX" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label17" runat="server" Text="บันทึก"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtNote" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label21" runat="server" Text="ประจำภาค"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtArea" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label22" runat="server" Text="เขตการขาย"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtzoneCode" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label23" runat="server" Text="พนักงานขาย"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtsalesCode" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label24" runat="server" Text="PC supervisor"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtsupCode" runat="server"></asp:TextBox>
                </td>
            </tr>
        </table>
    </div>
    <div data-role="footer" data-theme="d">
        <h4><asp:Label ID="lblUsername" runat="server" Text="Develop by PK"></asp:Label></h4>
    </div>
    </form>
</body>
</html>
