using UnityEngine;

/// <summary>
/// Safe area yönetimi için component.
/// Notch, home indicator ve diğer sistem UI'ları için güvenli alan sağlar.
/// </summary>
public class SafeArea : MonoBehaviour
{
    [Header("Safe Area Ayarları")]
    [SerializeField] private bool applySafeArea = true;
    [SerializeField] private bool applyOnStart = true;
    [SerializeField] private bool applyOnUpdate = true;
    
    [Header("Debug")]
    [SerializeField] private bool debugMode = false;
    
    private RectTransform rectTransform;
    private Rect lastSafeArea = Rect.zero;
    private Vector2 lastScreenSize = Vector2.zero;
    
    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        if (rectTransform == null)
        {
            Debug.LogError("[SafeArea] RectTransform bulunamadı!");
            enabled = false;
            return;
        }
    }
    
    void Start()
    {
        if (applyOnStart)
        {
            ApplySafeArea();
        }
    }
    
    void Update()
    {
        if (!applyOnUpdate) return;
        
        // Ekran boyutu veya safe area değiştiğinde güncelle
        if (Screen.width != lastScreenSize.x || Screen.height != lastScreenSize.y || 
            Screen.safeArea != lastSafeArea)
        {
            lastScreenSize = new Vector2(Screen.width, Screen.height);
            lastSafeArea = Screen.safeArea;
            ApplySafeArea();
        }
    }
    
    /// <summary>
    /// Safe area'yı uygular
    /// </summary>
    public void ApplySafeArea()
    {
        if (!applySafeArea || rectTransform == null) return;
        
        Rect safeArea = Screen.safeArea;
        
        // Safe area koordinatlarını 0-1 aralığına normalize et
        Vector2 anchorMin = safeArea.position;
        Vector2 anchorMax = safeArea.position + safeArea.size;
        
        anchorMin.x /= Screen.width;
        anchorMin.y /= Screen.height;
        anchorMax.x /= Screen.width;
        anchorMax.y /= Screen.height;
        
        // Anchor'ları uygula
        rectTransform.anchorMin = anchorMin;
        rectTransform.anchorMax = anchorMax;
        
        // Offset'leri sıfırla (anchor'lar zaten pozisyonu belirliyor)
        rectTransform.offsetMin = Vector2.zero;
        rectTransform.offsetMax = Vector2.zero;
        
        if (debugMode)
        {
            Debug.Log($"[SafeArea] Uygulandı:\n" +
                     $"Ekran: {Screen.width}x{Screen.height}\n" +
                     $"Safe Area: {safeArea}\n" +
                     $"Anchor Min: {anchorMin}\n" +
                     $"Anchor Max: {anchorMax}");
        }
    }
    
    /// <summary>
    /// Safe area'yı devre dışı bırak
    /// </summary>
    public void DisableSafeArea()
    {
        if (rectTransform == null) return;
        
        rectTransform.anchorMin = Vector2.zero;
        rectTransform.anchorMax = Vector2.one;
        rectTransform.offsetMin = Vector2.zero;
        rectTransform.offsetMax = Vector2.zero;
        
        if (debugMode)
        {
            Debug.Log("[SafeArea] Devre dışı bırakıldı");
        }
    }
    
    /// <summary>
    /// Manuel olarak safe area'yı yeniden uygula
    /// </summary>
    [ContextMenu("Safe Area'yı Yeniden Uygula")]
    public void RefreshSafeArea()
    {
        ApplySafeArea();
    }
    
    /// <summary>
    /// Mevcut safe area bilgilerini göster
    /// </summary>
    [ContextMenu("Safe Area Bilgilerini Göster")]
    public void ShowSafeAreaInfo()
    {
        Debug.Log($"[SafeArea] Bilgiler:\n" +
                 $"Ekran: {Screen.width}x{Screen.height}\n" +
                 $"Safe Area: {Screen.safeArea}\n" +
                 $"Cutouts: {Screen.cutouts.Length}\n" +
                 $"Platform: {Application.platform}\n" +
                 $"Device Model: {SystemInfo.deviceModel}");
    }
}
