using System;
using System.Data;
using System.Data.Common;
using System.Web;
using Microsoft.Practices.EnterpriseLibrary.Data;
using MiniCore;

using System.Net.Mail;
using LogSistemi;

namespace ETicaretIslemleri
{
    public class cGenelIslemler : IDisposable
    {
        private Database db;
        private cLogging cLog;
        private int _iKullaniciID = HttpContext.Current.Session["USERID"].ToInt(0);
        public cGenelIslemler()
        {
            cLog = new cLogging(false);
            db = DatabaseFactory.CreateDatabase();
        }

        public void Dispose()
        {
            cLog.Dispose();
            GC.SuppressFinalize(this);
        }

        #region Şehir İşlemleri
        /// <summary>
        /// Şehirler listelenir.
        /// </summary>
        /// <param name="piID">0 ise tümü listelenir şehrin ID si yzılırsa sadece o ID li şehir yazılır</param>
        /// <returns></returns>
        public DataTable ListeleSehirler(int piID)
        {
            try
            {
                string strSorgu = string.Empty;
                if (piID > 0)
                    strSorgu = " WHERE ID=" + piID;
                return db.ExecuteDataSet(CommandType.Text, "SELECT * FROM TBLSEHIR" + strSorgu + " ORDER BY KODU ASC").Tables[0];
            }
            catch (Exception ex)
            {
                cLog.Write(ex, _iKullaniciID);
                return null;
            }
        }
        #endregion

        #region Kargo Tanımları
        public short EkleKargo(TBLKARGOLAR ptKargolar)
        {
            try
            {
                DbCommand dbCommand = db.GetSqlStringCommand("INSERT INTO TBLKARGOLAR(ADI,TELEFON,EMAIL,AKTIF) VALUES(@ADI,@TELEFON,@EMAIL,@AKTIF) SELECT @@IDENTITY");
                db.AddInParameter(dbCommand, "ADI", DbType.String, ptKargolar.ADI);
                db.AddInParameter(dbCommand, "TELEFON", DbType.String, ptKargolar.TELEFON);
                db.AddInParameter(dbCommand, "EMAIL", DbType.String, ptKargolar.EMAIL);
                db.AddInParameter(dbCommand, "AKTIF", DbType.Int16, (short)ptKargolar.AKTIF);

                return db.ExecuteScalar(dbCommand).ToShort(0);
            }
            catch (Exception ex)
            {
                cLog.Write(ex, _iKullaniciID);
                return 0;
            }
        }
        public short DuzenleKargo(TBLKARGOLAR ptKargolar)
        {
            try
            {
                DbCommand dbCommand = db.GetSqlStringCommand("UPDATE TBLKARGOLAR SET ADI=@ADI, TELEFON=@TELEFON,EMAIL=@EMAIL,AKTIF=@AKTIF WHERE ID=@ID");
                db.AddInParameter(dbCommand, "ID", DbType.Int16, ptKargolar.ID);
                db.AddInParameter(dbCommand, "ADI", DbType.String, ptKargolar.ADI);
                db.AddInParameter(dbCommand, "TELEFON", DbType.String, ptKargolar.TELEFON);
                db.AddInParameter(dbCommand, "EMAIL", DbType.String, ptKargolar.EMAIL);
                db.AddInParameter(dbCommand, "AKTIF", DbType.Int16, (short)ptKargolar.AKTIF);
                db.ExecuteNonQuery(dbCommand);
                return 1;
            }
            catch (Exception ex)
            {
                cLog.Write(ex, _iKullaniciID);
                return 0;
            }
        }
        public DataTable ListeleKargo(int piID, eAktifDurum? peAktif)
        {
            try
            {
                string strQuery = string.Empty;
                if (piID > 0)
                    strQuery = "ID=" + piID;
                else if (!peAktif.IsNull())
                    strQuery = "AKTIF=" + (short)peAktif;

                return db.ExecuteDataSet(CommandType.Text, "SELECT * FROM TBLKARGOLAR" + (strQuery == string.Empty ? "" : " WHERE " + strQuery)).Tables[0];
            }
            catch (Exception ex)
            {
                cLog.Write(ex, _iKullaniciID);
                return null;
            }
        }
        public short SilKargo(short psID)
        {
            try
            {
                DbCommand dbcommand = db.GetSqlStringCommand("DELETE FROM TBLKARGOLAR WHERE ID=@ID");
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

        #region Kurum Tanımları
        public short EkleKurumParametreleri(TBLKURUM_PARAMETRELERI ptKurumParametreleri)
        {
            try
            {
                DbCommand dbCommand = db.GetSqlStringCommand("INSERT INTO TBLKURUM_PARAMETRELERI(KURUM_ADI, ADRES, TELEFON, FAX, VERGI_DAIRESI, VERGI_NO, TIC_SICIL_NO,SMTP_ADRES,SMTP_PORT,MAIL,MAILSIFRE,AKTIF) VALUES(@KURUM_ADI, @ADRES, @TELEFON, @FAX, @VERGI_DAIRESI, @VERGI_NO, @TIC_SICIL_NO, @SMTP_ADRES, @SMTP_PORT, @MAIL, @MAILSIFRE, @AKTIF) SELECT @@IDENTITY");

                db.AddInParameter(dbCommand, "KURUM_ADI", DbType.String, ptKurumParametreleri.KURUM_ADI);
                db.AddInParameter(dbCommand, "ADRES", DbType.String, ptKurumParametreleri.ADRES);
                db.AddInParameter(dbCommand, "TELEFON", DbType.String, ptKurumParametreleri.TELEFON);
                db.AddInParameter(dbCommand, "FAX", DbType.String, ptKurumParametreleri.FAX);
                db.AddInParameter(dbCommand, "VERGI_DAIRESI", DbType.String, ptKurumParametreleri.VERGI_DAIRESI);
                db.AddInParameter(dbCommand, "VERGI_NO", DbType.String, ptKurumParametreleri.VERGI_NO);
                db.AddInParameter(dbCommand, "TIC_SICIL_NO", DbType.String, ptKurumParametreleri.TIC_SICIL_NO);
                db.AddInParameter(dbCommand, "SMTP_ADRES", DbType.String, ptKurumParametreleri.SMTP_ADRES);
                db.AddInParameter(dbCommand, "SMTP_PORT", DbType.Int32, ptKurumParametreleri.SMTP_PORT);
                db.AddInParameter(dbCommand, "MAIL", DbType.String, ptKurumParametreleri.MAIL);
                db.AddInParameter(dbCommand, "MAILSIFRE", DbType.String, ptKurumParametreleri.MAILSIFRE);
                db.AddInParameter(dbCommand, "AKTIF", DbType.Int16, (short)ptKurumParametreleri.AKTIF);
                return db.ExecuteScalar(dbCommand).ToShort(0);
            }
            catch (Exception ex)
            {
                cLog.Write(ex, _iKullaniciID);
                return 0;
            }
        }
        public short DuzenleKurumParametreleri(TBLKURUM_PARAMETRELERI ptKurumParametreleri)
        {
            try
            {
                DbCommand dbCommand = db.GetSqlStringCommand("UPDATE TBLKURUM_PARAMETRELERI SET KURUM_ADI=@KURUM_ADI, ADRES=@ADRES, TELEFON=@TELEFON, FAX=@FAX, VERGI_DAIRESI=@VERGI_DAIRESI, VERGI_NO=@VERGI_NO, TIC_SICIL_NO=@TIC_SICIL_NO, SMTP_ADRES=@SMTP_ADRES, SMTP_PORT=@SMTP_PORT, MAIL=@MAIL, MAILSIFRE=@MAILSIFRE, AKTIF=@AKTIF WHERE ID=@ID");
                db.AddInParameter(dbCommand, "ID", DbType.String, ptKurumParametreleri.ID);
                db.AddInParameter(dbCommand, "KURUM_ADI", DbType.String, ptKurumParametreleri.KURUM_ADI);
                db.AddInParameter(dbCommand, "ADRES", DbType.String, ptKurumParametreleri.ADRES);
                db.AddInParameter(dbCommand, "TELEFON", DbType.String, ptKurumParametreleri.TELEFON);
                db.AddInParameter(dbCommand, "FAX", DbType.String, ptKurumParametreleri.FAX);
                db.AddInParameter(dbCommand, "VERGI_DAIRESI", DbType.String, ptKurumParametreleri.VERGI_DAIRESI);
                db.AddInParameter(dbCommand, "VERGI_NO", DbType.String, ptKurumParametreleri.VERGI_NO);
                db.AddInParameter(dbCommand, "TIC_SICIL_NO", DbType.String, ptKurumParametreleri.TIC_SICIL_NO);
                db.AddInParameter(dbCommand, "SMTP_ADRES", DbType.String, ptKurumParametreleri.SMTP_ADRES);
                db.AddInParameter(dbCommand, "SMTP_PORT", DbType.Int32, ptKurumParametreleri.SMTP_PORT);
                db.AddInParameter(dbCommand, "MAIL", DbType.String, ptKurumParametreleri.MAIL);
                db.AddInParameter(dbCommand, "MAILSIFRE", DbType.String, ptKurumParametreleri.MAILSIFRE);
                db.AddInParameter(dbCommand, "AKTIF", DbType.Int16, (short)ptKurumParametreleri.AKTIF);
                db.ExecuteNonQuery(dbCommand);
                return 1;
            }
            catch (Exception ex)
            {
                cLog.Write(ex, _iKullaniciID);
                return 0;
            }
        }
        public short SilKurumParametreleri(short psID)
        {
            try
            {
                DbCommand dbcommand = db.GetSqlStringCommand("DELETE FROM TBLKURUM_PARAMETRELERI WHERE ID=@ID");
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
        public DataTable ListeleKurumParametreleri(short psID, eAktifDurum? peAktif)
        {
            try
            {
                string strQuery = string.Empty;
                if (psID > 0)
                    strQuery = "ID=" + psID;
                else if (peAktif != null)
                    strQuery = "AKTIF=" + (short)peAktif;
                return db.ExecuteDataSet(CommandType.Text, "SELECT * FROM TBLKURUM_PARAMETRELERI" + (strQuery.IsNull() ? "" : " WHERE ") + strQuery).Tables[0];
            }
            catch (Exception ex)
            {
                cLog.Write(ex, _iKullaniciID);
                return null;
            }
        }
        #endregion

        #region Mail Gönderme
        public short MailGonder(string pstrBaslik, string pstrIcerik, string pstrGonderilecekAdres)
        {
            try
            {
                DataRow drAktifKurum = ListeleKurumParametreleri(0, eAktifDurum.Aktif).Rows[0];
                MailMessage mmEPosta = new MailMessage();
                mmEPosta.Subject = pstrBaslik;
                mmEPosta.Body = pstrIcerik;
                mmEPosta.IsBodyHtml = true;
                mmEPosta.To.Add(pstrGonderilecekAdres.ToString());
                mmEPosta.From = new MailAddress(drAktifKurum["MAIL"].ToString(), drAktifKurum["KURUM_ADI"].ToString(), System.Text.Encoding.UTF8);

                System.Net.NetworkCredential ncCredential;
                ncCredential = new System.Net.NetworkCredential(drAktifKurum["MAIL"].ToString(), drAktifKurum["MAILSIFRE"].ToString());

                SmtpClient SMTP = new SmtpClient();
                SMTP.Port = drAktifKurum["SMTP_PORT"].ToInt(0);
                SMTP.Host = drAktifKurum["SMTP_ADRES"].ToString();
                SMTP.UseDefaultCredentials = true;
                SMTP.EnableSsl = true;
                SMTP.Credentials = ncCredential;
                SMTP.DeliveryMethod = SmtpDeliveryMethod.Network;
                SMTP.Send(mmEPosta);
                return 1;
            }
            catch (Exception ex)
            {
                cLog.Write(ex, _iKullaniciID);
                return 0;
            }
        }
        #endregion

        public int EkleBildirim(TBLBILDIRIMLER ptBildirim)
        {
            try
            {
                DbCommand dbcommand = db.GetSqlStringCommand("INSERT INTO TBLBILDIRIMLER(ADI_SOYADI, EMAIL, BASLIK, ICERIK, USERIP, OLUSMA_ZAMANI) VALUES(@ADI_SOYADI, @EMAIL, @BASLIK, @ICERIK, @USERIP, @OLUSMA_ZAMANI) SELECT @@IDENTITY");
                db.AddInParameter(dbcommand, "ADI_SOYADI", DbType.String, ptBildirim.ADI_SOYADI);
                db.AddInParameter(dbcommand, "EMAIL", DbType.String, ptBildirim.EMAIL);
                db.AddInParameter(dbcommand, "BASLIK", DbType.String, ptBildirim.BASLIK);
                db.AddInParameter(dbcommand, "ICERIK", DbType.String, ptBildirim.ICERIK);
                db.AddInParameter(dbcommand, "USERIP", DbType.String, HttpContext.Current.Request.UserHostAddress);
                db.AddInParameter(dbcommand, "OLUSMA_ZAMANI", DbType.DateTime, DateTime.Now);
                return db.ExecuteScalar(dbcommand).ToInt(0);
            }
            catch (Exception ex)
            {
                cLog.Write(ex, _iKullaniciID);
                return 0;
            }
        }

        public DataTable ListeleBildirimler(int piID)
        {
            try
            {
                string strQuery = string.Empty;
                if (piID > 0)
                    strQuery = "ID=" + piID;
                return db.ExecuteDataSet(CommandType.Text, "SELECT * FROM TBLBILDIRIMLER" + (strQuery.IsNull() ? "" : " WHERE ") + strQuery+" ORDER BY ID DESC").Tables[0];
            }
            catch (Exception ex)
            {
                cLog.Write(ex, _iKullaniciID);
                return null;
            }
        }
    }
}

