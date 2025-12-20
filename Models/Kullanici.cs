using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BerberYonetimSistemi.Models
{
    public class Kullanici
    {
        [Key]
        public int KullaniciId { get; set; }
        public string? KullaniciAdi { get; set; }
        public string? KullaniciSoyadi { get; set; }
        public string? KullaniciTelefon { get; set; }
        public string? KullaniciSifre { get; set; }
        public string? KullaniciSifreTekrar { get; set; }

        public Rol? KullaniciRolu { get; set; } // Enum türü burada kullanılıyor

        public bool IsApproved { get; set; }
        public bool IsAdmin { get; set; }

        public ICollection<Antrenor>? Calisanlar { get; set; }
        public ICollection<Randevu>? Randevular { get; set; }


        [NotMapped]
        public string AdSoyad
        {
            get
            {
                return KullaniciAdi + " " + KullaniciSoyadi;
            }
        }


    }


}
