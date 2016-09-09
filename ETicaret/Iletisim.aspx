<%@ Page Title="About Us" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Iletisim.aspx.cs" Inherits="ETicaret.About" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div class="radsatir baslik">
        İletişim</div>
    <div class="radw320">
        <div class="radsatir">
            <div class="radleft radw120">
                Adınız Soyadınız</div>
            <div class="radright radw200">
                <asp:TextBox ID="txtAdSoyad" Width="180" runat="server"></asp:TextBox></div>
        </div>
        <div class="radsatir">
            <div class="radleft radw120">
                Email</div>
            <div class="radright radw200">
                <asp:TextBox ID="txtEmail" Width="180" runat="server"></asp:TextBox></div>
        </div>
        <div class="radsatir">
            <div class="radleft radw120">
                Başlık</div>
            <div class="radright radw200">
                <asp:TextBox ID="txtBaslik" Width="180" runat="server"></asp:TextBox></div>
        </div>
        <div class="radsatir">
            <div class="radleft radw120">
                İçerik</div>
            <div class="radright radw200">
                <asp:TextBox ID="txtIcerik" Width="180" TextMode="MultiLine" runat="server"></asp:TextBox></div>
        </div>
        <div class="radsatir radleft">
            <asp:Button CssClass="greenbutton" ID="btnGonder" runat="server" Text="Gönder" 
                onclick="btnGonder_Click" />
        </div>
    </div>
</asp:Content>
