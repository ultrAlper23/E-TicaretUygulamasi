using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MNG_ResimEkleme : System.Web.UI.Page
{
    DataAccessLayer db = new DataAccessLayer();
    string urunID = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        urunID = Request.QueryString["id"];

        if (!string.IsNullOrEmpty(urunID))
        {
            UrunAdiGetir();
            VitrinBilgisi();
            ResimSayisi();

        }

        else
        {
            Response.Redirect("UrunListesi.aspx");
        }
    }

    List<Parametreler> liste = new List<Parametreler>();

    private void ResimSayisi()
    {
        liste.Clear();
        liste.Add(new Parametreler { Name = "@id", Value = urunID });
        int resimSayisi = int.Parse(db.GetScalar("select count(*) from Fotograflar where UrunId=@id", liste));
        //max 5 resim eklenebilsin.

        if (resimSayisi > 4)
        {
            lblSayi.Text = "Ürüne eklenebilcek maximum resim sayısına ulaşıldı.";
            btnEkle.Enabled = false;
        }
        else
            lblSayi.Text = string.Format("Ürüne ait {0} resim mevcut.", resimSayisi);


    }

    private void VitrinBilgisi()
    {
        //vitrin resmi varsa bunu söyleyecek ve checkbox ın kullanımını engelleyecek.
        liste.Clear();
        liste.Add(new Parametreler { Name = "@id", Value = urunID });
        //ürün resminin olması ya da olmaması mesajı ilgilendirsin. eğer vitrin resmi varsa ve sonradan eklenen resimler önceden belirlenen vitrin fotosu bilgisini değiştirecekse chcVitrin enable true/false size kalmış.

        int sayi = int.Parse(db.GetScalar("select count(*) from Fotograflar where UrunId=@id and FotoVitrin=1", liste)); // 0dan farklı bir sayı elde edilirse ürüne ait vitrin fotoğrafı var demektir.

        if (sayi == 0)
            lblVitrin.Text = "Ürüne ait vitrin bulunmamaktadır.";
        else
            lblVitrin.Text = string.Format("Ürüne ait {0} adet vitrin fotoğrafı mevcut.", sayi);
    }

    private void UrunAdiGetir()
    {
        liste.Clear();
        liste.Add(new Parametreler { Name = "@id", Value = urunID });
        lblUrun.Text = db.GetScalar("select UrunAdi from Urunler where UrunID=@id", liste);
    }
    protected void btnEkle_Click(object sender, EventArgs e)
    {

        string resim = "", uzanti = "", resimYolu = "";
        //kullanıcının yüklediği resim geçici olarak kaydedilcek. kaydedilen resim random bir isimle hem 152*152, hem de 350*350 px lik boyutlara dönüştürülecek ve sıbra da geçici resim silinerek resmin adı veri tabanına kaydedilcek (ürünid ile beraber).

        if (fu.FileName != "")
        {
            resimYolu = Server.MapPath(@"~\urunresimleri\" + fu.FileName);

            uzanti = Path.GetExtension(fu.PostedFile.FileName);//resmin .jpg, .png vs uzantısı saklansın.

            resim = Guid.NewGuid().ToString() + uzanti; // uzantisi korunarak unique bie isme sahip olmuş olur.

            //bir resmin boyutları ile oynayacaksak öncelikle dosya saklanmalı ve bu koda dönüştürülmeli.

            fu.SaveAs(Server.MapPath(@"~\urunresimleri\gecici" + uzanti)); //örneğin; gecici.jpg

            Bitmap bmp = new Bitmap(Server.MapPath(@"~\urunresimleri\gecici" + uzanti));//dönüştürmede kullanılacak nesne

            //350px için;

            using (Bitmap orijinal = bmp)
            {
                //using içinde oluşturulan nesneler sadece bu blokta geçerlidir ve blok tamamlandığında nesneler otomatik olarak yok edilir. burada sadece IDisposable dan miras alan nesnelerle çalışılabilir.

                int yukseklik = 350, genislik = 350;

                Bitmap yeniResim = new Bitmap(orijinal, new Size { Height = yukseklik, Width = genislik });

                yeniResim.Save(Server.MapPath(@"~\urunresimleri\350\" + resim));//oluşturulan 350:350 lik resmi belirlenen random isimle belirtilen klasöre kaydeder.

                yeniResim.Dispose();//nesne olarak yok et.
                orijinal.Dispose();
                bmp.Dispose();
                //dispose işlemleri ayrılan bellek bölgelerini temizler ve performansı arttırır.

            }//350 lik resim kaydedildi.
                      

            //152px için;

            bmp = new Bitmap(Server.MapPath(@"~\urunresimleri\gecici" + uzanti));
            using (Bitmap orijinal = bmp)
            {
                int yukseklik = 152, genislik = 152;

                Bitmap yeniResim = new Bitmap(orijinal, new Size { Height = yukseklik, Width = genislik });

                yeniResim.Save(Server.MapPath(@"~\urunresimleri\152\" + resim));

                yeniResim.Dispose();
                orijinal.Dispose();
                bmp.Dispose();


            }//152 lik resim kaydedildi.


            //600px için;

            bmp = new Bitmap(Server.MapPath(@"~\urunresimleri\gecici" + uzanti));

            using (Bitmap orijinal = bmp)
            {
                int yukseklik = 600, genislik = 600;

                Bitmap yeniResim = new Bitmap(orijinal, new Size { Height = yukseklik, Width = genislik });

                yeniResim.Save(Server.MapPath(@"~\urunresimleri\600\" + resim));

                yeniResim.Dispose();
                orijinal.Dispose();
                bmp.Dispose();

            }//600lük resim kaydedildi.


            //oluşturulan gecici.bisey dosyasını silelim.

            FileInfo fi = new FileInfo(Server.MapPath(@"~\urunresimleri\gecici" + uzanti));

            fi.Delete();

            //resim 152 ve 350 klasörüne kaydedildi db ye kaydını yapalım.

            liste.Clear();

            liste.Add(new Parametreler { Name = "@id", Value = urunID });
            liste.Add(new Parametreler { Name = "@yol", Value = resim });//sadece resim adı saklanır.
            liste.Add(new Parametreler { Name = "@vitrin", Value = chkVitrin.Checked });

            //eğer chkbox sürekli kullanımda bırakılıyorsa ve geçerli resim vitrin olacaksa tablo update edilmeli. 

            if (chkVitrin.Checked)
            {
                List<Parametreler> birDegil = new List<Parametreler>();

                birDegil.Add(new Parametreler { Name = "@id", Value = urunID });
                //önceden eklenen tüm vitrin fotolarını 0 a çek

                db.RunASqlStatement("update Fotograflar set FotoVitrin=0 where UrunId=@id", birDegil);
                
            }

            db.RunASqlStatement("insert Fotograflar values(@id,@yol,@vitrin)", liste);
            chkVitrin.Checked = false;//vitrin seçimi sürekli işaretli kalmasın.
            ResimSayisi();
            VitrinBilgisi();


        }


    }
}