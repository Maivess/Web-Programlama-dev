using System.ComponentModel.DataAnnotations;

namespace BerberYonetimSistemi.Models
{
    public class Randevu
    {
        [Key]
        public int RandevuId { get; set; }

        [Display(Name = "Tarih")]
        [DataType(DataType.Date)]
        public DateTime Tarih { get; set; }

        [Display(Name = "Saat")]
        [DataType(DataType.Time)]
        public TimeSpan Saat { get; set; }

        public bool OnaylandiMi { get; set; }

        // --- İLİŞKİLER (GÜNCELLENMİŞ) ---

       
        public int SalonId { get; set; }
        public Salon? Salon { get; set; }

        
        public int AntrenorId { get; set; }
        public Antrenor? Antrenor { get; set; }

        
        public int HizmetId { get; set; }
        public Hizmet? Hizmet { get; set; }

       
        public int KullaniciId { get; set; }
        public Kullanici? Kullanici { get; set; }
    }
}