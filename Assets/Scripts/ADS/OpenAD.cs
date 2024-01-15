using System;
using UnityEngine;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;


public class OpenAD : MonoBehaviour
{
    private InterstitialAd interstitial;

    public string InterID = "ca-app-pub-8814625507259202/6038771069";

    private void Start()
    {
        RequestInterstitial();
    }

    private void RequestInterstitial()
    {


        // Initialize an InterstitialAd.
        this.interstitial = new InterstitialAd(InterID);

        // Called when an ad request has successfully loaded.
        this.interstitial.OnAdLoaded += HandleOnAdLoaded;
        // Called when an ad request failed to load.
        this.interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        // Called when an ad is shown.
        this.interstitial.OnAdOpening += HandleOnAdOpening;
        // Called when the ad is closed.
        this.interstitial.OnAdClosed += HandleOnAdClosed;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        this.interstitial.LoadAd(request);
    }
    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLoaded event received");
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print("HandleFailedToReceiveAd event received with message: "
                            + args);
    }

    public void HandleOnAdOpening(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdOpening event received");
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdClosed event received");
    }

    public void ShowAd()
    {
        if (this.interstitial.IsLoaded())
        {
            this.interstitial.Show();
            RequestInterstitial();
        }
    }
}
