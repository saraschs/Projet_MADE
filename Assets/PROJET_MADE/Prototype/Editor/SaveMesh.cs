using UnityEngine;
using UnityEditor;
using System.IO;

public class SaveMesh
{
    [MenuItem("Tools/Save Complete Cave Prefab")]
    static void SaveCompleteCave()
    {
        GameObject MapGenerator = GameObject.Find("MapGenerator");

        if (MapGenerator == null)
        {
            Debug.LogError("Objet 'MapGenerator' introuvable !");
            return;
        }

        string folderPath = "Assets/PROJET_MADE/Prototype/Prefabs";

        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        string prefabPath = folderPath + "/Cave_" + System.DateTime.Now.Ticks + ".prefab";

        GameObject caveInstance = Object.Instantiate(MapGenerator);

        MeshFilter[] meshFilters = caveInstance.GetComponentsInChildren<MeshFilter>();

        foreach (MeshFilter mf in meshFilters)
        {
            if (mf.sharedMesh == null)
                continue;

            Mesh meshCopy = Object.Instantiate(mf.sharedMesh);

            string meshPath = folderPath + "/" + mf.gameObject.name + "_" + System.Guid.NewGuid() + ".asset";

            AssetDatabase.CreateAsset(meshCopy, meshPath);

            mf.sharedMesh = meshCopy;
        }

        PrefabUtility.SaveAsPrefabAsset(caveInstance, prefabPath);

        Object.DestroyImmediate(caveInstance);

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log("Prefab sauvegardé : " + prefabPath);
    }
}