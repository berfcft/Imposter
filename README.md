🕵️ Imposter - Unity Kelime Avı
Imposter, arkadaş gruplarıyla aynı cihaz üzerinden oynanan, sosyal çıkarım ve dikkate dayalı bir kelime oyunudur. Unity motoru ile geliştirilen bu projede temel amaç, gruptaki "hain"i (imposter) bulmak veya imposter iseniz gizli kelimeyi tahmin ederek kendinizi gizlemektir.

🎮 Oyunun Akışı
Giriş: Ana menüdeki "Oyna" butonu ile oyuna başlanır.

Seçim Ekranı: Oyuncu sayısı ve gruptaki Imposter sayısı belirlenir.

Kelime Dağıtımı: * Sistem, oyunculara sırayla telefonun verilmesini ister.

Normal oyunculara bir "Ana Kelime" gösterilir.

Sistemin rastgele seçtiği Imposter'a ise farklı bir "İpucu Kelimesi" verilir.

Tartışma ve Oylama: Oyuncular kelimeleri hakkında konuşur ve aralarındaki farklı kelimeye sahip olan Imposter'ı bulmaya çalışır.

🛠️ Teknik Özellikler
Unity Sürümü: 2021 LTS (veya senin kullandığın sürüm)

Dil: C#

Rastgele Seçim Algoritması: Imposter'ın her seferinde farklı biri olmasını sağlayan adil seçim sistemi.

Dinamik Arayüz: Seçilen oyuncu sayısına göre kendini ayarlayan kullanıcı panelleri.

📂 Proje Yapısı
Scenes: MainMenu ve GameScene (veya senin kullandığın sahne isimleri).

Scripts: * GameManager.cs: Oyunun genel akışını ve mantığını yönetir.

UIManager.cs: Butonlar, paneller ve metin kutularının kontrolünü sağlar.

Assets: Oyun içinde kullanılan görseller ve yazı tipleri.

🚀 Kurulum ve Çalıştırma
Bu depoyu bilgisayarınıza indirin veya klonlayın:

Bash
git clone https://github.com/berfcft/Imposter.git
Unity Hub'ı açın ve projeyi listeye ekleyin.

Projeyi açtıktan sonra Scenes klasöründeki ana sahneyi başlatın.
