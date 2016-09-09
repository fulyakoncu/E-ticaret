<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="UrunListem.aspx.cs" Inherits="ETicaret.Uye.UrunListem" %>
<%@ Import Namespace="MiniCore" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="radsatir">
        <asp:GridView ID="gvUrunListem" runat="server" AutoGenerateColumns="false" 
            DataKeyNames="ID" 
            onselectedindexchanged="gvUrunListem_SelectedIndexChanged">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:CommandField HeaderStyle-Width="40px" HeaderText="Sil" SelectText="Sil" ShowSelectButton="True" />
                <asp:TemplateField HeaderText="İncele">
                    <ItemTemplate>
                        <a href='<%#ResolveUrl("~/Urunler/" + Eval("URUNID") + "_" +MiniCore.cAraclar.URLDuzelt(Eval("URUN_ADI").ToString())+".aspx") %>'>İncele</a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="MARKA_ADI" HeaderText="Marka" />
                <asp:BoundField DataField="URUN_ADI" HeaderText="Ürün Adı" />
            </Columns>
            <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
            <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
        </asp:GridView>
    </div>
    <div class="radsatir">
        <asp:GridView ID="gvSepetim" runat="server" AutoGenerateColumns="false" DataKeyNames="URUNID">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField DataField="URUNID" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:CheckBox ID="chcSecim" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="MARKAADI" HeaderText="Marka" />
                <asp:BoundField DataField="URUNADI" HeaderText="Ürün Adı" />
            </Columns>
            <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
            <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
        </asp:GridView>
    </div>
    <div class="radsatir">
        <asp:Button ID="btnAktar" CssClass="bluebutton" runat="server" Text="Aktar" OnClick="btnAktar_Click" />
    </div>
</asp:Content>
