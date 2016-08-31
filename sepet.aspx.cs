using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class sepet : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (HttpContext.Current.Session["sepet"] != null)
            {
                DataTable dtSession = (DataTable)HttpContext.Current.Session["sepet"];
                rpSepet.DataSource = dtSession;
                rpSepet.DataBind();
            }
        }

        AlisVeris.Listele();

        lblToplam.Text = AlisVeris.genelToplam;
    }

    //string insert işlemleri için yazıldı

    //string sorgu = "SELECT dbo.Fotograflar.FotoYol, dbo.Urunler.* FROM   dbo.Fotograflar INNER JOIN dbo.Urunler ON dbo.Fotograflar.UrunId = dbo.Urunler.UrunID where Urunler.UrunId in() and FotoVitrin=1";
    //kaç adet ürün alışmışsa buna bağlı olarak in(@p1,@p2...) şeklinde değişim sağlayacak bir metod yazalım.


    //public string Insert(string sql)
    //{
    //    string s = "";
    //    int index = Array.IndexOf(sql.ToCharArray(), '(');
    //    if (dtSession.Rows.Count == 0)
    //    {
    //        //bişi yok
    //    }
    //    else if (dtSession.Rows.Count == 1)
    //    {
    //        s = sql.Substring(0, index + 1);
    //        s += "@p1";
    //        s += sql.Substring(index + 1);
    //    }
    //    else
    //    {
    //        s = sql.Substring(0, index + 1);
    //        //s = s.TrimEnd('(');
    //        s += "@p1";
    //        for (int i = 1; i < dtSession.Rows.Count; i++)
    //        {
    //            s += ",@p" + (i + 1);

    //        }
    //        s += sql.Substring(index + 1);
    //    }
    //    return s;
    //}

    protected void rpSepet_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        string s = Request.Form["quantity"];
        //HTML elementlerindeki değerlere ulaşım şekli Request.Form[html name] dir. Ancak repeater, datalist, listview vs.. içerikteki kontrolleri sürekli çoğaltan durumlarda (5 ürün varsa) aynı sayıda aynı id ve name bilgisini taşıyan kontrol var demektir.
        rpSepet.DataSource = AlisVeris.IslemYap(source, e, s);
        rpSepet.DataBind();
        AlisVeris.Listele();
        lblToplam.Text = AlisVeris.genelToplam;
    }
}