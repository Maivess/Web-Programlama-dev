using System.ComponentModel.DataAnnotations;

namespace BerberYonetimSistemi.Models
{
    public class UzmanlikAlani
    {
        [Key]
        public int UzmanlikAlaniId { get; set; }

        [Display(Name = "Uzmanlık Alanı")]
        [Required(ErrorMessage = "Uzmanlık alanı adı gereklidir.")]
        public string UzmanlikAlaniAdi { get; set; } // Örn: Fitness, Pilates, Yoga

        // İLİŞKİLER

        // Eski: Calisanlar -> Yeni: Antrenorler
        public ICollection<Antrenor>? Antrenorler { get; set; }

        // Hizmetlerle ilişkisi (Hangi hizmet bu alana giriyor?)
        public ICollection<Hizmet>? Hizmetler { get; set; }
    }
}