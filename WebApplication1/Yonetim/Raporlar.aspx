<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Raporlar.aspx.cs" Inherits="ETicaret.Yonetim.Raporlar" %>
<%@ Import Namespace="MiniCore" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="radsatir">
        <div class="baslik">
            Raporlar</div>
    </div>
        <div class="radsatir">
        <div class="radw260 radleft">
            Kayıt No</div>
        <div class="radw260 radleft">
            Rapor Adı</div>
    </div>
    <div class="radsatir">
        <div class="radw260 radleft">
            <asp:Label ID="lblID" runat="server" Text="0"></asp:Label></div>
        <div class="radw260 radleft">
            <asp:TextBox ID="txtRaporAdi" Width="180" runat="server"></asp:TextBox></div>
    </div>
    <div class="radsatir">
        <div class="radw260 radleft">
            Rapor(.rpt)</div>
        <div class="radw260 radleft">
            Rapor(.cs)</div>
    </div>
    <div class="radsatir">
        <div class="radw260 radleft">
            <asp:FileUpload ID="fuRaporRpt" runat="server" />
        </div>
        <div class="radw260 radleft">
            <asp:FileUpload ID="fuRaporCs" runat="server" />
        </div>
    </div>
    <div class="radsatir">
            <asp:Button ID="btnSil" CssClass="redbutton" runat="server" Text="Sil" onclick="btnSil_Click" />
        <asp:Button ID="btnYeniKayit" CssClass="bluebutton" runat="server" Text="Yeni Kayıt" 
            onclick="btnYeniKayit_Click" />
        <asp:Button ID="btnKaydet" CssClass="greenbutton" runat="server" Text="Kaydet" 
            onclick="btnKaydet_Click" />
    </div>
    <div class="radsatir">
        <asp:GridView ID="gvRaporlar" AutoGenerateColumns="false" DataKeyNames="ID" runat="server"
            onselectedindexchanged="gvRaporlar_SelectedIndexChanged" Width="538px">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:CommandField HeaderStyle-Width="40px" HeaderText="Seç" SelectText="Seç" ShowSelectButton="True" />
                <asp:BoundField HeaderStyle-Width="35px" DataField="ID" HeaderText="Kayıt No" >
<HeaderStyle Width="20px"></HeaderStyle>
                </asp:BoundField>
                <asp:BoundField HeaderStyle-Width="200px" DataField="AD" 
                    HeaderText="Rapor Adı" >
<HeaderStyle Width="200px"></HeaderStyle>
                </asp:BoundField>
                                <asp:BoundField HeaderStyle-Width="200px" DataField="YOL" 
                    HeaderText="Rapor Yolu" >
<HeaderStyle Width="200px"></HeaderStyle>
                </asp:BoundField>
            </Columns>
            <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
            <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
        </asp:GridView>
    </div>
</asp:Content>
