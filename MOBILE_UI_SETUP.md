# Mobil UI Uyumluluğu Kurulum Rehberi

Bu rehber, Imposter oyununu tüm telefon ekranları için uyumlu hale getirmek için gerekli adımları açıklar.

## 🎯 Hedef

- Tüm telefon ekran boyutları için uyumlu UI
- Notch, home indicator ve diğer sistem UI'ları için güvenli alan
- Portrait ve landscape modları için dinamik ölçeklendirme
- Kenarlardaki mavilik sorununu çözme

## 📱 Eklenen Scriptler

### 1. UIScaler.cs
**Konum:** `Assets/Scripts/UIScaler.cs`

**Amaç:** Canvas Scaler'ı otomatik olarak ayarlar ve farklı ekran boyutları için UI ölçeklendirmesi yapar.

**Özellikler:**
- Portrait/landscape modları için otomatik referans çözünürlük seçimi
- Safe area desteği
- Debug modu ile ekran bilgilerini görüntüleme

**Kullanım:**
```csharp
// Canvas'a ekleyin
UIScaler uiScaler = canvas.AddComponent<UIScaler>();
```

### 2. SafeArea.cs
**Konum:** `Assets/Scripts/SafeArea.cs`

**Amaç:** Notch, home indicator ve diğer sistem UI'ları için güvenli alan sağlar.

**Özellikler:**
- Otomatik safe area tespiti
- Runtime'da safe area değişikliklerini takip etme
- Debug modu

**Kullanım:**
```csharp
// UI container'a ekleyin
SafeArea safeArea = container.AddComponent<SafeArea>();
```

### 3. MobileUIHelper.cs
**Konum:** `Assets/Scripts/MobileUIHelper.cs`

**Amaç:** Mobil UI için yardımcı fonksiyonlar sağlar.

**Özellikler:**
- UI elemanlarını otomatik ölçeklendirme
- Font boyutlarını ekran boyutuna göre ayarlama
- UI elemanlarını ekranın farklı konumlarına yerleştirme

**Kullanım:**
```csharp
// UI elemanını ölçeklendir
MobileUIHelper.ScaleUIElement(rectTransform, 200, 100);

// Font boyutunu ayarla
MobileUIHelper.ScaleTMPFontSize(textComponent, 24);

// UI elemanını köşeye sabitle
MobileUIHelper.AnchorToCorner(rectTransform, UICorner.TopRight, 20);
```

## 🔧 Kurulum Adımları

### 1. Canvas Ayarları

Her Canvas'a UIScaler component'i ekleyin:

1. Canvas'ı seçin
2. Add Component → Scripts → UIScaler
3. Ayarları kontrol edin:
   - **Reference Resolution:** 1080x1920 (Portrait)
   - **Landscape Reference Resolution:** 1920x1080
   - **Use Safe Area:** ✓ (işaretli)
   - **Debug Mode:** Test için açık

### 2. Safe Area Container

Ana UI container'a SafeArea component'i ekleyin:

1. Ana UI container'ı seçin (genellikle Canvas'ın ilk child'ı)
2. Add Component → Scripts → SafeArea
3. Ayarları kontrol edin:
   - **Apply Safe Area:** ✓ (işaretli)
   - **Apply On Start:** ✓ (işaretli)
   - **Apply On Update:** ✓ (işaretli)

### 3. UI Elemanlarının Anchor'larını Düzenleme

Tüm UI elemanları için uygun anchor'ları ayarlayın:

#### Butonlar için:
- **Top buttons:** Anchor Min (0,1), Anchor Max (1,1)
- **Bottom buttons:** Anchor Min (0,0), Anchor Max (1,0)
- **Side buttons:** Anchor Min (0,0), Anchor Max (0,1) veya (1,0), (1,1)

#### Paneller için:
- **Full screen panels:** Anchor Min (0,0), Anchor Max (1,1)
- **Centered panels:** Anchor Min (0.5,0.5), Anchor Max (0.5,0.5)

#### Text elemanları için:
- **Headers:** Anchor Min (0,1), Anchor Max (1,1)
- **Body text:** Anchor Min (0,0), Anchor Max (1,1)

### 4. Font Boyutlarını Ayarlama

Script'lerde font boyutlarını dinamik hale getirin:

```csharp
// Önceki kod:
text.fontSize = 24;

// Yeni kod:
MobileUIHelper.ScaleTMPFontSize(text, 24);
```

### 5. UI Eleman Boyutlarını Ayarlama

Sabit boyutlar yerine dinamik boyutlar kullanın:

```csharp
// Önceki kod:
rectTransform.sizeDelta = new Vector2(200, 100);

// Yeni kod:
MobileUIHelper.ScaleUIElement(rectTransform, 200, 100);
```

## 🎮 Sahne Bazlı Kurulum

### ModeSelection Sahnesi

1. **Canvas'a UIScaler ekleyin**
2. **Ana container'a SafeArea ekleyin**
3. **Butonların anchor'larını düzenleyin:**
   - Local Play Button: Center
   - Settings Button: Top Right
4. **Font boyutlarını dinamik hale getirin**

### LocalGame Sahnesi

1. **Canvas'a UIScaler ekleyin**
2. **SetupPanel'a SafeArea ekleyin**
3. **MaskPanel ve RevealPanel'ları tam ekran yapın**
4. **Butonların pozisyonlarını düzenleyin**

## 📐 Test Senaryoları

### Test Edilecek Ekran Boyutları:

1. **iPhone SE (375x667)** - Küçük ekran
2. **iPhone 12 (390x844)** - Orta ekran
3. **iPhone 12 Pro Max (428x926)** - Büyük ekran
4. **Samsung Galaxy S21 (360x800)** - Android küçük
5. **Samsung Galaxy S21 Ultra (412x915)** - Android büyük
6. **iPad (768x1024)** - Tablet test

### Test Edilecek Özellikler:

- [ ] UI elemanları tüm ekranlarda görünür
- [ ] Safe area düzgün çalışıyor
- [ ] Font boyutları okunabilir
- [ ] Butonlar dokunulabilir boyutta
- [ ] Kenarlarda mavilik yok
- [ ] Portrait/landscape geçişleri düzgün

## 🐛 Sorun Giderme

### Kenarlarda Mavilik Sorunu

**Sebep:** Canvas'ın safe area dışında render edilmesi

**Çözüm:**
1. Canvas'a SafeArea component'i ekleyin
2. `androidRenderOutsideSafeArea` ayarını 0 yapın
3. UI elemanlarının anchor'larını kontrol edin

### UI Elemanları Çok Küçük/Büyük

**Sebep:** Yanlış referans çözünürlük

**Çözüm:**
1. UIScaler'da referans çözünürlükleri kontrol edin
2. Debug modunu açarak ekran bilgilerini görün
3. `MatchWidthOrHeight` değerini ayarlayın

### Safe Area Çalışmıyor

**Sebep:** Component ayarları yanlış

**Çözüm:**
1. SafeArea component'inin `Apply Safe Area` seçeneğini kontrol edin
2. RectTransform'ın anchor'larını kontrol edin
3. Debug modunu açarak safe area bilgilerini görün

### Oyun Ters Dönüyor

**Sebep:** Ekran yönlendirmesi ayarları yanlış

**Çözüm:**
1. Project Settings → Player → Resolution and Presentation
2. Default Orientation: Portrait
3. Allowed Orientations: Sadece Portrait işaretli
4. UIScaler component'ine ScreenOrientationManager ekleyin
5. `lockToPortrait = true` ayarlayın

## 📱 Platform Özel Ayarlar

### Android

- **Minimum SDK:** 23 (Android 6.0)
- **Target SDK:** 33 (Android 13)
- **Screen Orientation:** Portrait
- **Fullscreen Mode:** Enabled
- **Render Outside Safe Area:** Disabled

### iOS

- **Minimum iOS Version:** 13.0
- **Screen Orientation:** Portrait
- **Safe Area:** Enabled
- **Status Bar:** Hidden

## 🎯 Performans Optimizasyonları

1. **Canvas Sayısını Azaltın:** Tek Canvas kullanın
2. **UI Elemanlarını Pool'layın:** Gereksiz instantiate/destroy işlemlerini önleyin
3. **Texture Atlasing:** UI texture'larını birleştirin
4. **Font Asset'lerini Optimize Edin:** Sadece kullanılan karakterleri dahil edin

## 📋 Kontrol Listesi

- [ ] UIScaler tüm Canvas'lara eklendi
- [ ] SafeArea ana container'lara eklendi
- [ ] UI elemanlarının anchor'ları düzenlendi
- [ ] Font boyutları dinamik hale getirildi
- [ ] UI eleman boyutları dinamik hale getirildi
- [ ] Farklı ekran boyutlarında test edildi
- [ ] Safe area test edildi
- [ ] Portrait/landscape geçişleri test edildi
- [ ] Kenarlarda mavilik kontrol edildi
- [ ] Ekran yönlendirmesi doğru çalışıyor
- [ ] Oyun ters dönmüyor

## 🔄 Güncellemeler

Bu rehber, yeni özellikler ve düzeltmeler eklendikçe güncellenecektir. En son sürüm için GitHub repository'sini kontrol edin.
