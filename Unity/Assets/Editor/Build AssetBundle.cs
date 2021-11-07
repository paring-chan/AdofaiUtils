using System.IO;
using UnityEditor;

public static class BuildAssetBundle
{
    [MenuItem("Assets/Build AssetBundles")]
    static void BuildAllAssetBundles()
    {
        string assetBundleDirectory = "../AdofaiUtils/Resources/AssetBundles";
        if (!Directory.Exists(assetBundleDirectory))
        {
            Directory.CreateDirectory(assetBundleDirectory);
        }

        BuildPipeline.BuildAssetBundles(assetBundleDirectory, BuildAssetBundleOptions.None,
            EditorUserBuildSettings.activeBuildTarget);
    }
}
