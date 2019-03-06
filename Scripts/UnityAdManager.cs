using System.Collections;
using UnityEngine;
#if UNITY_ADS
using UnityEngine.Advertisements;
#endif

public class UnityAdManager : MonoBehaviour {

    public static UnityAdManager instance;
    private bool m_awaitingCallback = false;
    private string m_callbackPlacementID;
    
    void Awake()
    {

        DontDestroyOnLoad(this.gameObject);

        if(instance == null)
        {
            instance = this;
        }

        else
        {
            Destroy(this.gameObject);
        }
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ShowAd()
    {
        if (Advertisement.IsReady("rewardedVideo"))
        {
            Advertisement.Show("rewardedVideo");
        }
    }

    public void showUnityAds(string placementID, bool enableCallback)
    {
#if UNITY_ADS
        if (!Advertisement.IsReady(placementID))
        {
            Debug.LogWarning("Ad is not ready"+ placementID);
        }
        ShowOptions options = new ShowOptions();
        if (enableCallback)
        {
            {
                if (m_awaitingCallback)
                    return;

                m_callbackPlacementID = placementID;
                m_awaitingCallback = true;
                options.resultCallback = onAd_result;
            }
            Advertisement.Show(placementID, options);
        }
#endif
    }

#if UNITY_ADS
    private void onAd_result(ShowResult result)
    {
        m_awaitingCallback = false;
        switch (result)
        {
            case ShowResult.Finished:
                Debug.Log("Finished successfully");
                break;
            case ShowResult.Failed:
                Debug.Log("Failed to play!");
                break;
        }
    }
#endif

}
