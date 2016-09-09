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
    public partial class UrunYorum : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (cUIAraclari._iKullaniciTipi != eKullaniciTipi.Yonetici)
                    Response.Redirect(ResolveUrl("~/Default.aspx"));
                if (!Page.IsPostBack)
                    GridDoldur();
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
                using (cUrunGenel UI = new cUrunGenel())
                {
                    gvYorumlar.DataSource = UI.ListeleYorum(0, 0, null);
                    gvYorumlar.DataBind();
                }
            }
            catch (Exception ex)
            {
                cUIAraclari.cLog.Write(ex, cUIAraclari._iKullaniciID);
            }
        }

        [System.Web.Services.WebMethod()]
        public static string GetirYorum(int piID)
        {
            try
            {
                string strJson = string.Empty;
                using (cUrunGenel UI = new cUrunGenel())
                {
                    strJson = MiniCore.cAraclar.GetDataTableToJSon(UI.ListeleYorum(piID, 0, null), null);
                }
                return strJson;
            }
            catch (Exception ex)
            {
                cUIAraclari.cLog.Write(ex, cUIAraclari._iKullaniciID);
                return "";
            }
        }

        protected void gvYorumlar_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                using (cUrunGenel UI = new cUrunGenel())
                {
                    DataRow dr = UI.ListeleYorum(gvYorumlar.SelectedDataKey["ID"].ToShort(-1), 0, null).Rows[0];
                    if (dr["AKTIF"].ToShort(0) == (short)eAktifDurum.Pasif)
                    {
                        UI.IslemYorum(dr["ID"].ToInt(), eAktifDurum.Aktif);
                    }
                    else if (dr["AKTIF"].ToShort(0) == (short)eAktifDurum.Aktif)
                    {
                        UI.IslemYorum(dr["ID"].ToInt(), eAktifDurum.Pasif);
                    }
                    GridDoldur();
                }
            }
            catch (Exception ex)
            {
                cUIAraclari.cLog.Write(ex, cUIAraclari._iKullaniciID);
            }

        }
    }
}