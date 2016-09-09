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
    public partial class Raporlar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (cUIAraclari._iKullaniciTipi != eKullaniciTipi.Yonetici)
                Response.Redirect(ResolveUrl("~/Default.aspx"));
            if (!Page.IsPostBack)
            {
                GridDoldur();
                lblID.Text = "0";
            }
        }
        private void GridDoldur()
        {
            using (cRaporlama RP = new cRaporlama())
            {
                gvRaporlar.DataSource = RP.ListeleRaporlar(0, null);
                gvRaporlar.DataBind();
            }
        }

        protected void btnKaydet_Click(object sender, EventArgs e)
        {
            try
            {
                TBLRAPOR rpr = new TBLRAPOR();
                if (fuRaporRpt.HasFile && fuRaporCs.HasFile)
                {
                    string extRpt = System.IO.Path.GetExtension(fuRaporRpt.FileName).ToLower();
                    string extCs = System.IO.Path.GetExtension(fuRaporCs.FileName).ToLower();
                    if (extRpt == ".rpt" && extCs == ".cs")
                    {
                        fuRaporRpt.SaveAs(Server.MapPath("../Raporlar\\" + fuRaporRpt.FileName));
                        fuRaporCs.SaveAs(Server.MapPath("../Raporlar\\" + fuRaporCs.FileName));
                        rpr.YOL = fuRaporRpt.FileName;
                    }
                }
                rpr.AD = txtRaporAdi.Text;
                short sonuc = 0;
                using (cRaporlama R = new cRaporlama())
                {
                    if (lblID.Text.ToInt(0) == 0)
                        sonuc = R.RaporEkle(rpr).ToShort(0);
                    else
                    {
                        rpr.ID = lblID.Text.ToShort(0);
                        sonuc = R.DuzenleRaporlar(rpr);
                    }
                }
                GridDoldur();
                if (sonuc > 0)
                    cUIAraclari.toastMesaj(this, eStatusType.Onay, "İşleminiz Başarıyla Gerçekleşti");
                else
                    cUIAraclari.toastMesaj(this, eStatusType.Hata, "İşleminiz Gerçekleşirken Hata Oluştu");
            }
            catch (Exception ex)
            {
                cUIAraclari.cLog.Write(ex, cUIAraclari._iKullaniciID);
            }
        }

        protected void btnYeniKayit_Click(object sender, EventArgs e)
        {
            try
            {
                txtRaporAdi.Text = string.Empty;
                lblID.Text = "0";
            }
            catch (Exception ex)
            {
                cUIAraclari.cLog.Write(ex, cUIAraclari._iKullaniciID);
            }
        }

        protected void btnSil_Click(object sender, EventArgs e)
        {
            try
            {
                if (lblID.Text.ToShort(0) > 0)
                {
                    using (cRaporlama R = new cRaporlama())
                    {
                        if (R.SilRaporlar(lblID.Text.ToShort(0)) == 1)
                        {
                            cUIAraclari.toastMesaj(this, eStatusType.Onay, "Seçiminiz Silindi");
                            btnYeniKayit_Click(null, null);
                            GridDoldur();
                        }
                        else
                            cUIAraclari.toastMesaj(this, eStatusType.Hata, "İşleminiz Gerçekleşirken Hata Oluştu");
                    }
                }
                else
                    cUIAraclari.toastMesaj(this, eStatusType.Bilgi, "Lütfen Seçiminizi Yapın..");
            }
            catch (Exception ex)
            {
                cUIAraclari.cLog.Write(ex, cUIAraclari._iKullaniciID);
            }
        }

        protected void gvRaporlar_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                using (cRaporlama R = new cRaporlama())
                {
                    DataRow dr = R.ListeleRaporlar(gvRaporlar.SelectedDataKey["ID"].ToShort(-1), null).Rows[0];
                    txtRaporAdi.Text = dr["AD"].ToString();
                    lblID.Text = dr["ID"].ToString();
                }
            }
            catch (Exception ex)
            {
                cUIAraclari.cLog.Write(ex, cUIAraclari._iKullaniciID);
            }
        }
    }
}