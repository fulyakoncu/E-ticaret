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
    public partial class KargoTanimlamalari : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (cUIAraclari._iKullaniciTipi != eKullaniciTipi.Yonetici)
                    Response.Redirect(ResolveUrl("~/Default.aspx"));
                if(!Page.IsPostBack)
                    GridDoldur();
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
                using (cGenelIslemler GI = new cGenelIslemler())
                {
                    gvKargolar.DataSource = GI.ListeleKargo(0, null);
                    gvKargolar.DataBind();
                }
            }
            catch (Exception ex)
            {
                cUIAraclari.cLog.Write(ex, cUIAraclari._iKullaniciID);
            }
        }
        protected void gvKargolar_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                using (cGenelIslemler GI = new cGenelIslemler())
                {
                    DataRow dr = GI.ListeleKargo(gvKargolar.SelectedDataKey["ID"].ToShort(-1), null).Rows[0];
                    txtEmail.Text = dr["EMAIL"].ToString();
                    txtKAdi.Text = dr["ADI"].ToString();
                    lblID.Text = dr["ID"].ToString();
                    txtTelefon.Text = dr["TELEFON"].ToString();
                    chcAktif.Checked = (dr["AKTIF"].ToShort(0) == (short)eAktifDurum.Aktif) ? true : false;
                    chcAktif.Text = chcAktif.Checked ? cAraclar.GetDescription(eAktifDurum.Aktif) : cAraclar.GetDescription(eAktifDurum.Pasif);
                }
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
                txtKAdi.Text = string.Empty;
                txtEmail.Text = string.Empty;
                txtTelefon.Text = string.Empty;
                lblID.Text = "0";
                chcAktif.Checked = false;
                chcAktif.Text = chcAktif.Checked ? cAraclar.GetDescription(eAktifDurum.Aktif) : cAraclar.GetDescription(eAktifDurum.Pasif);
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
                TBLKARGOLAR kargo = new TBLKARGOLAR();
                kargo.ADI = txtKAdi.Text;
                kargo.EMAIL = txtEmail.Text;
                kargo.TELEFON = txtTelefon.Text;
                kargo.AKTIF = chcAktif.Checked ? eAktifDurum.Aktif : eAktifDurum.Pasif;
                short sIslemSonuc=0;
                using (cGenelIslemler GI = new cGenelIslemler())
                {
                    if (lblID.Text.ToShort(0) == 0)
                        sIslemSonuc = GI.EkleKargo(kargo);
                    else
                    {
                        kargo.ID = lblID.Text.ToShort(0);
                        sIslemSonuc = GI.DuzenleKargo(kargo);
                    }
                }
                if (sIslemSonuc > 0)
                    cUIAraclari.toastMesaj(this, eStatusType.Onay, "İşleminiz Başarıyla Gerçekleştirildi");
                else
                    cUIAraclari.toastMesaj(this, eStatusType.Hata, "İşleminiz Gerçekleştirilemedi");
                GridDoldur();
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
                using (cGenelIslemler GI = new cGenelIslemler())
                {
                    if (lblID.Text.ToShort(0) > 0)
                    {
                        if (GI.SilKargo(lblID.Text.ToShort(0)) > 0)
                            cUIAraclari.toastMesaj(this, eStatusType.Onay, "İşleminiz Başarıyla Gerçekleşti");
                        else
                            cUIAraclari.toastMesaj(this, eStatusType.Hata, "İşleminiz Gerçekleştirilemedi");
                    }
                    else
                        cUIAraclari.toastMesaj(this, eStatusType.Uyari, "Lütfen Önce Seçiminizi Yapın");

                    GridDoldur();
                }
            }
            catch (Exception ex)
            {
                cUIAraclari.cLog.Write(ex, cUIAraclari._iKullaniciID);
            }
        }
    }
}