using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MNG_UrunListesi : System.Web.UI.Page
{
    DataAccessLayer db = new DataAccessLayer();

    protected void Page_Load(object sender, EventArgs e)
    {
        List<Parametreler> liste = new List<Parametreler>();

        dlUrun.DataSource = db.GetDataTable("select UrunID,UrunAdi,MarkaAdi from Urunler,Markalar where Markalar.MarkaID=Urunler.MarkaId");
        dlUrun.DataBind();
    }
}