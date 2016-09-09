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
    public partial class UrunStokIslemi : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (cUIAraclari._iKullaniciTipi != eKullaniciTipi.Yonetici)
                Response.Redirect(ResolveUrl("~/Default.aspx"));
            if (!Page.IsPostBack)
            {
                GridDoldur();
                using (cUrunGenel UG = new cUrunGenel())
                {
                    ddlIslemTipi.DataSource = cAraclar.VerEnumListesi(typeof(eStokIslemTipi));
                    ddlIslemTipi.DataValueField = "Key";
                    ddlIslemTipi.DataTextField = "Value";
                    ddlIslemTipi.DataBind();
                }
            }
        }

        private void GridDoldur()
        {
            try
            {
                using (cUrunGenel UG = new cUrunGenel())
                {
                    gvUStok.DataSource = UG.ListeleStokIslemleri(0, Request["UID"].ToInt(0), null);
                    gvUStok.DataBind();
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
                TBLSTOKISLEMLERI stok = new TBLSTOKISLEMLERI();
                stok.ISLEMTIPI = (eStokIslemTipi)ddlIslemTipi.SelectedValue.ToShort(0);
                stok.MIKTAR = txtMiktar.Text.ToDecimal(0);
                stok.URUNID = Request["UID"].ToInt(0);
                
                int sonuc = 0;
                using (cUrunGenel UG = new cUrunGenel())
                {
                    if (lblID.Text.ToInt(0) == 0)
                        sonuc = UG.EkleUrunStokIslemi(stok);
                    else
                    {
                        stok.ID = lblID.Text.ToInt(0);
                        sonuc = UG.EkleUrunStokIslemi(stok);
                    }
                }
                if (sonuc > 0)
                    cUIAraclari.toastMesaj(this, eStatusType.Onay, "İşleminiz Başarıyla Gerçekleştirildi");
                else
                    cUIAraclari.toastMesaj(this, eStatusType.Hata, "Hata Oluştu");
                GridDoldur();
            }
            catch (Exception ex)
            {
                cUIAraclari.cLog.Write(ex, cUIAraclari._iKullaniciID);
            }
        }

        protected void btnYeniKayit_Click(object sender, EventArgs e)
        {
            lblID.Text = "0";
            ddlIslemTipi.SelectedIndex = 0;
            txtMiktar.Text = string.Empty;
        }

        protected void btnSil_Click(object sender, EventArgs e)
        {
            try
            {
                using (cUrunGenel UG = new cUrunGenel())
                {
                    if (UG.SilStokIslem(lblID.Text.ToLong(0)) > 0)
                        cUIAraclari.toastMesaj(this, eStatusType.Onay, "İşleminiz Başarıyla Gerçekleşti");
                    else
                        cUIAraclari.toastMesaj(this, eStatusType.Hata, "Hata Oluştu");

                    GridDoldur();
                }
            }
            catch (Exception ex)
            {
                cUIAraclari.cLog.Write(ex, cUIAraclari._iKullaniciID);
            }
        }

        protected void gvUOzellik_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                using (cUrunGenel UG = new cUrunGenel())
                {
                    DataRow dr = UG.ListeleStokIslemleri(gvUStok.SelectedDataKey["ID"].ToLong(0), 0, null).Rows[0];
                    lblID.Text = dr["ID"].ToString();
                    ddlIslemTipi.SelectedValue = dr["ISLEMTIPI"].ToString();
                    txtMiktar.Text = dr["MIKTAR"].ToString();
                }
            }
            catch (Exception ex)
            {
                cUIAraclari.cLog.Write(ex, cUIAraclari._iKullaniciID);
            }
        }
    }
}