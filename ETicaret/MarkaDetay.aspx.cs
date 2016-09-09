using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETicaretIslemleri;
using MiniCore;

namespace ETicaret
{
    public partial class ModelDetay : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    if (!Page.IsPostBack)
                    {
                        using (cUrunGenel UG = new cUrunGenel())
                        {
                            DataTable dt = UG.ListeleUrun(0, Request["markaID"].ToShort(-1), 0, null, null, null, null, string.Empty, string.Empty,false);
                            lstUrunler.DataSource = dt;
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
}