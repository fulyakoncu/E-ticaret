using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETicaretIslemleri;
using MiniCore;
using System.Data;

namespace ETicaret
{
    public partial class SifreSifirla : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    using (cUyeIslemleri UI = new cUyeIslemleri())
                    {
                        DataRow[] drU = UI.ListeleKullanici(Request["UID"].ToInt(-1), null, null, null).Select("EMAIL='" + Request["MAIL"].ToString()+"'");
                        if (drU.Length != 1)
                        {
                            btnGonder.Enabled = false;
                            UIAraclari.toastMesaj(this, eStatusType.Uyari, "Hatalı Bir İşlem Gerçekleştirdiniz, Şifre Sıfırlama Mailini Tekrar İsteyiniz");
                        }
                        else
                        {
                            if (drU[0]["GUID"].ToString() == Request["GUID"].ToString())
                                UIAraclari._iKullaniciID = UI.ListeleKullanici(0, null, null, Request["GUID"].ToString()).Rows[0]["ID"].ToInt(0);
                            else
                                UIAraclari.toastMesaj(this, eStatusType.Uyari, "Hatalı Bir İşlem Gerçekleştirdiniz, Şifre Sıfırlama Mailini Tekrar İsteyiniz");
                        }
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
                using (cUyeIslemleri UI = new cUyeIslemleri())
                {
                    if (UI.DeğistirSifre(UIAraclari._iKullaniciID, txtSifre.Text) == 1)
                        UIAraclari.toastMesaj(this, eStatusType.Onay, "İşleminiz Başarıyla Gerçekleştirildi");
                    else
                        UIAraclari.toastMesaj(this, eStatusType.Hata, "İşleminiz Gerçekleştirilemedi");
                }
            }
            catch (Exception ex)
            {
                UIAraclari.cLog.Write(ex, UIAraclari._iKullaniciID);
            }
        }
    }
}