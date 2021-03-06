﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UrunOzellikleri.aspx.cs" Inherits="ETicaret.Yonetim.UrunOzellikleri" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="radsatir">
        <div class="baslik">
            Özellik Tanımlamaları</div>
    </div>
    <div class="radsatir">
        <div class="radw100 radleft">
            Kayıt No:</div>
        <div class="radw100 radleft">
            <asp:Label ID="lblID" runat="server" Text="0"></asp:Label></div>
    </div>
    <div class="radsatir">
        <div class="radw260 radleft">
            Adı</div>
        <div class="radw260 radleft">
            Kodu</div>
    </div>
    <div class="radsatir">
        <div class="radw260 radleft">
            <asp:TextBox ID="txtOzellikAdi" Width="180" runat="server"></asp:TextBox></div>
        <div class="radw260 radleft">
            <asp:TextBox ID="txtBirimi" TextMode="MultiLine" Width="180" runat="server"></asp:TextBox></div>
    </div>
    <div class="radsatir">
        <asp:Button ID="btnYeniKayit" CssClass="bluebutton" runat="server" Text="Yeni Kayıt" 
            onclick="btnYeniKayit_Click" />
        <asp:Button ID="btnKaydet" CssClass="greenbutton" runat="server" Text="Kaydet" 
            onclick="btnKaydet_Click" />
    </div>
    <div class="radsatir">
        <asp:GridView ID="gvOzellikler" runat="server" AutoGenerateColumns="false" DataKeyNames="ID" 
            onselectedindexchanged="gvOzellikler_SelectedIndexChanged">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:CommandField HeaderStyle-Width="40px" HeaderText="Seç" SelectText="Seç" ShowSelectButton="True" />
                <asp:BoundField HeaderStyle-Width="35px" DataField="ID" HeaderText="Kayıt No"></asp:BoundField>
                <asp:BoundField HeaderStyle-Width="100px" DataField="OZELLIK_ADI" HeaderText="Adı"></asp:BoundField>
                <asp:BoundField HeaderStyle-Width="100px" DataField="BIRIMI" HeaderText="Kodu"></asp:BoundField>
            </Columns>
            <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
            <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
        </asp:GridView>
    </div>
</asp:Content>
