using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PassAndPlayManager : MonoBehaviour
{
    [Header("Scenes")]
    public string mainMenuSceneName = "ModeSelection";

    [Header("Setup (Pre-Start)")]
    public GameObject setupPanel;
    public TMP_InputField playerCountInput;
    public TMP_InputField impostorCountInput;
    public Button startGameButton;
    public TMP_Text warningText;

    [Header("UI References")]
    public GameObject maskPanel;
    public GameObject revealPanel;
    public TMP_Text maskText;
    public TMP_Text wordText;
    public Button showButton;
    public Button doneButton;
    public Button backButton;
    [SerializeField] private string backButtonObjectName = "BackButton";

    [Header("Toast Layout")]
    [SerializeField] private RectTransform toastAnchor;
    [SerializeField] private Vector2 toastExtraOffset = Vector2.zero;

    [Header("Limits")]
    [SerializeField] private int minPlayers = 3;
    [SerializeField] private int maxPlayers = 50;

    [Header("Game Settings")]
    public int playerCount = 4;
    public int impostorCount = 1;
    public string mainWord = "Elma";
    public string impostorWord = "Armut";

    [Header("Debug")]
    public bool debugMode = false;

    [SerializeField] private float warningDuration = 5f;
    [SerializeField] private float warningFadeTime = 0.4f;
    private Coroutine warningRoutine;

    private List<string> words;
    private int currentPlayer = 0;

    public void Start()
    {
        Debug.Log("PassAndPlayManager başlatıldı");

        EnsureBackButtonBound();
        SetupNumericOnlyInputs();
        SetupPreStartUI();

        if (playerCountInput != null)
        {
            playerCountInput.onValueChanged.AddListener(_ =>
            {
                SanitizeNumeric(playerCountInput);
                ValidateInputsAndUpdateUI();
            });
            playerCountInput.onEndEdit.AddListener(_ =>
            {
                SanitizeNumeric(playerCountInput);
                TryToastCurrentError();
            });
        }

        if (impostorCountInput != null)
        {
            impostorCountInput.onValueChanged.AddListener(_ =>
            {
                SanitizeNumeric(impostorCountInput);
                ValidateInputsAndUpdateUI();
            });
            impostorCountInput.onEndEdit.AddListener(_ =>
            {
                SanitizeNumeric(impostorCountInput);
                TryToastCurrentError();
            });
        }

        ValidateInputsAndUpdateUI();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            OnBackButton();
    }

    void SetupPreStartUI()
    {
        if (playerCountInput != null) playerCountInput.text = playerCount.ToString();
        if (impostorCountInput != null) impostorCountInput.text = impostorCount.ToString();

        if (startGameButton != null)
        {
            startGameButton.onClick.RemoveAllListeners();
            startGameButton.onClick.AddListener(() =>
            {
                SanitizeNumeric(playerCountInput);
                SanitizeNumeric(impostorCountInput);
                ValidateInputsAndUpdateUI();

                if (!startGameButton.interactable)
                {
                    TryToastCurrentError();
                    return;
                }

                ConfirmSetupAndStart();
            });
        }

        if (setupPanel != null) setupPanel.SetActive(true);
        if (maskPanel != null) maskPanel.SetActive(false);
        if (revealPanel != null) revealPanel.SetActive(false);

        ClearWarning();
    }

    void ConfirmSetupAndStart()
    {
        int pc = ReadIntOrDefault(playerCountInput, playerCount);
        int ic = ReadIntOrDefault(impostorCountInput, impostorCount);

        string error = Validate(pc, ic);
        if (!string.IsNullOrEmpty(error))
        {
            ShowWarning(error);
            ValidateInputsAndUpdateUI();
            return;
        }

        pc = Mathf.Clamp(pc, minPlayers, maxPlayers);
        playerCount = pc;

        int maxImpostor = Mathf.Max(1, playerCount / 3);
        maxImpostor = Mathf.Min(maxImpostor, Mathf.Max(1, playerCount - 1));
        impostorCount = Mathf.Clamp(ic, 1, maxImpostor);

        var pair = WordManager.GetRandomWords();
        mainWord = pair.mainWord;
        impostorWord = pair.impostorWord;

        words = WordManager.GenerateWords(playerCount, impostorCount, mainWord, impostorWord);

        if (debugMode)
        {
            Debug.Log($"Oyuncu: {playerCount} | Impostor: {impostorCount}");
            Debug.Log($"Oluşturulan kelimeler: {string.Join(", ", words)}");
        }

        if (setupPanel != null) setupPanel.SetActive(false);
        InitializeUI();
        SetupButtonEvents();
        ShowMaskPanel();

        PlayerPrefs.SetInt("PP_PlayerCount", playerCount);
        PlayerPrefs.Save();
    }

    void InitializeUI()
    {
        if (maskPanel != null) maskPanel.SetActive(true);
        if (revealPanel != null) revealPanel.SetActive(false);
        ClearWarning();
    }

    void SetupButtonEvents()
    {
        if (showButton != null)
        {
            showButton.onClick.RemoveAllListeners();
            showButton.onClick.AddListener(OnShowButton);
        }

        if (doneButton != null)
        {
            doneButton.onClick.RemoveAllListeners();
            doneButton.onClick.AddListener(OnDoneButton);
        }
    }

    void ShowMaskPanel()
    {
        if (maskPanel != null)
        {
            maskPanel.SetActive(true);
            UpdateMaskPanel();
        }
        if (revealPanel != null) revealPanel.SetActive(false);
    }

    void UpdateMaskPanel()
    {
        if (maskText != null)
            maskText.text = $"Sıradaki: Oyuncu {currentPlayer + 1}";
    }

    void OnShowButton()
    {
        if (maskPanel != null) maskPanel.SetActive(false);
        if (revealPanel != null) revealPanel.SetActive(true);

        if (wordText != null && words != null && currentPlayer >= 0 && currentPlayer < words.Count)
        {
            string w = words[currentPlayer];
            bool isImpostor = w == impostorWord;
            wordText.text = isImpostor ? $"Kelimeniz: {w} (İmposter)" : $"Kelimeniz: {w}";
        }
    }

    void OnDoneButton()
    {
        currentPlayer++;

        if (currentPlayer >= playerCount)
        {
            if (!string.IsNullOrEmpty(mainMenuSceneName))
                SceneManager.LoadSceneAsync(mainMenuSceneName, LoadSceneMode.Single);
        }
        else
        {
            ShowMaskPanel();
        }
    }

    public void OnBackButton()
    {
        if (!string.IsNullOrEmpty(mainMenuSceneName))
            SceneManager.LoadSceneAsync(mainMenuSceneName, LoadSceneMode.Single);
    }

    void EnsureBackButtonBound()
    {
        if (backButton == null && !string.IsNullOrEmpty(backButtonObjectName))
        {
            var go = GameObject.Find(backButtonObjectName);
            if (go != null) backButton = go.GetComponent<Button>();
        }

        if (backButton != null)
        {
            backButton.onClick.RemoveAllListeners();
            backButton.onClick.AddListener(OnBackButton);
        }
    }

    void SetupNumericOnlyInputs()
    {
        SetupNumericOnly(playerCountInput);
        SetupNumericOnly(impostorCountInput);
    }

    void SetupNumericOnly(TMP_InputField field)
    {
        if (field == null) return;
        field.contentType = TMP_InputField.ContentType.IntegerNumber;
        field.lineType = TMP_InputField.LineType.SingleLine;
        field.onValidateInput = (string _, int __, char addedChar) =>
        {
            return char.IsDigit(addedChar) ? addedChar : '\0';
        };
    }

    void SanitizeNumeric(TMP_InputField field)
    {
        if (field == null) return;
        string raw = field.text ?? "";
        string digits = new string(raw.Where(char.IsDigit).ToArray());
        if (raw != digits)
        {
            int caret = field.caretPosition;
            field.text = digits;
            field.caretPosition = Mathf.Min(caret, field.text.Length);
        }
    }

    int ReadIntOrDefault(TMP_InputField field, int fallback)
    {
        if (field == null) return fallback;
        string t = field.text?.Trim();
        if (string.IsNullOrEmpty(t)) return fallback;
        if (int.TryParse(t, out int val)) return val;
        return fallback;
    }

    string Validate(int pc, int ic)
    {
        if (pc < minPlayers)
            return $"Kişi Sayısı Yetersiz (En az {minPlayers} kişi olmalı)";
        if (pc > maxPlayers)
            return $"En fazla {maxPlayers} oyuncu destekleniyor";
        if (ic < 1)
            return "En az 1 impostor olmalı";

        int maxImpostor = Mathf.Max(1, pc / 3);
        maxImpostor = Mathf.Min(maxImpostor, Mathf.Max(1, pc - 1));
        if (ic > maxImpostor)
            return $"Impostor sayısı fazla (Maksimum {maxImpostor})";

        return null;
    }

    void ValidateInputsAndUpdateUI()
    {
        int pc = ReadIntOrDefault(playerCountInput, 0);
        int ic = ReadIntOrDefault(impostorCountInput, 0);
        string error = Validate(pc, ic);
        bool valid = string.IsNullOrEmpty(error);
        if (!valid) ShowWarning(error);
        else ClearWarning();
        if (startGameButton != null) startGameButton.interactable = valid;
    }

    void TryToastCurrentError()
    {
        int pc = ReadIntOrDefault(playerCountInput, 0);
        int ic = ReadIntOrDefault(impostorCountInput, 0);
        string error = Validate(pc, ic);
        if (!string.IsNullOrEmpty(error))
            ShowCenterToast(error);
    }

    void ShowWarning(string msg) => ShowCenterToast(msg);

    void ClearWarning()
    {
        if (warningText != null)
            warningText.gameObject.SetActive(false);
    }

    void ShowCenterToast(string msg)
    {
        if (warningText == null)
        {
            Debug.LogWarning(msg);
            return;
        }

        warningText.alignment = TextAlignmentOptions.Center;
        var c = warningText.color; c.a = 1f; warningText.color = c;
        warningText.text = msg;
        warningText.gameObject.SetActive(true);

        var wrt = warningText.rectTransform;

        if (toastAnchor != null)
        {
            wrt.SetParent(toastAnchor, worldPositionStays: false);
            wrt.anchorMin = new Vector2(0.5f, 0.5f);
            wrt.anchorMax = new Vector2(0.5f, 0.5f);
            wrt.pivot = new Vector2(0.5f, 0.5f);
            wrt.anchoredPosition = toastExtraOffset;
        }
        else
        {
            wrt.SetParent(setupPanel != null ? setupPanel.transform : wrt.parent, false);
            wrt.anchorMin = wrt.anchorMax = new Vector2(0.5f, 0.5f);
            wrt.pivot = new Vector2(0.5f, 0.5f);
            wrt.anchoredPosition = Vector2.zero;
        }

        if (warningRoutine != null) StopCoroutine(warningRoutine);
        warningRoutine = StartCoroutine(CoToastLifetime());
    }

    System.Collections.IEnumerator CoToastLifetime()
    {
        float t = 0f;
        while (t < warningDuration)
        {
            t += Time.unscaledDeltaTime;
            yield return null;
        }

        float ft = 0f;
        var c = warningText.color;
        while (ft < warningFadeTime)
        {
            ft += Time.unscaledDeltaTime;
            c.a = Mathf.Lerp(1f, 0f, ft / warningFadeTime);
            warningText.color = c;
            yield return null;
        }

        warningText.gameObject.SetActive(false);
    }
}
