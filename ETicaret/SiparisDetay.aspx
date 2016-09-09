<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SiparisDetay.aspx.cs" Inherits="ETicaret.SiparisDetay" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="radsatir">
    <asp:Literal ID="ltSiparis" runat="server"></asp:Literal>
</div>
    <div class="radsatir">
        <asp:GridView ID="gvSiparisDetaylar" runat="server" AutoGenerateColumns="false" DataKeyNames="ID">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
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
</asp:Content>
