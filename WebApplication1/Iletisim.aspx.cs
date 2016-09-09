using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETicaretIslemleri;
using System.Data;

namespace ETicaret
{
    public partial class About : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if(!Page.IsPostBack)
                    if (UIAraclari._iKullaniciID > 0)
                    {
                        using (cUyeIslemleri cUI = new cUyeIslemleri())
                        {
                            DataRow dr = cUI.ListeleKullanici(UIAraclari._iKullaniciID, null, null, null).Rows[0];
                            txtAdSoyad.Text = dr["ADI"].ToString() + " " + dr["SOYADI"].ToString();
                            txtEmail.Text = dr["EMAIL"].ToString();
                        }
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
                TBLBILDIRIMLER tBildirimler = new TBLBILDIRIMLER();
                tBildirimler.ADI_SOYADI = txtAdSoyad.Text;
                tBildirimler.BASLIK = txtBaslik.Text;
                tBildirimler.EMAIL = txtEmail.Text;
                tBildirimler.ICERIK = txtIcerik.Text;
                using (cGenelIslemler cGI = new cGenelIslemler())
                {
                    if (cGI.EkleBildirim(tBildirimler) > 0)
                        UIAraclari.toastMesaj(this, eStatusType.Onay, "İşleminiz Başarıyla Gerçekleştirildi");
                    else
                        UIAraclari.toastMesaj(this, eStatusType.Hata, "Mesajınız Gönderilemedi");
                }
            }
            catch (Exception ex)
            {
                UIAraclari.cLog.Write(ex, UIAraclari._iKullaniciID);
            }
        }

    }
}
