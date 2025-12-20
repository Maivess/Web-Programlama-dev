using Microsoft.EntityFrameworkCore;
// Eğer hata verirse aşağıdaki satırın başındaki // işaretini kaldır ve kendi proje adını yaz
// using BerberYonetimSistemi.Models;

var builder = WebApplication.CreateBuilder(args);

// Servisleri ekle
builder.Services.AddControllersWithViews();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // 30 dk boş durursa oturum düşer
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddDistributedMemoryCache(); // Session'ı hafızada tutmak için şart
// (Az önce eklediysen AddHttpContextAccessor burada kalsın, eklemediysen onu da ekle)
builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthentication("Cookies").AddCookie(); // Cookie hatası vermesin diye
builder.Services.AddHttpContextAccessor();

// Veritabanı Bağlantısı (SQL Server)
// NOT: Buradaki 'BerberContext' ismi hatalıysa, Models klasöründeki context dosyasının adını yaz.
builder.Services.AddDbContext < BerberYonetimSistemi.Data.BerberDbContext > (options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<BerberYonetimSistemi.Services.GeminiService>();
var app = builder.Build();

// HTTP istek boru hattını yapılandır
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();