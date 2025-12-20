using Microsoft.AspNetCore.Mvc;
using BerberYonetimSistemi.Services;

namespace BerberYonetimSistemi.Controllers
{
    public class YapayZekaController : Controller
    {
        private readonly GeminiService _geminiService;

        // Dependency Injection ile servisi içeri alıyoruz
        public YapayZekaController(GeminiService geminiService)
        {
            _geminiService = geminiService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(int yas, double kilo, double boy, string cinsiyet, string hedef)
        {
            // Servise git ve sonucu al (Servis artık her zaman dolu dönecek)
            ViewBag.Sonuc = await _geminiService.GetFitnessAdvice(yas, kilo, boy, cinsiyet, hedef);

            // Verileri geri gönder ki formda kalsınlar
            ViewBag.Yas = yas;
            ViewBag.Kilo = kilo;
            ViewBag.Boy = boy;

            return View();
        }
    }
}