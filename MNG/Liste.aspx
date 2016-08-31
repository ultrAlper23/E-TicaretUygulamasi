<%@ Page Title="" Language="C#" MasterPageFile="~/MNG/MNG.master" AutoEventWireup="true" CodeFile="Liste.aspx.cs" Inherits="MNG_Liste" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <form id="frm" runat="server">
        <asp:GridView ID="gvListe" runat="server" AutoGenerateColumns="False" DataKeyNames="KategoriID" DataSourceID="SqlDataSource1" Width="100%">
            <Columns>
                <asp:BoundField DataField="KategoriID" HeaderText="KategoriID" InsertVisible="False" ReadOnly="True" SortExpression="KategoriID" />
                <asp:BoundField DataField="KategoriAdi" HeaderText="KategoriAdi" SortExpression="KategoriAdi" />
                <asp:HyperLinkField DataNavigateUrlFields="KategoriID" DataNavigateUrlFormatString="UrunEkleme.aspx?id={0}" HeaderText="Ürün" Text="Ekle" />
            </Columns>

        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBNET11ProjeConnectionString %>" SelectCommand="SELECT * FROM [Kategoriler] ORDER BY [KategoriAdi]"></asp:SqlDataSource>
    </form>
</asp:Content>

