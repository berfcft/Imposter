# Unity Social Word Guessing Game

Bu proje, Unity 2021 LTS kullanılarak geliştirilmiş bir sosyal kelime tahmin oyunudur. Oyun hem "pass-and-play" (aynı cihazda) hem de gerçek zamanlı "online multiplayer" modlarını destekler.

## 🎮 Oyun Özellikleri

- **İki Oyun Modu:**
  - **Yerel Oyun (Pass-and-Play):** Aynı cihazda sırayla oynanan mod
  - **Online Oyun:** Mirror networking kullanarak gerçek zamanlı çok oyunculu mod

- **Oyun Mekaniği:**
  - Oyunculara rastgele kelimeler dağıtılır (ana kelime ve impostor kelimesi)
  - Fisher-Yates shuffle algoritması ile adil dağıtım
  - Oylama sistemi ile en çok oy alan oyuncu elenir
  - Zaman sınırlı oylama

## 🏗️ Proje Yapısı

### Sahneler (Scenes)

1. **ModeSelection.unity** - Ana menü sahnesi
   - "Online Oyna" butonu
   - "Yerel Oyna" butonu

2. **LocalGame.unity** - Yerel oyun sahnesi
   - MaskPanel: Oyuncu sırasını gösterir
   - RevealPanel: Kelimeyi gösterir
   - PassAndPlayManager script'i ile yönetilir

3. **OnlineGame.unity** - Online oyun sahnesi
   - LobbyPanel: Oda oluşturma/katılma
   - RevealPanel: Kelime gösterimi
   - OnlineGameManager script'i ile yönetilir

4. **VotingScene.unity** - Oylama sahnesi
   - Dinamik oyuncu butonları
   - Oylama sonuçları
   - Yeniden başlat/Ana menü butonları

### Scripts

#### Core Scripts

1. **WordManager.cs** - Kelime yönetimi
   ```csharp
   public static List<string> GenerateWords(int playerCount, int impostorCount, string mainWord, string impostorWord)
   ```
   - Fisher-Yates shuffle algoritması ile kelime dağıtımı
   - Static utility class

2. **ModeSelectionManager.cs** - Ana menü yönetimi
   - Online/Local oyun seçimi
   - Mirror Host başlatma
   - Sahne geçişleri

3. **PassAndPlayManager.cs** - Yerel oyun yönetimi
   - Tur bazlı oyun akışı
   - Mask → Reveal geçişleri
   - Oyuncu sırası yönetimi

4. **OnlineGameManager.cs** - Online oyun yönetimi
   - Mirror networking entegrasyonu
   - [Server] ve [TargetRpc] metodları
   - Zaman yönetimi

5. **VotingManager.cs** - Oylama sistemi
   - Dinamik buton oluşturma
   - Oylama toplama
   - Sonuç gösterimi

6. **CustomCursor.cs** - Özel imleç yönetimi
   - Cursor.SetCursor desteği

#### Assembly Definitions

- **Game.asmdef** - Mirror networking referansı

## 🚀 Kurulum

### Gereksinimler

- Unity 2021 LTS
- Mirror Networking Package
- Git (opsiyonel, manuel kurulum için)

### Kurulum Adımları

1. **Unity Projesi Oluşturma:**
   - Unity 2021 LTS'yi açın
   - Yeni 3D projesi oluşturun

2. **Mirror Networking Kurulumu:**
   - Package Manager → Add package from git URL
   - URL: `https://github.com/vis2k/Mirror.git#release/2021`
   - **Alternatif (Git hatası durumunda):**
     - [Mirror GitHub Releases](https://github.com/vis2k/Mirror/releases) sayfasından zip indirin
     - `Assets/Mirror` klasörünü projenizin `Assets` klasörüne kopyalayın

3. **Proje Dosyalarını Kopyalama:**
   - Tüm script dosyalarını `Assets/Scripts/` klasörüne kopyalayın
   - Sahne dosyalarını `Assets/Scenes/` klasörüne kopyalayın
   - Prefab dosyalarını `Assets/Prefabs/` klasörüne kopyalayın

4. **Build Settings:**
   - File → Build Settings
   - Sahneleri şu sırayla ekleyin:
     1. ModeSelection
     2. LocalGame
     3. OnlineGame
     4. VotingScene

## 🎯 Kullanım

### Yerel Oyun (Pass-and-Play)

1. **ModeSelection** sahnesinde "Yerel Oyna" butonuna basın
2. **LocalGame** sahnesinde:
   - "Sıradaki: Oyuncu X" mesajı görünür
   - "GÖSTER" butonuna basarak kelimeyi görün
   - "TAMAM" butonuna basarak sırayı geçin
3. Tüm oyuncular tamamladığında **VotingScene**'e geçer

### Online Oyun

1. **ModeSelection** sahnesinde "Online Oyna" butonuna basın
2. **OnlineGame** sahnesinde:
   - Host: "Oda Oluştur" butonuna basın
   - Client: "Odaya Katıl" butonuna basın
3. Oyun başladığında kelimeler otomatik dağıtılır
4. Süre bitiminde **VotingScene**'e geçer

### Oylama Sistemi

1. **VotingScene**'de dinamik oyuncu butonları görünür
2. Her oyuncu bir kez oy verebilir
3. En çok oy alan oyuncu elenir
4. Sonuçlar gösterilir
5. "Yeniden Başlat" veya "Ana Menü" seçenekleri

## ⚙️ Yapılandırma

### Inspector Ayarları

#### PassAndPlayManager
- `playerCount`: Oyuncu sayısı (varsayılan: 4)
- `impostorCount`: Impostor sayısı (varsayılan: 1)
- `mainWord`: Ana kelime (varsayılan: "Elma")
- `impostorWord`: Impostor kelimesi (varsayılan: "Armut")

#### OnlineGameManager
- `playerCount`: Oyuncu sayısı
- `impostorCount`: Impostor sayısı
- `mainWord`: Ana kelime
- `impostorWord`: Impostor kelimesi
- `gameDuration`: Oyun süresi (saniye)

#### VotingManager
- `playerCount`: Oyuncu sayısı
- `votingDuration`: Oylama süresi (saniye)
- `buttonPrefab`: Oyuncu butonu prefab'ı

### UI Referansları

Her script'in Inspector'ında aşağıdaki UI referansları atanmalıdır:

- **PassAndPlayManager:**
  - `maskPanel`: Mask panel GameObject'i
  - `revealPanel`: Reveal panel GameObject'i
  - `maskText`: Mask text component'i
  - `wordText`: Word text component'i
  - `showButton`: Show button component'i
  - `doneButton`: Done button component'i

- **OnlineGameManager:**
  - `lobbyPanel`: Lobby panel GameObject'i
  - `revealPanel`: Reveal panel GameObject'i
  - `wordText`: Word text component'i
  - `timerText`: Timer text component'i

- **VotingManager:**
  - `votingPanel`: Voting panel GameObject'i
  - `buttonPrefab`: Voting button prefab'ı
  - `resultText`: Result text component'i
  - `restartButton`: Restart button component'i
  - `mainMenuButton`: Main menu button component'i

## 🐛 Sorun Giderme

### Mirror Kurulum Sorunları

**Hata:** "No 'git' executable was found"

**Çözümler:**
1. Git'i [resmi sitesinden](https://git-scm.com/) indirin ve kurun
2. Unity ve Unity Hub'ı yeniden başlatın
3. **Alternatif:** Mirror'u manuel olarak indirin ve kopyalayın

### Script Referans Sorunları

**Hata:** Script referansları eksik

**Çözüm:**
1. Script dosyalarının doğru klasörde olduğundan emin olun
2. Unity'de Assets → Reimport All yapın
3. Inspector'da referansları manuel olarak atayın

### Build Sorunları

**Hata:** Sahne bulunamıyor

**Çözüm:**
1. Build Settings'te tüm sahnelerin eklendiğinden emin olun
2. Sahne sırasını kontrol edin
3. Sahne dosyalarının doğru konumda olduğunu kontrol edin

## 📝 Notlar

- Tüm script'ler Türkçe yorumlarla yazılmıştır
- Debug modu Inspector'da açılabilir
- Custom cursor desteği mevcuttur
- Fisher-Yates shuffle algoritması kullanılmıştır
- Mirror networking için gerekli assembly definitions mevcuttur

## 🤝 Katkıda Bulunma

1. Projeyi fork edin
2. Feature branch oluşturun (`git checkout -b feature/AmazingFeature`)
3. Değişikliklerinizi commit edin (`git commit -m 'Add some AmazingFeature'`)
4. Branch'inizi push edin (`git push origin feature/AmazingFeature`)
5. Pull Request oluşturun

## 📄 Lisans

Bu proje MIT lisansı altında lisanslanmıştır. Detaylar için `LICENSE` dosyasına bakın.
