<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UrunGrupEkle.aspx.cs" Inherits="ETicaret.Yonetim.UrunGrupEkle" %>

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
    <form id="form1" runat="server">
    <div class="page radw400" style="padding:20px">
        <div class="radsatir">
            <asp:GridView ID="gvUrun" AutoGenerateColumns="False" DataKeyNames="ID" 
                runat="server" onrowdatabound="gvUrun_RowDataBound">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                <asp:TemplateField>
                <ItemTemplate>
                    <asp:CheckBox ID="chcEkDurum" runat="server" />
                </ItemTemplate>
                </asp:TemplateField>
                    <asp:BoundField HeaderStyle-Width="35px" DataField="ID" HeaderText="Kayıt No">
                        <HeaderStyle Width="20px"></HeaderStyle>
                    </asp:BoundField>
                    <asp:BoundField HeaderStyle-Width="200px" DataField="GRUP_ADI" HeaderText="Kodu">
                        <HeaderStyle Width="200px"></HeaderStyle>
                    </asp:BoundField>
                </Columns>
                <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
            </asp:GridView>
        </div>
        <div class="radsatir">
            <asp:Button ID="btnKaydet" CssClass="greenbutton" runat="server" Text="Kaydet" 
                onclick="btnKaydet_Click"/>
        </div>
    </div>
    </form>
</body>
</html>
