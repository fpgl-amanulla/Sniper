using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New PrefabsList", menuName = "PrefabsList")]
public class PrefabsList : ScriptableObject
{
    [Header("Animal Prefabs List")]
    public List<GameObject> allAnimalPrefabs = new List<GameObject>();

    [Header("Animal Canvas Prefabs")]
    public AnimalCanvas animalCanvasPrefab;

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
}
