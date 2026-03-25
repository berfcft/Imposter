using UnityEngine;

/// <summary>
/// Özel imleç yönetimi için sınıf.
/// Unity'de varsayılan imleci özel bir imleç ile değiştirir.
/// </summary>
public class CustomCursor : MonoBehaviour
{
    [Header("Cursor Settings")]
    public Texture2D customCursor;         // Özel imleç resmi
    public Vector2 hotSpot = Vector2.zero; // İmlecin tıklama noktası
    public CursorMode cursorMode = CursorMode.Auto; // İmleç modu

    [Header("Debug")]
    public bool debugMode = false;         // Debug modu

    void Start()
    {
        // Özel imleci ayarla
        if (customCursor != null)
        {
            Cursor.SetCursor(customCursor, hotSpot, cursorMode);
            
            if (debugMode)
            {
                Debug.Log("Özel imleç ayarlandı");
            }
        }
        else
        {
            if (debugMode)
            {
                Debug.LogWarning("Özel imleç resmi atanmamış!");
            }
        }
    }

    /// <summary>
    /// İmleci varsayılan haline döndürür
    /// </summary>
    public void ResetCursor()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        
        if (debugMode)
        {
            Debug.Log("İmleç varsayılan haline döndürüldü");
        }
    }

    /// <summary>
    /// Yeni bir imleç ayarlar
    /// </summary>
    /// <param name="newCursor">Yeni imleç resmi</param>
    public void SetCustomCursor(Texture2D newCursor)
    {
        if (newCursor != null)
        {
            customCursor = newCursor;
            Cursor.SetCursor(customCursor, hotSpot, cursorMode);
            
            if (debugMode)
            {
                Debug.Log("Yeni özel imleç ayarlandı");
            }
        }
    }
}
