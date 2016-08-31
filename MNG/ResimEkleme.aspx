<%@ Page Title="" Language="C#" MasterPageFile="~/MNG/MNG.master" AutoEventWireup="true" CodeFile="ResimEkleme.aspx.cs" Inherits="MNG_ResimEkleme" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <form id="frm" runat="server">
    <table style="width: 100%; border: 1px solid #000000">
        <tr>
            <td colspan="2">
                <asp:Label ID="lblUrun" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td>Resim Sayısı :</td>
            <td>
                <asp:Label ID="lblSayi" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td>Uyarı :</td>
            <td>
                <asp:Label ID="lblVitrin" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td>Resim :</td>
            <td>
                <asp:FileUpload ID="fu" runat="server" /></td>
        </tr>
        <tr>
            <td>Vitrin Resmi :</td>
            <td>
                <asp:CheckBox ID="chkVitrin" runat="server" /></td>
        </tr>
        <tr>
            <td></td>
            <td>
                <asp:Button ID="btnEkle" runat="server" Text="EKLE" OnClick="btnEkle_Click" /></td>
        </tr>
    </table>
    </form>
</asp:Content>

