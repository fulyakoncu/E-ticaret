using System;
using System.Data;
using System.Data.Common;
using System.Web;
using Microsoft.Practices.EnterpriseLibrary.Data;
using MiniCore;
using LogSistemi;


namespace ETicaretIslemleri
{
    public class cUyeIslemleri : IDisposable
    {
        private Database db;
        private cLogging cLog;
        private int _iKullaniciID = HttpContext.Current.Session["USERID"].ToInt(0);
        public cUyeIslemleri()
        {
            cLog = new cLogging(false);
            db = DatabaseFactory.CreateDatabase();
        }
        public void Dispose()
        {
            cLog.Dispose();
            GC.SuppressFinalize(this);
        }

        #region Uye Ekleme-Düzenleme-Listeleme
        public int EkleKullanici(TBLUYELER ptUyeler)
        {
            try
            {
                DbCommand dbcommand = db.GetSqlStringCommand("INSERT INTO TBLUYELER(ADI,SOYADI,EMAIL,SIFRE,DOGUM_TARIHI,CINSIYET,ADRES,SEHIR_ID,CEPTELEFONU,EVTELEFONU,KULLANICI_TIPI,GUID,OLUSMA_ZAMANI) VALUES(@ADI,@SOYADI,@EMAIL,@SIFRE,@DOGUM_TARIHI,@CINSIYET,@ADRES,@SEHIR_ID,@CEPTELEFONU,@EVTELEFONU,@KULLANICI_TIPI, @GUID,@OLUSMA_ZAMANI) SELECT @@IDENTITY");
                db.AddInParameter(dbcommand, "ADI", DbType.String, ptUyeler.ADI);
                db.AddInParameter(dbcommand, "SOYADI", DbType.String, ptUyeler.SOYADI);
                db.AddInParameter(dbcommand, "EMAIL", DbType.String, ptUyeler.EMAIL);
                db.AddInParameter(dbcommand, "SIFRE", DbType.String, ptUyeler.SIFRE.ToMD5());
                db.AddInParameter(dbcommand, "DOGUM_TARIHI", DbType.DateTime, ptUyeler.DOGUM_TARIHI);
                db.AddInParameter(dbcommand, "CINSIYET", DbType.Int16, (short)ptUyeler.CINSIYET);
                db.AddInParameter(dbcommand, "ADRES", DbType.String, ptUyeler.ADRES);
                db.AddInParameter(dbcommand, "SEHIR_ID", DbType.Int32, ptUyeler.SEHIR_ID);
                db.AddInParameter(dbcommand, "CEPTELEFONU", DbType.String, ptUyeler.CEPTELEFONU);
                db.AddInParameter(dbcommand, "EVTELEFONU", DbType.String, ptUyeler.EVTELEFONU);
                db.AddInParameter(dbcommand, "KULLANICI_TIPI", DbType.Int16, (short)ptUyeler.KULLANICI_TIPI);
                db.AddInParameter(dbcommand, "GUID", DbType.String, Guid.NewGuid().ToString());
                db.AddInParameter(dbcommand, "OLUSMA_ZAMANI", DbType.DateTime, DateTime.Now);
                return db.ExecuteScalar(dbcommand).ToInt(0);
            }
            catch (Exception ex)
            {
                cLog.Write(ex, _iKullaniciID);
                return 0;
            }
        }

        public short DuzenleKullanici(TBLUYELER ptUyeler)
        {
            try
            {
                DbCommand dbcommand = db.GetSqlStringCommand("UPDATE TBLUYELER SET ADI=@ADI,SOYADI=@SOYADI,EMAIL=@EMAIL,DOGUM_TARIHI=@DOGUM_TARIHI,CINSIYET=@CINSIYET,ADRES=@ADRES,SEHIR_ID=@SEHIR_ID,CEPTELEFONU=@CEPTELEFONU,EVTELEFONU=@EVTELEFONU,GUID=@GUID WHERE ID=@ID");
                db.AddInParameter(dbcommand, "ID", DbType.Int32, ptUyeler.ID);
                db.AddInParameter(dbcommand, "ADI", DbType.String, ptUyeler.ADI);
                db.AddInParameter(dbcommand, "SOYADI", DbType.String, ptUyeler.SOYADI);
                db.AddInParameter(dbcommand, "EMAIL", DbType.String, ptUyeler.EMAIL);
                //db.AddInParameter(dbcommand, "SIFRE", DbType.String, ptUyeler.SIFRE.ToMD5());
                db.AddInParameter(dbcommand, "DOGUM_TARIHI", DbType.DateTime, ptUyeler.DOGUM_TARIHI);
                db.AddInParameter(dbcommand, "CINSIYET", DbType.Int16, (short)ptUyeler.CINSIYET);
                db.AddInParameter(dbcommand, "ADRES", DbType.String, ptUyeler.ADRES);
                db.AddInParameter(dbcommand, "SEHIR_ID", DbType.Int32, ptUyeler.SEHIR_ID);
                db.AddInParameter(dbcommand, "CEPTELEFONU", DbType.String, ptUyeler.CEPTELEFONU);
                db.AddInParameter(dbcommand, "EVTELEFONU", DbType.String, ptUyeler.EVTELEFONU);
                //db.AddInParameter(dbcommand, "KULLANICI_TIPI", DbType.Int16, (short)ptUyeler.KULLANICI_TIPI);
                db.AddInParameter(dbcommand, "GUID", DbType.String, Guid.NewGuid().ToString());
                db.ExecuteNonQuery(dbcommand);
                return 1;
            }
            catch (Exception ex)
            {
                cLog.Write(ex, _iKullaniciID);
                return 0;
            }
        }

        public DataTable ListeleKullanici(int piID, string pstrMail, eKullaniciTipi? peKullaniciTipi, string pstrGUID)
        {
            try
            {
                string strQuery = string.Empty;
                if (piID != 0)
                    strQuery = "ID=" + piID;
                else
                {
                    if (!peKullaniciTipi.IsNull())
                        strQuery = "KULLANICI_TIPI=" + (short)peKullaniciTipi;
                    if (!pstrMail.IsNull())
                        strQuery += (strQuery.IsNull() ? "" : " AND ") + "EMAIL=@EMAIL";
                    if (!pstrGUID.IsNull())
                        strQuery += (strQuery.IsNull() ? "" : " AND ") + "GUID=@GUID";
                }
                DbCommand dbcommand = db.GetSqlStringCommand("SELECT *,(SELECT S.ADI FROM TBLSEHIR S WHERE S.ID=U.SEHIR_ID )SEHIRADI FROM TBLUYELER U" + (strQuery.IsNull() ? "" : " WHERE " + strQuery));
                if (!pstrMail.IsNull())
                    db.AddInParameter(dbcommand, "EMAIL", DbType.String, pstrMail);
                if (!pstrGUID.IsNull())
                    db.AddInParameter(dbcommand, "GUID", DbType.String, pstrGUID);
                return db.ExecuteDataSet(dbcommand).Tables[0];
            }
            catch (Exception ex)
            {
                cLog.Write(ex, _iKullaniciID);
                return null;
            }
        }
        public DataTable EmailKontrol(string pstrEmail)
        {
            try
            {
                DbCommand dbcommand = db.GetSqlStringCommand("SELECT * FROM TBLUYELER U WHERE EMAIL=@EMAIL AND KULLANICI_TIPI !=@KULLANICI_TIPI");
                db.AddInParameter(dbcommand, "EMAIL", DbType.String, pstrEmail);
                db.AddInParameter(dbcommand, "KULLANICI_TIPI", DbType.Int16, (short)eKullaniciTipi.Misafir);
                return db.ExecuteDataSet(dbcommand).Tables[0];
            }
            catch (Exception ex)
            {
                cLog.Write(ex, _iKullaniciID);
                return null;
            }
        }
        public short DegistirKullaniciTipi(int piID, eKullaniciTipi peKullaniciTipi)
        {
            try
            {
                DbCommand dbCommand = db.GetSqlStringCommand("UPDATE TBLUYELER SET KULLANICI_TIPI=@KULLANICI_TIPI WHERE ID=@ID");
                db.AddInParameter(dbCommand, "ID", DbType.Int32, piID);
                db.AddInParameter(dbCommand, "KULLANICI_TIPI", DbType.Int16, (short)peKullaniciTipi);
                db.ExecuteNonQuery(dbCommand);
                return 1;
            }
            catch (Exception ex)
            {
                cLog.Write(ex, _iKullaniciID);
                return 0;
            }
        }

        public short DeğistirGUID(int piID)
        {
            try
            {
                DbCommand dbCommand = db.GetSqlStringCommand("UPDATE TBLUYELER SET GUID=@GUID WHERE ID=@ID");
                db.AddInParameter(dbCommand, "ID", DbType.Int32, piID);
                db.AddInParameter(dbCommand, "GUID", DbType.String, Guid.NewGuid().ToString());
                db.ExecuteNonQuery(dbCommand);
                return 1;
            }
            catch (Exception ex)
            {
                cLog.Write(ex, _iKullaniciID);
                return 0;
            }
        }

        public short DeğistirSifre(int piID, string pstrYeniSifre)
        {
            try
            {
                DbCommand dbCommand = db.GetSqlStringCommand("UPDATE TBLUYELER SET SIFRE=@SIFRE WHERE ID=@ID");
                db.AddInParameter(dbCommand, "ID", DbType.Int32, piID);
                db.AddInParameter(dbCommand, "SIFRE", DbType.String, pstrYeniSifre.ToMD5());
                db.ExecuteNonQuery(dbCommand);
                return 1;
            }
            catch (Exception ex)
            {
                cLog.Write(ex, _iKullaniciID);
                return 0;
            }
        }

        #endregion

        #region Uye Urun Listesi Metotları
        public long EkleUyeUrunListesi(TBLUYE_URUNLISTESI ptUyeUrunListesi)
        {
            try
            {
                DbCommand dbCommand = db.GetSqlStringCommand("INSERT INTO TBLUYE_URUNLISTESI(UYEID,URUNID) VALUES(@UYEID,@URUNID) SELECT @@IDENTITY");
                db.AddInParameter(dbCommand, "UYEID", DbType.Int32, ptUyeUrunListesi.UYEID);
                db.AddInParameter(dbCommand, "URUNID", DbType.Int32, ptUyeUrunListesi.URUNID);
                return db.ExecuteScalar(dbCommand).ToLong(0);
            }
            catch (Exception ex)
            {
                cLog.Write(ex, _iKullaniciID);
                return 0;
            }
        }
        public short SilUyeUrunListesi(long plID)
        {
            try
            {
                db.ExecuteNonQuery(CommandType.Text, "DELETE FROM TBLUYE_URUNLISTESI WHERE ID=0" + plID);
                return 1;
            }
            catch (Exception ex)
            {
                cLog.Write(ex, _iKullaniciID);
                return 0;
            }
        }
        public DataTable ListeleUyeUrunListesi(long plID, int piUyeID,int piurunID)
        {
            try
            {
                string strQuery = String.Empty;
                if (plID > 0)
                    strQuery = "ID=" + plID;
                if (piUyeID > 0)
                    strQuery = strQuery+(strQuery.IsNull()?"":" AND ")+ "UYEID=" + piUyeID;
                if(piurunID>0)
                    strQuery = strQuery + (strQuery.IsNull() ? "" : " AND ") + "URUNID=" + piurunID;
                return db.ExecuteDataSet(CommandType.Text, "SELECT *,(SELECT U.MODEL FROM TBLURUNLER U WHERE U.ID=L.URUNID)URUN_ADI,(SELECT M.ADI FROM TBLMARKALAR M WHERE M.ID=(SELECT U.MARKA FROM TBLURUNLER U WHERE U.ID=L.URUNID))MARKA_ADI FROM TBLUYE_URUNLISTESI L" + (strQuery == String.Empty ? "" : " WHERE " + strQuery)).Tables[0];
            }
            catch (Exception ex)
            {
                cLog.Write(ex, _iKullaniciID);
                return null;
            }
        }
        #endregion
        public DataRow GirisYap(string pstrEMail, string pstrSifre)
        {
            try
            {
                DbCommand dbCommand = db.GetSqlStringCommand("SELECT * FROM TBLUYELER WHERE KULLANICI_TIPI != @KULLANICI_TIPI AND EMAIL=@EMAIL AND SIFRE=@SIFRE");
                db.AddInParameter(dbCommand, "KULLANICI_TIPI", DbType.Int16, (short)eKullaniciTipi.Misafir);
                db.AddInParameter(dbCommand, "EMAIL", DbType.String, pstrEMail);
                db.AddInParameter(dbCommand, "SIFRE", DbType.String, pstrSifre.ToMD5());
                DataTable dt = db.ExecuteDataSet(dbCommand).Tables[0];
                if (dt.Rows.Count == 1)
                    return dt.Rows[0];
                else
                    return null;
            }
            catch (Exception ex)
            {
                cLog.Write(ex, _iKullaniciID);
                return null;
            }
        }
    }
}
