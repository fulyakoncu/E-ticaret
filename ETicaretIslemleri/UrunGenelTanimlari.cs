using System;
using System.Data;
using System.Data.Common;
using System.Web;
using Microsoft.Practices.EnterpriseLibrary.Data;
using MiniCore;

using System.Collections;
using LogSistemi;

namespace ETicaretIslemleri
{
    public class cUrunGenel : IDisposable
    {
        private Database db;
        private cLogging cLog;
        private int _iKullaniciID = HttpContext.Current.Session["USERID"].ToInt(0);

        public cUrunGenel()
        {
            cLog = new cLogging(false);
            db = DatabaseFactory.CreateDatabase();
        }

        public void Dispose()
        {
            cLog.Dispose();
            GC.SuppressFinalize(this);
        }

        #region Marka Tanımlamaları
        public int EkleMarka(TBLMARKALAR ptMarkalar)
        {
            try
            {
                DbCommand dbcommand = db.GetSqlStringCommand("INSERT INTO TBLMARKALAR(KODU,ADI,LOGO,ACIKLAMA) VALUES(@KODU, @ADI,@LOGO,@ACIKLAMA) SELECT @@IDENTITY");
                db.AddInParameter(dbcommand, "KODU", DbType.String, ptMarkalar.KODU);
                db.AddInParameter(dbcommand, "ADI", DbType.String, ptMarkalar.ADI);
                db.AddInParameter(dbcommand, "LOGO", DbType.String, ptMarkalar.LOGO);
                db.AddInParameter(dbcommand, "ACIKLAMA", DbType.String, ptMarkalar.ACIKLAMA);
                return db.ExecuteScalar(dbcommand).ToInt(0);
            }
            catch (Exception ex)
            {
                cLog.Write(ex, _iKullaniciID);
                return 0;
            }
        }
        /// <summary>
        /// Marka bilgilerini günceller.
        /// Logo verisi NULL gelirse logo alanı güncellenmez.
        /// </summary>
        /// <param name="ptMarkalar"></param>
        /// <returns></returns>
        public short DuzenleMarka(TBLMARKALAR ptMarkalar)
        {
            try
            {
                DbCommand dbcommand;
                if (ptMarkalar.LOGO.IsNull())
                    dbcommand = db.GetSqlStringCommand("UPDATE TBLMARKALAR SET KODU=@KODU, ADI=@ADI, LOGO=@LOGO, ACIKLAMA=@ACIKLAMA WHERE ID=@ID");
                else
                    dbcommand = db.GetSqlStringCommand("UPDATE TBLMARKALAR SET KODU=@KODU, ADI=@ADI, ACIKLAMA=@ACIKLAMA WHERE ID=@ID");

                db.AddInParameter(dbcommand, "ID", DbType.Int32, ptMarkalar.ID);
                db.AddInParameter(dbcommand, "KODU", DbType.String, ptMarkalar.KODU);
                db.AddInParameter(dbcommand, "ADI", DbType.String, ptMarkalar.ADI);
                if (!ptMarkalar.LOGO.IsNull())
                    db.AddInParameter(dbcommand, "LOGO", DbType.String, ptMarkalar.LOGO);
                db.AddInParameter(dbcommand, "ACIKLAMA", DbType.String, ptMarkalar.ACIKLAMA);
                return 1;
            }
            catch (Exception ex)
            {
                cLog.Write(ex, _iKullaniciID);
                return 0;
            }
        }

        public DataTable ListeleMarka(int piID)
        {
            try
            {
                string strQuery = null;
                if (piID > 0)
                    strQuery = "ID=" + piID;
                return db.ExecuteDataSet(CommandType.Text, "SELECT * FROM TBLMARKALAR" + (strQuery.IsNull() ? "" : " WHERE ") + strQuery).Tables[0];
            }
            catch (Exception ex)
            {
                cLog.Write(ex, _iKullaniciID);
                return null;
            }
        }
        #endregion

        #region Kategori Tanımları
        public short EkleKategoriler(TBLKATEGORILER ptKategori)
        {
            try
            {
                DbCommand dbcommand = db.GetSqlStringCommand("INSERT INTO TBLKATEGORILER(NODEID, ADI, SIRANO, KATEGORI_RESMI, AKTIF) VALUES(@NODEID, @ADI, @SIRANO, @KATEGORI_RESMI, @AKTIF) SELECT @@IDENTITY");
                db.AddInParameter(dbcommand, "NODEID", DbType.Int16, ptKategori.NODEID);
                db.AddInParameter(dbcommand, "ADI", DbType.String, ptKategori.ADI);
                db.AddInParameter(dbcommand, "SIRANO", DbType.Int16, ptKategori.SIRANO);
                db.AddInParameter(dbcommand, "KATEGORI_RESMI", DbType.String, ptKategori.KATEGORI_RESMI);
                db.AddInParameter(dbcommand, "AKTIF", DbType.Int16, (short)ptKategori.AKTIF);
                return db.ExecuteScalar(dbcommand).ToShort(0);
            }
            catch (Exception ex)
            {
                cLog.Write(ex, _iKullaniciID);
                return 0;
            }
        }
        /// <summary>
        /// Resim bilgisi null gelirse resim alanını güncellemez.
        /// </summary>
        /// <param name="ptKategori"></param>
        /// <returns></returns>
        public short DuzenleKategori(TBLKATEGORILER ptKategori)
        {
            try
            {
                DbCommand dbcommand;
                if (ptKategori.KATEGORI_RESMI.IsNull())
                    dbcommand = db.GetSqlStringCommand("UPDATE TBLKATEGORILER SET NODEID=@NODEID, ADI=@ADI, SIRANO=@SIRANO, AKTIF=@AKTIF WHERE ID=@ID");
                else
                    dbcommand = db.GetSqlStringCommand("UPDATE TBLKATEGORILER SET NODEID=@NODEID, ADI=@ADI, SIRANO=@SIRANO, KATEGORI_RESMI=@KATEGORI_RESMI, AKTIF=@AKTIF WHERE ID=@ID");
                db.AddInParameter(dbcommand, "ID", DbType.Int16, ptKategori.ID);
                db.AddInParameter(dbcommand, "NODEID", DbType.Int16, ptKategori.NODEID);
                db.AddInParameter(dbcommand, "ADI", DbType.String, ptKategori.ADI);
                db.AddInParameter(dbcommand, "SIRANO", DbType.Int16, ptKategori.SIRANO);
                if (!ptKategori.KATEGORI_RESMI.IsNull())
                    db.AddInParameter(dbcommand, "KATEGORI_RESMI", DbType.String, ptKategori.KATEGORI_RESMI);
                db.AddInParameter(dbcommand, "AKTIF", DbType.Int16, (short)ptKategori.AKTIF);
                return db.ExecuteNonQuery(dbcommand).ToShort(0);
            }
            catch (Exception ex)
            {
                cLog.Write(ex, _iKullaniciID);
                return 0;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="psID"></param>
        /// <param name="psNodeID">-1 Girilirse tüm tablo gelir</param>
        /// <param name="peAktif"></param>
        /// <returns></returns>
        public DataTable ListeleKategori(short psID, short psNodeID, eAktifDurum? peAktif)
        {
            try
            {
                string strQuery = string.Empty;
                if (psID > 0)
                    strQuery = "ID=" + psID;
                else
                {
                    if (psNodeID > -1)
                        strQuery = "NODEID=" + psNodeID;
                    if (!peAktif.IsNull())
                        strQuery += (strQuery.IsNull() ? "" : " AND ") + " AKTIF=" + (short)peAktif;
                }

                return db.ExecuteDataSet(CommandType.Text, "SELECT * FROM TBLKATEGORILER" + (strQuery.IsNull() ? "" : " WHERE ") + strQuery + " ORDER BY SIRANO ASC").Tables[0];
            }
            catch (Exception ex)
            {
                cLog.Write(ex, _iKullaniciID);
                return null;
            }
        }
        #endregion

        #region ürün tanımlaması
        public int EkleUrun(TBLURUNLER ptUrunler)
        {
            try
            {
                DbCommand dbcommand = db.GetSqlStringCommand("INSERT INTO TBLURUNLER(KODU,MARKA,MODEL, KATEGORI,FIYATI,PARABIRIMI,KAMPANYALI,ACIKLAMA,SLIDER,INDIRIM,INDIRIMLI_FIYAT,AKTIF) VALUES(@KODU, @MARKA, @MODEL, @KATEGORI, @FIYATI, @PARABIRIMI, @KAMPANYALI, @ACIKLAMA,@SLIDER,@INDIRIM,@INDIRIMLI_FIYAT, @AKTIF) SELECT @@IDENTITY");
                db.AddInParameter(dbcommand, "KODU", DbType.String, ptUrunler.KODU);
                db.AddInParameter(dbcommand, "MARKA", DbType.Int16, ptUrunler.MARKA);
                db.AddInParameter(dbcommand, "MODEL", DbType.String, ptUrunler.MODEL);
                db.AddInParameter(dbcommand, "KATEGORI", DbType.Int16, ptUrunler.KATEGORI);
                db.AddInParameter(dbcommand, "FIYATI", DbType.Decimal, ptUrunler.FIYATI);
                db.AddInParameter(dbcommand, "PARABIRIMI", DbType.Int16, (short)ptUrunler.PARABIRIMI);
                db.AddInParameter(dbcommand, "KAMPANYALI", DbType.Int16, (short)ptUrunler.KAMPANYALI);
                db.AddInParameter(dbcommand, "ACIKLAMA", DbType.String, ptUrunler.ACIKLAMA);
                db.AddInParameter(dbcommand, "SLIDER", DbType.Int16, (short)ptUrunler.SLIDER);
                db.AddInParameter(dbcommand, "INDIRIM", DbType.Int16, (short)ptUrunler.INDIRIM);
                db.AddInParameter(dbcommand, "INDIRIMLI_FIYAT", DbType.Decimal, ptUrunler.INDIRIMLI_FIYAT);
                db.AddInParameter(dbcommand, "AKTIF", DbType.Int16, (short)ptUrunler.AKTIF);
                return db.ExecuteScalar(dbcommand).ToInt(0);
            }
            catch (Exception ex)
            {
                cLog.Write(ex, _iKullaniciID);
                return 0;
            }
        }
        public int DuzenleUrun(TBLURUNLER ptUrunler)
        {
            try
            {
                DbCommand dbcommand = db.GetSqlStringCommand("UPDATE TBLURUNLER SET KODU=@KODU, MARKA=@MARKA, MODEL=@MODEL, KATEGORI=@KATEGORI, FIYATI=@FIYATI, PARABIRIMI=@PARABIRIMI, KAMPANYALI=@KAMPANYALI, AKTIF=@AKTIF, ACIKLAMA=@ACIKLAMA, SLIDER=@SLIDER, INDIRIM=@INDIRIM, INDIRIMLI_FIYAT=@INDIRIMLI_FIYAT WHERE ID=@ID");
                db.AddInParameter(dbcommand, "ID", DbType.Int32, ptUrunler.ID);
                db.AddInParameter(dbcommand, "KODU", DbType.String, ptUrunler.KODU);
                db.AddInParameter(dbcommand, "MARKA", DbType.Int16, ptUrunler.MARKA);
                db.AddInParameter(dbcommand, "MODEL", DbType.String, ptUrunler.MODEL);
                db.AddInParameter(dbcommand, "KATEGORI", DbType.Int16, ptUrunler.KATEGORI);
                db.AddInParameter(dbcommand, "FIYATI", DbType.Decimal, ptUrunler.FIYATI);
                db.AddInParameter(dbcommand, "PARABIRIMI", DbType.Int16, (short)ptUrunler.PARABIRIMI);
                db.AddInParameter(dbcommand, "KAMPANYALI", DbType.Int16, (short)ptUrunler.KAMPANYALI);
                db.AddInParameter(dbcommand, "ACIKLAMA", DbType.String, ptUrunler.ACIKLAMA);
                db.AddInParameter(dbcommand, "SLIDER", DbType.Int16, (short)ptUrunler.SLIDER);
                db.AddInParameter(dbcommand, "INDIRIM", DbType.Int16, (short)ptUrunler.INDIRIM);
                db.AddInParameter(dbcommand, "INDIRIMLI_FIYAT", DbType.Decimal, ptUrunler.INDIRIMLI_FIYAT);
                db.AddInParameter(dbcommand, "AKTIF", DbType.Int16, (short)ptUrunler.AKTIF);
                db.ExecuteNonQuery(dbcommand);
                return 1;
            }
            catch (Exception ex)
            {
                cLog.Write(ex, _iKullaniciID);
                return 0;
            }
        }
        public DataTable ListeleUrun(int piID, short psMarka, short psKategori, eEvetHayir? peKampanya, eEvetHayir? peSlider, eEvetHayir? peIndirimli, eAktifDurum? peAktif, string pstrModel, string pstrKodu,bool pbAltKategorilerDahil)
        {
            try
            {
                string strQuery = string.Empty;
                if (piID != 0)
                    strQuery = "ID=" + piID;
                else
                {
                    if (psMarka > 0)
                        strQuery += (strQuery.IsNull() ? "" : " AND ") + "MARKA=" + psMarka;
                    if (psKategori > 0)
                    {
                        if (pbAltKategorilerDahil)
                        {
                            strQuery += (strQuery.IsNull() ? "" : " AND ") + "KATEGORI IN(";
                            ArrayList diziID = new ArrayList();
                            foreach (Object obj in AltKategoriler(diziID, psKategori))
                            {
                                strQuery += obj.ToString() + ",";
                            }
                            strQuery += psKategori + ")";
                        }else
                            strQuery += (strQuery.IsNull() ? "" : " AND ") + "KATEGORI=" + psKategori;
                    }
                    if (!peKampanya.IsNull())
                        strQuery += (strQuery.IsNull() ? "" : " AND ") + "KAMPANYALI=" + (short)peKampanya;
                    if (!peSlider.IsNull())
                        strQuery += (strQuery.IsNull() ? "" : " AND ") + "SLIDER=" + (short)peSlider;
                    if (!peIndirimli.IsNull())
                        strQuery += (strQuery.IsNull() ? "" : " AND ") + "INDIRIM=" + (short)peIndirimli;
                    if (pstrKodu != string.Empty)
                        strQuery += (strQuery.IsNull() ? "" : " AND ") + "KODU LIKE '%'+@KODU+'%'";
                    if (pstrModel != string.Empty)
                        strQuery += (strQuery.IsNull() ? "" : " AND ") + "MODEL LIKE '%'+ @MODEL+'%'";

                }
                DbCommand dbcommand = db.GetSqlStringCommand("SELECT *,(SELECT ADI FROM TBLMARKALAR M WHERE M.ID=U.MARKA)MARKA_ADI,(SELECT ADI FROM TBLKATEGORILER K WHERE K.ID=U.KATEGORI)KATEGORI_ADI FROM TBLURUNLER U" + (strQuery.IsNull() ? "" : "  WHERE ") + strQuery);
                if (pstrKodu != string.Empty)
                    db.AddInParameter(dbcommand, "KODU", DbType.String, pstrKodu);
                if (pstrModel != string.Empty)
                    db.AddInParameter(dbcommand, "MODEL", DbType.String, pstrModel);
                return db.ExecuteDataSet(dbcommand).Tables[0];
            }
            catch (Exception ex)
            {
                cLog.Write(ex, _iKullaniciID);
                return null;
            }
        }

        public ArrayList AltKategoriler(ArrayList diziID, short nodeID)
        {
            try
            {
                foreach (DataRow dr in ListeleKategori(0, nodeID, eAktifDurum.Aktif).Rows)
                {
                    diziID.Add(dr["ID"].ToInt());
                    AltKategoriler(diziID, dr["ID"].ToShort());
                }
                return diziID;

            }
            catch (Exception ex)
            {
                cLog.Write(ex, _iKullaniciID);
                return diziID;
            }
        }
        #endregion

        #region Özellik tanımlamaları
        public int EkleOzellikTanimi(TBLURUN_OZELLIK_TANIMLARI ptOzellik)
        {
            try
            {
                DbCommand dbcommand = db.GetSqlStringCommand("INSERT INTO TBLURUN_OZELLIK_TANIMLARI(OZELLIK_ADI,BIRIMI) VALUES(@OZELLIK_ADI,@BIRIMI) SELECT @@IDENTITY");
                db.AddInParameter(dbcommand, "OZELLIK_ADI", DbType.String, ptOzellik.OZELLIK_ADI);
                db.AddInParameter(dbcommand, "BIRIMI", DbType.String, ptOzellik.BIRIMI);
                return db.ExecuteScalar(dbcommand).ToInt(0);
            }
            catch (Exception ex)
            {
                cLog.Write(ex, _iKullaniciID);
                return 0;
            }
        }
        public int DuzenleOzellikTanimi(TBLURUN_OZELLIK_TANIMLARI ptOzellik)
        {
            try
            {
                DbCommand dbcommand = db.GetSqlStringCommand("UPDATE TBLURUN_OZELLIK_TANIMLARI SET OZELLIK_ADI=@OZELLIK_ADI, BIRIMI=@BIRIMI WHERE ID=@ID");
                db.AddInParameter(dbcommand, "ID", DbType.Int32, ptOzellik.ID);
                db.AddInParameter(dbcommand, "OZELLIK_ADI", DbType.String, ptOzellik.OZELLIK_ADI);
                db.AddInParameter(dbcommand, "BIRIMI", DbType.String, ptOzellik.BIRIMI);
                db.ExecuteNonQuery(dbcommand);
                return 1;
            }
            catch (Exception ex)
            {
                cLog.Write(ex, _iKullaniciID);
                return 0;
            }
        }
        public DataTable ListeOzellikTanimi(int piID)
        {
            try
            {
                return db.ExecuteDataSet(CommandType.Text, "SELECT * FROM TBLURUN_OZELLIK_TANIMLARI" + (piID == 0 ? "" : " WHERE ID=" + piID) + " ORDER BY OZELLIK_ADI ASC").Tables[0];
            }
            catch (Exception ex)
            {
                cLog.Write(ex, _iKullaniciID);
                return null;
            }
        }
        #endregion

        #region grup tanımlamaları
        public int EkleGrupTanimi(TBLURUN_GRUP_TANIMLARI ptGrup)
        {
            try
            {
                DbCommand dbcommand = db.GetSqlStringCommand("INSERT INTO TBLURUN_GRUP_TANIMLARI(GRUP_ADI,ACIKLAMA,GRUP_TIPI) VALUES(@GRUP_ADI,@ACIKLAMA,@GRUP_TIPI) SELECT @@IDENTITY");
                db.AddInParameter(dbcommand, "GRUP_ADI", DbType.String, ptGrup.GRUP_ADI);
                db.AddInParameter(dbcommand, "ACIKLAMA", DbType.String, ptGrup.ACIKLAMA);
                db.AddInParameter(dbcommand, "GRUP_TIPI", DbType.Int16, (short)ptGrup.GRUP_TIPI);
                return db.ExecuteScalar(dbcommand).ToInt(0);
            }
            catch (Exception ex)
            {
                cLog.Write(ex, _iKullaniciID);
                return 0;
            }
        }
        public int DuzenleGrupTanimi(TBLURUN_GRUP_TANIMLARI ptGrup)
        {
            try
            {
                DbCommand dbcommand = db.GetSqlStringCommand("UPDATE TBLURUN_GRUP_TANIMLARI SET GRUP_ADI=@GRUP_ADI, ACIKLAMA=@ACIKLAMA,GRUP_TIPI=@GRUP_TIPI WHERE ID=@ID");
                db.AddInParameter(dbcommand, "ID", DbType.Int32, ptGrup.ID);
                db.AddInParameter(dbcommand, "GRUP_ADI", DbType.String, ptGrup.GRUP_ADI);
                db.AddInParameter(dbcommand, "ACIKLAMA", DbType.String, ptGrup.ACIKLAMA);
                db.AddInParameter(dbcommand, "GRUP_TIPI", DbType.Int16, (short)ptGrup.GRUP_TIPI);
                db.ExecuteNonQuery(dbcommand);
                return 1;
            }
            catch (Exception ex)
            {
                cLog.Write(ex, _iKullaniciID);
                return 0;
            }
        }
        public DataTable ListeGrupTanimi(int piID,eGrupTipi? peGrupTipi)
        {
            try
            {
                string strQuery = null;
                if (piID > 0)
                    strQuery = "ID=" + piID;
                else if (!peGrupTipi.IsNull())
                    strQuery = "GRUP_TIPI=" + peGrupTipi;
                return db.ExecuteDataSet(CommandType.Text, "SELECT * FROM TBLURUN_GRUP_TANIMLARI"+ (strQuery.IsNull() ? "" : " WHERE ") + strQuery+ " ORDER BY GRUP_ADI ASC").Tables[0];
            }
            catch (Exception ex)
            {
                cLog.Write(ex, _iKullaniciID);
                return null;
            }
        }
        #endregion

        #region Urun-Özellik Eşleştirmesi
        public int EkleUrunOzellik(TBLURUN_OZELLIKLERI ptUrunOzellik)
        {
            try
            {
                DbCommand dbcommand = db.GetSqlStringCommand("INSERT INTO TBLURUN_OZELLIKLERI(URUNID,OZELLIKID,DEGER) VALUES(@URUNID,@OZELLIKID,@DEGER) SELECT @@IDENTITY");
                db.AddInParameter(dbcommand, "URUNID", DbType.Int32, ptUrunOzellik.URUNID);
                db.AddInParameter(dbcommand, "OZELLIKID", DbType.Int32, ptUrunOzellik.OZELLIKID);
                db.AddInParameter(dbcommand, "DEGER", DbType.String, ptUrunOzellik.DEGER);
                return db.ExecuteScalar(dbcommand).ToInt(0);
            }
            catch (Exception ex)
            {
                cLog.Write(ex, _iKullaniciID);
                return 0;
            }
        }

        public short DuzenleUrunOzellik(TBLURUN_OZELLIKLERI ptUrunOzellik)
        {
            try
            {
                DbCommand dbcommand = db.GetSqlStringCommand("UPDATE TBLURUN_OZELLIKLERI SET URUNID=@URUNID, OZELLIKID=@OZELLIKID, DEGER=@DEGER WHERE ID=@ID");
                db.AddInParameter(dbcommand, "ID", DbType.Int32, ptUrunOzellik.ID);
                db.AddInParameter(dbcommand, "URUNID", DbType.Int32, ptUrunOzellik.URUNID);
                db.AddInParameter(dbcommand, "OZELLIKID", DbType.Int32, ptUrunOzellik.OZELLIKID);
                db.AddInParameter(dbcommand, "DEGER", DbType.String, ptUrunOzellik.DEGER);
                db.ExecuteNonQuery(dbcommand);
                return 1;
            }
            catch (Exception ex)
            {
                cLog.Write(ex, _iKullaniciID);
                return 0;
            }
        }

        public DataTable ListeleUrunOzellik(int piID, int piUrunID)
        {
            try
            {
                string strQuery = null;
                if (piID != 0)
                    strQuery = "ID=" + piID;
                else if (piUrunID > 0)
                    strQuery = "URUNID=" + piUrunID;
                return db.ExecuteDataSet(CommandType.Text, "SELECT *,(SELECT T.OZELLIK_ADI FROM TBLURUN_OZELLIK_TANIMLARI T WHERE T.ID=O.OZELLIKID)OZELLIK_ADI,(SELECT T.BIRIMI FROM TBLURUN_OZELLIK_TANIMLARI T WHERE T.ID=O.OZELLIKID)OZELLIK_BIRIMI FROM TBLURUN_OZELLIKLERI O" + (strQuery.IsNull() ? "" : " WHERE ") + strQuery).Tables[0];
            }
            catch (Exception ex)
            {
                cLog.Write(ex, _iKullaniciID);
                return null;
            }
        }
        public short SilUrunOzellik(int piID)
        {
            try
            {
                db.ExecuteNonQuery(CommandType.Text, "DELETE FROM TBLURUN_OZELLIKLERI WHERE ID=" + piID);
                return 1;
            }
            catch (Exception ex)
            {
                cLog.Write(ex, _iKullaniciID);
                return 0;
            }
        }
        #endregion

        #region Urun Grup Eşleştirmesi
        public int EkleUrunGrup(TBLURUN_GRUPLARI ptUrunGruplari)
        {
            try
            {
                DbCommand dbcommand = db.GetSqlStringCommand("INSERT INTO TBLURUN_GRUPLARI(URUNID,GRUPID) VALUES(@URUNID,@GRUPID) SELECT @@IDENTITY");
                db.AddInParameter(dbcommand, "URUNID", DbType.Int32, ptUrunGruplari.URUNID);
                db.AddInParameter(dbcommand, "GRUPID", DbType.Int32, ptUrunGruplari.GRUPID);
                return db.ExecuteScalar(dbcommand).ToInt(0);
            }
            catch (Exception ex)
            {
                cLog.Write(ex, _iKullaniciID);
                return 0;
            }
        }

        public short DuzenleUrunGrup(TBLURUN_GRUPLARI ptUrunGruplari)
        {
            try
            {
                DbCommand dbcommand = db.GetSqlStringCommand("UPDATE TBLURUN_OZELLIKLERI SET URUNID=@URUNID, GRUPID=@GRUPID WHERE ID=@ID");
                db.AddInParameter(dbcommand, "ID", DbType.Int32, ptUrunGruplari.ID);
                db.AddInParameter(dbcommand, "URUNID", DbType.Int32, ptUrunGruplari.URUNID);
                db.AddInParameter(dbcommand, "GRUPID", DbType.Int32, ptUrunGruplari.GRUPID);

                db.ExecuteNonQuery(dbcommand);
                return 1;
            }
            catch (Exception ex)
            {
                cLog.Write(ex, _iKullaniciID);
                return 0;
            }
        }

        public DataTable ListeleUrunGrup(int piID, int piUrunID)
        {
            try
            {
                string strQuery = null;
                if (piID != 0)
                    strQuery = "ID=" + piID;
                else if (piUrunID != 0)
                    strQuery = "URUNID=" + piUrunID;
                return db.ExecuteDataSet(CommandType.Text, "SELECT *,(SELECT G.GRUP_ADI FROM TBLURUN_GRUP_TANIMLARI G WHERE G.ID=A.GRUPID)GRUP_ADI,(SELECT G.GRUP_TIPI FROM TBLURUN_GRUP_TANIMLARI G WHERE G.ID=A.GRUPID)GRUP_TIPI,(SELECT G.ACIKLAMA FROM TBLURUN_GRUP_TANIMLARI G WHERE G.ID=A.GRUPID)GRUP_ACIKLAMA FROM TBLURUN_GRUPLARI A" + (strQuery.IsNull() ? "" : " WHERE ") + strQuery).Tables[0];
            }
            catch (Exception ex)
            {
                cLog.Write(ex, _iKullaniciID);
                return null;
            }
        }
        public short SilUrunGrup(int piID)
        {
            try
            {
                db.ExecuteNonQuery(CommandType.Text, "DELETE FROM TBLURUN_GRUPLARI WHERE ID=" + piID);
                return 1;
            }
            catch (Exception ex)
            {
                cLog.Write(ex, _iKullaniciID);
                return 0;
            }
        }
        #endregion

        #region Ürün Resim Eşleştirmesi
        public int EkleUrunResim(TBLURUN_RESIMLERI ptUrunResimleri)
        {
            try
            {
                DbCommand dbcommand = db.GetSqlStringCommand("INSERT INTO TBLURUN_RESIMLERI(URUNID,RESIMADI,ANARESIM,SLIDERRESIM) VALUES(@URUNID,@RESIMADI,@ANARESIM,@SLIDERRESIM) SELECT @@IDENTITY");
                db.AddInParameter(dbcommand, "URUNID", DbType.Int32, ptUrunResimleri.URUNID);
                db.AddInParameter(dbcommand, "RESIMADI", DbType.String, ptUrunResimleri.RESIMADI);
                db.AddInParameter(dbcommand, "ANARESIM", DbType.Int16, (short)ptUrunResimleri.ANARESIM);
                db.AddInParameter(dbcommand, "SLIDERRESIM", DbType.Int16, (short)ptUrunResimleri.SLIDERRESIM);
                return db.ExecuteScalar(dbcommand).ToInt(0);
            }
            catch (Exception ex)
            {
                cLog.Write(ex, _iKullaniciID);
                return 0;
            }
        }
        public short DuzenleUrunResim(TBLURUN_RESIMLERI ptUrunResimleri)
        {
            try
            {
                DbCommand dbcommand;
                if (ptUrunResimleri.RESIMADI.IsNull())
                    dbcommand = db.GetSqlStringCommand("UPDATE TBLURUN_RESIMLERI SET URUNID=@URUNID,  ANARESIM=@ANARESIM, SLIDERRESIM=@SLIDERRESIM WHERE ID=@ID");
                else
                    dbcommand = db.GetSqlStringCommand("UPDATE TBLURUN_RESIMLERI SET URUNID=@URUNID, RESIMADI=@RESIMADI, ANARESIM=@ANARESIM, SLIDERRESIM=@SLIDERRESIM WHERE ID=@ID");
                db.AddInParameter(dbcommand, "ID", DbType.Int32, ptUrunResimleri.ID);
                db.AddInParameter(dbcommand, "URUNID", DbType.Int32, ptUrunResimleri.URUNID);
                if (!ptUrunResimleri.RESIMADI.IsNull())
                    db.AddInParameter(dbcommand, "RESIMADI", DbType.String, ptUrunResimleri.RESIMADI);
                db.AddInParameter(dbcommand, "ANARESIM", DbType.Int16, (short)ptUrunResimleri.ANARESIM);
                db.AddInParameter(dbcommand, "SLIDERRESIM", DbType.Int16, (short)ptUrunResimleri.SLIDERRESIM);

                db.ExecuteNonQuery(dbcommand);
                return 1;
            }
            catch (Exception ex)
            {
                cLog.Write(ex, _iKullaniciID);
                return 0;
            }
        }
        public DataTable ListeleUrunResim(int piID, int piUrunID, eEvetHayir? peSlider, eEvetHayir? peAnaResim)
        {
            try
            {
                string strQuery = null;
                if (piID != 0)
                    strQuery = "ID=" + piID;
                else
                {
                    if (piUrunID != 0)
                        strQuery = "URUNID=" + piUrunID;
                    if (!peSlider.IsNull())
                        strQuery += (strQuery.IsNull() ? "" : " AND ") + "SLIDERRESIM=" + (short)peSlider;
                    if (!peAnaResim.IsNull())
                        strQuery += (strQuery.IsNull() ? "" : " AND ") + "ANARESIM=" + (short)peAnaResim;
                }
                return db.ExecuteDataSet(CommandType.Text, "SELECT * FROM TBLURUN_RESIMLERI" + (strQuery.IsNull() ? "" : " WHERE ") + strQuery).Tables[0];
            }
            catch (Exception ex)
            {
                cLog.Write(ex, _iKullaniciID);
                return null;
            }
        }
        public short SilUrunResim(int piID)
        {
            try
            {
                db.ExecuteNonQuery(CommandType.Text, "DELETE FROM TBLURUN_RESIMLERI WHERE ID=" + piID);
                return 1;
            }
            catch (Exception ex)
            {
                cLog.Write(ex, _iKullaniciID);
                return 0;
            }
        }
        #endregion

        #region Ürün Stok İşlemleri
        public int EkleUrunStokIslemi(TBLSTOKISLEMLERI ptStokIslemleri)
        {
            try
            {
                DbCommand dbcommand = db.GetSqlStringCommand("INSERT INTO TBLSTOKISLEMLERI(URUNID,MIKTAR,ISLEMTIPI,REFERANSID,OLUSMA_ZAMANI) VALUES(@URUNID, @MIKTAR, @ISLEMTIPI, @REFERANSID, @OLUSMA_ZAMANI) SELECT @@IDENTITY");
                db.AddInParameter(dbcommand, "URUNID", DbType.Int32, ptStokIslemleri.URUNID);
                db.AddInParameter(dbcommand, "MIKTAR", DbType.Decimal, ptStokIslemleri.MIKTAR);
                db.AddInParameter(dbcommand, "ISLEMTIPI", DbType.Int16, (short)ptStokIslemleri.ISLEMTIPI);
                db.AddInParameter(dbcommand, "REFERANSID", DbType.Double,ptStokIslemleri.REFERANSID);
                db.AddInParameter(dbcommand, "OLUSMA_ZAMANI", DbType.DateTime, DateTime.Now);
                return db.ExecuteScalar(dbcommand).ToInt(0);
            }
            catch (Exception ex)
            {
                cLog.Write(ex, _iKullaniciID);
                return 0;
            }
        }
        public short DuzenleStokIslemi(TBLSTOKISLEMLERI ptStokIslemleri)
        {
            try
            {
                DbCommand dbcommand = db.GetSqlStringCommand("UPDATE TBLSTOKISLEMLERI SET URUNID=@URUNID, MIKTAR=@MIKTAR, ISLEMTIPI=@ISLEMTIPI,REFERANSID=@REFERANSID WHERE ID=@ID");
                db.AddInParameter(dbcommand, "ID", DbType.Int64, ptStokIslemleri.ID);
                db.AddInParameter(dbcommand, "URUNID", DbType.Int32, ptStokIslemleri.URUNID);
                db.AddInParameter(dbcommand, "MIKTAR", DbType.Decimal, ptStokIslemleri.MIKTAR);
                db.AddInParameter(dbcommand, "ISLEMTIPI", DbType.Int16, (short)ptStokIslemleri.ISLEMTIPI);
                db.AddInParameter(dbcommand, "REFERANSID", DbType.Double, ptStokIslemleri.REFERANSID);

                db.ExecuteNonQuery(dbcommand);
                return 1;
            }
            catch (Exception ex)
            {
                cLog.Write(ex, _iKullaniciID);
                return 0;
            }
        }
        public DataTable ListeleStokIslemleri(long plID, int piUrunID, eStokIslemTipi? peIslemTipi)
        {
            try
            {
                string strQuery = null;
                if (plID != 0)
                    strQuery = "ID=" + plID;
                else
                {
                    if (piUrunID > 0)
                        strQuery = "URUNID=" + piUrunID;
                    if (!peIslemTipi.IsNull())
                        strQuery += (strQuery.IsNull() ? "" : " AND ") + "ISLEMTIPI=" + (short)peIslemTipi;
                }
                return db.ExecuteDataSet(CommandType.Text, "SELECT *,(SELECT MODEL FROM TBLURUNLER U WHERE U.ID=I.URUNID)URUN_ADI FROM TBLSTOKISLEMLERI I" + (strQuery.IsNull() ? "" : " WHERE ") + strQuery).Tables[0];
            }
            catch (Exception ex)
            {
                cLog.Write(ex, _iKullaniciID);
                return null;
            }
        }
        public short SilStokIslem(long plID)
        {
            try
            {
                db.ExecuteNonQuery(CommandType.Text, "DELETE FROM TBLSTOKISLEMLERI WHERE ID=" + plID);
                return 1;
            }
            catch (Exception ex)
            {
                cLog.Write(ex, _iKullaniciID);
                return 0;
            }
        }

        public decimal VerKalanStok(int piUrunID)
        {
            try
            {
                DataTable dt = db.ExecuteDataSet(CommandType.Text, "SELECT (SELECT ISNULL(SUM(MIKTAR),0) FROM TBLSTOKISLEMLERI X WHERE ISLEMTIPI=" + (short)eStokIslemTipi.Giris + " AND X.URUNID=S.URUNID)-(select ISNULL(SUM(MIKTAR),0) FROM TBLSTOKISLEMLERI X WHERE ISLEMTIPI=" + (short)eStokIslemTipi.Cikis + " AND X.URUNID=S.URUNID)STOKMIKTARI FROM TBLSTOKISLEMLERI S WHERE URUNID=" + piUrunID + " GROUP BY URUNID").Tables[0];
                if (dt.Rows.Count > 0)
                    return dt.Rows[0]["STOKMIKTARI"].ToDecimal();
                else
                    return 0;
            }
            catch (Exception ex)
            {
                cLog.Write(ex, _iKullaniciID);
                return 0;
            }
        }
        #endregion

        #region Ürün Yorum İşlemleri
        public int EkleYorum(TBLYORUMLAR ptYorumlar)
        {
            try
            {
                DbCommand dbcommand = db.GetSqlStringCommand("INSERT INTO TBLYORUMLAR(URUN_ID,UYE_ID,BASLIK,MESAJ,AKTIF,TARIH,IP) VALUES(@URUN_ID,@UYE_ID,@BASLIK,@MESAJ,@AKTIF,@TARIH,@IP) SELECT @@IDENTITY");
                db.AddInParameter(dbcommand, "URUN_ID", DbType.Int16, ptYorumlar.URUN_ID);
                db.AddInParameter(dbcommand, "UYE_ID", DbType.Int16, ptYorumlar.UYE_ID);
                db.AddInParameter(dbcommand, "BASLIK", DbType.String, ptYorumlar.BASLIK);
                db.AddInParameter(dbcommand, "MESAJ", DbType.String, ptYorumlar.MESAJ);
                db.AddInParameter(dbcommand, "TARIH", DbType.DateTime, ptYorumlar.TARIH);
                db.AddInParameter(dbcommand, "AKTIF", DbType.Int16,(short) ptYorumlar.AKTIF);
                db.AddInParameter(dbcommand, "IP", DbType.String, ptYorumlar.IP);
                return db.ExecuteScalar(dbcommand).ToInt(0);
            }
            catch (Exception ex)
            {
                cLog.Write(ex, _iKullaniciID);
                return 0;
            }
        }
        public short IslemYorum(int piID, eAktifDurum peDurum)
        {
            try
            {
                DbCommand dbCommand = db.GetSqlStringCommand("UPDATE TBLYORUMLAR SET AKTIF=@AKTIF WHERE ID=@ID");
                db.AddInParameter(dbCommand, "ID", DbType.Int16, piID);
                db.AddInParameter(dbCommand, "AKTIF", DbType.Int16, (short)peDurum);
                db.ExecuteNonQuery(dbCommand);
                return 1;
            }
            catch (Exception ex)
            {
                cLog.Write(ex, _iKullaniciID);
                return 0;
            }
        }
        public DataTable ListeleYorum(int piID, int piUID, eAktifDurum? peAktif)
        {
            try
            {
                string strQuery = null;
                if (piUID > 0)
                {
                    strQuery = "URUN_ID=" + piUID + " AND AKTIF=" + (short)peAktif;
                }
                else if (piID > 0)
                {
                    strQuery = "ID=" + piID;
                }
                return db.ExecuteDataSet(CommandType.Text, "SELECT * FROM TBLYORUMLAR" + (strQuery.IsNull() ? "" : " WHERE ") + strQuery).Tables[0];
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
