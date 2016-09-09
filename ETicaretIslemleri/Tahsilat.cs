using System;
using System.Data;
using System.Data.Common;
using System.Web;
using Microsoft.Practices.EnterpriseLibrary.Data;
using MiniCore;
using LogSistemi;


namespace ETicaretIslemleri
{
    public class cTahsilat : IDisposable
    {
        private Database db;
        private cLogging cLog;
        private int _iKullaniciID = HttpContext.Current.Session["USERID"].ToInt(0);
        public cTahsilat()
        {
            cLog = new cLogging(false);
            db = DatabaseFactory.CreateDatabase();
        }

        public void Dispose()
        {
            cLog.Dispose();
            GC.SuppressFinalize(this);
        }

        #region Banka Tanımları
        public short EkleBanka(TBLBANKALAR ptBankalar)
        {
            try
            {
                DbCommand dbCommand = db.GetSqlStringCommand("INSERT INTO TBLBANKALAR(BANKA_ADI,KULLANICI_ADI,SIFRE,MAGAZA_NO,AKTIF,TAKSIT,HOST) VALUES(@BANKA_ADI,@KULLANICI_ADI,@SIFRE,@MAGAZA_NO,@AKTIF,@TAKSIT,@HOST) SELECT @@IDENTITY");
                db.AddInParameter(dbCommand, "BANKA_ADI", DbType.String, ptBankalar.BANKA_ADI);
                db.AddInParameter(dbCommand, "KULLANICI_ADI", DbType.String, ptBankalar.KULLANICI_ADI);
                db.AddInParameter(dbCommand, "SIFRE", DbType.String, ptBankalar.SIFRE);
                db.AddInParameter(dbCommand, "MAGAZA_NO", DbType.String, ptBankalar.MAGAZA_NO);
                db.AddInParameter(dbCommand, "AKTIF", DbType.Int16, (short)ptBankalar.AKTIF);
                db.AddInParameter(dbCommand, "TAKSIT", DbType.Int16, (short)ptBankalar.TAKSIT);
                db.AddInParameter(dbCommand, "HOST", DbType.String, ptBankalar.HOST);

                return db.ExecuteScalar(dbCommand).ToShort(0);
            }
            catch (Exception ex)
            {
                cLog.Write(ex, _iKullaniciID);
                return 0;
            }
        }
        public short DuzenleBanka(TBLBANKALAR ptBankalar)
        {
            try
            {
                DbCommand dbCommand = db.GetSqlStringCommand("UPDATE TBLBANKALAR SET BANKA_ADI=@BANKA_ADI, KULLANICI_ADI=@KULLANICI_ADI, SIFRE=@SIFRE, MAGAZA_NO=@MAGAZA_NO, AKTIF=@AKTIF, TAKSIT=@TAKSIT,HOST=@HOST WHERE ID=@ID");
                db.AddInParameter(dbCommand, "ID", DbType.Int16, ptBankalar.ID);
                db.AddInParameter(dbCommand, "BANKA_ADI", DbType.String, ptBankalar.BANKA_ADI);
                db.AddInParameter(dbCommand, "KULLANICI_ADI", DbType.String, ptBankalar.KULLANICI_ADI);
                db.AddInParameter(dbCommand, "SIFRE", DbType.String, ptBankalar.SIFRE);
                db.AddInParameter(dbCommand, "MAGAZA_NO", DbType.String, ptBankalar.MAGAZA_NO);
                db.AddInParameter(dbCommand, "AKTIF", DbType.Int16, (short)ptBankalar.AKTIF);
                db.AddInParameter(dbCommand, "TAKSIT", DbType.Int16, (short)ptBankalar.TAKSIT);
                db.AddInParameter(dbCommand, "HOST", DbType.String, ptBankalar.HOST);
                db.ExecuteNonQuery(dbCommand);
                return 1;
            }
            catch (Exception ex)
            {
                cLog.Write(ex, _iKullaniciID);
                return 0;
            }
        }
        public DataTable ListeleBanka(int piID, eAktifDurum? peAktif)
        {
            try
            {
                string strQuery = string.Empty;
                if (piID > 0)
                    strQuery = "ID=" + piID;
                if (!peAktif.IsNull())
                    strQuery = "AKTIF=" + (short)peAktif;

                return db.ExecuteDataSet(CommandType.Text, "SELECT * FROM TBLBANKALAR" + (strQuery == string.Empty ? "" : " WHERE " + strQuery)).Tables[0];

            }
            catch (Exception ex)
            {
                cLog.Write(ex, _iKullaniciID);
                return null;
            }
        }
        public short SilBanka(short psID)
        {
            try
            {
                DbCommand dbcommand = db.GetSqlStringCommand("DELETE FROM TBLBANKALAR WHERE ID=@ID");
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
        #endregion

        #region Taksit Tanımları
        public short EkleTaksit(TBLTAKSITLER ptTaksitler)
        {
            try
            {
                DbCommand dbCommand = db.GetSqlStringCommand("INSERT INTO TBLTAKSITLER(BANKA_ID,TAKSIT,ORAN) VALUES(@BANKA_ID,@TAKSIT,@ORAN) SELECT @@IDENTITY");
                db.AddInParameter(dbCommand, "BANKA_ID", DbType.Int16, ptTaksitler.BANKA_ID);
                db.AddInParameter(dbCommand, "TAKSIT", DbType.Int16, ptTaksitler.TAKSIT);
                db.AddInParameter(dbCommand, "ORAN", DbType.Decimal, ptTaksitler.ORAN);

                return db.ExecuteScalar(dbCommand).ToShort(0);
            }
            catch (Exception ex)
            {
                cLog.Write(ex, _iKullaniciID);
                return 0;
            }
        }
        public short DuzenleTaksit(TBLTAKSITLER ptTaksitler)
        {
            try
            {
                DbCommand dbCommand = db.GetSqlStringCommand("UPDATE TBLTAKSITLER SET BANKA_ID=@BANKA_ID, TAKSIT=@TAKSIT, ORAN=@ORAN WHERE ID=@ID");
                db.AddInParameter(dbCommand, "ID", DbType.Int16, ptTaksitler.ID);
                db.AddInParameter(dbCommand, "BANKA_ID", DbType.Int16, ptTaksitler.BANKA_ID);
                db.AddInParameter(dbCommand, "TAKSIT", DbType.Int16, ptTaksitler.TAKSIT);
                db.AddInParameter(dbCommand, "ORAN", DbType.Decimal, ptTaksitler.ORAN);
                db.ExecuteNonQuery(dbCommand);
                return 1;
            }
            catch (Exception ex)
            {
                cLog.Write(ex, _iKullaniciID);
                return 0;
            }
        }
        public DataTable ListeleTaksit(int piID,int piBankaID)
        {
            try
            {
                string strSorgu = string.Empty;
                if (piID > 0)
                    strSorgu = " WHERE ID=" + piID;
                if (piBankaID > 0)
                    strSorgu = " WHERE BANKA_ID=" + piBankaID;
                return db.ExecuteDataSet(CommandType.Text, "SELECT * FROM TBLTAKSITLER" + strSorgu + " ORDER BY TAKSIT ASC").Tables[0];
            }
            catch (Exception ex)
            {
                cLog.Write(ex, _iKullaniciID);
                return null;
            }
        }
        public short SilTaksit(short psID)
        {
            try
            {
                DbCommand dbcommand = db.GetSqlStringCommand("DELETE FROM TBLTAKSITLER WHERE ID=@ID");
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
        #endregion
    }
}
