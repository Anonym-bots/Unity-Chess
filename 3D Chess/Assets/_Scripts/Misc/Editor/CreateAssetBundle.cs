using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Application = UnityEngine.Device.Application;
using UnityEditor;


public class CreateAssetBundle
{

    [MenuItem("Assets/Build AssetBundles")]
    static void BuildAllAssetBundles()
    {
        string assetBundleDirectory = Path.Combine(Application.streamingAssetsPath, "AssetBundles");
        if(!Directory.Exists(assetBundleDirectory))
        {
            Directory.CreateDirectory(assetBundleDirectory);
        }
        BuildPipeline.BuildAssetBundles(assetBundleDirectory,
                                        BuildAssetBundleOptions.None,
                                        BuildTarget.StandaloneWindows);

        Logger.Instance.Log("AssetBundles built to: " + assetBundleDirectory, null, LoggerClass.System);
    }

}
