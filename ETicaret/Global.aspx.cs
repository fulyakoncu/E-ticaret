using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using LogSistemi;

namespace ETicaret
{
    public class Global : System.Web.HttpApplication
    {

        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
        }

        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown

        }

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs

        }

        void Session_Start(object sender, EventArgs e)
        {
            // Code that runs when a new session is started
            cUIAraclari.cLog = new cLogging(false);
            cUIAraclari._dtSepet=new System.Data.DataTable();
        }

        void Session_End(object sender, EventArgs e)
        {
            // Code that runs when a session ends. 
            // Note: The Session_End event is raised only when the sessionstate mode
            // is set to InProc in the Web.config file. If session mode is set to StateServer 
            // or SQLServer, the event is not raised.
            cUIAraclari.cLog.Dispose();
            if(cUIAraclari._dtSepet!=null)
                cUIAraclari._dtSepet.Dispose();
        }

        void Application_BeginRequest(Object sender, EventArgs e)
        {

            string DosyaYolu = Request.RawUrl;// Burada Dosyanın Yolu Alınır

            if (DosyaYolu.IndexOf("/Kategoriler/") != -1)
            {

                Context.RewritePath("~/KategoriDetay.aspx", "", "kategoriId=" + DosyaYolu.Split('/')[DosyaYolu.Split('/').Length - 1].Split('_')[0], true);
            }
            else if (DosyaYolu.IndexOf("/Markalar/") != -1)
            {
                Context.RewritePath("~/MarkaDetay.aspx", "", "markaID=" + DosyaYolu.Split('/')[DosyaYolu.Split('/').Length - 1].Split('_')[0], true);
            }
            else if (DosyaYolu.IndexOf("/Urunler/") != -1)
            {
                Context.RewritePath("~/UrunDetay.aspx", "", "ID=" + DosyaYolu.Split('/')[DosyaYolu.Split('/').Length - 1].Split('_')[0], true);
            }
            else
                return;

        }

    }
}
