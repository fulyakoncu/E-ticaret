using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETicaretIslemleri;
using MiniCore;

namespace ETicaret.Yonetim
{
    public partial class UyeListesi : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (cUIAraclari._iKullaniciTipi != eKullaniciTipi.Yonetici)
                    Response.Redirect(ResolveUrl("~/Default.aspx"));
                if (!Page.IsPostBack)
                {
                    using(cUyeIslemleri UI=new cUyeIslemleri())
                    {
                        gvUyeler.DataSource = UI.ListeleKullanici(0, string.Empty, eKullaniciTipi.Uye, string.Empty);
                        gvUyeler.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                cUIAraclari.cLog.Write(ex, cUIAraclari._iKullaniciID);
            }
        }
    }
}