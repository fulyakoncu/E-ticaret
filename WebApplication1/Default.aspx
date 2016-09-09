<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="ETicaret._Default" %>

<%@ Register Src="~/Controls/UrunListele.ascx" TagName="Urun" TagPrefix="uc" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <link href="Styles/number_slideshow.css" rel="stylesheet" type="text/css" />
    <script src='<%=ResolveUrl("~/Scripts/number_slideshow.js") %>' type="text/javascript"></script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <script language="javascript" type="text/javascript">
        $(function () {
            $("#dvSlider").number_slideshow({
                slideshow_autoplay: 'enable', //enable disable
                slideshow_time_interval: '3000',
                slideshow_window_background_color: "#ccc",
                slideshow_window_padding: '1',
                slideshow_window_width: '580',
                slideshow_window_height: '200',
                slideshow_border_size: '1',
                slideshow_border_color: 'black',
                slideshow_show_button: 'enable', //enable disable
                slideshow_show_title: 'disable', //enable disable
                slideshow_button_text_color: '#CCC',
                slideshow_button_background_color: '#333',
                slideshow_button_current_background_color: '#666',
                slideshow_button_border_color: '#000',
                slideshow_loading_gif: '<%=ResolveUrl("~/Styles/images") %>' + '/loading.gif', //loading pic position, you can replace it.
                slideshow_button_border_size: '1'
            });
        });
    </script>
    <div class="radsatir">
        <div id="dvSlider" class="number_slideshow">
            <ul id="ulSlider" runat="server">
            </ul>
            <ul id="ulSliderNav" runat="server" class="number_slideshow_nav">
            </ul>
            <div style="clear: both">
            </div>
        </div>
    </div>
    <div class="radsatir">
        <asp:ListView ID="lstUrunler" runat="server">
            <ItemTemplate>
                <uc:Urun runat="server" ID="urun" UrunID='<%# DataBinder.Eval(Container.DataItem, "ID") %>' />
                
            </ItemTemplate>
        </asp:ListView>
    </div>
</asp:Content>
