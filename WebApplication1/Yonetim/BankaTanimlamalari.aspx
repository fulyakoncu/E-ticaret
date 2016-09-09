<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BankaTanimlamalari.aspx.cs" Inherits="ETicaret.Yonetim.BankaTanimlamalari" %>
<%@ Import Namespace="MiniCore" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript">
        function changeCheckboxText(checkbox) {
            if (checkbox.checked)
                checkbox.nextSibling.innerHTML = 'Aktif';
            else
                checkbox.nextSibling.innerHTML = 'Pasif';
        }
    </script>

    <div class="radsatir">
        <div class="baslik">Banka Tanımlamaları</div>
    </div>
    <div class="radsatir">
        <div class="radw100 radleft">Kayıt No:</div>
        <div class="radw100 radleft"><asp:Label ID="lblID" runat="server" Text="0"></asp:Label></div>
    </div>
    <div class="radsatir">
        <div class="radw260 radleft">Banka Adı</div>
        <div class="radw260 radleft">Sanal Pos Host Adresi</div>
    </div>
    <div class="radsatir">
        <div class="radw260 radleft"><asp:TextBox ID="txtBAdi" Width="180" runat="server"></asp:TextBox></div>
        <div class="radw260 radleft"><asp:TextBox ID="txtHost" Width="180" runat="server"></asp:TextBox></div>
    </div>
    <div class="radsatir">
        <div class="radw260 radleft">Şifre</div>
        <div class="radw260 radleft">Kullanıcı Adı</div>
    </div>
    <div class="radsatir">
        <div class="radw260 radleft"><asp:TextBox ID="txtSifre" Width="180" runat="server"></asp:TextBox></div>
        <div class="radw260 radleft"><asp:TextBox ID="txtKAdi" Width="180" runat="server"></asp:TextBox></div>
    </div>
    <div class="radsatir">
        <div class="radw160 radleft">Aktif</div>
        <div class="radw160 radleft">Taksitli Çekim</div>
                <div class="radw160 radleft">Mağza No</div>
    </div>
    <div class="radsatir">
        <div class="radw160 radleft"><asp:CheckBox ID="chcAktif" runat="server" Text="Pasif" onclick="changeCheckboxText(this);" /></div>
        <div class="radw160 radleft"><asp:CheckBox ID="chcTaksit" runat="server" Text="Pasif" onclick="changeCheckboxText(this);" /></div>
                <div class="radw160 radleft"><asp:TextBox ID="txtMNo" Width="140" runat="server"></asp:TextBox></div>
    </div>
    <div class="radsatir">
        <asp:Button ID="btnSil" CssClass="redbutton" runat="server" Text="Sil" OnClick="btnSil_Click" />
        <asp:Button ID="btnYeniKayit" CssClass="bluebutton" runat="server" Text="Yeni Kayıt" OnClick="btnYeniKayit_Click" />
        <asp:Button ID="btnKaydet" CssClass="greenbutton" runat="server" Text="Kaydet" OnClick="btnKaydet_Click" />
    </div>
    <div class="radsatir">
        <asp:GridView ID="gvBankalar" runat="server" AutoGenerateColumns="false" DataKeyNames="ID" OnSelectedIndexChanged="gvBankalar_SelectedIndexChanged">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:CommandField HeaderStyle-Width="40px" HeaderText="Seç" SelectText="Seç" ShowSelectButton="True" />
                <asp:BoundField HeaderStyle-Width="35px" DataField="ID" HeaderText="Kayıt No">
                </asp:BoundField>
                <asp:BoundField HeaderStyle-Width="100px" DataField="BANKA_ADI" HeaderText="Banka Adı">
                </asp:BoundField>
                <asp:BoundField HeaderStyle-Width="100px" DataField="KULLANICI_ADI" HeaderText="Kullanıcı Adı">
                </asp:BoundField>
                <asp:BoundField HeaderStyle-Width="100px" DataField="SIFRE" HeaderText="Şifre">
                </asp:BoundField>
                <asp:BoundField HeaderStyle-Width="100px" DataField="MAGAZA_NO" HeaderText="Mağaza No">
                </asp:BoundField>
                <asp:TemplateField HeaderStyle-Width="50px" HeaderText="Durumu">
                    <ItemTemplate>
                        <%# MiniCore.cAraclar.GetDescription((MiniCore.eAktifDurum)DataBinder.Eval(Container.DataItem,"AKTIF").ToShort(0)) %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-Width="50px" HeaderText="Taksitli">
                    <ItemTemplate>
                        <%# MiniCore.cAraclar.GetDescription((MiniCore.eAktifDurum)DataBinder.Eval(Container.DataItem,"TAKSIT").ToShort(0)) %>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
            <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
        </asp:GridView>
    </div>

</asp:Content>
