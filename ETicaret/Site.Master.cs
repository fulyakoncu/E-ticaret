using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETicaretIslemleri;
using MiniCore;
using System.Data;
using System.Text;

namespace ETicaret
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    if (cUIAraclari._iKullaniciID != 0)
                    {
                        dvLogin.Visible = false;
                        dvUPanel.Visible = true;
                        UyePaneli();
                    }
                    else
                    {
                        dvLogin.Visible = true;
                        dvUPanel.Visible = false;
                    }
                    GelecekUrunler();
                    StringBuilder sb=new StringBuilder();
                    ulKategoriler.InnerHtml= KategoriDoldur(0,sb);
                    if (cUIAraclari._dtSepet.Columns.Count == 0)
                        OlusturKullaniciSepeti();
                    DoldurSepet();
                }
            }
            catch (Exception ex)
            {
                cUIAraclari.cLog.Write(ex, cUIAraclari._iKullaniciID); 
            }
        }
        private string KategoriDoldur(short psID,StringBuilder sb)
        {
            try
            {
                using (cUrunGenel UG = new cUrunGenel())
                {
                    DataTable dt = UG.ListeleKategori(0, psID, null);
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dt.Rows[0]["ID"].ToShort(0) == dr["ID"].ToShort(0) && dr["NODEID"].ToShort(0) != 0)
                            sb.Append("<ul><li class=\"top_border\"></li>");
                        if (UG.ListeleKategori(0, dr["ID"].ToShort(0), null).Rows.Count > 0)
                            sb.Append("<li class=\"current\">");
                        else
                            sb.Append("<li>");
                        sb.Append("<a href=\"" + ResolveUrl("~/Kategoriler/" + dr["ID"] + "_" + cAraclar.URLDuzelt(dr["ADI"].ToString()+".aspx")) + "\">" + dr["ADI"].ToString() + "</a>");
                        KategoriDoldur(dr["ID"].ToShort(0),sb);
                        sb.Append("</li>");
                        if (dt.Rows[dt.Rows.Count - 1]["ID"].ToShort(0) == dr["ID"].ToShort(0))
                            sb.Append("<li class=\"bottom_border\"></li></ul>");
                    }
                }
                return sb.ToString();
            }
            catch (Exception ex)
            {
                cUIAraclari.cLog.Write(ex, cUIAraclari._iKullaniciID);
                return string.Empty;
            }
        }
        protected void btnSifre_Click(object sender, EventArgs e)
        {
            try
            {
                using(cGenelIslemler GI=new cGenelIslemler())
                using(cUyeIslemleri UI=new cUyeIslemleri())
                {
                    DataTable dt=UI.ListeleKullanici(0, txtMailsifre.Text,null,null);
                    if (dt.Rows.Count == 0)
                        cUIAraclari.toastMesaj(this, eStatusType.Uyari, "Geçersiz Mail Adresi");
                    else
                    {
                        StringBuilder sbIcerik = new StringBuilder();
                        sbIcerik.Append("Sayın " + dt.Rows[0]["ADI"].ToString() + " " + dt.Rows[0]["SOYADI"].ToString() + ",<br>");
                        sbIcerik.Append("Aşağıdaki linke tıklayarak yeni bir şifre oluşturabilirsiniz<br>");
                        sbIcerik.Append("<a href=\"" + Request.Url.Host+ "/SifreSifirla.aspx?UID=" + dt.Rows[0]["ID"].ToString() + "&MAIL=" + dt.Rows[0]["EMAIL"] + "&GUID=" + dt.Rows[0]["GUID"].ToString() + "\">Şifrenizi Değiştirmek için Tıklayın </a>");
                        if (GI.MailGonder("Şifre Değiştirme", sbIcerik.ToString(), dt.Rows[0]["EMAIL"].ToString()) > 0)
                            cUIAraclari.toastMesaj(this, eStatusType.Onay, "İşleminiz Başarıyla Tamamlanmıştır");
                        else
                            cUIAraclari.toastMesaj(this, eStatusType.Hata, "İşleminiz Gerçekleştirilemedi. Lütfen Tekrar Deneyiniz");
                    }
                }
            }
            catch (Exception ex)
            {
                cUIAraclari.cLog.Write(ex, cUIAraclari._iKullaniciID);
            }
        }
        protected void btnGiris_Click(object sender, EventArgs e)
        {
            try
            {
                using (cUyeIslemleri UI = new cUyeIslemleri())
                {
                    DataRow dr = UI.GirisYap(txtEmail.Text, txtSifre.Text);
                    if (dr != null)
                    {
                        cUIAraclari._iKullaniciID = dr["ID"].ToInt(0);
                        cUIAraclari._iKullaniciTipi = (eKullaniciTipi)dr["KULLANICI_TIPI"].ToShort(0);
                        dvLogin.Visible = false;
                        dvUPanel.Visible = true;
                        UyePaneli();
                    }
                    else
                        cUIAraclari.toastMesaj(this, eStatusType.Bilgi, "Kullanıcı Adı ve Şifrenizi Kontrol Ediniz");
                }
            }
            catch (Exception ex)
            {
                cUIAraclari.cLog.Write(ex, cUIAraclari._iKullaniciID);
            }
        }
        private void UyePaneli()
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("<li class=\"top_border\"></li>");
                sb.Append("<li><a href=\""+ResolveUrl("~/Uye/Bilgilerim.aspx")+"\">Bilgilerim</a></li>");
                sb.Append("<li><a href=\""+ResolveUrl("~/Uye/Siparislerim.aspx")+"\">Siparişlerim</a></li>");
                sb.Append("<li><a href=\"" + ResolveUrl("~/Uye/UrunListem.aspx") + "\">Ürün Listem</a></li>");
                if ((eKullaniciTipi)cUIAraclari._iKullaniciTipi == eKullaniciTipi.Yonetici)
                {
                    //Genel Tanımlar
                    sb.Append("<li class=\"current\"><a href=\"#\">Genel Tanımlamalar</a><ul>");
                    sb.Append("<li class=\"top_border\"></li>");
                    sb.Append("<li><a href=\""+ResolveUrl("~/Yonetim/KurumTanimlamalari.aspx")+"\">Kurum Tanımlamaları</a></li>");
                    sb.Append("<li><a href=\""+ResolveUrl("~/Yonetim/KargoTanimlamalari.aspx")+"\">Kargo Tanımlamaları</a></li>");
                    sb.Append("<li><a href=\""+ResolveUrl("~/Yonetim/BankaTanimlamalari.aspx")+"\">Banka Tanımlamaları</a></li>");
                    sb.Append("<li><a href=\""+ResolveUrl("~/Yonetim/TaksitTanimlamalari.aspx")+"\">Sanal Pos</a></li>");
                    sb.Append("<li><a href=\"" + ResolveUrl("~/Yonetim/Raporlar.aspx") + "\">Rapor Tanımlamaları</a></li>");
                    sb.Append("<li class=\"bottom_border\"></li></ul></li>");
                    //Uye İşlemleri
                    sb.Append("<li><a href=\"" + ResolveUrl("~/Yonetim/UyeListesi.aspx") + "\">Üye Listesi</a></li>");
                    //Sipariş İşlemleri Sipariş GUID eklenecek üyeliksiz siparişler için
                    sb.Append("<li><a href=\"" + ResolveUrl("~/Yonetim/Siparisler.aspx") + "\">Siparişler</a></li>");
                    //kategori
                    sb.Append("<li><a href=\""+ResolveUrl("~/Yonetim/Kategoriler.aspx")+"\">Kategori İşlemleri</a></li>");
                    //urun Ürün Kodu istenecek, faturlarda kullanılacak
                    sb.Append("<li class=\"current\"><a href=\"#\">Ürün İşlemleri</a><ul>");
                    sb.Append("<li class=\"top_border\"></li>");
                    sb.Append("<li><a href=\""+ResolveUrl("~/Yonetim/UrunEkleme.aspx")+"\">Ürün Ekleme</a></li>");
                    sb.Append("<li><a href=\""+ResolveUrl("~/Yonetim/UrunGruplari.aspx")+"\">Ürün Grupları</a></li>");
                    sb.Append("<li><a href=\""+ResolveUrl("~/Yonetim/UrunOzellikleri.aspx")+"\">Ürün Özellikleri</a></li>");
                    sb.Append("<li><a href=\"" + ResolveUrl("~/Yonetim/MarkaTanimlamalari.aspx") + "\">Marka Tanımlamaları</a></li>");
                    sb.Append("<li><a href=\"" + ResolveUrl("~/Yonetim/UrunStokListesi.aspx") + "\">Stok Raporu</a></li>");
                    sb.Append("<li><a href=\""+ResolveUrl("~/Yonetim/UrunYorum.aspx")+"\">Yorumlar</a></li>");
                    sb.Append("<li class=\"bottom_border\"></li></ul></li>");

                    sb.Append("<li><a href=\"" + ResolveUrl("~/Yonetim/Bildirimler.aspx") + "\">Bildirimler</a></li>");
                }
                sb.Append("<li><a href=\"" + ResolveUrl("~/Cikis.ashx") + "\">Çıkış</a></li>");
                sb.Append("<li class=\"bottom_border\"></li>");
                ulYonetim.InnerHtml = sb.ToString();
            }
            catch (Exception ex)
            {
                cUIAraclari.cLog.Write(ex, cUIAraclari._iKullaniciID);
            }
        }
        private void GelecekUrunler()
        {
            try
            {
                using(cUrunListeleri cUL=new cUrunListeleri())
                {
                    foreach (DataRow dr in cUL.ListeGrupTipiUrunleri(eGrupTipi.GelecekUrun,3).Rows)
                    {
                        ulYeniUrunler.InnerHtml = ulYeniUrunler.InnerHtml + "<li><table width=\"100%\"><tr><td align=\"center\"><img src=\"" + ResolveUrl("~/Upload/UrunResimleri/" + dr["ID"].ToString() + "/" + dr["ANARESIM"].ToString()) + "\"></td></tr><tr><td align=\"center\"><a href=\"" + ResolveUrl("~/Markalar/" + dr["MARKA"]) + "_" + cAraclar.URLDuzelt(dr["MARKA_ADI"].ToString()) + ".aspx\">" + dr["MARKA_ADI"].ToString() + "</a><br><a href=\"" + ResolveUrl("~/Urunler/" + dr["ID"] + "_" +cAraclar.URLDuzelt(dr["MODEL"].ToString())) + ".aspx\">" + dr["MODEL"].ToString() + "</a></td></tr></table></li>";
                    }
                }
            }
            catch (Exception ex)
            {
                cUIAraclari.cLog.Write(ex, cUIAraclari._iKullaniciID);
            }
        }
        private void OlusturKullaniciSepeti()
        {
            try
            {
                cUIAraclari._dtSepet.Columns.Add("SIRANO", typeof(int));
                cUIAraclari._dtSepet.Columns.Add("URUNID", typeof(int));
                cUIAraclari._dtSepet.Columns.Add("MIKTAR", typeof(int));
                cUIAraclari._dtSepet.Columns.Add("URUNADI", typeof(string));
                cUIAraclari._dtSepet.Columns.Add("MARKAADI", typeof(string));
            }
            catch (Exception ex)
            {
                cUIAraclari.cLog.Write(ex, cUIAraclari._iKullaniciID);
            }
        }
        private void DoldurSepet()
        {
            gvSepet.DataSource = cUIAraclari._dtSepet;
            gvSepet.DataBind();
        }
        protected void gvSepet_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cUIAraclari._dtSepet.Rows.Remove(cUIAraclari._dtSepet.Select("SIRANO=" + gvSepet.SelectedDataKey["SIRANO"].ToString())[0]);
                DoldurSepet();
            }
            catch (Exception ex)
            {
                cUIAraclari.cLog.Write(ex, cUIAraclari._iKullaniciID);
            }
        }

        protected void btnAra_Click(object sender, EventArgs e)
        {
            Response.Redirect(ResolveUrl("~/Arama.aspx?Aranan=" + txtAra.Text));
        }

        protected void btnSiparisDogrula_Click(object sender, EventArgs e)
        {
            Response.Redirect(ResolveUrl("~/SiparisDetay.aspx?GUID=" + txtSiparisKodu.Text));
        }
    }
}
