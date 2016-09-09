<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="UrunEkleme.aspx.cs" Inherits="ETicaret.Yonetim.UrunEkle" %>

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
        function changeCheckboxTextEH(checkbox) {
            if (checkbox.checked)
                checkbox.nextSibling.innerHTML = 'Evet';
            else
                checkbox.nextSibling.innerHTML = 'Hayır';
        }
        function AcWindow(deger) {
            var iID = $('#<%=lblID.ClientID %>').text();
            var url;
            if (iID > 0) {
                if (deger == 1)
                { url = "UrunOzellikEkle.aspx?UID=" + iID; }
                else if (deger == 2)
                { url = "UrunGrupEkle.aspx?UID=" + iID; }
                else if (deger == 3)
                { url = "UrunResimEkle.aspx?UID=" + iID; }
                else if (deger == 4)
                { url = "UrunStokIslemi.aspx?UID=" + iID; }
                window.open(url, 'Deneme', 'menubar=1,resizable=1,height=450px,width=450px');
            }
        }
    </script>
    <div class="radsatir">
        <div class="baslik">
            Ürün Listesi</div>
    </div>
    <div class="radsatir">
        <div class="radw120 radleft">
            Kayıt No</div>
        <div class="radw120 radleft">
            <asp:Label ID="lblID" runat="server" Text="0"></asp:Label></div>
    </div>
    <div class="radsatir">
        <div class="radw260 radleft">
            Kategori</div>
        <div class="radw260 radleft">
            Marka</div>
    </div>
    <div class="radsatir">
        <div class="radw260 radleft">
            <asp:DropDownList ID="ddlKategoriler" runat="server">
            </asp:DropDownList>
        </div>
        <div class="radw260 radleft">
            <asp:DropDownList ID="ddlMarkalar" runat="server">
            </asp:DropDownList>
        </div>
    </div>
    <div class="radsatir">
        <div class="radw260 radleft">
            Kodu</div>
        <div class="radw260 radleft">
            Modeli</div>
    </div>
    <div class="radsatir">
        <div class="radw260 radleft">
            <asp:TextBox ID="txtKodu" runat="server"></asp:TextBox>
        </div>
        <div class="radw260 radleft">
            <asp:TextBox ID="txtModeli" runat="server"></asp:TextBox>
        </div>
    </div>
    <div class="radsatir">
        <div class="radw260 radleft">
            Fiyatı</div>
        <div class="radw260 radleft">
            Para Birimi</div>
    </div>
    <div class="radsatir">
        <div class="radw260 radleft">
            <asp:TextBox ID="txtFiyat" runat="server"></asp:TextBox>
        </div>
        <div class="radw260 radleft">
            <asp:DropDownList ID="ddlParaBirimi" runat="server">
            </asp:DropDownList>
        </div>
    </div>
    <div class="radsatir">
        <div class="radw260 radleft">
            Kampanyalı</div>
        <div class="radw260 radleft">
            Açıklama</div>
    </div>
    <div class="radsatir">
        <div class="radw260 radleft">
            <asp:CheckBox ID="chcKampanyali" onclick="changeCheckboxTextEH(this);" Text="Hayır"
                runat="server" />
        </div>
        <div class="radw260 radleft">
            <asp:TextBox ID="txtAciklama" runat="server"></asp:TextBox>
        </div>
    </div>
    <div class="radsatir">
        <div class="radw260 radleft">
            İndirimli</div>
        <div class="radw260 radleft">
            İndirimli Fiyatı</div>
    </div>
    <div class="radsatir">
        <div class="radw260 radleft">
            <asp:CheckBox ID="chcIndirimli" onclick="changeCheckboxTextEH(this);" Text="Hayır"
                runat="server" />
        </div>
        <div class="radw260 radleft">
            <asp:TextBox ID="txtIndFiyati" runat="server"></asp:TextBox>
        </div>
    </div>
    <div class="radsatir">
        <div class="radw260 radleft">
            Sliderda</div>
        <div class="radw260 radleft">
            Aktif</div>
    </div>
    <div class="radsatir">
        <div class="radw260 radleft">
            <asp:CheckBox ID="chcSlider" onclick="changeCheckboxTextEH(this);" Text="Hayır" runat="server" />
        </div>
        <div class="radw260 radleft">
            <asp:CheckBox ID="chcAktif" Text="Pasif" onclick="changeCheckboxText(this);" runat="server" />
        </div>
    </div>
    <div class="radsatir">
        <asp:Button ID="btnYeniKayit" CssClass="bluebutton" runat="server" Text="Yeni Kayıt" OnClick="btnYeniKayit_Click" />
        <asp:Button ID="btnKaydet" CssClass="greenbutton" runat="server" Text="Kaydet" OnClick="btnKaydet_Click" />
    </div>
    <div id="dvEkleme" runat="server" class="radsatir">
        <asp:Button ID="BtnOzellik" runat="server" Text="Özellik Ekle" OnClientClick="AcWindow(1)" />
        <asp:Button ID="BtnGrup" runat="server" Text="Grup Ekle" OnClientClick="AcWindow(2)" />
        <asp:Button ID="BtnResim" runat="server" Text="Resim Ekle" OnClientClick="AcWindow(3)" />
        <asp:Button ID="BtnStok" runat="server" Text="Stok İşlemi" OnClientClick="AcWindow(4)" />
    </div>
    <div class="radsatir">
        <asp:GridView ID="gvUrunler" AutoGenerateColumns="false" DataKeyNames="ID" runat="server"
            OnSelectedIndexChanged="gvUrunler_SelectedIndexChanged" AllowPaging="true" 
            PageSize="10" onpageindexchanging="gvUrunler_PageIndexChanging">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:CommandField HeaderStyle-Width="40px" HeaderText="Seç" SelectText="Seç" ShowSelectButton="True" />
                <asp:BoundField HeaderStyle-Width="35px" DataField="ID" HeaderText="Kayıt No">
                    <HeaderStyle Width="20px"></HeaderStyle>
                </asp:BoundField>
                <asp:BoundField HeaderStyle-Width="200px" DataField="KODU" HeaderText="Kodu">
                    <HeaderStyle Width="200px"></HeaderStyle>
                </asp:BoundField>
                <asp:BoundField HeaderStyle-Width="200px" DataField="MARKA_ADI" HeaderText="Marka">
                    <HeaderStyle Width="200px"></HeaderStyle>
                </asp:BoundField>
                <asp:BoundField HeaderStyle-Width="200px" DataField="MODEL" HeaderText="Model">
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
