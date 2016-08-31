<%@ Page Title="" Language="C#" MasterPageFile="~/MNG/MNG.master" AutoEventWireup="true" CodeFile="UrunEkleme.aspx.cs" Inherits="MNG_UrunEkleme" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <form id="frm" runat="server">
        

        <table class="nav-justified">
            <tr>
                <td>Kategori :</td>
                <td>
                    <asp:DropDownList ID="ddlKategori" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlKategori_SelectedIndexChanged"></asp:DropDownList></td>
            </tr>
            <tr>
                <td>Marka :</td>
                <td>
                    <asp:DropDownList ID="ddlMarka" runat="server"></asp:DropDownList></td>
            </tr>
            <tr>
                <td>Ürün Adı :</td>
                <td>
                    <asp:TextBox ID="txtUrun" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td>Stok :</td>
                <td>
                    <asp:TextBox ID="txtStok" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td>Fiyat :</td>
                <td>
                    <asp:TextBox ID="txtFiyat" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFiyat" ErrorMessage="Ürün fiyatı girilmelidir."></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>İndirim Oranı (%) :</td>
                <td>
                    <asp:TextBox ID="txtIndirim" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td>Detay :</td>
                <td>
                    <asp:TextBox ID="txtDetay" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td>Aktif :</td>
                <td>
                    <asp:CheckBox ID="checkAktif" runat="server" /></td>
            </tr>
            <tr>
                <td>Vitrin :</td>
                <td>
                    <asp:CheckBox ID="checkVitrin" runat="server" /></td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>
                    <asp:Button ID="btnEkle" runat="server" Text="EKLE" OnClick="btnEkle_Click" /></td>
            </tr>
        </table>
        




        
        




    </form>



</asp:Content>

