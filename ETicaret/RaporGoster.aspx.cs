using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MiniCore;
using ETicaretIslemleri;
using System.Data;
using CrystalDecisions.CrystalReports.Engine;

namespace ETicaret
{
    public partial class RaporGoster : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                using (cRaporlama rapor = new cRaporlama())
                {
                    DataTable dtRapor = rapor.ListeleRaporlar(0, Request["KODU"].ToString());
                    
                    DataSet ds = (DataSet)Session[Request["SNAME"]];
                    if (dtRapor.Rows.Count > 0)
                    {
                        
                        ReportDocument rpr = new ReportDocument();
                        rpr.Load(Server.MapPath("\\Raporlar\\" + dtRapor.Rows[0]["YOL"].ToString()));
                        rpr.SetDataSource(ds.Tables[0]);
                        CrystalReportViewer1.DisplayToolbar = true;
                        CrystalReportViewer1.HasToggleGroupTreeButton = false;
                        CrystalReportViewer1.ReportSource = rpr;
                        CrystalReportViewer1.RefreshReport();
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