using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BerberYonetimSistemi.Data;
using BerberYonetimSistemi.Models;

namespace BerberYonetimSistemi.Controllers
{
    public class RandevuController : Controller
    {
        private readonly BerberDbContext _context;

        public RandevuController(BerberDbContext context)
        {
            _context = context;
        }

        // GET: Randevu (Listeleme)
        public async Task<IActionResult> Index()
        {
            var randevular = _context.Randevular
                .Include(r => r.Salon)
                .Include(r => r.Antrenor)
                .Include(r => r.Hizmet)
                .Include(r => r.Kullanici);
            return View(await randevular.ToListAsync());
        }

        // GET: Randevu/Create (Yeni Randevu Sayfası)
        public IActionResult Create()
        {
            // Dropdownları dolduruyoruz

            // Salonlar
            ViewData["SalonId"] = new SelectList(_context.Salonlar, "SalonId", "SalonAdi");

            // Antrenörler
            ViewData["AntrenorId"] = new SelectList(_context.Antrenorler, "AntrenorId", "AntrenorAdi");

            // Hizmetler
            ViewData["HizmetId"] = new SelectList(_context.Hizmetler, "HizmetId", "HizmetAdi");

            // Kullanıcılar (Senin modelindeki 'KullaniciAdi'nı kullanıyoruz, hata vermesin diye)
            ViewData["KullaniciId"] = new SelectList(_context.Kullanicilar, "KullaniciId", "KullaniciAdi");

            return View();
        }

        // POST: Randevu/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Randevu randevu)
        {
            // ÖNCE HOCAYI BULALIM (Çalışma saatlerini kontrol etmek için lazım)
            var secilenAntrenor = await _context.Antrenorler.FindAsync(randevu.AntrenorId);

            if (secilenAntrenor != null)
            {
                // 1. KURAL: GEÇMİŞE RANDEVU OLMAZ
                // Randevu tarihini ve saatini birleştirip tam zamanı buluyoruz
                var randevuZamani = randevu.Tarih.Date + randevu.Saat;

                if (randevuZamani < DateTime.Now)
                {
                    ModelState.AddModelError("", "Geçmiş bir tarihe veya saate randevu alamazsınız. Lütfen ileri bir tarih seçin.");
                }

                // 2. KURAL: MESAİ SAATLERİ DIŞINA ÇIKILAMAZ
                // Seçilen saat, hocanın başlangıç saatinden küçükse VEYA bitiş saatinden büyük/eşitse hata ver.
                if (randevu.Saat < secilenAntrenor.BaslangicSaati || randevu.Saat >= secilenAntrenor.BitisSaati)
                {
                    ModelState.AddModelError("", $"Seçilen antrenör sadece {secilenAntrenor.BaslangicSaati} ile {secilenAntrenor.BitisSaati} saatleri arasında hizmet vermektedir.");
                }
            }

            // 3. KURAL: ÇAKIŞMA KONTROLÜ (Zaten vardı, koruyoruz)
            if (ModelState.IsValid)
            {
                bool doluMu = await _context.Randevular.AnyAsync(r =>
                    r.AntrenorId == randevu.AntrenorId &&
                    r.Tarih == randevu.Tarih &&
                    r.Saat == randevu.Saat);

                if (doluMu)
                {
                    ModelState.AddModelError("", "Bu antrenör seçilen tarih ve saatte zaten dolu. Lütfen başka bir saat seçin.");
                }
                else
                {
                    _context.Add(randevu);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }

            // Hata varsa formu tekrar doldurup geri gönderiyoruz ki kullanıcı verileri kaybetmesin
            ViewData["SalonId"] = new SelectList(_context.Salonlar, "SalonId", "SalonAdi", randevu.SalonId);
            ViewData["AntrenorId"] = new SelectList(_context.Antrenorler, "AntrenorId", "AntrenorAdi", randevu.AntrenorId);
            ViewData["HizmetId"] = new SelectList(_context.Hizmetler, "HizmetId", "HizmetAdi", randevu.HizmetId);
            ViewData["KullaniciId"] = new SelectList(_context.Kullanicilar, "KullaniciId", "KullaniciAdi", randevu.KullaniciId);

            return View(randevu);
        }

        // GET: Randevu/Edit (Düzenleme Sayfası)
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var randevu = await _context.Randevular.FindAsync(id);
            if (randevu == null) return NotFound();

            ViewData["SalonId"] = new SelectList(_context.Salonlar, "SalonId", "SalonAdi", randevu.SalonId);
            ViewData["AntrenorId"] = new SelectList(_context.Antrenorler, "AntrenorId", "AntrenorAdi", randevu.AntrenorId);
            ViewData["HizmetId"] = new SelectList(_context.Hizmetler, "HizmetId", "HizmetAdi", randevu.HizmetId);
            ViewData["KullaniciId"] = new SelectList(_context.Kullanicilar, "KullaniciId", "KullaniciAdi", randevu.KullaniciId);

            return View(randevu);
        }

        // POST: Randevu/Edit (Güncelleme İşlemi)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Randevu randevu)
        {
            if (id != randevu.RandevuId) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(randevu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["SalonId"] = new SelectList(_context.Salonlar, "SalonId", "SalonAdi", randevu.SalonId);
            ViewData["AntrenorId"] = new SelectList(_context.Antrenorler, "AntrenorId", "AntrenorAdi", randevu.AntrenorId);
            ViewData["HizmetId"] = new SelectList(_context.Hizmetler, "HizmetId", "HizmetAdi", randevu.HizmetId);
            ViewData["KullaniciId"] = new SelectList(_context.Kullanicilar, "KullaniciId", "KullaniciAdi", randevu.KullaniciId);

            return View(randevu);
        }

        // GET: Randevu/Delete (Silme Sayfası)
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var randevu = await _context.Randevular
                .Include(r => r.Salon)
                .Include(r => r.Antrenor)
                .Include(r => r.Hizmet)
                .FirstOrDefaultAsync(m => m.RandevuId == id);

            if (randevu == null) return NotFound();

            return View(randevu);
        }

        // POST: Randevu/Delete (Silme İşlemi)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var randevu = await _context.Randevular.FindAsync(id);
            if (randevu != null)
            {
                _context.Randevular.Remove(randevu);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}