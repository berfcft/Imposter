using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Mobil UI bileşenlerinin nasıl kullanılacağını gösteren örnek script.
/// Bu script'i referans olarak kullanarak mevcut UI script'lerinizi güncelleyebilirsiniz.
/// </summary>
public class MobileUIExample : MonoBehaviour
{
    [Header("UI Referansları")]
    [SerializeField] private RectTransform mainPanel;
    [SerializeField] private RectTransform titleText;
    [SerializeField] private RectTransform playButton;
    [SerializeField] private RectTransform settingsButton;
    [SerializeField] private TMP_Text titleTextComponent;
    [SerializeField] private TMP_Text playButtonText;
    
    [Header("Mobil UI Ayarları")]
    [SerializeField] private bool autoSetupOnStart = true;
    [SerializeField] private bool useMobileScaling = true;
    
    void Start()
    {
        if (autoSetupOnStart)
        {
            SetupMobileUI();
        }
    }
    
    /// <summary>
    /// Mobil UI'ı otomatik olarak ayarlar
    /// </summary>
    [ContextMenu("Mobil UI'ı Ayarla")]
    public void SetupMobileUI()
    {
        if (!useMobileScaling) return;
        
        Debug.Log("[MobileUIExample] Mobil UI ayarlanıyor...");
        
        // 1. Canvas'a UIScaler ekle
        SetupCanvasScaler();
        
        // 2. Safe area ayarla
        SetupSafeArea();
        
        // 3. UI elemanlarını ölçeklendir
        ScaleUIElements();
        
        // 4. Font boyutlarını ayarla
        ScaleFontSizes();
        
        // 5. UI pozisyonlarını ayarla
        SetupUIPositions();
        
        Debug.Log("[MobileUIExample] Mobil UI ayarları tamamlandı!");
    }
    
    /// <summary>
    /// Canvas'a UIScaler component'i ekler
    /// </summary>
    void SetupCanvasScaler()
    {
        Canvas canvas = GetComponentInParent<Canvas>();
        if (canvas != null && canvas.GetComponent<UIScaler>() == null)
        {
            canvas.gameObject.AddComponent<UIScaler>();
            Debug.Log("[MobileUIExample] UIScaler eklendi");
        }
    }
    
    /// <summary>
    /// Safe area component'ini ayarlar
    /// </summary>
    void SetupSafeArea()
    {
        if (mainPanel != null && mainPanel.GetComponent<SafeArea>() == null)
        {
            mainPanel.gameObject.AddComponent<SafeArea>();
            Debug.Log("[MobileUIExample] SafeArea eklendi");
        }
    }
    
    /// <summary>
    /// UI elemanlarını ölçeklendirir
    /// </summary>
    void ScaleUIElements()
    {
        // Ana panel'i tam ekran yap
        if (mainPanel != null)
        {
            MobileUIHelper.MakeFullScreen(mainPanel);
        }
        
        // Başlık metnini ölçeklendir
        if (titleText != null)
        {
            MobileUIHelper.ScaleUIElement(titleText, 800, 150);
        }
        
        // Butonları ölçeklendir
        if (playButton != null)
        {
            MobileUIHelper.ScaleUIElement(playButton, 300, 80);
        }
        
        if (settingsButton != null)
        {
            MobileUIHelper.ScaleUIElement(settingsButton, 200, 60);
        }
    }
    
    /// <summary>
    /// Font boyutlarını ölçeklendirir
    /// </summary>
    void ScaleFontSizes()
    {
        // Başlık font boyutu
        if (titleTextComponent != null)
        {
            MobileUIHelper.ScaleTMPFontSize(titleTextComponent, 48);
        }
        
        // Buton font boyutu
        if (playButtonText != null)
        {
            MobileUIHelper.ScaleTMPFontSize(playButtonText, 24);
        }
    }
    
    /// <summary>
    /// UI elemanlarının pozisyonlarını ayarlar
    /// </summary>
    void SetupUIPositions()
    {
        // Başlığı üst kısma yerleştir
        if (titleText != null)
        {
            MobileUIHelper.SetUIPosition(titleText, 
                new Vector2(0, 0.7f), new Vector2(1, 0.9f), 
                Vector2.zero, Vector2.zero);
        }
        
        // Play butonunu merkeze yerleştir
        if (playButton != null)
        {
            MobileUIHelper.CenterUIElement(playButton);
            playButton.anchoredPosition = new Vector2(0, 0);
        }
        
        // Settings butonunu sağ üst köşeye yerleştir
        if (settingsButton != null)
        {
            MobileUIHelper.AnchorToCorner(settingsButton, UICorner.TopRight, 20);
        }
    }
    
    /// <summary>
    /// Mevcut UI'ı test modunda gösterir
    /// </summary>
    [ContextMenu("UI Bilgilerini Göster")]
    public void ShowUIInfo()
    {
        Debug.Log($"[MobileUIExample] UI Bilgileri:\n" +
                 $"Ekran: {Screen.width}x{Screen.height}\n" +
                 $"Aspect Ratio: {MobileUIHelper.GetScreenAspectRatio():F2}\n" +
                 $"Orientasyon: {(MobileUIHelper.IsPortrait() ? "Portrait" : "Landscape")}\n" +
                 $"UI Scale Factor: {MobileUIHelper.GetUIScaleFactor():F2}\n" +
                 $"Safe Area: {Screen.safeArea}");
    }
    
    /// <summary>
    /// UI'ı farklı ekran boyutları için test eder
    /// </summary>
    [ContextMenu("UI Test Modunu Başlat")]
    public void StartUITest()
    {
        Debug.Log("[MobileUIExample] UI test modu başlatılıyor...");
        
        // Test için farklı ekran boyutları simüle et
        TestScreenSize(375, 667, "iPhone SE");
        TestScreenSize(390, 844, "iPhone 12");
        TestScreenSize(428, 926, "iPhone 12 Pro Max");
        TestScreenSize(360, 800, "Samsung Galaxy S21");
        TestScreenSize(412, 915, "Samsung Galaxy S21 Ultra");
    }
    
    /// <summary>
    /// Belirli bir ekran boyutu için UI'ı test eder
    /// </summary>
    void TestScreenSize(int width, int height, string deviceName)
    {
        Debug.Log($"[MobileUIExample] {deviceName} ({width}x{height}) test ediliyor...");
        
        // Bu fonksiyon sadece debug amaçlıdır
        // Gerçek test için Unity Editor'da Game View'da farklı çözünürlükler deneyin
    }
    
    /// <summary>
    /// Runtime'da UI'ı yeniden ayarlar
    /// </summary>
    public void RefreshUI()
    {
        SetupMobileUI();
    }
    
    /// <summary>
    /// Mobil UI ayarlarını devre dışı bırakır
    /// </summary>
    public void DisableMobileUI()
    {
        useMobileScaling = false;
        
        // UIScaler'ı kaldır
        Canvas canvas = GetComponentInParent<Canvas>();
        if (canvas != null)
        {
            UIScaler uiScaler = canvas.GetComponent<UIScaler>();
            if (uiScaler != null)
            {
                DestroyImmediate(uiScaler);
            }
        }
        
        // SafeArea'yı kaldır
        if (mainPanel != null)
        {
            SafeArea safeArea = mainPanel.GetComponent<SafeArea>();
            if (safeArea != null)
            {
                DestroyImmediate(safeArea);
            }
        }
        
        Debug.Log("[MobileUIExample] Mobil UI devre dışı bırakıldı");
    }
}
