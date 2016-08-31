using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MNG_Kategori : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    DataAccessLayer db = new DataAccessLayer();
    protected void btnEkle_Click(object sender, EventArgs e)
    {
        List<Parametreler> liste = new List<Parametreler>();
        liste.Add(new Parametreler { Name = "@ad", Value = txtKategori.Text });

        db.RunASqlStatement("insert Kategoriler values(@ad)", liste);

        txtKategori.Text = "";

    }
}