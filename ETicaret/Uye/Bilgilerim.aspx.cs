using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MiniCore;
using ETicaretIslemleri;
using System.Data;
using System.Text;

namespace ETicaret.Uye
{
    public partial class Bilgilerim : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (cUIAraclari._iKullaniciTipi == eKullaniciTipi.Misafir)
                    Response.Redirect(ResolveUrl("~/Default.aspx"));
                if (!Page.IsPostBack)
                {
                    DataRow dr = null;
                    dvGruptipi.Visible = false;
                    using (cGenelIslemler GI = new cGenelIslemler())
                    using (cUyeIslemleri UI = new cUyeIslemleri())
                    {
                        if (cUIAraclari._iKullaniciID != 0 && Request["ID"].ToInt(0) == 0)
                        {
                            dr = UI.ListeleKullanici(cUIAraclari._iKullaniciID, null, null, null).Rows[0];
                        }
                        else if (Request["ID"].ToInt(0) > 0 && (eKullaniciTipi)cUIAraclari._iKullaniciTipi == eKullaniciTipi.Yonetici)
                        {
                            dr = UI.ListeleKullanici(Request["ID"].ToInt(0), null, null, null).Rows[0];
                            dvGruptipi.Visible = true;
                        }
                        if (dr != null)
                        {
                            txtAd.Text = dr["ADI"].ToString();
                            txtAdres.Text = dr["ADRES"].ToString();
                            txtCepTel.Text = dr["CEPTELEFONU"].ToString();
                            txtEmail.Text = dr["EMAIL"].ToString();
                            txtEmail2.Text = dr["EMAIL"].ToString();
                            txtEvTel.Text = dr["EVTELEFONU"].ToString();
                            txtSoyad.Text = dr["SOYADI"].ToString();
                            txtDogumTar.Text = dr["DOGUM_TARIHI"].ToString();

                            rblCinsiyet.DataSource = cAraclar.VerEnumListesi(typeof(eCinsiyet));
                            rblCinsiyet.DataValueField = "Key";
                            rblCinsiyet.DataTextField = "Value";
                            rblCinsiyet.DataBind();
                            rblCinsiyet.SelectedValue = dr["CINSIYET"].ToString();

                            ddlIl.DataSource = GI.ListeleSehirler(0);
                            ddlIl.DataTextField = "ADI";
                            ddlIl.DataValueField = "ID";
                            ddlIl.DataBind();
                            ddlIl.SelectedValue = dr["SEHIR_ID"].ToString();

                            ddlUyeTipi.DataSource = cAraclar.VerEnumListesi(typeof(eKullaniciTipi));
                            ddlUyeTipi.DataTextField = "Value";
                            ddlUyeTipi.DataValueField = "Key";
                            ddlUyeTipi.DataBind();
                            ddlUyeTipi.SelectedValue = dr["KULLANICI_TIPI"].ToString();
                        }
                    }

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
                if (txtSifre.Text != string.Empty && txtSifre.Text != txtSifre2.Text)
                {
                    cUIAraclari.toastMesaj(this, eStatusType.Uyari, "Şifreleriniz uyuşmuyor");
                }
                else if (txtEmail.Text != txtEmail2.Text)
                    cUIAraclari.toastMesaj(this, eStatusType.Uyari, "Mail Bilgileri uyuşmuyor");
                else
                {
                    TBLUYELER uye = new TBLUYELER();
                    uye.ID = Request["ID"].ToInt(0) == 0 ? cUIAraclari._iKullaniciID : Request["ID"].ToInt(0);
                    uye.ADI = txtAd.Text;
                    uye.ADRES = txtAdres.Text;
                    uye.CEPTELEFONU = txtCepTel.Text;
                    uye.CINSIYET = rblCinsiyet.SelectedValue.ToShort(0) == (short)eCinsiyet.Erkek ? eCinsiyet.Erkek : eCinsiyet.Bayan;
                    uye.DOGUM_TARIHI = txtDogumTar.Text.ToDateTime();
                    uye.EMAIL = txtEmail.Text;
                    uye.EVTELEFONU = txtEvTel.Text;
                    uye.SEHIR_ID = ddlIl.SelectedValue.ToInt(0);
                    uye.SOYADI = txtSoyad.Text;

                    short sIslemSonuc = 0;
                    using (cUyeIslemleri UI = new cUyeIslemleri())
                    {
                        if (txtSifre.Text != "" && txtSifre2.Text != "")
                        {
                            sIslemSonuc = UI.DeğistirSifre(cUIAraclari._iKullaniciID, txtSifre.Text);
                        }
                        if (dvGruptipi.Visible == true)
                        {
                            sIslemSonuc = UI.DegistirKullaniciTipi(Request["ID"].ToInt(0), (eKullaniciTipi)ddlUyeTipi.SelectedValue.ToShort(0));
                        }
                        sIslemSonuc = UI.DuzenleKullanici(uye);

                    }
                    if (sIslemSonuc > 0)
                        cUIAraclari.toastMesaj(this, eStatusType.Onay, "İşleminiz Başarıyla Gerçekleştirildi");
                    else
                        cUIAraclari.toastMesaj(this, eStatusType.Hata, "İşleminiz Gerçekleştirilemedi");
                }
            }
            catch (Exception ex)
            {
                cUIAraclari.cLog.Write(ex, cUIAraclari._iKullaniciID.ToInt(0));
            }
        }
    }
}