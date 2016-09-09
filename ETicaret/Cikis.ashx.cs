using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MiniCore;
namespace ETicaret
{
    /// <summary>
    /// Summary description for Cikis
    /// </summary>
    public class Cikis : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            cUIAraclari._iKullaniciID = 0;
            cUIAraclari._dtSepet.Rows.Clear();
            cUIAraclari._iKullaniciTipi = eKullaniciTipi.Misafir;
            context.Response.Redirect("Default.aspx");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}