<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TaksitTanimlamalari.aspx.cs" Inherits="ETicaret.Yonetim.TaksitTanimlamalari" %>
<%@ Import Namespace="MiniCore" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="radsatir">
        <div class="baslik">Taksit Tanımlamaları</div>
    </div>
    <div class="radsatir">
        <div class="radw100 radleft">Kayıt No:</div>
        <div class="radw100 radleft"><asp:Label ID="lblID" runat="server" Text="0"></asp:Label></div>
    </div>
    <div class="radsatir">
        <div class="radw100 radleft">Banka</div>
        <div class="radw100 radleft"><asp:DropDownList ID="ddlBanka" runat="server" 
                onselectedindexchanged="ddlBanka_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
        </div>
    </div>
    <div class="radsatir">
        <div class="radw260 radleft">Taksit</div>
        <div class="radw260 radleft">Oran</div>
    </div>
    <div class="radsatir">
        <div class="radw260 radleft"><asp:TextBox ID="txtTaksit" Width="40" runat="server"></asp:TextBox></div>
        <div class="radw260 radleft"><asp:TextBox ID="txtOran" Width="40" runat="server"></asp:TextBox></div>
    </div>
    <div class="radsatir">
        <asp:Button ID="btnSil" CssClass="redbutton" runat="server" Text="Sil" OnClick="btnSil_Click" />
        <asp:Button ID="btnYeniKayit" CssClass="bluebutton" runat="server" Text="Yeni Kayıt" OnClick="btnYeniKayit_Click" />
        <asp:Button ID="btnKaydet" CssClass="greenbutton" runat="server" Text="Kaydet" OnClick="btnKaydet_Click" />
    </div>
    <div class="radsatir">
        <asp:GridView ID="gvTaksitler" runat="server" AutoGenerateColumns="false" DataKeyNames="ID" OnSelectedIndexChanged="gvTaksitler_SelectedIndexChanged">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:CommandField HeaderStyle-Width="40px" HeaderText="Seç" SelectText="Seç" ShowSelectButton="True" />
                <asp:BoundField HeaderStyle-Width="35px" DataField="ID" HeaderText="Kayıt No">
                </asp:BoundField>
                <asp:BoundField HeaderStyle-Width="100px" DataField="TAKSIT" HeaderText="Taksit">
                </asp:BoundField>
                <asp:BoundField HeaderStyle-Width="100px" DataField="ORAN" HeaderText="Oran">
                </asp:BoundField>
            </Columns>
            <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
            <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
        </asp:GridView>
    </div>

</asp:Content>
