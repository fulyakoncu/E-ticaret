using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Web;
using MiniCore;
using System.Data.Common;
using System.Data;
using LogSistemi;

namespace ETicaretIslemleri
{
    public class cRaporlama:IDisposable
    {
        private Database db;
        private cLogging cLog;
        private int _iKullaniciID = HttpContext.Current.Session["USERID"].ToInt(0);
        public cRaporlama()
        {
            cLog = new cLogging(false);
            db = DatabaseFactory.CreateDatabase();
        }
        public void Dispose()
        {
            cLog.Dispose();
            GC.SuppressFinalize(this);
        }

        #region Rapor Ekle,Düzenle,Sil
        public int RaporEkle(TBLRAPOR ptRapor)
        {
            try
            {
                DbCommand dbcommand = db.GetSqlStringCommand("INSERT INTO TBLRAPOR(AD,YOL) VALUES(@AD,@YOL) SELECT @@IDENTITY");
                db.AddInParameter(dbcommand, "AD", DbType.String, ptRapor.AD);
                db.AddInParameter(dbcommand, "YOL", DbType.String, ptRapor.YOL);
                return db.ExecuteScalar(dbcommand).ToInt(0);
            }
            catch (Exception ex)
            {
                cLog.Write(ex, _iKullaniciID);
                return 0;
            }
        }

        public short DuzenleRaporlar(TBLRAPOR ptRapor)
        {
            try
            {
                DbCommand dbCommand = db.GetSqlStringCommand("UPDATE TBLRAPOR SET AD=@AD, YOL=@YOL WHERE ID=@ID");
                db.AddInParameter(dbCommand, "AD", DbType.String, ptRapor.AD);
                db.AddInParameter(dbCommand, "YOL", DbType.String, ptRapor.YOL);
                db.AddInParameter(dbCommand, "ID", DbType.Int32, ptRapor.ID);
                db.ExecuteNonQuery(dbCommand);
                return 1;
            }
            catch (Exception ex)
            {
                cLog.Write(ex, _iKullaniciID);
                return 0;
            }
        }

        public DataTable ListeleRaporlar(int piID, string pstrKODU)
        {
            try
            {
                string strQuery = string.Empty;
                if (piID > 0)
                    strQuery = "ID=" + piID.ToInt(0);
                else
                {
                    if (!pstrKODU.IsNull())
                        strQuery = "AD=@AD";
                }
                DbCommand dbcommand= db.GetSqlStringCommand("SELECT * FROM TBLRAPOR" + (strQuery.IsNull() ? "" : " WHERE " + strQuery));
                if (!pstrKODU.IsNull())
                    db.AddInParameter(dbcommand, "AD", DbType.String, pstrKODU);
                return db.ExecuteDataSet(dbcommand).Tables[0];
            }
            catch (Exception ex)
            {
                cLog.Write(ex, _iKullaniciID);
                return null;
            } 
        }

        public short SilRaporlar(short psID)
        {
            try
            {
                DbCommand dbcommand = db.GetSqlStringCommand("DELETE FROM TBLRAPOR WHERE ID=@ID");
                db.AddInParameter(dbcommand, "ID", DbType.Int16, psID);
                db.ExecuteNonQuery(dbcommand);
                return 1;
            }
            catch (Exception ex)
            {
                cLog.Write(ex, _iKullaniciID);
                return 0;
            }
        }

        public DataTable RaporStokListesi()
        {
            try
            {
                return db.ExecuteDataSet(CommandType.Text, "SELECT URUNID,(SELECT U.MODEL FROM TBLURUNLER U WHERE U.ID=S.URUNID )URUN_ADI,(SELECT M.ADI FROM TBLMARKALAR M WHERE M.ID=(SELECT U.MARKA FROM TBLURUNLER U WHERE U.ID=S.URUNID))MARKA_ADI,(SELECT ISNULL(SUM(MIKTAR),0) FROM TBLSTOKISLEMLERI X WHERE ISLEMTIPI=" + (short)eStokIslemTipi.Giris + " AND X.URUNID=S.URUNID)GIRIS_MIKTARI,(SELECT ISNULL(SUM(MIKTAR),0) FROM TBLSTOKISLEMLERI X WHERE ISLEMTIPI=" + (short)eStokIslemTipi.Cikis + " AND X.URUNID=S.URUNID)CIKIS_MIKTARI, (SELECT ISNULL(SUM(MIKTAR),0) FROM TBLSTOKISLEMLERI X WHERE ISLEMTIPI=" + (short)eStokIslemTipi.Giris + " AND X.URUNID=S.URUNID)-(select ISNULL(SUM(MIKTAR),0) FROM TBLSTOKISLEMLERI X WHERE ISLEMTIPI=" + (short)eStokIslemTipi.Cikis + " AND X.URUNID=S.URUNID)STOKMIKTARI FROM TBLSTOKISLEMLERI S GROUP BY URUNID").Tables[0];
            }
            catch (Exception ex)
            {
                cLog.Write(ex, _iKullaniciID);
                return null;
            }
        }
        #endregion
    }
}
