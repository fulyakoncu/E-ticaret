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
    public partial class UrunEkle : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (cUIAraclari._iKullaniciTipi != eKullaniciTipi.Yonetici)
                    Response.Redirect(ResolveUrl("~/Default.aspx"));
                if (!Page.IsPostBack)
                {
                    ddlParaBirimi.DataSource = cAraclar.VerEnumListesi(typeof(eParaBirimi));
                    ddlParaBirimi.DataValueField = "Key";
                    ddlParaBirimi.DataTextField = "Value";
                    ddlParaBirimi.DataBind();

                    using (cUrunGenel UG = new cUrunGenel())
                    {
                        ddlKategoriler.DataSource = UG.ListeleKategori(0, -1, null);
                        ddlKategoriler.DataValueField = "ID";
                        ddlKategoriler.DataTextField = "ADI";
                        ddlKategoriler.DataBind();

                        ddlMarkalar.DataSource = UG.ListeleMarka(0);
                        ddlMarkalar.DataTextField = "ADI";
                        ddlMarkalar.DataValueField = "ID";
                        ddlMarkalar.DataBind();
                    }
                    GridDoldur();
                    dvEkleme.Visible = false;
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
                using (cUrunGenel UG = new cUrunGenel())
                {
                    gvUrunler.DataSource = UG.ListeleUrun(0, 0, 0, null, null, null, null,string.Empty,string.Empty,false);
                    gvUrunler.DataBind();
                }
            }
            catch (Exception ex)
            {
                cUIAraclari.cLog.Write(ex, cUIAraclari._iKullaniciID);
            }
        }

        protected void gvUrunler_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                using (cUrunGenel UG = new cUrunGenel())
                {
                    DataRow dr = UG.ListeleUrun(gvUrunler.SelectedDataKey["ID"].ToShort(-1),0,0,null,null,null,null,string.Empty,string.Empty,false).Rows[0];
                    lblID.Text = dr["ID"].ToString();
                    txtKodu.Text = dr["KODU"].ToString();
                    ddlMarkalar.SelectedValue = dr["MARKA"].ToString();
                    txtModeli.Text = dr["MODEL"].ToString();
                    ddlKategoriler.SelectedValue = dr["KATEGORI"].ToString();
                    txtFiyat.Text = dr["FIYATI"].ToString();
                    ddlParaBirimi.SelectedValue = dr["PARABIRIMI"].ToString();
                    chcKampanyali.Checked = (eEvetHayir)dr["KAMPANYALI"].ToShort(0) == eEvetHayir.Evet;
                    chcKampanyali.Text=chcKampanyali.Checked?cAraclar.GetDescription(eEvetHayir.Evet):cAraclar.GetDescription(eEvetHayir.Hayir);
                    txtAciklama.Text = dr["ACIKLAMA"].ToString();
                    chcSlider.Checked = (eEvetHayir)dr["SLIDER"].ToShort(0) == eEvetHayir.Evet;
                    chcSlider.Text = chcSlider.Checked ? cAraclar.GetDescription(eEvetHayir.Evet) : cAraclar.GetDescription(eEvetHayir.Hayir);
                    chcIndirimli.Checked = (eEvetHayir)dr["INDIRIM"].ToShort(0) == eEvetHayir.Evet;
                    chcIndirimli.Text = chcIndirimli.Checked ? cAraclar.GetDescription(eEvetHayir.Evet) : cAraclar.GetDescription(eEvetHayir.Hayir);
                    txtIndFiyati.Text = dr["INDIRIMLI_FIYAT"].ToString();
                    chcAktif.Checked = (eEvetHayir)dr["AKTIF"].ToShort(0) == eEvetHayir.Evet;
                    chcAktif.Text = chcAktif.Checked ? cAraclar.GetDescription(eAktifDurum.Aktif) : cAraclar.GetDescription(eAktifDurum.Pasif);
                    dvEkleme.Visible = true;
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
                TBLURUNLER urun = new TBLURUNLER();
                urun.ACIKLAMA = txtAciklama.Text;
                urun.AKTIF = chcAktif.Checked ? eAktifDurum.Aktif : eAktifDurum.Pasif;
                urun.FIYATI = txtFiyat.Text.ToDecimal(0);
                urun.INDIRIM = chcIndirimli.Checked ? eEvetHayir.Evet : eEvetHayir.Hayir;
                urun.INDIRIMLI_FIYAT = txtIndFiyati.Text.ToDecimal(0);
                urun.KAMPANYALI = chcKampanyali.Checked ? eEvetHayir.Evet : eEvetHayir.Hayir;
                urun.KATEGORI = ddlKategoriler.SelectedValue.ToShort(0);
                urun.KODU = txtKodu.Text;
                urun.MARKA = ddlMarkalar.SelectedValue.ToShort(0);
                urun.MODEL = txtModeli.Text;
                urun.PARABIRIMI = (eParaBirimi)ddlParaBirimi.SelectedValue.ToShort(0);
                urun.SLIDER = chcSlider.Checked ? eEvetHayir.Evet : eEvetHayir.Hayir;
                int sonuc=0;
                using (cUrunGenel UG = new cUrunGenel())
                {
                    if (lblID.Text.ToInt(0) == 0)
                    {
                        sonuc = UG.EkleUrun(urun);
                        lblID.Text = sonuc.ToString();
                    }
                    else
                    {
                        urun.ID = lblID.Text.ToInt(0);
                        sonuc = UG.DuzenleUrun(urun);
                    }
                }
                if (sonuc > 0)
                    cUIAraclari.toastMesaj(this, eStatusType.Onay, "İşleminiz Başarıyla Tamamlandı");
                else
                    cUIAraclari.toastMesaj(this, eStatusType.Hata, "Hata Oluştu");
                GridDoldur();
                dvEkleme.Visible = true;
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
                txtKodu.Text = string.Empty;
                ddlMarkalar.SelectedIndex = 0;
                txtModeli.Text = string.Empty;
                ddlKategoriler.SelectedIndex = 0;
                txtFiyat.Text = string.Empty;
                ddlParaBirimi.SelectedIndex = 0;
                chcKampanyali.Checked = false;
                txtAciklama.Text = string.Empty;
                chcSlider.Checked = false;
                chcIndirimli.Checked = false;
                txtIndFiyati.Text = string.Empty;
                chcAktif.Checked = false;
                chcKampanyali.Text = chcKampanyali.Checked ? cAraclar.GetDescription(eEvetHayir.Evet) : cAraclar.GetDescription(eEvetHayir.Hayir);
                chcSlider.Text = chcSlider.Checked ? cAraclar.GetDescription(eEvetHayir.Evet) : cAraclar.GetDescription(eEvetHayir.Hayir);
                chcIndirimli.Text = chcIndirimli.Checked ? cAraclar.GetDescription(eEvetHayir.Evet) : cAraclar.GetDescription(eEvetHayir.Hayir);
                chcAktif.Text = chcAktif.Checked ? cAraclar.GetDescription(eAktifDurum.Aktif) : cAraclar.GetDescription(eAktifDurum.Pasif);
                dvEkleme.Visible = false;
            }
            catch (Exception ex)
            {
                cUIAraclari.cLog.Write(ex, cUIAraclari._iKullaniciID);
            }
        }

        protected void gvUrunler_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridDoldur();
            gvUrunler.PageIndex = e.NewPageIndex;
            gvUrunler.DataBind(); 
        }
    }
}