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
    public partial class BankaTanimlamalari : System.Web.UI.Page
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
                using (cTahsilat TI = new cTahsilat())
                {
                    gvBankalar.DataSource = TI.ListeleBanka(0, null);
                    gvBankalar.DataBind();
                }
            }
            catch (Exception ex)
            {
                cUIAraclari.cLog.Write(ex, cUIAraclari._iKullaniciID);
            }
        }
        protected void gvBankalar_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                using (cTahsilat TI = new cTahsilat())
                {
                    DataRow dr = TI.ListeleBanka(gvBankalar.SelectedDataKey["ID"].ToShort(-1), null).Rows[0];
                    lblID.Text = dr["ID"].ToString();
                    txtBAdi.Text = dr["BANKA_ADI"].ToString();
                    txtKAdi.Text = dr["KULLANICI_ADI"].ToString();
                    txtSifre.Text = dr["SIFRE"].ToString();
                    txtMNo.Text = dr["MAGAZA_NO"].ToString();
                    txtHost.Text = dr["HOST"].ToString();
                    chcAktif.Checked = (dr["AKTIF"].ToShort(0) == (short)eAktifDurum.Aktif) ? true : false;
                    chcAktif.Text = chcAktif.Checked ? cAraclar.GetDescription(eAktifDurum.Aktif) : cAraclar.GetDescription(eAktifDurum.Pasif);
                    chcTaksit.Checked = (dr["TAKSIT"].ToShort(0) == (short)eAktifDurum.Aktif) ? true : false;
                    chcTaksit.Text = chcTaksit.Checked ? cAraclar.GetDescription(eAktifDurum.Aktif) : cAraclar.GetDescription(eAktifDurum.Pasif);
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
                lblID.Text = "0";
                txtBAdi.Text = string.Empty;
                txtKAdi.Text = string.Empty;
                txtSifre.Text = string.Empty;
                txtMNo.Text = string.Empty;
                txtHost.Text = string.Empty;
                chcAktif.Checked = false;
                chcTaksit.Checked = false;
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
                TBLBANKALAR banka = new TBLBANKALAR();
                banka.BANKA_ADI = txtBAdi.Text;
                banka.KULLANICI_ADI = txtKAdi.Text;
                banka.SIFRE = txtSifre.Text;
                banka.MAGAZA_NO = txtMNo.Text;
                banka.AKTIF = chcAktif.Checked ? eAktifDurum.Aktif : eAktifDurum.Pasif;
                banka.TAKSIT = chcTaksit.Checked ? eAktifDurum.Aktif : eAktifDurum.Pasif;
                banka.HOST = txtHost.Text;
                short sIslemSonuc = 0;
                using (cTahsilat TI = new cTahsilat())
                {
                    if (lblID.Text.ToShort(0) == 0)
                        sIslemSonuc = TI.EkleBanka(banka);
                    else
                    {
                        banka.ID = lblID.Text.ToShort(0);
                        sIslemSonuc = TI.DuzenleBanka(banka);
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
                using (cTahsilat TI = new cTahsilat())
                {
                    if (lblID.Text.ToShort(0) > 0)
                    {
                        if (TI.SilBanka(lblID.Text.ToShort(0)) > 0)
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