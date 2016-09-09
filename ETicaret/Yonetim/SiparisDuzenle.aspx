<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SiparisDuzenle.aspx.cs"
    Inherits="ETicaret.Yonetim.SiparisDuzenle1" %>

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
    <div class="page radw560" style="padding: 20px">
        <div class="radsatir">
            <div class="baslik">
                Sipariş Düzenle</div>
        </div>
        <div class="radsatir">
            <div class="radw100 radleft">
                Uye:</div>
            <div class="radw100 radleft">
                <asp:Label ID="lblUye" runat="server" Text=""></asp:Label></div>
        </div>
        <div class="radsatir">
            <div class="radw260 radleft">
                Fatura Adı</div>
            <div class="radw260 radleft">
                Fatura Vergi No</div>
        </div>
        <div class="radsatir">
            <div class="radw260 radleft">
                <asp:TextBox ID="txtFAdi" Width="180" runat="server"></asp:TextBox></div>
            <div class="radw260 radleft">
                <asp:TextBox ID="txtFVergiNo" Width="180" runat="server"></asp:TextBox></div>
        </div>
        <div class="radsatir">
            <div class="radw260 radleft">
                Adres</div>
        </div>
        <div class="radsatir">
            <div class="radw260 radleft">
                <asp:TextBox ID="txtAdres" TextMode="MultiLine" Width="180" runat="server"></asp:TextBox></div>
        </div>
        <div class="radsatir">
            <div class="radw260 radleft">
                Kargo</div>
            <div class="radw260 radleft">
                FKargo Kodu</div>
        </div>
        <div class="radsatir">
            <div class="radw260 radleft">
                <asp:DropDownList ID="ddlKargo" runat="server">
                </asp:DropDownList>
            </div>
            <div class="radw260 radleft">
                <asp:TextBox ID="txtKargoKodu" Width="180" runat="server"></asp:TextBox></div>
        </div>
        <div class="radsatir">
            <div class="radw260 radleft">
                Banka Adı</div>
            <div class="radw260 radleft">
                Sonuc</div>
        </div>
        <div class="radsatir">
            <div class="radw260 radleft">
                <asp:DropDownList ID="ddlBanka" runat="server">
                </asp:DropDownList>
            </div>
            <div class="radw260 radleft">
                <asp:TextBox ID="txtSonuc" TextMode="MultiLine" Width="180" runat="server"></asp:TextBox></div>
        </div>
        <div class="radsatir">
            <div class="radw260 radleft">
                Ödeme Tipi</div>
            <div class="radw260 radleft">
                Sipariş Durumu</div>
        </div>
        <div class="radsatir">
            <div class="radw260 radleft">
                <asp:DropDownList ID="ddlOdemeTipi" runat="server">
                </asp:DropDownList>
            </div>
            <div class="radw260 radleft">
                <asp:DropDownList ID="ddlSiparisDurumu" runat="server">
                </asp:DropDownList>
            </div>
        </div>
        <div class="radsatir">
            <asp:Button ID="btnKaydet" CssClass="greenbutton" runat="server" Text="Kaydet" OnClick="btnKaydet_Click" />
        </div>
        <div class="radsatir">
            <asp:GridView ID="gvSiparisDetaylar" runat="server" AutoGenerateColumns="false" DataKeyNames="ID">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:BoundField DataField="URUNID" HeaderText="Ürün No" />
                    <asp:BoundField DataField="MARKA_ADI" HeaderText="Marka" />
                    <asp:BoundField DataField="URUN_ADI" HeaderText="Ürün" />
                    <asp:BoundField DataField="MIKTAR" HeaderText="Miktar" />
                    <asp:BoundField DataField="TUTAR" HeaderText="Tutar" />
                </Columns>
                <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
            </asp:GridView>
        </div>
        <div class="radsatir">
            <asp:Button ID="btnCikisYap" CssClass="bluebutton" runat="server" Text="Ürün Çıkışlarını Yap" OnClick="btnCikisYap_Click" />
        </div>
    </div>
    </form>
</body>
</html>
