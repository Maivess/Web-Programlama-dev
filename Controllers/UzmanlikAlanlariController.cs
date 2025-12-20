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
    public class UzmanlikAlanlariController : Controller
    {
        private readonly BerberDbContext _context;

        public UzmanlikAlanlariController(BerberDbContext context)
        {
            _context = context;
        }

        // GET: UzmanlikAlanlari
        public async Task<IActionResult> Index()
        {
            // BURASI DÜZELDİ: Islemler -> Hizmetler
            return View(await _context.UzmanlikAlanlari.Include(u => u.Hizmetler).ToListAsync());
        }

        // GET: UzmanlikAlanlari/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var uzmanlikAlani = await _context.UzmanlikAlanlari
                .Include(u => u.Hizmetler) // BURASI DÜZELDİ: Islemler -> Hizmetler
                .FirstOrDefaultAsync(m => m.UzmanlikAlaniId == id);

            if (uzmanlikAlani == null) return NotFound();

            return View(uzmanlikAlani);
        }

        // GET: UzmanlikAlanlari/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UzmanlikAlanlari/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UzmanlikAlaniId,UzmanlikAlaniAdi")] UzmanlikAlani uzmanlikAlani)
        {
            if (ModelState.IsValid)
            {
                _context.Add(uzmanlikAlani);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(uzmanlikAlani);
        }

        // GET: UzmanlikAlanlari/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var uzmanlikAlani = await _context.UzmanlikAlanlari.FindAsync(id);
            if (uzmanlikAlani == null) return NotFound();
            return View(uzmanlikAlani);
        }

        // POST: UzmanlikAlanlari/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UzmanlikAlaniId,UzmanlikAlaniAdi")] UzmanlikAlani uzmanlikAlani)
        {
            if (id != uzmanlikAlani.UzmanlikAlaniId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(uzmanlikAlani);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UzmanlikAlaniExists(uzmanlikAlani.UzmanlikAlaniId)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(uzmanlikAlani);
        }

        // GET: UzmanlikAlanlari/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var uzmanlikAlani = await _context.UzmanlikAlanlari
                .FirstOrDefaultAsync(m => m.UzmanlikAlaniId == id);

            if (uzmanlikAlani == null) return NotFound();

            return View(uzmanlikAlani);
        }

        // POST: UzmanlikAlanlari/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var uzmanlikAlani = await _context.UzmanlikAlanlari.FindAsync(id);
            if (uzmanlikAlani != null)
            {
                _context.UzmanlikAlanlari.Remove(uzmanlikAlani);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool UzmanlikAlaniExists(int id)
        {
            return _context.UzmanlikAlanlari.Any(e => e.UzmanlikAlaniId == id);
        }
    }
}