<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Odeme.aspx.cs" Inherits="ETicaret.Odeme" %>

<%@ Import Namespace="MiniCore" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
            <script type="text/javascript">
                $(function () {
                    $("#accordion").accordion({ autoHeight: false });
                });
            </script>
            <div id="accordion">
                <h3>
                    <a href="#">Kişisel Bilgiler</a></h3>
                <div id="dvKisisel">
                    <div class="radsatir">
                        <div class="radw360">
                            <div class="radw120 radleft">
                                Adınız</div>
                            <div class="radw200 radright">
                                <asp:TextBox ID="txtAd" Width="180" runat="server"></asp:TextBox></div>
                        </div>
                    </div>
                    <div class="radsatir">
                        <div class="radw360">
                            <div class="radw120 radleft">
                                Soyadınız</div>
                            <div class="radw200 radright">
                                <asp:TextBox ID="txtSoyad" Width="180" runat="server"></asp:TextBox></div>
                        </div>
                    </div>
                    <div class="radsatir">
                        <div class="radw360">
                            <div class="radw120 radleft">
                                Email</div>
                            <div class="radw200 radright">
                                <asp:TextBox ID="txtEmail" Width="180" runat="server"></asp:TextBox></div>
                        </div>
                    </div>
                    <div class="radsatir">
                        <div class="radw360">
                            <div class="radw120 radleft">
                                Cep Telefonu</div>
                            <div class="radw200 radright">
                                <asp:TextBox ID="txtCepTelefonu" Width="180" runat="server"></asp:TextBox></div>
                        </div>
                    </div>
                </div>
                <h3>
                    <a href="#">Fatura Bilgileri</a></h3>
                <div>
                    <div class="radsatir">
                        <div class="radw360">
                            <div class="radw120 radleft">
                                Fatura Adı</div>
                            <div class="radw200 radright">
                                <asp:TextBox ID="txtFaturaAdi" Width="180" runat="server"></asp:TextBox></div>
                        </div>
                    </div>
                    <div class="radsatir">
                        <div class="radw360">
                            <div class="radw120 radleft">
                                Fatura VergiNo</div>
                            <div class="radw200 radright">
                                <asp:TextBox ID="txtFaturaVergiNo" Width="180" runat="server"></asp:TextBox></div>
                        </div>
                    </div>
                    <div class="radsatir">
                        <div class="radw360">
                            <div class="radw120 radleft">
                                Fatura Adresi</div>
                            <div class="radw200 radright">
                                <asp:TextBox ID="txtAdres" TextMode="MultiLine" Width="180" runat="server"></asp:TextBox></div>
                        </div>
                    </div>
                </div>
                <h3>
                    <a href="#">Alınan Ürünler</a></h3>
                <div>
                    <div class="radsatir">
                        <asp:GridView ID="gvSiparisDetaylar" runat="server" AutoGenerateColumns="false" OnRowDataBound="gvSiparisDetaylar_RowDataBound">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:BoundField DataField="URUNID" HeaderText="Ürün No" />
                                <asp:BoundField DataField="MARKAADI" HeaderText="Marka" />
                                <asp:BoundField DataField="URUNADI" HeaderText="Ürün" />
                                <asp:BoundField DataField="MIKTAR" HeaderText="Miktar" />
                                <asp:TemplateField HeaderText="Birim Fiyat">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTutar" runat="server" Text="0"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                            <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                        </asp:GridView>
                        <asp:Literal ID="ltTutarBilgileri" runat="server"></asp:Literal>
                        <asp:HiddenField ID="hfToplamTutar" runat="server" />
                    </div>
                </div>
                <h3>
                    <a href="#">Ödeme</a></h3>
                <div>
                    <div class="radsatir">
                        <div class="radw120 radleft">
                            Ödeme Tipi
                        </div>
                        <div class="radw200 radright">
                            <asp:DropDownList ID="ddlOdemeTipi" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlOdemeTipi_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div id="dvSanalPos" runat="server">
                        <asp:Panel ID="pnKrediKartıBilgileri" runat="server">
                            <div class="radw400 form">
                                <div class="radsatir baslik">
                                    Kredi Kartı Bilgileri</div>
                                <div class="radsatir">
                                    <div class="radw360">
                                        <div class="radw120 radleft">
                                            Kart Üzerindeki İsim</div>
                                        <div class="radw200 radright">
                                            <asp:TextBox ID="txtAdSoyad" Width="180" runat="server"></asp:TextBox></div>
                                    </div>
                                </div>
                                <div class="radsatir">
                                    <div class="radw360">
                                        <div class="radw120 radleft">
                                            Kart Tipi</div>
                                        <div class="radw200 radright">
                                            <asp:DropDownList ID="ddlKartTipi" Width="180" runat="server">
                                                <asp:ListItem Value="1">Visa</asp:ListItem>
                                                <asp:ListItem Value="2">MasterCard</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="radsatir">
                                    <div class="radw360">
                                        <div class="radw120 radleft">
                                            Son Kullanma Tarihi</div>
                                        <div class="radw200 radright">
                                            <asp:DropDownList ID="ddlAylar" runat="server">
                                                <asp:ListItem Value="0">Ay</asp:ListItem>
                                                <asp:ListItem Value="01">Ocak</asp:ListItem>
                                                <asp:ListItem Value="02">Şubat</asp:ListItem>
                                                <asp:ListItem Value="03">Mart</asp:ListItem>
                                                <asp:ListItem Value="04">Nisan</asp:ListItem>
                                                <asp:ListItem Value="05">Mayıs</asp:ListItem>
                                                <asp:ListItem Value="06">Haziran</asp:ListItem>
                                                <asp:ListItem Value="07">Temmuz</asp:ListItem>
                                                <asp:ListItem Value="08">Ağustos</asp:ListItem>
                                                <asp:ListItem Value="09">Eylül</asp:ListItem>
                                                <asp:ListItem Value="10">Ekim</asp:ListItem>
                                                <asp:ListItem Value="11">Kasım</asp:ListItem>
                                                <asp:ListItem Value="12">Aralık</asp:ListItem>
                                            </asp:DropDownList>
                                            /
                                            <asp:DropDownList ID="ddlYillar" runat="server">
                                                <asp:ListItem Value="0">Yıl</asp:ListItem>
                                                <asp:ListItem Value="11">2011</asp:ListItem>
                                                <asp:ListItem Value="12">2012</asp:ListItem>
                                                <asp:ListItem Value="13">2013</asp:ListItem>
                                                <asp:ListItem Value="14">2014</asp:ListItem>
                                                <asp:ListItem Value="15">2015</asp:ListItem>
                                                <asp:ListItem Value="16">2016</asp:ListItem>
                                                <asp:ListItem Value="17">2017</asp:ListItem>
                                                <asp:ListItem Value="18">2018</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="radsatir">
                                    <div class="radw360">
                                        <div class="radw120 radleft">
                                            Kart Numarası</div>
                                        <div class="radw200 radright">
                                            <asp:TextBox ID="txtKartNumarasi" Width="180" runat="server"></asp:TextBox></div>
                                    </div>
                                </div>
                                <div class="radsatir">
                                    <div class="radw360">
                                        <div class="radw120 radleft">
                                            Güvenlik Kodu</div>
                                        <div class="radw200 radright">
                                            <asp:TextBox ID="txtGuvenlikKodu" Width="60px" TextMode="Password" runat="server"></asp:TextBox>
                                            <a href="#">Detaylı Bilgi</a></div>
                                    </div>
                                </div>
                                <div class="radsatir">
                                    <div class="radw360">
                                        <div class="radw120 radleft">
                                            Banka Seçiniz</div>
                                        <div class="radw200 radright">
                                            <asp:DropDownList ID="ddlBankalar" Width="180" runat="server">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="radsatir">
                                    <div class="radw360">
                                        <div class="radw120 radleft">
                                            Ödeme Şekli</div>
                                        <div class="radw200 radright">
                                            <asp:DropDownList ID="ddlOdeme" Width="180" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlOdeme_SelectedIndexChanged">
                                                <asp:ListItem Value="1">Peşin</asp:ListItem>
                                                <asp:ListItem Value="2">Taksitli</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="pnTaksitlendirme" runat="server" Visible="false">
                            <div class="radw760 form">
                                <div class="radsatir baslik">
                                    Taksitlendirme</div>
                                <div class="radsatir">
                                    <div class="radw360">
                                        <div class="radw120 radleft">
                                        </div>
                                        <div class="radw200 radright">
                                        </div>
                                    </div>
                                </div>
                                <asp:Repeater ID="rpTaksitler" runat="server" OnItemDataBound="rpTaksitler_ItemDataBound">
                                    <ItemTemplate>
                                        <div class="radsatir">
                                            <div class="radw360">
                                                <div class="radw120 radleft">
                                                    <asp:Literal ID="ltRadioButon" runat="server"></asp:Literal>
                                                    <%#Eval("TAKSIT") %>
                                                    . Taksit</div>
                                                <div class="radw200 radright">
                                                    <asp:Literal ID="ltFiyat" runat="server"></asp:Literal>
                                                </div>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </asp:Panel>
                    </div>
                    <div id="dvHavale" runat="server">
                        <div class="radsatir">
                            <div class="radw360">
                                <div class="radw120 radleft">
                                    Banka Havale Bilgilerinizi ve TC Kimlik Numaranızı yazınız.
                                </div>
                                <div class="radw200 radright">
                                    <asp:TextBox ID="txtBankaHavale" TextMode="MultiLine" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="dvPostaCeki" runat="server">
                        <div class="radsatir">
                            <div class="radw360">
                                <div class="radw120 radleft">
                                    Posta Çeki Bilgilerinizi ve TC Kimlik Numaranızı yazınız.
                                </div>
                                <div class="radw200 radright">
                                    <asp:TextBox ID="txtPostaCeki" TextMode="MultiLine" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="dvKapida" runat="server">
                        <div class="radsatir">
                            <div class="radw360">
                                <div class="radw120 radleft">
                                    Alternatif Telefon Numarası,Adres ve Kapıdan Ödeme ile ilgili Bilgilerinizi yazın
                                    (Kredi Kartı,Nakit vb)
                                </div>
                                <div class="radw200 radright">
                                    <asp:TextBox ID="txtKapıda" TextMode="MultiLine" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="radsatir">
                <asp:Button CssClass="greenbutton" ID="btnSiparisTamamla" runat="server" Text="Siparişi Tamamla"
                    OnClick="btnSiparisTamamla_Click" />
            </div>
            <div class="radsatir">
                Sipariş Kodu :
                <asp:Literal ID="ltBilgi" runat="server"></asp:Literal><br />
                <asp:Literal ID="ltSonuc" runat="server"></asp:Literal>
            </div>
</asp:Content>
