<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UrunResimEkle.aspx.cs"
    Inherits="ETicaret.Yonetim.UrunResimEkle" %>
<%@ Import Namespace="MiniCore" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="~/Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="~/Styles/core.css" rel="stylesheet" type="text/css" />
    <link href="~/Styles/form-elements.css" rel="stylesheet" type="text/css" />
    <link href="~/Styles/toastmessage/toastmessage.css" rel="stylesheet" type="text/css" />
    <script src='<%=ResolveUrl("~/Scripts/jquery-1.6.2.min.js") %>' type="text/javascript"></script>
    <script src='<%=ResolveUrl("~/Scripts/jquery.toastmessage.js") %>' type="text/javascript"></script>
    <title></title>
</head>
<body>
    <script type="text/javascript">
        function changeCheckboxText(checkbox) {
            if (checkbox.checked)
                checkbox.nextSibling.innerHTML = 'Evet';
            else
                checkbox.nextSibling.innerHTML = 'Hayır';
        }
    </script>
    <form id="form1" runat="server">
    <div class="page radw400" style="padding:20px">
        <div class="radsatir">
            <div class="baslik">
                Ürüne Resim Ekleme</div>
        </div>
        <div class="radsatir">
            <div class="radw100 radleft">
                Kayıt No:</div>
            <div class="radw100 radleft">
                <asp:Label ID="lblID" runat="server" Text="0"></asp:Label></div>
        </div>
        <div class="radsatir">
            <div class="radw260 radleft">
                Resim</div>
            <div class="radw260 radleft">
                <asp:FileUpload ID="fuResim" runat="server" />
            </div>
        </div>
        <div class="radsatir">
            <div class="radw260 radleft">
                Slider :<asp:CheckBox ID="chcSlider" Text="Hayır" onclick="changeCheckboxText(this);" runat="server" />
            </div>
            <div class="radw260 radleft">
                Ana Resim: <asp:CheckBox ID="chcAnaResim" Text="Hayır" onclick="changeCheckboxText(this);" runat="server" />
            </div>
        </div>
        <div class="radsatir">
            <asp:Button ID="btnSil" CssClass="redbutton" runat="server" Text="Sil" OnClick="btnSil_Click" />
            <asp:Button ID="btnYeniKayit" CssClass="bluebutton" runat="server" Text="Yeni Kayıt" OnClick="btnYeniKayit_Click" />
            <asp:Button ID="btnKaydet" CssClass="greenbutton" runat="server" Text="Kaydet" OnClick="btnKaydet_Click" />
        </div>
        <div class="radsatir">
            <asp:GridView ID="gvUResim" runat="server" AutoGenerateColumns="false" DataKeyNames="ID"
                OnSelectedIndexChanged="gvUResim_SelectedIndexChanged">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:CommandField HeaderStyle-Width="40px" HeaderText="Seç" SelectText="Seç" ShowSelectButton="True" />
                    <asp:BoundField HeaderStyle-Width="35px" DataField="ID" HeaderText="Kayıt No"></asp:BoundField>
                    <asp:BoundField HeaderStyle-Width="150px" DataField="RESIMADI" HeaderText="Resim Adı">
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Ana Resim">
                        <ItemTemplate>
                            <%# MiniCore.cAraclar.GetDescription((MiniCore.eEvetHayir)DataBinder.Eval(Container.DataItem,"ANARESIM").ToShort(0)) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Slider">
                        <ItemTemplate>
                            <%# MiniCore.cAraclar.GetDescription((MiniCore.eEvetHayir)DataBinder.Eval(Container.DataItem,"SLIDERRESIM").ToShort(0)) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
            </asp:GridView>
        </div>
    </div>
    </form>
</body>
</html>
