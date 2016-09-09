using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETicaretIslemleri;
using MiniCore;
using System.Data;
using System.Text;

namespace ETicaret
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    using (cUrunListeleri cUL = new cUrunListeleri())
                    {
                        int sayi = 0;
                        foreach (DataRow dr in cUL.ListeSliderUrunleri().Rows)
                        {
                            ulSlider.InnerHtml = ulSlider.InnerHtml + "<li><a href=\"" + ResolveUrl("~/Urunler/" + dr["ID"] + "_" +cAraclar.URLDuzelt(dr["MODEL"].ToString())) + ".aspx\"><img src=\"Upload/UrunResimleri/" + dr["ID"] + "/" + dr["RESIM_ADI"] + "\"></a></li>";
                            sayi++;
                            ulSliderNav.InnerHtml = ulSliderNav.InnerHtml + "<li><a href=\"#\">"+sayi+"</a></li>";
                        }
                        lstUrunler.DataSource = cUL.ListeGrupTipiUrunleri(eGrupTipi.YeniUrun, 4);
                        lstUrunler.DataBind();
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
