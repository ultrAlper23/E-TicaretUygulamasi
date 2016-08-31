<%@ Control Language="C#" AutoEventWireup="true" CodeFile="kategoriler.ascx.cs" Inherits="webUserControls_kategoriler" %>

    <asp:Repeater ID="rpKategori" runat="server">
        <ItemTemplate>
            <li>
                <a href="Kategori.aspx?id=<%#Eval("KategoriID") %>"><%#Eval("KategoriAdi") %></a>
            </li>
        </ItemTemplate>
    </asp:Repeater>

