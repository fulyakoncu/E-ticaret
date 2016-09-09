<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Bilgilerim.aspx.cs" Inherits="ETicaret.Uye.Bilgilerim" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        $(function () {
            $("#<%= txtDogumTar.ClientID %>").datepicker({
                defaultDate: "-20y"
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="radw760 form">
        <div class="radsatir baslik">
            Üyelik Bilgileri</div>
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
                    E Posta</div>
                <div class="radw200 radright">
                    <asp:TextBox ID="txtEmail" Width="180" runat="server"></asp:TextBox></div>
            </div>
        </div>
        <div class="radsatir">
            <div class="radw360">
                <div class="radw120 radleft">
                    E Posta (Tekrar)</div>
                <div class="radw200 radright">
                    <asp:TextBox ID="txtEmail2" Width="180" runat="server"></asp:TextBox></div>
            </div>
        </div>
        <div class="radsatir">
            <div class="radw360">
                <div class="radw120 radleft">
                    Şifreniz</div>
                <div class="radw200 radright">
                    <asp:TextBox ID="txtSifre" Width="180" TextMode="Password" runat="server"></asp:TextBox></div>
            </div>
        </div>
        <div class="radsatir">
            <div class="radw360">
                <div class="radw120 radleft">
                    Şifreniz (tekrar)</div>
                <div class="radw200 radright">
                    <asp:TextBox ID="txtSifre2" Width="180" TextMode="Password" runat="server"></asp:TextBox></div>
            </div>
        </div>
        <div class="radsatir" id="dvGruptipi" runat="server">
            <div class="radw360">
                <div class="radw120 radleft">
                    Grup Tipi</div>
                <div class="radw200 radright">
                    <asp:DropDownList ID="ddlUyeTipi" runat="server">
                    </asp:DropDownList>
              </div>
            </div>
        </div>
        <div class="radsatir">
            <div class="radw360">
                <div class="radw120 radleft">
                    Doğum Tarihiniz</div>
                <div class="radw200 radright">
                    <asp:TextBox ID="txtDogumTar" Width="180" runat="server"></asp:TextBox>
                </div>
            </div>
        </div>
        <div class="radsatir">
            <div class="radw360">
                <div class="radw120 radleft">
                    Cinsiyetiniz</div>
                <div class="radw200 radright">
                    <asp:RadioButtonList ID="rblCinsiyet" RepeatDirection="Horizontal" runat="server">
                    </asp:RadioButtonList>
                </div>
            </div>
        </div>
        <div class="radsatir">
            <div class="radw360">
                <div class="radw120 radleft">
                    Adres</div>
                <div class="radw200 radright">
                    <asp:TextBox ID="txtAdres" TextMode="MultiLine" Width="180" runat="server"></asp:TextBox>
                </div>
            </div>
        </div>
        <div class="radsatir">
            <div class="radw360">
                <div class="radw120 radleft">
                    Şehir</div>
                <div class="radw200 radright">
                    <asp:DropDownList ID="ddlIl" Width="180" runat="server">
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <div class="radsatir">
            <div class="radw360">
                <div class="radw120 radleft">
                    Cep Telefonu</div>
                <div class="radw200 radright">
                    <asp:TextBox ID="txtCepTel" Width="180" runat="server"></asp:TextBox></div>
            </div>
        </div>
        <div class="radsatir">
            <div class="radw360">
                <div class="radw120 radleft">
                    Ev Telefonu</div>
                <div class="radw200 radright">
                    <asp:TextBox ID="txtEvTel" Width="180" runat="server"></asp:TextBox></div>
            </div>
        </div>
        <div class="radsatir">
            <asp:Button ID="btnKaydet" CssClass="greenbutton" runat="server" Text="Kaydet" OnClick="btnKaydet_Click" />
        </div>
    </div>
</asp:Content>
