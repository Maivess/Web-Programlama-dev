using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BerberYonetimSistemi.Data;
using BerberYonetimSistemi.Models;

namespace BerberYonetimSistemi.Controllers
{
    public class HizmetController : Controller
    {
        private readonly BerberDbContext _context;

        public HizmetController(BerberDbContext context)
        {
            _context = context;
        }

        // GET: Hizmet
        public async Task<IActionResult> Index()
        {
            var hizmetler = _context.Hizmetler.Include(h => h.UzmanlikAlani);
            return View(await hizmetler.ToListAsync());
        }

        // GET: Hizmet/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var hizmet = await _context.Hizmetler
                .Include(h => h.UzmanlikAlani)
                .FirstOrDefaultAsync(m => m.HizmetId == id);

            if (hizmet == null) return NotFound();

            return View(hizmet);
        }

        // GET: Hizmet/Create
        public IActionResult Create()
        {
            if (!_context.UzmanlikAlanlari.Any())
            {
                // Eğer kategori yoksa hata vermesin diye boş liste gönderiyoruz
                ViewData["UzmanlikAlaniId"] = new SelectList(new List<UzmanlikAlani>(), "UzmanlikAlaniId", "UzmanlikAlaniAdi");
                TempData["Hata"] = "Önce Uzmanlık Alanı/Kategori eklemelisiniz!";
            }
            else
            {
                ViewData["UzmanlikAlaniId"] = new SelectList(_context.UzmanlikAlanlari, "UzmanlikAlaniId", "UzmanlikAlaniAdi");
            }
            return View();
        }

        // POST: Hizmet/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HizmetId,HizmetAdi,Ucret,Sure,UzmanlikAlaniId")] Hizmet hizmet)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hizmet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UzmanlikAlaniId"] = new SelectList(_context.UzmanlikAlanlari, "UzmanlikAlaniId", "UzmanlikAlaniAdi", hizmet.UzmanlikAlaniId);
            return View(hizmet);
        }

        // GET: Hizmet/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var hizmet = await _context.Hizmetler.FindAsync(id);
            if (hizmet == null) return NotFound();

            ViewData["UzmanlikAlaniId"] = new SelectList(_context.UzmanlikAlanlari, "UzmanlikAlaniId", "UzmanlikAlaniAdi", hizmet.UzmanlikAlaniId);
            return View(hizmet);
        }

        // POST: Hizmet/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HizmetId,HizmetAdi,Ucret,Sure,UzmanlikAlaniId")] Hizmet hizmet)
        {
            if (id != hizmet.HizmetId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hizmet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HizmetExists(hizmet.HizmetId)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["UzmanlikAlaniId"] = new SelectList(_context.UzmanlikAlanlari, "UzmanlikAlaniId", "UzmanlikAlaniAdi", hizmet.UzmanlikAlaniId);
            return View(hizmet);
        }

        // GET: Hizmet/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var hizmet = await _context.Hizmetler
                .Include(h => h.UzmanlikAlani)
                .FirstOrDefaultAsync(m => m.HizmetId == id);

            if (hizmet == null) return NotFound();

            return View(hizmet);
        }

        // POST: Hizmet/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hizmet = await _context.Hizmetler.FindAsync(id);
            if (hizmet != null)
            {
                _context.Hizmetler.Remove(hizmet);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool HizmetExists(int id)
        {
            return _context.Hizmetler.Any(e => e.HizmetId == id);
        }
    }
}