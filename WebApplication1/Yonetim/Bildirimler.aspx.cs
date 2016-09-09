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
    public partial class Bildirimler : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (UIAraclari._iKullaniciTipi != eKullaniciTipi.Yonetici)
                    Response.Redirect(ResolveUrl("~/Default.aspx"));
                if(!Page.IsPostBack)
                    GridDoldur();
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
                using (cGenelIslemler GI = new cGenelIslemler())
                {
                    gvBildirimler.DataSource = GI.ListeleBildirimler(0);
                    gvBildirimler.DataBind();
                }
            }
            catch (Exception ex)
            {
                UIAraclari.cLog.Write(ex, UIAraclari._iKullaniciID);
            }
        }

        [System.Web.Services.WebMethod()]
        public static string GetirBildirim(int piID)
        {
            try
            {
                string strJson=string.Empty;
                using(cGenelIslemler cGI=new cGenelIslemler())
                {
                    strJson =MiniCore.cAraclar.GetDataTableToJSon(cGI.ListeleBildirimler(piID),null);
                }
                return strJson;
            }
            catch (Exception ex)
            {
                UIAraclari.cLog.Write(ex, UIAraclari._iKullaniciID);
                return "";
            }
        }

        protected void gvBildirimler_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                using (cGenelIslemler GI = new cGenelIslemler())
                {
                    DataRow dr = GI.ListeleBildirimler(gvBildirimler.SelectedDataKey["ID"].ToInt(0)).Rows[0];
                    txtEmail.Text = dr["EMAIL"].ToString();
                    txtBaslik.Text = dr["BASLIK"].ToString();
                }
            }
            catch (Exception ex)
            {
                UIAraclari.cLog.Write(ex, UIAraclari._iKullaniciID);
            }
        }

        protected void btnGonder_Click(object sender, EventArgs e)
        {
            try
            {
                using (cGenelIslemler GI = new cGenelIslemler())
                {
                    if (GI.MailGonder(txtBaslik.Text, txtCevap.Text, txtEmail.Text) > 0)
                        UIAraclari.toastMesaj(this, eStatusType.Onay, "Mail Gönderildi");
                    else
                        UIAraclari.toastMesaj(this, eStatusType.Hata, "Mail Gönderilemedi");
                }
            }
            catch (Exception ex)
            {
                UIAraclari.cLog.Write(ex, UIAraclari._iKullaniciID);
            }
        }
    }
}