using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETicaretIslemleri;
using MiniCore;

namespace ETicaret
{
    public partial class Arama : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    using (cUrunListeleri UG = new cUrunListeleri())
                    {
                        lstUrunler.DataSource = UG.ListeAramaSonuclari(Request["Aranan"].ToString());
                        lstUrunler.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                cUIAraclari.cLog.Write(ex, cUIAraclari._iKullaniciID.ToInt(0));
            }
        }
    }
}