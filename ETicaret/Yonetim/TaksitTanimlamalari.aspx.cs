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
    public partial class TaksitTanimlamalari : System.Web.UI.Page
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
                    {
                        ddlBanka.DataSource = TI.ListeleBanka(0, null);
                        ddlBanka.DataTextField = "BANKA_ADI";
                        ddlBanka.DataValueField = "ID";
                        ddlBanka.DataBind();
                    }
                    GridDoldur();
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
                using (cTahsilat TI = new cTahsilat())
                {
                    gvTaksitler.DataSource = TI.ListeleTaksit(0,ddlBanka.SelectedValue.ToInt(-1));
                    gvTaksitler.DataBind();
                }
            }
            catch (Exception ex)
            {
                cUIAraclari.cLog.Write(ex, cUIAraclari._iKullaniciID);
            }
        }
        protected void gvTaksitler_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                using (cTahsilat TI = new cTahsilat())
                {
                    DataRow dr = TI.ListeleTaksit(gvTaksitler.SelectedDataKey["ID"].ToShort(-1),0).Rows[0];
                    lblID.Text = dr["ID"].ToString();
                    ddlBanka.SelectedValue = dr["BANKA_ID"].ToString();
                    txtTaksit.Text = dr["TAKSIT"].ToString();
                    txtOran.Text = dr["ORAN"].ToString();
                }
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
                txtTaksit.Text = string.Empty;
                txtOran.Text = string.Empty;
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
                TBLTAKSITLER taksit = new TBLTAKSITLER();
                taksit.BANKA_ID = Convert.ToInt16(ddlBanka.SelectedValue);
                taksit.TAKSIT = Convert.ToInt16(txtTaksit.Text);
                taksit.ORAN = Convert.ToDecimal(txtOran.Text);

                short sIslemSonuc = 0;
                using (cTahsilat TI = new cTahsilat())
                {
                    if (lblID.Text.ToShort(0) == 0)
                        sIslemSonuc = TI.EkleTaksit(taksit);
                    else
                    {
                        taksit.ID = lblID.Text.ToShort(0);
                        sIslemSonuc = TI.DuzenleTaksit(taksit);
                    }
                }
                if (sIslemSonuc > 0)
                    cUIAraclari.toastMesaj(this, eStatusType.Onay, "İşleminiz Başarıyla Gerçekleştirildi");
                else
                    cUIAraclari.toastMesaj(this, eStatusType.Hata, "İşleminiz Gerçekleştirilemedi");
                GridDoldur();
            }
            catch (Exception ex)
            {
                cUIAraclari.cLog.Write(ex, cUIAraclari._iKullaniciID);
            }
        }
        protected void btnSil_Click(object sender, EventArgs e)
        {
            try
            {
                using (cTahsilat TI = new cTahsilat())
                {
                    if (lblID.Text.ToShort(0) > 0)
                    {
                        if (TI.SilTaksit(lblID.Text.ToShort(0)) > 0)
                            cUIAraclari.toastMesaj(this, eStatusType.Onay, "İşleminiz Başarıyla Gerçekleşti");
                        else
                            cUIAraclari.toastMesaj(this, eStatusType.Hata, "İşleminiz Gerçekleştirilemedi");
                    }
                    else
                        cUIAraclari.toastMesaj(this, eStatusType.Uyari, "Lütfen Önce Seçiminizi Yapın");

                    GridDoldur();
                }
            }
            catch (Exception ex)
            {
                cUIAraclari.cLog.Write(ex, cUIAraclari._iKullaniciID);
            }
        }

        protected void ddlBanka_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GridDoldur();
                btnYeniKayit_Click(null, null);
            }
            catch (Exception ex)
            {
                cUIAraclari.cLog.Write(ex, cUIAraclari._iKullaniciID);
            }
        }
    }
}