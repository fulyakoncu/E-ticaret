using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ETicaretIslemleri;
using MiniCore;

namespace ETicaret
{
    public partial class KategoriDetay : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    using (cUrunGenel UG = new cUrunGenel())
                    {
                        DataTable dt = UG.ListeleUrun(0, 0, Request["kategoriId"].ToShort(0), null, null, null, null, string.Empty, string.Empty,true);
                        lstKategori.DataSource = dt;
                        lstKategori.DataBind();
                    }
                }
                catch (Exception ex)
                {
                    UIAraclari.cLog.Write(ex, UIAraclari._iKullaniciID);
                }
            }
        }
    }
}