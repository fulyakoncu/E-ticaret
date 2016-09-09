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
    public partial class GenelTanimlamalar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (UIAraclari._iKullaniciTipi != eKullaniciTipi.Yonetici)
                Response.Redirect(ResolveUrl("~/Default.aspx"));
            if(!Page.IsPostBack)
                GridDoldur();
        }
        private void GridDoldur()
        {
            try
            {
                using (cGenelIslemler GI = new cGenelIslemler())
                {
                    gvKurumlar.DataSource = GI.ListeleKurumParametreleri(0, null);
                    gvKurumlar.DataBind();
                }
            }
            catch (Exception ex)
            {
                UIAraclari.cLog.Write(ex, UIAraclari._iKullaniciID);
            }
        }
        protected void btnYeniKayit_Click(object sender, EventArgs e)
        {
            try
            {
                txtAdres.Text = string.Empty;
                txtFax.Text = string.Empty;
                txtKurumAdi.Text = string.Empty;
                txtMail.Text = string.Empty;
                txtMailSifre.Text = string.Empty;
                txtSMTPPort.Text = string.Empty;
                txtSMTPAdres.Text = string.Empty;
                txtTelefon.Text = string.Empty;
                txtTicSicilNo.Text = string.Empty;
                txtVDairesi.Text = string.Empty;
                txtVNo.Text = string.Empty;
                lblID.Text = "0";
                chcAktif.Checked = false;
                chcAktif.Text = chcAktif.Checked ? cAraclar.GetDescription(eAktifDurum.Aktif) : cAraclar.GetDescription(eAktifDurum.Pasif);
            }
            catch (Exception ex)
            {
                UIAraclari.cLog.Write(ex, UIAraclari._iKullaniciID);
            }
        }

        protected void btnKaydet_Click(object sender, EventArgs e)
        {
            try
            {
                TBLKURUM_PARAMETRELERI kp = new TBLKURUM_PARAMETRELERI();
                kp.ADRES = txtAdres.Text;
                kp.AKTIF = chcAktif.Checked ? eAktifDurum.Aktif : eAktifDurum.Pasif;
                kp.FAX = txtFax.Text;
                kp.KURUM_ADI = txtKurumAdi.Text;
                kp.MAIL = txtMail.Text;
                kp.MAILSIFRE = txtMailSifre.Text;
                kp.SMTP_ADRES = txtSMTPAdres.Text;
                kp.SMTP_PORT = txtSMTPPort.Text.ToInt(0);
                kp.TELEFON = txtTelefon.Text;
                kp.TIC_SICIL_NO = txtTicSicilNo.Text;
                kp.VERGI_DAIRESI = txtVDairesi.Text;
                kp.VERGI_NO = txtVNo.Text;
                short sonuc = 0;
                using (cGenelIslemler GI = new cGenelIslemler())
                {
                    if (lblID.Text.ToInt(0) == 0)
                        sonuc = GI.EkleKurumParametreleri(kp);
                    else
                    {
                        kp.ID = lblID.Text.ToShort(0);
                        sonuc = GI.DuzenleKurumParametreleri(kp);
                    }
                }
                GridDoldur();
                if (sonuc > 0)
                    UIAraclari.toastMesaj(this, eStatusType.Onay, "İşleminiz Başarıyla Gerçekleşti");
                else
                    UIAraclari.toastMesaj(this, eStatusType.Hata, "İşleminiz Gerçekleşirken Hata Oluştu");
            }
            catch (Exception ex)
            {
                UIAraclari.cLog.Write(ex, UIAraclari._iKullaniciID);
            }
        }

        protected void gvKurumlar_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                using (cGenelIslemler GI = new cGenelIslemler())
                {
                    DataRow dr = GI.ListeleKurumParametreleri(gvKurumlar.SelectedDataKey["ID"].ToShort(-1), null).Rows[0];
                    txtAdres.Text = dr["ADRES"].ToString();
                    chcAktif.Checked = (dr["AKTIF"].ToShort(0) == (short)eAktifDurum.Aktif) ? true : false;
                    txtFax.Text = dr["FAX"].ToString();
                    txtKurumAdi.Text = dr["KURUM_ADI"].ToString();
                    lblID.Text = dr["ID"].ToString();
                    txtMail.Text = dr["MAIL"].ToString();
                    txtMailSifre.Text = dr["MAILSIFRE"].ToString();
                    txtSMTPAdres.Text = dr["SMTP_ADRES"].ToString();
                    txtSMTPPort.Text = dr["SMTP_PORT"].ToString();
                    txtTelefon.Text = dr["TELEFON"].ToString();
                    txtTicSicilNo.Text = dr["TIC_SICIL_NO"].ToString();
                    txtVDairesi.Text = dr["VERGI_DAIRESI"].ToString();
                    txtVNo.Text = dr["VERGI_NO"].ToString();
                    chcAktif.Text = chcAktif.Checked ? cAraclar.GetDescription(eAktifDurum.Aktif) : cAraclar.GetDescription(eAktifDurum.Pasif);
                }
            }
            catch (Exception ex)
            {
                UIAraclari.cLog.Write(ex, UIAraclari._iKullaniciID);
            }

        }

        protected void btnSil_Click(object sender, EventArgs e)
        {
            try
            {
                if (lblID.Text.ToShort(0) > 0)
                {
                    using (cGenelIslemler GI = new cGenelIslemler())
                    {
                        if (GI.SilKurumParametreleri(lblID.Text.ToShort(0)) == 1)
                        {
                            UIAraclari.toastMesaj(this, eStatusType.Onay, "Seçiminiz Silindi");
                            btnYeniKayit_Click(null, null);
                            GridDoldur();
                        }
                        else
                            UIAraclari.toastMesaj(this, eStatusType.Hata, "İşleminiz Gerçekleşirken Hata Oluştu");
                    }
                }
                else
                    UIAraclari.toastMesaj(this, eStatusType.Bilgi, "Lütfen Seçiminizi Yapın..");
            }
            catch (Exception ex)
            {
                UIAraclari.cLog.Write(ex, UIAraclari._iKullaniciID);
            }
        }
    }
}