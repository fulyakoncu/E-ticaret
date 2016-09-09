<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="KurumTanimlamalari.aspx.cs" Inherits="ETicaret.Yonetim.GenelTanimlamalar" %>

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
        <div class="baslik">
            Kurum Tanımlamaları</div>
    </div>
    <div class="radsatir">
    <div class="radw120 radleft">Kayıt No</div>
    <div class="radw120 radleft"><asp:Label ID="lblID" runat="server" Text="0"></asp:Label></div>
    </div>
    <div class="radsatir">
        <div class="radw260 radleft">
            Kurum Adı</div>
        <div class="radw260 radleft">
            Adres</div>
    </div>
    <div class="radsatir">
        <div class="radw260 radleft">
            <asp:TextBox ID="txtKurumAdi" Width="180" runat="server"></asp:TextBox></div>
        <div class="radw260 radleft">
            <asp:TextBox ID="txtAdres" TextMode="MultiLine" Width="180" runat="server"></asp:TextBox></div>
    </div>
    <div class="radsatir">
        <div class="radw260 radleft">
            Telefon</div>
        <div class="radw260 radleft">
            Fax</div>
    </div>
    <div class="radsatir">
        <div class="radw260 radleft">
            <asp:TextBox ID="txtTelefon" Width="180" runat="server"></asp:TextBox></div>
        <div class="radw260 radleft">
            <asp:TextBox ID="txtFax" Width="180" runat="server"></asp:TextBox></div>
    </div>
    <div class="radsatir">
        <div class="radw260 radleft">
            Vergi Dairesi</div>
        <div class="radw260 radleft">
            Vergi No</div>
    </div>
    <div class="radsatir">
        <div class="radw260 radleft">
            <asp:TextBox ID="txtVDairesi" Width="180" runat="server"></asp:TextBox></div>
        <div class="radw260 radleft">
            <asp:TextBox ID="txtVNo" Width="180" runat="server"></asp:TextBox></div>
    </div>
    <div class="radsatir">
        <div class="radw260 radleft">
            Ticaret Sicil No</div>
        <div class="radw260 radleft">
            SMTP Adres\Port</div>
    </div>
    <div class="radsatir">
        <div class="radw260 radleft">
            <asp:TextBox ID="txtTicSicilNo" Width="180" runat="server"></asp:TextBox></div>
        <div class="radw260 radleft">
            <asp:TextBox ID="txtSMTPAdres" Width="150" runat="server"></asp:TextBox>
            <asp:TextBox
                ID="txtSMTPPort" Width="30" runat="server"></asp:TextBox></div>
    </div>
    <div class="radsatir">
        <div class="radw260 radleft">
            Mail Adres\Şifreo</div>
        <div class="radw260 radleft">
            Aktif</div>
    </div>
    <div class="radsatir">
        <div class="radw260 radleft">
            <asp:TextBox ID="txtMail" Width="120" runat="server"></asp:TextBox>
            <asp:TextBox ID="txtMailSifre"
                Width="60" runat="server"></asp:TextBox></div>
        <div class="radw260 radleft">
            <asp:CheckBox ID="chcAktif" runat="server" Text="Pasif" onclick="changeCheckboxText(this);" />
        </div>
    </div>
    <div class="radsatir">
            <asp:Button ID="btnSil" CssClass="redbutton" runat="server" Text="Sil" onclick="btnSil_Click" />
        <asp:Button ID="btnYeniKayit" CssClass="bluebutton" runat="server" Text="Yeni Kayıt" 
            onclick="btnYeniKayit_Click" />
        <asp:Button ID="btnKaydet" CssClass="greenbutton" runat="server" Text="Kaydet" 
            onclick="btnKaydet_Click" />
    </div>
    <div class="radsatir">
        <asp:GridView ID="gvKurumlar" AutoGenerateColumns="false" DataKeyNames="ID" runat="server"
            onselectedindexchanged="gvKurumlar_SelectedIndexChanged">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:CommandField HeaderStyle-Width="40px" HeaderText="Seç" SelectText="Seç" ShowSelectButton="True" />
                <asp:BoundField HeaderStyle-Width="35px" DataField="ID" HeaderText="Kayıt No" >
<HeaderStyle Width="20px"></HeaderStyle>
                </asp:BoundField>
                <asp:BoundField HeaderStyle-Width="200px" DataField="KURUM_ADI" 
                    HeaderText="Kurum Adı" >
<HeaderStyle Width="200px"></HeaderStyle>
                </asp:BoundField>
                                <asp:BoundField HeaderStyle-Width="200px" DataField="TELEFON" 
                    HeaderText="Telefon" >
<HeaderStyle Width="200px"></HeaderStyle>
                </asp:BoundField>
                <asp:TemplateField HeaderStyle-Width="50px" HeaderText="DURUMU">
                    <ItemTemplate>
                        <%# MiniCore.cAraclar.GetDescription((MiniCore.eAktifDurum)DataBinder.Eval(Container.DataItem,"AKTIF").ToShort(0)) %>
                    </ItemTemplate>

<HeaderStyle Width="50px"></HeaderStyle>
                </asp:TemplateField>
            </Columns>
            <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
            <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
        </asp:GridView>
    </div>
</asp:Content>
