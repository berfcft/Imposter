using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Mobil UI için yardımcı fonksiyonlar ve utility'ler.
/// Responsive tasarım ve mobil optimizasyon için kullanılır.
/// </summary>
public static class MobileUIHelper
{
    /// <summary>
    /// Mevcut ekranın en-boy oranını döndürür
    /// </summary>
    public static float GetScreenAspectRatio()
    {
        return (float)Screen.width / Screen.height;
    }
    
    /// <summary>
    /// Ekranın portrait modda olup olmadığını kontrol eder
    /// </summary>
    public static bool IsPortrait()
    {
        return Screen.height > Screen.width;
    }
    
    /// <summary>
    /// Ekranın landscape modda olup olmadığını kontrol eder
    /// </summary>
    public static bool IsLandscape()
    {
        return Screen.width > Screen.height;
    }
    
    /// <summary>
    /// Ekran boyutuna göre UI elemanının boyutunu ayarlar
    /// </summary>
    /// <param name="rectTransform">Ayarlanacak RectTransform</param>
    /// <param name="baseWidth">Temel genişlik (1080p için)</param>
    /// <param name="baseHeight">Temel yükseklik (1920p için)</param>
    public static void ScaleUIElement(RectTransform rectTransform, float baseWidth, float baseHeight)
    {
        if (rectTransform == null) return;
        
        float scaleFactor = GetUIScaleFactor();
        Vector2 newSize = new Vector2(baseWidth * scaleFactor, baseHeight * scaleFactor);
        
        rectTransform.sizeDelta = newSize;
    }
    
    /// <summary>
    /// UI ölçek faktörünü hesaplar
    /// </summary>
    public static float GetUIScaleFactor()
    {
        float referenceHeight = 1920f; // Portrait için referans
        float referenceWidth = 1080f;
        
        if (IsPortrait())
        {
            return Screen.height / referenceHeight;
        }
        else
        {
            return Screen.width / referenceWidth;
        }
    }
    
    /// <summary>
    /// Font boyutunu ekran boyutuna göre ayarlar
    /// </summary>
    /// <param name="text">Ayarlanacak Text component</param>
    /// <param name="baseFontSize">Temel font boyutu</param>
    public static void ScaleFontSize(Text text, float baseFontSize)
    {
        if (text == null) return;
        
        float scaleFactor = GetUIScaleFactor();
        text.fontSize = Mathf.RoundToInt(baseFontSize * scaleFactor);
    }
    
    /// <summary>
    /// TMP_Text font boyutunu ekran boyutuna göre ayarlar
    /// </summary>
    /// <param name="text">Ayarlanacak TMP_Text component</param>
    /// <param name="baseFontSize">Temel font boyutu</param>
    public static void ScaleTMPFontSize(TMPro.TMP_Text text, float baseFontSize)
    {
        if (text == null) return;
        
        float scaleFactor = GetUIScaleFactor();
        text.fontSize = baseFontSize * scaleFactor;
    }
    
    /// <summary>
    /// UI elemanını ekranın belirli bir konumuna yerleştirir
    /// </summary>
    /// <param name="rectTransform">Yerleştirilecek RectTransform</param>
    /// <param name="anchorMin">Minimum anchor (0-1)</param>
    /// <param name="anchorMax">Maximum anchor (0-1)</param>
    /// <param name="offsetMin">Minimum offset</param>
    /// <param name="offsetMax">Maximum offset</param>
    public static void SetUIPosition(RectTransform rectTransform, Vector2 anchorMin, Vector2 anchorMax, Vector2 offsetMin, Vector2 offsetMax)
    {
        if (rectTransform == null) return;
        
        rectTransform.anchorMin = anchorMin;
        rectTransform.anchorMax = anchorMax;
        rectTransform.offsetMin = offsetMin;
        rectTransform.offsetMax = offsetMax;
    }
    
    /// <summary>
    /// UI elemanını ekranın kenarına sabitler
    /// </summary>
    /// <param name="rectTransform">Sabitlenecek RectTransform</param>
    /// <param name="edge">Kenar (Top, Bottom, Left, Right)</param>
    /// <param name="margin">Kenar boşluğu</param>
    public static void AnchorToEdge(RectTransform rectTransform, UIEdge edge, float margin = 0f)
    {
        if (rectTransform == null) return;
        
        switch (edge)
        {
            case UIEdge.Top:
                rectTransform.anchorMin = new Vector2(0, 1);
                rectTransform.anchorMax = new Vector2(1, 1);
                rectTransform.offsetMin = new Vector2(margin, -rectTransform.sizeDelta.y);
                rectTransform.offsetMax = new Vector2(-margin, 0);
                break;
                
            case UIEdge.Bottom:
                rectTransform.anchorMin = new Vector2(0, 0);
                rectTransform.anchorMax = new Vector2(1, 0);
                rectTransform.offsetMin = new Vector2(margin, 0);
                rectTransform.offsetMax = new Vector2(-margin, rectTransform.sizeDelta.y);
                break;
                
            case UIEdge.Left:
                rectTransform.anchorMin = new Vector2(0, 0);
                rectTransform.anchorMax = new Vector2(0, 1);
                rectTransform.offsetMin = new Vector2(0, margin);
                rectTransform.offsetMax = new Vector2(rectTransform.sizeDelta.x, -margin);
                break;
                
            case UIEdge.Right:
                rectTransform.anchorMin = new Vector2(1, 0);
                rectTransform.anchorMax = new Vector2(1, 1);
                rectTransform.offsetMin = new Vector2(-rectTransform.sizeDelta.x, margin);
                rectTransform.offsetMax = new Vector2(0, -margin);
                break;
        }
    }
    
    /// <summary>
    /// UI elemanını ekranın köşesine sabitler
    /// </summary>
    /// <param name="rectTransform">Sabitlenecek RectTransform</param>
    /// <param name="corner">Köşe</param>
    /// <param name="margin">Kenar boşluğu</param>
    public static void AnchorToCorner(RectTransform rectTransform, UICorner corner, float margin = 0f)
    {
        if (rectTransform == null) return;
        
        switch (corner)
        {
            case UICorner.TopLeft:
                rectTransform.anchorMin = new Vector2(0, 1);
                rectTransform.anchorMax = new Vector2(0, 1);
                rectTransform.anchoredPosition = new Vector2(margin + rectTransform.sizeDelta.x * 0.5f, 
                                                           -margin - rectTransform.sizeDelta.y * 0.5f);
                break;
                
            case UICorner.TopRight:
                rectTransform.anchorMin = new Vector2(1, 1);
                rectTransform.anchorMax = new Vector2(1, 1);
                rectTransform.anchoredPosition = new Vector2(-margin - rectTransform.sizeDelta.x * 0.5f, 
                                                           -margin - rectTransform.sizeDelta.y * 0.5f);
                break;
                
            case UICorner.BottomLeft:
                rectTransform.anchorMin = new Vector2(0, 0);
                rectTransform.anchorMax = new Vector2(0, 0);
                rectTransform.anchoredPosition = new Vector2(margin + rectTransform.sizeDelta.x * 0.5f, 
                                                           margin + rectTransform.sizeDelta.y * 0.5f);
                break;
                
            case UICorner.BottomRight:
                rectTransform.anchorMin = new Vector2(1, 0);
                rectTransform.anchorMax = new Vector2(1, 0);
                rectTransform.anchoredPosition = new Vector2(-margin - rectTransform.sizeDelta.x * 0.5f, 
                                                           margin + rectTransform.sizeDelta.y * 0.5f);
                break;
        }
    }
    
    /// <summary>
    /// UI elemanını ekranın merkezine yerleştirir
    /// </summary>
    /// <param name="rectTransform">Yerleştirilecek RectTransform</param>
    public static void CenterUIElement(RectTransform rectTransform)
    {
        if (rectTransform == null) return;
        
        rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
        rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
        rectTransform.anchoredPosition = Vector2.zero;
    }
    
    /// <summary>
    /// UI elemanını tam ekran yapar
    /// </summary>
    /// <param name="rectTransform">Tam ekran yapılacak RectTransform</param>
    public static void MakeFullScreen(RectTransform rectTransform)
    {
        if (rectTransform == null) return;
        
        rectTransform.anchorMin = Vector2.zero;
        rectTransform.anchorMax = Vector2.one;
        rectTransform.offsetMin = Vector2.zero;
        rectTransform.offsetMax = Vector2.zero;
    }
}

/// <summary>
/// UI kenarları için enum
/// </summary>
public enum UIEdge
{
    Top,
    Bottom,
    Left,
    Right
}

/// <summary>
/// UI köşeleri için enum
/// </summary>
public enum UICorner
{
    TopLeft,
    TopRight,
    BottomLeft,
    BottomRight
}
