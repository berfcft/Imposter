using UnityEngine;

/// <summary>
/// Ekran yönlendirmesi sorununu hızlıca çözmek için script.
/// Bu script'i her sahneye ekleyebilirsiniz.
/// </summary>
public class OrientationFix : MonoBehaviour
{
    [Header("Hızlı Düzeltme")]
    [SerializeField] private bool fixOnStart = true;
    [SerializeField] private bool forcePortrait = true;
    
    void Start()
    {
        if (fixOnStart)
        {
            FixOrientation();
        }
    }
    
    /// <summary>
    /// Ekran yönlendirmesini düzeltir
    /// </summary>
    [ContextMenu("Yönlendirmeyi Düzelt")]
    public void FixOrientation()
    {
        if (forcePortrait)
        {
            // Portrait modunu zorla
            Screen.orientation = ScreenOrientation.Portrait;
            Screen.autorotateToPortrait = true;
            Screen.autorotateToPortraitUpsideDown = false;
            Screen.autorotateToLandscapeLeft = false;
            Screen.autorotateToLandscapeRight = false;
            
            Debug.Log("[OrientationFix] Portrait modu zorlandı");
        }
        else
        {
            // Sadece normal portrait'e izin ver
            Screen.autorotateToPortrait = true;
            Screen.autorotateToPortraitUpsideDown = false;
            Screen.autorotateToLandscapeLeft = false;
            Screen.autorotateToLandscapeRight = false;
            
            Debug.Log("[OrientationFix] Yönlendirme ayarlandı");
        }
    }
    
    /// <summary>
    /// Mevcut durumu kontrol et
    /// </summary>
    [ContextMenu("Durumu Kontrol Et")]
    public void CheckStatus()
    {
        Debug.Log($"[OrientationFix] Mevcut Durum:\n" +
                 $"Yönlendirme: {Screen.orientation}\n" +
                 $"Portrait: {Screen.autorotateToPortrait}\n" +
                 $"Portrait Upside Down: {Screen.autorotateToPortraitUpsideDown}\n" +
                 $"Landscape Left: {Screen.autorotateToLandscapeLeft}\n" +
                 $"Landscape Right: {Screen.autorotateToLandscapeRight}");
    }
}
