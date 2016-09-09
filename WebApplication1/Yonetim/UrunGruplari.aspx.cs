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
    public partial class UrunGruplari : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (UIAraclari._iKullaniciTipi != eKullaniciTipi.Yonetici)
                Response.Redirect(ResolveUrl("~/Default.aspx"));
            if (!Page.IsPostBack)
            {
                GridDoldur();
                using (cUrunGenel UG = new cUrunGenel())
                {
                    ddlGruplar.DataSource = cAraclar.VerEnumListesi(typeof(eGrupTipi));
                    ddlGruplar.DataTextField = "Value";
                    ddlGruplar.DataValueField = "Key";
                    ddlGruplar.DataBind();
                }
            }
        }
        private void GridDoldur()
        {
            try
            {
                using (cUrunGenel UG = new cUrunGenel())
                {
                    gvGruplar.DataSource = UG.ListeGrupTanimi(0,null);
                    gvGruplar.DataBind();
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
                TBLURUN_GRUP_TANIMLARI Grup = new TBLURUN_GRUP_TANIMLARI();
                Grup.GRUP_ADI = txtGrupAdi.Text;
                Grup.ACIKLAMA = txtAciklama.Text;
                Grup.GRUP_TIPI = (eGrupTipi)ddlGruplar.SelectedValue.ToShort(0);
                int sonuc=0;
                using (cUrunGenel UG = new cUrunGenel())
                {
                    if (lblID.Text.ToInt(0) == 0)
                        sonuc = UG.EkleGrupTanimi(Grup);
                    else
                    {
                        Grup.ID = lblID.Text.ToInt(0);
                        sonuc = UG.DuzenleGrupTanimi(Grup);
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

        protected void gvGruplar_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                using (cUrunGenel UG = new cUrunGenel())
                {
                    DataRow dr = UG.ListeGrupTanimi(gvGruplar.SelectedDataKey["ID"].ToShort(0),null).Rows[0];
                    txtGrupAdi.Text = dr["GRUP_ADI"].ToString();
                    txtAciklama.Text = dr["ACIKLAMA"].ToString();
                    ddlGruplar.SelectedValue = dr["GRUP_TIPI"].ToShort(9).ToString();
                    lblID.Text = dr["ID"].ToString();
                }
            }
            catch (Exception ex)
            {
                UIAraclari.cLog.Write(ex, UIAraclari._iKullaniciID);
            }
        }

        protected void btnYeniKayit_Click(object sender, EventArgs e)
        {
            txtGrupAdi.Text = string.Empty;
            txtAciklama.Text = string.Empty;
            lblID.Text = "0";
        }
    }
}