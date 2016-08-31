using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MNG_UrunEkleme : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            kategori = Request.QueryString["id"];
            KategoriGetir();
            ddlKategori.SelectedValue = kategori;
            MarkaGetir();
        }
    }

    DataAccessLayer db = new DataAccessLayer();
    string kategori = "";


    void KategoriGetir()
    {
        ddlKategori.DataValueField = "KategoriID";
        ddlKategori.DataTextField = "KategoriAdi";
        ddlKategori.DataSource = db.GetDataTable("select * from Kategoriler order by KategoriAdi");
        ddlKategori.DataBind();
    }

    void MarkaGetir()
    {
        List<Parametreler> liste = new List<Parametreler>();
        liste.Add(new Parametreler { Name = "@id", Value = ddlKategori.SelectedValue });

        ddlMarka.DataValueField = "MarkaID";
        ddlMarka.DataTextField = "MarkaAdi";
        ddlMarka.DataSource = db.GetDataTable("select * from Markalar where MarkaID  in (select MarkaID from KategoriMarka where KategoriId=@id)", liste);
        ddlMarka.DataBind();
    }

    protected void ddlKategori_SelectedIndexChanged(object sender, EventArgs e)
    {
        MarkaGetir();
    }




    protected void btnEkle_Click(object sender, EventArgs e)
    {
        int oran;
        int.TryParse(txtIndirim.Text, out oran);//txtOran dönüştürülebilir bir değere sahipse OK, boş bırakılmış veya dönüştürülemezse oran 0 kabul edilir.


        List<Parametreler> liste = new List<Parametreler>();
        liste.Add(new Parametreler { Name = "@kategori", Value = ddlKategori.SelectedValue });
        liste.Add(new Parametreler { Name = "@marka", Value = ddlMarka.SelectedValue });
        liste.Add(new Parametreler { Name = "@urun", Value = txtUrun.Text });
        liste.Add(new Parametreler { Name = "@stok", Value = txtStok.Text });//stok fiyat vs gibi alanların sayısallığından emin olmak lazım. (burda kontrol edilmedi.)

        double sonFiyat, fiyat;
        double.TryParse(db.ToCurrencyCode(txtFiyat.Text), out fiyat);
        sonFiyat = double.Parse(db.ToCurrencyCode(txtFiyat.Text));

        sonFiyat = fiyat - (fiyat * oran / 100);

        liste.Add(new Parametreler { Name = "@fiyat", Value = fiyat });
        liste.Add(new Parametreler { Name = "@indirim", Value = oran });
        liste.Add(new Parametreler { Name = "@sonfiyat", Value = sonFiyat });
        liste.Add(new Parametreler { Name = "@detay", Value = txtDetay.Text });
        liste.Add(new Parametreler { Name = "@aktif", Value = checkAktif.Checked });
        liste.Add(new Parametreler { Name = "@vitrin", Value = checkVitrin.Checked });


        db.RunASqlStatement("insert Urunler values (@kategori,@marka, @urun, @stok, @fiyat, @indirim, @sonfiyat, @detay, @aktif, @vitrin)", liste);


        txtUrun.Text = "";
        txtStok.Text = "";
        txtIndirim.Text = "";
        txtFiyat.Text = "";
        txtDetay.Text = "";
        checkVitrin.Checked = false;
        checkAktif.Checked = false;

    }
}