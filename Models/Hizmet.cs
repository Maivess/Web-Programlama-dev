using System.ComponentModel.DataAnnotations;

namespace BerberYonetimSistemi.Models
{
    public class Hizmet
    {
        [Key]
        public int HizmetId { get; set; } // Eski IslemId

        [Display(Name = "Hizmet Adı")]
        [Required(ErrorMessage = "Hizmet adı boş bırakılamaz.")]
        public string HizmetAdi { get; set; } // Eski IslemAdi (Örn: PT, Reformer, Yoga)

        [Display(Name = "Ücret (TL)")]
        [Range(0, 100000, ErrorMessage = "Geçerli bir ücret giriniz.")]
        public decimal Ucret { get; set; } // Eski IslemFiyat

        [Display(Name = "Süre (Dakika)")]
        [Range(1, 600, ErrorMessage = "Süre 1 ile 600 dakika arasında olmalıdır.")]
        public int Sure { get; set; } // Yeni özellik: Hizmet süresi

        // İLİŞKİLER

        // Bu hizmet hangi kategoriye ait? (Fitness, Pilates vb.)
        [Display(Name = "Kategori")]
        public int UzmanlikAlaniId { get; set; }
        public UzmanlikAlani? UzmanlikAlani { get; set; }

        // Bu hizmeti hangi antrenörler veriyor?
        public ICollection<Antrenor>? Antrenorler { get; set; } // Eski Calisanlar

        public ICollection<Randevu>? Randevular { get; set; }
    }
}