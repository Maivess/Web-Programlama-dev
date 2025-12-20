namespace BerberYonetimSistemi.Services
{
    public class GeminiService
    {
        public async Task<string> GetFitnessAdvice(int yas, double kilo, double boy, string cinsiyet, string hedef)
        {
            // API ile vakit kaybetmiyoruz, yerel zekayı çalıştırıyoruz
            return await Task.Run(() =>
            {
                // 1. Metabolizma Hızı Hesaplama (Harris-Benedict)
                double bmr = (cinsiyet == "Erkek")
                    ? (10 * kilo) + (6.25 * boy) - (5 * yas) + 5
                    : (10 * kilo) + (6.25 * boy) - (5 * yas) - 161;

                double günlükKalori = bmr * 1.4; // Orta hareketli katsayısı
                double hedefKalori = (hedef == "Kilo Vermek") ? günlükKalori - 500 : günlükKalori + 300;

                // 2. Makro Besin Hesaplama
                double protein = (hedef == "Kas Yapmak") ? kilo * 2 : kilo * 1.6;
                double yag = kilo * 0.8;
                double karbonhidrat = (hedefKalori - (protein * 4 + yag * 9)) / 4;

                // 3. Antrenman Programı Belirleme
                string program = (hedef == "Kilo Vermek")
                    ? "Haftada 3 gün tüm vücut ağırlık + 2 gün 30dk yüksek yoğunluklu kardiyo (HIIT)."
                    : "Haftada 4 gün split antrenman (İtme-Çekme-Bacak). Progresif yükleme prensibiyle çalışın.";

                
                var sb = new System.Text.StringBuilder();
                sb.Append("<div class='ai-response'>");
                sb.Append($"<h5><b>Vücut Analiz Raporu</b></h5>");
                sb.Append($"<p>Mevcut verilere göre günlük bazal metabolizma hızınız <b>{bmr:F0} kalori</b> olarak saptanmıştır.</p>");
                sb.Append("<ul>");
                sb.Append($"<li><b>Hedef Günlük Kalori:</b> {hedefKalori:F0} kcal</li>");
                sb.Append($"<li><b>Günlük Protein Hedefi:</b> {protein:F0}g</li>");
                sb.Append($"<li><b>Günlük Karbonhidrat:</b> {karbonhidrat:F0}g</li>");
                sb.Append($"<li><b>Günlük Yağ:</b> {yag:F0}g</li>");
                sb.Append("</ul>");
                sb.Append("<h5><b>Önerilen Egzersiz Planı</b></h5>");
                sb.Append($"<p>{program}</p>");
                sb.Append("<p class='text-success'><i>Sistem verilerinizi işledi ve en uygun programı optimize etti.</i></p>");
                sb.Append("</div>");

                return sb.ToString();
            });
        }
    }
}