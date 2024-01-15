using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class Banner : MonoBehaviour
{
    private BannerView bannerView;
    public string BannerID = "ca-app-pub-8814625507259202/4917261080";

   public void Start()
    {
        DontDestroyOnLoad(gameObject);
        MobileAds.Initialize(initStatus => { });

        // Replace "your_banner_ad_unit_id" with the actual ad unit ID you obtained from AdMob.
        string adUnitId =BannerID;

        bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Top);

        // Register for ad events
        bannerView.OnAdLoaded += HandleAdLoaded;
        bannerView.OnAdFailedToLoad += HandleAdFailedToLoad;
        bannerView.OnAdOpening += HandleAdOpening;
        

        AdRequest request = new AdRequest.Builder().Build();

        bannerView.LoadAd(request);
    }

    void HandleAdLoaded(object sender, EventArgs args)
    {
        Debug.Log("Ad loaded successfully");
    }

    void HandleAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        Debug.LogError("Ad failed to load: ");
    }

    void HandleAdOpening(object sender, EventArgs args)
    {
        Debug.Log("Ad opened");
    }

    void HandleAdClosed(object sender, EventArgs args)
    {
        Debug.Log("Ad closed");
    }

    void HandleAdLeavingApplication(object sender, EventArgs args)
    {
        Debug.Log("User leaving application after clicking ad");
    }

    // You may want to destroy the banner when the scene changes or when the banner is no longer needed.
   public void OnDestroy()
    {
        if (bannerView != null)
        {
            // Unregister callbacks to prevent memory leaks
            bannerView.OnAdLoaded -= HandleAdLoaded;
            bannerView.OnAdFailedToLoad -= HandleAdFailedToLoad;
            bannerView.OnAdOpening -= HandleAdOpening;
            bannerView.OnAdClosed -= HandleAdClosed;
            

            bannerView.Destroy();
        }
    }
}
