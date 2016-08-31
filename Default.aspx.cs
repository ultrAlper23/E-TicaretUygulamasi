using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        VitrinGetir();
    }

    DataAccessLayer db = new DataAccessLayer();
    void VitrinGetir()
    {
        lvVitrin.DataSource = db.GetDataTable("SELECT dbo.Urunler.UrunID, dbo.Urunler.UrunAdi, dbo.Urunler.SonFiyat, dbo.Fotograflar.FotoYol FROM dbo.Fotograflar INNER JOIN dbo.Urunler ON dbo.Fotograflar.UrunId = dbo.Urunler.UrunID where FotoVitrin=1 and Vitrin=1 and Aktif=1");
        lvVitrin.DataBind();
    }
    protected void lvVitrin_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        AlisVeris.SepeteAt(sender, e, e.CommandArgument.ToString());
    }
}