using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class webUserControls_kategoriler : System.Web.UI.UserControl
{
    DataAccessLayer db = new DataAccessLayer();

    protected void Page_Load(object sender, EventArgs e)
    {
        KategoriGetir();
    }

    void KategoriGetir()
    {
        rpKategori.DataSource = db.GetDataTable("select * from Kategoriler order by KategoriAdi");
        rpKategori.DataBind();
    }
}