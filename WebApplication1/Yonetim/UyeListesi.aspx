<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="UyeListesi.aspx.cs" Inherits="ETicaret.Yonetim.UyeListesi" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="radsatir">
        <asp:GridView ID="gvUyeler" runat="server" AutoGenerateColumns="false" DataKeyNames="ID">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <a href='<%#ResolveUrl("~/Uye/Bilgilerim.aspx?ID="+(Eval("ID"))) %>'>Düzenle</a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderStyle-Width="100px" DataField="ADI" HeaderText="Adı">
                </asp:BoundField>
                <asp:BoundField HeaderStyle-Width="100px" DataField="SOYADI" HeaderText="Soyadı"></asp:BoundField>
                <asp:BoundField HeaderStyle-Width="100px" DataField="EMAIL" HeaderText="Email">
                </asp:BoundField>
                <asp:BoundField HeaderStyle-Width="100px" DataField="CEPTELEFONU" HeaderText="Cep Telefonu">
                </asp:BoundField>
            </Columns>
            <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
            <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
        </asp:GridView>
    </div>
</asp:Content>
