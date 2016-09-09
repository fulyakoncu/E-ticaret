using System;
using System.Data.Common;
using MiniCore;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Web;
using System.Data;
using LogSistemi;

namespace ETicaretIslemleri
{
    public class cUrunListeleri:IDisposable
    {
        private Database db;
        private cLogging cLog;
        private int _iKullaniciID = HttpContext.Current.Session["USERID"].ToInt(0);

        public cUrunListeleri()
        {
            cLog = new cLogging(false);
            db = DatabaseFactory.CreateDatabase();
        }

        public void Dispose()
        {
            cLog.Dispose();
            GC.SuppressFinalize(this);
        }

        public DataTable ListeSliderUrunleri()
        {
            try
            {
                return db.ExecuteDataSet(CommandType.Text, "SELECT U.ID,U.MODEL,(SELECT R.RESIMADI FROM TBLURUN_RESIMLERI R WHERE R.URUNID=U.ID AND R.SLIDERRESIM="+(short)eEvetHayir.Evet+")RESIM_ADI from TBLURUNLER U WHERE U.SLIDER="+(short)eEvetHayir.Evet+" AND U.AKTIF="+(short)eAktifDurum.Aktif).Tables[0];
            }
            catch (Exception ex)
            {
                cLog.Write(ex, _iKullaniciID);
                return null;
            }
        }

        public DataTable ListeGrupTipiUrunleri(eGrupTipi peGrupTipleri,int piGosterimSayisi)
        {
            try
            {
                return db.ExecuteDataSet(CommandType.Text, "SELECT "+(piGosterimSayisi==0?"":" TOP "+piGosterimSayisi)+" *,(SELECT M.ADI FROM TBLMARKALAR M WHERE M.ID=U.MARKA)MARKA_ADI,(SELECT RESIMADI FROM TBLURUN_RESIMLERI R WHERE R.URUNID=U.ID AND R.ANARESIM=" + (short)eEvetHayir.Evet + ")ANARESIM  from TBLURUNLER U WHERE U.AKTIF=" + (short)eAktifDurum.Aktif + " AND U.ID IN(SELECT URUNID FROM TBLURUN_GRUPLARI G WHERE G.GRUPID IN(SELECT ID FROM TBLURUN_GRUP_TANIMLARI WHERE GRUP_TIPI=" + (short)peGrupTipleri + ")) ORDER BY U.ID DESC").Tables[0];
            }
            catch (Exception ex)
            {
                cLog.Write(ex, _iKullaniciID);
                return null;
            } 
        }

        public DataTable ListeAramaSonuclari(string pstrArananKelime)
        {
            try
            {
                DbCommand dbcommand = db.GetSqlStringCommand("SELECT * FROM TBLURUNLER WHERE MODEL LIKE'%'+@ARANAN+'%' OR MARKA IN(SELECT ID FROM TBLMARKALAR M WHERE M.ADI LIKE'%'+@ARANAN+'%')");
                db.AddInParameter(dbcommand, "ARANAN", DbType.String, pstrArananKelime);
                return db.ExecuteDataSet(dbcommand).Tables[0];
            }
            catch (Exception ex)
            {
                cLog.Write(ex, _iKullaniciID);
                return null;
            }   
        }
        
    }
}
