using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MiniCore;
using ETicaretIslemleri;
using System.Data;
namespace ETicaret.Yonetim
{
    public partial class SiparisDuzenle1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (cUIAraclari._iKullaniciTipi != eKullaniciTipi.Yonetici)
                    Response.Redirect(ResolveUrl("~/Default.aspx"));
                if (!Page.IsPostBack)
                {
                    using (cTahsilat TI = new cTahsilat())
                    using (cGenelIslemler GI = new cGenelIslemler())
                    using (cSiparisIslemleri SI = new cSiparisIslemleri())
                    {
                        ddlBanka.DataSource = TI.ListeleBanka(0, null);
                        ddlBanka.DataTextField = "BANKA_ADI";
                        ddlBanka.DataValueField = "ID";
                        ddlBanka.DataBind();
                        ddlBanka.Items.Insert(0, new ListItem("Banka Seçilmemiş", "0"));

                        ddlKargo.DataSource = GI.ListeleKargo(0, null);
                        ddlKargo.DataTextField = "ADI";
                        ddlKargo.DataValueField = "ID";
                        ddlKargo.DataBind();
                        ddlKargo.Items.Insert(0, new ListItem("Kargo Seçilmemiş", "0"));

                        ddlOdemeTipi.DataSource = cAraclar.VerEnumListesi(typeof(eOdemeTipi));
                        ddlOdemeTipi.DataTextField = "Value";
                        ddlOdemeTipi.DataValueField = "Key";
                        ddlOdemeTipi.DataBind();

                        ddlSiparisDurumu.DataSource = cAraclar.VerEnumListesi(typeof(eSiparisDurumu));
                        ddlSiparisDurumu.DataTextField = "Value";
                        ddlSiparisDurumu.DataValueField = "Key";
                        ddlSiparisDurumu.DataBind();

                        DataRow dr = SI.ListeleSiparis(Request["ID"].ToLong(0), 0, null, string.Empty).Rows[0];
                        lblUye.Text = dr["UYE_ADISOYADI"].ToString();
                        txtFAdi.Text = dr["FATURA_ADI"].ToString();
                        txtFVergiNo.Text = dr["FATURA_VERGINO"].ToString();
                        txtAdres.Text = dr["ADRES"].ToString();
                        txtSonuc.Text = dr["SPOSSONUC"].ToString();
                        txtKargoKodu.Text = dr["KARGOKODU"].ToString();
                        ddlBanka.SelectedValue = dr["BANKAID"].ToInt(0).ToString();
                        ddlKargo.SelectedValue = dr["KARGOID"].ToInt(0).ToString();
                        ddlOdemeTipi.SelectedValue = dr["ODEMETIPI"].ToString();
                        ddlSiparisDurumu.SelectedValue = dr["SIPARISDURUMU"].ToString();
                        gvSiparisDetaylar.DataSource = SI.ListeleSiparisDetay(0, Request["ID"].ToLong(0), 0);
                        gvSiparisDetaylar.DataBind();
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
                TBLSIPARIS tSiparis = new TBLSIPARIS();
                tSiparis.ADRES = txtAdres.Text;
                if (ddlBanka.SelectedValue.ToInt(0) > 0)
                    tSiparis.BANKAID = ddlBanka.SelectedValue.ToInt(0);
                tSiparis.FATURA_ADI = txtFAdi.Text;
                tSiparis.FATURA_VERGINO = txtFVergiNo.Text;
                tSiparis.ID = Request["ID"].ToInt(0);
                if (ddlKargo.SelectedValue.ToInt(0) > 0)
                    tSiparis.KARGOID = ddlKargo.SelectedValue.ToShort(0);
                tSiparis.KARGOKODU = txtKargoKodu.Text;
                tSiparis.ODEMETIPI = (eOdemeTipi)ddlOdemeTipi.SelectedValue.ToShort(0);
                tSiparis.SIPARISDURUMU = (eSiparisDurumu)ddlSiparisDurumu.SelectedValue.ToShort(0);
                tSiparis.SPOSSONUC = txtSonuc.Text;
                using (cSiparisIslemleri SI = new cSiparisIslemleri())
                    if (SI.DuzenleSiparis(tSiparis) > 0)
                        cUIAraclari.toastMesaj(this, eStatusType.Onay, "İşleminiz Başarıyla Tamamlandı");
                    else
                        cUIAraclari.toastMesaj(this, eStatusType.Hata, "Hata Oluştu");
            }
            catch (Exception ex)
            {
                cUIAraclari.cLog.Write(ex, cUIAraclari._iKullaniciID);
            }
        }

        protected void btnCikisYap_Click(object sender, EventArgs e)
        {
            try
            {
                TBLSTOKISLEMLERI stok = new TBLSTOKISLEMLERI();
                bool sonuc = true;
                using(cUrunGenel UG=new cUrunGenel())
                foreach (GridViewRow gvRow in gvSiparisDetaylar.Rows)
                {
                    if (gvRow.RowType == DataControlRowType.DataRow)
                    {
                        stok.ISLEMTIPI = eStokIslemTipi.Cikis;
                        stok.MIKTAR = gvRow.Cells[3].Text.ToDecimal();
                        stok.REFERANSID = Request["ID"].ToLong(-1);
                        stok.URUNID = gvRow.Cells[0].Text.ToInt(-1);
                        if (UG.EkleUrunStokIslemi(stok) == 0)
                            sonuc = false;
                    }
                }
                if (sonuc)
                    cUIAraclari.toastMesaj(this, eStatusType.Onay, "İşleminiz Başarıyla tamamlandı");
                else
                    cUIAraclari.toastMesaj(this, eStatusType.Hata, "İşleminiz sırasında hata oluştu lütfen kayıtları kontrol ediniz");
            }
            catch (Exception ex)
            {
                cUIAraclari.cLog.Write(ex, cUIAraclari._iKullaniciID);
            }
        }
    }
}