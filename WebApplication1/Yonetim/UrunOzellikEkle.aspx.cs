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
    public partial class UrunOzellikEkle : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (UIAraclari._iKullaniciTipi != eKullaniciTipi.Yonetici)
                    Response.Redirect(ResolveUrl("~/Default.aspx"));
                if (!Page.IsPostBack)
                {
                    GridDoldur();
                    using (cUrunGenel UG = new cUrunGenel())
                    {
                        ddlOzellik.DataSource = UG.ListeOzellikTanimi(0);
                        ddlOzellik.DataTextField = "OZELLIK_ADI";
                        ddlOzellik.DataValueField = "ID";
                        ddlOzellik.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                UIAraclari.cLog.Write(ex, UIAraclari._iKullaniciID);
            }
        }
        private void GridDoldur()
        {
            try
            {
                using (cUrunGenel UG = new cUrunGenel())
                {

                    gvUOzellik.DataSource = UG.ListeleUrunOzellik(0, Request["UID"].ToInt(0));
                    gvUOzellik.DataBind();
                }
            }
            catch (Exception ex)
            {
                UIAraclari.cLog.Write(ex, UIAraclari._iKullaniciID);
            }
        }

        protected void gvUOzellik_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                using (cUrunGenel UG = new cUrunGenel())
                {
                    DataRow dr = UG.ListeleUrunOzellik(gvUOzellik.SelectedDataKey["ID"].ToInt(0), 0).Rows[0];
                    ddlOzellik.SelectedValue = dr["OZELLIKID"].ToString();
                    txtDeger.Text = dr["DEGER"].ToString();
                    lblID.Text = dr["ID"].ToString();
                }
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
                TBLURUN_OZELLIKLERI ptUOzellik = new TBLURUN_OZELLIKLERI();
                ptUOzellik.DEGER = txtDeger.Text;
                ptUOzellik.OZELLIKID = ddlOzellik.SelectedValue.ToInt(0);
                ptUOzellik.URUNID = Request["UID"].ToInt(0);
                int sonuc = 0;
                using (cUrunGenel UG = new cUrunGenel())
                {
                    if (lblID.Text.ToInt(0) == 0)
                        sonuc = UG.EkleUrunOzellik(ptUOzellik);
                    else
                    {
                        ptUOzellik.ID = lblID.Text.ToInt(0);
                        sonuc = UG.DuzenleUrunOzellik(ptUOzellik);
                    }
                }
                if (sonuc > 0)
                    UIAraclari.toastMesaj(this, eStatusType.Onay, "İşleminiz Başarıyla Gerçekleştirildi");
                else
                    UIAraclari.toastMesaj(this, eStatusType.Hata, "Hata Oluştu");
                GridDoldur();
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
                txtDeger.Text = string.Empty;
                ddlOzellik.SelectedIndex = 0;
                lblID.Text = "0";
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
                using (cUrunGenel UG = new cUrunGenel())
                {
                    if (lblID.Text.ToInt(0) > 0)
                        if (UG.SilUrunOzellik(lblID.Text.ToInt(0)) > 0)
                            UIAraclari.toastMesaj(this, eStatusType.Onay, "İşleminiz Başarıyla Gerçekleştirildi");
                        else
                            UIAraclari.toastMesaj(this, eStatusType.Hata, "Hata Oluştu");
                    else
                        UIAraclari.toastMesaj(this, eStatusType.Uyari, "Lütfen Kayıt Seçiniz");
                    GridDoldur();
                }
            }
            catch (Exception ex)
            {
                UIAraclari.cLog.Write(ex, UIAraclari._iKullaniciID);
            }
        }
    }
}