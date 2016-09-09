using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LogSistemi;
using System.Web.UI;
using MiniCore;
using System.Xml;
using System.Data;

namespace ETicaret
{
    public enum eStatusType : short
    {
        Onay = 1,
        Bilgi = 2,
        Uyari = 3,
        Hata = 4
    }

    public class cUIAraclari
    {
        public static int _iKullaniciID
        {
            get
            {
                if (HttpContext.Current.Session["USERID"] == null)
                {
                    return 0;
                }
                else
                    return HttpContext.Current.Session["USERID"].ToInt(0);
            }
            set
            {
                HttpContext.Current.Session["USERID"] = value;
            }
        }
        public static eKullaniciTipi _iKullaniciTipi
        {
            get
            {
                if (HttpContext.Current.Session["KULLANICITIPI"] == null)
                {
                    return eKullaniciTipi.Misafir;
                }
                //9 olmasının sebebi misafir kullanıcı atıyoruz
                else
                    return (eKullaniciTipi)HttpContext.Current.Session["KULLANICITIPI"].ToShort(9);
            }
            set
            {
                HttpContext.Current.Session["KULLANICITIPI"] = value;
            }
        }
        public static DataTable _dtSepet
        {
            get
            {
                if (HttpContext.Current == null || HttpContext.Current.Session["KULLANICISEPETI"] == null)
                    return null;
                return (DataTable)HttpContext.Current.Session["KULLANICISEPETI"];
            }
            set
            {
                HttpContext.Current.Session["KULLANICISEPETI"] = value;
            }
        }
        public static cLogging cLog;
        public static void toastMesaj(Control pctrlControl, eStatusType peUyariTuru, string pstrMesaj)
        {
            try
            {
                string strTur = "";
                switch (peUyariTuru)
                {
                    case eStatusType.Onay:
                        strTur = "showSuccessToast";
                        break;
                    case eStatusType.Bilgi:
                        strTur = "showNoticeToast";
                        break;
                    case eStatusType.Uyari:
                        strTur = "showWarningToast";
                        break;
                    case eStatusType.Hata:
                        strTur = "showErrorToast";
                        break;
                    default:
                        break;
                }
                ScriptManager.RegisterStartupScript(pctrlControl, pctrlControl.GetType(), "ShowMessage", "$().toastmessage('" + strTur + "', '" + pstrMesaj + "');", true);
            }
            catch
            {
                throw;
            }
        }
        public static decimal KurDegeri(ETicaretIslemleri.eParaBirimi peParaBirimi)
        {
            try
            {
                XmlTextReader okuyucu = new XmlTextReader("http://www.tcmb.gov.tr/kurlar/today.xml"); // xml imizin yolu...
                XmlDocument dokuman = new XmlDocument();
                dokuman.Load(okuyucu);  // okuyucu değişkenimdeki xml'in yolunu oku...

                XmlNode dolar = dokuman.SelectSingleNode("/Tarih_Date/Currency[CurrencyName='US DOLLAR']");
                XmlNode euro = dokuman.SelectSingleNode("/Tarih_Date/Currency[CurrencyName='EURO']");
                //euro_alis = double.Parse(euro.ChildNodes[3].InnerText, new System.Globalization.CultureInfo("en-US"));
                //dolar_alis = double.Parse(dolar.ChildNodes[3].InnerText, new System.Globalization.CultureInfo("en-US"));;
                System.Globalization.NumberFormatInfo nfi = new System.Globalization.CultureInfo( "en-US", false ).NumberFormat;
                nfi.CurrencyDecimalDigits=2;
                if (peParaBirimi == ETicaretIslemleri.eParaBirimi.Dolar)
                return Decimal.Parse(dolar.ChildNodes[4].InnerText, new System.Globalization.CultureInfo("en-US"));
                else if (peParaBirimi == ETicaretIslemleri.eParaBirimi.Euro)
                    return Decimal.Parse(euro.ChildNodes[4].InnerText, new System.Globalization.CultureInfo("en-US"));
                else
                    return 1;

            }
            catch (Exception ex)
            {
                cLog.Write(ex, _iKullaniciID);
                return 1;
            }
        }
    }


}