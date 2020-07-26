using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Prefabs
{
    public PrefabName prefabName;
    public GameObject prefab;
}

[CreateAssetMenu(fileName = "New PrefabsList", menuName = "PrefabsList")]
public class PrefabsList : ScriptableObject
{
    [Header("Animal Prefabs List")]
    public List<GameObject> allAnimalPrefabs = new List<GameObject>();

    public List<Prefabs> allPrefabs = new List<Prefabs>();
    public GameObject GetAnimalPrefeb(int productId)
    {
        for (int i = 0; i < allAnimalPrefabs.Count; i++)
        {
            if (allAnimalPrefabs[i].name == productId.ToString())
            {
                return allAnimalPrefabs[i];
            }
        }
        return allAnimalPrefabs[0];
    }

    public GameObject LoadPrefab(PrefabName _prefabName, Transform transform = null)
    {
        for (int i = 0; i < allPrefabs.Count; i++)
        {
            if (allPrefabs[i].prefabName == _prefabName)
            {
                if (transform != null)
                {
                    return Instantiate(allPrefabs[i].prefab, transform);
                }
                else
                {
                    return Instantiate(allPrefabs[i].prefab);
                }
            }
        }
        return null;
    }
}
