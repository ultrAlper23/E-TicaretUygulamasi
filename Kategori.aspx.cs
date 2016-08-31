using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Kategori : System.Web.UI.Page
{
    DataAccessLayer db = new DataAccessLayer();
    string id = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        id = Request.QueryString["id"];

        if (string.IsNullOrEmpty(id))
            Response.Redirect("Default.aspx");

        List<Parametreler> liste = new List<Parametreler>();
        liste.Add(new Parametreler { Name = "@id", Value = id });


        DataTable dt = db.GetDataTable("SELECT dbo.Urunler.UrunID, dbo.Urunler.UrunAdi, dbo.Urunler.SonFiyat, dbo.Fotograflar.FotoYol FROM dbo.Fotograflar right JOIN  dbo.Urunler ON dbo.Fotograflar.UrunId = dbo.Urunler.UrunID  where KategoriId=@id and (FotoVitrin=1 or FotoVitrin is null)", liste);
        //sorgu sonucunda fotoyolunun null gelme durumunda ürün resmi için standart bir resim gösterilsin.

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            //if(dt.Rows["FotoYol"]==null)  burası hata üretir.
            if (dt.Rows[i]["FotoYol"] == DBNull.Value)
                dt.Rows[i]["FotoYol"] = "yok.jpg";
        }

        lvVitrin.DataSource = dt;
        lvVitrin.DataBind();

    }
    
    protected void lvVitrin_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        AlisVeris.SepeteAt(sender, e, e.CommandArgument.ToString());
    }
}