<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<%--    <form id="frm" runat="server">--%>
        <div class="box-product">
            <asp:ListView ID="lvVitrin" runat="server" OnItemCommand="lvVitrin_ItemCommand">
                <ItemTemplate>
                    <div>
                        <div class="image" ><a href="UrunDetay.aspx?id=<%#Eval("UrunID") %>">
                            <img src="urunresimleri/152/<%#Eval("FotoYol") %>" alt="<%#Eval("UrunAdi") %>"  /> </a> </div>
                        <div class="name"><%#Eval("UrunAdi") %></div>
                        <div class="price">₺<%#Eval("SonFiyat") %></div>
                        <div class="cart">
                            <asp:LinkButton ID="LinkButton1" runat="server" CssClass="button" CommandName="sepeteAt" CommandArgument='<%#Eval("UrunID") %>'>Sepete Ekle</asp:LinkButton>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:ListView>
        </div>
 <%--   </form>--%>
</asp:Content>

