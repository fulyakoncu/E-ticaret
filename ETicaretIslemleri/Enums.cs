using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace ETicaretIslemleri
{
    /// <summary>
    /// Para Birimleri
    /// </summary>
    public enum eParaBirimi : short
    {
        [Description("TL")]
        TL = 949,
        [Description("$")]
        Dolar = 840,
        [Description("€")]
        Euro = 978
    }

    public enum eStokIslemTipi:byte
    {
        [Description("Giriş")]
        Giris = 1,
        [Description("Çıkış")]
        Cikis = 2
    }

    public enum eKayitBicimi:byte
    {
        [Description("Manuel")]
        Manuel = 1,
        [Description("Otomatik")]
        Otomatik = 2
    }

    public enum eGrupTipi : byte
    {
        [Description("Yeni Ürün Grubu")]
        YeniUrun = 1,
        [Description("Gelecek Ürün Grubu")]
        GelecekUrun = 2,
        [Description("Sabit Kur(Dolar)")]
        SabitKurDolar = 3,
        [Description("Sabit Kur(Euro)")]
        SabitKurEuro = 4,
        [Description("Diğer")]
        Diger=9
    }

    public enum eOdemeTipi : byte
    {
        [Description("Sanal Pos")]
        SanalPos = 1,
        [Description("Havale / EFT")]
        Havale=2,
        [Description("Posta Çeki")]
        PostaCeki = 3,
        [Description("Kapıda Ödeme")]
        Kapida = 4
    }

    public enum eSiparisDurumu : byte
    {
        [Description("Sipariş Onaylanmadı")]
        SiparisOnaylanmadi = 1,
        [Description("Sipariş Hazırlanıyor")]
        Hazirlaniyor = 2,
        [Description("Kargoya Verildi")]
        Kargoda = 3,
        [Description("Teslim Edildi")]
        TeslimEdildi = 4
    }

    /// <summary>
    /// Sanal Pos - İşlem Durumu
    /// </summary>
    public enum eSanalPosIslemDurumu : byte
    {
        [Description("Gerçek")]
        Gercek = 0,
        [Description("Test")]
        Test = 1
    }
    /// <summary>
    /// Sanal Pos - İşlem Tipi
    /// </summary>
    public enum eSanalPosIslemTipi
    {
        [Description("Satış")]
        Auth,
        [Description("Ön Otorizasyon")]
        PreAuth,
        [Description("Son Otorizasyon")]
        PostAuth,
        [Description("İade")]
        Credit,
        [Description("İptal")]
        Void
    }
}
