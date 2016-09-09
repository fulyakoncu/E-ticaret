<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Kategoriler.aspx.cs" Inherits="ETicaret.Yonetim.Kategoriler" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        window.onload = OnLoad;
        function OnLoad() {
            var links = document.getElementById("<%=tvKategoriler.ClientID %>").getElementsByTagName("a");
            $.each(links, function (i, item) {
                if (item.className == "MainContent_tvKategoriler_0")
                    item.setAttribute("href", "javascript:OnTreeClick(\"" + item.id + "\", \"" + item.getAttribute("href") + "\")");
            });
        }
        function OnTreeClick(id, attribute) {

            var nodeValue = GetNodeValue(document.getElementById(id));
            PageMethods.GetirKategori(nodeValue, function (result) {
                var jsondata = JSON.parse(result);

                $.each(jsondata.Table, function (i, item) {
                    $("#<%=lblID.ClientID %>").text(item.ID);
                    $("#<%=hfID.ClientID %>").val(item.ID);
                    $("#<%=txtAdi.ClientID %>").val(item.ADI);
                    $("#<%=txtSirasi.ClientID %>").val(item.SIRANO);
                    $("#<%=chcAktif.ClientID %>")[0].checked = item.AKTIF == 1;
                    changeCheckboxText($("#<%=chcAktif.ClientID %>")[0]);
                    //$("#MainContent_ddlKategoriler")[0].options.0.selected
                    var ddlKat = $("#<%=ddlKategoriler.ClientID %>")[0];
                    for (i = 0; i < ddlKat.length; i++) {
                        if (ddlKat[i].value == item.NODEID) {
                            ddlKat[i].selected = true;
                        }
                    }
                });

            });
            //alert(nodeLink.innerHTML + " clicked "+nodeValue);
        }
        function GetNodeValue(node) {
            //node value
            var nodeValue = "";
            var nodePath = node.href.substring(node.href.indexOf(",") + 2, node.href.length - 2);
            var nodeValues = nodePath.split("\\");
            if (nodeValues.length > 1)
                nodeValue = nodeValues[nodeValues.length - 1];
            else
                nodeValue = nodeValues[0].split(',')[1].substring(2);
            return nodeValue.substring(0,nodeValue.length-2);
        }
        function changeCheckboxText(checkbox) {
            if (checkbox.checked)
                checkbox.nextSibling.innerHTML = 'Aktif';
            else
                checkbox.nextSibling.innerHTML = 'Pasif';
        }
    </script>
    <div class="radsatir">
        <div class="baslik">
            Kategori Tanımlamaları</div>
    </div>
    <asp:HiddenField ID="hfID" runat="server" />
    <div class="radsatir">
        <div class="radleft radw260">
            <asp:TreeView ID="tvKategoriler" runat="server">
            </asp:TreeView>
        </div>
        <div class="radright radw260">
            <div class="radsatir">
                Kayıt No:<asp:Label ID="lblID" runat="server" Text=""></asp:Label>
            </div>
            <div class="radsatir">
                Kategorisi
            </div>
            <div class="radsatir">
                <asp:DropDownList ID="ddlKategoriler" runat="server">
                </asp:DropDownList>
            </div>
            <div class="radsatir">
                Adı
            </div>
            <div class="radsatir">
                <asp:TextBox ID="txtAdi" runat="server"></asp:TextBox>
            </div>
            <div class="radsatir">
                Kategori Resmi
            </div>
            <div class="radsatir">
                <asp:FileUpload ID="fuKatResmi" runat="server" />
            </div>
            <div class="radsatir">
                <asp:CheckBox ID="chcAktif"  Text="Pasif" onclick="changeCheckboxText(this);" runat="server" />
                &nbsp;&nbsp;Sırası<asp:TextBox ID="txtSirasi" Width="50" runat="server"></asp:TextBox>
            </div>
            <div class="radsatir">
 <asp:Button ID="btnTemizle" runat="server" CssClass="bluebutton" Text="Temizle" onclick="btnTemizle_Click" />
                <asp:Button ID="btnKaydet"
                    runat="server" CssClass="greenbutton" Text="Kaydet" onclick="btnKaydet_Click" />
            </div>
        </div>
    </div>
</asp:Content>
