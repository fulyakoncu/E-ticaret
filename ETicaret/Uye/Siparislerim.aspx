<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Siparislerim.aspx.cs" Inherits="ETicaret.Uye.Siparislerim" %>

<%@ Import Namespace="MiniCore" %>
<%@ Import Namespace="ETicaretIslemleri" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="radsatir">
        <asp:GridView ID="gvSiparisler" runat="server" AutoGenerateColumns="false" DataKeyNames="ID"
            OnSelectedIndexChanged="gvSiparisler_SelectedIndexChanged">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:CommandField HeaderStyle-Width="40px" HeaderText="Seç" SelectText="Seç" ShowSelectButton="True" />
                <asp:BoundField DataField="FATURA_ADI" HeaderText="Fatura Adı" />
                <asp:BoundField DataField="KARGO_ADI" HeaderText="Kargo Adı" />
                <asp:BoundField DataField="KARGOKODU" HeaderText="Kargo Kodu" />
                <asp:BoundField DataField="TUTAR" HeaderText="Tutar" />
                <asp:TemplateField HeaderText="Durumu">
                    <ItemTemplate>
                        <%# MiniCore.cAraclar.GetDescription((ETicaretIslemleri.eSiparisDurumu)DataBinder.Eval(Container.DataItem,"SIPARISDURUMU").ToShort(0)) %>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
            <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
        </asp:GridView>
    </div>
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
