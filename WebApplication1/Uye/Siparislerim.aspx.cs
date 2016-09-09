using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETicaretIslemleri;
using MiniCore;
using System.Data;
namespace ETicaret.Uye
{
    public partial class Siparislerim : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (UIAraclari._iKullaniciTipi == eKullaniciTipi.Misafir)
                    Response.Redirect(ResolveUrl("~/Default.aspx"));
                if (!Page.IsPostBack)
                {
                    using (cSiparisIslemleri SP = new cSiparisIslemleri())
                    {
                        gvSiparisler.DataSource = SP.ListeleSiparis(0, UIAraclari._iKullaniciID, null, string.Empty);
                        gvSiparisler.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                UIAraclari.cLog.Write(ex, UIAraclari._iKullaniciID);
            }
        }

        protected void gvSiparisler_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                using(cSiparisIslemleri SP=new cSiparisIslemleri())
                {
                    DataTable dt = SP.ListeleSiparis(gvSiparisler.SelectedDataKey["ID"].ToLong(-1), 0, null, string.Empty);
                    ltSiparis.Text = "Adı :" + dt.Rows[0]["FATURA_ADI"].ToString() + "<br>";
                    ltSiparis.Text += "Adres :" + dt.Rows[0]["ADRES"].ToString() + "<br>";
                    ltSiparis.Text += "Durumu :" + (cAraclar.GetDescription((eSiparisDurumu)dt.Rows[0]["SIPARISDURUMU"].ToShort())) + "<br>";
                    ltSiparis.Text += "Kargo Adı :" + dt.Rows[0]["KARGO_ADI"].ToString() + "<br>";
                    ltSiparis.Text += "Kargo Kodu :" + dt.Rows[0]["KARGOKODU"].ToString() + "<br>";
                    ltSiparis.Text += "Tutar :" + dt.Rows[0]["TUTAR"].ToString() + "<br>";
                    gvSiparisDetaylar.DataSource = SP.ListeleSiparisDetay(0, gvSiparisler.SelectedDataKey["ID"].ToLong(-1), 0);
                    gvSiparisDetaylar.DataBind();
                }
            }
            catch (Exception ex)
            {
                UIAraclari.cLog.Write(ex, UIAraclari._iKullaniciID);
            }
        }
    }
}