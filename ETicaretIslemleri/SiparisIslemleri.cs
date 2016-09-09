using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Web;
using LogSistemi;
using MiniCore;
using System.Data.Common;
using System.Data;

namespace ETicaretIslemleri
{
    public class cSiparisIslemleri:IDisposable
    {
        private Database db;
        private cLogging cLog;
        private int _iKullaniciID = HttpContext.Current.Session["USERID"].ToInt(0);

        public cSiparisIslemleri()
        {
            cLog = new cLogging(false);
            db = DatabaseFactory.CreateDatabase();
        }

        public void Dispose()
        {
            cLog.Dispose();
            GC.SuppressFinalize(this);
        }
        #region TBLSIPARIS tablosunun metotları
        /// <summary>
        /// ilk başta kargo bilgisi ve değişme bilgisieklenmez
        /// </summary>
        /// <param name="ptSiparis"></param>
        /// <returns></returns>
        public long EkleSparis(TBLSIPARIS ptSiparis)
        {
            try
            {
                DbCommand dbCommand = db.GetSqlStringCommand("INSERT INTO TBLSIPARIS(UYEID,FATURA_ADI, FATURA_VERGINO,TUTAR,ADRES,BANKAID,SPOSSONUC,ODEMETIPI,SIPARISDURUMU,OLUSMA_ZAMANI,GUID) VALUES(@UYEID,@FATURA_ADI,@FATURA_VERGINO,@TUTAR,@ADRES,@BANKAID,@SPOSSONUC, @ODEMETIPI, @SIPARISDURUMU, @OLUSMA_ZAMANI,@GUID) SELECT @@IDENTITY");
                db.AddInParameter(dbCommand, "UYEID", DbType.Int32, ptSiparis.UYEID);
                db.AddInParameter(dbCommand, "FATURA_ADI", DbType.String, ptSiparis.FATURA_ADI);
                db.AddInParameter(dbCommand, "FATURA_VERGINO", DbType.String, ptSiparis.FATURA_VERGINO);
                db.AddInParameter(dbCommand, "TUTAR", DbType.Decimal, ptSiparis.TUTAR);
                db.AddInParameter(dbCommand, "ADRES", DbType.String, ptSiparis.ADRES);
                db.AddInParameter(dbCommand, "BANKAID", DbType.Int16, ptSiparis.BANKAID);
                db.AddInParameter(dbCommand, "SPOSSONUC", DbType.String, ptSiparis.SPOSSONUC);
                db.AddInParameter(dbCommand, "ODEMETIPI", DbType.Int16,(short) ptSiparis.ODEMETIPI);
                db.AddInParameter(dbCommand, "SIPARISDURUMU", DbType.Int16,(short) ptSiparis.SIPARISDURUMU);
                db.AddInParameter(dbCommand, "OLUSMA_ZAMANI", DbType.DateTime, DateTime.Now);
                db.AddInParameter(dbCommand, "GUID", DbType.String, Guid.NewGuid().ToString());
                return db.ExecuteScalar(dbCommand).ToLong(0);
            }
            catch (Exception ex)
            {
                cLog.Write(ex, _iKullaniciID);
                return 0;
            }
        }
        /// <summary>
        /// ÜYE ID, OLUSMA_ZAMANI,GUID GÜNCELLENMEZ
        /// </summary>
        /// <param name="ptSiparis"></param>
        /// <returns></returns>
        public short DuzenleSiparis(TBLSIPARIS ptSiparis)
        {
            try
            {
                DbCommand dbCommand = db.GetSqlStringCommand("UPDATE TBLSIPARIS SET FATURA_ADI=@FATURA_ADI, FATURA_VERGINO=@FATURA_VERGINO, ADRES=@ADRES, BANKAID=@BANKAID, SPOSSONUC=@SPOSSONUC,ODEMETIPI=@ODEMETIPI,KARGOID=@KARGOID,KARGOKODU=@KARGOKODU,SIPARISDURUMU=@SIPARISDURUMU, DEGISTIREN=@DEGISTIREN, DEGISME_ZAMANI=@DEGISME_ZAMANI WHERE ID=@ID"); 
                db.AddInParameter(dbCommand, "ID", DbType.Int64, ptSiparis.ID);       
                db.AddInParameter(dbCommand, "FATURA_ADI", DbType.String, ptSiparis.FATURA_ADI);
                db.AddInParameter(dbCommand, "FATURA_VERGINO", DbType.String, ptSiparis.FATURA_VERGINO);
                //db.AddInParameter(dbCommand, "TUTAR", DbType.Decimal, ptSiparis.TUTAR);
                db.AddInParameter(dbCommand, "ADRES", DbType.String, ptSiparis.ADRES);
                db.AddInParameter(dbCommand, "BANKAID", DbType.Int16, ptSiparis.BANKAID);
                db.AddInParameter(dbCommand, "KARGOID", DbType.Int16, ptSiparis.KARGOID);
                db.AddInParameter(dbCommand, "KARGOKODU", DbType.String, ptSiparis.KARGOKODU);
                db.AddInParameter(dbCommand, "SPOSSONUC", DbType.String,ptSiparis.SPOSSONUC);
                db.AddInParameter(dbCommand, "ODEMETIPI", DbType.Int16, (short)ptSiparis.ODEMETIPI);
                db.AddInParameter(dbCommand, "SIPARISDURUMU", DbType.Int16, (short)ptSiparis.SIPARISDURUMU);
                db.AddInParameter(dbCommand, "DEGISTIREN", DbType.Int32, ptSiparis.DEGISTIREN);
                db.AddInParameter(dbCommand, "DEGISME_ZAMANI", DbType.DateTime,DateTime.Now);
                db.ExecuteNonQuery(dbCommand);
                return 1;
            }
            catch (Exception ex)
            {
                cLog.Write(ex, _iKullaniciID);
                return 0;
            }
        }

        public DataTable ListeleSiparis(long plID, int piUyeID, eSiparisDurumu? peSiparisDurumu, string pstrGUID)
        {
            try
            {
                string strQuery = string.Empty;
                if (plID > 0)
                {
                    strQuery = "ID=" + plID;
                }
                else
                {
                    if (piUyeID > 0)
                        strQuery += (strQuery.IsNull() ? "" : " AND ") + "UYEID=" + piUyeID;
                    if (!peSiparisDurumu.IsNull())
                        strQuery += (strQuery.IsNull() ? "" : " AND ") + "SIPARISDURUMU=" + (short)peSiparisDurumu;
                    if(!pstrGUID.IsNull())
                        strQuery += (strQuery.IsNull() ? "" : " AND ") + "GUID=@GUID";
                }
                DbCommand dbCommand = db.GetSqlStringCommand("SELECT *,(SELECT U.ADI+' '+U.SOYADI FROM TBLUYELER U WHERE U.ID=S.UYEID)UYE_ADISOYADI,(SELECT B.BANKA_ADI FROM TBLBANKALAR B WHERE B.ID=S.BANKAID)BANKA_ADI,(SELECT K.ADI FROM TBLKARGOLAR K WHERE K.ID=S.KARGOID)KARGO_ADI  FROM TBLSIPARIS S" + (strQuery.IsNull() ? "" : " WHERE " + strQuery));
                if(!pstrGUID.IsNull())
                    db.AddInParameter(dbCommand, "GUID", DbType.String,pstrGUID);

                return db.ExecuteDataSet(dbCommand).Tables[0];
            }
            catch (Exception ex)
            {
                cLog.Write(ex, _iKullaniciID);
                return null;
            }
        }

        public DataTable ListeleSiparisler(bool pbBekleyenmi)
        {
            try
            {
                if (pbBekleyenmi)
                    return db.ExecuteDataSet(CommandType.Text, "SELECT *,(SELECT U.ADI+' '+U.SOYADI FROM TBLUYELER U WHERE U.ID=S.UYEID)UYE_ADISOYADI,(SELECT B.BANKA_ADI FROM TBLBANKALAR B WHERE B.ID=S.BANKAID)BANKA_ADI,(SELECT K.ADI FROM TBLKARGOLAR K WHERE K.ID=S.KARGOID)KARGO_ADI  FROM TBLSIPARIS S WHERE S.SIPARISDURUMU IN("+(short)eSiparisDurumu.Hazirlaniyor+","+(short)eSiparisDurumu.Kargoda+")").Tables[0];
                else
                    return db.ExecuteDataSet(CommandType.Text, "SELECT *,(SELECT U.ADI+' '+U.SOYADI FROM TBLUYELER U WHERE U.ID=S.UYEID)UYE_ADISOYADI,(SELECT B.BANKA_ADI FROM TBLBANKALAR B WHERE B.ID=S.BANKAID)BANKA_ADI,(SELECT K.ADI FROM TBLKARGOLAR K WHERE K.ID=S.KARGOID)KARGO_ADI  FROM TBLSIPARIS S WHERE S.SIPARISDURUMU IN(" + (short)eSiparisDurumu.SiparisOnaylanmadi + "," + (short)eSiparisDurumu.TeslimEdildi + ")").Tables[0];
            }
            catch (Exception ex)
            {
                cLog.Write(ex, _iKullaniciID);
                return null;
            }
        }
        #endregion

        #region TBLSIPARISDETAY tablosunun metotları
        
        public long EkleSiparisDetay(TBLSIPARISDETAY ptSDetay)
        {
            try
            {
                DbCommand dbCommand = db.GetSqlStringCommand("INSERT INTO TBLSIPARISDETAY(SIPARISID, URUNID,MIKTAR,TUTAR) VALUES(@SIPARISID,@URUNID,@MIKTAR,@TUTAR) SELECT @@IDENTITY");
                db.AddInParameter(dbCommand, "SIPARISID", DbType.Int64, ptSDetay.SIPARISID);
                db.AddInParameter(dbCommand, "URUNID", DbType.Int32, ptSDetay.URUNID);
                db.AddInParameter(dbCommand, "MIKTAR", DbType.Decimal, ptSDetay.MIKTAR);
                db.AddInParameter(dbCommand, "TUTAR", DbType.Decimal, ptSDetay.TUTAR);

                return db.ExecuteScalar(dbCommand).ToLong(0);
            }
            catch (Exception ex)
            {
                cLog.Write(ex, _iKullaniciID);
                return 0;
            }
        }

        public short DuzenleSiparisDetay(TBLSIPARISDETAY ptSDetay)
        {
            try
            {
                DbCommand dbCommand = db.GetSqlStringCommand("UPDATE TBLSIPARISDETAY SET SIPARISID=@SIPARISID,URUNID=@URUNID,MIKTAR=@MIKTAR,TUTAR=@TUTAR WHERE ID=@ID");
                db.AddInParameter(dbCommand, "ID", DbType.Int64, ptSDetay.ID);
                db.AddInParameter(dbCommand, "SIPARISID", DbType.Int64, ptSDetay.SIPARISID);
                db.AddInParameter(dbCommand, "URUNID", DbType.Int32, ptSDetay.URUNID);
                db.AddInParameter(dbCommand, "MIKTAR", DbType.Decimal, ptSDetay.MIKTAR);
                db.AddInParameter(dbCommand, "TUTAR", DbType.Decimal, ptSDetay.TUTAR);

                db.ExecuteNonQuery(dbCommand);
                return 1;
            }
            catch (Exception ex)
            {
                cLog.Write(ex, _iKullaniciID);
                return 0;
            } 
        }

        public DataTable ListeleSiparisDetay(long plID, long plSiparisID, int piUrunID)
        {
            try
            {
                string strQuery=String.Empty;
                if (plID > 0)
                    strQuery = "ID=" + plID;
                else
                {
                    if (plSiparisID > 0)
                        strQuery += (strQuery.IsNull() ? "" : " AND ") + "SIPARISID=" + plSiparisID;
                    if(piUrunID>0)
                        strQuery += (strQuery.IsNull() ? "" : " AND ") + "URUNID=" + piUrunID;
                }
                return db.ExecuteDataSet(CommandType.Text, "SELECT *,(SELECT U.MODEL FROM TBLURUNLER U WHERE U.ID=D.URUNID )URUN_ADI,(SELECT M.ADI FROM TBLMARKALAR M WHERE M.ID=(SELECT U.MARKA FROM TBLURUNLER U WHERE U.ID=D.URUNID))MARKA_ADI FROM TBLSIPARISDETAY D" + (strQuery.IsNull() ? "" : " WHERE " + strQuery)).Tables[0];
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
