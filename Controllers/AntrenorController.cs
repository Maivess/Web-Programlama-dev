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
    public class AntrenorController : Controller
    {
        private readonly BerberDbContext _context;

        public AntrenorController(BerberDbContext context)
        {
            _context = context;
        }

        // GET: Antrenor (Listeleme)
        public async Task<IActionResult> Index()
        {
            // Salon (Berber) bilgisini de çekiyoruz
            var antrenorler = _context.Antrenorler.Include(a => a.Salon);
            return View(await antrenorler.ToListAsync());
        }

        // GET: Antrenor/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var antrenor = await _context.Antrenorler
                .Include(a => a.Salon)
                .Include(a => a.UzmanlikAlanlari) // Uzmanlıkları da görelim
                .FirstOrDefaultAsync(m => m.AntrenorId == id);

            if (antrenor == null) return NotFound();

            return View(antrenor);
        }

        // GET: Antrenor/Create
        public IActionResult Create()
        {
            // Salon seçimi için Dropdown dolduruyoruz (Eski BerberId)
            ViewData["SalonId"] = new SelectList(_context.Salonlar, "SalonId", "SalonAdi");
            return View();
        }

        // POST: Antrenor/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AntrenorId,AntrenorAdi,AntrenorSoyadi,AntrenorTelefon,BaslangicSaati,BitisSaati,SalonId")] Antrenor antrenor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(antrenor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            // Hata olursa Dropdown tekrar dolsun
            ViewData["SalonId"] = new SelectList(_context.Salonlar, "SalonId", "SalonAdi", antrenor.SalonId);
            return View(antrenor);
        }

        // GET: Antrenor/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var antrenor = await _context.Antrenorler.FindAsync(id);
            if (antrenor == null) return NotFound();

            ViewData["SalonId"] = new SelectList(_context.Salonlar, "SalonId", "SalonAdi", antrenor.SalonId);
            return View(antrenor);
        }

        // POST: Antrenor/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AntrenorId,AntrenorAdi,AntrenorSoyadi,AntrenorTelefon,BaslangicSaati,BitisSaati,SalonId")] Antrenor antrenor)
        {
            if (id != antrenor.AntrenorId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(antrenor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AntrenorExists(antrenor.AntrenorId)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["SalonId"] = new SelectList(_context.Salonlar, "SalonId", "SalonAdi", antrenor.SalonId);
            return View(antrenor);
        }

        // GET: Antrenor/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var antrenor = await _context.Antrenorler
                .Include(a => a.Salon)
                .FirstOrDefaultAsync(m => m.AntrenorId == id);

            if (antrenor == null) return NotFound();

            return View(antrenor);
        }

        // POST: Antrenor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var antrenor = await _context.Antrenorler.FindAsync(id);
            if (antrenor != null)
            {
                _context.Antrenorler.Remove(antrenor);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool AntrenorExists(int id)
        {
            return _context.Antrenorler.Any(e => e.AntrenorId == id);
        }
    }
}