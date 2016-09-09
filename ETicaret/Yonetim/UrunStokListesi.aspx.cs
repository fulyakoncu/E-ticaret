using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETicaretIslemleri;
using MiniCore;
using System.Data;

namespace ETicaret.Yonetim
{
    public partial class UrunStokListesi : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (cUIAraclari._iKullaniciTipi != eKullaniciTipi.Yonetici)
                    Response.Redirect(ResolveUrl("~/Default.aspx"));
                using (cRaporlama raporlar = new cRaporlama())
                {
                    DataSet ds = new DataSet();
                    ds.Tables.Add(raporlar.RaporStokListesi().Copy());
                    ds.Tables[0].TableName = "RPSTOKLISTESI";
                    Session["dsSTOK"] = ds;
                    Response.Redirect(ResolveUrl("~/RaporGoster.aspx?KODU=STOKLISTESI&SNAME=dsSTOK"),false);
                }
            }
            catch (Exception ex)
            {
                cUIAraclari.cLog.Write(ex, cUIAraclari._iKullaniciID);
            }
        }
    }
}