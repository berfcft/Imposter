using UnityEngine;
using GoogleMobileAds;
using GoogleMobileAds.Api;

public class AdsBootstrap : MonoBehaviour
{
    void Start()
    {
        // SDK'yý bir kez baþlatmak yeterli.
        MobileAds.Initialize(initStatus =>
        {
            Debug.Log("Google Mobile Ads initialized.");
            // Ýstersen burada Banner'ý yükleyen baþka bir script'e haber ver.
        });
    }
}
