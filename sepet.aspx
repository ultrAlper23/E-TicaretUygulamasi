<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="sepet.aspx.cs" Inherits="sepet" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="updatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>

            <div class="cart-info">
                <table>
                    <thead>
                        <tr>
                            <td class="image">Image</td>
                            <td class="name">Product Name</td>
                            <td class="quantity">Quantity</td>
                            <td class="price">Unit Price</td>
                            <td class="total">Total</td>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="rpSepet" runat="server" OnItemCommand="rpSepet_ItemCommand">
                            <ItemTemplate>
                                <tr>
                                    <td class="image"><a href="#">
                                        <img title="Bag Lady" alt="Bag Lady" src="urunresimleri/152/<%#Eval("yol") %>" width="60" height="60"></a></td>
                                    <td class="name"><a href="#"><%#Eval("isim") %></a></td>
                                    <td class="quantity">
                                        <input id="quantity" name="quantity" type="text" size="1" value="<%#Eval("adet") %>" name="" class="w30">
                                        &nbsp;
                  <asp:ImageButton ID="ibUpdate" CommandName="guncelle" CommandArgument='<%#Eval("id") %>'  runat ="server" ImageUrl="~/image/update.png" />
                  <asp:ImageButton ID="ibRemove" CommandName="sil" CommandArgument='<%#Eval("id") %>' runat="server" ImageUrl="~/image/remove.png" />
                                    <td class="price">₺<%#Eval("fiyat")%></td>
                                    <td class="total">₺<%#Eval("tutar")%></td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
            </div>
            <div class="cart-total">
                <table id="total">
                    <tbody>
                        <tr>
                            <td class="right"><b>Toplam :</b></td>
                            <td class="right">₺
                                <asp:Label ID="lblToplam" runat="server" Text="Label"></asp:Label></td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <div class="buttons">
                <div class="right"><a class="button" href="../checkout.aspx">Checkout</a></div>
                <div class="center"><a class="button" href="../Default.aspx">Continue Shopping</a></div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>








