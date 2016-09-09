<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MarkaDetay.aspx.cs" Inherits="ETicaret.ModelDetay" %>

<%@ Register src="~/Controls/UrunListele.ascx" tagname="Urun" tagprefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<style type="text/css">
#modeldetay
{
    width:570px;
    margin-left:-8px;
    height:auto;
    margin-top:-10px;
    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="radsatir">
<div id="kategoridetay">
    <asp:ListView ID="lstUrunler" runat="server">
    <ItemTemplate>
    <uc:Urun runat="server" ID="urun" UrunID='<%# DataBinder.Eval(Container.DataItem, "ID") %>' />
    </ItemTemplate>
    </asp:ListView>
</div>
</div>
<div class="radsatir">
<center>
<asp:DataPager ID="dpUrunSayfalama" PagedControlID="lstUrunler" PageSize="4" QueryStringField="sayfa" runat="server">
                <Fields>
                    <asp:NextPreviousPagerField ButtonType="Link" ShowFirstPageButton="True" ShowNextPageButton="False"
                        ShowPreviousPageButton="False" FirstPageText="İlk" LastPageText="Son" 
                        NextPageText="Sonraki" PreviousPageText="Geri" />
                    <asp:NumericPagerField ButtonCount="8" />
                </Fields>
</asp:DataPager>
</center>
</div>
</asp:Content>
