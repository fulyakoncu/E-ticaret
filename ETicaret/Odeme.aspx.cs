using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETicaretIslemleri;
using MiniCore;
using System.Text;

namespace ETicaret
{
    public partial class Odeme : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    hfToplamTutar.Value = "0";

                    ddlOdemeTipi.DataSource = cAraclar.VerEnumListesi(typeof(eOdemeTipi));
                    ddlOdemeTipi.DataTextField = "Value";
                    ddlOdemeTipi.DataValueField = "Key";
                    ddlOdemeTipi.DataBind();
                    ddlOdemeTipi_SelectedIndexChanged(null, null);

                    if (cUIAraclari._iKullaniciID > 0)
                    {
                        using (cUyeIslemleri UI = new cUyeIslemleri())
                        {
                            DataRow drKullanici = UI.ListeleKullanici(cUIAraclari._iKullaniciID, string.Empty, null, string.Empty).Rows[0];
                            txtFaturaAdi.Text = drKullanici["ADI"].ToString() + " " + drKullanici["SOYADI"].ToString();
                            txtAdres.Text = drKullanici["ADRES"].ToString() + " " + drKullanici["SEHIRADI"].ToString();
                            txtAd.Text = drKullanici["ADI"].ToString();
                            txtSoyad.Text = drKullanici["SOYADI"].ToString();
                            txtEmail.Text = drKullanici["EMAIL"].ToString();
                            txtCepTelefonu.Text = drKullanici["CEPTELEFONU"].ToString();
                        }
                    }
                    gvSiparisDetaylar.DataSource = cUIAraclari._dtSepet;
                    gvSiparisDetaylar.DataBind();

                }
            }
            catch (Exception ex)
            {
                cUIAraclari.cLog.Write(ex, cUIAraclari._iKullaniciID.ToInt(0));
            }
        }
        protected void ddlOdeme_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlOdeme.SelectedValue == "1") // Peşin Ödeme
                {
                    // Peşin ödemede taksitlendirme tablosunu göndermeden direk bilgileri post ediyoruz.
                    pnTaksitlendirme.Visible = false;
                    ddlBankalar.Enabled = true;
                }
                else if (ddlOdeme.SelectedValue == "2") // Taksitli Ödeme
                {
                    // Hiç bir işlem yapmadan evvel taksitlendirme tablosunu açıyoruz
                    ddlBankalar.Enabled = false;
                    pnTaksitlendirme.Visible = true;

                    using (cTahsilat TI = new cTahsilat())
                    {
                        rpTaksitler.DataSource = TI.ListeleTaksit(0,ddlBankalar.SelectedValue.ToInt(0));
                        rpTaksitler.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                cUIAraclari.cLog.Write(ex, cUIAraclari._iKullaniciID.ToInt(0));
            }
        }
        protected void rpTaksitler_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            DataRowView rw = e.Item.DataItem as DataRowView;

            Literal ltradio = e.Item.FindControl("ltRadioButon") as Literal;
            Literal ltfiyat = e.Item.FindControl("ltFiyat") as Literal;

            ltradio.Text = "<input type=\"radio\" name=\"rbTaksit\" id=\"rbTaksit\" value=\"" + rw["TAKSIT"] + "\" runat=\"server\" />";
            ltfiyat.Text = (hfToplamTutar.Value.ToDecimal(1) * (1+rw["ORAN"].ToDecimal(1) / 100)).ToString();
        }
        protected void btnSiparisTamamla_Click(object sender, EventArgs e)
        {
            try
            {
                bool AdimDurum = true;
                long iTempID = 0;
                #region Adım 1. Kullanıcı Üye değilse üyeler tablomuza ekliyoruz
                if (cUIAraclari._iKullaniciID == 0)
                {
                    TBLUYELER tuye = new TBLUYELER();
                    tuye.ADI = txtAd.Text;
                    tuye.SOYADI = txtSoyad.Text;
                    tuye.EMAIL = txtEmail.Text;
                    tuye.CEPTELEFONU = txtCepTelefonu.Text;
                    tuye.ADRES = txtAdres.Text;
                    tuye.KULLANICI_TIPI = eKullaniciTipi.Misafir;

                    using (cUyeIslemleri UI = new cUyeIslemleri())
                        iTempID = UI.EkleKullanici(tuye);
                    if (iTempID > 0)
                        AdimDurum = true;
                    else
                        AdimDurum = false;
                }
                #endregion
                if (AdimDurum)
                {
                    TBLSIPARIS tsiparis = new TBLSIPARIS();
                    tsiparis.ADRES = txtAdres.Text;
                    tsiparis.FATURA_ADI = txtFaturaAdi.Text == String.Empty ? txtAd.Text : txtFaturaAdi.Text;
                    tsiparis.FATURA_VERGINO = txtFaturaVergiNo.Text;
                    tsiparis.ODEMETIPI = (eOdemeTipi)ddlOdemeTipi.SelectedValue.ToShort(0);
                    tsiparis.SIPARISDURUMU = eSiparisDurumu.Hazirlaniyor;
                    tsiparis.TUTAR = hfToplamTutar.Value.ToDecimal();
                    tsiparis.UYEID = cUIAraclari._iKullaniciID > 0 ? cUIAraclari._iKullaniciID : iTempID.ToInt(0);
                    #region Adım 2 Sanal Pos Tahsilatı yapılır.
                    if ((eOdemeTipi)ddlOdemeTipi.SelectedValue.ToShort() == eOdemeTipi.SanalPos)
                    {
                        using (cTahsilat TI = new cTahsilat())
                        {
                            DataRow dr = TI.ListeleBanka(ddlBankalar.SelectedValue.ToShort(0), null).Rows[0];
                            pnKrediKartıBilgileri.Visible = false;
                            pnTaksitlendirme.Visible = false;
                            if ((eOdemeTipi)ddlOdemeTipi.SelectedValue.ToShort() == eOdemeTipi.SanalPos)
                            {
                                // Sanal Pos Bilgileri, Başlangıç
                                ePayment.cc5payment payment = new ePayment.cc5payment();
                                payment.host = dr["HOST"].ToString();
                                payment.name = dr["KULLANICI_ADI"].ToString();
                                payment.password = dr["SIFRE"].ToString();
                                payment.clientid = dr["MAGAZA_NO"].ToString();
                                payment.orderresult = eSanalPosIslemDurumu.Test.ToInt(); // 0 olursa gerçek işlem, 1 olursa test işlemi
                                payment.cardnumber = txtKartNumarasi.Text; // kart no
                                payment.expmonth = ddlAylar.SelectedValue; // son kullanma ay
                                payment.expyear = ddlYillar.SelectedValue; // son kullanma yıl
                                payment.cv2 = txtGuvenlikKodu.Text; // güvenlik no
                                payment.currency = eParaBirimi.TL.ToString(); // para pirimi ( TL için 949 )
                                payment.chargetype = eSanalPosIslemTipi.Auth.ToString(); // satış
                                payment.subtotal = hfToplamTutar.Value; ; // toplam ücret
                                if (ddlOdeme.SelectedValue == "2")
                                    payment.taksit = Request.Form["rbTaksit"];
                                string sonuc = payment.appr;
                                string islemkodu = payment.procreturncode;
                                if (payment.processorder() == "1")
                                {
                                    if (sonuc == "Approved")
                                    {
                                        ltSonuc.Text = "İşleminiz Başarıyla Gerçekleşti";
                                        AdimDurum = true;
                                    }
                                    else if (sonuc == "Declined")
                                    {
                                        ltSonuc.Text = "Ödeme işlemi rededildi " + payment.errmsg;
                                        AdimDurum = false;
                                    }
                                    else
                                    {
                                        ltSonuc.Text = "Hata Oluştu : " + payment.errmsg;
                                        AdimDurum = false;
                                    }
                                }
                                else
                                {
                                    ltSonuc.Text = "Bağlantı Kurulmadı";
                                    AdimDurum = false;
                                }
                                tsiparis.BANKAID = ddlBankalar.SelectedValue.ToInt(0);
                                tsiparis.SPOSSONUC = ltSonuc.Text;
                            }
                        }// Sanal Pos Bilgileri, Sonu
                    }
                    else if ((eOdemeTipi)ddlOdemeTipi.SelectedValue.ToShort() == eOdemeTipi.Havale)
                        tsiparis.SPOSSONUC = txtBankaHavale.Text;
                    else if ((eOdemeTipi)ddlOdemeTipi.SelectedValue.ToShort() == eOdemeTipi.Kapida)
                        tsiparis.SPOSSONUC = txtKapıda.Text;
                    else if ((eOdemeTipi)ddlOdemeTipi.SelectedValue.ToShort() == eOdemeTipi.PostaCeki)
                        tsiparis.SPOSSONUC = txtPostaCeki.Text;
                    #endregion
                    #region Adim 3 Sipariş ve Detaylar Kaydedilir
                    if (AdimDurum)
                    {
                        using (cSiparisIslemleri SI = new cSiparisIslemleri())
                            iTempID = SI.EkleSparis(tsiparis);

                        
                        if (iTempID > 0)
                        {
                            #region Sipariş Detayları kaydediliyor
                            TBLSIPARISDETAY tSiparisDetay = new TBLSIPARISDETAY();
                            using (cSiparisIslemleri SI = new cSiparisIslemleri())
                            {
                                ltBilgi.Text = SI.ListeleSiparis(iTempID, 0, null, string.Empty).Rows[0]["GUID"].ToString();
                                foreach (GridViewRow gvRow in gvSiparisDetaylar.Rows)
                                {
                                    if (gvRow.RowType == DataControlRowType.DataRow)
                                    {
                                        Label lblTutar = (Label)gvRow.Cells[4].FindControl("lblTutar");
                                        tSiparisDetay.MIKTAR = gvRow.Cells[3].Text.ToInt(1);
                                        tSiparisDetay.SIPARISID = iTempID;
                                        tSiparisDetay.TUTAR = lblTutar.Text.ToDecimal();
                                        tSiparisDetay.URUNID = gvRow.Cells[0].Text.ToInt(1);
                                        if (SI.EkleSiparisDetay(tSiparisDetay) == 0)
                                            AdimDurum = false;
                                    }
                                }
                            }
                            #endregion
                            if (AdimDurum)
                            {
                                cUIAraclari.toastMesaj(this, eStatusType.Onay, "Siparişiniz Kaydedildi");
                                cUIAraclari._dtSepet.Rows.Clear();
                                StringBuilder sbIcerik = new StringBuilder();
                                sbIcerik.Append("Sayın " + txtAd.Text+ " " + txtSoyad.Text + ",<br>");
                                sbIcerik.Append("Sipariş İşleminiz Başarıyla Tamamlanmıştır.<br>");
                                sbIcerik.Append("<a href=\"" + Request.Url.Host + "/SiparisDetay.aspx?GUID=" +ltBilgi.Text+ "\">Siparişiniz Durumunu görmek için tıklayın </a>");
                                using(cGenelIslemler GI=new cGenelIslemler())
                                    GI.MailGonder("Sipariş Tamamlandı Bilgisi",sbIcerik.ToString()+ltBilgi.Text,txtEmail.Text);
                            }else
                                cUIAraclari.toastMesaj(this, eStatusType.Hata, "İşleminiz tamamlanmadı veya eksik tamamlandı, Tahsilat işlemi yapılmış olabilir. Lütfen yetkililerle iletişime geçiniz");
                        }
                        else
                            cUIAraclari.toastMesaj(this, eStatusType.Hata, "İşleminiz tamamlanmadı, Tahsilat işlemi yapılmış olabilir. Lütfen yetkililerle iletişime geçiniz");
                    }
                    else
                        cUIAraclari.toastMesaj(this, eStatusType.Uyari, "İşleminiz tamamlanmadı, Tahsilat işlemi yapılmamıştır. Tekrar deneyin veya yetkililerle iletişime geçiniz");
                    #endregion
                }
                else
                    cUIAraclari.toastMesaj(this, eStatusType.Bilgi, "İşleminiz tamamlanmadı, Tahsilat işlemi yapılmamıştır. Tekrar deneyin veya yetkililerle iletişime geçiniz");
            }
            catch (Exception ex)
            {
                cUIAraclari.cLog.Write(ex, cUIAraclari._iKullaniciID.ToInt(0));
            }
        }

        protected void gvSiparisDetaylar_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                using (cUrunGenel UG = new cUrunGenel())
                {
                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        DataRow dr = UG.ListeleUrun(DataBinder.Eval(e.Row.DataItem, "URUNID").ToInt(), 0, 0, null, null, null, null, string.Empty, string.Empty,false).Rows[0];
                        DataTable dtUrunGruplari = UG.ListeleUrunGrup(0, DataBinder.Eval(e.Row.DataItem, "URUNID").ToInt());
                        Label lblTutar = e.Row.FindControl("lblTutar") as Label;

                        if ((eParaBirimi)dr["PARABIRIMI"].ToShort() != eParaBirimi.TL)
                        {
                            if (dtUrunGruplari.Select("GRUP_TIPI=" + (short)eGrupTipi.SabitKurDolar).Length > 0)
                                lblTutar.Text = (((eEvetHayir)dr["INDIRIM"].ToShort(0) == eEvetHayir.Evet ? dr["INDIRIMLI_FIYAT"].ToDecimal() : dr["FIYATI"].ToDecimal()) * dtUrunGruplari.Select("GRUP_TIPI=" + (short)eGrupTipi.SabitKurDolar)[0]["GRUP_ACIKLAMA"].ToDecimal(1)).ToString();
                            else if (dtUrunGruplari.Select("GRUP_TIPI=" + (short)eGrupTipi.SabitKurEuro).Length > 0)
                                lblTutar.Text =(((eEvetHayir)dr["INDIRIM"].ToShort(0) == eEvetHayir.Evet ? dr["INDIRIMLI_FIYAT"].ToDecimal() : dr["FIYATI"].ToDecimal()) * dtUrunGruplari.Select("GRUP_TIPI=" + (short)eGrupTipi.SabitKurEuro)[0]["GRUP_ACIKLAMA"].ToDecimal(1)).ToString();
                            else
                                lblTutar.Text =(((eEvetHayir)dr["INDIRIM"].ToShort(0) == eEvetHayir.Evet ? dr["INDIRIMLI_FIYAT"].ToDecimal() : dr["FIYATI"].ToDecimal()) * cUIAraclari.KurDegeri((eParaBirimi)dr["PARABIRIMI"].ToShort())).ToString();
                        }
                        else
                        {
                            lblTutar.Text = (eEvetHayir)dr["INDIRIM"].ToShort(0) == eEvetHayir.Evet ? dr["INDIRIMLI_FIYAT"].ToString() : dr["FIYATI"].ToString();
                        }

                        hfToplamTutar.Value = (hfToplamTutar.Value.ToDecimal() + (lblTutar.Text.ToDecimal() * DataBinder.Eval(e.Row.DataItem, "MIKTAR").ToDecimal(1))).ToString();
                    }
                    ltTutarBilgileri.Text = "Toplam Tutar : " + hfToplamTutar.Value.ToString();
                }
            }
            catch (Exception ex)
            {
                cUIAraclari.cLog.Write(ex, cUIAraclari._iKullaniciID);
            }
        }

        protected void ddlOdemeTipi_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                dvHavale.Visible = false;
                dvKapida.Visible = false;
                dvPostaCeki.Visible = false;
                dvSanalPos.Visible = false;
                if ((eOdemeTipi)ddlOdemeTipi.SelectedValue.ToShort(0) == eOdemeTipi.Havale)
                {
                    dvHavale.Visible = true;
                }
                else if ((eOdemeTipi)ddlOdemeTipi.SelectedValue.ToShort(0) == eOdemeTipi.Kapida)
                {
                    dvKapida.Visible = true;
                }
                else if ((eOdemeTipi)ddlOdemeTipi.SelectedValue.ToShort(0) == eOdemeTipi.PostaCeki)
                {
                    dvPostaCeki.Visible = true;
                }
                else if ((eOdemeTipi)ddlOdemeTipi.SelectedValue.ToShort(0) == eOdemeTipi.SanalPos)
                {
                    dvSanalPos.Visible = true;
                    using (cTahsilat TI = new cTahsilat())
                    {
                        ddlBankalar.DataSource = TI.ListeleBanka(0, null);
                        ddlBankalar.DataTextField = "BANKA_ADI";
                        ddlBankalar.DataValueField = "ID";
                        ddlBankalar.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                cUIAraclari.cLog.Write(ex, cUIAraclari._iKullaniciID);
            }
        }
    }
}