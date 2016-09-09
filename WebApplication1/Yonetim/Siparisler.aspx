<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Siparisler.aspx.cs" Inherits="ETicaret.Yonetim.BekleyenSiparisler" %>

<%@ Import Namespace="MiniCore" %>
<%@ Import Namespace="ETicaretIslemleri" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<script type="text/javascript">
    function AcWindow(deger) {
        var url = "SiparisDuzenle.aspx?ID=" + deger;
        window.open(url, 'Deneme', 'menubar=1,resizable=1,height=600px,width=550px');
    }
</script>
    <div class="radsatir">
        Sipariş Durumu :
        <asp:DropDownList ID="ddlSiparisTuru" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSiparisTuru_SelectedIndexChanged">
            <asp:ListItem Text="Bekleyen Siparişler" Value="B"></asp:ListItem>
            <asp:ListItem Text="Sonuçlanan Siparişler" Value="S"></asp:ListItem>
        </asp:DropDownList>
    </div>
    <div class="radsatir">
        <asp:GridView ID="gvSiparisler" runat="server" AutoGenerateColumns="false" DataKeyNames="ID">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <a href="javascript:AcWindow(<%# Eval("ID") %>);">Düzenle</a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderStyle-Width="135px" DataField="UYE_ADISOYADI" HeaderText="Üye Adı Soyadı">
                </asp:BoundField>
                <asp:BoundField HeaderStyle-Width="100px" DataField="FATURA_ADI" HeaderText="Adı"></asp:BoundField>
                <asp:BoundField HeaderStyle-Width="100px" DataField="SPOSSONUC" HeaderText="Sonuç"></asp:BoundField>
                <asp:TemplateField HeaderStyle-Width="50px" HeaderText="Ödeme Tipi">
                    <ItemTemplate>
                        <%# MiniCore.cAraclar.GetDescription((ETicaretIslemleri.eOdemeTipi)DataBinder.Eval(Container.DataItem,"ODEMETIPI").ToShort(0)) %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-Width="50px" HeaderText="Durumu">
                    <ItemTemplate>
                        <%# MiniCore.cAraclar.GetDescription((ETicaretIslemleri.eSiparisDurumu)DataBinder.Eval(Container.DataItem,"SIPARISDURUMU").ToShort(0)) %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderStyle-Width="10px" DataField="OLUSMA_ZAMANI" HeaderText="Sipariş Zamanı">
                </asp:BoundField>
                <asp:BoundField HeaderStyle-Width="80px" DataField="TUTAR" HeaderText="Tutar">
                </asp:BoundField>
            </Columns>
            <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
            <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
        </asp:GridView>
    </div>
</asp:Content>
