using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MiniCore;
using ETicaretIslemleri;

namespace ETicaret.Uye
{
    public partial class UrunListem : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (cUIAraclari._iKullaniciTipi == eKullaniciTipi.Misafir)
                    Response.Redirect(ResolveUrl("~/Default.aspx"));
                if (!Page.IsPostBack)
                {
                    GridDoldur();
                    if (cUIAraclari._dtSepet.Rows.Count > 0)
                        btnAktar.Visible = true;
                    else
                        btnAktar.Visible = false;
                }
            }
            catch (Exception ex)
            {
                cUIAraclari.cLog.Write(ex, cUIAraclari._iKullaniciID);
            }
        }
        private void GridDoldur()
        {
            try
            {
                using (cUyeIslemleri UI = new cUyeIslemleri())
                {
                    gvUrunListem.DataSource = UI.ListeleUyeUrunListesi(0, cUIAraclari._iKullaniciID,0);
                    gvUrunListem.DataBind();

                    gvSepetim.DataSource = cUIAraclari._dtSepet;
                    gvSepetim.DataBind();
                }
            }
            catch (Exception ex)
            {
                cUIAraclari.cLog.Write(ex, cUIAraclari._iKullaniciID);
            }
        }
        protected void btnAktar_Click(object sender, EventArgs e)
        {
            try
            {
                using (cUyeIslemleri UI = new cUyeIslemleri())
                    foreach (GridViewRow gvRow in gvSepetim.Rows)
                    {
                        if (gvRow.RowType == DataControlRowType.DataRow)
                        {
                            CheckBox chc = (CheckBox)gvRow.Cells[1].FindControl("chcSecim");
                            //işaretli fakat tabloda yok yani yeni eklenmiş ise
                            if (chc.Checked)
                            {
                                TBLUYE_URUNLISTESI ptUL = new TBLUYE_URUNLISTESI();
                                ptUL.UYEID = cUIAraclari._iKullaniciID;
                                ptUL.URUNID = gvRow.Cells[0].Text.ToInt(0);
                                if (UI.ListeleUyeUrunListesi(0, ptUL.UYEID, ptUL.URUNID).Rows.Count == 0)
                                {
                                    if (UI.EkleUyeUrunListesi(ptUL) > 0)
                                        cUIAraclari.toastMesaj(this, eStatusType.Onay, "İşleminiz Başarıyla Gerçekleşti");
                                    else
                                        cUIAraclari.toastMesaj(this, eStatusType.Hata, "Hata Oluştu");
                                }else
                                    cUIAraclari.toastMesaj(this, eStatusType.Bilgi, "Ürün Listenizde Ekli");

                            }
                        }
                    }
                GridDoldur();
            }
            catch (Exception ex)
            {
                cUIAraclari.cLog.Write(ex, cUIAraclari._iKullaniciID);
            }
        }

        protected void gvUrunListem_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                using (cUyeIslemleri UI = new cUyeIslemleri())
                {
                    if (UI.SilUyeUrunListesi(gvUrunListem.SelectedDataKey["ID"].ToInt(0)) > 0)
                        cUIAraclari.toastMesaj(this, eStatusType.Onay, "İşleminiz Başarıyla Gerçekleşti");
                    else
                        cUIAraclari.toastMesaj(this, eStatusType.Hata, "Hata Oluştu");
                }
                GridDoldur();
            }
            catch (Exception ex)
            {
                cUIAraclari.cLog.Write(ex, cUIAraclari._iKullaniciID);
            }
        }
    }
}