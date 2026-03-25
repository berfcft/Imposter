using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Tüm telefon ekranları için UI ölçeklendirme yöneticisi.
/// Farklı ekran boyutları ve en-boy oranları için UI'ı dinamik olarak ayarlar.
/// </summary>
public class UIScaler : MonoBehaviour
{
    [Header("Canvas Scaler Ayarları")]
    [SerializeField] private CanvasScaler canvasScaler;
    [SerializeField] private Canvas canvas;
    
    [Header("Referans Çözünürlükler")]
    [SerializeField] private Vector2 referenceResolution = new Vector2(1080, 1920); // Portrait için
    [SerializeField] private Vector2 landscapeReferenceResolution = new Vector2(1920, 1080); // Landscape için
    
    [Header("Güvenli Alan Ayarları")]
    [SerializeField] private bool useSafeArea = true;
    [SerializeField] private RectTransform safeAreaContainer;
    
    [Header("Debug")]
    [SerializeField] private bool debugMode = false;
    
    private Rect lastSafeArea = Rect.zero;
    private Vector2 lastScreenSize = Vector2.zero;
    
    void Awake()
    {
        // Canvas Scaler'ı otomatik bul
        if (canvasScaler == null)
            canvasScaler = GetComponent<CanvasScaler>();
            
        if (canvas == null)
            canvas = GetComponent<Canvas>();
            
        // Safe area container'ı otomatik bul
        if (safeAreaContainer == null)
            safeAreaContainer = transform as RectTransform;
    }
    
    void Start()
    {
        // Ekran yönlendirmesini ayarla
        SetupScreenOrientation();
        SetupCanvasScaler();
        ApplySafeArea();
    }
    
    void Update()
    {
        // Ekran boyutu değiştiğinde güncelle
        if (Screen.width != lastScreenSize.x || Screen.height != lastScreenSize.y)
        {
            lastScreenSize = new Vector2(Screen.width, Screen.height);
            SetupCanvasScaler();
        }
        
        // Safe area değiştiğinde güncelle
        if (useSafeArea && Screen.safeArea != lastSafeArea)
        {
            lastSafeArea = Screen.safeArea;
            ApplySafeArea();
        }
    }
    
    /// <summary>
    /// Canvas Scaler'ı mevcut ekran boyutuna göre ayarlar
    /// </summary>
    void SetupCanvasScaler()
    {
        if (canvasScaler == null) return;
        
        float screenAspect = (float)Screen.width / Screen.height;
        bool isPortrait = Screen.height > Screen.width;
        
        // Referans çözünürlüğü seç
        Vector2 targetReference = isPortrait ? referenceResolution : landscapeReferenceResolution;
        
        // Canvas Scaler ayarları
        canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        canvasScaler.referenceResolution = targetReference;
        canvasScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
        
        // En-boy oranına göre match değerini ayarla
        if (isPortrait)
        {
            // Portrait modda height'a göre ölçekle
            canvasScaler.matchWidthOrHeight = 1f;
        }
        else
        {
            // Landscape modda width'a göre ölçekle
            canvasScaler.matchWidthOrHeight = 0f;
        }
        
        if (debugMode)
        {
            Debug.Log($"[UIScaler] Ekran: {Screen.width}x{Screen.height}, " +
                     $"Aspect: {screenAspect:F2}, " +
                     $"Orientasyon: {(isPortrait ? "Portrait" : "Landscape")}, " +
                     $"Match: {canvasScaler.matchWidthOrHeight}");
        }
    }
    
    /// <summary>
    /// Ekran yönlendirmesini ayarlar
    /// </summary>
    void SetupScreenOrientation()
    {
        // Sadece portrait modunu zorla
        Screen.orientation = ScreenOrientation.Portrait;
        Screen.autorotateToPortrait = true;
        Screen.autorotateToPortraitUpsideDown = false;
        Screen.autorotateToLandscapeLeft = false;
        Screen.autorotateToLandscapeRight = false;
        
        if (debugMode)
        {
            Debug.Log($"[UIScaler] Ekran yönlendirmesi ayarlandı: {Screen.orientation}");
        }
    }
    
    /// <summary>
    /// Safe area'yı uygular (notch, home indicator vb. için)
    /// </summary>
    void ApplySafeArea()
    {
        if (!useSafeArea || safeAreaContainer == null) return;
        
        Rect safeArea = Screen.safeArea;
        Vector2 anchorMin = safeArea.position;
        Vector2 anchorMax = safeArea.position + safeArea.size;
        
        anchorMin.x /= Screen.width;
        anchorMin.y /= Screen.height;
        anchorMax.x /= Screen.width;
        anchorMax.y /= Screen.height;
        
        safeAreaContainer.anchorMin = anchorMin;
        safeAreaContainer.anchorMax = anchorMax;
        
        if (debugMode)
        {
            Debug.Log($"[UIScaler] Safe Area: {safeArea}, " +
                     $"Anchor Min: {anchorMin}, Anchor Max: {anchorMax}");
        }
    }
    
    /// <summary>
    /// Manuel olarak UI'ı yeniden ölçeklendir
    /// </summary>
    [ContextMenu("UI'ı Yeniden Ölçeklendir")]
    public void RefreshUIScaling()
    {
        SetupCanvasScaler();
        ApplySafeArea();
    }
    
    /// <summary>
    /// Mevcut ekran bilgilerini debug için göster
    /// </summary>
    [ContextMenu("Ekran Bilgilerini Göster")]
    public void ShowScreenInfo()
    {
        Debug.Log($"[UIScaler] Ekran Bilgileri:\n" +
                 $"Çözünürlük: {Screen.width}x{Screen.height}\n" +
                 $"En-Boy Oranı: {(float)Screen.width / Screen.height:F2}\n" +
                 $"Orientasyon: {(Screen.height > Screen.width ? "Portrait" : "Landscape")}\n" +
                 $"Safe Area: {Screen.safeArea}\n" +
                 $"DPI: {Screen.dpi}\n" +
                 $"Platform: {Application.platform}");
    }
}
