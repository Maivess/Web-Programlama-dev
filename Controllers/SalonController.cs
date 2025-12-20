using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BerberYonetimSistemi.Data;
using BerberYonetimSistemi.Models;

namespace BerberYonetimSistemi.Controllers
{
    public class SalonController : Controller
    {
        private readonly BerberDbContext _context;

        public SalonController(BerberDbContext context)
        {
            _context = context;
        }

        // GET: Salon (Listeleme)
        public async Task<IActionResult> Index()
        {
            return View(await _context.Salonlar.ToListAsync());
        }

        // GET: Salon/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var salon = await _context.Salonlar
                .Include(s => s.Antrenorler) // <--- İŞTE BU EKSİKTİ! Hocaları da getir diyoruz.
                .FirstOrDefaultAsync(m => m.SalonId == id);

            if (salon == null) return NotFound();

            return View(salon);
        }

        // GET: Salon/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Salon/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SalonId,SalonAdi,Sehir,Ilce,Adres,Telefon")] Salon salon)
        {
            if (ModelState.IsValid)
            {
                _context.Add(salon);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(salon);
        }

        // GET: Salon/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var salon = await _context.Salonlar.FindAsync(id);
            if (salon == null) return NotFound();
            return View(salon);
        }

        // POST: Salon/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SalonId,SalonAdi,Sehir,Ilce,Adres,Telefon")] Salon salon)
        {
            if (id != salon.SalonId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(salon);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalonExists(salon.SalonId)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(salon);
        }

        // GET: Salon/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var salon = await _context.Salonlar
                .FirstOrDefaultAsync(m => m.SalonId == id);

            if (salon == null) return NotFound();

            return View(salon);
        }

        // POST: Salon/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var salon = await _context.Salonlar.FindAsync(id);
            if (salon != null)
            {
                _context.Salonlar.Remove(salon);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool SalonExists(int id)
        {
            return _context.Salonlar.Any(e => e.SalonId == id);
        }
    }
}