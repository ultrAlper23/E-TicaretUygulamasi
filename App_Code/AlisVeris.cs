using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for AlisVeris
/// </summary>
public class AlisVeris
{
    public AlisVeris()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    static DataAccessLayer db = new DataAccessLayer();

    public static void SepeteAt(object s, CommandEventArgs e, string urunID)
    {
        string id = urunID;

        List<Parametreler> liste = new List<Parametreler>();
        liste.Add(new Parametreler { Name = "@id", Value = id });//hangi üründen alınacağını biliyoruz.

        DataRow satir = db.GetDataRow("select dbo.Fotograflar.FotoYol, dbo.Urunler.* from dbo.Fotograflar right join dbo.Urunler on dbo.Fotograflar.UrunId=dbo.Urunler.UrunID where Urunler.UrunId = @id and (FotoVitrin=1 or FotoVitrin is null)", liste); //ürüne ait tüm bilgileri aktarmış olduk.

        //sepette gösterilcek bilgiler ;
        int adet = 1;
        string urunAdi = satir["UrunAdi"].ToString();
        double fiyat = Convert.ToDouble(string.Format("{0:0.00}", satir["SonFiyat"].ToString()));//virgülden sonra 2 basamak gösterilmesi için.


        DataTable dt = new DataTable();//sepette bulunan ürün bilgilerini saklamak için.

        if (HttpContext.Current.Session["sepet"] != null)//sepete daha önce bir ürün atılmışsa
        {
            dt = (DataTable)HttpContext.Current.Session["sepet"];//önceki eklenen ürün bilgilerini dt ye yükle.
        }
        else //sepete ilk ürünün eklenmesi
        {
            dt.Columns.Add("id");//ürünün hangisi olduğu
            dt.Columns.Add("isim");//ürünün isminin ne olduğu
            dt.Columns.Add("fiyat");//ürünün fiyatı ne
            dt.Columns.Add("adet");//kaç adet ürün
            dt.Columns.Add("tutar");//her ürüne ait tutar.
            dt.Columns.Add("yol");//her ürüne ait yol
        }

        bool urunSepetteMi = Kontrol(id);//ürünün daha önce eklenip eklenmediğini kontrol eder.

        //eğer ürün sepetteyse arttırma işlemi yapılmalı.

        if (urunSepetteMi)//true (ürün var) ise
        {
            Arttir(id, adet);
        }
        else // false (ürün yok) ise
        {
            DataRow row = dt.NewRow();
            row["id"] = urunID;
            row["isim"] = urunAdi;
            row["fiyat"] = fiyat;
            row["adet"] = adet;
            row["tutar"] = adet * fiyat;
            row["yol"] = satir["FotoYol"]; //satırın FotoYol kolonuna yol bilgisini aktar.
            dt.Rows.Add(row);
        }

        HttpContext.Current.Session["sepet"] = dt;//ürün ilk kez ekleniyor ya da var olanın sayısı arttırılıyorsa bile her durumda session son bilgileri saklamalı.

        Listele();
    }

    private static void Arttir(string id, int adet)
    {
        DataTable dt = new DataTable();

        if (HttpContext.Current.Session["sepet"] != null)
        {
            dt = (DataTable)HttpContext.Current.Session["sepet"];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["id"].ToString() == id)
                {
                    int urunAdedi = Convert.ToInt32(dt.Rows[i]["adet"]);
                    urunAdedi += adet;
                    dt.Rows[i]["adet"] = urunAdedi;
                    dt.Rows[i]["tutar"] = urunAdedi * Convert.ToDouble(dt.Rows[i]["fiyat"]);
                    HttpContext.Current.Session["sepet"] = dt;//güncellenmiş bilgiler sessiona atılmış oldu.
                    break;
                }
            }
        }

        AlisVeris.Listele();
    }

    public static DataTable IslemYap(object s, CommandEventArgs e, string yeniMiktar)
    {
        DataTable dtSession = (DataTable)HttpContext.Current.Session["sepet"];
        if (e.CommandName == "sil")
        {
            for (int i = 0; i < dtSession.Rows.Count; i++)
            {
                if (dtSession.Rows[i]["id"].ToString() == e.CommandArgument.ToString())
                {
                    dtSession.Rows.RemoveAt(i);
                    break;
                }
            }
            HttpContext.Current.Session["sepet"] = dtSession;
            //silinen ürün session dan da kaldırılsın ki kullanıcı başka sayfaya gidip tekrar sepete döndüğünde sildiği ürünü görmesin.

        }
            

        else if (e.CommandName == "guncelle")
        {

            string[] dizi = yeniMiktar.Split(',');
            ArrayList arr = new ArrayList();

            for (int i = 0; i < dizi.Length; i++)
            {
                if (dizi[i].ToString() != ",")
                {
                    arr.Add(dizi[i]);
                }
            }

            for (int i = 0; i < dtSession.Rows.Count; i++)
            {
                if (dtSession.Rows[i]["id"].ToString() == e.CommandArgument.ToString())
                {
                    dtSession.Rows[i]["adet"] = arr[i];
                    dtSession.Rows[i]["tutar"] = Convert.ToInt32(dtSession.Rows[i]["adet"]) * Convert.ToDouble(dtSession.Rows[i]["fiyat"]);
                    break;
                }
            }
            HttpContext.Current.Session["sepet"] = dtSession;
        }

        AlisVeris.Listele();

        return dtSession;

        
    }



    private static bool Kontrol(string id)
    {
        DataTable dt = new DataTable();

        bool onay = false;

        if (HttpContext.Current.Session["sepet"] != null)
        {
            dt = (DataTable)HttpContext.Current.Session["sepet"];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["id"].ToString() == id)//ürünü sepette buldu demektir.
                {
                    onay = true;
                    break;//diğer ürünlerin id bilgisi aynı olamayacağı için eşleşme durumundan hemen sonra diğer id leri kontrol etmesin.
                }
            }

            return onay;//ürün var
        }
        else
            return onay;//ürün yok

    }


    public static void Listele() // kaç adet ürün alındı ve toplam tutar ne?
    {
        double tutar = 0;

        if (HttpContext.Current.Session["sepet"] != null)
        {
            DataTable dt = (DataTable)HttpContext.Current.Session["sepet"];

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                tutar += Convert.ToDouble(dt.Rows[i]["tutar"]);
            }
        }

        genelToplam = string.Format("{0}", tutar);
    }

    public static string genelToplam;
   

}