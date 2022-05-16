using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public static class ScriptableObjectManager 
{
    //make asset 
   public static void CreateMonster<T>(MonsterData data,string path="Inventory")where T: MonsterData
    {
        T asset = ScriptableObject.CreateInstance<T>();
        string ID = asset.GetInstanceID().ToString();
        string assetPath = AssetDatabase.GenerateUniqueAssetPath("Assets/Resources/" + path + "/" + typeof(T).ToString() +
            ID + ".asset");

        MonsterData newData = asset;
        newData.SetData(data);
        AssetDatabase.CreateAsset(asset, assetPath);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = asset;
    }
}
