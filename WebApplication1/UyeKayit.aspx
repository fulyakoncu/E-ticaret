<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="UyeKayit.aspx.cs" Inherits="ETicaret.UyeKayit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        $(function () {
            $("#<%= txtDogumTar.ClientID %>").datepicker({
                defaultDate: "-20y"
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="radw760 form">
        <div class="radsatir baslik">
            Üyelik Bilgileri</div>
        <div class="radsatir">
            <div class="radw400">
                <div class="radw120 radleft">
                    Adınız</div>
                <div class="radw260 radright">
                    <asp:TextBox ID="txtAd" Width="180" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ErrorMessage="*" ControlToValidate="txtAd" ValidationGroup="kayit"></asp:RequiredFieldValidator> 
                    </div>
            </div>
        </div>
        <div class="radsatir">
            <div class="radw400">
                <div class="radw120 radleft">
                    Soyadınız</div>
                <div class="radw260 radright">
                    <asp:TextBox ID="txtSoyad" Width="180" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" 
                        runat="server" ErrorMessage="*" ControlToValidate="txtSoyad" 
                        ValidationGroup="kayit"></asp:RequiredFieldValidator> 
                    </div>
            </div>
        </div>
        <div class="radsatir">
            <div class="radw400">
                <div class="radw120 radleft">
                    E Posta</div>
                <div class="radw260 radright">
                    <asp:TextBox ID="txtEmail" Width="180" runat="server"></asp:TextBox>                                        
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                        ErrorMessage="*" ControlToValidate="txtEmail" ValidationGroup="kayit"></asp:RequiredFieldValidator> 
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                        ErrorMessage="*" ControlToValidate="txtEmail" 
                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
                        ValidationGroup="kayit"></asp:RegularExpressionValidator>                    
                    <asp:CustomValidator ID="cvEmail" runat="server" ControlToValidate="txtSifre" 
                        ErrorMessage="*" onservervalidate="cvEmail_ServerValidate" 
                        ValidationGroup="kayit" Display="Dynamic"></asp:CustomValidator>
                    </div>
            </div>
        </div>
        <div class="radsatir">
            <div class="radw400">
                <div class="radw120 radleft">
                    E Posta (Tekrar)</div>
                <div class="radw260 radright">
                    <asp:TextBox ID="txtEmail2" Width="180" runat="server"></asp:TextBox>

                    <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="*" 
                        ControlToCompare="txtEmail" ControlToValidate="txtEmail2" 
                        ValidationGroup="kayit"></asp:CompareValidator>                   
                    </div>
            </div>
        </div>
        <div class="radsatir">
            <div class="radw400">
                <div class="radw120 radleft">
                    Şifreniz</div>
                <div class="radw260 radright">
                    <asp:TextBox ID="txtSifre" Width="180" TextMode="Password" runat="server"></asp:TextBox>                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                        ErrorMessage="*" ControlToValidate="txtSifre" ValidationGroup="kayit"></asp:RequiredFieldValidator> 
                    </div>
            </div>
        </div>
        <div class="radsatir">
            <div class="radw400">
                <div class="radw120 radleft">
                    Şifreniz (tekrar)</div>
                <div class="radw260 radright">
                    <asp:TextBox ID="txtSifre2" Width="180" TextMode="Password" runat="server"></asp:TextBox><asp:CompareValidator
                        ID="CompareValidator2" runat="server" ErrorMessage="*" 
                        ControlToCompare="txtSifre" ControlToValidate="txtSifre2" 
                        ValidationGroup="kayit"></asp:CompareValidator></div>
            </div>
        </div>
        <div class="radsatir">
            <div class="radw400">
                <div class="radw120 radleft">
                    Doğum Tarihiniz</div>
                <div class="radw260 radright">
                    <asp:TextBox ID="txtDogumTar" Width="180" runat="server"></asp:TextBox>
                    <asp:RangeValidator ID="RangeValidator1" runat="server" ErrorMessage="*" 
                        ControlToValidate="txtDogumTar" MaximumValue="01.01.1994" 
                        MinimumValue="01.01.1900" Type="Date" ValidationGroup="kayit"></asp:RangeValidator>
                </div>
            </div>
        </div>
        <div class="radsatir">
            <div class="radw400">
                <div class="radw120 radleft">
                    Cinsiyetiniz </div>
                <div class="radw260 radright">
                    <asp:RadioButtonList ID="rblCinsiyet" RepeatDirection="Horizontal" runat="server">
                    </asp:RadioButtonList>
                </div>
            </div>
        </div>
        <div class="radsatir">
            <div class="radw400">
                <div class="radw120 radleft">
                    Adres</div>
                <div class="radw260 radright">
                    <asp:TextBox ID="txtAdres" TextMode="MultiLine" Width="180" runat="server"></asp:TextBox>
                </div>
            </div>
        </div>
        <div class="radsatir">
            <div class="radw400">
                <div class="radw120 radleft">
                    Şehir</div>
                <div class="radw260 radright">
                    <asp:DropDownList ID="ddlIl" Width="180" runat="server">
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <div class="radsatir">
            <div class="radw400">
                <div class="radw120 radleft">
                    Cep Telefonu</div>
                <div class="radw260 radright">
                    <asp:TextBox ID="txtCepTel" Width="180" runat="server"></asp:TextBox></div>
            </div>
        </div>
        <div class="radsatir">
            <div class="radw400">
                <div class="radw120 radleft">
                    Ev Telefonu</div>
                <div class="radw260 radright">
                    <asp:TextBox ID="txtEvTel" Width="180" runat="server"></asp:TextBox></div>
            </div>
        </div>
        <div class="radsatir">
            <asp:Button ID="kaydetbtn" runat="server" OnClick="kaydetbtn_Click" Text="Kaydet!" />
        </div>
    </div>
</asp:Content>
