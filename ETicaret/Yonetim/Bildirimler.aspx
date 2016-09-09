<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
	CodeBehind="Bildirimler.aspx.cs" Inherits="ETicaret.Yonetim.Bildirimler" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<script type="text/javascript">
	function BildirimOku(iID) {
	    PageMethods.GetirBildirim(iID, function (result) {
	        var jsondata = JSON.parse(result);

	        $.each(jsondata.Table, function (i, item) {
	            $("#dvBildirim").empty();
	            $("#dvBildirim").append("Adı Soyadı: " + item.ADI_SOYADI + "<br />");
	            $("#dvBildirim").append("Email: " + item.EMAIL + "<br />");
	            $("#dvBildirim").append("Başlık: " + item.BASLIK + "<br />");
	            $("#dvBildirim").append("İçerik: " + item.ICERIK + "<br />");
	            $("#dvBildirim").append("Gönderim Zamanı: " + item.OLUSMA_ZAMANI + "<br />");
	            $("#dvBildirim").append("IP: " + item.USERIP + "<br />");
	            $("#<%=txtEmail.ClientID %>").val(item.EMAIL);
	            $("#<%=txtBaslik.ClientID %>").val("RE:"+item.BASLIK);
	        });

	    });
	}
	</script>
	<div class="radsatir baslik">
		Bildirimler
	</div>
	<div class="radsatir">
		<div class="radw100 radleft">
			Email</div>
		<div class="radw200 radleft">
			<asp:TextBox ID="txtEmail" runat="server"></asp:TextBox></div>
	</div>
    	<div class="radsatir">
		<div class="radw100 radleft">
			Başlık</div>
		<div class="radw200 radleft">
			<asp:TextBox ID="txtBaslik" runat="server"></asp:TextBox></div>
	</div>
	<div class="radsatir">
		<div class="radw100 radleft">
			Cevap</div>
		<div class="radw200 radleft">
			<asp:TextBox ID="txtCevap" TextMode="MultiLine" runat="server"></asp:TextBox></div>
	</div>
    <div class="radsatir">
        <asp:Button ID="btnGonder" CssClass="bluebutton" runat="server" Text="Mail Gönder" 
            onclick="btnGonder_Click" />
    </div>
	<div class="radsatir">
		<asp:GridView ID="gvBildirimler" AutoGenerateColumns="false" DataKeyNames="ID" 
            runat="server" onselectedindexchanged="gvBildirimler_SelectedIndexChanged">
			<AlternatingRowStyle BackColor="White" />
			<Columns>
				<asp:TemplateField>
					<ItemTemplate>
						<a href="javascript:BildirimOku(<%# Eval("ID") %>);">Oku</a>
					</ItemTemplate>
				</asp:TemplateField>
				<asp:CommandField HeaderStyle-Width="40px" HeaderText="Cevapla" SelectText="Cevapla"
					ShowSelectButton="True" />
				<asp:BoundField HeaderStyle-Width="35px" DataField="ID" HeaderText="Kayıt No"></asp:BoundField>
				<asp:BoundField HeaderStyle-Width="150px" DataField="ADI_SOYADI" HeaderText="Gönderen">
				</asp:BoundField>
				<asp:BoundField HeaderStyle-Width="150px" DataField="BASLIK" HeaderText="Başlık">
				</asp:BoundField>
			</Columns>
			<HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
			<PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
			<RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
		</asp:GridView>
	</div>
	<div class="radsatir">
		<div id="dvBildirim">
		</div>
	</div>
</asp:Content>
