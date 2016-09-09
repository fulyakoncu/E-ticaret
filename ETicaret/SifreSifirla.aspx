<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="SifreSifirla.aspx.cs" Inherits="ETicaret.SifreSifirla" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="radsatir">
        <div class="baslik">
            Şifre Sıfırla</div>
    </div>
    <div class="radsatir">
        <div class="radw360">
            <div class="radw120 radleft">
                Yeni Şifreniz</div>
            <div class="radw200 radright">
                <asp:TextBox ID="txtSifre" Width="180" runat="server"></asp:TextBox></div>
        </div>
        <div class="radsatir">
            <asp:Button ID="btnGonder" CssClass="greenbutton" runat="server" Text="Değiştir" 
                onclick="btnGonder_Click" />
        </div>
    </div>
</asp:Content>
