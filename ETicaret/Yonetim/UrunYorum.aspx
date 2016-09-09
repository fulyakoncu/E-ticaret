<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UrunYorum.aspx.cs" Inherits="ETicaret.Yonetim.UrunYorum" %>
<%@ Import Namespace="MiniCore" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<script type="text/javascript">
	    function YorumOku(iID) {
	        PageMethods.GetirYorum(iID, function (result) {
	            var jsondata = JSON.parse(result);
	            $.each(jsondata.Table, function (i, item) {
	                $("#dvYorum").empty();
	                $("#dvYorum").append("Başlık: " + item.BASLIK + "<br />");
	                $("#dvYorum").append("Mesaj: " + item.MESAJ + "<br />");
	                $("#dvYorum").append("Tarih: " + item.TARIH + "<br />");
	                $("#dvYorum").append("IP: " + item.IP + "<br />");
	            });

	        });
	    }
	</script>
	<div class="radsatir baslik">
		Yorumlar
	</div>
	<div class="radsatir">
		<asp:GridView ID="gvYorumlar" AutoGenerateColumns="False" DataKeyNames="ID" 
            runat="server" onselectedindexchanged="gvYorumlar_SelectedIndexChanged">
			<AlternatingRowStyle BackColor="White" />
			<Columns>
            <asp:CommandField HeaderStyle-Width="40px" HeaderText="İşlem" SelectText="Durumunu Değiştir" 
                    ShowSelectButton="True" >
<HeaderStyle Width="40px"></HeaderStyle>
                </asp:CommandField>
				<asp:TemplateField>
					<ItemTemplate>
						<a href="javascript:YorumOku(<%# Eval("ID") %>);">Oku</a>
					</ItemTemplate>

				</asp:TemplateField>
				<asp:BoundField HeaderStyle-Width="35px" DataField="BASLIK" HeaderText="Başlık">
<HeaderStyle Width="35px"></HeaderStyle>
                </asp:BoundField>
				<asp:BoundField HeaderStyle-Width="150px" DataField="TARIH" HeaderText="Tarih">
<HeaderStyle Width="150px"></HeaderStyle>
				</asp:BoundField>
                <asp:TemplateField HeaderStyle-Width="50px" HeaderText="Durumu">
                    <ItemTemplate>
                        <%# MiniCore.cAraclar.GetDescription((MiniCore.eAktifDurum)DataBinder.Eval(Container.DataItem,"AKTIF").ToShort(0)) %>
                    </ItemTemplate>
                </asp:TemplateField>
			</Columns>
			<HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
			<PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
			<RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
		</asp:GridView>
	</div>
	<div class="radsatir">
		<div id="dvYorum">
		</div>
	</div>
</asp:Content>
