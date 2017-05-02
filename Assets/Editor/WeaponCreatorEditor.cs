using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class WeaponCreatorEditor : Editor {
    [MenuItem("Assets/Create/ProjectileWeapon")]
    public static void CreateMyAsset()
    {
        ProjectileWeapon asset = ScriptableObject.CreateInstance<ProjectileWeapon>();
                
        AssetDatabase.CreateAsset(asset, "Assets/ProjectileWeapon.asset");
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();

        Selection.activeObject = asset;
    }
}
