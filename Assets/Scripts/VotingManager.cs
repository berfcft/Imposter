using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

/// <summary>
/// Oylama sistemi için yönetici sınıf.
/// Oyuncuların birbirlerine oy vermesini ve sonuçları yönetir.
/// </summary>
public class VotingManager : MonoBehaviour
{
    [Header("UI References")]
    public GameObject votingPanel;         // Oylama paneli
    public GameObject buttonPrefab;        // Oyuncu butonu prefab'ı
    public TMP_Text resultText;            // Sonuç metni
    public Button restartButton;           // Yeniden başlat butonu
    public Button mainMenuButton;          // Ana menü butonu

    [Header("Game Settings")]
    public int playerCount = 4;            // Oyuncu sayısı
    public float votingDuration = 30f;     // Oylama süresi (saniye)

    [Header("Debug")]
    public bool debugMode = false;         // Debug modu

    private Dictionary<int, int> votes;    // Oyuncu oyları
    private float votingTimer;             // Oylama sayacı
    private bool votingEnded = false;      // Oylama bitti mi?
    private List<GameObject> playerButtons; // Oyuncu butonları

    void Start()
    {
        Debug.Log("VotingManager başlatıldı");
        
        // Eğer PassAndPlay sahnesinden farklı bir oyuncu sayısı belirlendiyse UI'den ayarlananı kullanırız
        // Bu değer genelde GameSettings gibi kalıcı bir yapıdan okunur; basitlik için PlayerPrefs kullanılabilir
        if (PlayerPrefs.HasKey("PP_PlayerCount"))
        {
            playerCount = Mathf.Max(2, PlayerPrefs.GetInt("PP_PlayerCount", playerCount));
        }

        // Oyları sıfırla
        votes = new Dictionary<int, int>();
        for (int i = 0; i < playerCount; i++)
        {
            votes[i] = 0;
        }
        
        // UI'yi başlangıç durumuna getir
        InitializeUI();
        
        // Oyuncu butonlarını oluştur
        CreatePlayerButtons();
        
        // Oylama sayacını başlat
        votingTimer = votingDuration;
        
        if (debugMode)
        {
            Debug.Log($"Oylama başladı - {playerCount} oyuncu, {votingDuration} saniye");
        }
    }

    /// <summary>
    /// UI'yi başlangıç durumuna getirir
    /// </summary>
    void InitializeUI()
    {
        if (votingPanel != null) votingPanel.SetActive(true);
        if (resultText != null) resultText.text = "Oy verin:";
        
        // Butonları gizle (oylama bitince gösterilecek)
        if (restartButton != null) restartButton.gameObject.SetActive(false);
        if (mainMenuButton != null) mainMenuButton.gameObject.SetActive(false);
    }

    /// <summary>
    /// Oyuncu butonlarını oluşturur
    /// </summary>
    void CreatePlayerButtons()
    {
        playerButtons = new List<GameObject>();
        
        if (buttonPrefab == null)
        {
            Debug.LogError("Button prefab atanmamış!");
            return;
        }
        
        for (int i = 0; i < playerCount; i++)
        {
            int playerIndex = i; // Closure için local değişken
            
            // Buton oluştur
            GameObject btnObj = Instantiate(buttonPrefab, votingPanel.transform);
            playerButtons.Add(btnObj);
            
            // Buton metnini ayarla
            TMP_Text buttonText = btnObj.GetComponentInChildren<TMP_Text>();
            if (buttonText != null)
            {
                buttonText.text = $"Oyuncu {i + 1}";
            }
            
            // Buton event'ini ekle
            Button button = btnObj.GetComponent<Button>();
            if (button != null)
            {
                button.onClick.AddListener(() => OnVote(playerIndex));
            }
            
            if (debugMode)
            {
                Debug.Log($"Oyuncu {i + 1} butonu oluşturuldu");
            }
        }
    }

    void Update()
    {
        if (!votingEnded)
        {
            // Oylama sayacını güncelle
            votingTimer -= Time.deltaTime;
            
            // Süreyi göster
            if (resultText != null)
            {
                int minutes = Mathf.FloorToInt(votingTimer / 60);
                int seconds = Mathf.FloorToInt(votingTimer % 60);
                string timeText = string.Format("{0:00}:{1:00}", minutes, seconds);
                resultText.text = $"Oy verin: ({timeText})";
            }
            
            // Süre bittiyse oylamayı bitir
            if (votingTimer <= 0)
            {
                EndVoting();
            }
        }
    }

    /// <summary>
    /// Oyuncuya oy verildiğinde çağrılır
    /// </summary>
    void OnVote(int playerIndex)
    {
        if (votingEnded) return;
        
        Debug.Log($"Oy verildi: Oyuncu {playerIndex + 1}");
        
        // Oyu kaydet
        votes[playerIndex]++;
        
        // Butonu devre dışı bırak
        if (playerIndex < playerButtons.Count)
        {
            Button button = playerButtons[playerIndex].GetComponent<Button>();
            if (button != null)
            {
                button.interactable = false;
            }
        }
        
        // Tüm oyuncular oy verdi mi kontrol et
        int totalVotes = votes.Values.Sum();
        if (totalVotes >= playerCount)
        {
            EndVoting();
        }
    }

    /// <summary>
    /// Oylamayı bitirir ve sonuçları gösterir
    /// </summary>
    void EndVoting()
    {
        if (votingEnded) return;
        
        votingEnded = true;
        Debug.Log("Oylama bitti");
        
        // En çok oy alan oyuncuyu bul
        int maxVotes = votes.Values.Max();
        var votedOutPlayers = votes.Where(x => x.Value == maxVotes).ToList();
        
        // Sonucu göster
        if (resultText != null)
        {
            if (votedOutPlayers.Count == 1)
            {
                int votedOut = votedOutPlayers[0].Key;
                resultText.text = $"En çok oy alan: Oyuncu {votedOut + 1}\n" +
                                 $"Oy sayısı: {maxVotes}";
                
                Debug.Log($"Oyuncu {votedOut + 1} oylandı (Oy sayısı: {maxVotes})");
            }
            else
            {
                // Beraberlik durumu
                string tiedPlayers = string.Join(", ", votedOutPlayers.Select(p => $"Oyuncu {p.Key + 1}"));
                resultText.text = $"Beraberlik: {tiedPlayers}\n" +
                                 $"Oy sayısı: {maxVotes}";
                
                Debug.Log($"Beraberlik: {tiedPlayers} (Oy sayısı: {maxVotes})");
            }
        }
        
        // Yeniden başlat ve ana menü butonlarını göster
        ShowEndButtons();
    }

    /// <summary>
    /// Oyun sonu butonlarını gösterir
    /// </summary>
    void ShowEndButtons()
    {
        if (restartButton != null)
        {
            restartButton.gameObject.SetActive(true);
            restartButton.onClick.AddListener(OnRestartButton);
        }
        
        if (mainMenuButton != null)
        {
            mainMenuButton.gameObject.SetActive(true);
            mainMenuButton.onClick.AddListener(OnMainMenuButton);
        }
    }

    /// <summary>
    /// Yeniden başlat butonuna basıldığında çağrılır
    /// </summary>
    void OnRestartButton()
    {
        Debug.Log("Oyun yeniden başlatılıyor");
        SceneManager.LoadScene("ModeSelection");
    }

    /// <summary>
    /// Ana menü butonuna basıldığında çağrılır
    /// </summary>
    void OnMainMenuButton()
    {
        Debug.Log("Ana menüye dönülüyor");
        SceneManager.LoadScene("ModeSelection");
    }

    /// <summary>
    /// Debug için oy durumunu konsola yazdırır
    /// </summary>
    [ContextMenu("Debug Oy Durumu")]
    void DebugVoteStatus()
    {
        if (votes != null)
        {
            string voteStatus = string.Join(", ", votes.Select(v => $"Oyuncu {v.Key + 1}: {v.Value} oy"));
            Debug.Log($"Oy durumu: {voteStatus}");
        }
    }
}
