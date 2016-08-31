<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="UrunDetay.aspx.cs" Inherits="UrunDetay" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<%--    <form id="frm" runat="server">--%>
        <asp:Repeater ID="rpUrun" runat="server" OnItemDataBound="rpUrun_ItemDataBound">
            <ItemTemplate>
                <div id="content">
      <!--Breadcrumb Part Start-->
      <div class="breadcrumb"> <a href="Default.aspx">Home</a> >> <a href="#"><%#Eval("UrunAdi") %></a></div>
      <!--Breadcrumb Part End-->
      <div class="product-info">
        <div class="left">
          <div class="image"> <a href="urunresimleri/600/<%#Eval("FotoYol") %>" title="iPhone" class="cloud-zoom colorbox" id='zoom1' rel="adjustX: 0, adjustY:0, tint:'#000000',tintOpacity:0.2, zoomWidth:360, position:'inside', showTitle:false"> <img src="urunresimleri/350/<%#Eval("FotoYol") %>" title="#" alt="#" id="image" /></a> </div>

          <div class="image-additional"> 
              <asp:Repeater ID="rpFoto" runat="server">
                  <ItemTemplate>
                      <a href="urunresimleri/350/<%#Eval("FotoYol") %>" title="#" class="cloud-zoom-gallery" rel="useZoom: 'zoom1', smallImage: 'urunresimleri/350/<%#Eval("FotoYol") %>' "> <img src="urunresimleri/152/<%#Eval("FotoYol") %>" width="62" title="#" alt="#" /></a> 
                  </ItemTemplate>
              </asp:Repeater>
          </div>
        </div>
        <div class="right">
          <h1><%#Eval("UrunAdi") %></h1>
          <div class="description"> <span>Brand:</span> <a href="#"><%#Eval("MarkaAdi") %></a><br>
            <span>Availability:</span><%#Eval("Stok") %></div>
          <div class="price">Price: <span class="price-old">₺<%#Eval("Fiyat") %></span>
            <div class="price-tag">₺<%#Eval("SonFiyat") %></div>
            <br>
          
          </div>
          <div class="cart">
            <div>
              <div class="qty"> <strong>Qty:</strong> <a href="javascript:void(0);" class="qtyBtn mines">-</a>
                <input type="text" value="1" size="2" name="quantity" class="w30" id="qty">
                <a href="javascript:void(0);" class="qtyBtn plus">+</a>
                <div class="clear"></div>
              </div>
             <asp:LinkButton ID="LinkButton1" runat="server" CssClass="button" CommandName="sepeteAt" CommandArgument='<%#Eval("UrunID") %>'>Sepete Ekle</asp:LinkButton>
            </div>
            <div><span>&nbsp;&nbsp;&nbsp;- OR -&nbsp;&nbsp;&nbsp;</span></div>
            <div><a href="#" class="wishlist">Add to Wish List</a><br>
              <a href="#" class="wishlist">Add to Compare</a></div>
          </div>
            
          <div class="review">

            <div><img alt="0 reviews" src="image/stars-3.png">&nbsp;&nbsp;<a onClick="$('a[href=\'#tab-review\']').trigger('click');">0 reviews</a>&nbsp;&nbsp;|&nbsp;&nbsp;<a onClick="$('a[href=\'#tab-review\']').trigger('click');">Write a review</a></div>
          </div>
          <!-- AddThis Button BEGIN -->
          <div class="addthis_toolbox addthis_default_style "><a class="addthis_button_tweet"></a> <a class="addthis_button_pinterest_pinit"></a> <a class="addthis_counter addthis_pill_style"></a> </div>
          <script type="text/javascript" src="http://s7.addthis.com/js/300/addthis_widget.js#pubid=xa-506f325f57fbfc95"></script>
          <!-- AddThis Button END -->
          <div class="tags"> <b>Tags:</b> <a href="#">Apple</a>, <a href="#">Mobile</a>, <a href="#">Latest</a> </div>
        </div>
      </div>
      <!-- Tabs Start -->
      <div id="tabs" class="htabs"> <a href="#tab-description">Description</a> <a href="#tab-review">Reviews</a> </div>
      <div id="tab-description" class="tab-content">
        <p>iPhone is a revolutionary new mobile phone that allows you to make a call by simply tapping a name or number in your address book, a favorites list, or a call log. It also automatically syncs all your contacts from a PC, Mac, or Internet service. And it lets you select and listen to voicemail messages in whatever order you want just like email.</p>
        <h3>Features:</h3>
        <p>Unrivaled display performance</p>
        <ul>
          <li> 30-inch (viewable) active-matrix liquid crystal display provides breathtaking image quality and vivid, richly saturated color.</li>
          <li> Support for 2560-by-1600 pixel resolution for display of high definition still and video imagery.</li>
          <li> Wide-format design for simultaneous display of two full pages of text and graphics.</li>
          <li> Industry standard DVI connector for direct attachment to Mac- and Windows-based desktops and notebooks</li>
          <li> Incredibly wide (170 degree) horizontal and vertical viewing angle for maximum visibility and color performance.</li>
          <li> Lightning-fast pixel response for full-motion digital video playback.</li>
          <li> Support for 16.7 million saturated colors, for use in all graphics-intensive applications.</li>
        </ul>
      </div>
      <div class="tab-content" id="tab-review">

          <asp:Repeater ID="rpYorum" runat="server">
              <ItemTemplate>
                  <div class="review-list">
          <div class="author"><b><%#Eval("UserName") %></b> on  <%#Eval("Tarih").ToString().Substring(0,10) %></div>   <%--string.Format("{0:DD/mm/yyyy}",--%>
          <div class="text"><%#Eval("Yorum") %></div>
        </div>
              </ItemTemplate>
          </asp:Repeater>

        

          <asp:LoginView ID="LoginView1" runat="server">
              <LoggedInTemplate>
                  <h2 id="review-title">Write a review</h2>
&nbsp;<br>
          <b>Your Review:</b>
        <textarea style="width: 98%;" rows="8" cols="40" name="text"></textarea>
        <br>
        <br>
        <div class="buttons">
          <div class="right"><a class="button" id="button-review">Continue</a></div>
        </div>
              </LoggedInTemplate>
              <AnonymousTemplate>
                  Yorum yapabilmek için <asp:LoginStatus ID="LoginStatus1" runat="server" LoginText="Giriş Yapın"/>
              </AnonymousTemplate>
          </asp:LoginView>

        
      </div>
      <!-- Tabs End -->
      <!-- Related Products Start -->
      <div class="box">
        <div class="box-heading">Related Products (4)</div>
        <div class="box-content">
          <div class="box-product">
            <div>
              <div class="image"><a href="product.html"><img alt="iPad Classic" src="image/product/ipod_classic_1-152x152.jpg"></a></div>
              <div class="name"><a href="product.html">iPad Classic</a></div>
              <div class="price"> <span class="price-old">$119.50</span> <span class="price-new">$107.75</span> </div>
              <a class="button">Add to Cart</a></div>
          </div>
        </div>
      </div>
      <!-- Related Products End -->
    </div>
            </ItemTemplate>
        </asp:Repeater>
<%--    </form>--%>

    
</asp:Content>

