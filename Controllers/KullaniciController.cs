using Microsoft.AspNetCore.Mvc;
using BerberYonetimSistemi.Data;
using BerberYonetimSistemi.Models;
using Microsoft.AspNetCore.Http; // Session işlemleri için
using System.Linq; // LINQ sorguları için
using System.Collections.Generic;

namespace BerberYonetimSistemi.Controllers
{
    public class KullaniciController : Controller // BaseController varsa onu da yazabilirsin, yoksa Controller kalsın
    {
        private readonly BerberDbContext _context;

        public KullaniciController(BerberDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // Session'dan admin bilgisini kontrol et
            var isAdmin = HttpContext.Session.GetString("IsAdmin");

            if (!string.IsNullOrEmpty(isAdmin) && isAdmin == "true")
            {
                ViewBag.IsAdmin = true;
            }
            else
            {
                ViewBag.IsAdmin = false;
            }

            return View();
        }

        // Giriş Yapma Sayfası (GET)
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // Giriş Yapma İşlemi (POST)
        [HttpPost]
        public IActionResult Login(string kullaniciAdi, string KullaniciSifre)
        {
            // 1. Veritabanından kullanıcıyı bul
            var kullanici = _context.Kullanicilar
                                    .FirstOrDefault(k => k.KullaniciAdi == kullaniciAdi && k.KullaniciSifre == KullaniciSifre);

            if (kullanici != null)
            {
                // 2. Hesap Onaylı mı Kontrolü (Admin onayı yoksa giremez)
                if (!kullanici.IsApproved)
                {
                    ViewBag.ErrorMessage = "Hesabınız henüz onaylanmamış. Lütfen yönetici onayı bekleyin.";
                    return View();
                }

                // 3. Session (Oturum) Bilgilerini Doldur
                HttpContext.Session.SetString("KullaniciAdi", kullanici.KullaniciAdi);
                HttpContext.Session.SetInt32("KullaniciId", kullanici.KullaniciId);

                // Rol bilgisini al (Varsa)
                if (kullanici.KullaniciRolu.HasValue)
                {
                    HttpContext.Session.SetString("Rol", kullanici.KullaniciRolu.ToString());
                }

                // 4. KRİTİK HAMLE: Admin ise session'a küçük harfle "true" yazıyoruz
                if (kullanici.IsAdmin)
                {
                    HttpContext.Session.SetString("IsAdmin", "true");
                }
                else
                {
                    HttpContext.Session.SetString("IsAdmin", "false");
                }

                // Eğer berber/salon sahibi ise onu da işaretleyelim (Opsiyonel)
                // if (kullanici.KullaniciRolu == Rol.Berber) ... gibi

                // Ana sayfaya yönlendir
                return RedirectToAction("Index", "Home");
            }

            // Hata mesajı: Kullanıcı adı veya şifre yanlış
            ViewBag.ErrorMessage = "Kullanıcı adı veya şifre hatalı.";
            return View();
        }

        // Kayıt Olma Sayfası (GET)
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // Kayıt Olma İşlemi (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(Kullanici model)
        {
            if (ModelState.IsValid)
            {
                // Şifreler eşleşiyor mu kontrolü
                if (model.KullaniciSifre != model.KullaniciSifreTekrar)
                {
                    ModelState.AddModelError("KullaniciSifre", "Şifreler uyuşmuyor.");
                    return View(model);
                }

                // Aynı kullanıcı adı/email var mı kontrolü
                var existingUser = _context.Kullanicilar.FirstOrDefault(k => k.KullaniciAdi == model.KullaniciAdi);
                if (existingUser != null)
                {
                    ModelState.AddModelError("KullaniciAdi", "Bu kullanıcı adı/email zaten kayıtlı.");
                    return View(model);
                }

                try
                {
                    // Yeni kullanıcı nesnesi oluşturuluyor
                    var kullanici = new Kullanici
                    {
                        KullaniciAdi = model.KullaniciAdi, // Email buraya geliyor
                        KullaniciSoyadi = model.KullaniciSoyadi,
                        KullaniciTelefon = model.KullaniciTelefon,
                        KullaniciSifre = model.KullaniciSifre,
                        KullaniciSifreTekrar = model.KullaniciSifreTekrar,
                        KullaniciRolu = model.KullaniciRolu,
                        // Admin mi? (Rol enum'ından kontrol edilebilir veya manuel verilebilir)
                        IsAdmin = false, // Varsayılan olarak hayır
                        IsApproved = false // Varsayılan olarak onaysız
                    };

                    _context.Kullanicilar.Add(kullanici);
                    _context.SaveChanges();

                    TempData["SuccessMessage"] = "Kayıt başarılı! Admin onayı bekleniyor.";
                    return RedirectToAction("Login", "Kullanici");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Bir hata oluştu: " + ex.Message);
                }
            }
            return View(model);
        }

        // Çıkış İşlemi
        public IActionResult Logout()
        {
            // Tüm session verilerini temizler
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Kullanici"); // Giriş sayfasına at
        }
    }
}