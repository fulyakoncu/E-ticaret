using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETicaretIslemleri;
using MiniCore;
using System.Web.UI.HtmlControls;

namespace ETicaret
{
    public partial class UrunDetay : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    using (cUrunGenel UI = new cUrunGenel())
                    {
                        dvTLDegeri.Visible = false;
                        DataRow dr = UI.ListeleUrun(Request["ID"].ToInt(-1), 0, 0, null, null, null, null, string.Empty, string.Empty,false).Rows[0];
                        DataTable dtResim = UI.ListeleUrunResim(0, Request["ID"].ToInt(-1), eEvetHayir.Hayir, null);
                        DataTable dtUrunGruplari = UI.ListeleUrunGrup(0, Request["ID"].ToInt(-1));
                        ltUrunAdi.Text = dr["MODEL"].ToString();
                        ltUrunFiyat.Text = dr["FIYATI"].ToString() + cAraclar.GetDescription((eParaBirimi)dr["PARABIRIMI"].ToShort());
                        ltMarka.Text = dr["MARKA_ADI"].ToString();
                        ltAciklama.Text = dr["ACIKLAMA"].ToString()+"<br>";
                        ltKategori.Text = dr["KATEGORI_ADI"].ToString();
                        ltStok.Text=UI.VerKalanStok(Request["ID"].ToInt()) > 0?"Stokta var":"Stokta Yok";
                        // ürünün indirimli olup olmadığu durumu
                        if ((eEvetHayir)dr["INDIRIM"].ToShort(0) == eEvetHayir.Evet)
                        {
                            pnIndirim.Visible = true;
                            ltIndirim.Text = dr["INDIRIMLI_FIYAT"].ToString() + cAraclar.GetDescription((eParaBirimi)dr["PARABIRIMI"].ToShort());
                        }
                        if ((eParaBirimi)dr["PARABIRIMI"].ToShort() != eParaBirimi.TL)
                        {
                            dvTLDegeri.Visible = true;
                            
                            if (dtUrunGruplari.Select("GRUP_TIPI=" + (short)eGrupTipi.SabitKurDolar).Length > 0)
                                ltTLDegeri.Text = (((eEvetHayir)dr["INDIRIM"].ToShort(0) == eEvetHayir.Evet?dr["INDIRIMLI_FIYAT"].ToDecimal():dr["FIYATI"].ToDecimal()) * dtUrunGruplari.Select("GRUP_TIPI=" + (short)eGrupTipi.SabitKurDolar)[0]["GRUP_ACIKLAMA"].ToDecimal(1)).ToString();
                            else if(dtUrunGruplari.Select("GRUP_TIPI=" + (short)eGrupTipi.SabitKurEuro).Length > 0)
                                ltTLDegeri.Text = (((eEvetHayir)dr["INDIRIM"].ToShort(0) == eEvetHayir.Evet ? dr["INDIRIMLI_FIYAT"].ToDecimal() : dr["FIYATI"].ToDecimal()) * dtUrunGruplari.Select("GRUP_TIPI=" + (short)eGrupTipi.SabitKurEuro)[0]["GRUP_ACIKLAMA"].ToDecimal(1)).ToString();
                            else
                                ltTLDegeri.Text = (((eEvetHayir)dr["INDIRIM"].ToShort(0) == eEvetHayir.Evet ? dr["INDIRIMLI_FIYAT"].ToDecimal() : dr["FIYATI"].ToDecimal()) * cUIAraclari.KurDegeri((eParaBirimi)dr["PARABIRIMI"].ToShort())).ToString();
                        }


                        // eğer ürün resmi 1 den fazlaysa resim galerideki ileri geri butonlar çıkıcak tek resim ise çıkmıycak
                        foreach (DataRow drGrup in dtUrunGruplari.Rows)
                        {
                            if ((eGrupTipi)drGrup["GRUP_TIPI"].ToShort() == eGrupTipi.Diger)
                                ltAciklama.Text += drGrup["GRUP_ACIKLAMA"].ToString() + "<br>";
                        }

                        if (dtResim.Rows.Count > 1)
                        {
                            pnCokluResim.Visible = true;
                        }
                        rpResimler.DataSource = dtResim;
                        rpResimler.DataBind();

                        // ürün özellikleri listele
                        rpUrunOzellikleri.DataSource = UI.ListeleUrunOzellik(0, Request["ID"].ToInt(-1));
                        rpUrunOzellikleri.DataBind();

                        // yorumları listele
                        rpYorumlar.DataSource = UI.ListeleYorum(0,Request["ID"].ToInt(-1), eAktifDurum.Aktif);
                        rpYorumlar.DataBind();

                    Page.Title=dr["MODEL"].ToString();
                    Page.MetaKeywords = dr["MODEL"].ToString() +","+ dr["MARKA_ADI"].ToString();
                    Page.MetaDescription =dr["MODEL"].ToString() +","+ dr["MARKA_ADI"].ToString()+ dr["ACIKLAMA"].ToString();
                    }
                }
                if (cUIAraclari._iKullaniciID == 0)
                {
                    dvYorumEkle.Visible = false;
                    //cUIAraclari.toastMesaj(this, eStatusType.Hata, "Lütfen Üye Girşi Yapınız");
                }
            }
            catch (Exception ex)
            {
                cUIAraclari.cLog.Write(ex, cUIAraclari._iKullaniciID.ToInt(0));
            }
        }

        protected void rpYorumlar_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            DataRowView rw = e.Item.DataItem as DataRowView;

            Literal ltTarih = e.Item.FindControl("ltTarih") as Literal;
            Literal ltYazan = e.Item.FindControl("ltYorumYazan") as Literal;
            
            ltTarih.Text = rw["TARIH"].ToDateTime().ToShortDateString();

            using (cUyeIslemleri UI = new cUyeIslemleri())
            {
                DataRow dr = UI.ListeleKullanici(rw["UYE_ID"].ToInt(), "", null, "").Rows[0];
                ltYazan.Text = dr["ADI"].ToString() + " " + dr["SOYADI"].ToString();
            }   
        }
        protected void btnKaydet_Click(object sender, EventArgs e)
        {
            try
            {
                TBLYORUMLAR yorumlar = new TBLYORUMLAR();
                yorumlar.URUN_ID = Request["ID"].ToInt(-1);
                yorumlar.UYE_ID = cUIAraclari._iKullaniciID;
                yorumlar.BASLIK = txtBaslik.Text;
                yorumlar.MESAJ = txtMesaj.Text;
                yorumlar.TARIH = DateTime.Now.ToDateTime();
                yorumlar.IP = HttpContext.Current.Request.UserHostAddress;
                yorumlar.AKTIF = eAktifDurum.Pasif;
                using (cUrunGenel UI = new cUrunGenel())
                {
                    if (UI.EkleYorum(yorumlar) > 0)
                        cUIAraclari.toastMesaj(this, eStatusType.Onay, "İşleminiz Başarıyla Gerçekleşti.");
                    else
                        cUIAraclari.toastMesaj(this, eStatusType.Hata, "İşleminiz Gerçekleştirilirken Hata oluştu. Lütfen Sistem Yöneticisine Başvurun.");
                }
            }
            catch (Exception ex)
            {
                cUIAraclari.cLog.Write(ex, cUIAraclari._iKullaniciID.ToInt(0));
            }
        }

        protected void btnSepeteAta_Click(object sender, EventArgs e)
        {
            try
            {
                using (cUrunGenel UG = new cUrunGenel())
                {
                    if (UG.VerKalanStok(Request["ID"].ToInt(-1)) < txtAdet.Text.ToInt())
                    {
                        cUIAraclari.toastMesaj(this, eStatusType.Hata, "Stokta Yeterli Ürün Yok!");
                    }
                    else
                    {
                        DataRow dr = cUIAraclari._dtSepet.NewRow();
                        if (cUIAraclari._dtSepet.Rows.Count == 0)
                            dr["SIRANO"] = 1;
                        else
                            dr["SIRANO"] = cUIAraclari._dtSepet.Rows[cUIAraclari._dtSepet.Rows.Count - 1]["SIRANO"].ToInt(0) + 1;
                        dr["URUNID"] = Request["ID"].ToInt();
                        dr["MIKTAR"] = txtAdet.Text.ToInt(1);
                        dr["URUNADI"] = ltUrunAdi.Text;
                        dr["MARKAADI"] = ltMarka.Text;
                        cUIAraclari._dtSepet.Rows.Add(dr);
                        SepetDoldur();
                    }
                }
            }
            catch (Exception ex)
            {
                cUIAraclari.cLog.Write(ex, cUIAraclari._iKullaniciID);
            }
        }
        private void SepetDoldur()
        {
            GridView gvSepet = (GridView)this.Page.Master.FindControl("gvSepet");
            gvSepet.DataSource = cUIAraclari._dtSepet;
            gvSepet.DataBind();
        }
    }
}