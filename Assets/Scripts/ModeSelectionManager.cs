using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ModeSelectionManager : MonoBehaviour
{
    [Header("Ana Butonlar (Ana Menü)")]
    [SerializeField] private Button localPlayButton;    // Yerel Oyun
    [SerializeField] private Button settingsButton;     // Ayarlar (panel açar)
    [SerializeField] private Button howToPlayButton;    // Nasıl Oynanır

    [Header("Ana Menü Kökleri (panel açılınca gizlenecekler)")]
    [SerializeField] private GameObject mainMenuRoot;           // Ana menü (butonlar + görseller)
    [SerializeField] private GameObject[] hideWhenAnyPanelOpen; // Ekstra gizlenecekler (opsiyonel)

    [Header("Ayarlar Paneli")]
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private Button settingsBackButton;         // Geri/Kapat

    [Header("Nasıl Oynanır Paneli")]
    [SerializeField] private GameObject howToPlayPanel;
    [SerializeField] private Button howToPlayBackButton;        // Geri/Kapat

    [Header("Yerel (Offline) Oyun Sahnesi")]
    [SerializeField] private string localGameSceneName = "LocalGame";

    private void Start()
    {
        // Başlangıçta tüm paneller kapalı, ana menü açık olsun
        ToggleAllPanels(false);
        ShowMainMenu(true);

        // Ana menü butonları
        if (localPlayButton) localPlayButton.onClick.AddListener(OnLocalButton);
        if (settingsButton) settingsButton.onClick.AddListener(OpenSettingsPanel);
        if (howToPlayButton) howToPlayButton.onClick.AddListener(OpenHowToPlayPanel);

        // Panel içi geri/kapama butonları
        if (settingsBackButton) settingsBackButton.onClick.AddListener(CloseAllPanels);
        if (howToPlayBackButton) howToPlayBackButton.onClick.AddListener(CloseAllPanels);
    }

    private void Update()
    {
        // Android'de geri tuşu / PC'de ESC ile paneller kapanıp ana menüye dönsün
        if (Input.GetKeyDown(KeyCode.Escape))
            CloseAllPanels();
    }

    // ===================== PANEL AÇ/KAPA =====================

    public void OpenSettingsPanel()
    {
        Debug.Log("[UI] OpenSettingsPanel");
        ShowOnlyPanel(settingsPanel);
    }

    public void OpenHowToPlayPanel()
    {
        Debug.Log("[UI] OpenHowToPlayPanel");
        ShowOnlyPanel(howToPlayPanel);
    }

    /// <summary>
    /// Sadece verilen paneli açar, diğer panelleri kapatır ve ana menüyü gizler.
    /// </summary>
    private void ShowOnlyPanel(GameObject panelToOpen)
    {
        ToggleAllPanels(false);  // Diğer panelleri kapat
        ShowMainMenu(false);     // Ana menüyü gizle
        TogglePanel(panelToOpen, true); // İstenen paneli aç
    }

    /// <summary>
    /// Tüm panelleri kapatır ve ana menüyü geri getirir.
    /// </summary>
    public void CloseAllPanels()
    {
        Debug.Log("[UI] CloseAllPanels → Ana menü geri");
        ToggleAllPanels(false);
        ShowMainMenu(true);
    }

    /// <summary>
    /// Bilinen tüm panelleri (ayarlar / nasıl oynanır) topluca aç-kapa.
    /// </summary>
    private void ToggleAllPanels(bool show)
    {
        TogglePanel(settingsPanel, show);
        TogglePanel(howToPlayPanel, show);
    }

    private void TogglePanel(GameObject panel, bool show)
    {
        if (panel) panel.SetActive(show);
    }

    /// <summary>
    /// Ana menü köklerini ve istenirse ekstra objeleri aç-kapa.
    /// </summary>
    private void ShowMainMenu(bool show)
    {
        if (mainMenuRoot) mainMenuRoot.SetActive(show);

        if (hideWhenAnyPanelOpen != null)
        {
            foreach (var go in hideWhenAnyPanelOpen)
            {
                if (go) go.SetActive(show);
            }
        }
    }

    // ===================== AKIŞ =====================

    // Yerel (offline) oyun
    public void OnLocalButton()
    {
        if (!string.IsNullOrEmpty(localGameSceneName))
        {
            Debug.Log("[LOCAL] Load LocalGame");
            SceneManager.LoadSceneAsync(localGameSceneName);
        }
        else
        {
            Debug.LogWarning("[LOCAL] localGameSceneName boş!");
        }
    }
}
