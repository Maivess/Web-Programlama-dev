using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BerberYonetimSistemi.Models;
using BerberYonetimSistemi.Data;

namespace BerberYonetimSistemi.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BerberDbContext _context;

        public HomeController(ILogger<HomeController> logger, BerberDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Eski: _context.Berberler
            // Yeni: _context.Salonlar
            var salonlar = await _context.Salonlar.ToListAsync();
            return View(salonlar);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult SacOnerisi() // Bunu yapay zeka entegrasyonu için "EgzersizOnerisi" olarak kullanacağız ilerde
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}