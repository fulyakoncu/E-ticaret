using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MiniCore;
using ETicaretIslemleri;
using System.Data;


using System.Data.SqlClient;
namespace ETicaret
{
    public partial class UyeKayit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    using (cGenelIslemler GI = new cGenelIslemler())
                    {
                        rblCinsiyet.DataSource = cAraclar.VerEnumListesi(typeof(eCinsiyet));
                        rblCinsiyet.DataValueField = "Key";
                        rblCinsiyet.DataTextField = "Value";
                        rblCinsiyet.DataBind();
            
                        ddlIl.DataSource = GI.ListeleSehirler(0);
                        ddlIl.DataTextField = "ADI";
                        ddlIl.DataValueField = "ID";
                        ddlIl.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                UIAraclari.cLog.Write(ex, UIAraclari._iKullaniciID.ToInt(0));
            }
        }

       
        

        protected void cvEmail_ServerValidate(object source, ServerValidateEventArgs args)
        {
            try
            {
                using (cUyeIslemleri UI = new cUyeIslemleri())
                {
                    DataTable dt = UI.EmailKontrol(txtEmail.Text);
                    if (dt.Rows.Count > 0)
                        args.IsValid = false;
                    else
                        args.IsValid = true;
                }
            }
            catch (Exception ex)
            {
                UIAraclari.cLog.Write(ex, UIAraclari._iKullaniciID.ToInt(0));
            }
        }

        protected void kaydetbtn_Click(object sender, EventArgs e)
        {
             /* SqlConnection con = new SqlConnection("Data Source=127.0.0.1; Initial Catalog=ETICARET; Integrated Security=True");
                con.Open();
                SqlCommand cmd = new SqlCommand("Insert Into TBLUYELER(ADI,SOYADI,EMAIL,SIFRE,DOGUM_TARIHI,CINSIYET,ADRES,SEHIR_ID,CEPTELEFONU,EVTELEFONU,KULLANICI_TIPI) values ('" + txtAd.Text + "','" + txtSoyad.Text + "','" + txtEmail.Text + "','" + txtSifre.Text + "','" + txtDogumTar.Text.ToDateTime() + "','" + (rblCinsiyet.SelectedValue.ToShort(0) == (short)eCinsiyet.Erkek ? eCinsiyet.Erkek : eCinsiyet.Bayan) + "','" + txtAdres.Text + "','" + ddlIl.SelectedValue.ToInt(0) + "','" + txtCepTel.Text + "','" + txtEvTel.Text + "','" + eKullaniciTipi.Uye + "')", con);
                cmd.ExecuteNonQuery();

                con.Close();
                con.Dispose();*/
            try
            {
                if (Page.IsValid)
                {
                    TBLUYELER tUye = new TBLUYELER();
                    tUye.ADI = txtAd.Text;
                    tUye.ADRES = txtAdres.Text;
                    tUye.CEPTELEFONU = txtCepTel.Text;
                    tUye.CINSIYET = rblCinsiyet.SelectedValue.ToShort(0) == (short)eCinsiyet.Erkek ? eCinsiyet.Erkek : eCinsiyet.Bayan;
                    tUye.DOGUM_TARIHI = txtDogumTar.Text.ToDateTime();
                    tUye.EMAIL = txtEmail.Text;
                    tUye.EVTELEFONU = txtEvTel.Text;
                    tUye.KULLANICI_TIPI = eKullaniciTipi.Uye;
                    tUye.SEHIR_ID = ddlIl.SelectedValue.ToInt(0);
                    tUye.SIFRE = txtSifre.Text;
                    tUye.SOYADI = txtSoyad.Text;

                    using (cUyeIslemleri cUyeIs = new cUyeIslemleri())
                    {
                        if (cUyeIs.EkleKullanici(tUye) > 0)
                            UIAraclari.toastMesaj(this, eStatusType.Onay, "İşleminiz Başarıyla Gerçekleşti. Giriş Yapabilirsiniz");
                        else
                            UIAraclari.toastMesaj(this, eStatusType.Hata, "İşleminiz Gerçekleştirilirken Hata oluştu. Lütfen Sistem Yöneticisine Başvurun.");
                    }
                }
            }
            catch (Exception ex)
            {
                UIAraclari.cLog.Write(ex, UIAraclari._iKullaniciID.ToInt(0));
            }


























      

        }
    }
}