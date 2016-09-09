using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETicaretIslemleri;
using System.Data;
using MiniCore;

namespace ETicaret
{
    public partial class Kampanyalar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    using (cUrunGenel UG = new cUrunGenel())
                    {
                        lstUrunler.DataSource = UG.ListeleUrun(0, 0, 0, eEvetHayir.Evet, null, null, null, string.Empty, string.Empty,false);
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