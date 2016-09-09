using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LogSistemi
{
    public struct TBLLOGS
    {
        public Int64 ID { get; internal set; }
        /// <summary>
        /// Giriş yapmış bir üye tarafından hata düşmüş ise üyenin ID si, Giriş Yapmamış biri tarafından düşer ise 0 yazacak.
        /// </summary>
        public int KULLANICI_ID { get; set; }
        /// <summary>
        /// Hata Mesajı
        /// </summary>
        public string MESAJ { get; set; }
        /// <summary>
        /// Hata Kaynağı
        /// </summary>
        public string KAYNAGI { get; set; }
        /// <summary>
        /// Hata düşüren kullanıcının IP si
        /// </summary>
        public string IP { get; internal set; }
        /// <summary>
        /// Hatanın düştüğü sayfa, Web servislerden hata oluşmuş ise Web servis yazmalı
        /// </summary>
        public string PAGE { get; set; }
        /// <summary>
        /// Hatanın tüm mesajı
        /// </summary>
        public string STACKTRACE { get; set; }
        /// <summary>
        /// Bu alan tabloda olmayacak, Hatanın Sayfalardan Web servisden mi geldiği kontrol edilecek
        /// </summary>
        public bool IsWeb { get; set; }
        /// <summary>
        /// Hatanın oluşma zamanı
        /// </summary>
        public DateTime OLUSMA_ZAMANI { get; set; }
    }
}
