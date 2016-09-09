using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using LogSistemi;
using ETicaretIslemleri;
using MiniCore;
using System.ComponentModel;

namespace ETicaret
{
    public partial class UrunListele : System.Web.UI.UserControl
    {
        [Bindable(true)]
        [DefaultValue(0)]
        [Localizable(true)]
        public int UrunID
        {
            get { return ViewState["UrunID"] == null ? 0 : (int)ViewState["UrunID"]; }
            set { ViewState["UrunID"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    using (cUrunGenel UG = new cUrunGenel())
                    {
                        if (UG.VerKalanStok(this.UrunID) <= 0)
                        {
                            btnSepeteEkle.Visible = false;
                            txtAdet.Visible = false;
                        }

                            DataTable dtUrunResim = UG.ListeleUrunResim(0, this.UrunID, null, eEvetHayir.Evet);
                            if (dtUrunResim.Rows.Count > 0)
                                imgUrun.ImageUrl = ResolveUrl("~/Upload/UrunResimleri/" + this.UrunID + "/" + dtUrunResim.Rows[0]["RESIMADI"].ToString());
                            DataRow drUrun = UG.ListeleUrun(this.UrunID, 0, 0, null, null, null, null, string.Empty, string.Empty,false).Rows[0];
                            ltMarka.Text = "<a href=\"" + ResolveUrl("~/Markalar/" + drUrun["MARKA"]) + "_" + cAraclar.URLDuzelt(drUrun["MARKA_ADI"].ToString()) + ".aspx\">" + drUrun["MARKA_ADI"].ToString() + "</a>";
                            lblMarka.Text = drUrun["MARKA_ADI"].ToString();
                            lblModel.Text = drUrun["MODEL"].ToString();
                            lblFiyat.Text =((eEvetHayir)drUrun["INDIRIM"].ToShort()==eEvetHayir.Evet?drUrun["INDIRIMLI_FIYAT"].ToString():drUrun["FIYATI"].ToString())+" "+cAraclar.GetDescription((eParaBirimi)drUrun["PARABIRIMI"].ToShort());
                            ltIncele.Text = "<u><a href=\"" + ResolveUrl("~/Urunler/" + this.UrunID + "_" + cAraclar.URLDuzelt(drUrun["MARKA_ADI"].ToString())) + ".aspx\">Detaylar</a></u>";
                    }
                }

            }
            catch (Exception ex)
            {

                UIAraclari.cLog.Write(ex, UIAraclari._iKullaniciID);
            }
        }

        protected void btnSepeteEkle_Click(object sender, EventArgs e)
        {
            try
            {
                using (cUrunGenel UG = new cUrunGenel())
                {
                    if (UG.VerKalanStok(this.UrunID) < txtAdet.Text.ToInt())
                    {
                        UIAraclari.toastMesaj(this, eStatusType.Hata, "Stokta Yeterli Ürün Yok!");
                    }
                    else
                    {
                        DataRow dr = UIAraclari._dtSepet.NewRow();
                        if (UIAraclari._dtSepet.Rows.Count == 0)
                            dr["SIRANO"] = 1;
                        else
                            dr["SIRANO"] = UIAraclari._dtSepet.Rows[UIAraclari._dtSepet.Rows.Count - 1]["SIRANO"].ToInt(0) + 1;
                        dr["URUNID"] = this.UrunID;
                        dr["MIKTAR"] = txtAdet.Text.ToInt(1);
                        dr["URUNADI"] = lblModel.Text;
                        dr["MARKAADI"] = lblMarka.Text;
                        UIAraclari._dtSepet.Rows.Add(dr);
                        SepetDoldur();
                    }
                }
            }
            catch (Exception ex)
            {
                UIAraclari.cLog.Write(ex, UIAraclari._iKullaniciID);
            }
        }

        private void SepetDoldur()
        {
            GridView gvSepet = (GridView)this.Parent.Page.Master.FindControl("gvSepet");
            gvSepet.DataSource = UIAraclari._dtSepet;
            gvSepet.DataBind();
        }
    }
}