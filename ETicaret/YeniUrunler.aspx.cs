using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETicaretIslemleri;
using MiniCore;
using System.Data;

namespace ETicaret
{
    public partial class YeniUrunler : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    using (cUrunListeleri UG = new cUrunListeleri())
                    {
                        lstUrunler.DataSource = UG.ListeGrupTipiUrunleri(eGrupTipi.YeniUrun, 0);
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