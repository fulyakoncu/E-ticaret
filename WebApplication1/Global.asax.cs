using ETicaret;
using LogSistemi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Security;


namespace WebApplication1
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            UIAraclari.cLog = new cLogging(false);
 
        }

        void Application_End(object sender, EventArgs e)
        {
            UIAraclari.cLog.Dispose();

        }

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs

        }
    }
}
