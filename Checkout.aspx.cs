using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Checkout : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            AdresBilgileri();
        }
    }

    DataAccessLayer db = new DataAccessLayer();

    void AdresBilgileri()
    {
        string userId = "";

        try
        {
            userId = Membership.GetUser().ProviderUserKey.ToString();

        }
        catch
        {
        }

        if (!string.IsNullOrEmpty(userId))
        {
            List<Parametreler> liste = new List<Parametreler>();
            liste.Add(new Parametreler { Name = "@user", Value = userId });

            Repeater rpt = (Repeater)LoginView1.FindControl("rpAdres");
            rpt.DataSource = db.GetDataTable("select * from Iletisim where UserId=@user", liste);
            rpt.DataBind();
        }

    }

    protected void Isaret(object sender, EventArgs e)
    {
        foreach (RadioButton item in LoginView1.Controls)
        {
            if (item is RadioButton)
            {
                if (item.ID=="rbSec")
                {
                    RadioButton secim = (RadioButton)item;
                    item.Checked = false;
                }
            }
        }



        RadioButton rb = sender as RadioButton;

        rb.Checked = true;
    }




    protected void rpAdres_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        Isaret(source,e);
    }
}