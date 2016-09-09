<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="UrunDetay.aspx.cs" Inherits="ETicaret.UrunDetay" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" type="text/css" href="<%= Page.ResolveUrl("~")%>Styles/jquery.popeye.style.css" media="screen" />
    <script type="text/javascript" src='<%=ResolveUrl("~/Scripts/jquery.popeye-2.1.js") %>'></script>
    <script type="text/javascript">
        $(document).ready(function () {

            var options2 = {
                caption: false,
                navigation: 'permanent',
                direction: 'right'
            }
            $('#ppy2').popeye(options2);
        });
        $(function () {
            $("#dvtabs").tabs();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="radsatir">
        <div class="radleft radw200">
            <div class="ppy" id="ppy2">
                <ul class="ppy-imglist">
                    <asp:Repeater ID="rpResimler" runat="server">
                        <ItemTemplate>
                            <li><a href="<%= Page.ResolveUrl("~")%>Upload/UrunResimleri/<%#Eval("URUNID") %>/<%#Eval("RESIMADI") %>">
                                <img src="<%= Page.ResolveUrl("~")%>Upload/UrunResimleri/<%#Eval("URUNID") %>/<%#Eval("RESIMADI") %>" /></a></li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
                <div class="ppy-outer">
                    <div class="ppy-stage">
                        <div class="ppy-counter">
                            <strong class="ppy-current"></strong>/ <strong class="ppy-total"></strong>
                        </div>
                    </div>
                    <div class="ppy-nav">
                        <asp:Panel ID="pnCokluResim" runat="server" Visible="false">
                            <div class="nav-wrap">
                                <a class="ppy-next" title="Next image">İleri</a> <a class="ppy-prev" title="Previous image">
                                    Geri</a>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>
        <div class="radright radw360">
            <div class="radsatir">
                <b>
                    <asp:Literal ID="ltMarka" runat="server"></asp:Literal>
                    &nbsp;/
                    <asp:Literal ID="ltUrunAdi" runat="server"></asp:Literal></b>
            </div>
            <div class="radsatir">
                <asp:Literal ID="ltKategori" runat="server"></asp:Literal></div>
            <div class="radsatir">
                Fiyatı :
                <asp:Literal ID="ltUrunFiyat" runat="server"></asp:Literal>
            </div>
            <div class="radsatir">
                <asp:Panel ID="pnIndirim" runat="server" Visible="false">
                    İndirimli :
                    <asp:Literal ID="ltIndirim" runat="server"></asp:Literal>
                </asp:Panel>
            </div>
            <div class="radsatir" id="dvTLDegeri" runat="server">
                TL Fiyatı :
                <asp:Literal ID="ltTLDegeri" runat="server"></asp:Literal>
            </div>
            <div class="radsatir">
                Stok Durumu :
                <asp:Literal ID="ltStok" runat="server"></asp:Literal>
            </div>
            <div class="radsatir">
                <asp:Label ID="lblAdet" runat="server" Text="Adet : "></asp:Label>
    <asp:TextBox ID="txtAdet" CssClass="tbadet" runat="server" Text="1" Width="34px"></asp:TextBox>&nbsp;<asp:Button
        ID="btnSepeteAta" runat="server" CssClass="redbutton" Text="SepeteAt" onclick="btnSepeteAta_Click" />
            </div>
        </div>
    </div>
    <div class="radsatir">
        <div id="dvtabs">
            <ul>
                <li><a href="#dvOzellik">Özellikler</a></li>
                <li><a href="#dvYorum">Yorumlar</a></li>
                <li><a href="#dvAciklama">Açıklama</a></li>
            </ul>
            <div id="dvOzellik">
                <div class="radsatir">
                    Ürün Özellikleri
                </div>
                <div class="radsatir">
                    <asp:Repeater ID="rpUrunOzellikleri" runat="server">
                        <ItemTemplate>
                            <div class="radsatir">
                                <div class="radleft radw200">
                                    <%#Eval("OZELLIK_ADI") %>
                                </div>
                                <div class="radleft">
                                    <%#Eval("DEGER") %>&nbsp;<%#Eval("OZELLIK_BIRIMI") %></div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
            <div id="dvYorum">
                <div class="radsatir">
                    Yorumlar
                </div>
                <div class="radsatir">
                    <asp:Repeater ID="rpYorumlar" runat="server" OnItemDataBound="rpYorumlar_ItemDataBound">
                        <ItemTemplate>
                            <div class="radsatir">
                                <asp:Literal ID="ltYorumYazan" runat="server"></asp:Literal>
                                /
                                <asp:Literal ID="ltTarih" runat="server"></asp:Literal>
                            </div>
                            <div class="radsatir">
                                <%#Eval("BASLIK") %>
                            </div>
                            <div class="radsatir">
                                <%#Eval("MESAJ") %>
                            </div>
                            <div class="radsatir">
                                <hr />
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
                <div class="radsatir">
                    <div id="dvYorumEkle" runat="server">
                        <div class="radsatir baslik">
                            Yorum Ekle</div>
                        <div class="radsatir">
                            <div class="radw360">
                                <div class="radw120 radleft">
                                    Başlık</div>
                                <div class="radw200 radright">
                                    <asp:TextBox ID="txtBaslik" Width="180" runat="server"></asp:TextBox></div>
                            </div>
                        </div>
                        <div class="radsatir">
                            <div class="radw360">
                                <div class="radw120 radleft">
                                    Mesaj</div>
                                <div class="radw200 radright">
                                    <asp:TextBox ID="txtMesaj" TextMode="MultiLine" Width="180" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="radsatir">
                            <asp:Button ID="btnKaydet" runat="server" Text="Kaydet" OnClick="btnKaydet_Click" />
                        </div>
                    </div>
                </div>
            </div>
            <div id="dvAciklama">
                <asp:Literal ID="ltAciklama" runat="server"></asp:Literal></div>
        </div>
    </div>
</asp:Content>
