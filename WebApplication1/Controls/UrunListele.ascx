<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UrunListele.ascx.cs"
    Inherits="ETicaret.UrunListele" %>
<script type="text/javascript">
function ValidateNumeric()
{
var keyCode = window.event.keyCode;
if (keyCode > 57 || keyCode < 4)
window.event.returnValue = false;
}
</script>
<div class="ucurun">
    <div class="ucresim">
        <asp:Image ID="imgUrun" runat="server" Width="134px" Height="142px" />
    </div>
    <div class="ucmarka">
        <asp:Literal ID="ltMarka" runat="server"></asp:Literal>
        <asp:Label ID="lblMarka" runat="server" Text="" Visible="false"></asp:Label>
    </div>
    <div class="ucmodel">
        <asp:Label ID="lblModel" runat="server" Text=""></asp:Label>
    </div>
    <div class="ucurunalt">
        <div class="ucfiyat-incele">
            <div class="ucfiyat">
                <asp:Label ID="lblFiyat" runat="server" Text=""></asp:Label>
            </div>
            <div class="ucDetay">
                <asp:Literal ID="ltIncele" runat="server"></asp:Literal>
            </div>
        </div>
        <div class="ucsepet-adet">
            <div class="ucadet">
                <asp:Label ID="lblAdet" runat="server" Text="Adet :"></asp:Label>
                <asp:TextBox ID="txtAdet" CssClass="tbadet" runat="server" OnKeyPress="ValidateNumeric()"
                    Text="1" Width="34px"></asp:TextBox>
            </div>
            <div class="ucsepet">
                <asp:Button ID="btnSepeteEkle" CssClass="SepeteAt" runat="server" Text="" OnClick="btnSepeteEkle_Click" />
            </div>
        </div>
    </div>
</div>
