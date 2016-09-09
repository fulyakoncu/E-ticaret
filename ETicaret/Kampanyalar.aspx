<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Kampanyalar.aspx.cs" Inherits="ETicaret.Kampanyalar" %>
<%@ Register src="~/Controls/UrunListele.ascx" tagname="Urun" tagprefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="radsatir">
        <asp:ListView ID="lstUrunler" runat="server">
            <ItemTemplate>
                <uc:urun runat="server" id="urun" urunid='<%# DataBinder.Eval(Container.DataItem, "ID") %>' />
            </ItemTemplate>
        </asp:ListView>
    </div>
    <div class="radsatir">
        <center>
            <asp:DataPager ID="dpUrunSayfalama" PagedControlID="lstUrunler" PageSize="4" QueryStringField="sayfa"
                 runat="server">
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
