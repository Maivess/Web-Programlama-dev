using System.ComponentModel.DataAnnotations;

namespace BerberYonetimSistemi.Models
{
    public class Antrenor
    {
        [Key]
        public int AntrenorId { get; set; } // Eski CalisanId

        [Display(Name = "Adı")]
        [Required(ErrorMessage = "Ad alanı zorunludur.")]
        public string AntrenorAdi { get; set; }

        [Display(Name = "Soyadı")]
        [Required(ErrorMessage = "Soyad alanı zorunludur.")]
        public string AntrenorSoyadi { get; set; }

        [Display(Name = "Telefon")]
        public string? AntrenorTelefon { get; set; }

        [Display(Name = "Başlangıç Saati")]
        [DataType(DataType.Time)]
        public TimeSpan BaslangicSaati { get; set; } // İsimleri kısalttım

        [Display(Name = "Bitiş Saati")]
        [DataType(DataType.Time)]
        public TimeSpan BitisSaati { get; set; }

        // --- İLİŞKİLER ---

        // Hangi Salonda çalışıyor?
        public int SalonId { get; set; } // Eski BerberId
        public Salon? Salon { get; set; }

        // Hangi Kullanıcıya bağlı? (Login için)
        public int? KullaniciId { get; set; }
        public Kullanici? Kullanici { get; set; }

        // Hangi Uzmanlıkları var? (Fitness, Pilates vb.)
        public ICollection<UzmanlikAlani>? UzmanlikAlanlari { get; set; }

        // Hangi Dersleri verebiliyor? (Many-to-Many)
        // Eski "Islemler" yerine "Hizmetler"
        public ICollection<Hizmet>? Hizmetler { get; set; }
    }
}