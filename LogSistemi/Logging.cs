using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;

namespace LogSistemi
{
    public class cLogging:IDisposable
    {
        public TBLLOGS Logs;
        public cLogging(bool webService)
        {
            Logs = new TBLLOGS();
            Logs.ID = 0;
            if (webService)
            {
                Logs.IP = string.Empty;
                Logs.IsWeb = true;
            }
            else
            {
                Logs.IP = "127.0.0.1";
                Logs.IsWeb = false;
            }
            Logs.KAYNAGI = string.Empty;
            Logs.KULLANICI_ID = 0;
            Logs.MESAJ = String.Empty;
            Logs.PAGE = string.Empty;
            //Logs.QUERY = string.Empty;
            Logs.STACKTRACE = string.Empty;
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        public void Write(Exception pex,int piKullaniciID)
        {
            Logs.MESAJ = pex.Message;
            Logs.KAYNAGI = pex.Source;
            Logs.STACKTRACE = pex.StackTrace;
            Logs.KULLANICI_ID = piKullaniciID;
            EkleLog();
        }

        private void EkleLog()
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbcommand = db.GetSqlStringCommand("INSERT INTO TBLLOGS(KULLANICI_ID,MESAJ,IP,PAGE,STACKTRACE,KAYNAGI,OLUSMA_ZAMANI) VALUES(@KULLANICI_ID,@MESAJ,@IP,@PAGE,@STACKTRACE,@KAYNAGI,@OLUSMA_ZAMANI)");
            db.AddInParameter(dbcommand, "KULLANICI_ID", DbType.Int32, Logs.KULLANICI_ID);
            db.AddInParameter(dbcommand, "MESAJ", DbType.String, Logs.MESAJ);
            db.AddInParameter(dbcommand, "IP", DbType.String, Logs.IP);
            //db.AddInParameter(dbcommand, "QUERY", DbType.String, Logs.QUERY);
            if(!Logs.IsWeb)
                db.AddInParameter(dbcommand, "PAGE", DbType.String, HttpContext.Current.Request.RawUrl.Substring(0, HttpContext.Current.Request.RawUrl.Length > 150 ? 150 : HttpContext.Current.Request.RawUrl.Length));
            else
                db.AddInParameter(dbcommand, "PAGE", DbType.String, "Web Servis");
            db.AddInParameter(dbcommand, "STACKTRACE", DbType.String, Logs.STACKTRACE);
            db.AddInParameter(dbcommand, "KAYNAGI", DbType.String, Logs.KAYNAGI);
            db.AddInParameter(dbcommand, "OLUSMA_ZAMANI", DbType.DateTime,DateTime.Now);
            db.ExecuteNonQuery(dbcommand);
        }
    }
}
