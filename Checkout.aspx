<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Checkout.aspx.cs" Inherits="Checkout" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="content">
        <!--Breadcrumb Part Start-->
        <div class="breadcrumb"><a href="index.html">Home</a> » <a href="">Shopping Cart</a></div>
        <!--Breadcrumb Part End-->
        <h1>Checkout</h1>
        <div class="checkout">
            <asp:LoginView ID="LoginView1" runat="server">
                <AnonymousTemplate>
                    <div class="checkout">
                        <div class="checkout-heading">Step 1: Checkout Options</div>
                        <div class="checkout-content" style="display: block;">
                            <div class="left">
                                <h2>New Customer</h2>
                                <asp:CreateUserWizard ID="CreateUserWizard1" runat="server">
                                    <WizardSteps>
                                        <asp:CreateUserWizardStep ID="CreateUserWizardStep1" runat="server">
                                        </asp:CreateUserWizardStep>
                                        <asp:CompleteWizardStep ID="CompleteWizardStep1" runat="server">
                                        </asp:CompleteWizardStep>
                                    </WizardSteps>
                                </asp:CreateUserWizard>
                            </div>
                            <div class="right" id="login">
                                <h2>Returning Customer</h2>
                                <asp:Login ID="Login1" runat="server">
                                </asp:Login>
                            </div>
                        </div>
                    </div>
                </AnonymousTemplate>
                <LoggedInTemplate>
                    <div class="checkout">
                        <div class="checkout-heading">Step 2: Shipping Details</div>
                        <div class="checkout-content">
                            <table class="form">
                                <thead>
                                    <tr>
                                        <th>Adres</th>
                                        <th>Seçim</th>
                                    </tr>
                                    </thead>
                                <tbody>
                                <asp:Repeater ID="rpAdres" runat="server" OnItemCommand="rpAdres_ItemCommand">
                                    <ItemTemplate>
                                        <tr>
                                            <td><%#Eval("Isim") %></td>
                                            <td>
                                                <asp:RadioButton ID="rbSec" runat="server" Text='<%#Eval("Isim") %>' OnCheckedChanged="Isaret"/>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                                    </tbody>
                            </table>
                            <div class="buttons">
                                <div class="right">
                                    <input type="button" class="button" id="button1" value="Continue">
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="checkout">
                        <div class="checkout-heading">Step 3: Confirm Order</div>
                        <div class="checkout-content">
                            <div class="checkout-product">
                                <table>
                                    <thead>
                                        <tr>
                                            <td class="name">Product Name</td>
                                            <td class="model">Model</td>
                                            <td class="quantity">Quantity</td>
                                            <td class="price">Price</td>
                                            <td class="total">Total</td>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td class="name"><a href="#">Canon EOS 5D</a></td>
                                            <td class="model">Product 3</td>
                                            <td class="quantity">1</td>
                                            <td class="price">£61.33</td>
                                            <td class="total">£61.33</td>
                                        </tr>
                                    </tbody>
                                    <tfoot>
                                        <tr>
                                            <td class="price" colspan="4"><b>Sub-Total:</b></td>
                                            <td class="total">£51.11</td>
                                        </tr>
                                        <tr>
                                            <td class="price" colspan="4"><b>Flat Shipping Rate:</b></td>
                                            <td class="total">£3.19</td>
                                        </tr>
                                        <tr>
                                            <td class="price" colspan="4"><b>Total:</b></td>
                                            <td class="total">£66.37</td>
                                        </tr>
                                    </tfoot>
                                </table>
                            </div>
                            <div class="buttons">
                                <div class="right">
                                    <input type="button" class="button" id="button-confirm" value="Confirm Order">
                                </div>
                            </div>
                        </div>
                    </div>
                </LoggedInTemplate>
            </asp:LoginView>
        </div>
        </div>
</asp:Content>
