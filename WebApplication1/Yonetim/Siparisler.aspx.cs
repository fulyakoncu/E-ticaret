using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MiniCore;
using ETicaretIslemleri;

namespace ETicaret.Yonetim
{
    public partial class BekleyenSiparisler : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (UIAraclari._iKullaniciTipi != eKullaniciTipi.Yonetici)
                Response.Redirect(ResolveUrl("~/Default.aspx"));
            if (!Page.IsPostBack)
            {
                ddlSiparisTuru.SelectedValue = "B";
                GridDoldur(true);
            }
        }
        private void GridDoldur(bool SiparisDurumu)
        {
            try
            {
                using (cSiparisIslemleri SI = new cSiparisIslemleri())
                {
                    gvSiparisler.DataSource = SI.ListeleSiparisler(SiparisDurumu);
                    gvSiparisler.DataBind();
                }
            }
            catch (Exception ex)
            {
                UIAraclari.cLog.Write(ex, UIAraclari._iKullaniciID);
            }
        }
        protected void ddlSiparisTuru_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlSiparisTuru.SelectedValue == "B")
                    GridDoldur(true);
                else if (ddlSiparisTuru.SelectedValue == "S")
                    GridDoldur(false);
            }
            catch (Exception ex)
            {
                UIAraclari.cLog.Write(ex, UIAraclari._iKullaniciID);
            }
        }
    }
}