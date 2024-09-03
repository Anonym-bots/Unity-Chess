using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

// [RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class AssetBundleLoader : MonoBehaviour
{
    [SerializeField] private Image loadingBar;


    void Start()
    {
        // LoadAssetBundleFromFile();
        // StartCoroutine(LoadAssetBundleFromFileAsync());
        // StartCoroutine(LoadAssetBundleFromUrl());
    }

    private void LoadAssetBundleFromFile() 
    {
        var loaded =
            AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, "AssetBundles/Ole/AssetBundleName"));
        if(loaded == null)
        {
            Logger.Instance.Log("Failed to load AssetBundle.", this, LoggerClass.System);
            return;
        }

        GameObject prefab = loaded.LoadAsset<GameObject>("AssetName");
        Instantiate(prefab);
    }

    private IEnumerator LoadAssetBundleFromFileAsync()
    {
        var loadOperation
            = AssetBundle.LoadFromFileAsync(Path.Combine(Application.streamingAssetsPath, "AssetBundles/Ole/AssetBundleName"));

        while(!loadOperation.isDone)
        {
            Logger.Instance.Log(loadOperation.progress.ToString(), this, LoggerClass.System);
            yield return null;
        }

        var loaded = loadOperation.assetBundle;
        if(loaded == null)
        {
            Logger.Instance.Log("Failed to load AssetBundle.", this, LoggerClass.System);
            yield break;
        }

        GameObject prefab = loaded.LoadAsset<GameObject>("AssetName");  // could also do LoadAssetAsync
        Instantiate(prefab);
    }

    private IEnumerator LoadAssetBundleFromUrl()
    {
        string url = "someGoogleDriveLink";
        using(var uwr = new UnityWebRequest(url, UnityWebRequest.kHttpVerbGET))
        {
            uwr.downloadHandler = new DownloadHandlerAssetBundle(url, 0);
            uwr.SendWebRequest();

            while(!uwr.isDone)
            {
                Logger.Instance.Log(uwr.downloadProgress.ToString(), this, LoggerClass.System);
                yield return null;
            }

            AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(uwr);

            GameObject prefab = bundle.LoadAsset<GameObject>("AssetName");
            Instantiate(prefab);
        }
    }
}
