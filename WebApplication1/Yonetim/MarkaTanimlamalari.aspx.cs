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
    public partial class MarkaTanimlamalari : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (UIAraclari._iKullaniciTipi != eKullaniciTipi.Yonetici)
                Response.Redirect(ResolveUrl("~/Default.aspx"));
            if (!Page.IsPostBack)
                GridDoldur();
        }

        private void GridDoldur()
        {
            try
            {
                using (cUrunGenel UG = new cUrunGenel())
                {
                    gvMarkalar.DataSource = UG.ListeleMarka(0);
                    gvMarkalar.DataBind();
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

                txtMarkaAdi.Text = string.Empty;
                txtAciklama.Text = string.Empty;
                txtKodu.Text = string.Empty;
                lblID.Text = "0";
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
                TBLMARKALAR tMarka = new TBLMARKALAR();
                tMarka.ADI = txtMarkaAdi.Text;
                tMarka.KODU = txtKodu.Text;
                tMarka.ACIKLAMA = txtAciklama.Text;
                if (!fuLogo.FileName.IsNull())
                {
                    fuLogo.SaveAs(Server.MapPath("~/Upload/Logo/") + fuLogo.FileName);
                    tMarka.LOGO = fuLogo.FileName;
                }
                int sonuc = 0;
                using (cUrunGenel UG = new cUrunGenel())
                {
                    if (lblID.Text.ToInt(0) == 0)
                        sonuc = UG.EkleMarka(tMarka);
                    else
                    {
                        tMarka.ID = lblID.Text.ToInt(0);
                        sonuc=UG.DuzenleMarka(tMarka);
                    }
                }
                if (sonuc > 0)
                    UIAraclari.toastMesaj(this, eStatusType.Onay, "İşleminiz Başarıyla Gerçekleştirildi");
                else
                    UIAraclari.toastMesaj(this, eStatusType.Hata, "İşleminiz Gerçekleştirilemedi");
                GridDoldur();
               
            }
            catch (Exception ex)
            {
                UIAraclari.cLog.Write(ex, UIAraclari._iKullaniciID);
                UIAraclari.toastMesaj(this, eStatusType.Hata, "İşleminiz Gerçekleştirilemedi");
            }
        }

        protected void gvKargolar_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                using (cUrunGenel UG = new cUrunGenel())
                {
                    DataRow dr = UG.ListeleMarka(gvMarkalar.SelectedDataKey["ID"].ToShort(-1)).Rows[0];
                    txtMarkaAdi.Text = dr["ADI"].ToString();
                    txtAciklama.Text = dr["ACIKLAMA"].ToString();
                    txtKodu.Text = dr["KODU"].ToString();
                    lblID.Text = dr["ID"].ToString();
                }
            }
            catch (Exception ex)
            {
                UIAraclari.cLog.Write(ex, UIAraclari._iKullaniciID);
            }
        }
    }
}