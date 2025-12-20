using System.ComponentModel.DataAnnotations;

namespace BerberYonetimSistemi.Models
{
    public class Salon
    {
        [Key]
        public int SalonId { get; set; }

        [Display(Name = "Salon Adı")]
        [Required(ErrorMessage = "Salon adı zorunludur.")]
        public string SalonAdi { get; set; }

        [Display(Name = "Şehir")]
        public string? Sehir { get; set; }

        [Display(Name = "İlçe")]
        public string? Ilce { get; set; }

        [Display(Name = "Adres Detayı")]
        public string? Adres { get; set; }

        [Display(Name = "Telefon")]
        public string? Telefon { get; set; }

        // İlişkiler
        // Bir salonda birden fazla antrenör çalışır.
        public ICollection<Antrenor>? Antrenorler { get; set; }
    }
}