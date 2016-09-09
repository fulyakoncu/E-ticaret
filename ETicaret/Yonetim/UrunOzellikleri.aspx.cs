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
    public partial class UrunOzellikleri : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (cUIAraclari._iKullaniciTipi != eKullaniciTipi.Yonetici)
                Response.Redirect(ResolveUrl("~/Default.aspx"));
            if(!Page.IsPostBack)
                GridDoldur();
        }
        private void GridDoldur()
        {
            try
            {
                using (cUrunGenel UG = new cUrunGenel())
                {
                    gvOzellikler.DataSource = UG.ListeOzellikTanimi(0);
                    gvOzellikler.DataBind();
                }
            }
            catch (Exception ex)
            {
                cUIAraclari.cLog.Write(ex, cUIAraclari._iKullaniciID);
            }
        }
        protected void btnKaydet_Click(object sender, EventArgs e)
        {
            try
            {
                TBLURUN_OZELLIK_TANIMLARI Ozellik = new TBLURUN_OZELLIK_TANIMLARI();
                Ozellik.BIRIMI = txtBirimi.Text;
                Ozellik.OZELLIK_ADI = txtOzellikAdi.Text;
                int sonuc = 0;
                using (cUrunGenel UG = new cUrunGenel())
                {
                    if (lblID.Text.ToInt(0) == 0)
                        sonuc = UG.EkleOzellikTanimi(Ozellik);
                    else
                    {
                        Ozellik.ID = lblID.Text.ToInt(0);
                        sonuc = UG.DuzenleOzellikTanimi(Ozellik);
                    }
                }
                if (sonuc > 0)
                    cUIAraclari.toastMesaj(this, eStatusType.Onay, "İşleminiz Başarıyla Gerçekleştirildi");
                else
                    cUIAraclari.toastMesaj(this, eStatusType.Hata, "Hata Oluştu");
                GridDoldur();
            }
            catch (Exception ex)
            {
                cUIAraclari.cLog.Write(ex, cUIAraclari._iKullaniciID);
            }
        }

        protected void gvOzellikler_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                using (cUrunGenel UG = new cUrunGenel())
                {
                    DataRow dr = UG.ListeOzellikTanimi(gvOzellikler.SelectedDataKey["ID"].ToShort(0)).Rows[0];
                    txtBirimi.Text = dr["BIRIMI"].ToString();
                    txtOzellikAdi.Text = dr["OZELLIK_ADI"].ToString();
                    lblID.Text = dr["ID"].ToString();
                }
            }
            catch (Exception ex)
            {
                cUIAraclari.cLog.Write(ex, cUIAraclari._iKullaniciID);
            }
        }

        protected void btnYeniKayit_Click(object sender, EventArgs e)
        {
            txtBirimi.Text = string.Empty;
            txtOzellikAdi.Text = string.Empty;
            lblID.Text = "0";
        }
    }
}