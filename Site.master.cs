using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Site : System.Web.UI.MasterPage
{
    DataAccessLayer db = new DataAccessLayer();

    protected void Page_Load(object sender, EventArgs e)
    {
        SonEklenenleriGetir();
        FirsatUrunleriniGetir();
    }

    void SonEklenenleriGetir()
    {
        rpSonEklenenler.DataSource= db.GetDataTable("SELECT top 5 dbo.Urunler.UrunID, dbo.Urunler.UrunAdi, dbo.Urunler.SonFiyat, dbo.Fotograflar.FotoYol FROM dbo.Fotograflar INNER JOIN dbo.Urunler ON dbo.Fotograflar.UrunId = dbo.Urunler.UrunID where FotoVitrin=1 order by UrunId desc");

        rpSonEklenenler.DataBind();
        
    }

    void FirsatUrunleriniGetir()
    {
        rpIndirimliUrunler.DataSource= db.GetDataTable("SELECT top 2 dbo.Urunler.UrunID, dbo.Urunler.UrunAdi, dbo.Urunler.Fiyat, dbo.Urunler.SonFiyat, dbo.Fotograflar.FotoYol FROM dbo.Fotograflar INNER JOIN dbo.Urunler ON dbo.Fotograflar.UrunId = dbo.Urunler.UrunID where FotoVitrin=1 and IndirimOrani>10 order by NEWID()");

        rpIndirimliUrunler.DataBind();
    }
}
