using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MiniCore;
using ETicaretIslemleri;
using System.Data;
namespace ETicaret
{
    public partial class SiparisDetay : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    using (cSiparisIslemleri SI = new cSiparisIslemleri())
                    {
                        DataTable dt = SI.ListeleSiparis(0, 0, null, Request["GUID"].ToString());
                        if (dt.Rows.Count == 1)
                        {
                            ltSiparis.Text="Adı :"+dt.Rows[0]["FATURA_ADI"].ToString()+"<br>";
                            ltSiparis.Text += "Adres :" + dt.Rows[0]["ADRES"].ToString() + "<br>";
                            ltSiparis.Text += "Durumu :" +(cAraclar.GetDescription((eSiparisDurumu)dt.Rows[0]["SIPARISDURUMU"].ToShort()))+ "<br>";
                            ltSiparis.Text += "Kargo Adı :" + dt.Rows[0]["KARGO_ADI"].ToString() + "<br>";
                            ltSiparis.Text += "Kargo Kodu :" + dt.Rows[0]["KARGOKODU"].ToString() + "<br>"; 
                            ltSiparis.Text += "Tutar :" + dt.Rows[0]["TUTAR"].ToString() + "<br>";
                            gvSiparisDetaylar.DataSource = SI.ListeleSiparisDetay(0, dt.Rows[0]["ID"].ToLong(), 0);
                            gvSiparisDetaylar.DataBind();
                        }
                        else
                            UIAraclari.toastMesaj(this, eStatusType.Uyari, "Siparişiniz Bulunamadı");
                    }
                }
            }
            catch (Exception ex)
            {
                UIAraclari.cLog.Write(ex, UIAraclari._iKullaniciID);
            }
        }
    }
}