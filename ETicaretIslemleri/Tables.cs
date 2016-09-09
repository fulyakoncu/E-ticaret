using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MiniCore;
namespace ETicaretIslemleri
{
    /// <summary>
    /// şehir bilgilerinin tutulduğu tablo
    /// </summary>
    public struct TBLSEHIR
    {
        public int ID { get; set; }
        /// <summary>
        /// şehir plaka kodu, Listelerken KODU alanına göre sıralanacak
        /// </summary> 
        public int KODU { get; set; }
        /// <summary>
        /// şehir adı
        /// </summary>
        public string ADI { get; set; }
        /// <summary>
        /// birden fazla ülkeyi desteklemek için yapılmıştır. Ülkeler tablosu eklenebilme ihtimaline karşı esneklik için eklenmiştir.
        /// </summary>
        public short ULKEID { get; set; }
    }
    /// <summary>
    /// Üye bilgilerinin tutulduğu tablo
    /// </summary>
    public struct TBLUYELER
    {
        public int ID { get; set; }
        public string ADI { get; set; }
        public string SOYADI { get; set; }
        public string EMAIL { get; set; }
        public string SIFRE { get; set; }
        public DateTime DOGUM_TARIHI { get; set; }
        public eCinsiyet CINSIYET { get; set; }
        public string ADRES { get; set; }
        public int SEHIR_ID { get; set; }
        public string CEPTELEFONU { get; set; }
        public string EVTELEFONU { get; set; }
        /// <summary>
        /// şifre değiştirme vb işlemler için kriptolu alan
        /// </summary>
        public string GUID { get; set; }
        public eKullaniciTipi KULLANICI_TIPI { get; set; }
        public DateTime OLUSMA_ZAMANI { get; set; }
    }
    /// <summary>
    /// Kurum bilgileri tutulur. fatura,mail gönderme vb işlemlerde kullanılacaktır.
    /// </summary>
    public struct TBLKURUM_PARAMETRELERI
    {
        public short ID { get; set; }
        public string KURUM_ADI { get; set; }
        public string ADRES { get; set; }
        public string TELEFON { get; set; }
        public string FAX { get; set; }
        public string VERGI_DAIRESI { get; set; }
        public string VERGI_NO { get; set; }
        /// <summary>
        /// Ticaret Sicil Numarası
        /// </summary>
        public string TIC_SICIL_NO { get; set; }
        /// <summary>
        /// Mail göndermek için SMTP adresi
        /// </summary>
        public string SMTP_ADRES { get; set; }
        /// <summary>
        /// Mail SMTP Port numarası
        /// </summary>
        public int SMTP_PORT { get; set; }
        /// <summary>
        /// Mail Adresi
        /// </summary>
        public string MAIL { get; set; }
        /// <summary>
        /// Mail Adresinin şifresi, Mail Göndermek için Kullanılacak
        /// </summary>
        public string MAILSIFRE { get; set; }
        /// <summary>
        /// Tanımlama Aktif mi? Bu tabloda sadece 1 kayıt Aktif Olmalı
        /// </summary>
        public eAktifDurum AKTIF { get; set; }
    }
    /// <summary>
    /// Öneri Şikayet İstek formu için
    /// </summary>
    public struct TBLBILDIRIMLER
    {
        public int ID { get; set; }
        /// <summary>
        /// bildirimi gönderen kişinin adı soyadı
        /// </summary>
        public string ADI_SOYADI { get; set; }
        public string EMAIL { get; set; }
        /// <summary>
        /// Bildirimin başlığı
        /// </summary>
        public string BASLIK { get; set; }
        public string ICERIK { get; set; }
        /// <summary>
        /// Bildirimi gönderen kişinin IP adresi
        /// </summary>
        public string USERIP { get; set; }
        /// <summary>
        /// Bildirimin Gönderim zamanı
        /// </summary>
        public DateTime OLUSMA_ZAMANI { get; set; }
    }
    /// <summary>
    /// Kargo firmalarının bilgileri
    /// </summary>
    public struct TBLKARGOLAR
    {
        public short ID { get; set; }
        public string ADI { get; set; }
        public string TELEFON { get; set; }
        public string EMAIL { get; set; }
        public eAktifDurum AKTIF { get; set; }
    }
    /// <summary>
    /// Ürünler için marka tanımlası yapılır
    /// </summary>
    public struct TBLMARKALAR
    {
        public int ID { get; set; }
        public string KODU { get; set; }
        public string ADI { get; set; }
        public string LOGO { get; set; }
        /// <summary>
        /// Marka ile ilgili kampanya, hediye çeki garanti vb açıklama varsa belirtilir.
        /// </summary>
        public string ACIKLAMA { get; set; }
    }
    /// <summary>
    /// kategorilerin tutulduğu tablo, tree şeklinde bir tablodur
    /// </summary>
    public struct TBLKATEGORILER
    {
        public short ID { get; set; }
        public short NODEID { get; set; }
        public string ADI { get; set; }
        public short SIRANO { get; set; }
        /// <summary>
        /// Tasarımda kullanılmayacak ancak tasarım değişikliği ihitmaline karşı eklenmiştir.
        /// </summary>
        public string KATEGORI_RESMI { get; set; }
        public eAktifDurum AKTIF { get; set; }
    }
    /// <summary>
    /// rapor tanımları
    /// </summary>
    public struct TBLRAPOR
    {
        public int ID { get; set; }
        /// <summary>
        /// Rapor Kod Adı
        /// </summary>
        public string AD { get; set; }
        /// <summary>
        /// Rapor Dosya Adı
        /// </summary>
        public string YOL { get; set; }
    }
    /// <summary>
    /// ürün tanımları
    /// </summary>
    public struct TBLURUNLER
    {
        public int ID { get; set; }
        public string KODU { get; set; }
        /// <summary>
        /// TBLMARKALAR ile ilişkili
        /// </summary>
        public short MARKA { get; set; }
        public string MODEL { get; set; }
        /// <summary>
        /// TBLKATEGORILER ile ilişkili
        /// </summary>
        public short KATEGORI { get; set; }
        public decimal FIYATI { get; set; }
        /// <summary>
        /// DOLAR,EURO,TL
        /// </summary>
        public eParaBirimi PARABIRIMI { get; set; }
        /// <summary>
        /// KAMPANYALI ÜRÜN MÜ?
        /// </summary>
        public eEvetHayir KAMPANYALI { get; set; }
        /// <summary>
        /// KAMPANYALI VB ACIKLAMA
        /// </summary>
        public string ACIKLAMA { get; set; }
        /// <summary>
        /// SLİDERDA GÖSTERİLSİNMİ
        /// </summary>
        public eEvetHayir SLIDER { get; set; }
        /// <summary>
        /// INDIRIM VAR MI
        /// </summary>
        public eEvetHayir INDIRIM { get; set; }
        /// <summary>
        /// INDIRIM VAR ISE INDIRIMLI FİYATI
        /// </summary>
        public decimal INDIRIMLI_FIYAT { get; set; }
        public eAktifDurum AKTIF { get; set; }
    }
    /// <summary>
    /// ÜRÜNLERİN ÖZELLİK TANIMLARI
    /// BIRIM İFADESİ ÖZELLİĞİN SONUNA EKLENİR
    /// </summary>
    public struct TBLURUN_OZELLIK_TANIMLARI
    {
        public int ID { get; set; }
        public string OZELLIK_ADI { get; set; }
        public string BIRIMI { get; set; }
    }
    /// <summary>
    /// ÜRÜNLERİ KOLAYCA GRUPLAMAK ICIN YAZILMISTIR.
    /// </summary>
    public struct TBLURUN_GRUP_TANIMLARI
    {
        public int ID { get; set; }
        public string GRUP_ADI { get; set; }
        //Grup Tipi Sabit Kur ise Aciklama kısmına kur değeri yazılacak.
        public string ACIKLAMA { get; set; }
        public eGrupTipi GRUP_TIPI { get; set; }
    }
    /// <summary>
    /// Banka bilgilerinin tutulduğu tablo
    /// </summary>
    public struct TBLBANKALAR
    {
        public int ID { get; set; }
        public string BANKA_ADI { get; set; }
        public string KULLANICI_ADI { get; set; }
        public string SIFRE { get; set; }
        public string HOST { get; set; }
        public string MAGAZA_NO { get; set; }
        public eAktifDurum AKTIF { get; set; }
        public eAktifDurum TAKSIT { get; set; }
    }
    /// <summary>
    /// Taksit bilgilerinin tutulduğu tablo
    /// </summary>
    public struct TBLTAKSITLER
    {
        public int ID { get; set; }
        public int BANKA_ID { get; set; }
        public int TAKSIT { get; set; }
        public decimal ORAN { get; set; }
    }
    /// <summary>
    /// ürünlerin-özellikleri ile ilişkilendirilmesi
    /// </summary>
    public struct TBLURUN_OZELLIKLERI
    {
        public int ID { get; set; }
        public int URUNID { get; set; }
        public int OZELLIKID { get; set; }
        public string DEGER { get; set; }
    }
    /// <summary>
    /// ürünün gruplar ile eşleştirilmesi
    /// </summary>
    public struct TBLURUN_GRUPLARI
    {
        public int ID { get; set; }
        public int URUNID { get; set; }
        public int GRUPID { get; set; }
    }
    /// <summary>
    /// ürünlerin resim kaydını tutar,
    /// Resimler Upload/Urun_Resimleri/[Urun ID]/
    /// altında tutulacak.
    /// </summary>
    public struct TBLURUN_RESIMLERI
    {
        public int ID { get; set; }
        public int URUNID { get; set; }
        public string RESIMADI { get; set; }
        public eEvetHayir ANARESIM { get; set; }
        public eEvetHayir SLIDERRESIM { get; set; }
    }
    /// <summary>
    /// Uygulamamızın stok kontrol işlemleri, stok giriş-çıkışı tutulur
    /// 
    /// </summary>
    public struct TBLSTOKISLEMLERI
    {
        public long ID { get; set; }
        public int URUNID { get; set; }
        public decimal MIKTAR { get; set; }
        public eStokIslemTipi ISLEMTIPI { get; set; }
        /// <summary>
        /// Çıkış işlemlerinde SIPARIS DETAY ID tutulacak
        /// </summary>
        public long REFERANSID { get; set; }
        public DateTime OLUSMA_ZAMANI { get; set; }
    }
    /// <summary>
    /// Sipariş Bilgileri tutuluyor, 
    /// </summary>
    public struct TBLSIPARIS
    {
        public long ID { get; set; }
        public int UYEID { get; set; }
        /// <summary>
        /// fatura üzerine yazılacak ad
        /// </summary>
        public string FATURA_ADI { get; set; }
        public string FATURA_VERGINO { get; set; }
        public decimal TUTAR { get; set; }
        public string ADRES { get; set; }
        /// <summary>
        /// Siparişin verildiği kargo
        /// </summary>
        public short KARGOID { get; set; }
        /// <summary>
        /// Kargo tarafından verilen kargo kodu
        /// </summary>
        public string KARGOKODU { get; set; }
        public int BANKAID { get; set; }
        public string SPOSSONUC { get; set; }
        public eOdemeTipi ODEMETIPI { get; set; }
        public eSiparisDurumu SIPARISDURUMU { get; set; }
        public DateTime OLUSMA_ZAMANI { get; set; }
        public int DEGISTIREN { get; set; }
        public DateTime DEGISME_ZAMANI { get; set; }
        public string GUID { get; set; }
    }
    /// <summary>
    /// Siparişlerin detayları tutuluyor
    /// </summary>
    public struct TBLSIPARISDETAY
    {
        public long ID { get; set; }
        public long SIPARISID { get; set; }
        public int URUNID { get; set; }
        public decimal MIKTAR { get; set; }
        public decimal TUTAR { get; set; }
    }
    /// <summary>
    /// Yorumlar tablosu
    /// </summary>
    public struct TBLYORUMLAR
    {
        public int ID { get; set; }
        public int URUN_ID { get; set; }
        public int UYE_ID { get; set; }
        public string BASLIK { get; set; }
        public string MESAJ { get; set; }
        public DateTime TARIH { get; set; }
        public string IP { get; set; }
        public eAktifDurum AKTIF { get; set; }
    }
    /// <summary>
    /// Üyelerin Ürün listesini tutan tablo
    /// </summary>
    public struct TBLUYE_URUNLISTESI
    {
        public long ID { get; set; }
        public int UYEID { get; set; }
        public int URUNID { get; set; }
    }
}