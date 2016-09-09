using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace MiniCore
{
    /// <summary>
    /// Cinsiyet Tip bilgisi
    /// </summary>
    public enum eCinsiyet : byte
    {
        [Description("Erkek")]
        Erkek = 1,
        [Description("Bayan")]
        Bayan = 2
    }
    /// <summary>
    /// Anahtar alanlar için Evet/Hayır Durumu tutan enumdur.
    /// </summary>
    public enum eEvetHayir : byte
    {
        [Description("Evet")]
        Evet = 1,
        [Description("Hayır")]
        Hayir = 0
    }
    /// <summary>
    /// Bu tip Durum Bilgisi Tip tanımı için Kullanılır.
    /// </summary>
    public enum eAktifDurum : byte
    {
        [Description("Aktif")]
        Aktif = 1,
        [Description("Pasif")]
        Pasif = 0,
    }
    /// <summary>
    /// Var Yok durum bilgisini tutan Enum
    /// </summary>
    public enum eVarYok
    {
        [Description("Var")]
        Var = 1,
        [Description("Yok")]
        Yok = 0
    }
    /// <summary>
    /// Sistemdeki kullanıcı tipleri
    /// </summary>
    public enum eKullaniciTipi : byte
    {
        [Description("Yönetici")]
        Yonetici = 1,
        [Description("Üye")]
        Uye = 2,
        [Description("Misafir")]
        Misafir=9
    }
}
