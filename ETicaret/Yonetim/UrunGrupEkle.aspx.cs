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
    public partial class UrunGrupEkle : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (cUIAraclari._iKullaniciTipi != eKullaniciTipi.Yonetici)
                    Response.Redirect(ResolveUrl("~/Default.aspx"));
                if (!Page.IsPostBack)
                {
                    using (cUrunGenel UG = new cUrunGenel())
                    {
                        gvUrun.DataSource = UG.ListeGrupTanimi(0,null);
                        gvUrun.DataBind();
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
                using (cUrunGenel UG = new cUrunGenel())
                {
                    DataTable dt = UG.ListeleUrunGrup(0, Request["UID"].ToInt(0));
                    int sonuc = 0;
                    foreach (GridViewRow gvRow in gvUrun.Rows)
                    {
                        if (gvRow.RowType == DataControlRowType.DataRow)
                        {
                            CheckBox chc = (CheckBox)gvRow.Cells[0].FindControl("chcEkDurum");
                            //işaretli fakat tabloda yok yani yeni eklenmiş ise
                            if (chc.Checked && dt.Select("GRUPID=" + gvRow.Cells[1].Text).Length == 0)
                            {
                                TBLURUN_GRUPLARI tblUG = new TBLURUN_GRUPLARI();
                                tblUG.GRUPID = gvRow.Cells[1].Text.ToInt(0);
                                tblUG.URUNID = Request["UID"].ToInt(0);
                                sonuc = UG.EkleUrunGrup(tblUG);
                            }//işareti yok fakat tabloda kaydı var, yani gruptan çıkarılmış
                            else if (!chc.Checked && dt.Select("GRUPID=" + gvRow.Cells[1].Text).Length > 0)
                            {
                                sonuc = UG.SilUrunGrup(dt.Select("GRUPID=" + gvRow.Cells[1].Text)[0]["ID"].ToInt(0));
                            }
                        }
                    }
                    if(sonuc>0)
                        cUIAraclari.toastMesaj(this, eStatusType.Onay, "İşleminiz Başarı ile Gerçekleşti");
                    else
                        cUIAraclari.toastMesaj(this, eStatusType.Hata, "Herhangi bir değişiklik yapılmadı");
                }
            }
            catch (Exception ex)
            {
                cUIAraclari.cLog.Write(ex, cUIAraclari._iKullaniciID);
            }
        }

        protected void gvUrun_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    using (cUrunGenel UG = new cUrunGenel())
                    {
                        CheckBox chc = (CheckBox)e.Row.Cells[0].FindControl("chcEkDurum");
                        DataTable dt = UG.ListeleUrunGrup(0,Request["UID"].ToInt(0));
                        foreach (DataRow dr in dt.Rows)
                        {
                            if (e.Row.Cells[1].Text == dr["GRUPID"].ToString())
                            {
                                chc.Checked = true;
                            }
                        }
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