using Microsoft.EntityFrameworkCore;
using BerberYonetimSistemi.Models;

namespace BerberYonetimSistemi.Data
{
    public class BerberDbContext : DbContext
    {
        public BerberDbContext(DbContextOptions<BerberDbContext> options) : base(options)
        {
        }

        public DbSet<Salon> Salonlar { get; set; }
        public DbSet<Antrenor> Antrenorler { get; set; }
        public DbSet<Hizmet> Hizmetler { get; set; }
        public DbSet<Kullanici> Kullanicilar { get; set; }
        public DbSet<Randevu> Randevular { get; set; }
        public DbSet<Admin> Admin { get; set; }
        public DbSet<UzmanlikAlani> UzmanlikAlanlari { get; set; }

        

protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 1. DECIMAL UYARISINI ÇÖZME (Ücret alanı için hassasiyet ayarı)
            modelBuilder.Entity<Hizmet>()
                .Property(h => h.Ucret)
                .HasColumnType("decimal(18,2)");

            // --- İLİŞKİLER VE CASCADE AYARLARI ---

            // 2. Randevu - Kullanıcı
            modelBuilder.Entity<Randevu>()
                .HasOne(r => r.Kullanici)
                .WithMany(k => k.Randevular)
                .HasForeignKey(r => r.KullaniciId)
                .OnDelete(DeleteBehavior.Restrict); // Kullanıcı silinirse randevuları silinmesin (Hata vermesin)

            // 3. Randevu - Salon (DÖNGÜYÜ KIRAN NOKTA BURASI!)
            modelBuilder.Entity<Randevu>()
                .HasOne(r => r.Salon)
                .WithMany()
                .HasForeignKey(r => r.SalonId)
                .OnDelete(DeleteBehavior.Restrict); // Salon silinirse randevular otomatik silinmesin, hata versin.

            // 4. Randevu - Antrenör
            modelBuilder.Entity<Randevu>()
                .HasOne(r => r.Antrenor)
                .WithMany()
                .HasForeignKey(r => r.AntrenorId)
                .OnDelete(DeleteBehavior.Restrict); // Antrenör silinirse randevular patlamasın.

            // 5. Randevu - Hizmet
            modelBuilder.Entity<Randevu>()
                .HasOne(r => r.Hizmet)
                .WithMany(h => h.Randevular)
                .HasForeignKey(r => r.HizmetId)
                .OnDelete(DeleteBehavior.Restrict);

            // 6. Antrenör - Salon
            modelBuilder.Entity<Antrenor>()
                .HasOne(c => c.Salon)
                .WithMany(s => s.Antrenorler)
                .HasForeignKey(c => c.SalonId)
                .OnDelete(DeleteBehavior.Cascade); // Salon silinirse hocalar silinsin (Bu mantıklı)

            // 7. Antrenör - Uzmanlık (Many-to-Many)
            modelBuilder.Entity<Antrenor>()
                .HasMany(c => c.UzmanlikAlanlari)
                .WithMany(u => u.Antrenorler)
                .UsingEntity<Dictionary<string, object>>(
                    "AntrenorUzmanlik",
                    x => x.HasOne<UzmanlikAlani>().WithMany().HasForeignKey("UzmanlikAlaniId"),
                    x => x.HasOne<Antrenor>().WithMany().HasForeignKey("AntrenorId")
                );

            // 8. Antrenör - Hizmet (Many-to-Many)
            modelBuilder.Entity<Antrenor>()
                .HasMany(c => c.Hizmetler)
                .WithMany(h => h.Antrenorler)
                .UsingEntity<Dictionary<string, object>>(
                    "AntrenorHizmet",
                    x => x.HasOne<Hizmet>().WithMany().HasForeignKey("HizmetId"),
                    x => x.HasOne<Antrenor>().WithMany().HasForeignKey("AntrenorId")
                );

            // 9. Hizmet - Kategori (Uzmanlık Alanı)
            modelBuilder.Entity<Hizmet>()
                .HasOne(h => h.UzmanlikAlani)
                .WithMany(u => u.Hizmetler)
                .HasForeignKey(h => h.UzmanlikAlaniId)
                .OnDelete(DeleteBehavior.Restrict);


        }
    }
}