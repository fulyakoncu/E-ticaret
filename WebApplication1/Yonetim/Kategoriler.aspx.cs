using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ETicaretIslemleri;
using MiniCore;
namespace ETicaret.Yonetim
{
    public partial class Kategoriler : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (UIAraclari._iKullaniciTipi != eKullaniciTipi.Yonetici)
                    Response.Redirect(ResolveUrl("~/Default.aspx"));
                if (!Page.IsPostBack)
                {
                    lblID.Text = "0";
                    hfID.Value = "0";
                    TreeDoldur(0, tvKategoriler.Nodes);
                    tvKategoriler.ExpandAll();
                    using (cUrunGenel cUG = new cUrunGenel())
                    {
                        ddlKategoriler.DataSource = cUG.ListeleKategori(0, -1, null);
                        ddlKategoriler.DataTextField = "ADI";
                        ddlKategoriler.DataValueField = "ID";
                        ddlKategoriler.DataBind();
                        ddlKategoriler.Items.Insert(0, new ListItem("Ana Menü", "0"));
                    }
                    //tvKategoriler.Attributes.Add("OnClick", "OnTreeClick(event)");
                }
            }
            catch (Exception ex)
            {
                UIAraclari.cLog.Write(ex, UIAraclari._iKullaniciID); 
            }
        }

        private void TreeDoldur(short psID, TreeNodeCollection tn)
        {
            try
            {
                using (cUrunGenel UG = new cUrunGenel())
                    foreach (DataRow dr in UG.ListeleKategori(0, psID, null).Rows)
                    {
                        TreeNode sub = new TreeNode(dr["ADI"].ToString(), dr["ID"].ToString());            
                        
                        tn.Add(sub);
                        TreeDoldur(sub.Value.ToShort(0), sub.ChildNodes);
                    }
            }
            catch (Exception ex)
            {
                UIAraclari.cLog.Write(ex, UIAraclari._iKullaniciID);  
            }
        }
        
        [System.Web.Services.WebMethod()]
        public static string GetirKategori(short psKID)
        {
            try
            {
                string strJson = string.Empty;
                using (cUrunGenel cUG = new cUrunGenel())
                {
                    strJson = MiniCore.cAraclar.GetDataTableToJSon(cUG.ListeleKategori(psKID,-1, null),null);
                }
                return strJson;
            }
            catch (Exception ex)
            {
                UIAraclari.cLog.Write(ex, UIAraclari._iKullaniciID);
                return "";
            }
        }

        protected void btnTemizle_Click(object sender, EventArgs e)
        {
            try
            {
                lblID.Text = "0";
                hfID.Value = "0";
                txtAdi.Text = string.Empty;
                txtSirasi.Text = string.Empty;
                chcAktif.Checked = false;

                chcAktif.Text = chcAktif.Checked ? cAraclar.GetDescription(eAktifDurum.Aktif) : cAraclar.GetDescription(eAktifDurum.Pasif);
                using (cUrunGenel cUG = new cUrunGenel())
                {
                    ddlKategoriler.Items.Clear();

                    ddlKategoriler.DataSource = cUG.ListeleKategori(0, -1, null);
                    ddlKategoriler.DataTextField = "ADI";
                    ddlKategoriler.DataValueField = "ID";
                    ddlKategoriler.DataBind();
                    ddlKategoriler.Items.Insert(0, new ListItem("Ana Menü", "0"));
                }                
                ddlKategoriler.SelectedValue = "0";
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
                TBLKATEGORILER kategori = new TBLKATEGORILER();
                kategori.ADI = txtAdi.Text;
                kategori.AKTIF = chcAktif.Checked == true ? eAktifDurum.Aktif : eAktifDurum.Pasif;
                //kategori.KATEGORI_RESMI
                kategori.NODEID = ddlKategoriler.SelectedValue.ToShort(0);
                kategori.SIRANO = txtSirasi.Text.ToShort(0);
                short sonuc = 0;
                using (cUrunGenel UG = new cUrunGenel())
                {
                    if (hfID.Value.ToInt(0) == 0)
                    {
                        sonuc=UG.EkleKategoriler(kategori);
                    }
                    else
                    {
                        kategori.ID = hfID.Value.ToShort(0);
                        sonuc=UG.DuzenleKategori(kategori);
                    }
                }
                tvKategoriler.Nodes.Clear();
                TreeDoldur(0, tvKategoriler.Nodes);
                tvKategoriler.ExpandAll();
                btnTemizle_Click(null, null);
                if (sonuc > 0)
                    UIAraclari.toastMesaj(this, eStatusType.Onay, "İşleminiz Başarıyla Gerçekleşti");
                else
                    UIAraclari.toastMesaj(this, eStatusType.Hata, "İşleminiz Gerçekleştirilemedi");
            }
            catch (Exception ex)
            {
                UIAraclari.cLog.Write(ex, UIAraclari._iKullaniciID);
            }
        }
    }
}