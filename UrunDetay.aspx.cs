using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UrunDetay : System.Web.UI.Page
{
    DataAccessLayer db = new DataAccessLayer();
    string urunID = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        urunID = Request.QueryString["id"];
        if (!string.IsNullOrEmpty(urunID))
        {
            UrunBilgisiGetir();
        }
    }

    void UrunBilgisiGetir()
    {
        List<Parametreler> liste = new List<Parametreler>();
        liste.Add(new Parametreler { Name = "@id", Value = urunID });

        DataTable dtUrun = db.GetDataTable("SELECT   dbo.Urunler.*, dbo.Fotograflar.FotoYol, MarkaAdi FROM  dbo.Fotograflar right JOIN dbo.Urunler ON dbo.Fotograflar.UrunId = dbo.Urunler.UrunID inner join Markalar on Markalar.MarkaID=Urunler.MarkaId where Urunler.UrunID=@id and (FotoVitrin=1 or FotoVitrin is null)", liste);

        //resim yoksa standart resim kullanılsın
        for (int i = 0; i < dtUrun.Rows.Count; i++)
        {
            if (dtUrun.Rows[i]["FotoYol"] == DBNull.Value)
                dtUrun.Rows[i]["FotoYol"] = "yok.jpg";

            if (dtUrun.Rows[i]["IndirimOrani"].ToString()=="0")
            {
                dtUrun.Rows[i]["Fiyat"] = DBNull.Value;
            }
        }
        //indirim yoksa eski fiyat göstermemek için
        
        rpUrun.DataSource = dtUrun;
        rpUrun.DataBind();

    }
    protected void rpUrun_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
      //rpUrune bilgi yüklendikten sonra (ürün bilgileri ve vitrin resmi getirildikten sonra) bu metot devreye girer.

        Repeater rpt = (Repeater)e.Item.FindControl("rpFoto"); //rpUrun içide kontrol id si verilerek bulunur. Aynen masterpage üzerinde bir kontrolün değerini değiştirmek gibi.

        List<Parametreler> liste = new List<Parametreler>();
        liste.Add(new Parametreler { Name = "@id", Value = urunID });

        rpt.DataSource = db.GetDataTable("select * from Fotograflar where UrunId=@id order by FotoVitrin desc", liste);
        rpt.DataBind();

        YorumGetir(sender, e);

    }

    void YorumGetir(object sender, RepeaterItemEventArgs e)
    {
        Repeater rpt = (Repeater)e.Item.FindControl("rpYorum"); 

        List<Parametreler> liste = new List<Parametreler>();
        liste.Add(new Parametreler { Name = "@id", Value = urunID });

        rpt.DataSource = db.GetDataTable("SELECT dbo.Yorumlar.Yorum, dbo.Yorumlar.Tarih, dbo.Users.UserName FROM dbo.Users INNER JOIN dbo.Yorumlar ON dbo.Users.UserId = dbo.Yorumlar.UserId and UrunId=@id and Onay=1 order by Tarih desc", liste);
        rpt.DataBind();
    }
}