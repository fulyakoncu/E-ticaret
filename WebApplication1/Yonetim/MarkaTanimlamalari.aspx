<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="MarkaTanimlamalari.aspx.cs" Inherits="ETicaret.Yonetim.MarkaTanimlamalari" %>

 <%@ Import Namespace="MiniCore" %>    
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="radsatir">
        <div class="baslik">
            Marka Tanımlamaları</div>
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
            <asp:TextBox ID="txtMarkaAdi" Width="180" runat="server"></asp:TextBox></div>
        <div class="radw260 radleft">
            <asp:TextBox ID="txtKodu" Width="180" runat="server"></asp:TextBox></div>
    </div>
    <div class="radsatir">
        <div class="radw260 radleft">
            Logo (Güncellemeyecekseniz boş bırakın)</div>
        <div class="radw260 radleft">
            Açıklama (Markaya özel kampanya, garanti vb)</div>
    </div>
    <div class="radsatir">
        <div class="radw260 radleft">
            <asp:FileUpload ID="fuLogo" Width="220" runat="server" />
        </div>
        <div class="radw260 radleft">
            <asp:TextBox ID="txtAciklama" TextMode="MultiLine" Width="180" runat="server"></asp:TextBox>
        </div>
    </div>
    <div class="radsatir">
        <asp:Button ID="btnYeniKayit" CssClass="bluebutton" runat="server" Text="Yeni Kayıt" OnClick="btnYeniKayit_Click" />
        <asp:Button ID="btnKaydet" CssClass="greenbutton" runat="server" Text="Kaydet" OnClick="btnKaydet_Click" />
    </div>
    <div class="radsatir">
        <asp:GridView ID="gvMarkalar" runat="server" AutoGenerateColumns="false" DataKeyNames="ID" onselectedindexchanged="gvKargolar_SelectedIndexChanged">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:CommandField HeaderStyle-Width="40px" HeaderText="Seç" SelectText="Seç" ShowSelectButton="True" />
                <asp:BoundField HeaderStyle-Width="35px" DataField="ID" HeaderText="Kayıt No"></asp:BoundField>
                <asp:BoundField HeaderStyle-Width="100px" DataField="ADI" HeaderText="Adı"></asp:BoundField>
                <asp:BoundField HeaderStyle-Width="100px" DataField="KODU" HeaderText="Kodu"></asp:BoundField>
                <asp:TemplateField HeaderStyle-Width="100px"  HeaderText="Logo">
                <ItemTemplate>
                <img width="100" src='<%#ResolveUrl("~/Upload/Logo/"+(Eval("LOGO").IsNull()?"image-not-found.gif":Eval("LOGO"))) %>' />
                </ItemTemplate>
                </asp:TemplateField>
<%--                <asp:BoundField HeaderStyle-Width="80px" DataField="LOGO" HeaderText="Logo"></asp:BoundField>--%>
                <asp:BoundField HeaderStyle-Width="180px" DataField="ACIKLAMA" HeaderText="Açıklama"></asp:BoundField>
            </Columns>
            <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
            <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
        </asp:GridView>
    </div>
</asp:Content>
