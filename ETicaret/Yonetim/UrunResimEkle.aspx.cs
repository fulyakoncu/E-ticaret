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
    public partial class UrunResimEkle : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (cUIAraclari._iKullaniciTipi != eKullaniciTipi.Yonetici)
                Response.Redirect(ResolveUrl("~/Default.aspx"));
            if (!Page.IsPostBack)
                GridDoldur();
        }

        private void GridDoldur()
        {
            try
            {
                using (cUrunGenel UG = new cUrunGenel())
                {
                    gvUResim.DataSource = UG.ListeleUrunResim(0, Request["UID"].ToInt(0), null, null);
                    gvUResim.DataBind();
                }
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
                using (cUrunGenel UG = new cUrunGenel())
                {
                    if (UG.SilUrunResim(lblID.Text.ToInt(0)) > 0)
                        cUIAraclari.toastMesaj(this, eStatusType.Onay, "İşleminiz Başarıyla Gerçekleşti");
                    else
                        cUIAraclari.toastMesaj(this, eStatusType.Hata, "Hata Oluştu");

                    GridDoldur();
                }
            }
            catch (Exception ex)
            {
                cUIAraclari.cLog.Write(ex, cUIAraclari._iKullaniciID);
            }
        }

        protected void btnYeniKayit_Click(object sender, EventArgs e)
        {
            chcAnaResim.Checked = false;
            chcAnaResim.Text = cAraclar.GetDescription(eEvetHayir.Hayir);
            chcSlider.Checked = false;
            chcSlider.Text = cAraclar.GetDescription(eEvetHayir.Hayir);
            lblID.Text = "0";
        }

        protected void btnKaydet_Click(object sender, EventArgs e)
        {
            try
            {
                TBLURUN_RESIMLERI UResim = new TBLURUN_RESIMLERI();
                UResim.ANARESIM = chcAnaResim.Checked ? eEvetHayir.Evet : eEvetHayir.Hayir;
                UResim.SLIDERRESIM = chcSlider.Checked ? eEvetHayir.Evet : eEvetHayir.Hayir;
                UResim.URUNID = Request["UID"].ToInt(0);
                if (!fuResim.FileName.IsNull())
                {
                    if (!System.IO.Directory.Exists(Server.MapPath("~/Upload/UrunResimleri/" + Request["UID"].ToString())))
                        System.IO.Directory.CreateDirectory(Server.MapPath("~/Upload/UrunResimleri/" + Request["UID"].ToString()));
                    fuResim.SaveAs(Server.MapPath("~/Upload/UrunResimleri/" + Request["UID"].ToString()+"/") + fuResim.FileName);
                    UResim.RESIMADI = fuResim.FileName;
                }
                int sonuc = 0;
                using (cUrunGenel UG = new cUrunGenel())
                {
                    if (lblID.Text.ToInt(0) == 0)
                        sonuc = UG.EkleUrunResim(UResim);
                    else
                    {
                        UResim.ID = lblID.Text.ToInt(0);
                        sonuc = UG.DuzenleUrunResim(UResim);
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

        protected void gvUResim_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                using (cUrunGenel UG = new cUrunGenel())
                {
                    DataRow dr = UG.ListeleUrunResim(gvUResim.SelectedDataKey["ID"].ToShort(0),0,null,null).Rows[0];
                    lblID.Text = dr["ID"].ToString();
                    chcAnaResim.Checked = (eEvetHayir)dr["ANARESIM"].ToInt(0) == eEvetHayir.Evet;
                    chcSlider.Checked = (eEvetHayir)dr["SLIDERRESIM"].ToInt(0) == eEvetHayir.Evet;
                }
            }
            catch (Exception ex)
            {
                cUIAraclari.cLog.Write(ex, cUIAraclari._iKullaniciID);
            }
        }
    }
}