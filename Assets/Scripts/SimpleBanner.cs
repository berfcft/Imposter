using UnityEngine;
using GoogleMobileAds.Api;

public class SimpleBanner : MonoBehaviour
{
    private BannerView bannerView;

#if UNITY_ANDROID
    // Google'un test banner birimi (Android)
    private string adUnitId = "ca-app-pub-4670320734892453/9511774220";
#elif UNITY_IPHONE
    // Google'un test banner birimi (iOS)
    private string adUnitId = "ca-app-pub-3940256099942544/2934735716";
#else
    private string adUnitId = "unexpected_platform";
#endif

    void Start()
    {
        // 320x50 banner'ý ekranýn altýna yerleþtir.
        bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);

        // ÖNEMLÝ: Yeni sürümlerde "AdRequest.Builder().Build()" YOK.
        // Doðrudan new AdRequest() kullan.
        var request = new AdRequest();

        // Olaylar (isteðe baðlý ama debug için faydalý)
        bannerView.OnBannerAdLoaded += () => Debug.Log("Banner yüklendi.");
        bannerView.OnBannerAdLoadFailed += (err) => Debug.LogError("Banner hata: " + err);
        bannerView.OnAdImpressionRecorded += () => Debug.Log("Impression.");
        bannerView.OnAdClicked += () => Debug.Log("Týklandý.");

        // Reklamý yükle
        bannerView.LoadAd(request);
    }

    public void Hide() => bannerView?.Hide();
    public void Show() => bannerView?.Show();

    void OnDestroy()
    {
        bannerView?.Destroy();
        bannerView = null;
    }
}
