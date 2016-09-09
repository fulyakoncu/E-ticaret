<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UrunOzellikEkle.aspx.cs"
    Inherits="ETicaret.Yonetim.UrunOzellikEkle" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="~/Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="~/Styles/core.css" rel="stylesheet" type="text/css" />
    <link href="~/Styles/form-elements.css" rel="stylesheet" type="text/css" />
    <link href="~/Styles/toastmessage/toastmessage.css" rel="stylesheet" type="text/css" />
    <script src='<%=ResolveUrl("~/Scripts/jquery-1.6.2.min.js") %>' type="text/javascript"></script>
    <script src='<%=ResolveUrl("~/Scripts/jquery.toastmessage.js") %>' type="text/javascript"></script>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div class="page radw400" style="padding:20px">
        <div class="radsatir">
            <div class="baslik">
                Ürüne Özellik Ekleme</div>
        </div>
        <div class="radsatir">
            <div class="radw100 radleft">
                Kayıt No:</div>
            <div class="radw100 radleft">
                <asp:Label ID="lblID" runat="server" Text="0"></asp:Label></div>
        </div>
        <div class="radsatir">
            <div class="radw100 radleft">
                Özellik Adı</div>
            <div class="radw260 radleft">
                                <asp:DropDownList ID="ddlOzellik" runat="server">
                </asp:DropDownList></div>
        </div>
        <div class="radsatir">
            <div class="radw100 radleft">
            Değer
            </div>
            <div class="radw260 radleft">
                <asp:TextBox ID="txtDeger" runat="server"></asp:TextBox></div>
        </div>
        <div class="radsatir">
            <asp:Button ID="btnSil" CssClass="redbutton" runat="server" Text="Sil" onclick="btnSil_Click" />
            <asp:Button ID="btnYeniKayit" CssClass="bluebutton" runat="server" Text="Yeni Kayıt" 
                onclick="btnYeniKayit_Click" />
            <asp:Button ID="btnKaydet" CssClass="greenbutton" runat="server" Text="Kaydet" 
                onclick="btnKaydet_Click"/>
        </div>
        <div class="radsatir">
            <asp:GridView ID="gvUOzellik" runat="server" AutoGenerateColumns="false" 
                DataKeyNames="ID" onselectedindexchanged="gvUOzellik_SelectedIndexChanged">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:CommandField HeaderStyle-Width="40px" HeaderText="Seç" SelectText="Seç" ShowSelectButton="True" />
                    <asp:BoundField HeaderStyle-Width="35px" DataField="ID" HeaderText="Kayıt No"></asp:BoundField>
                    <asp:BoundField HeaderStyle-Width="150px" DataField="OZELLIK_ADI" HeaderText="Özellik Adı">
                    </asp:BoundField>
                    <asp:BoundField HeaderStyle-Width="150px" DataField="DEGER" HeaderText="Değer"></asp:BoundField>
                </Columns>
                <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
            </asp:GridView>
        </div>
    </div>
    </form>
</body>
</html>
